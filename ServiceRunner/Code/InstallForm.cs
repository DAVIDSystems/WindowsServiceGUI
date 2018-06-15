using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Security.Principal;

namespace DigaSystem.ServiceRunner
{
    public partial class InstallServiceDlg : Form
    {
        #region Members

        private ServiceInfos _infos;

        #endregion

        #region Init

        public InstallServiceDlg()
        {
            InitializeComponent();
        }

        private void OnInit(object sender, EventArgs e)
        {
            InitInfos();
            DisplayInfos();
        }

        public void InitInfos()
        {
            string fname = Utility.GetInfoFile();
            if (File.Exists(fname))
            {
                _infos = ServiceInstaller.DeSerializeObject<ServiceInfos>(fname);
            }
            else
            {
                _infos = new ServiceInfos();
                _infos.ServiceType = ServiceInstaller.ServiceBootFlag.AutoStart;
                _infos.ServiceName = Utility.GetServiceName();
            }
        }

        #endregion

        #region GUI-Display

        public void DisplayInfos()
        {
            tbServiceName.Text = _infos.ServiceName;
            tbDisplayName.Text = _infos.DisplayName;
            tbServiceDescription.Text = _infos.ServiceDescription;
            dlStartType.SelectedIndex = TypeToIndex(_infos.ServiceType);
            if (string.IsNullOrWhiteSpace(_infos.ServiceAccount))
            {
                tbServiceAccount.Text = "";
                tbServicePassword.Text = "";
                cbLocalSystem.Checked = true;
                SetAccount(true);
            }
            else
            {
                SetAccount(false);
                tbServiceAccount.Text = _infos.ServiceAccount;
                tbServicePassword.Text = _infos.ServicePassword;
            }
        }

        #endregion

        #region Public

        public ServiceInfos GetInfos()
        {
            ServiceInfos info = new ServiceInfos();
            info.ServiceName = tbServiceName.Text;
            info.DisplayName = tbDisplayName.Text;
            info.ServiceDescription = tbServiceDescription.Text;
            info.ServiceType = IndexToType(dlStartType.SelectedIndex);
            if (cbLocalSystem.Checked)
            {
                info.ServiceAccount = null;
                info.ServicePassword = null;
            }
            else
            {
                info.ServiceAccount = tbServiceAccount.Text;
                info.ServicePassword = tbServicePassword.Text;
            }

            return info;
        }

        public void SetServiceInfos(ServiceInfos info)
        {
            _infos = info;
            DisplayInfos();
        }

        #endregion

        #region Logik

        private void SetAccount(bool isChecked)
        {
            if (isChecked)
            {
                tbServiceAccount.Enabled = false;
                tbServicePassword.Enabled = false;
                tbServiceAccount.Text = "";
                tbServicePassword.Text = "";
            }
            else
            {
                tbServiceAccount.Enabled = true;
                tbServicePassword.Enabled = true;
            }
        }

        private int TypeToIndex(ServiceInstaller.ServiceBootFlag flag)
        {
            int index;

            switch(flag)
            {
                case ServiceInstaller.ServiceBootFlag.AutoStart:
                    index = 0;
                    break;
                case ServiceInstaller.ServiceBootFlag.DemandStart:
                    index = 1;
                    break;
                case ServiceInstaller.ServiceBootFlag.Disabled:
                    index = 3;
                    break;
                default:
                    index = 1;
                    break;
            }

            return index;
        }

        private ServiceInstaller.ServiceBootFlag IndexToType(int index)
        {
            if (index == 0)
            {
                return ServiceInstaller.ServiceBootFlag.AutoStart;
            }

            if (index == 1)
            {
                return ServiceInstaller.ServiceBootFlag.DemandStart;
            }

            return ServiceInstaller.ServiceBootFlag.Disabled;
        }

        private bool IsValidServiceInfo()
        {
            var infos = GetInfos();

            if (string.IsNullOrWhiteSpace(infos.ServiceName))
            {
                MessageBox.Show("Service Name empty !", "Install Service", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            bool isInstalled = ServiceInstaller.ServiceIsInstalled(infos.ServiceName);
            if (isInstalled)
            {
                MessageBox.Show("Service allready installed !", "Install Service", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (string.IsNullOrWhiteSpace(infos.DisplayName))
            {
                MessageBox.Show("Display Name empty !", "Install Service", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (string.IsNullOrWhiteSpace(infos.ServiceDescription))
            {
                MessageBox.Show("Display Description empty !", "Install Service", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!Utility.IsAdministrator())
            {
                MessageBox.Show("Service Installation is only possible, when application was started as Administrator !", "Install Service", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }


            return true;
        }

        #endregion

        #region GUI-Events

        private void OnSelectCheckbox(object sender, EventArgs e)
        {
            bool isChecked = cbLocalSystem.Checked;
        }

        private void OnTest(object sender, EventArgs e)
        {
            bool canInstall = IsValidServiceInfo();
            if (canInstall)
            {
                btnInstall.Enabled = true;
            }
        }

        private void OnInstall(object sender, EventArgs e)
        {
            bool canInstall = IsValidServiceInfo();
            if (canInstall)
            {
                var info = GetInfos();
                var result = ServiceInstaller.Install(info.ServiceName, info.DisplayName, Utility.GetFilename());
                if (!result.Success)
                {
                    MessageBox.Show(result.Error, "Install Service", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    string fname = Utility.GetInfoFile();
                    ServiceInstaller.SerializeObject(info, fname);
                    MessageBox.Show("Service installed !", "Install Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }

        private void OnCancel(object sender, EventArgs e)
        {
            Close();
        }

        #endregion


    }
}
