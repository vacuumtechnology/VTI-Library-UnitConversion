using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtiUnitConversion.Helpers
{
    public class LeakRateTemperatureCorrectionResource
    {
        public decimal? TempCoeff { get; set; }
        public decimal? TestTempC { get; set; }
        public decimal? DesiredTempC { get; set; }
    }
}
