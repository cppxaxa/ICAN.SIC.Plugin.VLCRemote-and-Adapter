using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAN.SIC.Plugin.VLCRemote.DataTypes
{
    public class VLCCommand : IVLCCommand
    {
        private VLCCommandType commandType;

        public VLCCommand(VLCCommandType command)
        {
            this.commandType = command;
        }

        public VLCCommand(string rawCommand)
        {
            this.commandType = new VLCCommandType(rawCommand);
        }

        public VLCCommandType Command { get { return commandType; } }
    }
}
