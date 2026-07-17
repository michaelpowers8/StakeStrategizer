namespace ProvablyFairSimulation.ReverseEngineers;

internal class KenoReverseEngineer
{
    private const int TotalNumberOfCases = 40;
    private const int NumberOfWinningCases = 10;
    internal const string DefaultDifficulty = "high";
    internal static readonly int[] DefaultUserCases = [1];
    private readonly ProvablyFairAlgorithm _algorithm;
    private readonly string _difficulty;
    private readonly int[] _userCases;

    private static readonly IReadOnlyDictionary<string, Dictionary<int, Dictionary<int, double>>> PayoutMultipliers =
        new Dictionary<string, Dictionary<int, Dictionary<int, double>>>()
        {
            ["classic"] = new Dictionary<int, Dictionary<int, double>>()
            {
                [1] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 3.96
                },
                [2] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 1.9,
                    [2] = 4.5
                },
                [3] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 1,
                    [2] = 3.1,
                    [3] = 10.4
                },
                [4] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0.8,
                    [2] = 1.8,
                    [3] = 5,
                    [4] = 22.5
                },
                [5] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0.25,
                    [2] = 1.4,
                    [3] = 4.1,
                    [4] = 16.5,
                    [5] = 36
                },
                [6] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 1,
                    [3] = 3.68,
                    [4] = 7,
                    [5] = 16.5,
                    [6] = 40
                },
                [7] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 0.47,
                    [3] = 3,
                    [4] = 4.5,
                    [5] = 14,
                    [6] = 31,
                    [7] = 60
                },
                [8] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 0,
                    [3] = 2.2,
                    [4] = 4,
                    [5] = 13,
                    [6] = 22,
                    [7] = 55,
                    [8] = 70
                },
                [9] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 0,
                    [3] = 1.55,
                    [4] = 3,
                    [5] = 8,
                    [6] = 15,
                    [7] = 44,
                    [8] = 60,
                    [9] = 85
                },
                [10] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 0,
                    [3] = 1.4,
                    [4] = 2.25,
                    [5] = 4.5,
                    [6] = 8,
                    [7] = 17,
                    [8] = 50,
                    [9] = 80,
                    [10] = 100
                },
            },
            ["low"] = new Dictionary<int, Dictionary<int, double>>()
            {
                [1] = new Dictionary<int, double>()
                {
                    [0] = 0.7,
                    [1] = 1.85
                },
                [2] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 2,
                    [2] = 3.8
                },
                [3] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 1.1,
                    [2] = 1.38,
                    [3] = 26
                },
                [4] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 2.2,
                    [3] = 7.9,
                    [4] = 90
                },
                [5] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 1.5,
                    [3] = 4.2,
                    [4] = 13,
                    [5] = 300
                },
                [6] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 1.1,
                    [3] = 2,
                    [4] = 6.2,
                    [5] = 100,
                    [6] = 700
                },
                [7] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 1.1,
                    [3] = 1.6,
                    [4] = 3.5,
                    [5] = 15,
                    [6] = 225,
                    [7] = 700
                },
                [8] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 1.1,
                    [3] = 1.5,
                    [4] = 2,
                    [5] = 5.5,
                    [6] = 39,
                    [7] = 100,
                    [8] = 800
                },
                [9] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 1.1,
                    [3] = 1.3,
                    [4] = 1.7,
                    [5] = 2.5,
                    [6] = 7.5,
                    [7] = 50,
                    [8] = 250,
                    [9] = 1000
                },
                [10] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 1.1,
                    [3] = 1.2,
                    [4] = 1.3,
                    [5] = 1.8,
                    [6] = 3.5,
                    [7] = 13,
                    [8] = 50,
                    [9] = 250,
                    [10] = 1000
                },
            },
            ["medium"] = new Dictionary<int, Dictionary<int, double>>()
            {
                [1] = new Dictionary<int, double>()
                {
                    [0] = 0.4,
                    [1] = 2.75
                },
                [2] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 1.8,
                    [2] = 5.1
                },
                [3] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 2.8,
                    [3] = 50
                },
                [4] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 1.7,
                    [3] = 10,
                    [4] = 100
                },
                [5] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 1.4,
                    [3] = 4,
                    [4] = 14,
                    [5] = 390
                },
                [6] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 0,
                    [3] = 3,
                    [4] = 9,
                    [5] = 180,
                    [6] = 710
                },
                [7] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 0,
                    [3] = 2,
                    [4] = 7,
                    [5] = 30,
                    [6] = 400,
                    [7] = 800
                },
                [8] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 0,
                    [3] = 2,
                    [4] = 4,
                    [5] = 11,
                    [6] = 67,
                    [7] = 400,
                    [8] = 900
                },
                [9] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 0,
                    [3] = 2,
                    [4] = 2.5,
                    [5] = 5,
                    [6] = 15,
                    [7] = 100,
                    [8] = 500,
                    [9] = 1000
                },
                [10] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 0,
                    [3] = 1.6,
                    [4] = 2,
                    [5] = 4,
                    [6] = 7,
                    [7] = 26,
                    [8] = 100,
                    [9] = 500,
                    [10] = 1000
                },
            },
            ["high"] = new Dictionary<int, Dictionary<int, double>>()
            {
                [1] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 3.96
                },
                [2] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 17.1
                },
                [3] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 0,
                    [3] = 81.5
                },
                [4] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 0,
                    [3] = 10,
                    [4] = 259
                },
                [5] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 0,
                    [3] = 4.5,
                    [4] = 48,
                    [5] = 450
                },
                [6] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 0,
                    [3] = 0,
                    [4] = 11,
                    [5] = 350,
                    [6] = 710
                },
                [7] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 0,
                    [3] = 0,
                    [4] = 7,
                    [5] = 90,
                    [6] = 400,
                    [7] = 800
                },
                [8] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 0,
                    [3] = 0,
                    [4] = 5,
                    [5] = 20,
                    [6] = 270,
                    [7] = 600,
                    [8] = 900
                },
                [9] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 0,
                    [3] = 0,
                    [4] = 4,
                    [5] = 11,
                    [6] = 56,
                    [7] = 500,
                    [8] = 800,
                    [9] = 1000
                },
                [10] = new Dictionary<int, double>()
                {
                    [0] = 0,
                    [1] = 0,
                    [2] = 0,
                    [3] = 0,
                    [4] = 3.5,
                    [5] = 8,
                    [6] = 13,
                    [7] = 63,
                    [8] = 500,
                    [9] = 800,
                    [10] = 1000
                },
            }
        };

    internal KenoReverseEngineer(ProvablyFairAlgorithm algorithm)
    {
        _algorithm = algorithm;
        _difficulty =  DefaultDifficulty;
        _userCases = DefaultUserCases;
    }

    internal KenoReverseEngineer(ProvablyFairAlgorithm algorithm, string difficulty, int[] userCases)
    {
        _algorithm =  algorithm;
        _difficulty = difficulty;
        _userCases = userCases;
    }

    internal ProvablyFairAlgorithm GetAlgorithm()
    {
        return _algorithm;
    }

    internal string GetDifficulty()
    {
        return _difficulty;
    }

    internal int[] GetUserCases()
    {
        return _userCases;
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

    internal double GetPayoutMultiplier()
    {
        List<int> winningCases = GetWinningCases();
        int numberOfWins = 0;
        foreach (var caseNumber in winningCases)
        {
            if (_userCases.Contains(caseNumber))
            {
                numberOfWins++;
            }
        }

        if (!PayoutMultipliers.ContainsKey(_difficulty)) return 0;
        var innerLookup = PayoutMultipliers.GetValueOrDefault(_difficulty, new Dictionary<int, Dictionary<int, double>>());
        var numberOfCasesMultipliers = innerLookup.GetValueOrDefault(_userCases.Length, new Dictionary<int, double>());
        var payoutMultiplier = numberOfCasesMultipliers.GetValueOrDefault(numberOfWins, 0);
        return payoutMultiplier;
    }
    
    internal double GetPayoutMultiplier(List<int> winningCases)
    {
        int numberOfWins = 0;
        foreach (var caseNumber in winningCases)
        {
            if (_userCases.Contains(caseNumber))
            {
                numberOfWins++;
            }
        }

        if (!PayoutMultipliers.ContainsKey(_difficulty)) return 0;
        var innerLookup = PayoutMultipliers.GetValueOrDefault(_difficulty, new Dictionary<int, Dictionary<int, double>>());
        var numberOfCasesMultipliers = innerLookup.GetValueOrDefault(_userCases.Length, new Dictionary<int, double>());
        var payoutMultiplier = numberOfCasesMultipliers.GetValueOrDefault(numberOfWins, 0);
        return payoutMultiplier;
    }
}