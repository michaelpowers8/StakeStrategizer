namespace ProvablyFairSimulation.ReverseEngineers;

internal class RouletteReverseEngineer
{
    private const double BytesToNumberMultiplier = 37;
    private readonly ProvablyFairAlgorithm _algorithm;

    internal RouletteReverseEngineer(ProvablyFairAlgorithm algorithm)
    {
        _algorithm = algorithm;
    }
    
    private int GetResultNumbers()
    {
        return (int)_algorithm.RandomStakeNumbers(BytesToNumberMultiplier).First();
    }

    internal int RouletteSpinValue()
    {
        return GetResultNumbers();
    }
}