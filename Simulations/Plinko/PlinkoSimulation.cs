using System.Text;
using ProvablyFairSimulation.ReverseEngineers;

namespace ProvablyFairSimulation.Simulations.Plinko;

public class PlinkoSimulation
{
    private readonly PlinkoReverseEngineer _plinkoReverseEngineer;
    private readonly BettingStrategy _bettingStrategy;
    private readonly double _initialBalance;
    private double _currentBalance;
    private readonly int _numberOfPlays;
    private double _totalMoneyWagered;
    private double _totalMoneyWon;
    private int _numberOfWins;
    private int _longestWinningStreak;
    private int _longestLosingStreak;
    private int _currentWinningStreak;
    private int _currentLosingStreak;
    private double _largestBalanceDuringSimulation;
    private long _largestBalanceDuringSimulationNonce;
    private double _smallestBalanceDuringSimulation;
    private long _smallestBalanceDuringSimulationNonce;

    internal PlinkoSimulation()
    {
        _plinkoReverseEngineer = new PlinkoReverseEngineer(new ProvablyFairAlgorithm(), "expert", 16);
        _bettingStrategy =  new BettingStrategy();
        _initialBalance = 1_000_000;
        _currentBalance = _initialBalance;
        _numberOfPlays = (int)_initialBalance;
        _totalMoneyWagered = 0;
        _totalMoneyWon = 0;
        _numberOfWins = 0;
        _longestWinningStreak = 0;
        _longestLosingStreak = 0;
        _currentWinningStreak = 0;
        _currentLosingStreak = 0;
        _largestBalanceDuringSimulation = _initialBalance;
        _smallestBalanceDuringSimulation = _initialBalance;
        _largestBalanceDuringSimulationNonce = _plinkoReverseEngineer.GetAlgorithm().GetStartingNonce();
        _smallestBalanceDuringSimulationNonce = _plinkoReverseEngineer.GetAlgorithm().GetStartingNonce();
        ValidParameters();
    }
    
    internal PlinkoSimulation(PlinkoReverseEngineer plinkoReverseEngineer)
    {
        _plinkoReverseEngineer = plinkoReverseEngineer;
        _bettingStrategy = new BettingStrategy();
        _initialBalance = 1_000_000;
        _currentBalance = _initialBalance;
        _numberOfPlays = (int)_initialBalance;
        _totalMoneyWagered = 0;
        _totalMoneyWon = 0;
        _numberOfWins = 0;
        _longestWinningStreak = 0;
        _longestLosingStreak = 0;
        _currentWinningStreak = 0;
        _currentLosingStreak = 0;
        _largestBalanceDuringSimulation = _initialBalance;
        _smallestBalanceDuringSimulation = _initialBalance;
        _largestBalanceDuringSimulationNonce = _plinkoReverseEngineer.GetAlgorithm().GetStartingNonce();
        _smallestBalanceDuringSimulationNonce = _plinkoReverseEngineer.GetAlgorithm().GetStartingNonce();
        ValidParameters();
    }
    
    internal PlinkoSimulation(BettingStrategy bettingStrategy)
    {
        _plinkoReverseEngineer = new PlinkoReverseEngineer(new ProvablyFairAlgorithm(), "expert", 16);
        _bettingStrategy = bettingStrategy;
        _initialBalance = 1_000_000;
        _currentBalance = _initialBalance;
        _numberOfPlays = (int)_initialBalance;
        _totalMoneyWagered = 0;
        _totalMoneyWon = 0;
        _numberOfWins = 0;
        _longestWinningStreak = 0;
        _longestLosingStreak = 0;
        _currentWinningStreak = 0;
        _currentLosingStreak = 0;
        _largestBalanceDuringSimulation = _initialBalance;
        _smallestBalanceDuringSimulation = _initialBalance;
        _largestBalanceDuringSimulationNonce = _plinkoReverseEngineer.GetAlgorithm().GetStartingNonce();
        _smallestBalanceDuringSimulationNonce = _plinkoReverseEngineer.GetAlgorithm().GetStartingNonce();
        ValidParameters();
    }

