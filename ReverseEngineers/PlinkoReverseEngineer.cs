namespace ProvablyFairSimulation.ReverseEngineers;

public class PlinkoReverseEngineer
{
    private const double BytesToNumberMultiplier = 2;
    private const int MinimumNumberOfRows = 8;
    private const int MaximumNumberOfRows = 16;
    private readonly ProvablyFairAlgorithm _algorithm;
    private readonly string _difficulty;
    private readonly int _numberOfRows;
    private static readonly IReadOnlyDictionary<string, double[]> PayoutMultipliers = new Dictionary<string, double[]>
    {
        ["Low8"]   = [ 5.6, 2.1, 1.1, 1, 0.5, 1, 1.1, 2.1, 5.6 ],
        ["Medium8"]= [ 13, 3, 1.3, 0.7, 0.4, 0.7, 1.3, 3, 13 ],
        ["High8"]  = [ 29, 4, 1.5, 0.3, 0.2, 0.3, 1.5, 4, 29 ],
        ["Expert8"] =[ 50, 4.6, 1.1, 0.1, 0.1, 0.1, 1.1, 4.6, 50 ],
        ["Degenerate8"] =[ 126.72, 0, 0, 0, 0, 0, 0, 0, 126.72 ],

        ["Low9"]   = [ 5.6, 2, 1.6, 1, 0.7, 0.7, 1, 1.6, 2, 5.6 ],
        ["Medium9"]= [ 18, 4, 1.7, 0.9, 0.5, 0.5, 0.9, 1.7, 4, 18 ],
        ["High9"]  = [ 43, 7, 2, 0.6, 0.2, 0.2, 0.6, 2, 7, 43 ],
        ["Expert9"] =[ 100, 7.8, 1.5, 0.2, 0.1, 0.1, 0.2, 1.5, 7.8, 100 ],
        ["Degenerate9"] =[ 253.44, 0, 0, 0, 0, 0, 0, 0, 0, 253.44 ],

        ["Low10"]  = [ 8.9, 3, 1.4, 1.1, 1, 0.5, 1, 1.1, 1.4, 3, 8.9 ],
        ["Medium10"]=[ 22, 5, 2, 1.4, 0.6, 0.4, 0.6, 1.4, 2, 5, 22 ],
        ["High10"] = [ 76, 10, 3, 0.9, 0.3, 0.2, 0.3, 0.9, 3, 10, 76 ],
        ["Expert10"]=[ 201, 11, 2.0, 0.6, 0.1, 0.1, 0.1, 0.6, 2.0, 11, 201 ],
        ["Degenerate10"] =[ 506.88, 0, 0, 0, 0, 0, 0, 0, 0, 0, 506.88 ],

        ["Low11"]  = [ 8.4, 3, 1.9, 1.3, 1, 0.7, 0.7, 1, 1.3, 1.9, 3, 8.4 ],
        ["Medium11"]=[ 24, 6, 3, 1.8, 0.7, 0.5, 0.5, 0.7, 1.8, 3, 6, 24 ],
        ["High11"] = [ 120, 14, 5.2, 1.4, 0.4, 0.2, 0.2, 0.4, 1.4, 5.2, 14, 120 ],
        ["Expert11"]=[ 324, 16, 4.0, 1.1, 0.2, 0.1, 0.1, 0.2, 1.1, 4.0, 16, 324 ],
        ["Degenerate11"] =[ 1013.76, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1013.76 ],

        ["Low12"]  = [ 10, 3, 1.6, 1.4, 1.1, 1, 0.5, 1, 1.1, 1.4, 1.6, 3, 10 ],
        ["Medium12"]=[ 33, 11, 4, 2, 1.1, 0.6, 0.3, 0.6, 1.1, 2, 4, 11, 33 ],
        ["High12"] = [ 170, 24, 8.1, 2, 0.7, 0.2, 0.2, 0.7, 2, 8.1, 24, 170 ],
        ["Expert12"]=[ 619, 30, 6.0, 1.5, 0.4, 0.1, 0.1, 0.1, 0.4, 1.5, 6.0, 30, 619 ],
        ["Degenerate12"] =[ 2027.52, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2027.52 ],

        ["Low13"]  = [ 8.1, 4, 3, 1.9, 1.2, 0.9, 0.7, 0.7, 0.9, 1.2, 1.9, 3, 4, 8.1 ],
        ["Medium13"]=[ 43, 13, 6, 3, 1.3, 0.7, 0.4, 0.4, 0.7, 1.3, 3, 6, 13, 43 ],
        ["High13"] = [ 260, 37, 11, 4, 1, 0.2, 0.2, 0.2, 0.2, 1, 4, 11, 37, 260 ],
        ["Expert13"]=[ 1000, 52, 10, 3.0, 0.6, 0.1, 0.1, 0.1, 0.1, 0.6, 3.0, 10, 52, 1000 ],
        ["Degenerate13"] =[ 4055.04, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4055.04 ],

        ["Low14"]  = [ 7.1, 4, 1.9, 1.4, 1.3, 1.1, 1, 0.5, 1, 1.1, 1.3, 1.4, 1.9, 4, 7.1 ],
        ["Medium14"]=[ 58, 15, 7, 4, 1.9, 1, 0.5, 0.2, 0.5, 1, 1.9, 4, 7, 15, 58 ],
        ["High14"] = [ 420, 56, 18, 5, 1.9, 0.3, 0.2, 0.2, 0.2, 0.3, 1.9, 5, 18, 56, 420 ],
        ["Expert14"]=[ 2300, 80, 16, 3.0, 1.2, 0.2, 0.1, 0.1, 0.1, 0.2, 1.2, 3.0, 16, 80, 2300 ],
        ["Degenerate14"] =[ 8110.08, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8110.08 ],

        ["Low15"]  = [ 15, 8, 3, 2, 1.5, 1.1, 1, 0.7, 0.7, 1, 1.1, 1.5, 2, 3, 8, 15 ],
        ["Medium15"]=[ 88, 18, 11, 5, 3, 1.3, 0.5, 0.3, 0.3, 0.5, 1.3, 3, 5, 11, 18, 88 ],
        ["High15"] = [ 620, 83, 27, 8, 3, 0.5, 0.2, 0.2, 0.2, 0.2, 0.5, 3, 8, 27, 83, 620 ],
        ["Expert15"]=[ 5000, 125, 23, 6.0, 1.8, 0.2, 0.1, 0.1, 0.1, 0.1, 0.2, 1.8, 6.0, 23, 125, 5000 ],
        ["Degenerate15"] =[ 16220.16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16220.16 ],

        ["Low16"]  = [ 16, 9, 2, 1.4, 1.4, 1.2, 1.1, 1, 0.5, 1, 1.1, 1.2, 1.4, 1.4, 2, 9, 16 ],
        ["Medium16"]=[ 110, 41, 10, 5, 3, 1.5, 1, 0.5, 0.3, 0.5, 1, 1.5, 3, 5, 10, 41, 110 ],
        ["High16"] = [ 1000, 130, 26, 9, 4, 2, 0.2, 0.2, 0.2, 0.2, 0.2, 2, 4, 9, 26, 130, 1000 ],
        ["Expert16"]=[ 10000, 216, 26, 7.0, 2.5, 1.1, 0.1, 0.1, 0.1, 0.1, 0.1, 1.1, 2.5, 7.0, 26, 216, 10000 ],
        ["Degenerate16"] =[ 32440.32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 32440.32 ],
    };

