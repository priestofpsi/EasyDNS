using System;
using System.Linq;
using System.ServiceProcess;

namespace theDiary.EasyDNS.Windows.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();
            Main(args.LastOrDefault());
        }

        static void Main(string args)
        {
            switch (args.ToLower())
            {
                case "console":
                    EasyDNSConsole consoleRunner = new EasyDNSConsole();
                    consoleRunner.Start(null);
                    Console.ReadKey();
                    consoleRunner.Stop();                    
                    break;
                default:
                    ServiceBase[] ServicesToRun;
                    ServicesToRun = new ServiceBase[] { new EasyDNSService() };
                    ServiceBase.Run(ServicesToRun);
                    break;
            }
        }

        internal static WriteEventLogEntryDelegate EventLogEntryDelegate;
    }
}
