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
using System.Diagnostics;


namespace DigaSystem.ServiceRunner
{
    public partial class WindowControl : Form
    {
  
        public ServiceBaseEx _theService;
        private bool _istStarted = false;
        private bool _scrollChecked = true;
        private bool _autoStart = false;
        private Queue<string> _scrollBuffer;
        private int _noScrollCounter;
        private string _errorToken = "error";
        private string _warningToken = "warning";

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

        public bool AutoStart
        {
            get
            {
                return _autoStart;
            }
            set
            {
                _autoStart = value;
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
            AppendMenu(hSysMenu, MF_STRING|MF_CHECKED, SYSMENU_AUTOSCROLL_ID, "&Allow Scrolling");

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
                AppendMenu(hSysMenu, MF_STRING, SYSMENU_RUNAS_ID, "&Run as Administrator");
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
                    MessageBox.Show("DAVIDSystems ServiceRunner V 1.0");
                }

                if (command == SYSMENU_INSTALL_ID)
                {
                    InstallService();
                }

                if (command == SYSMENU_UNINSTALL_ID)
                {
                    UninstallService();
                }

                if (command == SYSMENU_RUNAS_ID)
                {
                    RunAsAdmin();
                }

                if (command == SYSMENU_AUTOSCROLL_ID)
                {
                    ToogleAutoscroll();
                }

            }
        }

        private void OnClickAutoScroll(object sender, EventArgs e)
        {
            ToogleAutoscroll();
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
                {
                    DisplayServiceStatus(ServiceState.Failed);
                    _istStarted = false;
                }
                else
                {
                    DisplayServiceStatus(ServiceState.Started);
                    _istStarted = true;
                }
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
                _istStarted = false;
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

        private void RunAsAdmin()
        {
            if (!Utility.IsAdministrator())
            {
                // Restart program and run as admin
                var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                startInfo.Verb = "runas";
                System.Diagnostics.Process.Start(startInfo);
                if (_istStarted)
                {
                    StopService();
                }
                Application.Exit();
                return;
            }
        }

        public void SetService(IEnumerable<ServiceBaseEx> services)
        {
            _theService = services.First();
            _theService._logEvent += _theService__logEvent;
            _theService._setEvent += _theService__setEvent;
            DisplayServiceStatus(ServiceState.Stopped);
        }

        private void _theService__setEvent(string error, string warning)
        {
            _errorToken = error;
            _warningToken = warning;
        }

        private void _theService__logEvent(string message)
        {
            DateTime dtNow = DateTime.Now;
            string prefix = dtNow.ToString("dd.MM.yyyy HH:mm:ss.fff ");
            WriteLine(prefix + message);
        }

        private void ToogleAutoscroll()
        {
            IntPtr hSysMenu = GetSystemMenu(this.Handle, false);
            if (_scrollChecked)
            {
                CheckMenuItem(hSysMenu, (uint)SYSMENU_AUTOSCROLL_ID, 0x0);
            }
            else
            {
                CheckMenuItem(hSysMenu, (uint)SYSMENU_AUTOSCROLL_ID, 0x8);
            }

            _scrollChecked = !_scrollChecked;
            tsAutoScroll.Checked = _scrollChecked;
            if (_scrollChecked)
            {
                this.tsScrollButton.Image = global::DigaSystem.ServiceRunner.Properties.Resources.scroller;
            }
            else
            {
                this.tsScrollButton.Image = global::DigaSystem.ServiceRunner.Properties.Resources.scroller_red;
            }

        }

        private void OnInitDialog(object sender, EventArgs e)
        {
            _scrollBuffer = new Queue<string>();

            if (_autoStart == true)
            {
                StartService();
            }
            _noScrollCounter = 0;
            rtbOutput.ViewWasScrolled += RtbOutput_ViewWasScrolled;
            rtbOutput.MouseDown += RtbOutput_MouseUp;
            rtbOutput._scrollEvent += RtbOutput__scrollEvent;
        }

        private void RtbOutput__scrollEvent(int pos, int max)
        {
            if (pos > max)
            {
                ToogleAutoscroll();
            }
        }

        private void RtbOutput_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {   //click event
                //MessageBox.Show("you got it!");
                ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
                MenuItem menuItem = new MenuItem("Copy");
                menuItem.Click += new EventHandler(CopyAction);
                contextMenu.MenuItems.Add(menuItem);

                rtbOutput.ContextMenu = contextMenu;
            }
        }