    internal PlinkoReverseEngineer(ProvablyFairAlgorithm algorithm, string difficulty, int numberOfRows)
    {
        _algorithm = algorithm;
        _difficulty = difficulty.ToLower();
        _numberOfRows = numberOfRows;
        ContainsValidParameters();
    }

    private void ContainsValidParameters()
    {
        List<string> validDifficulties = new List<string>() { "low", "medium", "high", "expert" };
        if (!validDifficulties.Contains(_difficulty))
        {
            throw new ArgumentException("Difficulty must be low, medium, high, expert");
        }

        if (_numberOfRows < MinimumNumberOfRows || _numberOfRows > MaximumNumberOfRows)
        {
            throw new ArgumentException($"Number of rows must be an integer between {MinimumNumberOfRows}-{MaximumNumberOfRows} inclusive");
        }
    }

    private List<double> GetResultNumbers()
    {
        return _algorithm.RandomStakeNumbers(Enumerable.Repeat(BytesToNumberMultiplier, 20).ToList());
    }

    private int ResultNumberToPrizeIndex(List<double> resultNumbers)
    {
        int prizeIndex = 0;
        int numRowsPlayed = 0;
        foreach (double number in resultNumbers)
        {
            if (number >= 1)
            {
                prizeIndex++;
            }
            numRowsPlayed++;
            if (numRowsPlayed == _numberOfRows)
            {
                break;
            }
        }
        return prizeIndex;
    }

    private double PrizeIndexToPayoutMultiplier(int prizeIndex)
    {
        string key = char.ToUpper(_difficulty[0]) + _difficulty.Substring(1) + _numberOfRows;
        return PayoutMultipliers[key][prizeIndex];
    }

    internal string GetDifficulty()
    {
        return _difficulty;
    }

    internal int GetNumberOfRows()
    {
        return _numberOfRows;
    }

    internal ProvablyFairAlgorithm GetAlgorithm()
    {
        return _algorithm;
    }

    internal double GetPayoutMultiplier()
    {
        List<double> resultNumbers = GetResultNumbers();
        int prizeIndexFromResultNumbers = ResultNumberToPrizeIndex(resultNumbers);
        double payoutMultiplierFromPrizeIndex = PrizeIndexToPayoutMultiplier(prizeIndexFromResultNumbers);
        return payoutMultiplierFromPrizeIndex;
    }
}