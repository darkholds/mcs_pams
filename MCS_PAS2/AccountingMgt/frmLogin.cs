using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace AccountingMgt
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                User user = new User();
               
                if (!user.LoginUser(txtUN.Text.Trim(), txtPW.Text.Trim()))
                {
                    Cursor.Current = Cursors.Default;
                    throw new Exception("Invalid login credentials.");
                }                 
                else
                {
                    if (user.Role.Equals(UserTypes.Admin.ToString()) || user.Role.Equals(UserTypes.Accountant.ToString()) || user.Role.Equals(UserTypes.Cashier.ToString()) || user.Role.Equals(UserTypes.Treasurer.ToString()))
                    {
                        (MdiParent as frmMDI).LoginUser = user;
                        (MdiParent as frmMDI).MenuStatus = true;
                        user.UserLogin(user.Username, "ACCTNG");
                        Cursor.Current = Cursors.Default;
                        Close();
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        throw new Exception("User has no permission");
                    }                       
                }
            }
            catch(MySqlException ex)
            {
                if(ex.Number==1042)
                    MessageBox.Show("Database server is offline. Contact administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(ex.Number + ": " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPW_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnLogin.PerformClick();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
