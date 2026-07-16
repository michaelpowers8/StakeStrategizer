namespace ProvablyFairSimulation.ReverseEngineers;

internal class DragonTowerReverseEngineer
{
    private const double Rtp = 0.98;
    private readonly ProvablyFairAlgorithm _algorithm;
    private readonly string _difficulty;
    private readonly int _totalSpacesPerRow;
    private readonly int _safeSpacesPerRow;
    private readonly int _dangerousSpacesPerRow;
    private const int NumberOfRounds = 9;
    private static readonly string[] ValidDifficulties = ["easy", "medium", "hard", "expert", "master"];

    internal DragonTowerReverseEngineer(ProvablyFairAlgorithm algorithm, string difficulty)
    {
        _algorithm = algorithm;
        _difficulty = difficulty.ToLower();
        ContainsValidParameters();
        _totalSpacesPerRow = CalculateTotalSpacesPerRow();
        _safeSpacesPerRow = CalculateSafeSpacesPerRow();
        _dangerousSpacesPerRow = _totalSpacesPerRow - _safeSpacesPerRow;
    }

    private void ContainsValidParameters()
    {
        if (!ValidDifficulties.Contains(_difficulty))
        {
            throw new ArgumentException($"Difficulty must be one of {string.Join(", ", ValidDifficulties)}");
        }
    }

    private int CalculateTotalSpacesPerRow()
    {
        return _difficulty switch
        {
            "easy" => 4,
            "medium" => 3,
            "hard" => 2,
            "expert" => 3,
            "master" => 4,
            _ => -1
        };
    }
    
    private int CalculateSafeSpacesPerRow()
    {
        return _difficulty switch
        {
            "easy" => 3,
            "medium" => 2,
            "hard" => 1,
            "expert" => 1,
            "master" => 1,
            _ => -1
        };
    }

    private List<double> GetResultNumbers()
    {
        var multipliers = new List<double>();
        for (int i = 0; i < NumberOfRounds; i++)
        {
            for (int j = _totalSpacesPerRow; j > _totalSpacesPerRow - _safeSpacesPerRow; j--)
            {
                multipliers.Add(j);
            }
        }
        return _algorithm.RandomStakeNumbers(multipliers);
    }

    private List<List<int>> NumbersToShuffle(List<double> numbers)
    {
        List<int> originalShuffle = new List<int>();
        List<List<int>> originalShuffles = new List<List<int>>();
        List<List<int>> finalShuffles = new List<List<int>>();
        for (int i = 0; i < NumberOfRounds; i++)
        {
            for (int j = 0; j < _totalSpacesPerRow; j++)
            {
                originalShuffle.Add(j);
            }
            originalShuffles.Add(new List<int>(originalShuffle));
            originalShuffle.Clear();
        }

        List<int> finalShuffle = new List<int>();
        int originalShuffleIndex = 0;
        foreach (var number in numbers)
        {
            int truncatedNumber = (int)number;
            finalShuffle.Add(originalShuffles[originalShuffleIndex][truncatedNumber]);
            originalShuffles[originalShuffleIndex].RemoveAt(truncatedNumber);
            if (finalShuffle.Count == _safeSpacesPerRow)
            {
                finalShuffles.Add(new List<int>(finalShuffle));
                finalShuffle.Clear();
                originalShuffleIndex++;
            }
        }
        return finalShuffles;
    }

    internal List<List<int>> GetSafePositions()
    {
        List<double> stakeNumbers = GetResultNumbers();
        List<List<int>> finalShuffle = NumbersToShuffle(stakeNumbers);
        return finalShuffle;
    }
}