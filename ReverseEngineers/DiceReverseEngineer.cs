namespace ProvablyFairSimulation.ReverseEngineers;

internal class DiceReverseEngineer
{
    //private const double Rtp = 0.99;
    private const double BytesToNumberMultiplier = 10001;
    private readonly ProvablyFairAlgorithm _algorithm;

    internal DiceReverseEngineer(ProvablyFairAlgorithm algorithm)
    {
        _algorithm = algorithm;
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
}