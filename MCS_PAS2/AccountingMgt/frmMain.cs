using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace AccountingMgt
{
    public partial class frmMain : Form
    {
        private Student Payer; 

        public frmMain()
        { 
            InitializeComponent();           
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
                dgvStudent.Columns[4].Width = 110;
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
            catch (MySqlException ex)
            {
                if (ex.Number == 1042)
                    MessageBox.Show("Database server is offline. Contact administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(ex.Number + ": " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (dgvStudent.SelectedRows.Count > 0)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Payer = new Student();
                    Payer.Id= dgvStudent.SelectedRows[0].Cells["STUDENT NUMBER"].Value.ToString();
                    Payer.FirstName = dgvStudent.SelectedRows[0].Cells["FIRST NAME"].Value.ToString();
                    Payer.LastName = dgvStudent.SelectedRows[0].Cells["LAST NAME"].Value.ToString();
                    Payer.MiddleName = dgvStudent.SelectedRows[0].Cells["MIDDLE NAME"].Value.ToString();
                    Payer.CurrentYearLevel = dgvStudent.SelectedRows[0].Cells["GRADE LEVEL"].Value.ToString();

                    txtSN.Text = Payer.Id;
                    txtName.Text = Payer.LastName + ", " + Payer.FirstName + " " + Payer.MiddleName;
                    lblGrade.Text = Payer.CurrentYearLevel;
                    scMain.Panel1Collapsed = true;
                    txtSN.ReadOnly = true;
                    txtName.ReadOnly = true;
                    tsbRefresh.Enabled = true;
                    Cursor.Current = Cursors.Default;

                    tsbRefresh.PerformClick();
                }          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtBal.Text = 0.ToString("N2");
            scMain.Panel1Collapsed = true;
            tsbPayment.Enabled = false;
            tsbRefresh.Enabled = false;
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
            lblGrade.Text = string.Empty;
            txtSN.ReadOnly = false;
            txtName.ReadOnly = false;
            tsbPayment.Enabled = false;
            tsbRefresh.Enabled = false;
            Payer = null;
        }

        private void tsbPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBalance.SelectedRows.Count > 0 && dgvBalance.Rows.Count > 0)
                {
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f.GetType() == typeof(frmPayment))
                        {
                            f.Activate();
                            return;
                        }
                    }

                    frmPayment payment = new frmPayment(Payer, Convert.ToInt32(dgvBalance.SelectedRows[0].Cells["ASSESSMENT No."].Value.ToString()));
                    payment.MdiParent = MdiParent;
                    payment.Show();
                }
                else
                {
                    throw new Exception("No balance is selected.");
                }
            }
            catch(Exception ex)
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

                    try
                    {
                        dgvBalance.DataSource = new Assessment().GetActiveAssessment(txtSN.Text.Trim());
                    }
                    catch (Exception)
                    {
                        throw new Exception("Student may not have active registration");
                    }
                    dgvBalance.Columns[0].Width = 100;
                    dgvBalance.Columns[1].Width = 410;
                    dgvBalance.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvBalance.Columns[2].DefaultCellStyle.Format = String.Format("N2");
                    dgvBalance.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvBalance.Columns[3].DefaultCellStyle.Format = String.Format("N2");
                    dgvBalance.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvBalance.Columns[4].DefaultCellStyle.Format = String.Format("N2");

                    if (dgvBalance.Rows.Count == 0)
                    {
                        txtBal.Text = 0.ToString("N2");
                        tsbPayment.Enabled = false;
                        MessageBox.Show("No balance found.", "Balance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        double bal = 0;

                        foreach (DataGridViewRow row in dgvBalance.Rows)
                        {
                            bal += Convert.ToDouble(row.Cells["BALANCE"].Value);
                        }

                        dgvBalance.Rows[0].Selected = true;
                        txtBal.Text = bal.ToString("N2");
                        tsbPayment.Enabled = true;
                    }
                }

                if (dgvBalance.SelectedRows.Count > 0)
                {
                    foreach(DataGridViewRow dRow in dgvPayments.Rows)
                    {
                        DataTable table = new Payment().GetPaymentDetailByORNumber(dRow.Cells["OR NUMBER"].Value.ToString());
                        string payments = string.Empty;
                        foreach (DataRow row in table.Rows)
                        {
                            if (payments == string.Empty)
                                payments += row.Field<string>("FEE").ToString() + " (" + row.Field<double>("AMOUNT").ToString("N2") + ")";
                            else
                                payments += Environment.NewLine + row.Field<string>("FEE").ToString() + " (" + row.Field<double>("AMOUNT").ToString("N2") + ")";
                        }

                        dRow.Cells[0].ToolTipText = payments;
                        dRow.Cells[1].ToolTipText = payments;
                        dRow.Cells[2].ToolTipText = payments;
                        dRow.Cells[3].ToolTipText = payments;
                    }                              
                }

                Cursor.Current = Cursors.Default;
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvBalance_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (dgvBalance.SelectedRows.Count > 0)
                {
                    dgvPayments.DataSource = new Payment().GetAllPaymentPerAssessment(txtSN.Text.Trim(), Convert.ToInt32(dgvBalance.SelectedRows[0].Cells["ASSESSMENT No."].Value));
                    dgvPayments.Columns[0].Width = 160;
                    dgvPayments.Columns[1].Width = 150;
                    dgvPayments.Columns[2].Width = 250;
                    dgvPayments.Columns[3].Width = 250;
                    dgvPayments.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvPayments.Columns[1].DefaultCellStyle.Format = String.Format("N2");
                }
                Cursor.Current = Cursors.Default;
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPayments_SelectionChanged(object sender, EventArgs e)
        {
            dgvPayments.ClearSelection();
        }
    }
}
