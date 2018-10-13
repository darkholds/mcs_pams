using MySql.Data.MySqlClient;
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
    public partial class frmPayment : Form
    {
        private long id_assessment { get; set; }
        private Student Payer;

        public frmPayment(Student payer, long idassessment)
        {
            InitializeComponent();
            Payer = payer;
            id_assessment = idassessment;
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lblSN.Text = Payer.Id;
                lblName.Text = Payer.LastName + ", " + Payer.FirstName + " " + Payer.MiddleName;
                lblGrade.Text = Payer.CurrentYearLevel;
                lblTotal.Text = 0.ToString("N2");        

                cmbPaymentType.Items.Add("Tuition and Standard Fees");
                cmbPaymentType.Items.Add("Non-Standard and Other Fees");
                cmbPaymentType.Items.Add("PTA");
                cmbPaymentType.Items.Add("Alumni");
                cmbPaymentType.SelectedIndex = 0;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            txtOR.Text = string.Empty;
            txtCash.Text = string.Empty;
            txtChange.Text = string.Empty;
            lblTotal.Text = 0.ToString("N2");
            RefreshFees();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (txtOR.Text.Trim().Equals("") || txtCash.Text.Trim().Equals("") || !Double.TryParse(txtCash.Text.Trim(), out double res))
                    throw new Exception("Please fill important fields.");

                List<PaymentDetail> paymentItems = new List<PaymentDetail>();

                foreach(DataGridViewRow row in dgvBalance.Rows)
                {
                    string value = (string) row.Cells["txtPayment"].Value;
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        if (!Double.TryParse(value, out double val))
                        {
                            dgvBalance.CurrentCell = row.Cells["txtPayment"];
                            dgvBalance.BeginEdit(true);
                            throw new Exception("Invalid amount.");
                        }

                        if (Convert.ToDouble(row.Cells["txtPayment"].Value) > Convert.ToDouble(row.Cells["BALANCE"].Value))
                        {
                            dgvBalance.CurrentCell = row.Cells["txtPayment"];
                            dgvBalance.BeginEdit(true);
                            throw new Exception("Amount entered is higher than the balance.");
                        }

                        if (Convert.ToDouble(row.Cells["txtPayment"].Value) > 0){
                            PaymentDetail feeObj = new PaymentDetail();
                            feeObj.Code = row.Cells["CODE"].Value.ToString();
                            feeObj.Amount = Convert.ToDouble(row.Cells["txtPayment"].Value);
                            paymentItems.Add(feeObj);
                        }
                    }
                }

                double paymentTotal = 0;
                if (paymentItems.Count > 0 && Double.TryParse(lblTotal.Text.Trim(), out paymentTotal) && paymentTotal>0)
                {
                    int paymenttype = 0;
                    if (cmbPaymentType.SelectedIndex == 0)
                        paymenttype = 1;
                    else if (cmbPaymentType.SelectedIndex == 1)
                        paymenttype = 2;
                    else if (cmbPaymentType.SelectedIndex == 2)
                        paymenttype = 3;
                    else if (cmbPaymentType.SelectedIndex == 3)
                        paymenttype = 4;

                    Payment payment = new Payment(id_assessment, txtOR.Text.Trim(), Convert.ToDouble(lblTotal.Text.Trim()), (MdiParent as frmMDI).LoginUser.Username, paymenttype);
                    payment.PaymentDetail = paymentItems;
                    if (payment.SavePayment())
                    {                       
                        DialogResult dlg = MessageBox.Show("Payment save successful.\nPlease insert Official Receipt(OR) for printing", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        if (dlg == DialogResult.OK)
                        {
                            frmReport frReport;
                            if (cmbPaymentType.SelectedIndex == 0)
                            {
                                frReport = new frmReport(ReportTypes.OR);                               
                            }
                            else if (cmbPaymentType.SelectedIndex == 1 || cmbPaymentType.SelectedIndex==3)
                            {
                                frReport = new frmReport(ReportTypes.OFOR);
                            }
                            else
                            {
                                frReport = new frmReport(ReportTypes.PTAOR);                               
                            }

                            frReport.Payer = Payer;
                            frReport.ORNumber = txtOR.Text.Trim();
                            frReport.MdiParent = MdiParent;
                            frReport.Show();
                        }                      
                        tsbClear.PerformClick();
                    }
                    else
                    {
                        throw new Exception("Error in saving this payment.");
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
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCash_Leave(object sender, EventArgs e)
        {
            try
            {
                if ((Double.Parse(txtCash.Text) - Double.Parse(lblTotal.Text)) < 0)
                {
                    txtCash.Focus();
                    throw new Exception("Cash should be bigger than Payment amount");
                }

                Double value;
                if (Double.TryParse(txtCash.Text, out value))
                    txtCash.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
                else
                {
                    txtCash.Text = String.Empty;
                    txtCash.Focus();
                }

                if (lblTotal.Text.Trim() != String.Empty && txtCash.Text.Trim() != String.Empty)
                {
                    txtChange.Text = (Double.Parse(txtCash.Text) - Double.Parse(lblTotal.Text)).ToString("N2");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Cash format is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvBalance_Enter(object sender, EventArgs e)
        {
            if (dgvBalance.Rows.Count > 0)
            {
                dgvBalance.CurrentCell = dgvBalance.Rows[0].Cells["txtPayment"];
                dgvBalance.BeginEdit(true);
            }        
        }

        private void dgvBalance_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double total = 0;
                foreach (DataGridViewRow row in dgvBalance.Rows)
                {
                    string strcol = (string)row.Cells["txtPayment"].Value;
                    if (!string.IsNullOrWhiteSpace(strcol))
                    {
                        double colval = Convert.ToDouble(strcol);
                        if (colval > Convert.ToDouble(row.Cells["BALANCE"].Value))
                        {
                            row.Cells["txtPayment"].Value = string.Empty;
                            throw new Exception("Amount entered is higher than the balance.");
                        }

                        total += colval;
                        row.Cells["txtPayment"].Value = colval.ToString("N2");
                    }                  
                }
                lblTotal.Text = total.ToString("N2");
            }
            catch (FormatException)
            {
                MessageBox.Show("Input amount format is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFees();
        }

        private void RefreshFees()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dgvBalance.Columns.Clear();
                if (cmbPaymentType.SelectedIndex == 0)
                {
                    dgvBalance.DataSource = new AssessmentDetail().GetBalancePerAssessmentItem(id_assessment, 0);
                }
                else if (cmbPaymentType.SelectedIndex == 1)
                {
                    dgvBalance.DataSource = new AssessmentDetail().GetBalancePerAssessmentItem(id_assessment, 1);
                }
                else if (cmbPaymentType.SelectedIndex == 2)
                {
                    dgvBalance.DataSource = new AssessmentDetail().GetBalancePerAssessmentItem(id_assessment, 2);
                }
                else if (cmbPaymentType.SelectedIndex == 3)
                {
                    dgvBalance.DataSource = new AssessmentDetail().GetBalancePerAssessmentItem(id_assessment, 3);
                }

                DataGridViewTextBoxColumn txtCol = new DataGridViewTextBoxColumn();
                txtCol.HeaderText = "ENTER AMOUNT";
                txtCol.Name = "txtPayment";
                txtCol.ReadOnly = false;
                txtCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvBalance.Columns.Add(txtCol);
                dgvBalance.Columns[0].ReadOnly = true;
                dgvBalance.Columns[1].ReadOnly = true;
                dgvBalance.Columns[2].ReadOnly = true;
                dgvBalance.Columns[0].Width = 75;
                dgvBalance.Columns[1].Width = 275;
                dgvBalance.Columns[2].Width = 75;
                dgvBalance.Columns[3].Width = 75;
                dgvBalance.Columns[2].DefaultCellStyle.Format = string.Format("N2");
                dgvBalance.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBalance.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvBalance.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvBalance.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvBalance.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
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
    }
}