    internal PlinkoSimulation(PlinkoReverseEngineer plinkoReverseEngineer, BettingStrategy bettingStrategy)
    {
        _plinkoReverseEngineer = plinkoReverseEngineer;
        _bettingStrategy = bettingStrategy;
        _initialBalance = 1_000_000;
        _currentBalance = _initialBalance;
        _numberOfPlays = (int)_initialBalance;
        _totalMoneyWagered = 0;
        _totalMoneyWon = 0;
        _numberOfWins = 0;
        _longestWinningStreak = 0;
        _longestLosingStreak = 0;
        _currentWinningStreak = 0;
        _currentLosingStreak = 0;
        _largestBalanceDuringSimulation = _initialBalance;
        _smallestBalanceDuringSimulation = _initialBalance;
        _largestBalanceDuringSimulationNonce = _plinkoReverseEngineer.GetAlgorithm().GetStartingNonce();
        _smallestBalanceDuringSimulationNonce = _plinkoReverseEngineer.GetAlgorithm().GetStartingNonce();
        ValidParameters();
    }
    
    internal PlinkoSimulation(PlinkoReverseEngineer plinkoReverseEngineer, BettingStrategy bettingStrategy, double initialBalance)
    {
        _plinkoReverseEngineer = plinkoReverseEngineer;
        _bettingStrategy = bettingStrategy;
        _initialBalance = initialBalance;
        _currentBalance = _initialBalance;
        _numberOfPlays = (int)_initialBalance;
        _totalMoneyWagered = 0;
        _totalMoneyWon = 0;
        _numberOfWins = 0;
        _longestWinningStreak = 0;
        _longestLosingStreak = 0;
        _currentWinningStreak = 0;
        _currentLosingStreak = 0;
        _largestBalanceDuringSimulation = _initialBalance;
        _smallestBalanceDuringSimulation = _initialBalance;
        _largestBalanceDuringSimulationNonce = _plinkoReverseEngineer.GetAlgorithm().GetStartingNonce();
        _smallestBalanceDuringSimulationNonce = _plinkoReverseEngineer.GetAlgorithm().GetStartingNonce();
        ValidParameters();
    }
    
    internal PlinkoSimulation(PlinkoReverseEngineer plinkoReverseEngineer, BettingStrategy bettingStrategy, double initialBalance, int numberOfPlays)
    {
        _plinkoReverseEngineer = plinkoReverseEngineer;
        _bettingStrategy = bettingStrategy;
        _initialBalance = initialBalance;
        _currentBalance = _initialBalance;
        _numberOfPlays = numberOfPlays;
        _totalMoneyWagered = 0;
        _totalMoneyWon = 0;
        _numberOfWins = 0;
        _longestWinningStreak = 0;
        _longestLosingStreak = 0;
        _currentWinningStreak = 0;
        _currentLosingStreak = 0;
        _largestBalanceDuringSimulation = _initialBalance;
        _smallestBalanceDuringSimulation = _initialBalance;
        _largestBalanceDuringSimulationNonce = _plinkoReverseEngineer.GetAlgorithm().GetStartingNonce();
        _smallestBalanceDuringSimulationNonce = _plinkoReverseEngineer.GetAlgorithm().GetStartingNonce();
        ValidParameters();
    }

    private void ValidParameters()
    {
        if (_initialBalance <= 0)
        {
            throw new ArgumentException("Initial balance must be greater than 0.");
        }

        if (_numberOfPlays <= 0)
        {
            throw new ArgumentException("Number of plays must be greater than 0.");
        }
    }
    
