using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBSTM32F400
{
    public class McbStm32Environment
    {
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public double? CoreTemperature { get; set; }
        public DateTime? DeviceTime { get; set; }
        public DateTime? AcquisitionTime { get; set; }
    }
}