        void CopyAction(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(rtbOutput.SelectedText))
            {
                Clipboard.SetText(rtbOutput.SelectedText);
            }
        }

        private void RtbOutput_ViewWasScrolled(object sender, EventArgs e)
        {
            if (_scrollChecked)
            {
                _noScrollCounter = 0;
                ToogleAutoscroll();
            }
        }

        private void CheckScroll(object sender, EventArgs e)
        {
            if (!_scrollChecked)
            {
                _noScrollCounter++;
                if (_noScrollCounter > 35)
                {
                    ToogleAutoscroll();
                    WriteLine("");
                }
            }
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

            if (_scrollChecked == false)
            {
                _scrollBuffer.Enqueue(message);
                return;
            }

            rtbOutput.InvokeIfRequired(() =>
            {
                if (_scrollBuffer.Count > 0)
                {
                    do
                    {
                        RenderMessage(_scrollBuffer.Dequeue(), rtbOutput.TextLength);

                        if (rtbOutput.Lines.Length > 6000)
                        {
                            rtbOutput.Select(0, rtbOutput.GetFirstCharIndexFromLine(1000));
                            rtbOutput.SelectedText = "";
                        }

                    } while (_scrollBuffer.Count > 0);
                }

                if (string.IsNullOrEmpty(message))
                {
                    return;
                }

                if (rtbOutput.Lines.Length > 6000)
                {
                    rtbOutput.Select(0, rtbOutput.GetFirstCharIndexFromLine(1000));
                    rtbOutput.SelectedText = "";
                }

                RenderMessage(message, rtbOutput.TextLength);

                rtbOutput.SelectionStart = rtbOutput.Text.Length; //Set the current caret position at the end
                rtbOutput.ScrollToCaret(); //Now scroll it automatically
            });

        }

        private void RenderMessage(string message, int len)
        {
            rtbOutput.AppendText(message + Environment.NewLine);
            if (message.Contains(_errorToken))
            {
                rtbOutput.Select(len, message.Length);
                rtbOutput.SelectionColor = Color.OrangeRed;
                rtbOutput.Select();
            }
            if (message.Contains(_warningToken))
            {
                rtbOutput.Select(len, message.Length);
                rtbOutput.SelectionColor = Color.Orange;
                rtbOutput.Select();
            }
        }

        #endregion

        #region SystemMenu

        private const int WM_SYSCOMMAND = 0x112;
        private const int MF_STRING = 0x0;
        private const int MF_SEPARATOR = 0x800;
        private const int MF_CHECKED = 0x8;


        // P/Invoke declarations
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool EnableMenuItem(IntPtr hMenu, int uIDEnableItem, uint uEnable);

        [DllImport("user32.dll")]
        static extern bool SetMenuItemInfo(IntPtr hMenu, uint uItem, bool fByPosition,
           [In] ref MENUITEMINFO lpmii);

        [DllImport("user32.dll")]
        static extern uint GetMenuItemID(IntPtr hMenu, int nPos);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool GetMenuItemInfo(IntPtr hMenu, int uItem, bool fByPosition, MENUITEMINFO lpmii);

        [DllImport("user32.dll")]
        public static extern uint CheckMenuItem(IntPtr hmenu, uint uIDCheckItem, uint uCheck);

        public const UInt32 MF_BYCOMMAND = 0x00000000;
        public const UInt32 MF_BYPOSITION = 0x00000400;

        // ID for the About item on the system menu
        private int SYSMENU_ABOUT_ID = 0x1;
        private int SYSMENU_INSTALL_ID = 0x2;
        private int SYSMENU_UNINSTALL_ID = 0x3;
        private int SYSMENU_RUNAS_ID = 0x4;
        private int SYSMENU_AUTOSCROLL_ID = 0x5;

        [StructLayout(LayoutKind.Sequential)]
        public class MENUITEMINFO
        {
            public int cbSize;
            public uint fMask;
            public uint fType;
            public uint fState;
            public uint wID;
            public IntPtr hSubMenu;
            public IntPtr hbmpChecked;
            public IntPtr hbmpUnchecked;
            public IntPtr dwItemData;
            public IntPtr dwTypeData;
            public uint cch;
            public IntPtr hbmpItem;

            public MENUITEMINFO()
            {
                cbSize = Marshal.SizeOf(typeof(MENUITEMINFO));
            }
        }
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
