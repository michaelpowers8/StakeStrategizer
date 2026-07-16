namespace ProvablyFairSimulation.ReverseEngineers;

internal class MinesReverseEngineer
{
    //private const double Rtp = 0.99;
    private const int MinimumNumberOfMines = 1;
    private const int MaximumNumberOfMines = 24;
    private const int GridSize = 25;
    private readonly ProvablyFairAlgorithm _algorithm;
    private readonly int _numberOfMines;

    internal MinesReverseEngineer(ProvablyFairAlgorithm algorithm, int numberOfMines)
    {
        _algorithm = algorithm;
        _numberOfMines = numberOfMines;
    }

    private List<double> GetResultNumbers()
    {
        var multipliers = new List<double>();
        for (int i = 25; i >= 2; i--)
        {
            multipliers.Add(i);
        }
        return _algorithm.RandomStakeNumbers(multipliers);
    }

    private static List<int> NumbersToShuffle(List<double> numbers)
    {
        List<int> originalShuffle = new List<int>();
        List<int> finalShuffle = new List<int>();
        for (int i = 0; i < GridSize; i++)
        {
            originalShuffle.Add(i);
        }

        foreach (var number in numbers)
        {
            int truncatedNumber = (int)number;
            finalShuffle.Add(originalShuffle[truncatedNumber]);
            originalShuffle.RemoveAt(truncatedNumber);
        }
        return finalShuffle;
    }
    
    private List<int> ShuffleToValues(List<int> shuffle)
    {
        if (_numberOfMines < MinimumNumberOfMines || _numberOfMines > MaximumNumberOfMines)
        {
            throw new ArgumentException($"Invalid number of mines. Must be between {MinimumNumberOfMines}-{MaximumNumberOfMines} inclusive");
        }
        return shuffle.GetRange(0, _numberOfMines);
    }

    private List<(int, int)> ValuesToMinePositions(List<int> values)
    {
        List<(int, int)> minesPositions = new List<(int, int)>();
        foreach (var value in values)
        {
            int x = value % 5 + 1;
            int y = 5 - value / 5;
            minesPositions.Add((x, y));
        }

        return minesPositions;
    }

    internal List<(int, int)> GetMinesPositions()
    {
        List<double> resultNumbers = GetResultNumbers();
        List<int> finalShuffle = NumbersToShuffle(resultNumbers);
        List<int> finalValues = ShuffleToValues(finalShuffle);
        List<(int, int)> minesPositions = ValuesToMinePositions(finalValues);
        return minesPositions;
    }
}