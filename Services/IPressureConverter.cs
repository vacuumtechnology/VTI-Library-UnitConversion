using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VtiUnitConversion.Helpers;

namespace VtiUnitConversion.Services
{
    public interface IPressureConverter
    {
        decimal Convert(decimal value, PressureConversionResource resource);
        IEnumerable<string> ListPressureUnitQueryNames();
    }
}
