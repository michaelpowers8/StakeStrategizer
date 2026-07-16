namespace ProvablyFairSimulation.ReverseEngineers;

internal class RockPaperScissorsReverseEngineer
{
    private const double BytesToNumberMultiplier = 3;
    private readonly ProvablyFairAlgorithm _algorithm;

    internal RockPaperScissorsReverseEngineer(ProvablyFairAlgorithm algorithm)
    {
        _algorithm = algorithm;
    }

    private List<double> GetResultNumbers()
    {
        return _algorithm.RandomStakeNumbers(Enumerable.Repeat(BytesToNumberMultiplier, 20).ToList());
    }

    private List<int> NumbersToChoices(List<double> numbers)
    {
        List<int> finalChoices = new List<int>();
        foreach (var number in numbers)
        {
            int truncatedNumber = (int)number;
            finalChoices.Add(truncatedNumber);
        }
        return finalChoices;
    }

    internal List<int> GetRockPaperScissorsHands()
    {
        List<double> stakeNumbers = GetResultNumbers();
        List<int> finalChoices = NumbersToChoices(stakeNumbers);
        return finalChoices;
    }
}