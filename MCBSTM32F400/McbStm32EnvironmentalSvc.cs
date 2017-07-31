using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MCBSTM32F400
{
    public partial class McbStm32EnvironmentalSvc : ServiceBase
    {
        public McbStm32EnvironmentalSvc()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Set up a timer to trigger every minute.  
            Timer timer = new Timer();
            timer.Interval = 60000; // 60 seconds  
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        protected override void OnStop()
        {
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            McbStm32MySql.InsertEnvironmentalData(McbStm32Http.RequestEnvironmentalData());
        }
    }
}
