using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VtiUnitConversion.Entities;
using VtiUnitConversion.Helpers;

namespace VtiUnitConversion.Services
{
    public class PressureConverter : IPressureConverter
    {
        // Unit to base all conversions on
        private static readonly PressureUnit BaseUnit = PressureUnit.PSIG;


        // Containers to hold all the conversion formulas
        public static ConcurrentDictionary<PressureUnit, Func<decimal, decimal>> ConversionsTo = new ConcurrentDictionary<PressureUnit, Func<decimal, decimal>>();
        public static ConcurrentDictionary<PressureUnit, Func<decimal, decimal>> ConversionsFrom = new ConcurrentDictionary<PressureUnit, Func<decimal, decimal>>();
        public static ConcurrentDictionary<PressureUnit, Func<double, double>> DoubleConversionsTo = new ConcurrentDictionary<PressureUnit, Func<double, double>>();
        public static ConcurrentDictionary<PressureUnit, Func<double, double>> DoubleConversionsFrom = new ConcurrentDictionary<PressureUnit, Func<double, double>>();

        public PressureConverter()
        {
            // Add conversion formulas to dictionary
            AddConversions(PressureUnit.PSIG, v => v, v => v); // root unit
            AddConversions(PressureUnit.PSIA, v => v + 14.69593m, v => v - 14.69593m);
            AddConversions(PressureUnit.Torr, v => (v + 14.69593m) * 51.715m, v => (v / 51.715m) - 14.69593m);
            AddConversions(PressureUnit.Micron, v => (v + 14.69593m) * 51.715m * 1000m, v => (v / 1000m / 51.715m) - 14.69593m);
            AddConversions(PressureUnit.Atm, v => (v + 14.69593m) * 0.06804m, v => (v / 0.06804m) - 14.69593m);
            AddConversions(PressureUnit.BarG, v => v * 0.0689475m, v => v / 0.0689475m);
            AddConversions(PressureUnit.BarAbs, v => (v + 14.69593m) * 0.0689475m, v => (v / 0.0689475m) - 14.69593m);
            AddConversions(PressureUnit.MbarG, v => v * 68.9475m, v => v / 68.9475m);
            AddConversions(PressureUnit.MbarAbs, v => (v + 14.69593m) * 68.9475m, v => (v / 68.9475m) - 14.69593m);
            AddConversions(PressureUnit.KgFCm2G, v => v * 0.070307m, v => v / 0.070307m);
            AddConversions(PressureUnit.kPaG, v => v * 6.89475m, v => v / 6.89475m);
            AddConversions(PressureUnit.MPaG, v => v * 6.89475m / 1000, v => v * 1000 / 6.89475m);
            AddConversions(PressureUnit.kPa, v => (v + 14.69593m) * 6.89475m, v => (v / 6.89475m) - 14.69593m);
            AddConversions(PressureUnit.MPa, v => (v + 14.69593m) * 6.89475m / 1000, v => (v * 1000 / 6.89475m) - 14.69593m);
            AddConversions(PressureUnit.KgFCm2, v => (v + 14.69593m) * 0.070307m, v => (v / 0.070307m) - 14.69593m);

            AddDoubleConversions(PressureUnit.PSIG, v => v, v => v); // root unit
            AddDoubleConversions(PressureUnit.PSIA, v => v + 14.69593d, v => v - 14.69593d);
            AddDoubleConversions(PressureUnit.Torr, v => (v + 14.69593d) * 51.715d, v => (v / 51.715d) - 14.69593d);
            AddDoubleConversions(PressureUnit.Micron, v => (v + 14.69593d) * 51.715d * 1000d, v => (v / 1000d / 51.715d) - 14.69593d);
            AddDoubleConversions(PressureUnit.Atm, v => (v + 14.69593d) * 0.06804d, v => (v / 0.06804d) - 14.69593d);
            AddDoubleConversions(PressureUnit.BarG, v => v * 0.0689475d, v => v / 0.0689475d);
            AddDoubleConversions(PressureUnit.BarAbs, v => (v + 14.69593d) * 0.0689475d, v => (v / 0.0689475d) - 14.69593d);
            AddDoubleConversions(PressureUnit.MbarG, v => v * 68.9475d, v => v / 68.9475d);
            AddDoubleConversions(PressureUnit.MbarAbs, v => (v + 14.69593d) * 68.9475d, v => (v / 68.9475d) - 14.69593d);
            AddDoubleConversions(PressureUnit.KgFCm2G, v => v * 0.070307d, v => v / 0.070307d);
            AddDoubleConversions(PressureUnit.kPaG, v => v * 6.89475d, v => v / 6.89475d);
            AddDoubleConversions(PressureUnit.MPaG, v => v * 6.89475d / 1000, v => v * 1000 / 6.89475d);
            AddDoubleConversions(PressureUnit.kPa, v => (v + 14.69593d) * 6.89475d, v => (v / 6.89475d) - 14.69593d);
            AddDoubleConversions(PressureUnit.MPa, v => (v + 14.69593d) * 6.89475d / 1000, v => (v * 1000 / 6.89475d) - 14.69593d);
            AddDoubleConversions(PressureUnit.KgFCm2, v => (v + 14.69593d) * 0.070307d, v => (v / 0.070307d) - 14.69593d);
        }

