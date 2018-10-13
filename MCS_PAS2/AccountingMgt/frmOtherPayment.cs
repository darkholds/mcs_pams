using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AccountingMgt
{
    public partial class frmOtherPayment : Form
    {
        public frmOtherPayment()
        {
            InitializeComponent();
        }

        private void frmOtherPayment_Load(object sender, EventArgs e)
        {
            cmbDepartment.Items.Add("JHS, Elem., Pre-school");
            cmbDepartment.Items.Add("Senior High School");
            cmbDepartment.SelectedIndex = 0;
            lblTotal.Text = 0.ToString("N2");
            tsbRefresh.PerformClick();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dgvItems.Columns.Clear();
                dgvItems.DataSource = new Fee().GetAllOtherFeesStrict();
                DataGridViewTextBoxColumn txtCol = new DataGridViewTextBoxColumn();
                txtCol.HeaderText = "ENTER AMOUNT";
                txtCol.Name = "txtPayment";
                txtCol.ReadOnly = false;
                txtCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvItems.Columns.Add(txtCol);              
                dgvItems.Columns[1].ReadOnly = true;
                dgvItems.Columns[2].ReadOnly = true;              
                dgvItems.Columns[1].Width = 100;
                dgvItems.Columns[2].Width = 275;
                dgvItems.Columns[3].Width = 100;
                dgvItems.Columns[0].Visible = false;              
                dgvItems.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;          
                dgvItems.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvItems.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvItems.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
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

        private void dgvItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double total = 0;
                foreach (DataGridViewRow row in dgvItems.Rows)
                {
                    string strcol = (string)row.Cells["txtPayment"].Value;
                    if (!string.IsNullOrWhiteSpace(strcol))
                    {
                        double colval = Convert.ToDouble(strcol);
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

        private void tsbClear_Click(object sender, EventArgs e)
        {
            txtName.Text = string.Empty;
            txtOR.Text = string.Empty;
            txtCash.Text = string.Empty;
            txtChange.Text = string.Empty;
            lblTotal.Text = 0.ToString("N2");
            tsbRefresh.PerformClick();
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

        private void tsbSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (txtOR.Text.Trim().Equals("") || txtCash.Text.Trim().Equals("") || !Double.TryParse(txtCash.Text.Trim(), out double res) || txtName.Text.Trim().Equals("") || cmbDepartment.Text==string.Empty)
                    throw new Exception("Please fill important fields.");

                List<PaymentDetail> paymentItems = new List<PaymentDetail>();

                foreach (DataGridViewRow row in dgvItems.Rows)
                {
                    string value = (string)row.Cells["txtPayment"].Value;
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        if (!Double.TryParse(value, out double val))
                        {
                            dgvItems.CurrentCell = row.Cells["txtPayment"];
                            dgvItems.BeginEdit(true);
                            throw new Exception("Invalid amount.");
                        }

                        if (Convert.ToDouble(row.Cells["txtPayment"].Value) > 0)
                        {
                            PaymentDetail feeObj = new PaymentDetail();
                            feeObj.Code = row.Cells["CODE"].Value.ToString();
                            feeObj.Amount = Convert.ToDouble(row.Cells["txtPayment"].Value);
                            paymentItems.Add(feeObj);
                        }
                    }
                }

                double paymentTotal = 0;
                if (paymentItems.Count > 0 && Double.TryParse(lblTotal.Text.Trim(), out paymentTotal) && paymentTotal > 0)
                {
                    int paymenttype = 5;
                    Payment payment = new Payment(-1, txtOR.Text.Trim(), Convert.ToDouble(lblTotal.Text.Trim()), (MdiParent as frmMDI).LoginUser.Username, paymenttype);
                    payment.PaymentDetail = paymentItems;
                    payment.Payer = txtName.Text.Trim();
                    if (cmbDepartment.SelectedIndex == 0)
                        payment.Division = 1;
                    else if (cmbDepartment.SelectedIndex == 1)
                        payment.Division = 2;

                    if (payment.SavePaymentOther())
                    {
                        DialogResult dlg = MessageBox.Show("Payment save successful.\nPlease insert Official Receipt(OR) for printing", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        if (dlg == DialogResult.OK)
                        {
                            frmReport frReport;
                            frReport = new frmReport(ReportTypes.OFOR);
                           
                            Student Payer = new Student();
                            Payer.LastName = txtName.Text.Trim();
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
    }
}
