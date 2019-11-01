
using DigaSystem.ServiceRunner;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceRunnerTest
{
    public partial class TestService : ServiceBaseEx
    {
        private bool _running;
        static AutoResetEvent _stopEvent;
        private Random _random;

        public TestService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _running = true;
            _stopEvent = new AutoResetEvent(false);
            _stopEvent.Reset();
            _random = new Random((int) DateTime.Now.Ticks);

            Task.Factory.StartNew(() =>
            {
                LogMessage("Successfully started Test Service !");

                do
                {
                    LogMessage(GetLogMessage());
                    _stopEvent.WaitOne(250, false);
                } while (_running);
            });
        }

        protected override void OnStop()
        {
            _running = false;
            _stopEvent.Set();
            LogMessage("Successfully stopped Test Service !");
        }

        private string GetLogMessage()
        {
            string logMessage = "???";
            int value = _random.Next(1, 4);
            switch(value)
            {
                case 1:
                    logMessage = "Warning | a random warning message";
                    break;
                case 2:
                    logMessage = "Error | a random error message";
                    break;
                case 3:
                    logMessage = "a random normal message";
                    break;
            }

            return logMessage;
        }
     }
}