    private void UpdateGameState(double payoutMultiplier)
    {
        if (payoutMultiplier > 1)
        {
            _numberOfWins++;
            _currentWinningStreak++;
            if (_currentLosingStreak > _longestLosingStreak)
            {
                _longestLosingStreak = _currentLosingStreak;
            }
            _currentLosingStreak = 0;
            
            switch (_bettingStrategy.GetBetChangePercentOnWin() == null)
            {
                case (true):
                    _bettingStrategy.SetCurrentBetSize(_bettingStrategy.GetBaseBetSize());
                    break;
                case (false):
                    _bettingStrategy.SetCurrentBetSize(
                        _bettingStrategy.GetCurrentBetSize()+
                        _bettingStrategy.GetCurrentBetSize()*
                        (_bettingStrategy.GetBetChangePercentOnWin() ?? 0)/100);
                    break;
            }
        }
        else
        {
            _currentLosingStreak++;
            if (_currentWinningStreak > _longestWinningStreak)
            {
                _longestWinningStreak = _currentWinningStreak;
            }
            _currentWinningStreak = 0;
            
            switch (_bettingStrategy.GetBetChangePercentOnLoss() == null)
            {
                case (true):
                    _bettingStrategy.SetCurrentBetSize(_bettingStrategy.GetBaseBetSize());
                    break;
                case (false):
                    _bettingStrategy.SetCurrentBetSize(
                        _bettingStrategy.GetCurrentBetSize()+
                        _bettingStrategy.GetCurrentBetSize()*
                        (_bettingStrategy.GetBetChangePercentOnLoss() ?? 0)/100
                    );
                    break;
            }
        }

        if (_currentBalance > _largestBalanceDuringSimulation)
        {
            _largestBalanceDuringSimulation = _currentBalance;
            _largestBalanceDuringSimulationNonce = _plinkoReverseEngineer.GetAlgorithm().GetNonce();
        }
        else if (_currentBalance < _smallestBalanceDuringSimulation)
        {
            _smallestBalanceDuringSimulation = _currentBalance;
            _smallestBalanceDuringSimulationNonce = _plinkoReverseEngineer.GetAlgorithm().GetNonce();
        }
    }

