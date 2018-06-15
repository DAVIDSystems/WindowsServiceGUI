using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks.Schedulers;
using System.Runtime.InteropServices;

namespace DigaSystem.ServiceRunner
{
    public static class ServiceRunner
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);
        const int STD_OUTPUT_HANDLE = -11;

        public static void LoadServices(this IEnumerable<ServiceBaseEx> services)
        {
            if (Debugger.IsAttached)
            {
                var t = Task.Factory.StartNew(() =>
                {
                    WindowControl app = new WindowControl();
                    app.Title = "Service Runner";
                    app.SetService(services);
                    app.ShowDialog();
                }, CancellationToken.None, TaskCreationOptions.PreferFairness, new StaTaskScheduler(25));
                t.Wait();
            }
            else
            {
                ServiceBase.Run(services.ToArray());
            }
        }

        public static void StartServices(this IEnumerable<ServiceBaseEx> services)
        {
            var t = Task.Factory.StartNew(() =>
            {
                WindowControl app = new WindowControl();
                app.Title = "Service Runner";
                app.SetService(services);
                app.ShowDialog();
            }, CancellationToken.None, TaskCreationOptions.PreferFairness, new StaTaskScheduler(25));
            t.Wait();
        }

        public static bool RunningAsService
        {
            get 
            {
                var p = ParentProcessUtilities.GetParentProcess();
                return (p != null && p.ProcessName == "services");
            }
        }
    }

    public enum ServiceState
    {
        Stopped,
        Started,
        Paused,
        Starting,
        Stopping,
        Pausing,
        Failed
    }
}
