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

        public delegate void SendLogMessage(string message);
        public event SendLogMessage _logEvent;

        public delegate void SetErrorWarning(string error, string warning);
        public event SetErrorWarning _setEvent;
        
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

        protected virtual void OnSendMessage(string message)
        {
            _logEvent?.Invoke(message);
        }

        protected virtual void OnSetMessage(string error, string warning)
        {
            _setEvent?.Invoke(error, warning);
        }

        public static void LogMessage(string msg)
        {
            _instance?.OnSendMessage(msg);
        }

        public static void SetErrorWarningString(string error, string warning)
        {
            _instance?.OnSetMessage(error, warning);
        }

        protected virtual void OnQuit(string[] args)
        {
            // Do nothing
        }
    }
}
