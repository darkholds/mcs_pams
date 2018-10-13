using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AccountingMgt
{
    public partial class frmAccounts : Form
    {
        private Student student;

        public frmAccounts()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
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
                    scReports.Panel1Collapsed = false;
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

        private void dgvStudent_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (dgvStudent.SelectedRows.Count > 0)
                {
                    student = new Student();
                    student.Id= dgvStudent.SelectedRows[0].Cells["STUDENT NUMBER"].Value.ToString();
                    student.FirstName = dgvStudent.SelectedRows[0].Cells["FIRST NAME"].Value.ToString();
                    student.LastName = dgvStudent.SelectedRows[0].Cells["LAST NAME"].Value.ToString();
                    student.MiddleName = dgvStudent.SelectedRows[0].Cells["MIDDLE NAME"].Value.ToString();
                    student.CurrentYearLevel = dgvStudent.SelectedRows[0].Cells["GRADE LEVEL"].Value.ToString();

                    txtSN.Text = student.Id;
                    txtName.Text = student.LastName + ", " + student.FirstName + " " + student.MiddleName;
                    lblGrade.Text = student.CurrentYearLevel;

                    scReports.Panel1Collapsed = true;
                    txtSN.ReadOnly = true;
                    txtName.ReadOnly = true;                  
                    tsbCloseAccount.Enabled = true;
                    tsbOpenAccount.Enabled = true;
                    tsbRefresh.Enabled = true;
                    tsbPrintSoA.Enabled = true;
                    tsbPrintLedger.Enabled = true;
                    tsbRefresh.PerformClick();                  
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            scReports.Panel1Collapsed = true;
            txtSN.Text = string.Empty;
            txtName.Text = string.Empty;
            lblGrade.Text = string.Empty;
            txtSN.ReadOnly = false;
            txtName.ReadOnly = false;
            tsbCloseAccount.Enabled = false;
            tsbRefresh.Enabled = false;
            tsbPrintSoA.Enabled = false;
            tsbOpenAccount.Enabled = false;
            tsbPrintLedger.Enabled = false;

            dgvStudent.DataSource = null;
            dgvStudent.Rows.Clear();
            dgvAccounts.DataSource = null;
            dgvAccounts.Rows.Clear();
            dgvAccountDetail.DataSource = null;
            dgvAccountDetail.Rows.Clear();
            dgvPayDetails.DataSource = null;
            dgvPayDetails.Rows.Clear();
            dgvAssessDetails.DataSource = null;
            dgvAssessDetails.Rows.Clear();
            student = null;
            Cursor.Current = Cursors.Default;
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbPrintSoA_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSN.Text.Trim().Equals("") && txtName.Text.Trim().Equals(""))
                    throw new Exception("Nothing to print");

                if (dgvAssessDetails.SelectedRows.Count == 0)
                    throw new Exception("Please select fees below");

                //add fees in soa list
                List<PaymentDetail> SoAItems = new List<PaymentDetail>();
                double totalsoa = 0;
                foreach (DataGridViewRow row in dgvAssessDetails.SelectedRows)
                {
                    PaymentDetail pd = new PaymentDetail();
                    pd.Code = row.Cells["CODE"].Value.ToString();
                    pd.Name = row.Cells["FEE"].Value.ToString();
                    double amount = 0;
                    bool isamount = Double.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Enter amount for " + pd.Name + "\n\nTOTAL: " + totalsoa.ToString("N2"), pd.Name, "0"),out amount);
                    if (isamount)
                    {
                        pd.Amount = amount;
                        totalsoa += amount;
                        SoAItems.Add(pd);
                    }
                    else
                    {
                        return;
                    }                     
                }

                frmReport newMDIChild = new frmReport(ReportTypes.SOA);
                newMDIChild.SoAItems = SoAItems;
                newMDIChild.SoAStudent = student;
                newMDIChild.MdiParent = ParentForm;
                newMDIChild.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmAccounts_Load(object sender, EventArgs e)
        {
            scReports.Panel1Collapsed = true;
            tsbCloseAccount.Enabled = false;
            tsbRefresh.Enabled = false;
            tsbPrintSoA.Enabled = false;
            tsbPrintLedger.Enabled = false;

            if((MdiParent as frmMDI).LoginUser.Role.Equals(UserTypes.Admin.ToString()))
            {
                tsbOpenAccount.Enabled = false;
                tsbOpenAccount.Visible = true;
            }
            else
            {
                tsbOpenAccount.Enabled = false;
                tsbOpenAccount.Visible = false;
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch.PerformClick();
            }
        }

        private void dgvAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (dgvAccounts.Rows.Count > 0)
                {
                    Account account = new Account();
                    account.GetAccountByRegistrationId(Convert.ToInt64(dgvAccounts.SelectedRows[0].Cells["REG. No."].Value.ToString()));
                    dgvAccountDetail.DataSource = account.StudentPayment;
                    dgvAccountDetail.Columns[0].Width = 100;
                    dgvAccountDetail.Columns[1].Width = 75;
                    dgvAccountDetail.Columns[2].Width = 75;
                    dgvAccountDetail.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvAccountDetail.Columns[1].DefaultCellStyle.Format = string.Format("N2");
                    dgvAccountDetail.Columns[3].Visible = false;

                    dgvAssessDetails.DataSource = new AssessmentDetail().GetBalancePerAssessmentItem(account.StudentAssessment.Id, 4);
                    dgvAssessDetails.Columns[0].Width = 75;
                    dgvAssessDetails.Columns[1].Width = 150;
                    dgvAssessDetails.Columns[2].Width = 75;
                    dgvAssessDetails.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvAssessDetails.Columns[2].DefaultCellStyle.Format = string.Format("N2");
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
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbCloseAccount_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (dgvAccounts.Rows.Count > 0)
                {
                    if (Convert.ToDouble(dgvAccounts.SelectedRows[0].Cells["BALANCE"].Value.ToString()) != 0)
                        throw new Exception("Account has balance and cannot be closed");

                    if(!dgvAccounts.SelectedRows[0].Cells["STATUS"].Value.ToString().Equals("ENROLLED"))
                        throw new Exception("Account should have status 'ENROLLED' to be closed");


                    long reg = Convert.ToInt64(dgvAccounts.SelectedRows[0].Cells["REG. No."].Value.ToString());
                    bool success = new Account().CloseAccount(reg);
                    if (success)
                    {
                        tsbRefresh.PerformClick();
                        MessageBox.Show("Account successfully closed", "Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }                        
                    else
                        throw new Exception("Fail to perform this action. Server might be not available.");
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Account account = new Account();
                dgvAccounts.DataSource = account.GetAllStudentAccount(txtSN.Text.Trim());
                dgvAccounts.Columns[0].Width = 100;
                dgvAccounts.Columns[1].Width = 150;
                dgvAccounts.Columns[2].Width = 150;
                dgvAccounts.Columns[3].Width = 150;
                dgvAccounts.Columns[4].Width = 150;
                dgvAccounts.Columns[5].Width = 100;
                dgvAccounts.Columns[6].Width = 100;
                dgvAccounts.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvAccounts.Columns[6].DefaultCellStyle.Format = string.Format("N2");

                if (dgvAccounts.Rows.Count > 0)
                {
                    dgvAccounts.Rows[0].Selected = true;
                    account.GetAccountByRegistrationId(Convert.ToInt64(dgvAccounts.SelectedRows[0].Cells["REG. No."].Value.ToString()));
                    dgvAccountDetail.DataSource = account.StudentPayment;
                    dgvAccountDetail.Columns[0].Width = 100;
                    dgvAccountDetail.Columns[1].Width = 75;
                    dgvAccountDetail.Columns[2].Width = 75;
                    dgvAccountDetail.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvAccountDetail.Columns[1].DefaultCellStyle.Format = string.Format("N2");
                    dgvAccountDetail.Columns[3].Visible = false;

                    dgvAssessDetails.DataSource = new AssessmentDetail().GetBalancePerAssessmentItem(account.StudentAssessment.Id, 4);
                    dgvAssessDetails.Columns[0].Width = 75;
                    dgvAssessDetails.Columns[1].Width = 150;
                    dgvAssessDetails.Columns[2].Width = 75;
                    dgvAssessDetails.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvAssessDetails.Columns[2].DefaultCellStyle.Format = string.Format("N2");
                }
                if (dgvAccountDetail.Rows.Count > 0)
                {
                    dgvAccountDetail.Rows[0].Selected = true;
                    dgvPayDetails.DataSource = new PaymentDetail().GetAllPaymentDetails(dgvAccountDetail.SelectedRows[0].Cells[0].Value.ToString());
                    dgvPayDetails.Columns[0].Width = 215;
                    dgvPayDetails.Columns[1].Width = 75;
                    dgvPayDetails.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvPayDetails.Columns[1].DefaultCellStyle.Format = string.Format("N2");
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
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbOpenAccount_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (dgvAccounts.Rows.Count > 0)
                {
                    if (!dgvAccounts.SelectedRows[0].Cells["STATUS"].Value.ToString().Equals("CLOSED"))
                        throw new Exception("Account should have status 'CLOSED' to be opened.");


                    long reg = Convert.ToInt64(dgvAccounts.SelectedRows[0].Cells["REG. No."].Value.ToString());
                    bool success = new Account().OpenAccount(reg);
                    if (success)
                    {
                        tsbRefresh.PerformClick();
                        MessageBox.Show("Account successfully opened", "Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        throw new Exception("Fail to perform this action. Server might be not available.");
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvAccountDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dgvAccountDetail.Rows.Count > 0)
            {
                dgvPayDetails.DataSource = new PaymentDetail().GetAllPaymentDetails(dgvAccountDetail.SelectedRows[0].Cells[0].Value.ToString());
                dgvPayDetails.Columns[0].Width = 215;
                dgvPayDetails.Columns[1].Width = 75;
                dgvAccountDetail.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvAccountDetail.Columns[1].DefaultCellStyle.Format = string.Format("N2");
            }
            Cursor.Current = Cursors.Default;
        }

        private void dgvPayDetails_SelectionChanged(object sender, EventArgs e)
        {
            dgvPayDetails.ClearSelection();
        }

        private void tsbPrintLedger_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSN.Text.Trim().Equals("") && txtName.Text.Trim().Equals(""))
                    throw new Exception("Nothing to print");

                frmReport newMDIChild = new frmReport(ReportTypes.Ledger);
                newMDIChild.SoAStudent = student;
                newMDIChild.MdiParent = ParentForm;
                newMDIChild.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
