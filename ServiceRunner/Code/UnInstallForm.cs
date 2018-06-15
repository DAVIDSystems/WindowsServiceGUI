using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigaSystem.ServiceRunner
{
    public partial class UninstallServiceDlg : Form
    {
        private ServiceInfos _infos;
        private string _infoFile;

        public UninstallServiceDlg()
        {
            InitializeComponent();
        }

        private void OnInit(object sender, EventArgs e)
        {
            _infoFile = Utility.GetInfoFile();
            if (File.Exists(_infoFile))
            {
                _infos = ServiceInstaller.DeSerializeObject<ServiceInfos>(_infoFile);
            }
            else
            {
                _infos = new ServiceInfos();
                _infos.ServiceType = ServiceInstaller.ServiceBootFlag.AutoStart;
                _infos.ServiceName = Utility.GetServiceName();
            }

            tbServiceName.Text = _infos.ServiceName;

            bool isInstalled = ServiceInstaller.ServiceIsInstalled(tbServiceName.Text);
            bool isAdmin = Utility.IsAdministrator();
            string message;

            if (isInstalled)
            {
                if (isAdmin)
                {
                    message = "Service installed ....";
                    btnUninstall.Enabled = true;
                }
                else {
                    message = "Service installed, but application not started as Administrator...";
                    btnUninstall.Enabled = false;
                }
            }
            else
            {
               message = "Service not installed ....";
                btnUninstall.Enabled = false;
            }

            lbMessage.Text = message;
        }

        private void OnUninstall(object sender, EventArgs e)
        {
            var result = ServiceInstaller.Uninstall(_infos.ServiceName);
            if (result.Success)
            {
                MessageBox.Show("Service uninstalled !", "Uninstall Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (File.Exists(_infoFile))
                {
                    File.Delete(_infoFile);
                }

                Close();
            }
            else
            {
                MessageBox.Show(result.Error, "Uninstall Service", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void OnCancel(object sender, EventArgs e)
        {
            Close();
        }
    }
}
