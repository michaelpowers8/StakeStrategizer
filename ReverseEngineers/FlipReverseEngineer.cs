namespace ProvablyFairSimulation.ReverseEngineers;

internal class FlipReverseEngineer
{
    private const double BytesToNumberMultiplier = 1;
    private readonly ProvablyFairAlgorithm _algorithm;

    internal FlipReverseEngineer(ProvablyFairAlgorithm algorithm)
    {
        _algorithm = algorithm;
    }
    
    private List<double> GetResultNumbers()
    {
        return _algorithm.RandomStakeNumbers(Enumerable.Repeat(BytesToNumberMultiplier, 20).ToList());
    }

    private static List<string> NumbersToValues(List<double> numbers)
    {
        List<string> values = new List<string>();
        foreach (var number in numbers)
        {
            if (number <= 0.5)
            {
                values.Add("Tails");
            }
            else
            {
                values.Add("Heads");
            }
        }
        return values;
    }

    internal List<string> GetFlipValues()
    {
        List<double> numbers = GetResultNumbers();
        return NumbersToValues(numbers);
    }
}