using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace AccountingMgt
{
    public partial class frmChangePW : Form
    {
        public frmChangePW()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text.Trim().Equals("") || !txtPassword.Text.Trim().Equals((MdiParent as frmMDI).LoginUser.Password))
                    throw new Exception("Invalid password");
                if (txtNewPassword1.Text.Trim().Equals("") || txtNewPassword2.Text.Trim().Equals(""))
                    throw new Exception("New password not valid");
                if(!txtNewPassword1.Text.Trim().Equals(txtNewPassword2.Text.Trim()))
                    throw new Exception("New password does not match");

                if (new User().ChangePassword((MdiParent as frmMDI).LoginUser.Username, txtNewPassword1.Text.Trim()))
                {
                    (MdiParent as frmMDI).LoginUser.Password = txtNewPassword1.Text.Trim();
                    btnClear.PerformClick();
                    throw new Exception("Password changed successfully.");
                }
                else
                    throw new Exception("Password change fail, server maybe offline.");
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1042)
                    MessageBox.Show("Database server is offline. Contact administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(ex.Number + ": " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void frmChangePW_Load(object sender, EventArgs e)
        {
            txtUsername.Text = (MdiParent as frmMDI).LoginUser.Username;
            txtPassword.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPassword.Text = string.Empty;
            txtNewPassword1.Text = string.Empty;
            txtNewPassword2.Text = string.Empty;
        }
    }
}
