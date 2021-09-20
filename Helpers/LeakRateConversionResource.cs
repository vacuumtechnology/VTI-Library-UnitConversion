using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VtiUnitConversion.Entities;

namespace VtiUnitConversion.Helpers
{
    public class LeakRateConversionResource
    {
        private string _from;
        private string _to;
        private decimal? _tempC = null;

        public string From
        {
            get
            {
                return _from;
            }

            set
            {
                _from = value;
                FromUnit = LeakRateUnit.FromName(value);
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
                ToUnit = LeakRateUnit.FromName(value);
            }
        }

        public decimal? Weight { get; set; }
        //public decimal? TempK { get; set; }
        public decimal? TempC
        {
            get
            {
                // Conversion from mol/s - set default test temp if none provided
                return (FromUnit.Equals(LeakRateUnit.MolSec) && _tempC == null) ? 0m : _tempC;
            }

            set
            {
                _tempC = value;
            }
        }

        // Grams or Ounces conversion in either direction requires a weight and temp
        public bool WeightAndTempRequired => (FromUnit.Equals(LeakRateUnit.GramsYear) ||
                                              FromUnit.Equals(LeakRateUnit.OuncesYear) ||
                                              ToUnit.Equals(LeakRateUnit.GramsYear) ||
                                              ToUnit.Equals(LeakRateUnit.OuncesYear)) ? true : false;

        // Conversion to mol/s requires tested temperature of from leak rate
        public bool TestTempRequired => (ToUnit.Equals(LeakRateUnit.MolSec)) ? true : false;

        public LeakRateUnit FromUnit { get; private set; }
        public LeakRateUnit ToUnit { get; private set; }
    }
}
