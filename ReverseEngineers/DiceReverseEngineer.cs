namespace ProvablyFairSimulation.ReverseEngineers;

internal class DiceReverseEngineer
{
    private const double Rtp = 0.99;
    private const double BytesToNumberMultiplier = 10001;
    private readonly ProvablyFairAlgorithm _algorithm;
    internal const double DefaultThreshold = 50.5;
    internal const string DefaultOverUnder = "over";
    private readonly double _threshold;
    private readonly string _overUnder;

    internal DiceReverseEngineer(ProvablyFairAlgorithm algorithm)
    {
        _algorithm = algorithm;
        _threshold = DefaultThreshold;
        _overUnder = DefaultOverUnder;
    }

    internal DiceReverseEngineer(ProvablyFairAlgorithm algorithm, string overUnder, double threshold)
    {
        _algorithm = algorithm;
        _overUnder = overUnder;
        _threshold = threshold;
    }

    internal ProvablyFairAlgorithm GetAlgorithm()
    {
        return _algorithm;
    }

    internal double GetThreshold()
    {
        return _threshold;
    }
    
    internal string GetOverUnder()
    {
        return _overUnder;
    }

private double GetResultNumbers()
    {
        return _algorithm.RandomStakeNumbers(BytesToNumberMultiplier).First();
    }

    private static double ResultNumberToFinalResult(double resultNumber)
    {
        int truncatedResult = (int)resultNumber;
        return truncatedResult * 0.01;
    }

    internal double GetDiceRoll()
    {
        double numbers = GetResultNumbers();
        return ResultNumberToFinalResult(numbers);
    }

    internal double GetPayoutMultiplier(double diceRoll)
    {
        if (diceRoll > _threshold && _overUnder.Equals("over"))
        {
            return 1 / ((100.0 - _threshold) / 100.0) * Rtp;
        }
        if (diceRoll < _threshold && _overUnder.Equals("under"))
        {
            return 1 / (_threshold / 100.0) * Rtp;
        }
        return 0;
    }
}