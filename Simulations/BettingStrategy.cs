namespace ProvablyFairSimulation.Simulations;

public class BettingStrategy
{
    private readonly double _baseBetSize;
    private readonly double? _betChangePercentOnWin;
    private readonly double? _betChangePercentOnLoss;
    private double _currentBetSize;
    private readonly double? _stopOnNetGain;
    private readonly double? _stopOnNetLoss;

    internal BettingStrategy()
    {
        _baseBetSize = 1;
        _betChangePercentOnWin = null;
        _betChangePercentOnLoss = null;
        _stopOnNetGain = null;
        _stopOnNetLoss = null;
        _currentBetSize = _baseBetSize;
    }
    
    internal BettingStrategy(double baseBetSize)
    {
        _baseBetSize = baseBetSize;
        _betChangePercentOnWin = null;
        _betChangePercentOnLoss = null;
        _stopOnNetGain = null;
        _stopOnNetLoss = null;
        _currentBetSize = _baseBetSize;
    }

    internal BettingStrategy(double baseBetSize, double betChangePercent, bool? isChangeOnWin)
    {
        _baseBetSize = baseBetSize;
        if (isChangeOnWin ?? false)
        {
            _betChangePercentOnWin = betChangePercent;
            _betChangePercentOnLoss = null;
        }
        else
        {
            _betChangePercentOnWin = null;
            _betChangePercentOnLoss = betChangePercent;
        }
        _stopOnNetGain = null;
        _stopOnNetLoss = null;
        _currentBetSize = _baseBetSize;
    }

    internal BettingStrategy(double baseBetSize, double stopOnNetChange, bool isOnNetGain)
    {
        _baseBetSize = baseBetSize;
        _betChangePercentOnWin = null;
        _betChangePercentOnLoss = null;
        if (isOnNetGain)
        {
            _stopOnNetGain = stopOnNetChange;
            _stopOnNetLoss = null;
        }
        else
        {
            _stopOnNetGain = null;
            _stopOnNetLoss = stopOnNetChange;
        }
        _currentBetSize = _baseBetSize;
    }
    
    internal BettingStrategy(double baseBetSize, double stopOnNetGain, double stopOnNetLoss)
    {
        _baseBetSize = baseBetSize;
        _betChangePercentOnWin = null;
        _betChangePercentOnLoss = null;
        _stopOnNetGain = stopOnNetGain;
        _stopOnNetLoss = stopOnNetLoss;
        _currentBetSize = _baseBetSize;
    }

    internal BettingStrategy(double baseBetSize, double betChangePercent, bool isChangeOnWin, double stopOnNetChange, bool isOnNetGain)
    {
        _baseBetSize = baseBetSize;
        switch(isChangeOnWin)
        {
            case true:
                _betChangePercentOnWin = betChangePercent;
                _betChangePercentOnLoss = null;
                break;
            case false:
                _betChangePercentOnWin = null;
                _betChangePercentOnLoss = betChangePercent;
                break;
        }

        if (isOnNetGain)
        {
            _stopOnNetGain = stopOnNetChange;
            _stopOnNetLoss = null;
        }
        else
        {
            _stopOnNetGain = null;
            _stopOnNetLoss = stopOnNetChange;
        }
        _currentBetSize = _baseBetSize;
    }
    
    internal BettingStrategy(double baseBetSize, double? betChangeOnWin, double? betChangeOnLoss, double? stopOnNetGain, double? stopOnNetLoss)
    {
        _baseBetSize = baseBetSize;
        _betChangePercentOnWin = betChangeOnWin;
        _betChangePercentOnLoss = betChangeOnLoss;
        _stopOnNetGain = stopOnNetGain;
        _stopOnNetLoss = stopOnNetLoss;
        _currentBetSize = _baseBetSize;
    }

    public double GetBaseBetSize()
    {
        return _baseBetSize;
    }

    public double? GetBetChangePercentOnWin()
    {
        return _betChangePercentOnWin;
    }
    
    public double? GetBetChangePercentOnLoss()
    {
        return _betChangePercentOnLoss;
    }

    public double? GetStopOnNetGain()
    {
        return _stopOnNetGain;
    }
    
    public double? GetStopOnNetLoss()
    {
        return _stopOnNetLoss;
    }

    public double GetCurrentBetSize()
    {
        return _currentBetSize;
    }

    public void SetCurrentBetSize(double newBetSize)
    {
        _currentBetSize = newBetSize;
    }

    public bool NetChangeReached(double moneyWagered, double moneyWon)
    {
        if (_stopOnNetGain == null && _stopOnNetLoss == null)
        {
            return false;
        }
        double netChange = moneyWon - moneyWagered;
        double absoluteChange = Math.Abs(netChange);
        if (netChange >= _stopOnNetGain)
        {
            return true;
        }
        return netChange < 0 && absoluteChange > _stopOnNetLoss;
    }
}