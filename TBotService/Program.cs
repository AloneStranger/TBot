using System.ServiceProcess;

namespace TBotService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
//#if debug
            new TBotSrv("").
//#elif
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new TBotSrv()
            };
            ServiceBase.Run(ServicesToRun);

//#endif
        }
    }
}