    private string SimulationReport(int numberOfBetsPlaced)
    {
        string equalsSeparatorLine = new string('=', 50);
        StringBuilder reportString = new StringBuilder();
        reportString.AppendLine("PLINKO SIMULATION REPORT");
        reportString.AppendLine(equalsSeparatorLine);
        reportString.AppendLine("GAME SETTINGS");
        reportString.AppendLine(equalsSeparatorLine);
        reportString.AppendLine($"Number of Rows: {_plinkoReverseEngineer.GetNumberOfRows():N0}");
        reportString.AppendLine($"Risk Level: {_plinkoReverseEngineer.GetDifficulty()}");
        reportString.AppendLine($"Number of Bets Initialized: {_numberOfPlays:N0}");
        reportString.AppendLine($"Number of Bets Actually Played: {numberOfBetsPlaced:N0}");
        reportString.AppendLine($"Number of Winning Spins: {_numberOfWins:N0}");
        reportString.AppendLine($"Number of Losing Spins: {numberOfBetsPlaced - _numberOfWins:N0}");
        reportString.AppendLine($"Longest Winning Streak: {_longestWinningStreak:N0}");
        reportString.AppendLine($"Longest Losing Streak: {_longestLosingStreak:N0}");
        reportString.AppendLine($"Initial Balance: ${_initialBalance:N2}");
        reportString.AppendLine($"Ending Balance: ${_currentBalance:N2}");
        reportString.AppendLine($"Total Money Wagered: ${_totalMoneyWagered:N2}");
        reportString.AppendLine($"Total Money Won: ${_totalMoneyWon:N2}");
        reportString.AppendLine($"Largest Balance Recorded During Simulation: ${_largestBalanceDuringSimulation:N2}");
        reportString.AppendLine($"Nonce Where Largest Balance Occurred: {_largestBalanceDuringSimulationNonce:N0}");
        reportString.AppendLine($"Smallest Balance Recorded During Simulation: ${_smallestBalanceDuringSimulation:N2}");
        reportString.AppendLine($"Nonce Where Smallest Balance Occurred: {_smallestBalanceDuringSimulationNonce:N0}");
        reportString.AppendLine($"Base Bet Size: ${_bettingStrategy.GetBaseBetSize():N2}");
        if (_bettingStrategy.GetBetChangePercentOnWin() != null)
        {
            reportString.AppendLine($"On Win, Increase Bet Size By {_bettingStrategy.GetBetChangePercentOnWin():N2}%");
        }
        else
        {
            reportString.AppendLine($"On Win, Reset Bet Size To ${_bettingStrategy.GetBaseBetSize():N2}");
        }
        if (_bettingStrategy.GetBetChangePercentOnLoss() != null)
        {
            reportString.AppendLine($"On Loss, Increase Bet Size By {_bettingStrategy.GetBetChangePercentOnLoss():N2}%");
        }
        else
        {
            reportString.AppendLine($"On Loss, Reset Bet Size To ${_bettingStrategy.GetBaseBetSize():N2}");
        }
        if (_bettingStrategy.GetStopOnNetGain() != null)
        {
            reportString.AppendLine($"Stop Playing After Net Gain Of ${_bettingStrategy.GetStopOnNetGain():N2}");
        }
        else
        {
            reportString.AppendLine("No Net Gain Threshold");
        }
        if (_bettingStrategy.GetStopOnNetLoss() != null)
        {
            reportString.AppendLine($"Stop Playing After Net Loss Of ${_bettingStrategy.GetStopOnNetLoss():N2}");
        }
        else
        {
            reportString.AppendLine("No Net Loss Threshold");
        }
        reportString.AppendLine("");
        reportString.AppendLine(equalsSeparatorLine);
        reportString.AppendLine("SIMULATION RESULTS");
        reportString.AppendLine(equalsSeparatorLine);
        
        if (numberOfBetsPlaced < _numberOfPlays && _currentBalance < _initialBalance)
        {
            reportString.AppendLine($"Strategy failed after {numberOfBetsPlaced:N0} bets. The net loss threshold of ${_bettingStrategy.GetStopOnNetLoss():N2} was reached resulting in a net loss of ${_initialBalance-_currentBalance:N2} and a house edge of {(1 - _totalMoneyWon / _totalMoneyWagered) * 100:N2}%");
        }
        else if (numberOfBetsPlaced < _numberOfPlays && _currentBalance >= _initialBalance)
        {
            reportString.AppendLine($"Strategy succeeded after {numberOfBetsPlaced:N0} bets. The net gain threshold of ${_bettingStrategy.GetStopOnNetGain():N2} was reached resulting in a net gain of ${_currentBalance-_initialBalance:N2} and an RTP of {_totalMoneyWon / _totalMoneyWagered * 100:N2}%");
        }
        else if (Math.Abs(numberOfBetsPlaced - _numberOfPlays) < 0.01 && _currentBalance >= _initialBalance)
        {
            reportString.AppendLine($"Strategy succeeded after {numberOfBetsPlaced:N0} bets. No concrete net gain threshold was reached. The total bets resulted in a net gain of ${_currentBalance-_initialBalance:N2} and an RTP of {_totalMoneyWon / _totalMoneyWagered * 100:N2}%");
        }
        else if (Math.Abs(numberOfBetsPlaced - _numberOfPlays) < 0.01 && _currentBalance < _initialBalance)
        {
            reportString.AppendLine($"Strategy failed after {numberOfBetsPlaced:N0} bets. No concrete net loss threshold was reached. The total bets resulted in a net loss of ${_initialBalance-_currentBalance:N2} and a house edge of {(1 - _totalMoneyWon / _totalMoneyWagered) * 100:N2}%");
        }
        return reportString.ToString();
    }

