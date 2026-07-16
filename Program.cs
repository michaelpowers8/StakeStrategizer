using System.IO;
using System.Text.Json;
using ProvablyFairSimulation.Simulations;
using ProvablyFairSimulation.Simulations.Plinko;
using ProvablyFairSimulation.ReverseEngineers;

namespace ProvablyFairSimulation
{
    internal static class Program
    {
        private static void Main()
        {
            ProvablyFairAlgorithm algorithm = new ProvablyFairAlgorithm();
            PlinkoReverseEngineer engineer = new PlinkoReverseEngineer(
                algorithm: algorithm,
                difficulty: "expert",
                numberOfRows: 16
            );
            BettingStrategy bettingStrategy = new BettingStrategy(
                baseBetSize: 1,
                betChangeOnWin: null,
                betChangeOnLoss: null,
                stopOnNetGain: 100_000,
                stopOnNetLoss: 50_000
            );
            PlinkoSimulation simulation = new PlinkoSimulation(
                plinkoReverseEngineer: engineer,
                bettingStrategy: bettingStrategy,
                initialBalance: 1_000_000,
                numberOfPlays: 100_000
            );
            var simulationResults = simulation.RunPlinkoSimulation();
            string jsonString = JsonSerializer.Serialize(simulationResults, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("Results.json", jsonString);
            Console.ReadLine();
        }
    }
}

