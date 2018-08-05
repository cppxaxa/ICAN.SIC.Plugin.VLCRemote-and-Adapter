using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ICAN.SIC.Plugin.VLCRemote.DataTypes;
using VLCControl;

namespace ICAN.SIC.Plugin.VLCRemote
{
    public class VLCRemoteConnectorHelper
    {
        string vlcHost, vlcPort;
        private VLCRemoteController m_vlcControl;
        Process vlcProcess;

        public VLCRemoteConnectorHelper(VLCRemoteController controller, string vlcHost, string vlcPort)
        {
            this.m_vlcControl = controller;
            this.vlcHost = vlcHost;
            this.vlcPort = vlcPort;
        }

        public string ExecuteCommand(IVLCCommand command)
        {
            string log = "[Acknowledged]";

            if (command.Command == VLCCommandType.LaunchAndConnect)
            {
                String exePath = m_vlcControl.getVLCExe();
                if (!String.IsNullOrEmpty(exePath))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = exePath;
                    startInfo.Arguments = @"--control=rc --rc-host " + vlcHost + ":" + vlcPort;
                    vlcProcess = Process.Start(startInfo);

                    int c = 0;
                    while (c < 10000 && vlcProcess.MainWindowHandle.ToInt32() == 0)
                    {
                        Console.WriteLine("[INFO] Waiting for VLC to start");
                        Thread.Sleep(500);
                        c++;
                    }

                    if (c == 10000)
                    {
                        Console.WriteLine("[INFO] Waiting limit exceeded. You need to connect VLC after it starts");
                    }
                    else
                    {
                        // Waiting sometime for server to start
                        Thread.Sleep(1000);

                        bool connectResult = false;
                        c = 0;
                        while (c < 10 && !(connectResult = m_vlcControl.connect(vlcHost, int.Parse(vlcPort))))
                        {
                            Thread.Sleep(1000);
                            Console.WriteLine("[INFO] Trying to connect VLC");

                            if (vlcProcess.HasExited)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] VLC Exited unexpectedly");
                                Console.ResetColor();
                            }

                            c++;
                        }

                        if (connectResult)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("[INFO] VLC connected");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("[ERROR] Failed to connect VLC");
                            Console.ResetColor();
                        }
                    }
                }
                else
                {
                    log = "[INFO] VLC is not found on PC. Please run it from command line:\r\nvlc.exe --control=rc --rc-host " + vlcHost + ":" + vlcPort + ", Cannot find VLC";
                }
            }
            else if (command.Command == VLCCommandType.Connect)
            {
                bool connectResult = false;
                int c = 0;
                while (c < 10 && !(connectResult = m_vlcControl.connect(vlcHost, int.Parse(vlcPort))))
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("[INFO] Trying to connect VLC");

                    if (vlcProcess.HasExited)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[ERROR] VLC Exited unexpectedly");
                        Console.ResetColor();
                    }

                    c++;
                }

                if (connectResult)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[INFO] VLC connected");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] Failed to connect VLC");
                    Console.ResetColor();
                }
            }
            else {
                if (m_vlcControl.sendCustomCommand(command.Command.ToString()))
                {
                    log = m_vlcControl.reciveAnswer();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] Invalid VLC Command, {0}", command.Command);
                    Console.ResetColor();
                }
            }

            return log;
        }
    }
}
