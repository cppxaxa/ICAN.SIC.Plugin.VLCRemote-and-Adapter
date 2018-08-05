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
    public class SIMLVLCRemoteAdapter : AbstractPlugin
    {
        public SIMLVLCRemoteAdapter() : base("SIMLVLCRemoteAdapter")
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

            IVLCCommand vlcCommand = new VLCCommand(vlcRawText);
            hub.Publish<IVLCCommand>(vlcCommand);
        }
    }
}
