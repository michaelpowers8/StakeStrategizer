namespace ProvablyFairSimulation.ReverseEngineers;

internal class MolesReverseEngineer
{
    private const double Rtp = 0.98;
    private readonly ProvablyFairAlgorithm _algorithm;
    private readonly int _numberOfMoles;
    private const int MaximumNumberOfMoles = 6;
    private const int NumberOfHoles = 7;
    private readonly int _maximumRounds;

    internal MolesReverseEngineer(ProvablyFairAlgorithm algorithm, int numberOfMoles)
    {
        _algorithm = algorithm;
        _numberOfMoles = numberOfMoles;
        _maximumRounds = CalculateMaxRounds();
    }

    private int CalculateMaxRounds()
    {
        if (_numberOfMoles == 1)
        {
            return 8;
        }
        if (_numberOfMoles <= MaximumNumberOfMoles)
        {
            return 9;
        }

        throw new ArgumentException($"Number of moles must be between 1 and {MaximumNumberOfMoles} inclusive");
    }

    private List<double> GetResultNumbers()
    {
        var multipliers = new List<double>();
        for (int i = 0; i < _maximumRounds; i++)
        {
            for (int j = NumberOfHoles; j > NumberOfHoles - _numberOfMoles; j--)
            {
                multipliers.Add(j);
            }
        }
        return _algorithm.RandomStakeNumbers(multipliers);
    }

    private List<List<int>> NumbersToShuffle(List<double> numbers)
    {
        List<List<int>> originalShuffles = new List<List<int>>();
        List<List<int>> finalShuffles = new List<List<int>>();
        for (int i = 0; i < _maximumRounds; i++)
        {
            List<int> originalShuffle = new List<int>();
            for (int j = 0; j < NumberOfHoles; j++)
            {
                originalShuffle.Add(j);
            }
            originalShuffles.Add(originalShuffle);
        }

        List<int> finalShuffle = new List<int>();
        int originalShuffleIndex = 0;
        for(int i = 0; i < numbers.Count; i++)
        {
            int truncatedNumber = (int)numbers[i];
            finalShuffle.Add(originalShuffles[originalShuffleIndex][truncatedNumber]);
            originalShuffles[originalShuffleIndex].RemoveAt(truncatedNumber);
            if (finalShuffle.Count == _numberOfMoles)
            {
                finalShuffles.Add(new List<int>(finalShuffle));
                finalShuffle.Clear();
                originalShuffleIndex++;
            }
        }
        return finalShuffles;
    }

    internal List<List<int>> GetMolePositions()
    {
        List<double> stakeNumbers = GetResultNumbers();
        List<List<int>> finalShuffle = NumbersToShuffle(stakeNumbers);
        return finalShuffle;
    }
}