using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAN.SIC.Plugin.VLCRemote.DataTypes
{
    public class VLCCommandType
    {
        public readonly static VLCCommandType Play = new VLCCommandType("play");
        public readonly static VLCCommandType Pause = new VLCCommandType("pause");
        public readonly static VLCCommandType Stop = new VLCCommandType("stop");
        public readonly static VLCCommandType StepVolumeUp = new VLCCommandType("volup 1");
        public readonly static VLCCommandType StepVolumeDown = new VLCCommandType("voldown 1");
        public readonly static VLCCommandType Help = new VLCCommandType("help");
        public readonly static VLCCommandType Quit = new VLCCommandType("quit");
        public readonly static VLCCommandType Connect = new VLCCommandType("connect");
        public readonly static VLCCommandType LaunchAndConnect = new VLCCommandType("launchAndConnect");
        public readonly static VLCCommandType Next = new VLCCommandType("next");
        public readonly static VLCCommandType Previous = new VLCCommandType("prev");
        public readonly static VLCCommandType FastForward = new VLCCommandType("fastforward");
        public readonly static VLCCommandType Rewind = new VLCCommandType("rewind");
        public readonly static VLCCommandType Faster = new VLCCommandType("faster");
        public readonly static VLCCommandType Slower = new VLCCommandType("slower");
        public readonly static VLCCommandType Normal = new VLCCommandType("normal");

        private string command;
        public string Command { get { return command; } }

        public VLCCommandType(string commandText)
        {
            command = commandText;
        }

        public override string ToString()
        {
            return command;
        }
    }
}
