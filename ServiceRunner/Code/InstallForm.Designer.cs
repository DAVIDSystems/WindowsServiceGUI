namespace DigaSystem.ServiceRunner
{
    partial class InstallServiceDlg
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
            this.btnTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbServiceName = new System.Windows.Forms.TextBox();
            this.tbDisplayName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbServiceAccount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbServicePassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dlStartType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbLocalSystem = new System.Windows.Forms.CheckBox();
            this.tbServiceDescription = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(15, 355);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(126, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.OnTest);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Service Name:";
            // 
            // tbServiceName
            // 
            this.tbServiceName.Location = new System.Drawing.Point(15, 25);
            this.tbServiceName.Name = "tbServiceName";
            this.tbServiceName.Size = new System.Drawing.Size(424, 20);
            this.tbServiceName.TabIndex = 2;
            // 
            // tbDisplayName
            // 
            this.tbDisplayName.Location = new System.Drawing.Point(15, 70);
            this.tbDisplayName.Name = "tbDisplayName";
            this.tbDisplayName.Size = new System.Drawing.Size(424, 20);
            this.tbDisplayName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Display Name:";
            // 
            // tbServiceAccount
            // 
            this.tbServiceAccount.Location = new System.Drawing.Point(15, 211);
            this.tbServiceAccount.Name = "tbServiceAccount";
            this.tbServiceAccount.Size = new System.Drawing.Size(332, 20);
            this.tbServiceAccount.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Service Account:";
            // 
            // tbServicePassword
            // 
            this.tbServicePassword.Location = new System.Drawing.Point(15, 261);
            this.tbServicePassword.Name = "tbServicePassword";
            this.tbServicePassword.PasswordChar = '*';
            this.tbServicePassword.Size = new System.Drawing.Size(424, 20);
            this.tbServicePassword.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 245);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Password:";
            // 
            // dlStartType
            // 
            this.dlStartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dlStartType.FormattingEnabled = true;
            this.dlStartType.Items.AddRange(new object[] {
            "Autostart",
            "Manual",
            "Disabled"});
            this.dlStartType.Location = new System.Drawing.Point(15, 310);
            this.dlStartType.Name = "dlStartType";
            this.dlStartType.Size = new System.Drawing.Size(424, 21);
            this.dlStartType.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 294);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "StartType:";
            // 
            // btnInstall
            // 
            this.btnInstall.Enabled = false;
            this.btnInstall.Location = new System.Drawing.Point(163, 355);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(126, 23);
            this.btnInstall.TabIndex = 11;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.OnInstall);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(313, 355);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(126, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnCancel);
            // 
            // cbLocalSystem
            // 
            this.cbLocalSystem.AutoSize = true;
            this.cbLocalSystem.Location = new System.Drawing.Point(353, 213);
            this.cbLocalSystem.Name = "cbLocalSystem";
            this.cbLocalSystem.Size = new System.Drawing.Size(86, 17);
            this.cbLocalSystem.TabIndex = 13;
            this.cbLocalSystem.Text = "LocalSystem";
            this.cbLocalSystem.UseVisualStyleBackColor = true;
            this.cbLocalSystem.CheckedChanged += new System.EventHandler(this.OnSelectCheckbox);
            // 
            // tbServiceDescription
            // 
            this.tbServiceDescription.Location = new System.Drawing.Point(15, 114);
            this.tbServiceDescription.Multiline = true;
            this.tbServiceDescription.Name = "tbServiceDescription";
            this.tbServiceDescription.Size = new System.Drawing.Size(424, 69);
            this.tbServiceDescription.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Service Description:";
            // 
            // ServiceInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 391);
            this.Controls.Add(this.tbServiceDescription);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbLocalSystem);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dlStartType);
            this.Controls.Add(this.tbServicePassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbServiceAccount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbDisplayName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbServiceName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTest);
            this.Name = "ServiceInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Install Service";
            this.Load += new System.EventHandler(this.OnInit);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbServiceName;
        private System.Windows.Forms.TextBox tbDisplayName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbServiceAccount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbServicePassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox dlStartType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbLocalSystem;
        private System.Windows.Forms.TextBox tbServiceDescription;
        private System.Windows.Forms.Label label5;
    }
}