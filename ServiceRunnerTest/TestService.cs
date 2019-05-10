
using DigaSystem.ServiceRunner;

namespace ServiceRunnerTest
{
    public partial class TestService : ServiceBaseEx
    {
        public TestService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            LogMessage("Successfully started Test Service !");
            LogMessage("Error|Huhu");
            LogMessage("Successfully started Test Service !");
        }

        protected override void OnStop()
        {
            LogMessage("Successfully stopped Test Service !");
        }   
     }
}
