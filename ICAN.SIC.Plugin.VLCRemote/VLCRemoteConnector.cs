using ICAN.SIC.Abstractions;
using ICAN.SIC.Abstractions.IMessageVariants;
using ICAN.SIC.Plugin.VLCRemote.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLCControl;

namespace ICAN.SIC.Plugin.VLCRemote
{
    public class VLCRemote : AbstractPlugin
    {
        private readonly VLCRemoteController controller = new VLCRemoteController();
        private readonly VLCRemoteConnectorHelper helper;
        private readonly VLCRemoteConnectorUtility utility = new VLCRemoteConnectorUtility();

        public VLCRemote() : base("VLCRemoteConnector")
        {
            string vlcHost, vlcPort;

            try
            {
                vlcHost = System.Configuration.ConfigurationSettings.AppSettings["VLCHost"];
                vlcPort = System.Configuration.ConfigurationSettings.AppSettings["VLCPort"];
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VLCRemoteConnector needs a configuration specifying VLCHost and VLCPort");
                Console.WriteLine("Configuration may look as follows");
                Console.WriteLine("<?xml version=\"1.0\" encoding=\"utf - 8\" ?>\n" +
                    "< configuration >\n" +
                    "< startup >\n" +
                    "< supportedRuntime version = \"v4.0\" sku = \".NETFramework,Version=v4.5\" />\n" +
                    "</ startup >\n" +
                    "< appSettings >\n" +
                    "< add key = \"VLCHost\" value = \"127.0.0.1\" />\n" +
                    "< add key = \"VLCPort\" value = \"4444\" />\n" +
                    "</ appSettings >\n" +
                    "</ configuration >\n");
                Console.ResetColor();

                throw new Exception("VLCRemoteConnector configuration");
            }

            helper = new VLCRemoteConnectorHelper(controller, vlcHost, vlcPort);

            hub.Subscribe<IVLCCommand>(this.SignalVLC);
        }

        public override void Dispose()
        {
            
        }

        private void SignalVLC(IVLCCommand command)
        {
            string log = helper.ExecuteCommand(command);

            VLCResponse response = new VLCResponse(log);

            Log sicLog = new Log(LogType.Info, log);
            hub.Publish<ILog>(sicLog);

            hub.Publish<VLCResponse>(response);
        }
    }
}
