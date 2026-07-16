using System.Text.Json;
using System.Configuration;
using ProvablyFairSimulation.ReverseEngineers;
using ProvablyFairSimulation.Simulations.Plinko;

namespace ProvablyFairSimulation; 
internal static class RunSimulation
{
    private const string DefaultGameToSimulate = "plinko";
    private const double DefaultInitialBalance = 1_000;
    private const int DefaultNumberOfSimulatedSpins = 1_000;
    
    internal static void Run()
    {
        string gameToSimulate = ConfigurationManager.AppSettings["GameToSimulate"] ?? DefaultGameToSimulate;
        Dictionary<string, object> simulationResults;
        switch (gameToSimulate.ToLowerInvariant())
        {
            case DefaultGameToSimulate:
                simulationResults = RunPlinkoSimulation();
                break;
            default:
                simulationResults = RunPlinkoSimulation();
                break;
        }
        string jsonString = JsonSerializer.Serialize(simulationResults, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("Results.json", jsonString);
    }
    
    private static Dictionary<string, object> RunPlinkoSimulation()
    {
        var initialBalanceString = ConfigurationManager.AppSettings["StartingBalance"];
        var numberOfPlaysString = ConfigurationManager.AppSettings["NumberOfSimulatedSpins"];
        
        var initialBalance = string.IsNullOrWhiteSpace(initialBalanceString)
            ? DefaultInitialBalance
            : double.Parse(initialBalanceString);
        
        var numberOfPlays = string.IsNullOrWhiteSpace(numberOfPlaysString)
            ? DefaultNumberOfSimulatedSpins
            : int.Parse(numberOfPlaysString);
        
        var plinkoSimulator = new PlinkoSimulation(
            plinkoReverseEngineer: ConfigureApplication.CreatePlinkoReverseEngineer(),
            bettingStrategy: ConfigureApplication.CreateBettingStrategy(),
            initialBalance: initialBalance,
            numberOfPlays: numberOfPlays
        );
        return plinkoSimulator.RunPlinkoSimulation();
    }
}