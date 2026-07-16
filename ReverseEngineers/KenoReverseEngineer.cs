namespace ProvablyFairSimulation.ReverseEngineers;

internal class KenoReverseEngineer
{
    private const int TotalNumberOfCases = 40;
    private const int NumberOfWinningCases = 10;
    private readonly ProvablyFairAlgorithm _algorithm;

    internal KenoReverseEngineer(ProvablyFairAlgorithm algorithm)
    {
        _algorithm = algorithm;
    }

    private List<double> GetResultNumbers()
    {
        var multipliers = new List<double>();
        for (int i = 40; i >= 31; i--)
        {
            multipliers.Add(i);
        }
        return _algorithm.RandomStakeNumbers(multipliers);
    }

    private List<int> NumbersToShuffle(List<double> numbers)
    {
        List<int> originalShuffle = new List<int>();
        List<int> finalShuffle = new List<int>();
        for (int i = 0; i < TotalNumberOfCases; i++)
        {
            originalShuffle.Add(i);
        }

        foreach (var number in numbers)
        {
            int truncatedNumber = (int)number;
            finalShuffle.Add(originalShuffle[truncatedNumber]);
            originalShuffle.RemoveAt(truncatedNumber);
        }
        return finalShuffle;
    }

    private List<int> ShuffleToValues(List<int> shuffle)
    {
        return shuffle.GetRange(0, NumberOfWinningCases);
    }

    internal List<int> GetWinningCases()
    {
        List<double> stakeNumbers = GetResultNumbers();
        List<int> finalShuffle = NumbersToShuffle(stakeNumbers);
        List<int> values = ShuffleToValues(finalShuffle);
        List<int> cases = new List<int>();
        foreach (var number in values)
        {
            cases.Add(number+1);
        }
        return cases;
    }
}