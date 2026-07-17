using System.Configuration;
using ProvablyFairSimulation.Simulations;
using ProvablyFairSimulation.ReverseEngineers;
namespace ProvablyFairSimulation;

public static class ConfigureApplication
{
    private const double DefaultBaseBetSize = 1;
    private static readonly double? DefaultBetChangeOnWin = null;
    private static readonly double? DefaultBetChangeOnLoss = null;
    private static readonly double? DefaultStopOnNetGain = null;
    private static readonly double? DefaultStopOnNetLoss = null;
    
    private const int DefaultNumberOfPlinkoRows = 16;
    private const string DefaultPlinkoDifficulty = "expert";
    
    private const int DefaultNumberOfMines = 3;

    private const string DefaultPumpDifficulty = "hard";
    
    internal static ProvablyFairAlgorithm CreateAlgorithm()
    {
        ProvablyFairAlgorithm algorithm;
        string? serverSeed = ConfigurationManager.AppSettings["ServerSeed"];
        string? clientSeed = ConfigurationManager.AppSettings["ClientSeed"];
        string? nonceString = ConfigurationManager.AppSettings["Nonce"];
        if (serverSeed != null && clientSeed != null && int.TryParse(nonceString, out var nonce))
        {
            algorithm = new ProvablyFairAlgorithm(
                serverSeed: serverSeed,
                clientSeed: clientSeed,
                nonce: nonce
            );
        }
        else if (serverSeed != null && clientSeed != null)
        {
            algorithm = new ProvablyFairAlgorithm(
                serverSeed: serverSeed,
                clientSeed: clientSeed
            );
        }
        else if (clientSeed != null)
        {
            algorithm = new ProvablyFairAlgorithm(clientSeed: clientSeed);
        }
        else
        {
            algorithm = new ProvablyFairAlgorithm();
        }
        return algorithm;
    }

    internal static BettingStrategy CreateBettingStrategy()
    {
        string? baseBetSizeString = ConfigurationManager.AppSettings["BaseBetSize"];
        string? betChangeOnWinString = ConfigurationManager.AppSettings["BetChangeOnWin"];
        string? betChangeOnLossString = ConfigurationManager.AppSettings["BetChangeOnLoss"];
        string? stopOnNetGainString = ConfigurationManager.AppSettings["StopOnNetGain"];
        string? stopOnNetLossString = ConfigurationManager.AppSettings["StopOnNetLoss"];
        
        double baseBetSize = string.IsNullOrWhiteSpace(baseBetSizeString)
            ? DefaultBaseBetSize
            : double.Parse(baseBetSizeString);
        
        double? betChangeOnWin = string.IsNullOrWhiteSpace(betChangeOnWinString) 
            ? DefaultBetChangeOnWin
            : double.Parse(betChangeOnWinString);
        
        double? betChangeOnLoss = string.IsNullOrWhiteSpace(betChangeOnLossString) 
            ? DefaultBetChangeOnLoss
            : double.Parse(betChangeOnLossString);
        
        double? stopOnNetGain = string.IsNullOrWhiteSpace(stopOnNetGainString) 
            ? DefaultStopOnNetGain
            : double.Parse(stopOnNetGainString);
        
        double? stopOnNetLoss = string.IsNullOrWhiteSpace(stopOnNetLossString) 
            ? DefaultStopOnNetLoss
            : double.Parse(stopOnNetLossString);
        
        BettingStrategy bettingStrategy = new BettingStrategy(
            baseBetSize: baseBetSize,
            betChangeOnWin: betChangeOnWin,
            betChangeOnLoss: betChangeOnLoss,
            stopOnNetGain: stopOnNetGain,
            stopOnNetLoss: stopOnNetLoss
        );
        return bettingStrategy;
    }
    
    internal static PlinkoReverseEngineer CreatePlinkoReverseEngineer()
    {
        var algorithm = CreateAlgorithm();
        var difficulty = ConfigurationManager.AppSettings["PlinkoDifficulty"] ?? DefaultPlinkoDifficulty;
        var numberOfRowsString = ConfigurationManager.AppSettings["PlinkoNumberOfRows"];
        var numberOfRows = string.IsNullOrWhiteSpace(numberOfRowsString)
            ? DefaultNumberOfPlinkoRows
            : int.Parse(numberOfRowsString);
        return new PlinkoReverseEngineer(
            algorithm: algorithm,
            difficulty: difficulty,
            numberOfRows: numberOfRows);
    }

    internal static MinesReverseEngineer CreateMinesReverseEngineer()
    {
        string? numberOfMinesString =  ConfigurationManager.AppSettings["MinesNumberOfMines"]; 
        int numberOfMines = string.IsNullOrWhiteSpace(numberOfMinesString)
            ? DefaultNumberOfMines
            : int.Parse(numberOfMinesString);
        var algorithm = CreateAlgorithm();
        return new MinesReverseEngineer(
            algorithm: algorithm,
            numberOfMines: numberOfMines
        );
    }

    internal static PumpReverseEngineer CreatePumpReverseEngineer()
    {
        string pumpDifficulty =  ConfigurationManager.AppSettings["PumpDifficulty"] ?? DefaultPumpDifficulty;
        var algorithm = CreateAlgorithm();
        return new PumpReverseEngineer(
            algorithm: algorithm,
            difficulty: pumpDifficulty
        );
    }

    internal static BlackjackReverseEngineer CreateBlackjackReverseEngineer()
    {
        var algorithm = CreateAlgorithm();
        return new BlackjackReverseEngineer(algorithm: algorithm);
    }

    internal static KenoReverseEngineer CreateKenoReverseEngineer()
    {
        var algorithm = CreateAlgorithm();
        string? difficulty =  ConfigurationManager.AppSettings["KenoDifficulty"]; 
        string? userCases = ConfigurationManager.AppSettings["KenoUserSelectedCases"];
        int[] userCasesSplit;
        if (!string.IsNullOrEmpty(userCases))
        {
            string[] userCasesSplitStrings = userCases.Split(',');
            userCasesSplit = new int[userCasesSplitStrings.Length];
            for (int i = 0; i < userCasesSplitStrings.Length; i++)
            {
                userCasesSplit[i] = Int32.Parse(userCasesSplitStrings[i]);
            }
        }
        else
        {
            userCasesSplit = KenoReverseEngineer.DefaultUserCases;
        }
        return new KenoReverseEngineer(
            algorithm, 
            difficulty ?? KenoReverseEngineer.DefaultDifficulty, 
            userCasesSplit
        );
    }
}