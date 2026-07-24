namespace ProvablyFairSimulation.ReverseEngineers;

internal class BlackjackReverseEngineer
{
    private const int BlackJackValue = 21;
    private const int InitialNumberOfCardsDealtPerPlayer = 2;
    private const int MaximumPossibleHits = BlackJackValue - InitialNumberOfCardsDealtPerPlayer;
    private const double BytesToNumberMultiplier = 52;
    private const int NumberOfCardsInDeck = 128;
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
        return _algorithm.RandomStakeNumbers(Enumerable.Repeat(BytesToNumberMultiplier, NumberOfCardsInDeck).ToList());
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

    internal double MaxPayoutMultiplierWithPerfectPlay(List<string>? deck = null)
    {
        deck ??= GetDeckHand();
        var playerHand = deck.GetRange(0, InitialNumberOfCardsDealtPerPlayer);
        deck.RemoveRange(0, InitialNumberOfCardsDealtPerPlayer);
        var playerHandValue = GetHandValue(playerHand);
        var dealerHand = deck.GetRange(0, InitialNumberOfCardsDealtPerPlayer);
        deck.RemoveRange(0, InitialNumberOfCardsDealtPerPlayer);
        var dealerHandValue = GetHandValue(dealerHand);
        if (playerHandValue == BlackJackValue && dealerHandValue < BlackJackValue)
        {
            return 2.5;
        }
        if (dealerHandValue == BlackJackValue && playerHandValue < BlackJackValue && dealerHand.First().Last().Equals('A'))
        {
            return 2.0/3.0;
        }
        if (dealerHandValue == BlackJackValue && playerHandValue < BlackJackValue && !dealerHand.First().Last().Equals('A'))
        {
            return 0;
        }
        if (dealerHandValue == BlackJackValue && playerHandValue == BlackJackValue)
        {
            return 1;
        }

        return SimulateAllBlackjackHands(playerHand, dealerHand, deck);
    }

    private static double SimulateAllBlackjackHands(
        List<string> playerHand,
        List<string> dealerHand,
        List<string> deck
    )
    {
        List<double> payoutMultipliers = new List<double>();
        for (int numberOfHits = 0; numberOfHits < MaximumPossibleHits; numberOfHits++)
        {
            int playerHandValue = GetPlayerHandValue(
                playerHand:playerHand.GetRange(index:0, count:playerHand.Count), 
                restOfDeck: deck.GetRange(index:0, count:deck.Count), 
                numberOfHits:numberOfHits
            );
            int dealerHandValue = GetDealerHandValue(
                dealerHand:dealerHand.GetRange(index:0, count:dealerHand.Count), 
                restOfDeck:deck.GetRange(
                    index: numberOfHits, 
                    count: deck.Count - numberOfHits
                )
            );
            if (
                (
                    (playerHandValue > dealerHandValue || dealerHandValue > BlackJackValue) && 
                    playerHandValue <= BlackJackValue && numberOfHits == 1
                )
                ||
                (
                    playerHandValue > dealerHandValue && 
                    playerHandValue <= BlackJackValue && numberOfHits != 1
                )
            )
            {
                payoutMultipliers.Add(2);
            }
            else if (playerHandValue == dealerHandValue)
            {
                payoutMultipliers.Add(1);
            }
            else if (playerHandValue > BlackJackValue)
            {
                payoutMultipliers.Add(0);
            }
            else if (dealerHandValue > BlackJackValue)
            {
                payoutMultipliers.Add(2);
            }
            else if (dealerHandValue > playerHandValue)
            {
                payoutMultipliers.Add(0);
            }
            else
            {
                Console.WriteLine($"Player Hand Value: {playerHandValue}\nDealer Hand Value: {dealerHandValue}");
            }
        }
        return payoutMultipliers.Max();
    }
    
    private static int GetHandValue(List<string> hand)
    {
        int numberOfAces = 0;
        int handValue = 0;
        foreach (var card in hand)
        {
            var cardValueString = card.Substring(1, card.Length - 1);
            if (Int32.TryParse(cardValueString, out int cardValue))
            {
                handValue += cardValue;
            }
            else if (!cardValueString.Equals("A"))
            {
                handValue += 10;
            }
            else
            {
                numberOfAces++;
            }
        }
        
        if (numberOfAces == 0)
        {
            return handValue;
        }
        handValue += numberOfAces - 1;
        if (handValue + 11 > BlackJackValue)
        {
            handValue++;
        }
        else
        {
            handValue += 11;
        }
        return handValue;
    }

    private static int GetPlayerHandValue(List<string> playerHand, List<string> restOfDeck, int numberOfHits)
    {
        playerHand.AddRange(restOfDeck.GetRange(0, numberOfHits));
        return GetHandValue(playerHand);
    }

    private static int GetDealerHandValue(List<string> dealerHand, List<string> restOfDeck)
    {
        int handValue = GetHandValue(dealerHand);
        int index = 0;
        while (true)
        {
            if (handValue >= 17)
            {
                return handValue;
            }
            dealerHand.Add(restOfDeck[index]);
            handValue = GetHandValue(dealerHand);
            index++;
        }
    }
}