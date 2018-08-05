using ICAN.SIC.Abstractions.IMessageVariants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAN.SIC.Plugin.VLCRemote.DataTypes
{
    public class Log : ILog
    {
        LogType logType;
        string message;

        public Log(LogType logType, string message)
        {
            this.logType = logType;
            this.message = message;
        }

        public LogType LogType { get { return this.logType; } }

        public string Message { get { return this.message; } }
    }
}
