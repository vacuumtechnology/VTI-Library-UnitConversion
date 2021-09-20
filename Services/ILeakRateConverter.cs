using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VtiUnitConversion.Helpers;

namespace VtiUnitConversion.Services
{
    public interface ILeakRateConverter
    {
        decimal Convert(decimal value, LeakRateConversionResource resource);
        decimal CorrectForTemperature(decimal value, LeakRateTemperatureCorrectionResource resource);
        IEnumerable<string> ListLeakRateUnitQueryNames();
    }
}
