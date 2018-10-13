namespace AccountingMgt
{
    partial class frmMDI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMDI));
            this.tsMDI = new System.Windows.Forms.ToolStrip();
            this.tsddbUser = new System.Windows.Forms.ToolStripDropDownButton();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tslDate = new System.Windows.Forms.ToolStripLabel();
            this.tslTime = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPayment = new System.Windows.Forms.ToolStripDropDownButton();
            this.paymentOfAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherPaymentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managementOfPaymentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAssessment = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFees = new System.Windows.Forms.ToolStripDropDownButton();
            this.listOfFeeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gradeLevelFeesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAccount = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSummary = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tssbDisburse = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tmrDateTime = new System.Windows.Forms.Timer(this.components);
            this.tsMDI.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMDI
            // 
            this.tsMDI.BackColor = System.Drawing.Color.White;
            this.tsMDI.Dock = System.Windows.Forms.DockStyle.Left;
            this.tsMDI.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMDI.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddbUser,
            this.toolStripSeparator2,
            this.tslDate,
            this.tslTime,
            this.toolStripSeparator9,
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.tsbPayment,
            this.toolStripSeparator3,
            this.tsbAssessment,
            this.toolStripSeparator5,
            this.tsbFees,
            this.toolStripSeparator4,
            this.tsbAccount,
            this.toolStripSeparator6,
            this.tsbSummary,
            this.toolStripSeparator7,
            this.tssbDisburse,
            this.toolStripSeparator10});
            this.tsMDI.Location = new System.Drawing.Point(0, 0);
            this.tsMDI.Name = "tsMDI";
            this.tsMDI.Size = new System.Drawing.Size(194, 739);
            this.tsMDI.TabIndex = 4;
            this.tsMDI.Text = "toolStrip1";
            // 
            // tsddbUser
            // 
            this.tsddbUser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logoutToolStripMenuItem,
            this.toolStripSeparator8,
            this.changePasswordToolStripMenuItem});
            this.tsddbUser.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsddbUser.ForeColor = System.Drawing.Color.SeaGreen;
            this.tsddbUser.Image = global::AccountingMgt.Properties.Resources.Users_icon;
            this.tsddbUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbUser.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsddbUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbUser.Name = "tsddbUser";
            this.tsddbUser.Size = new System.Drawing.Size(191, 52);
            this.tsddbUser.Text = "Not Logged";
            this.tsddbUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Image = global::AccountingMgt.Properties.Resources.logout_icon;
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(177, 6);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Image = global::AccountingMgt.Properties.Resources.Key;
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(191, 6);
            // 
            // tslDate
            // 
            this.tslDate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslDate.ForeColor = System.Drawing.Color.Red;
            this.tslDate.Name = "tslDate";
            this.tslDate.Size = new System.Drawing.Size(191, 21);
            this.tslDate.Text = "DATE";
            // 
            // tslTime
            // 
            this.tslTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslTime.ForeColor = System.Drawing.Color.DarkBlue;
            this.tslTime.Name = "tslTime";
            this.tslTime.Size = new System.Drawing.Size(191, 21);
            this.tslTime.Text = "TIME";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(191, 6);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Image = global::AccountingMgt.Properties.Resources.mcs_logo;
            this.toolStripLabel1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(191, 179);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(191, 6);
            // 
            // tsbPayment
            // 
            this.tsbPayment.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.paymentOfAccountsToolStripMenuItem,
            this.otherPaymentsToolStripMenuItem,
            this.managementOfPaymentsToolStripMenuItem});
            this.tsbPayment.Image = ((System.Drawing.Image)(resources.GetObject("tsbPayment.Image")));
            this.tsbPayment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbPayment.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPayment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPayment.Name = "tsbPayment";
            this.tsbPayment.Size = new System.Drawing.Size(191, 52);
            this.tsbPayment.Text = "&Payments";
            this.tsbPayment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // paymentOfAccountsToolStripMenuItem
            // 
            this.paymentOfAccountsToolStripMenuItem.Image = global::AccountingMgt.Properties.Resources.payment;
            this.paymentOfAccountsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.paymentOfAccountsToolStripMenuItem.Name = "paymentOfAccountsToolStripMenuItem";
            this.paymentOfAccountsToolStripMenuItem.Size = new System.Drawing.Size(246, 54);
            this.paymentOfAccountsToolStripMenuItem.Text = "Payment of Accounts";
            this.paymentOfAccountsToolStripMenuItem.Click += new System.EventHandler(this.paymentOfAccountsToolStripMenuItem_Click);
            // 
            // otherPaymentsToolStripMenuItem
            // 
            this.otherPaymentsToolStripMenuItem.Image = global::AccountingMgt.Properties.Resources.payment;
            this.otherPaymentsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.otherPaymentsToolStripMenuItem.Name = "otherPaymentsToolStripMenuItem";
            this.otherPaymentsToolStripMenuItem.Size = new System.Drawing.Size(246, 54);
            this.otherPaymentsToolStripMenuItem.Text = "Other Payments";
            this.otherPaymentsToolStripMenuItem.Click += new System.EventHandler(this.otherPaymentsToolStripMenuItem_Click);
            // 
            // managementOfPaymentsToolStripMenuItem
            // 
            this.managementOfPaymentsToolStripMenuItem.Image = global::AccountingMgt.Properties.Resources.summary;
            this.managementOfPaymentsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.managementOfPaymentsToolStripMenuItem.Name = "managementOfPaymentsToolStripMenuItem";
            this.managementOfPaymentsToolStripMenuItem.Size = new System.Drawing.Size(246, 54);
            this.managementOfPaymentsToolStripMenuItem.Text = "Management of Payments";
            this.managementOfPaymentsToolStripMenuItem.Click += new System.EventHandler(this.managementOfPaymentsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(191, 6);
            // 
            // tsbAssessment
            // 
            this.tsbAssessment.Image = global::AccountingMgt.Properties.Resources.user_student_assistant;
            this.tsbAssessment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbAssessment.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbAssessment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAssessment.Name = "tsbAssessment";
            this.tsbAssessment.Size = new System.Drawing.Size(191, 52);
            this.tsbAssessment.Text = "&Assessment of Fees";
            this.tsbAssessment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbAssessment.Click += new System.EventHandler(this.tsbAssessment_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(191, 6);
            // 
            // tsbFees
            // 
            this.tsbFees.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listOfFeeToolStripMenuItem,
            this.gradeLevelFeesToolStripMenuItem});
            this.tsbFees.Image = global::AccountingMgt.Properties.Resources.feemanage;
            this.tsbFees.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbFees.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbFees.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFees.Name = "tsbFees";
            this.tsbFees.Size = new System.Drawing.Size(191, 52);
            this.tsbFees.Text = "&Management of Fees";
            this.tsbFees.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listOfFeeToolStripMenuItem
            // 
            this.listOfFeeToolStripMenuItem.Image = global::AccountingMgt.Properties.Resources.listoffee;
            this.listOfFeeToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.listOfFeeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.listOfFeeToolStripMenuItem.Name = "listOfFeeToolStripMenuItem";
            this.listOfFeeToolStripMenuItem.Size = new System.Drawing.Size(193, 54);
            this.listOfFeeToolStripMenuItem.Text = "&List of Fee";
            this.listOfFeeToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.listOfFeeToolStripMenuItem.ToolTipText = "Manage List of Fee";
            this.listOfFeeToolStripMenuItem.Click += new System.EventHandler(this.listOfFeeToolStripMenuItem_Click);
            // 
            // gradeLevelFeesToolStripMenuItem
            // 
            this.gradeLevelFeesToolStripMenuItem.Image = global::AccountingMgt.Properties.Resources.list_all_participants;
            this.gradeLevelFeesToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.gradeLevelFeesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.gradeLevelFeesToolStripMenuItem.Name = "gradeLevelFeesToolStripMenuItem";
            this.gradeLevelFeesToolStripMenuItem.Size = new System.Drawing.Size(193, 54);
            this.gradeLevelFeesToolStripMenuItem.Text = "&Grade Level Fees";
            this.gradeLevelFeesToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.gradeLevelFeesToolStripMenuItem.ToolTipText = "Assign fees to each grade level";
            this.gradeLevelFeesToolStripMenuItem.Click += new System.EventHandler(this.gradeLevelFeesToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.ForeColor = System.Drawing.Color.SaddleBrown;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(191, 6);
            // 
            // tsbAccount
            // 
            this.tsbAccount.Image = global::AccountingMgt.Properties.Resources.open;
            this.tsbAccount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbAccount.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbAccount.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAccount.Name = "tsbAccount";
            this.tsbAccount.Size = new System.Drawing.Size(191, 52);
            this.tsbAccount.Text = "Account of &Students";
            this.tsbAccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbAccount.Click += new System.EventHandler(this.tsbAccount_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(191, 6);
            // 
            // tsbSummary
            // 
            this.tsbSummary.Image = global::AccountingMgt.Properties.Resources.summarycollection;
            this.tsbSummary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbSummary.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSummary.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSummary.Name = "tsbSummary";
            this.tsbSummary.Size = new System.Drawing.Size(191, 52);
            this.tsbSummary.Text = "Summary of &Collections";
            this.tsbSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbSummary.Click += new System.EventHandler(this.tsbSummary_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(191, 6);
            // 
            // tssbDisburse
            // 
            this.tssbDisburse.Image = global::AccountingMgt.Properties.Resources.disbursement;
            this.tssbDisburse.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tssbDisburse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssbDisburse.Name = "tssbDisburse";
            this.tssbDisburse.Size = new System.Drawing.Size(191, 52);
            this.tssbDisburse.Text = "Disbursement of Funds";
            this.tssbDisburse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tssbDisburse.Click += new System.EventHandler(this.tssbDisburse_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(191, 6);
            // 
            // tmrDateTime
            // 
            this.tmrDateTime.Enabled = true;
            this.tmrDateTime.Interval = 1000;
            this.tmrDateTime.Tick += new System.EventHandler(this.tmrDateTime_Tick);
            // 
            // frmMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::AccountingMgt.Properties.Resources.mcs_mdi_back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(828, 739);
            this.Controls.Add(this.tsMDI);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frmMDI";
            this.Text = "Mater Carmeli School - Payment, Assessment, and Account Monitoring";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMDI_FormClosing);
            this.Load += new System.EventHandler(this.frmMDI_Load);
            this.tsMDI.ResumeLayout(false);
            this.tsMDI.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMDI;
        private System.Windows.Forms.ToolStripButton tsbAssessment;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton tsddbUser;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbSummary;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbAccount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripDropDownButton tsbFees;
        private System.Windows.Forms.ToolStripMenuItem listOfFeeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gradeLevelFeesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tssbDisburse;
        private System.Windows.Forms.ToolStripLabel tslDate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripLabel tslTime;
        private System.Windows.Forms.Timer tmrDateTime;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripDropDownButton tsbPayment;
        private System.Windows.Forms.ToolStripMenuItem paymentOfAccountsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otherPaymentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem managementOfPaymentsToolStripMenuItem;
    }
}

