using System.Configuration;

namespace ProvablyFairSimulation;

internal static class RunSingleSpin
{
    private const string DefaultGameToSpin = "plinko";

    internal static void Run()
    {
        var gameToSpin = ConfigurationManager.AppSettings["GameToSpin"] ?? DefaultGameToSpin;
        switch (gameToSpin.ToLower())
        {
            case "dice":
                SpinDice();
                break;
            case "mines":
                SpinMines();
                break;
            case "pump":
                SpinPump();
                break;
            case "blackjack":
                SpinBlackjack();
                break;
            case "keno":
                SpinKeno();
                break;
            default:
                SpinPlinko();
                break;
        }
    }

    private static void SpinDice()
    {
        var diceEngineer = ConfigureApplication.CreateDiceReverseEngineer();
        var diceRoll = diceEngineer.GetDiceRoll();
        Console.WriteLine(
            $"Server Seed: {diceEngineer.GetAlgorithm().GetServerSeed()}\n" +
            $"Client Seed: {diceEngineer.GetAlgorithm().GetClientSeed()}\n" +
            $"Nonce: {diceEngineer.GetAlgorithm().GetNonce()-1:F0}\n" +
            $"Threshold: {diceEngineer.GetThreshold():F2}\n" +
            $"Over-Under: {diceEngineer.GetOverUnder().ToUpperInvariant()}\n" +
            $"Dice Result: {diceRoll:F2}\n" +
            $"Payout Multiplier: {diceEngineer.GetPayoutMultiplier(diceRoll):F2}"
        );
    }
    
    private static void SpinPlinko()
    {
        var plinkoEngineer = ConfigureApplication.CreatePlinkoReverseEngineer();
        var plinkoRoll = plinkoEngineer.GetPayoutMultiplier();
        Console.WriteLine(
            $"Server Seed: {plinkoEngineer.GetAlgorithm().GetServerSeed()}\n" +
            $"Client Seed: {plinkoEngineer.GetAlgorithm().GetClientSeed()}\n" +
            $"Nonce: {plinkoEngineer.GetAlgorithm().GetNonce()-1:F0}\n" +
            $"Plinko Result: {plinkoRoll:F2}"
        );
    }

    private static void SpinMines()
    {
        var minesEngineer = ConfigureApplication.CreateMinesReverseEngineer();
        var minesResult = minesEngineer.GetMinesPositions();
        List<List<string>> grid = new List<List<string>>
        {
            new List<string> { "💎", "💎", "💎", "💎", "💎"},
            new List<string> { "💎", "💎", "💎", "💎", "💎"},
            new List<string> { "💎", "💎", "💎", "💎", "💎"},
            new List<string> { "💎", "💎", "💎", "💎", "💎"},
            new List<string> { "💎", "💎", "💎", "💎", "💎"}
        };
        foreach (var mine in minesResult)
        {
            grid[5 - mine.Item2][mine.Item1 - 1] = "💣";
        }

        Console.WriteLine(
            $"Server Seed: {minesEngineer.GetAlgorithm().GetServerSeed()}\n" +
            $"Client Seed: {minesEngineer.GetAlgorithm().GetClientSeed()}\n" +
            $"Nonce: {minesEngineer.GetAlgorithm().GetNonce() - 1:F0}\n" +
            $"Mines Grid:"
        );
        foreach (var row in grid)
        {
            Console.WriteLine($"{string.Join("|", row)}");
        }
    }

    private static void SpinPump()
    {
        var pumpEngineer = ConfigureApplication.CreatePumpReverseEngineer();
        (int maxPumps, double maxMultiplier) = pumpEngineer.GetMaxPumpsAndMaxMultiplier();
        double payoutMultiplier = pumpEngineer.GetPayoutMultiplier(maxPumps);
        Console.WriteLine(
            $"Server Seed: {pumpEngineer.GetAlgorithm().GetServerSeed()}\n" +
            $"Client Seed: {pumpEngineer.GetAlgorithm().GetClientSeed()}\n" +
            $"Nonce: {pumpEngineer.GetAlgorithm().GetNonce()-1:F0}\n" + 
            $"Difficulty: {pumpEngineer.GetDifficulty()}\n" + 
            $"Max Pumps Possible: {maxPumps:F0}\n" +
            $"Max Multiplier Possible: {maxMultiplier:F8}\n" +
            $"Number of Pumps by User: {pumpEngineer.GetNumberOfUserPumps():F0}\n" + 
            $"Actual Payout Multiplier: {payoutMultiplier:F8}"
        );
    }

    private static void SpinBlackjack()
    {
        var blackjackEngineer = ConfigureApplication.CreateBlackjackReverseEngineer();
        var deck = blackjackEngineer.GetDeckHand();
        var payoutMultiplier = blackjackEngineer.MaxPayoutMultiplierWithPerfectPlay(deck.GetRange(0, deck.Count));
        Console.WriteLine(
            $"Server Seed: {blackjackEngineer.GetAlgorithm().GetServerSeed()}\n" +
            $"Client Seed: {blackjackEngineer.GetAlgorithm().GetClientSeed()}\n" +
            $"Nonce: {blackjackEngineer.GetAlgorithm().GetNonce()-1:F0}\n" +
            $"Best Payout Multiplier Possible: {payoutMultiplier}\n" +
            $"Blackjack Deck (1st & 2nd cards create player hand. 3rd and 4th cards create dealer hand):\n" +
            $"{string.Join("|", deck)}\n"
        );
    }

    private static void SpinKeno()
    {
        var kenoEngineer = ConfigureApplication.CreateKenoReverseEngineer();
        var winningCases = kenoEngineer.GetWinningCases();
        var payoutMultiplier = kenoEngineer.GetPayoutMultiplier(winningCases);
        Console.WriteLine(
            $"Server Seed: {kenoEngineer.GetAlgorithm().GetServerSeed()}\n" +
            $"Client Seed: {kenoEngineer.GetAlgorithm().GetClientSeed()}\n" +
            $"Nonce: {kenoEngineer.GetAlgorithm().GetNonce() - 1:F0}\n" +
            $"Difficulty: {kenoEngineer.GetDifficulty().ToUpper()}\n" +
            $"User Cases: {string.Join("|",kenoEngineer.GetUserCases())}\n" +
            $"Winning Cases: {string.Join("|", winningCases)}\n" +
            $"Payout Multiplier: {payoutMultiplier:F2}");
    }
}