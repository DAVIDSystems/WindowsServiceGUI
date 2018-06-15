using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
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
        }

        protected override void OnStop()
        {
            LogMessage("Successfully stopped Test Service !");
        }   
     }
}
