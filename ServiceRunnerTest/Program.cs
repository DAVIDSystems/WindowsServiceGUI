using DigaSystem.ServiceRunner;

namespace ServiceRunnerTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)        {
            ServiceBaseEx[] ServicesToRun;
            ServicesToRun = new ServiceBaseEx[]
            {
                new TestService()
            };

            if (ServiceRunner.RunningAsService)
            {
                ServicesToRun.LoadServices();
            }
            else
            {
                ServicesToRun.StartServices();
            }
        }
    }
}
