namespace ProvablyFairSimulation.ReverseEngineers;

internal class SnakeReverseEngineer
{
    private const double BytesToNumberMultiplier = 6;
    private readonly ProvablyFairAlgorithm _algorithm;
    private readonly string _difficulty;
    private static readonly double[] EasyPayoutMultipliers = [2, 1.3, 1.2, 1.1, 1.01, 0, 1.01, 1.1, 1.2, 1.3, 2];
    private static readonly double[] MediumPayoutMultipliers = [4, 2.5, 1.4, 1.11, 0, 0, 0, 1.11, 1.4, 2.5, 4];
    private static readonly double[] HardPayoutMultipliers = [7.5, 3, 1.38, 0, 0, 0, 0, 0, 1.38, 3, 7.5];
    private static readonly double[] ExpertPayoutMultipliers = [10, 3.82, 0, 0, 0, 0, 0, 0, 0, 3.82, 10];
    private static readonly double[] MasterPayoutMultipliers = [17.64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 17.64];
    
    internal SnakeReverseEngineer(ProvablyFairAlgorithm algorithm, string difficulty)
    {
        _algorithm = algorithm;
        _difficulty = difficulty;
    }

    private List<double> GetResultNumbers()
    {
        return _algorithm.RandomStakeNumbers(Enumerable.Repeat(BytesToNumberMultiplier, 10).ToList());
    }

    private List<string> NumbersToRolls(List<double> numbers)
    {
        List<string> snakeRolls = new List<string>();
        for(int i = 0; i < numbers.Count; i+=2)
        {
            int truncatedNumberOne = (int)numbers[i];
            int rollOne = truncatedNumberOne + 1;
            int truncatedNumberTwo = (int)numbers[i + 1];
            int rollTwo = truncatedNumberTwo + 1;
            int rollSum = rollOne + rollTwo;
            snakeRolls.Add($"Roll {i / 2 + 1}: {rollOne} + {rollTwo} = {rollSum}");
        }
        return snakeRolls;
    }

    private List<double> NumbersToPayoutMultipliers(List<double> numbers)
    {
        List<double> snakeRolls = new List<double>();
        for(int i = 0; i < numbers.Count; i+=2)
        {
            int truncatedNumberOne = (int)numbers[i];
            int rollOne = truncatedNumberOne + 1;
            int truncatedNumberTwo = (int)numbers[i + 1];
            int rollTwo = truncatedNumberTwo + 1;
            int rollSum = rollOne + rollTwo;
            //snakeRolls.Add($"Roll {i / 2 + 1}: {rollOne} + {rollTwo} = {rollSum}");
        }
        return snakeRolls;
    }

    internal List<string> GetSnakeRolls()
    {
        List<double> stakeNumbers = GetResultNumbers();
        List<string> snakeRolls = NumbersToRolls(stakeNumbers);
        return snakeRolls;
    }
}