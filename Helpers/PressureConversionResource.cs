using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VtiUnitConversion.Entities;

namespace VtiUnitConversion.Helpers
{
    public class PressureConversionResource
    {
        public PressureUnit[] EquivalentUnits = { PressureUnit.PSIG,
                                                  PressureUnit.Unknown,
                                                  PressureUnit.OuterScale,
                                                  PressureUnit.InnerScale,
                                                  PressureUnit.LiqFill
                                                };

        private string _from;
        private string _to;

        public string From
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
                FromUnit = PressureUnit.FromName(value);
            }
        }

        public string To
        {
            get
            {
                return _to;
            }
            set
            {
                _to = value;
                ToUnit = PressureUnit.FromName(value);
            }
        }

        public PressureUnit FromUnit { get; private set; }
        public PressureUnit ToUnit { get; private set; }

        // Check for units that represent the same mathmatical value
        public bool ToAndFromAreEquivalent()
        {
            return EquivalentUnits.Contains(FromUnit) && EquivalentUnits.Contains(ToUnit);
        }
    }
}
