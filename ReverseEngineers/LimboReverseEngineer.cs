namespace ProvablyFairSimulation.ReverseEngineers;

internal class LimboReverseEngineer
{
    private const double Rtp = 0.99;
    private const double BytesToNumberMultiplier = 16777216;
    private readonly ProvablyFairAlgorithm _algorithm;

    internal LimboReverseEngineer(ProvablyFairAlgorithm algorithm)
    {
        _algorithm = algorithm;
    }
    
    private double GetResultNumbers()
    {
        return _algorithm.RandomStakeNumbers(BytesToNumberMultiplier).First();
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