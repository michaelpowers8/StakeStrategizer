namespace ProvablyFairSimulation.ReverseEngineers;

internal class WheelReverseEngineer
{
    private static readonly int[] ValidSegmentCounts = [ 10, 20, 30, 40, 50 ];
    private readonly ProvablyFairAlgorithm _algorithm;
    private readonly string _riskLevel;
    private readonly int _numberOfSegments;
    private static readonly IReadOnlyDictionary<string, double[]> PayoutMultipliers = new Dictionary<string, double[]>
    {
        ["Low10"] = [ 1.5, 1.2, 1.2, 1.2, 0, 1.2, 1.2, 1.2, 1.2, 0 ],
        ["Medium10"] = [ 0, 1.9, 0, 1.5, 0, 2, 0, 1.5, 0, 3],
        ["High10"] = [ 0, 0, 0, 0, 0, 0, 0, 0, 0, 9.9],

        ["Low20"] = [ 1.5, 1.2, 1.2, 1.2, 0, 1.2, 1.2, 1.2, 1.2, 0, 1.5, 1.2, 1.2, 1.2, 0, 1.2, 1.2, 1.2, 1.2, 0],
        ["Medium20"] = [ 1.5, 0, 2, 0, 2, 0, 2, 0, 1.5, 0, 3, 0, 1.8, 0, 2, 0, 2, 0, 2, 0],
        ["High20"] = [ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 19.8],

        ["Low30"] = [ 1.5, 1.2, 1.2, 1.2, 0, 1.2, 1.2, 1.2, 1.2, 0, 1.5, 1.2, 1.2, 1.2, 0, 1.2, 1.2, 1.2, 1.2, 0, 1.5, 1.2, 1.2, 1.2, 0, 1.2, 1.2, 1.2, 1.2, 0],
        ["Medium30"] = [ 1.5, 0, 1.5, 0, 2, 0, 1.5, 0, 2, 0, 2, 0, 1.5, 0, 3, 0, 1.5, 0, 2, 0, 2, 0, 1.7, 0, 4, 0, 1.5, 0, 2, 0],
        ["High30"] = [ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 29.7],

        ["Low40"] = [ 1.5, 1.2, 1.2, 1.2, 0, 1.2, 1.2, 1.2, 1.2, 0, 1.5, 1.2, 1.2, 1.2, 0, 1.2, 1.2, 1.2, 1.2, 0, 1.5, 1.2, 1.2, 1.2, 0, 1.2, 1.2, 1.2, 1.2, 0, 1.5, 1.2, 1.2, 1.2, 0, 1.2, 1.2, 1.2, 1.2, 0],
        ["Medium40"] = [ 2, 0, 3, 0, 2, 0, 1.5, 0, 3, 0, 1.5, 0, 1.5, 0, 2, 0, 1.5, 0, 3, 0, 1.5, 0, 2, 0, 2, 0, 1.6, 0, 2, 0, 1.5, 0, 3, 0, 1.5, 0, 2, 0, 1.5, 0],
        ["High40"] = [ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 39.6],

        ["Low50"] = [ 1.5, 1.2, 1.2, 1.2, 0, 1.2, 1.2, 1.2, 1.2, 0, 1.5, 1.2, 1.2, 1.2, 0, 1.2, 1.2, 1.2, 1.2, 0, 1.5, 1.2, 1.2, 1.2, 0, 1.2, 1.2, 1.2, 1.2, 0, 1.5, 1.2, 1.2, 1.2, 0, 1.2, 1.2, 1.2, 1.2, 0, 1.5, 1.2, 1.2, 1.2, 0, 1.2, 1.2, 1.2, 1.2, 0],
        ["Medium50"] = [ 2, 0, 1.5, 0, 2, 0, 1.5, 0, 3, 0, 1.5, 0, 1.5, 0, 2, 0, 1.5, 0, 3, 0, 1.5, 0, 2, 0, 1.5, 0, 2, 0, 2, 0, 1.5, 0, 3, 0, 1.5, 0, 2, 0, 1.5, 0, 1.5, 0, 5, 0, 1.5, 0, 2, 0, 1.5, 0],
        ["High50"] = [ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 49.5 ]
    };

    internal WheelReverseEngineer(ProvablyFairAlgorithm algorithm, string riskLevel, int numberOfSegments)
    {
        _algorithm = algorithm;
        _riskLevel =  riskLevel.ToLower();
        _numberOfSegments = numberOfSegments;
        ContainsValidParameters();
    }
    
    private void ContainsValidParameters()
    {
        List<string> validDifficulties = new List<string>() { "low", "medium", "high"};
        if (!validDifficulties.Contains(_riskLevel))
        {
            throw new ArgumentException("Risk level must be low, medium, high");
        }

        if (!ValidSegmentCounts.Contains(_numberOfSegments))
        {
            throw new ArgumentException($"Number of segments must be an integer in [{string.Join(", ", ValidSegmentCounts)}].");
        }
    }
    
    private double GetResultNumbers()
    {
        return _algorithm.RandomStakeNumbers(_numberOfSegments).First();
    }

    private static int ResultNumberToPrizeIndex(double resultNumber)
    {
        return (int)resultNumber;
    }

    private double PrizeIndexToPayoutMultiplier(int prizeIndex)
    {
        string key = char.ToUpper(_riskLevel[0]) + _riskLevel.Substring(1) + _numberOfSegments;
        return PayoutMultipliers[key][prizeIndex];
    }

    internal double GetPayoutMultiplier()
    {
        double numbers = GetResultNumbers();
        int prizeIndex = ResultNumberToPrizeIndex(numbers);
        return PrizeIndexToPayoutMultiplier(prizeIndex);
    }
}