namespace theDiary.EasyDNS.Windows
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.lblClose = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cbDevices = new System.Windows.Forms.ComboBox();
            this.mainMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbDNSSettings = new System.Windows.Forms.ComboBox();
            this.manageDNSMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editDNSSettingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDNSSettingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.newDNSSettingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgLogo = new System.Windows.Forms.PictureBox();
            this.imgChangeDNS = new System.Windows.Forms.PictureBox();
            this.imgManageDNS = new System.Windows.Forms.PictureBox();
            this.imgMenu = new System.Windows.Forms.PictureBox();
            this.pnlDeviceDNS = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCPrimaryDNS = new System.Windows.Forms.Label();
            this.lblCSecondaryDNS = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblNPrimaryDNS = new System.Windows.Forms.Label();
            this.lblNSecondaryDNS = new System.Windows.Forms.Label();
            this.mainMenu.SuspendLayout();
            this.manageDNSMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgChangeDNS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgManageDNS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgMenu)).BeginInit();
            this.pnlDeviceDNS.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblClose
            // 
            this.lblClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClose.AutoSize = true;
            this.lblClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClose.ForeColor = System.Drawing.Color.DarkRed;
            this.lblClose.Location = new System.Drawing.Point(460, 8);
            this.lblClose.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(21, 20);
            this.lblClose.TabIndex = 7;
            this.lblClose.Text = "X";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Location = new System.Drawing.Point(13, 157);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(459, 23);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbDevices
            // 
            this.cbDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDevices.FormattingEnabled = true;
            this.cbDevices.Location = new System.Drawing.Point(44, 6);
            this.cbDevices.Margin = new System.Windows.Forms.Padding(4);
            this.cbDevices.Name = "cbDevices";
            this.cbDevices.Size = new System.Drawing.Size(388, 26);
            this.cbDevices.TabIndex = 5;
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.mainMenu.Name = "contextMenu";
            this.mainMenu.Size = new System.Drawing.Size(117, 54);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(113, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // cbDNSSettings
            // 
            this.cbDNSSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDNSSettings.DisplayMember = "Name";
            this.cbDNSSettings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDNSSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDNSSettings.FormattingEnabled = true;
            this.cbDNSSettings.Location = new System.Drawing.Point(44, 46);
            this.cbDNSSettings.Margin = new System.Windows.Forms.Padding(4);
            this.cbDNSSettings.Name = "cbDNSSettings";
            this.cbDNSSettings.Size = new System.Drawing.Size(388, 26);
            this.cbDNSSettings.TabIndex = 10;
            // 
            // manageDNSMenu
            // 
            this.manageDNSMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editDNSSettingMenuItem,
            this.deleteDNSSettingMenuItem,
            this.toolStripMenuItem2,
            this.newDNSSettingMenuItem});
            this.manageDNSMenu.Name = "manageDNSMenu";
            this.manageDNSMenu.Size = new System.Drawing.Size(211, 76);
            // 
            // editDNSSettingMenuItem
            // 
            this.editDNSSettingMenuItem.Name = "editDNSSettingMenuItem";
            this.editDNSSettingMenuItem.Size = new System.Drawing.Size(210, 22);
            this.editDNSSettingMenuItem.Text = "&Edit DNS Configuration";
            // 
            // deleteDNSSettingMenuItem
            // 
            this.deleteDNSSettingMenuItem.Name = "deleteDNSSettingMenuItem";
            this.deleteDNSSettingMenuItem.Size = new System.Drawing.Size(210, 22);
            this.deleteDNSSettingMenuItem.Text = "&Delete DNS Configuration";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(207, 6);
            // 
            // newDNSSettingMenuItem
            // 
            this.newDNSSettingMenuItem.Name = "newDNSSettingMenuItem";
            this.newDNSSettingMenuItem.Size = new System.Drawing.Size(210, 22);
            this.newDNSSettingMenuItem.Text = "&New DNS Configuration";
            // 
            // imgLogo
            // 
            this.imgLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.imgLogo.BackColor = System.Drawing.Color.Transparent;
            this.imgLogo.Image = global::theDiary.EasyDNS.Windows.Properties.Resources.Logo_1;
            this.imgLogo.Location = new System.Drawing.Point(293, 64);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Size = new System.Drawing.Size(190, 114);
            this.imgLogo.TabIndex = 16;
            this.imgLogo.TabStop = false;
            // 
            // imgChangeDNS
            // 
            this.imgChangeDNS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgChangeDNS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgChangeDNS.Image = global::theDiary.EasyDNS.Windows.Properties.Resources.Circled_Play_Green_24px;
            this.imgChangeDNS.Location = new System.Drawing.Point(440, 44);
            this.imgChangeDNS.Margin = new System.Windows.Forms.Padding(4);
            this.imgChangeDNS.Name = "imgChangeDNS";
            this.imgChangeDNS.Size = new System.Drawing.Size(32, 30);
            this.imgChangeDNS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imgChangeDNS.TabIndex = 15;
            this.imgChangeDNS.TabStop = false;
            // 
            // imgManageDNS
            // 
            this.imgManageDNS.Image = global::theDiary.EasyDNS.Windows.Properties.Resources.List_24px;
            this.imgManageDNS.Location = new System.Drawing.Point(7, 44);
            this.imgManageDNS.Margin = new System.Windows.Forms.Padding(4);
            this.imgManageDNS.Name = "imgManageDNS";
            this.imgManageDNS.Size = new System.Drawing.Size(32, 30);
            this.imgManageDNS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imgManageDNS.TabIndex = 11;
            this.imgManageDNS.TabStop = false;
            // 
            // imgMenu
            // 
            this.imgMenu.Image = global::theDiary.EasyDNS.Windows.Properties.Resources.Menu_24px;
            this.imgMenu.Location = new System.Drawing.Point(7, 6);
            this.imgMenu.Margin = new System.Windows.Forms.Padding(4);
            this.imgMenu.Name = "imgMenu";
            this.imgMenu.Size = new System.Drawing.Size(32, 30);
            this.imgMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imgMenu.TabIndex = 8;
            this.imgMenu.TabStop = false;
            // 
            // pnlDeviceDNS
            // 
            this.pnlDeviceDNS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDeviceDNS.ColumnCount = 2;
            this.pnlDeviceDNS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlDeviceDNS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlDeviceDNS.Controls.Add(this.label1, 0, 0);
            this.pnlDeviceDNS.Controls.Add(this.lblCPrimaryDNS, 0, 1);
            this.pnlDeviceDNS.Controls.Add(this.lblCSecondaryDNS, 0, 2);
            this.pnlDeviceDNS.Controls.Add(this.label4, 1, 0);
            this.pnlDeviceDNS.Controls.Add(this.lblNPrimaryDNS, 1, 1);
            this.pnlDeviceDNS.Controls.Add(this.lblNSecondaryDNS, 1, 2);
            this.pnlDeviceDNS.Location = new System.Drawing.Point(44, 85);
            this.pnlDeviceDNS.Name = "pnlDeviceDNS";
            this.pnlDeviceDNS.RowCount = 3;
            this.pnlDeviceDNS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlDeviceDNS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlDeviceDNS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlDeviceDNS.Size = new System.Drawing.Size(229, 64);
            this.pnlDeviceDNS.TabIndex = 17;
            this.pnlDeviceDNS.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current DNS";
            // 
            // lblCPrimaryDNS
            // 
            this.lblCPrimaryDNS.AutoSize = true;
            this.lblCPrimaryDNS.Location = new System.Drawing.Point(3, 22);
            this.lblCPrimaryDNS.Name = "lblCPrimaryDNS";
            this.lblCPrimaryDNS.Size = new System.Drawing.Size(35, 13);
            this.lblCPrimaryDNS.TabIndex = 1;
            this.lblCPrimaryDNS.Text = "label2";
            // 
            // lblCSecondaryDNS
            // 
            this.lblCSecondaryDNS.AutoSize = true;
            this.lblCSecondaryDNS.Location = new System.Drawing.Point(3, 44);
            this.lblCSecondaryDNS.Name = "lblCSecondaryDNS";
            this.lblCSecondaryDNS.Size = new System.Drawing.Size(35, 13);
            this.lblCSecondaryDNS.TabIndex = 2;
            this.lblCSecondaryDNS.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(87, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "New DNS";
            // 
            // lblNPrimaryDNS
            // 
            this.lblNPrimaryDNS.AutoSize = true;
            this.lblNPrimaryDNS.Location = new System.Drawing.Point(87, 22);
            this.lblNPrimaryDNS.Name = "lblNPrimaryDNS";
            this.lblNPrimaryDNS.Size = new System.Drawing.Size(35, 13);
            this.lblNPrimaryDNS.TabIndex = 4;
            this.lblNPrimaryDNS.Text = "label5";
            // 
            // lblNSecondaryDNS
            // 
            this.lblNSecondaryDNS.AutoSize = true;
            this.lblNSecondaryDNS.Location = new System.Drawing.Point(87, 44);
            this.lblNSecondaryDNS.Name = "lblNSecondaryDNS";
            this.lblNSecondaryDNS.Size = new System.Drawing.Size(35, 13);
            this.lblNSecondaryDNS.TabIndex = 5;
            this.lblNSecondaryDNS.Text = "label6";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(490, 185);
            this.Controls.Add(this.pnlDeviceDNS);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.imgChangeDNS);
            this.Controls.Add(this.imgManageDNS);
            this.Controls.Add(this.cbDNSSettings);
            this.Controls.Add(this.imgMenu);
            this.Controls.Add(this.lblClose);
            this.Controls.Add(this.cbDevices);
            this.Controls.Add(this.imgLogo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(490, 185);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EasyDNS";
            this.mainMenu.ResumeLayout(false);
            this.manageDNSMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgChangeDNS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgManageDNS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgMenu)).EndInit();
            this.pnlDeviceDNS.ResumeLayout(false);
            this.pnlDeviceDNS.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cbDevices;
        private System.Windows.Forms.PictureBox imgMenu;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbDNSSettings;
        private System.Windows.Forms.PictureBox imgManageDNS;
        private System.Windows.Forms.ContextMenuStrip mainMenu;
        private System.Windows.Forms.ContextMenuStrip manageDNSMenu;
        private System.Windows.Forms.ToolStripMenuItem editDNSSettingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteDNSSettingMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem newDNSSettingMenuItem;
        private System.Windows.Forms.PictureBox imgChangeDNS;
        private System.Windows.Forms.PictureBox imgLogo;
        private System.Windows.Forms.TableLayoutPanel pnlDeviceDNS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCPrimaryDNS;
        private System.Windows.Forms.Label lblCSecondaryDNS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNPrimaryDNS;
        private System.Windows.Forms.Label lblNSecondaryDNS;
    }
}

