using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payment
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
                    if (user.Role.Equals(UserTypes.Cashier.ToString()))
                    {
                        ((frmMain)Owner).LoginUser = user;
                        ((frmMain)Owner).MenuStatus = true;
                        user.UserLogin(user.Username, "PAYMENT");
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
