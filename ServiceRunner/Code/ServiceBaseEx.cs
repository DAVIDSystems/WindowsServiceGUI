using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DigaSystem.ServiceRunner
{
    public class ServiceBaseEx : ServiceBase
    {
        private bool _status = false;
        private static ServiceBaseEx _instance;

        public event EventHandler<string> sendMessage;

        public ServiceBaseEx()
        {
            _instance = this;
        }

        public bool HasFailed
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        protected virtual void OnSendMessage(string e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<string> handler = sendMessage;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public static void LogMessage(string msg)
        {
            if (_instance != null)
            {
                _instance.OnSendMessage(msg);
            }
        }

        protected virtual void OnQuit(string[] args)
        {
            // Do nothing
        }
    }
}
