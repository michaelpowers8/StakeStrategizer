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
            default:
                SpinPlinko();
                break;
        }
    }

    private static void SpinDice()
    {
        var algorithm = ConfigureApplication.CreateAlgorithm();
        DiceReverseEngineer diceEngineer = new DiceReverseEngineer(algorithm: algorithm);
        double diceRoll = diceEngineer.GetDiceRoll();
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
        
    }
}