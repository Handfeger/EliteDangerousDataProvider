<<<<<<< HEAD
﻿using Newtonsoft.Json;
using System;
using System.IO;
=======
﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
>>>>>>> b952a7bbf1d7e8398b6dbe0f5a0d32131def331e

namespace EliteDangerousSpeechService
{
    /// <summary>
    /// Storage for the Text-to-Speech Configs
    /// </summary>
    public class SpeechServiceConfiguration
    {
<<<<<<< HEAD
        [JsonProperty("StandardVoice")]
        public String StandardVoice { get; set;  }
=======
        [JsonProperty("standardVoice")]
        public String StandardVoice { get; set; }

        [JsonProperty("effectsLevel")]
        public int EffectsLevel { get; set; } = 50;

        [JsonProperty("distortOnDamage")]
        public bool DistortOnDamage { get; set; } = true;

        [JsonProperty("rate")]
        public int Rate{ get; set; } = 0;
>>>>>>> b952a7bbf1d7e8398b6dbe0f5a0d32131def331e

        [JsonIgnore]
        private String dataPath;

        /// <summary>
        /// Obtain speech config from a file. If  If the file name is not supplied the the default
<<<<<<< HEAD
        /// path of %APPDATA%\EDDI\edsm.json is used
        /// </summary>
        /// <param name="filename"></param>
        public static SpeechServiceConfiguration FromFile(string filename=null)
        {
            if (filename==null)
=======
        /// path of %APPDATA%\EDDI\speech.json is used
        /// </summary>
        /// <param name="filename"></param>
        public static SpeechServiceConfiguration FromFile(string filename = null)
        {
            if (filename == null)
>>>>>>> b952a7bbf1d7e8398b6dbe0f5a0d32131def331e
            {
                String dataDir = Environment.GetEnvironmentVariable("AppData") + "\\EDDI";
                Directory.CreateDirectory(dataDir);
                filename = dataDir + "\\speech.json";
            }

            SpeechServiceConfiguration speech;
            try
            {
                String configData = File.ReadAllText(filename);
                speech = JsonConvert.DeserializeObject<SpeechServiceConfiguration>(configData);
            }
            catch
            {
                speech = new SpeechServiceConfiguration();
            }

            speech.dataPath = filename;
            return speech;
        }

        /// <summary>
        /// Clear the information held by speech
        /// </summary>
        public void Clear()
        {
            StandardVoice = null;
<<<<<<< HEAD
=======
            EffectsLevel = 50;
            DistortOnDamage = true;
>>>>>>> b952a7bbf1d7e8398b6dbe0f5a0d32131def331e
        }

        public void ToFile(string filename = null)
        {
            if (filename == null)
            {
                filename = dataPath;
            }
            if (filename == null)
            {
                String dataDir = Environment.GetEnvironmentVariable("AppData") + "\\EDDI";
                Directory.CreateDirectory(dataDir);
                filename = dataDir + "\\speech.json";
            }

            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(filename, json);
        }
    }
}
