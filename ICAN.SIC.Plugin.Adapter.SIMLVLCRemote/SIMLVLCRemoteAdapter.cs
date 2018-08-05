using ICAN.SIC.Abstractions;
using ICAN.SIC.Abstractions.IMessageVariants;
using ICAN.SIC.Plugin.VLCRemote.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAN.SIC.Plugin.Adapter.SIMLVLCRemote
{
    public class SIMLVLCRemote : AbstractPlugin
    {
        public SIMLVLCRemote() : base("SIMLVLCRemoteAdapter")
        {
            hub.Subscribe<IBotResult>(this.BotResultToVLCCommand);
        }

        public void BotResultToVLCCommand(IBotResult botResult)
        {
            string prefix = "vlc:";
            string vlcRawText = String.Empty;
            if (botResult.Text.Length > prefix.Length && botResult.Text.StartsWith(prefix))
                vlcRawText = botResult.Text.Substring(prefix.Length);
            else
                return;

            vlcRawText = vlcRawText.Trim();

            VLCCommand vlcCommand;

            switch (vlcRawText)
            {
                case "step volume up":
                case "step volumeup":
                    vlcCommand = new VLCCommand(VLCCommandType.StepVolumeUp);
                    break;

                case "step volume down":
                case "step volumedown":
                    vlcCommand = new VLCCommand(VLCCommandType.StepVolumeDown);
                    break;

                case "launch and connect":
                case "launchandconnect":
                    vlcCommand = new VLCCommand(VLCCommandType.LaunchAndConnect);
                    break;

                case "fast forward":
                case "fastforward":
                    vlcCommand = new VLCCommand(VLCCommandType.FastForward);
                    break;

                case "previous":
                case "prev":
                    vlcCommand = new VLCCommand(VLCCommandType.Previous);
                    break;

                default:
                    vlcCommand = new VLCCommand(vlcRawText);
                    Console.WriteLine("Bot result as RawText");
                    break;
            }
            
            hub.Publish<VLCCommand>(vlcCommand);
        }
    }
}
