// Non weight dependant
LeakRateConverter leakRateConverter = new LeakRateConverter();
LeakRateConversionResource settings = new LeakRateConversionResource {
    From = LeakRateUnit.AtmCCSec.Name,
    To = LeakRateUnit.TorrLSec.Name,
};
double leakRateInAtmccSec = 1.43E-6;
double leakRateInTorrLSec = leakRateConverter.Convert(leakRateInAtmccSec, settings);


// weight dependant (Example using gas designator as defined in calibration AutoCal machines)
string thisLeakGasDesignator = Machine.Test[0].UnknownLeakSetup[port].Flow.Gas;
GasTypes gas = Machine.Cycle[0].apiINFO.GasList.First(g =&gt; g.Designator == thisLeakGasDesignator);
LeakRateConverter leakRateConverter = new LeakRateConverter();
LeakRateConversionResource settings = new LeakRateConversionResource {
    From = LeakRateUnit.AtmCCSec.Name,
    To = LeakRateUnit.OuncesYear.Name,
    TempC = 23.1m, // (decimal)
    Weight = gas.Weight,
};
double leakRateInAtmccSec = 1.43E-6;
double leakRateInTorrLSec = leakRateConverter.Convert(leakRateInAtmccSec, settings);
