using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VtiUnitConversion.Helpers;

namespace VtiUnitConversion.Entities
{
    public class PressureUnit : Enumeration
    {
        public string DisplayName { get; private set; }

        public static PressureUnit None = new PressureUnit(0, "None", "");
        public static PressureUnit PSIG = new PressureUnit(1, "PSIG", "psig");
        public static PressureUnit PSIA = new PressureUnit(2, "PSIA", "psia");
        public static PressureUnit Torr = new PressureUnit(3, "Torr", "Torr");
        public static PressureUnit Atm = new PressureUnit(4, "Atm", "Atm");
        public static PressureUnit BarG = new PressureUnit(5, "BarG", "bar(g)");
        public static PressureUnit BarAbs = new PressureUnit(6, "BarAbs", "bar(abs)");
        public static PressureUnit MbarG = new PressureUnit(7, "MbarG", "mbar(g)");
        public static PressureUnit MbarAbs = new PressureUnit(8, "MbarAbs", "mbar(abs)");
        public static PressureUnit KgFCm2G = new PressureUnit(9, "KgFCm2G", "KgF/Cm2(g)");
        public static PressureUnit KgFCm2 = new PressureUnit(10, "KgFCm2", "KgF/Cm2");
        public static PressureUnit kPaG = new PressureUnit(11, "kPaG", "kPa(g)");
        public static PressureUnit kPa = new PressureUnit(12, "kPa", "kPa");
        public static PressureUnit MPaG = new PressureUnit(13, "MPaG", "MPa(g)");
        public static PressureUnit MPa = new PressureUnit(14, "MPa", "MPa");
        public static PressureUnit Unknown = new PressureUnit(15, "Unknown", "Unknown");
        public static PressureUnit OuterScale = new PressureUnit(16, "OuterScale", "Outer Scale");
        public static PressureUnit InnerScale = new PressureUnit(17, "InnerScale", "Inner Scale");
        public static PressureUnit LiqFill = new PressureUnit(18, "LiqFill", "Liq. Fill");
        public static PressureUnit Micron = new PressureUnit(19, "Micron", "Micron (μ)");
        public static PressureUnit InHgG = new PressureUnit(20, "Inches Mercury (g)", "in. Hg(g)");
        public static PressureUnit InHgAbs = new PressureUnit(21, "Inches Mercury (abs)", "in. Hg(abs)");
        public static PressureUnit PaG = new PressureUnit(22, "PaG", "Pa(g)");
        public static PressureUnit Pa = new PressureUnit(23, "Pa", "Pa");
        public static PressureUnit hPaG = new PressureUnit(24, "hPaG", "hPaG");
        public static PressureUnit hPa = new PressureUnit(25, "hPa", "hPa");
        public static PressureUnit mmHg = new PressureUnit(26, "Milimeters Mercury", "mmHg");
        public static PressureUnit inWC = new PressureUnit(27, "InWC", "in. W.C.");

        protected PressureUnit() { }

        public PressureUnit(int id, string name, string displayName) : base(id, name)
        {
            DisplayName = displayName;
        }

        public static IEnumerable<PressureUnit> List()
        {
            return new[]
            {
                PSIG,
                PSIA,
                Torr,
                InHgG,
                InHgAbs,
                mmHg,
                Micron,
                Atm,
                BarG,
                BarAbs,
                MbarG,
                MbarAbs,
                KgFCm2G,
                KgFCm2,
                PaG,
                Pa,
                hPaG,
                hPa,
                kPaG,
                kPa,
                MPaG,
                MPa,
                Unknown,
                OuterScale,
                InnerScale,
                LiqFill,
                inWC
            };
        }

        public static IEnumerable<PressureUnit> VGMSList(bool includeNone = false)
        {
            if (includeNone)
            {
                return new[]
                {
                    None,
                    Torr,
                    MbarAbs,
                    BarAbs,
                    PSIA,
                    Atm,
                    Micron,
                    mmHg,
                    InHgAbs,
                    Pa,
                    hPa,
                    kPa,
                    MPa,
                    inWC,
                    KgFCm2,
                    MbarG,
                    BarG,
                    PSIG,
                    InHgG,
                    PaG,
                    hPaG,
                    kPaG,
                    MPaG,
                    KgFCm2G
                };
            }
            return new[]
            {
                Torr,
                MbarAbs,
                BarAbs,
                PSIA,
                Atm,
                Micron,
                mmHg,
                InHgAbs,
                Pa,
                hPa,
                kPa,
                MPa,
                inWC,
                KgFCm2,
                MbarG,
                BarG,
                PSIG,
                InHgG,
                PaG,
                hPaG,
                kPaG,
                MPaG,
                KgFCm2G
            };
        }

        public static IEnumerable<string> ListQueryNames()
        {
            return List().Select(l => l.Name).ToList();
        }

        public static PressureUnit FromId(int id)
        {
            return List().SingleOrDefault(pu => pu.Id == id) ?? None;
        }

        public static PressureUnit FromName(string name)
        {
            return List().SingleOrDefault(lr => string.Equals(lr.Name, name, StringComparison.CurrentCultureIgnoreCase)) ?? None;
        }

        public static PressureUnit FromDisplayName(string name)
        {
            return List().SingleOrDefault(lr => string.Equals(lr.DisplayName, name, StringComparison.CurrentCultureIgnoreCase)) ?? None;
        }

        public static PressureUnit Parse(string v)
        {
            return List().SingleOrDefault(
                lr => string.Equals(lr.Id.ToString(), v, StringComparison.CurrentCultureIgnoreCase)
                 || string.Equals(lr.Name, v, StringComparison.CurrentCultureIgnoreCase)
                 || string.Equals(lr.DisplayName, v, StringComparison.CurrentCultureIgnoreCase)
                ) ?? Unknown;
        }
    }
}
