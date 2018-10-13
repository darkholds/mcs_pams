using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountingMgt
{
    public partial class frmMDI : Form
    {
        private User loginUser;
        private bool menuStatus;

        public frmMDI()
        {
            InitializeComponent();
            loginUser = new User();
        }

        public User LoginUser
        {
            set
            {
                loginUser = value;
                tsddbUser.Text = LoginUser.Name + "\n[" + LoginUser.Role + "]";
            }
            get
            {
                return loginUser;
            }
        }

        public bool MenuStatus
        {
            set
            {
                menuStatus = value;
                tsMDI.Enabled = menuStatus;
                if (menuStatus)
                {
                    if (loginUser.Role.Equals(UserTypes.Cashier.ToString()) || loginUser.Role.Equals(UserTypes.Admin.ToString()))
                        tsbPayment.Enabled = true;
                    else
                        tsbPayment.Enabled = false;

                    if (loginUser.Role.Equals(UserTypes.Cashier.ToString()) || loginUser.Role.Equals(UserTypes.Accountant.ToString()) || loginUser.Role.Equals(UserTypes.Admin.ToString()))
                    {
                        tsbAssessment.Enabled = true;
                        tsbFees.Enabled = true;
                    }
                    else
                    {
                        tsbAssessment.Enabled = false;
                        tsbFees.Enabled = false;
                    }                      
                }
            }
        } 

        private void frmMDI_Load(object sender, EventArgs e)
        {
            MenuStatus = false;
            frmLogin newMDIChild = new frmLogin();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form f in MdiChildren)  //close all forms
                {
                    f.Close();
                }

                User user = new User();
                user.UserLogout(LoginUser.Username, "ACCTNG");
                MenuStatus = false;
                loginUser = null;
                tsddbUser.Text = "Not Logged";
                frmLogin newMDIChild = new frmLogin();
                newMDIChild.MdiParent = this;
                newMDIChild.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (loginUser != null)
                {
                    User user = new User();
                    user.UserLogout(LoginUser.Username, "ACCTNG");
                }
            }
            catch { }
        }

        private void tsbSummary_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(frmSummary))
                {
                    f.Activate();
                    return;
                }
            }
            frmSummary newMDIChild = new frmSummary();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void tsbAssessment_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(frmAssessment))
                {
                    f.Activate();
                    return;
                }
            }
            frmAssessment newMDIChild = new frmAssessment();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void listOfFeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(frmFee))
                {
                    f.Activate();
                    return;
                }
            }
            frmFee newMDIChild = new frmFee();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void gradeLevelFeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(frmFees))
                {
                    f.Activate();
                    return;
                }
            }
            frmFees newMDIChild = new frmFees();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void tsbAccount_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(frmAccounts))
                {
                    f.Activate();
                    return;
                }
            }
            frmAccounts newMDIChild = new frmAccounts();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(frmChangePW))
                {
                    f.Activate();
                    return;
                }
            }
            frmChangePW newMDIChild = new frmChangePW();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void tssbDisburse_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(frmDisbursement))
                {
                    f.Activate();
                    return;
                }
            }
            frmDisbursement newMDIChild = new frmDisbursement();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void tmrDateTime_Tick(object sender, EventArgs e)
        {
            tslDate.Text = DateTime.Now.ToLongDateString();
            tslTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void paymentOfAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(frmMain))
                {
                    f.Activate();
                    return;
                }
            }
            frmMain newMDIChild = new frmMain();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void otherPaymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(frmOtherPayment))
                {
                    f.Activate();
                    return;
                }
            }
            frmOtherPayment newMDIChild = new frmOtherPayment();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void managementOfPaymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(frmPaymentMgt))
                {
                    f.Activate();
                    return;
                }
            }
            frmPaymentMgt newMDIChild = new frmPaymentMgt();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }
    }
}
