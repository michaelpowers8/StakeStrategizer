namespace ProvablyFairSimulation.ReverseEngineers;
internal class PumpReverseEngineer
{
    private static readonly string[] ValidDifficulties = [ "easy", "medium", "hard", "expert" ];
    private const double Rtp = 0.98;
    private readonly ProvablyFairAlgorithm _algorithm;
    private readonly string _difficulty;
    private readonly int _maxPumps;
    private readonly int _numberOfUserPumps;

    internal PumpReverseEngineer(ProvablyFairAlgorithm algorithm, string difficulty, int numberOfUserPumps)
    {
        _algorithm = algorithm;
        _difficulty = difficulty;
        _numberOfUserPumps = numberOfUserPumps;
        _maxPumps = CalculateMaxPumps();
    }

    internal ProvablyFairAlgorithm GetAlgorithm()
    {
        return _algorithm;
    }

    internal string GetDifficulty()
    {
        return _difficulty;
    }

    internal int GetNumberOfUserPumps()
    {
        return _numberOfUserPumps;
    }

    private int CalculateMaxPumps()
    {
        return _difficulty switch
        {
            "easy" => 25,
            "medium" => 23,
            "hard" => 21,
            "expert" => 16,
            _ => 0
        };
    }

    private List<double> GetResultNumbers()
    {
        var multipliers = new List<double>();
        for (int i = 25; i >= 2; i--)
        {
            multipliers.Add(i);
        }
        return _algorithm.RandomStakeNumbers(multipliers);
    }

    private List<int> NumbersToShuffle(List<double> numbers)
    {
        List<int> originalShuffle = new List<int>();
        List<int> finalShuffle = new List<int>();
        for (int i = 0; i < numbers.Count+1; i++)
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
        if (!ValidDifficulties.Contains(_difficulty))
        {
            throw new ArgumentException("Invalid difficulty");
        }

        return _difficulty switch
        {
            "easy" => shuffle.GetRange(0, 1),
            "medium" => shuffle.GetRange(0, 3),
            "hard" => shuffle.GetRange(0, 5),
            "expert" => shuffle.GetRange(0, 10),
            _ => shuffle
        };
    }

    private double ValuesToMaxPayoutMultiplier(List<int> values)
    {
        int minimumValue = values.Min();
        double probabilityOfWinning = 1;
        for (double i = 0; i < minimumValue; i++)
        {
            probabilityOfWinning *= (_maxPumps - i - 1) / (25.0 - i);
        }
        double winningMultiplier = Math.Pow(probabilityOfWinning, -1) * Rtp;
        if (winningMultiplier < 1)
        {
            return 0;
        }
        return winningMultiplier;
    }

    internal (int, double) GetMaxPumpsAndMaxMultiplier()
    {
        List<double> stakeNumbers = GetResultNumbers();
        List<int> finalShuffle = NumbersToShuffle(stakeNumbers);
        List<int> values = ShuffleToValues(finalShuffle);
        return (values.Min(), ValuesToMaxPayoutMultiplier(values));
    }
    
    internal double GetPayoutMultiplier(int maxPossiblePumpsBeforeLosing)
    {
        List<int> payoutMultiplierPumpsValue = new List<int>();
        if (maxPossiblePumpsBeforeLosing > _numberOfUserPumps)
        {
            payoutMultiplierPumpsValue.Add(_numberOfUserPumps);
        }
        else
        {
            payoutMultiplierPumpsValue.Add(0);
        }
        return ValuesToMaxPayoutMultiplier(payoutMultiplierPumpsValue);
    }
}