        protected static void AddConversions(PressureUnit convertToUnit,
                                             Func<decimal, decimal> convertToFunc,
                                             Func<decimal, decimal> convertFromFunc)
        {
            ConversionsTo.TryAdd(convertToUnit, convertToFunc);
            ConversionsFrom.TryAdd(convertToUnit, convertFromFunc);
        }
        protected static void AddDoubleConversions(PressureUnit convertToUnit,
                                             Func<double, double> convertToFunc,
                                             Func<double, double> convertFromFunc)
        {
            DoubleConversionsTo.TryAdd(convertToUnit, convertToFunc);
            DoubleConversionsFrom.TryAdd(convertToUnit, convertFromFunc);
        }

        public double Convert(double value, PressureConversionResource resource)
        {
            // Segment resource parameters
            PressureUnit from = resource.FromUnit;
            PressureUnit to = resource.ToUnit;

            // Ignore conversion if units match
            if (from.Equals(to)) { return value; }

            // Ignore conversion if units are equivalent
            if (resource.ToAndFromAreEquivalent()) { return value; }

            double newUnitValue = value;

            try
            {
                // Convert to base unit if needed
                double baseUnitValue = from.Equals(BaseUnit) ? value : DoubleConversionsFrom[from](value);

                // Convert to desired unit
                newUnitValue = to.Equals(BaseUnit) ? baseUnitValue : DoubleConversionsTo[to](baseUnitValue);
            }
            catch (KeyNotFoundException ex)
            {
                newUnitValue = 0;
            }
            catch (Exception ex)
            {
                 // unified logger?
            }

            return newUnitValue;
        }

        public decimal Convert(decimal value, PressureConversionResource resource)
        {
            // Segment resource parameters
            PressureUnit from = resource.FromUnit;
            PressureUnit to = resource.ToUnit;

            // Ignore conversion if units match
            if (from.Equals(to)) { return value; }

            // Ignore conversion if units are equivalent
            if (resource.ToAndFromAreEquivalent()) { return value; }

            decimal newUnitValue = value;

            try
            {
                // Convert to base unit if needed
                decimal baseUnitValue = from.Equals(BaseUnit) ? value : ConversionsFrom[from](value);

                // Convert to desired unit
                newUnitValue = to.Equals(BaseUnit) ? baseUnitValue : ConversionsTo[to](baseUnitValue);
            }
            catch (KeyNotFoundException ex)
            {
                newUnitValue = 0;
            }
            catch (Exception ex)
            {
                // unified logger?
            }

            return newUnitValue;
        }

        public IEnumerable<string> ListPressureUnitQueryNames()
        {
            return PressureUnit.ListQueryNames();
        }
    }
}
