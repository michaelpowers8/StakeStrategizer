namespace ProvablyFairSimulation.ReverseEngineers;

internal class BlackjackReverseEngineer
{
    private const double BytesToNumberMultiplier = 52;
    private readonly ProvablyFairAlgorithm _algorithm;

    private static readonly IReadOnlyDictionary<int, string> PlayingCards = new Dictionary<int, string>
    {
        [0] = "♦2",
        [1] = "♥2",
        [2] = "♠2",
        [3] = "♣2",
        [4] = "♦3",
        [5] = "♥3",
        [6] = "♠3",
        [7] = "♣3",
        [8] = "♦4",
        [9] = "♥4",
        [10] = "♠4",
        [11] = "♣4",
        [12] = "♦5",
        [13] = "♥5",
        [14] = "♠5",
        [15] = "♣5",
        [16] = "♦6",
        [17] = "♥6",
        [18] = "♠6",
        [19] = "♣6",
        [20] = "♦7",
        [21] = "♥7",
        [22] = "♠7",
        [23] = "♣7",
        [24] = "♦8",
        [25] = "♥8",
        [26] = "♠8",
        [27] = "♣8",
        [28] = "♦9",
        [29] = "♥9",
        [30] = "♠9",
        [31] = "♣9",
        [32] = "♦10",
        [33] = "♥10",
        [34] = "♠10",
        [35] = "♣10",
        [36] = "♦J",
        [37] = "♥J",
        [38] = "♠J",
        [39] = "♣J",
        [40] = "♦Q",
        [41] = "♥Q",
        [42] = "♠Q",
        [43] = "♣Q",
        [44] = "♦K",
        [45] = "♥K",
        [46] = "♠K",
        [47] = "♣K",
        [48] = "♦A",
        [49] = "♥A",
        [50] = "♠A",
        [51] = "♣A",
    };

    internal BlackjackReverseEngineer(ProvablyFairAlgorithm algorithm)
    {
        _algorithm = algorithm;
    }

    internal ProvablyFairAlgorithm GetAlgorithm()
    {
        return _algorithm;
    }

    private List<double> GetResultNumbers()
    {
        return _algorithm.RandomStakeNumbers(Enumerable.Repeat(BytesToNumberMultiplier, 52).ToList());
    }

    private List<string> NumbersToDeck(List<double> numbers)
    {
        List<string> finalDeck = new List<string>();
        foreach (var number in numbers)
        {
            int truncatedNumber = (int)number;
            finalDeck.Add(PlayingCards[truncatedNumber]);
        }
        return finalDeck;
    }

    internal List<string> GetDeckHand()
    {
        List<double> stakeNumbers = GetResultNumbers();
        List<string> finalDeck = NumbersToDeck(stakeNumbers);
        return finalDeck;
    }
}