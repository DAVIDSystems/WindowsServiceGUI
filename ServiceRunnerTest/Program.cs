using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DigaSystem.ServiceRunner;

namespace ServiceRunnerTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
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

        static void Install(bool uninstall, string[] args)
        {
            try
            {
                using (AssemblyInstaller inst = new AssemblyInstaller("ServiceRunnerTest.exe", args))
                {
                    IDictionary state = new Hashtable();
                    inst.UseNewContext = true;
                    try
                    {
                        if (uninstall)
                        {
                            inst.Uninstall(state);
                        }
                        else
                        {
                            inst.Install(state);
                            inst.Commit(state);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        try
                        {
                            inst.Rollback(state);
                        }
                        catch { }
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
