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
    public class LeakRateConverter : ILeakRateConverter
    {
        // Unit to base all conversions on
        private static readonly LeakRateUnit BaseUnit = LeakRateUnit.AtmCCSec;

        // Conversion factor for mass based calculations
        static readonly decimal gramConversionFactor = 3600 * 24 * 365 * 273.15m / 22400;
        static readonly decimal ounceConversionFacator = gramConversionFactor / 28.35m;

        static readonly double gramDoubleConversionFactor = 3600 * 24 * 365 * 273.15d / 22400;
        static readonly double ounceDoubleConversionFacator = gramDoubleConversionFactor / 28.35d;

        // Containers to hold all the conversion formulas
        static ConcurrentDictionary<LeakRateUnit, Func<decimal, decimal?, decimal?, decimal>> ConversionsTo = new ConcurrentDictionary<LeakRateUnit, Func<decimal, decimal?, decimal?, decimal>>();
        static ConcurrentDictionary<LeakRateUnit, Func<decimal, decimal?, decimal?, decimal>> ConversionsFrom = new ConcurrentDictionary<LeakRateUnit, Func<decimal, decimal?, decimal?, decimal>>();


        static ConcurrentDictionary<LeakRateUnit, Func<double, double?, double?, double>> DoubleConversionsTo = new ConcurrentDictionary<LeakRateUnit, Func<double, double?, double?, double>>();
        static ConcurrentDictionary<LeakRateUnit, Func<double, double?, double?, double>> DoubleConversionsFrom = new ConcurrentDictionary<LeakRateUnit, Func<double, double?, double?, double>>();

        public LeakRateConverter()
        {
            // Add conversion formulas to dictionary
            AddConversions(LeakRateUnit.AtmCCMin, (v, w, t) => v * 60, (v, w, t) => v / 60);
            AddConversions(LeakRateUnit.AtmCCHour, (v, w, t) => v * 3600, (v, w, t) => v / 3600);
            AddConversions(LeakRateUnit.ULSec, (v, w, t) => v / 1000, (v, w, t) => v * 1000);
            AddConversions(LeakRateUnit.ULMin, (v, w, t) => v / (60 * 1000), (v, w, t) => v * (60 * 1000));
            AddConversions(LeakRateUnit.ULHour, (v, w, t) => v / (3600 * 1000), (v, w, t) => v * (3600 * 1000));
            AddConversions(LeakRateUnit.TorrLSec, (v, w, t) => v * 0.76m, (v, w, t) => v / 0.76m);
            AddConversions(LeakRateUnit.MbarLSec, (v, w, t) => v * 1.0108m, (v, w, t) => v / 1.0108m);
            AddConversions(LeakRateUnit.PaM3Sec, (v, w, t) => v * 0.1013m, (v, w, t) => v / 0.1013m);

            AddConversions(LeakRateUnit.GramsYear, (v, w, t) => v * gramConversionFactor * (decimal)w / (273.15m + (decimal)t),
                                                   (v, w, t) => v * (273.15m + (decimal)t) / (gramConversionFactor * (decimal)w));

            AddConversions(LeakRateUnit.OuncesYear, (v, w, t) => v * ounceConversionFacator * (decimal)w / (273.15m + (decimal)t),
                                                    (v, w, t) => v * (273.15m + (decimal)t) / (ounceConversionFacator * (decimal)w));

            AddConversions(LeakRateUnit.MolSec, (v, w, t) => v / (82.0573m * (273.15m + (decimal)t)),
                                                (v, w, t) => v * (82.0573m * (273.15m + (decimal)t)));
            AddConversions(LeakRateUnit.SCFM, (v, w, t) => v * 0.0021m, (v, w, t) => v / 0.0021m);
            AddConversions(LeakRateUnit.SCIM, (v, w, t) => v * 0.0021m * 12.0m, (v, w, t) => v / 12.0m / 0.0021m);

            AddDoubleConversions(LeakRateUnit.AtmCCMin, (v, w, t) => v * 60, (v, w, t) => v / 60);
            AddDoubleConversions(LeakRateUnit.AtmCCHour, (v, w, t) => v * 3600, (v, w, t) => v / 3600);
            AddDoubleConversions(LeakRateUnit.ULSec, (v, w, t) => v * 1000, (v, w, t) => v / 1000);
            AddDoubleConversions(LeakRateUnit.ULMin, (v, w, t) => v * (60 * 1000), (v, w, t) => v / (60 * 1000));
            AddDoubleConversions(LeakRateUnit.ULHour, (v, w, t) => v * (3600 * 1000), (v, w, t) => v / (3600 * 1000));
            AddDoubleConversions(LeakRateUnit.TorrLSec, (v, w, t) => v * 0.76d, (v, w, t) => v / 0.76d);
            AddDoubleConversions(LeakRateUnit.MbarLSec, (v, w, t) => v * 1.0108d, (v, w, t) => v / 1.0108d);
            AddDoubleConversions(LeakRateUnit.PaM3Sec, (v, w, t) => v * 0.1013d, (v, w, t) => v / 0.1013d);

            AddDoubleConversions(LeakRateUnit.GramsYear, (v, w, t) => v * gramDoubleConversionFactor * (double)w / (273.15d + (double)t),
                                                   (v, w, t) => v * (273.15d + (double)t) / (gramDoubleConversionFactor * (double)w));

            AddDoubleConversions(LeakRateUnit.OuncesYear, (v, w, t) => v * ounceDoubleConversionFacator * (double)w / (273.15d + (double)t),
                                                    (v, w, t) => v * (273.15d + (double)t) / (ounceDoubleConversionFacator * (double)w));

            AddDoubleConversions(LeakRateUnit.MolSec, (v, w, t) => v / (82.0573d * (273.15d + (double)t)),
                                                (v, w, t) => v * (82.0573d * (273.15d + (double)t)));
            AddDoubleConversions(LeakRateUnit.SCFM, (v, w, t) => v * 0.0021d, (v, w, t) => v / 0.0021d);
            AddDoubleConversions(LeakRateUnit.SCIM, (v, w, t) => v * 0.0021d * 12.0d, (v, w, t) => v / 12.0d / 0.0021d);
        }

        protected static void AddConversions(LeakRateUnit convertToUnit,
                                             Func<decimal, decimal?, decimal?, decimal> convertToFunc,
                                             Func<decimal, decimal?, decimal?, decimal> convertFromFunc)
        {
            ConversionsTo.TryAdd(convertToUnit, convertToFunc);
            ConversionsFrom.TryAdd(convertToUnit, convertFromFunc);
        }

        protected static void AddDoubleConversions(LeakRateUnit convertToUnit,
                                             Func<double, double?, double?, double> convertToFunc,
                                             Func<double, double?, double?, double> convertFromFunc)
        {
            DoubleConversionsTo.TryAdd(convertToUnit, convertToFunc);
            DoubleConversionsFrom.TryAdd(convertToUnit, convertFromFunc);
        }

        public double Convert(double value, LeakRateConversionResource resource)
        {
            // Segment resource parameters
            LeakRateUnit from = resource.FromUnit;
            LeakRateUnit to = resource.ToUnit;
            double? weight = (double?)resource.Weight;
            double? temp = (double?)resource.TempC;
            //double? temp = (from.Equals(LeakRateUnit.MolSec) || to.Equals(LeakRateUnit.MolSec)) ? resource.TestTempC : resource.TempK;

            // Ignore conversion if units match
            if (from.Equals(to)) { return value; }

            // Convert to base unit if needed
            double baseUnitValue = from.Equals(BaseUnit) ? value : DoubleConversionsFrom[from](value, weight, temp);

            // Convert to desired unit
            double newUnitValue = to.Equals(BaseUnit) ? baseUnitValue : DoubleConversionsTo[to](baseUnitValue, weight, temp);

            return newUnitValue;
        }

        public double CorrectForTemperature(double value, LeakRateTemperatureCorrectionResource resource)
        {
            double tempCoeff = (double)resource.TempCoeff;
            double testTempC = (double)resource.TestTempC;
            double desiredTempC = (double)resource.DesiredTempC;

            // Ignore correction if temperatures match
            if (resource.TestTempC.Equals(resource.DesiredTempC)) { return value; }

            return (value + (value * tempCoeff * (desiredTempC - testTempC)));
        }

        public decimal Convert(decimal value, LeakRateConversionResource resource)
        {
            // Segment resource parameters
            LeakRateUnit from = resource.FromUnit;
            LeakRateUnit to = resource.ToUnit;
            decimal? weight = resource.Weight;
            decimal? temp = resource.TempC;
            //decimal? temp = (from.Equals(LeakRateUnit.MolSec) || to.Equals(LeakRateUnit.MolSec)) ? resource.TestTempC : resource.TempK;

            // Ignore conversion if units match
            if (from.Equals(to)) { return value; }

            // Convert to base unit if needed
            decimal baseUnitValue = from.Equals(BaseUnit) ? value : ConversionsFrom[from](value, weight, temp);

            // Convert to desired unit
            decimal newUnitValue = to.Equals(BaseUnit) ? baseUnitValue : ConversionsTo[to](baseUnitValue, weight, temp);

            return newUnitValue;
        }

        public decimal CorrectForTemperature(decimal value, LeakRateTemperatureCorrectionResource resource)
        {
            decimal tempCoeff = (decimal)resource.TempCoeff;
            decimal testTempC = (decimal)resource.TestTempC;
            decimal desiredTempC = (decimal)resource.DesiredTempC;

            // Ignore correction if temperatures match
            if (resource.TestTempC.Equals(resource.DesiredTempC)) { return value; }

            return (value + (value * tempCoeff * (desiredTempC - testTempC)));
        }

        public IEnumerable<string> ListLeakRateUnitQueryNames()
        {
            return LeakRateUnit.ListQueryNames();
        }
    }
}
