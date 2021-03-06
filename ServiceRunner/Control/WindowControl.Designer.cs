﻿namespace DigaSystem.ServiceRunner
{
    partial class WindowControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowControl));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsScrollButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsAutoScroll = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.rtbOutput = new System.Windows.Forms.RTFScrolledBottom();
            this.tmScrollCheck = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatus,
            this.tsScrollButton});
            this.statusStrip1.Location = new System.Drawing.Point(0, 399);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1475, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbStatus
            // 
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(113, 17);
            this.lbStatus.Text = "Service X Stopped....";
            // 
            // tsScrollButton
            // 
            this.tsScrollButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsScrollButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAutoScroll});
            this.tsScrollButton.Image = global::DigaSystem.ServiceRunner.Properties.Resources.scroller;
            this.tsScrollButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsScrollButton.Name = "tsScrollButton";
            this.tsScrollButton.Size = new System.Drawing.Size(29, 20);
            this.tsScrollButton.Text = "toolStripDropDownButton1";
            // 
            // tsAutoScroll
            // 
            this.tsAutoScroll.Checked = true;
            this.tsAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsAutoScroll.Name = "tsAutoScroll";
            this.tsAutoScroll.Size = new System.Drawing.Size(153, 22);
            this.tsAutoScroll.Text = "Allow Scrolling";
            this.tsAutoScroll.Click += new System.EventHandler(this.OnClickAutoScroll);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Image = global::DigaSystem.ServiceRunner.Properties.Resources.control_stop_blue;
            this.btnStop.Location = new System.Drawing.Point(12, 204);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(172, 187);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Image = global::DigaSystem.ServiceRunner.Properties.Resources.control_play_blue;
            this.btnStart.Location = new System.Drawing.Point(10, 11);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(172, 187);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // rtbOutput
            // 
            this.rtbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbOutput.BackColor = System.Drawing.Color.Black;
            this.rtbOutput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbOutput.ForeColor = System.Drawing.Color.White;
            this.rtbOutput.Location = new System.Drawing.Point(188, 11);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(1275, 380);
            this.rtbOutput.TabIndex = 5;
            this.rtbOutput.Text = "";
            this.rtbOutput.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDownEvent);
            // 
            // tmScrollCheck
            // 
            this.tmScrollCheck.Enabled = true;
            this.tmScrollCheck.Interval = 1000;
            this.tmScrollCheck.Tick += new System.EventHandler(this.CheckScroll);
            // 
            // WindowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1475, 421);
            this.Controls.Add(this.rtbOutput);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WindowControl";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "WindowControl";
            this.Load += new System.EventHandler(this.OnInitDialog);
            this.Shown += new System.EventHandler(this.OnDialogShown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbStatus;
        private System.Windows.Forms.RTFScrolledBottom rtbOutput;
        private System.Windows.Forms.ToolStripDropDownButton tsScrollButton;
        private System.Windows.Forms.ToolStripMenuItem tsAutoScroll;
        private System.Windows.Forms.Timer tmScrollCheck;
    }
}