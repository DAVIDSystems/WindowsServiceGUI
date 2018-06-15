namespace DigaSystem.ServiceRunner
{
    partial class UninstallServiceDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbServiceName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUninstall = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbMessage = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbServiceName
            // 
            this.tbServiceName.Enabled = false;
            this.tbServiceName.Location = new System.Drawing.Point(12, 26);
            this.tbServiceName.Name = "tbServiceName";
            this.tbServiceName.Size = new System.Drawing.Size(424, 20);
            this.tbServiceName.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Service Name:";
            // 
            // btnUninstall
            // 
            this.btnUninstall.Location = new System.Drawing.Point(128, 86);
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.Size = new System.Drawing.Size(75, 23);
            this.btnUninstall.TabIndex = 5;
            this.btnUninstall.Text = "UnInstall";
            this.btnUninstall.UseVisualStyleBackColor = true;
            this.btnUninstall.Click += new System.EventHandler(this.OnUninstall);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Info:";
            // 
            // lbMessage
            // 
            this.lbMessage.AutoSize = true;
            this.lbMessage.Location = new System.Drawing.Point(46, 58);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(22, 13);
            this.lbMessage.TabIndex = 7;
            this.lbMessage.Text = ".....";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(230, 86);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnCancel);
            // 
            // UninstallServiceDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 123);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnUninstall);
            this.Controls.Add(this.tbServiceName);
            this.Controls.Add(this.label1);
            this.Name = "UninstallServiceDlg";
            this.Text = "Uninstall Service";
            this.Load += new System.EventHandler(this.OnInit);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbServiceName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUninstall;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.Button btnCancel;
    }
}