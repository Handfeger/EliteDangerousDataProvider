﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace EliteDangerousNetLogMonitor
{
    public class LogMonitor
    {
        // What we are monitoring and what to do with it
        private string directory;
        private Regex filter;
        private Action<string> callback;

        // Keep track of status
        private bool running;

        public LogMonitor(string directory, string filter, Action<string> callback)
        {
            this.directory = directory;
            this.filter = new Regex(filter);
            this.callback = callback;
        }

        /// <summary>Monitor the netlog for changes, running a callback when the file changes</summary>
        public void start()
        {
            running = true;

            // Start off by moving to the end of the file
            long lastSize = 0;
            FileInfo fileInfo = FindLatestFile(directory, filter);
            if (fileInfo != null)
            {
                lastSize = fileInfo.Length;
            }

            // Main loop
            while (running)
            {
                fileInfo = FindLatestFile(directory, filter);

                if (fileInfo == null)
                {
                    lastSize = 0;
                }
                else
                {
                    fileInfo.Refresh();
                    long thisSize = fileInfo.Length;
                    long seekPos = 0;
                    int readLen = 0;
                    if (lastSize != thisSize)
                    {
                        if (thisSize > lastSize)
                        {
                            // File has been appended - read the remaining info
                            seekPos = lastSize;
                            readLen = (int)(thisSize - lastSize);
                        }
                        else if (thisSize < lastSize)
                        {
                            // File has been truncated - read all of the info
                            seekPos = 0;
                            readLen = (int)thisSize;
                        }

                        using (FileStream fs = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            fs.Seek(seekPos, SeekOrigin.Begin);
                            byte[] bytes = new byte[readLen];
                            int haveRead = 0;
                            while (haveRead < readLen)
                            {
                                haveRead += fs.Read(bytes, haveRead, readLen - haveRead);
                                fs.Seek(seekPos + haveRead, SeekOrigin.Begin);
                            }
                            // Convert bytes to string
                            string s = Encoding.UTF8.GetString(bytes);
                            string[] lines = Regex.Split(s, "\r?\n");
                            foreach (string line in lines)
                            {
                                callback(line);
                            }
                        }
                    }
                    lastSize = thisSize;
                }
                Thread.Sleep(1000);
            }
        }

        public void stop()
        {
            running = false;
        }

        /// <summary>Find the latest file in a given directory matching a given expression, or null if no such file exists</summary>
        private static FileInfo FindLatestFile(string path, Regex filter = null)
        {
            var directory = new DirectoryInfo(path);
            try
            {
                return directory.GetFiles().Where(f => filter == null || filter.IsMatch(f.Name)).OrderByDescending(f => f.LastWriteTime).First();
            }
            catch
            {
                return null;
            }
        }
    }
}
