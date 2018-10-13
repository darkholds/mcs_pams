using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AccountingMgt
{
    public partial class frmDisbursement : Form
    {
        public frmDisbursement()
        {
            InitializeComponent();
        }

        private void frmDisbursement_Load(object sender, EventArgs e)
        {
            tsbRefresh.PerformClick();
            if (dgvDisbursements.RowCount > 0)
            {
                dgvDisbursements.Rows[0].Selected = true;
            }               
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
                dgvDisbursements.DataSource = new Disbursement().GetAllDisbursement();
                dgvDisbursements.Columns[0].Width = 50;
                dgvDisbursements.Columns[1].Width = 100;
                dgvDisbursements.Columns[2].Width = 100;
                dgvDisbursements.Columns[3].Width = 250;
                dgvDisbursements.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDisbursements.Columns[2].DefaultCellStyle.Format = String.Format("N2");
                dgvDisbursements.Columns[0].Visible = false;

                dgvSource.Columns.Clear();
                dgvSource.DataSource = new Assessment().GetTotalCollectionsSummary();
                dgvSource.Columns[0].Width = 100;
                dgvSource.Columns[1].Width = 175;
                dgvSource.Columns[2].Width = 100;
                dgvSource.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSource.Columns[2].DefaultCellStyle.Format = String.Format("N2");

                DataGridViewTextBoxColumn txtCol = new DataGridViewTextBoxColumn();
                txtCol.HeaderText = "ENTER AMOUNT";
                txtCol.Name = "colDisburse";
                txtCol.ReadOnly = false;
                txtCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                txtCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                txtCol.Width = 75;
                dgvSource.Columns.Add(txtCol);
                dgvSource.Columns[0].ReadOnly = true;
                dgvSource.Columns[1].ReadOnly = true;
                dgvSource.Columns[2].ReadOnly = true;
                dgvSource.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvSource.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvSource.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
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

        private void tsbClear_Click(object sender, EventArgs e)
        {
            txtPurpose.Text = string.Empty;
            tsbRefresh.PerformClick();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (txtPurpose.Text.Equals("")) throw new Exception("Please indicate purpose of disbursement");
                double total = 0;
                List<PaymentDetail> disburseDetail = new List<PaymentDetail>();
                foreach (DataGridViewRow row in dgvSource.Rows)
                {
                    string rowvalue = (string) row.Cells["colDisburse"].Value;
                    if (!string.IsNullOrWhiteSpace(rowvalue))
                    {
                        double val = 0;
                        bool isGood = Double.TryParse(row.Cells["colDisburse"].Value.ToString(), out val);
                        if (isGood)
                        {
                            total += val;
                            PaymentDetail pd = new PaymentDetail();
                            pd.Code = row.Cells["CODE"].Value.ToString();
                            pd.Amount = val;
                            disburseDetail.Add(pd);
                        }
                        else
                        {
                            throw new Exception("Please check entered amount.");
                        }
                    }                  
                }

                if (total == 0 || disburseDetail.Count==0) throw new Exception("Please allocate some amount for disbursement");

                if (new Disbursement().SaveDisbursement(txtPurpose.Text.Trim(), total, disburseDetail))
                {
                    MessageBox.Show("Disbursement saved.");
                    tsbClear.PerformClick();
                }                 
                else
                    throw new Exception("Error occurs in disbursement");
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Cursor.Current = Cursors.Default;
        }

        private void dgvSource_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double total = 0;
                foreach (DataGridViewRow row in dgvSource.Rows)
                {
                    string strval = (string)row.Cells["colDisburse"].Value;
                    if (!string.IsNullOrWhiteSpace(strval))
                    {
                        double colval = Convert.ToDouble(strval);
                        if (colval > Convert.ToDouble(row.Cells["TOTAL COLLECTIONS"].Value))
                        {
                            row.Cells["colDisburse"].Value = string.Empty;
                            throw new Exception("Amount entered is higher than the fund.");                           
                        }
                           
                        total += colval;
                        row.Cells["colDisburse"].Value = colval.ToString("N2");
                    }                       
                }
                tsslTotal.Text = "TOTAL: " + total.ToString("N2");
            }
            catch (FormatException)
            {
                MessageBox.Show("Input amount format is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvDisbursements_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (dgvDisbursements.SelectedRows.Count > 0)
                {
                    dgvDisburseDetail.DataSource = new Disbursement().GetDisbursementDetails(Convert.ToInt64(dgvDisbursements.SelectedRows[0].Cells["ID"].Value));
                    dgvDisburseDetail.Columns[0].Width = 300;
                    dgvDisburseDetail.Columns[1].Width = 150;
                    dgvDisburseDetail.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvDisburseDetail.Columns[1].DefaultCellStyle.Format = String.Format("N2");
                    dgvDisburseDetail.ClearSelection();
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvDisburseDetail_SelectionChanged(object sender, EventArgs e)
        {
            dgvDisburseDetail.ClearSelection();
        }
    }
}
