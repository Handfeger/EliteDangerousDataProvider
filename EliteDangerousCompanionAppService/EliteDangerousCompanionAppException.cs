﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteDangerousCompanionAppService
{
    /// <summary>Base class for exceptions thrown by the Elite:Dangerous companion app API</summary>
    public class EliteDangerousCompanionAppException : Exception
    {
        public EliteDangerousCompanionAppException() : base() { }

        public EliteDangerousCompanionAppException(string message) : base(message) { }
    }
}
