using ICAN.SIC.Plugin.VLCRemote.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAN.SIC.Plugin.VLCRemote.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            VLCRemoteConnector connector = new VLCRemoteConnector();

            connector.Hub.Subscribe<IVLCResponse>(printResponse);

            Console.WriteLine("Menu");

            Console.WriteLine("1. Play");
            Console.WriteLine("2. Help");
            Console.WriteLine("3. Next");
            Console.WriteLine("4. Previous");
            Console.WriteLine("5. Step Volume Up");
            Console.WriteLine("6. Step Volume Down");
            Console.WriteLine("7. Quit");
            Console.WriteLine("8. Connect");
            Console.WriteLine("9. Launch and connect");
            Console.WriteLine("0. Pause");
            Console.WriteLine("a. Stop");

            while (true)
            {
                ConsoleKeyInfo response = Console.ReadKey();

                VLCCommand command;

                switch (response.KeyChar)
                {
                    case 'a':
                        command = new VLCCommand(VLCCommandType.Stop);
                        connector.Hub.Publish<VLCCommand>(command);
                        break;

                    case '0':
                        command = new VLCCommand(VLCCommandType.Pause);
                        connector.Hub.Publish<VLCCommand>(command);
                        break;

                    case '1':
                        command = new VLCCommand(VLCCommandType.Play);
                        connector.Hub.Publish<VLCCommand>(command);
                        break;

                    case '2':
                        command = new VLCCommand(VLCCommandType.Help);
                        connector.Hub.Publish<VLCCommand>(command);
                        break;

                    case '3':
                        command = new VLCCommand(VLCCommandType.Next);
                        connector.Hub.Publish<VLCCommand>(command);
                        break;

                    case '4':
                        command = new VLCCommand(VLCCommandType.Previous);
                        connector.Hub.Publish<VLCCommand>(command);
                        break;

                    case '5':
                        command = new VLCCommand(VLCCommandType.StepVolumeUp);
                        connector.Hub.Publish<VLCCommand>(command);
                        break;

                    case '6':
                        command = new VLCCommand(VLCCommandType.StepVolumeDown);
                        connector.Hub.Publish<VLCCommand>(command);
                        break;

                    case '7':
                        command = new VLCCommand(VLCCommandType.Quit);
                        connector.Hub.Publish<VLCCommand>(command);
                        break;

                    case '8':
                        command = new VLCCommand(VLCCommandType.Connect);
                        connector.Hub.Publish<VLCCommand>(command);
                        break;

                    case '9':
                        command = new VLCCommand(VLCCommandType.LaunchAndConnect);
                        connector.Hub.Publish<VLCCommand>(command);
                        break;
                }
            }
        }

        private static void printResponse(IVLCResponse obj)
        {
            Console.WriteLine(obj.Log);
        }
    }
}