    private Dictionary<string, object> SimulationJsonReport(int numberOfBetsPlaced)
    {
        Dictionary<string, object> jsonReport = new Dictionary<string, object>()
        {
            ["gameSimulated"] = "Plinko",
            ["serverSeed"] = _plinkoReverseEngineer.GetAlgorithm().GetServerSeed(),
            ["serverSeedHashed"] = _plinkoReverseEngineer.GetAlgorithm().GetServerSeedHash(),
            ["clientSeed"] = _plinkoReverseEngineer.GetAlgorithm().GetClientSeed(),
            ["startingNonce"] = _plinkoReverseEngineer.GetAlgorithm().GetStartingNonce(),
            ["endingNonce"] = _plinkoReverseEngineer.GetAlgorithm().GetNonce(),
            ["numberOfRows"] = _plinkoReverseEngineer.GetNumberOfRows(),
            ["riskLevel"] = _plinkoReverseEngineer.GetDifficulty(),
            ["maximumNumberOfBetsSetForSimulation"] = _numberOfPlays,
            ["numberOfBetsPlaced"] = numberOfBetsPlaced,
            ["numberOfWins"] =  _numberOfWins,
            ["numberOfLosses"] = numberOfBetsPlaced -  _numberOfWins,
            ["initialBalance"] =  _initialBalance,
            ["endingBalance"] = _currentBalance,
            ["netChange"] = _currentBalance - _initialBalance,
            ["totalMoneyWagered"] = _totalMoneyWagered,
            ["totalMoneyWon"] = _totalMoneyWon,
            ["largestBalanceThroughoutSimulation"] = _largestBalanceDuringSimulation,
            ["nonceOfLargestBalance"] = _largestBalanceDuringSimulationNonce,
            ["smallestBalanceThroughoutSimulation"] = _smallestBalanceDuringSimulation,
            ["nonceOfSmallestBalance"] = _smallestBalanceDuringSimulationNonce,
            ["returnToPlayerPercent"] = _totalMoneyWon / _totalMoneyWagered * 100,
            ["baseBetSize"] = _bettingStrategy.GetBaseBetSize(),
            ["onWinPercentBetIncrease"] = _bettingStrategy.GetBetChangePercentOnWin() ?? 0,
            ["onLossPercentBetIncrease"] =  _bettingStrategy.GetBetChangePercentOnLoss() ?? 0,
            ["netGainThreshold"] = _bettingStrategy.GetStopOnNetGain() ?? 0,
            ["netLossThreshold"] = _bettingStrategy.GetStopOnNetLoss() ?? 0,
            ["writtenDetailedReport"] = SimulationReport(numberOfBetsPlaced)
        };
        return jsonReport;
    }
    
    private void ResetVariables()
    {
        _currentBalance = _initialBalance;
        _totalMoneyWagered = 0;
        _totalMoneyWon = 0;
        _numberOfWins = 0;
        _longestWinningStreak = 0;
        _longestLosingStreak = 0;
        _largestBalanceDuringSimulation = _initialBalance;
        _smallestBalanceDuringSimulation = _initialBalance;
        _largestBalanceDuringSimulationNonce = _plinkoReverseEngineer.GetAlgorithm().GetStartingNonce();
        _smallestBalanceDuringSimulationNonce = _plinkoReverseEngineer.GetAlgorithm().GetStartingNonce();
        _bettingStrategy.SetCurrentBetSize(_bettingStrategy.GetBaseBetSize());
    }
    
    internal Dictionary<string, object> RunPlinkoSimulation()
    {
        ResetVariables();
        for (var spinNumber = 0; spinNumber < _numberOfPlays; spinNumber++)
        {
            _currentBalance -= _bettingStrategy.GetCurrentBetSize();
            _totalMoneyWagered += _bettingStrategy.GetCurrentBetSize();
            double result = _plinkoReverseEngineer.GetPayoutMultiplier();
            _currentBalance += _bettingStrategy.GetCurrentBetSize()*result;
            _totalMoneyWon += _bettingStrategy.GetCurrentBetSize()*result;
            if (_bettingStrategy.NetChangeReached(moneyWagered: _totalMoneyWagered, moneyWon: _totalMoneyWon))
            {
                return SimulationJsonReport(spinNumber+1);
            }
            UpdateGameState(result);
            if (_bettingStrategy.GetCurrentBetSize() <= _currentBalance) continue;
            return SimulationJsonReport(spinNumber+1);
        }
        return SimulationJsonReport(_numberOfPlays);
    }
}