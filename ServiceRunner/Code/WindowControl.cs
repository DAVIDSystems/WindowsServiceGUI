using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Reflection;
using System.Runtime.InteropServices;

namespace DigaSystem.ServiceRunner
{
    public partial class WindowControl : Form
    {
  
        public ServiceBaseEx _theService;

        public WindowControl()
        {
            InitializeComponent();
        }

        #region Properties

        public string Title
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        #endregion

        #region Gui_Events

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartService();    
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopService();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Get a handle to a copy of this form's system (window) menu
            IntPtr hSysMenu = GetSystemMenu(this.Handle, false);

            // Add a separator
            AppendMenu(hSysMenu, MF_SEPARATOR, 0, string.Empty);

            // Add the About menu item
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_ABOUT_ID, "&About…");
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_INSTALL_ID, "&Install as Service…");
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_UNINSTALL_ID, "&Uninstall Service…");

            if (!Utility.IsAdministrator())
            {
                EnableMenuItem(hSysMenu, SYSMENU_INSTALL_ID, 3);
                EnableMenuItem(hSysMenu, SYSMENU_UNINSTALL_ID, 3);
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Test if the About item was selected from the system menu
            if (m.Msg == WM_SYSCOMMAND) 
            {
                int command = (int)m.WParam;

                if (command == SYSMENU_ABOUT_ID)
                {
                    MessageBox.Show("DAVID Systems ServiceRunner V 1.0");
                }

                if (command == SYSMENU_INSTALL_ID)
                {
                    InstallService();
                }

                if (command == SYSMENU_UNINSTALL_ID)
                {
                    UninstallService();
                }
            }
        }

        #endregion

        #region Misc

        private void StartService()
        {
            MethodInfo onStartMethod = typeof(ServiceBaseEx).GetMethod("OnStart", BindingFlags.Instance | BindingFlags.NonPublic);
            try
            {
                onStartMethod.Invoke(_theService, new object[] { new string[] { } });
                bool erg = _theService.HasFailed;
                if (erg)
                    DisplayServiceStatus(ServiceState.Failed);
                else
                    DisplayServiceStatus(ServiceState.Started);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to start: " + _theService.ServiceName + ex.Message);
            }
        }
 
        private void StopService()
        {
            MethodInfo onStopMethod = typeof(ServiceBaseEx).GetMethod("OnStop", BindingFlags.Instance | BindingFlags.NonPublic);
            try
            {
                onStopMethod.Invoke(_theService, null);
                DisplayServiceStatus(ServiceState.Stopped);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to stop: " + _theService.ServiceName + ex.Message);
            }
        }

        private void InstallService()
        {
            InstallServiceDlg installDlg = new InstallServiceDlg();
            installDlg.ShowDialog();
        }

        private void UninstallService()
        {
            UninstallServiceDlg uninstallDlg = new UninstallServiceDlg();
            uninstallDlg.ShowDialog();
        }

        public void SetService(IEnumerable<ServiceBaseEx> services)
        {
            _theService = services.First();
            _theService.sendMessage += _theService_sendMessage;
            DisplayServiceStatus(ServiceState.Stopped);
        }

        void _theService_sendMessage(object sender, string e)
        {
            DateTime dtNow = DateTime.Now;
            string prefix = dtNow.ToString("dd.MM.yyyy HH:mm:ss.fff ");
            WriteLine(prefix + e);
        }

        #endregion

        #region Gui-Display

        private void DisplayServiceStatus(ServiceState state)
        {
            string sname = _theService.ServiceName;

            lbStatus.Text = string.Format("Service: '{0}' {1}...", sname, state.ToString());

            switch (state)
            {
                case ServiceState.Failed:
                case ServiceState.Stopped:
                case ServiceState.Paused:
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                    break;
                case ServiceState.Started:
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                    break;
                case ServiceState.Pausing:
                case ServiceState.Starting:
                case ServiceState.Stopping:
                    btnStart.Enabled = false;
                    btnStop.Enabled = false;
                    break;                
            }
        }

        private void WriteLine(string message)
        {
            rtbOutput.InvokeIfRequired(() =>
            {
                int len = rtbOutput.TextLength;
                if (len > 10000)
                {
                    rtbOutput.Select(0, 5000);
                    rtbOutput.SelectedText = "";
                }
                rtbOutput.AppendText(message + Environment.NewLine);
                rtbOutput.SelectionStart = rtbOutput.Text.Length; //Set the current caret position at the end
                rtbOutput.ScrollToCaret(); //Now scroll it automatically
            });

        }

        #endregion

        #region SystemMenu

        private const int WM_SYSCOMMAND = 0x112;
        private const int MF_STRING = 0x0;
        private const int MF_SEPARATOR = 0x800;

        // P/Invoke declarations
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool EnableMenuItem(IntPtr hMenu, int uIDEnableItem, uint uEnable);

        // ID for the About item on the system menu
        private int SYSMENU_ABOUT_ID = 0x1;
        private int SYSMENU_INSTALL_ID = 0x2;
        private int SYSMENU_UNINSTALL_ID = 0x3;

        #endregion
    }

    public static class AutoInvoke
    {
        public static void InvokeIfRequired(this Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }

}
