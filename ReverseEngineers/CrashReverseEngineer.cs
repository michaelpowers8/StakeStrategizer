namespace ProvablyFairSimulation.ReverseEngineers;

internal class CrashReverseEngineer
{
    private const double Rtp = 0.99;
    private const double BytesToNumberMultiplier = 4294967296;
    private readonly ProvablyFairAlgorithm _algorithm;

    internal CrashReverseEngineer(ProvablyFairAlgorithm algorithm)
    {
        _algorithm = algorithm;
    }
    
    private double GetResultNumbers()
    {
        return _algorithm.GetCrashDecimal();
    }

    private static double ResultNumberToEdge(double resultNumber)
    {
        return BytesToNumberMultiplier / ((int)resultNumber + 1) * Rtp;
    }

    internal double GetPayoutMultiplier()
    {
        double numbers = GetResultNumbers();
        return ResultNumberToEdge(numbers);
    }
}