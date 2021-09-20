using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VtiUnitConversion.Helpers;

namespace VtiUnitConversion.Entities
{
    public class LeakRateUnit : Enumeration
    {
        public string DisplayName { get; private set; }

        public static LeakRateUnit None = new LeakRateUnit(0, "None", "");
        public static LeakRateUnit AtmCCSec = new LeakRateUnit(1, "AtmCCSec", "atm-cc/s");
        public static LeakRateUnit AtmCCMin = new LeakRateUnit(2, "AtmCCMin", "atm-cc/min");
        public static LeakRateUnit AtmCCHour = new LeakRateUnit(3, "AtmCCHour", "atm-cc/hr");
        public static LeakRateUnit TorrLSec = new LeakRateUnit(4, "TorrLSec", "Torr-L/s");
        public static LeakRateUnit GramsYear = new LeakRateUnit(5, "GramsYear", "g/y");
        public static LeakRateUnit OuncesYear = new LeakRateUnit(6, "OuncesYear", "oz/yr");
        public static LeakRateUnit MbarLSec = new LeakRateUnit(7, "MbarLSec", "mbar-L/s");
        public static LeakRateUnit PaM3Sec = new LeakRateUnit(8, "PaM3Sec", "Pa-m3/s");
        public static LeakRateUnit MolSec = new LeakRateUnit(9, "MolSec", "mol/s");
        public static LeakRateUnit ULSec = new LeakRateUnit(10, "uLSec", "\u00b5l/s"); // \u00b5 is micro sign
        public static LeakRateUnit ULMin = new LeakRateUnit(11, "uLMin", "\u00b5l/min");
        public static LeakRateUnit ULHour = new LeakRateUnit(12, "uLHour", "\u00b5l/hr");
        public static LeakRateUnit SCFM = new LeakRateUnit(13, "SCFM", "SCFM");

        protected LeakRateUnit() { }

        public LeakRateUnit(int id, string name, string displayName) : base(id, name)
        {
            DisplayName = displayName;
        }

        public static IEnumerable<LeakRateUnit> List()
        {
            return new[]
            {
                AtmCCSec,
                AtmCCMin,
                AtmCCHour,
                TorrLSec,
                GramsYear,
                OuncesYear,
                MbarLSec,
                PaM3Sec,
                MolSec,
                ULSec,
                ULMin,
                ULHour,
                SCFM
            };
        }

        public static IEnumerable<string> ListQueryNames()
        {
            return List().Select(l => l.Name).ToList();
        }

        public static LeakRateUnit FromId(int id)
        {
            return List().SingleOrDefault(lr => lr.Id == id);
        }

        public static LeakRateUnit FromName(string name)
        {
            return List().SingleOrDefault(lr => string.Equals(lr.Name, name, StringComparison.OrdinalIgnoreCase)) ?? None;
        }

        public static LeakRateUnit Parse(string v)
        {
            return List().SingleOrDefault(
                lr => string.Equals(lr.Id.ToString(), v, StringComparison.CurrentCultureIgnoreCase)
                 || string.Equals(lr.Name, v, StringComparison.CurrentCultureIgnoreCase)
                 || string.Equals(lr.DisplayName, v, StringComparison.CurrentCultureIgnoreCase)
                ) ?? None;
        }
    }
}
