using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MCBSTM32F400
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new McbStm32EnvironmentalSvc()
            };
            ServiceBase.Run(ServicesToRun);

            //while (true)
            //{
            //    McbStm32MySql.InsertEnvironmentalData(McbStm32Http.RequestEnvironmentalData());
            //}
        }
    }
}
