using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

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
            if (!string.IsNullOrWhiteSpace(args) && args.Equals("console", StringComparison.OrdinalIgnoreCase))
            {
                EasyDNSConsole consoleRunner = new EasyDNSConsole();
                consoleRunner.Start(null);
                Console.ReadKey();
                consoleRunner.Stop();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new EasyDNSService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }

        internal static WriteEventLogEntryDelegate EventLogEntryDelegate;
    }
}
