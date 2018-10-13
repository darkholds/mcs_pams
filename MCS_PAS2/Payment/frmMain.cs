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
    public partial class frmMain : Form
    {
        private User loginUser;
        private bool menuStatus;
        public Student PayeeStudent { get; set; }

        public frmMain()
        { 
            InitializeComponent();           
        }

        public User LoginUser
        {
            set
            {
                loginUser = value;
                tsddbUser.Text = LoginUser.FirstName + " " + LoginUser.LastName;                
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
                scMain.Enabled = menuStatus;
                tsMain.Enabled = menuStatus;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                txtBal.Text = 0.ToString("N2");
                dgvBalance.DataSource = null;
                dgvBalance.Rows.Clear();

                Student s = new Student();
                if (!txtSN.Text.Trim().Equals(""))
                {
                    dgvStudent.DataSource = s.SearchStudentsById(txtSN.Text.Trim());
                }
                else if (!txtName.Text.Trim().Equals(""))
                {
                    dgvStudent.DataSource = s.SearchStudentsByLastName(txtName.Text.Trim());
                }

                dgvStudent.Columns[0].Width = 100;
                dgvStudent.Columns[1].Width = 200;
                dgvStudent.Columns[2].Width = 200;
                dgvStudent.Columns[3].Width = 200;
                Cursor.Current = Cursors.Default;

                if (dgvStudent.Rows.Count > 0)
                {
                    scMain.Panel1Collapsed = false;
                }
                else
                {
                    throw new Exception("No record(s) found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch.PerformClick();
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch.PerformClick();
            }
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (dgvStudent.SelectedRows.Count > 0)
                {
                    PayeeStudent = new Student();
                    PayeeStudent.Id = dgvStudent.SelectedRows[0].Cells["SN"].Value.ToString();
                    PayeeStudent.FirstName = dgvStudent.SelectedRows[0].Cells["FIRST NAME"].Value.ToString();
                    PayeeStudent.LastName = dgvStudent.SelectedRows[0].Cells["LAST NAME"].Value.ToString();
                    PayeeStudent.MiddleName = dgvStudent.SelectedRows[0].Cells["MIDDLE NAME"].Value.ToString();
                    PayeeStudent.Course = dgvStudent.SelectedRows[0].Cells["COURSE"].Value.ToString();

                    txtSN.Text = PayeeStudent.Id;
                    txtName.Text = PayeeStudent.LastName + ", " + PayeeStudent.FirstName + " " + PayeeStudent.MiddleName;
                    txtCourse.Text = PayeeStudent.Course;
                    scMain.Panel1Collapsed = true;
                    txtSN.ReadOnly = true;
                    txtName.ReadOnly = true;
                    tsbPayment.Enabled = true;

                    tsbRefresh.PerformClick();
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            MenuStatus = false;
            scMain.Enabled = false;
            txtBal.Text = 0.ToString("N2");
            scMain.Panel1Collapsed = true;
            tsbPayment.Enabled = false;
            frmLogin frm = new frmLogin();
            frm.Show(this);
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            txtBal.Text = 0.ToString("N2");
            dgvBalance.DataSource = null;
            dgvBalance.Rows.Clear();
            dgvPayments.DataSource = null;
            dgvPayments.Rows.Clear();
            scMain.Panel1Collapsed = true;
            txtName.Text = string.Empty;
            txtSN.Text = string.Empty;
            txtCourse.Text = string.Empty;
            txtSN.ReadOnly = false;
            txtName.ReadOnly = false;
            tsbPayment.Enabled = false;
            PayeeStudent = null;
        }

        private void tsbPayment_Click(object sender, EventArgs e)
        {           
            new frmPayment().ShowDialog(this);
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                tsbClear.PerformClick();
                User user = new User();
                user.UserLogout(LoginUser.Username, "PAYMENT");
                MenuStatus = false;
                loginUser = null;
                tsddbUser.Text = "Not Logged";
                frmLogin frm = new frmLogin();
                frm.Show(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }         
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (loginUser != null)
                {
                    User user = new User();
                    user.UserLogout(LoginUser.Username, "PAYMENT");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }       
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (txtSN.Text.Trim() != string.Empty)
                {
                    dgvBalance.DataSource = new Assessment().GetBalance(txtSN.Text.Trim());
                    dgvBalance.Columns[0].Width = 200;
                    dgvBalance.Columns[1].Width = 500;
                    dgvBalance.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvBalance.Columns[2].DefaultCellStyle.Format = String.Format("N2");

                    if (dgvBalance.Rows.Count == 0)
                    {
                        txtBal.Text = 0.ToString("N2");
                        MessageBox.Show("No balance found.", "Balance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        double bal = 0;

                        foreach (DataGridViewRow row in dgvBalance.Rows)
                        {
                            bal += Convert.ToDouble(row.Cells["BALANCE"].Value);
                        }

                        txtBal.Text = bal.ToString("C2");
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvBalance_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvBalance.SelectedRows.Count > 0)
                {
                    dgvPayments.DataSource = new Payment().GetAllPayment(txtSN.Text.Trim(), Convert.ToInt32(dgvBalance.SelectedRows[0].Cells["ASSESSMENT No."].Value));
                    dgvPayments.Columns[0].Width = 150;
                    dgvPayments.Columns[1].Width = 150;
                    dgvPayments.Columns[2].Width = 250;
                    dgvPayments.Columns[3].Width = 250;
                    dgvPayments.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvPayments.Columns[1].DefaultCellStyle.Format = String.Format("N2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                
        }
    }
}
