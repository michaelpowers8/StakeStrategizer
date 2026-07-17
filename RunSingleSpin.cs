using System.Configuration;
using ProvablyFairSimulation.ReverseEngineers;

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
        var algorithm = ConfigureApplication.CreateAlgorithm();
        var diceEngineer = new DiceReverseEngineer(algorithm: algorithm);
        var diceRoll = diceEngineer.GetDiceRoll();
        Console.WriteLine($"Server Seed: {algorithm.GetServerSeed()}\nClient Seed: {algorithm.GetClientSeed()}\nNonce: {algorithm.GetNonce()-1:F0}\nDice Result: {diceRoll:F2}");
    }
    
    private static void SpinPlinko()
    {
        var plinkoEngineer = ConfigureApplication.CreatePlinkoReverseEngineer();
        var plinkoRoll = plinkoEngineer.GetPayoutMultiplier();
        Console.WriteLine($"Server Seed: {plinkoEngineer.GetAlgorithm().GetServerSeed()}\nClient Seed: {plinkoEngineer.GetAlgorithm().GetClientSeed()}\nNonce: {plinkoEngineer.GetAlgorithm().GetNonce()-1:F0}\nPlinko Result: {plinkoRoll:F2}");
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

        Console.WriteLine($"Server Seed: {minesEngineer.GetAlgorithm().GetServerSeed()}\nClient Seed: {minesEngineer.GetAlgorithm().GetClientSeed()}\nNonce: {minesEngineer.GetAlgorithm().GetNonce() - 1:F0}\nMines Grid:");
        foreach (var row in grid)
        {
            Console.WriteLine($"{string.Join("|", row)}");
        }
    }

    private static void SpinPump()
    {
        var pumpEngineer = ConfigureApplication.CreatePumpReverseEngineer();
        (int maxPumps, double maxMultiplier) = pumpEngineer.GetMaxPumpsAndMaxMultiplier();
        
        Console.WriteLine($"Server Seed: {pumpEngineer.GetAlgorithm().GetServerSeed()}\nClient Seed: {pumpEngineer.GetAlgorithm().GetClientSeed()}\nNonce: {pumpEngineer.GetAlgorithm().GetNonce()-1:F0}\nMax Pumps Possible: {maxPumps:F0}\nMax Multiplier Possible: {maxMultiplier:F8}");
    }

    private static void SpinBlackjack()
    {
        var blackjackEngineer = ConfigureApplication.CreateBlackjackReverseEngineer();
        var deck = blackjackEngineer.GetDeckHand();
        Console.WriteLine($"Server Seed: {blackjackEngineer.GetAlgorithm().GetServerSeed()}\nClient Seed: {blackjackEngineer.GetAlgorithm().GetClientSeed()}\nNonce: {blackjackEngineer.GetAlgorithm().GetNonce()-1:F0}\nBlackjack Deck (1st & 2nd cards create player hand. 3rd and 4th cards create dealer hand):\n{string.Join("|", deck)}");
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