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
        var algorithm = ConfigureApplication.CreateAlgorithm();
        var plinkoEngineer = ConfigureApplication.CreatePlinkoReverseEngineer();
        var plinkoRoll = plinkoEngineer.GetPayoutMultiplier();
        Console.WriteLine($"Server Seed: {algorithm.GetServerSeed()}\nClient Seed: {algorithm.GetClientSeed()}\nNonce: {algorithm.GetNonce()-1:F0}\nPlinko Result: {plinkoRoll:F2}");
    }
}