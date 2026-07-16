using System.Configuration;

namespace ProvablyFairSimulation
{
    internal static class Program
    {
        private static void Main()
        {
            if (bool.TryParse(ConfigurationManager.AppSettings["RunSimulation"], out var runSimulation) && runSimulation)
            {
                RunSimulation.Run();
            }
            if (bool.TryParse(ConfigurationManager.AppSettings["RunSingleSpin"], out var runSingleSpin) && runSingleSpin)
            {
                RunSingleSpin.Run();
            }
        }
    }
}

