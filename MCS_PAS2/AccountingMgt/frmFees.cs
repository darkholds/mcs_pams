using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace AccountingMgt
{
    public partial class frmFees : Form
    {
        public frmFees()
        {
            InitializeComponent();
        }

        private void frmFees_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                RefreshGradeLevels();
                RefreshAllGradeLevelFees();
                RefreshGradeLevelFees();
                Cursor.Current = Cursors.Default;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefreshGradeLevelFees()
        {
            try
            { 
                dgvFee.DataSource = new GradeLevelFees().GetAllStandardFeesInGradeLevel(Convert.ToInt16(cmbGradeLevel.SelectedValue));
                dgvFee.Columns[0].Width = 75;
                dgvFee.Columns[1].Width = 260;
                dgvFee.Columns[2].Width = 75;
                dgvFee.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvFee.Columns[2].DefaultCellStyle.Format = string.Format("N2");
                dgvFee.Columns[3].Visible = false;

                dgvNS.DataSource = new GradeLevelFees().GetAllNonStandardFeesInGradeLevel(Convert.ToInt16(cmbGradeLevel.SelectedValue));
                dgvNS.Columns[0].Width = 75;
                dgvNS.Columns[1].Width = 260;
                dgvNS.Columns[2].Width = 75;
                dgvNS.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvNS.Columns[2].DefaultCellStyle.Format = string.Format("N2");
                dgvNS.Columns[3].Visible = false;

                dgvOther.DataSource = new GradeLevelFees().GetAllOtherFeesInGradeLevel(Convert.ToInt16(cmbGradeLevel.SelectedValue));
                dgvOther.Columns[0].Width = 75;
                dgvOther.Columns[1].Width = 260;
                dgvOther.Columns[2].Width = 75;
                dgvOther.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvOther.Columns[2].DefaultCellStyle.Format = string.Format("N2");
                dgvOther.Columns[3].Visible = false;

                tsslItem.Text = (dgvFee.RowCount + dgvNS.RowCount + dgvOther.RowCount).ToString() + " items";

                //add all fees
                double totalOF=0;
                double tuition = 0;
                foreach(DataGridViewRow row in dgvFee.Rows)
                {
                    if (Convert.ToInt16(row.Cells["TYPE"].Value) == 2 || Convert.ToInt16(row.Cells["TYPE"].Value) == 3)
                        totalOF += Convert.ToDouble(row.Cells["AMOUNT"].Value);
                    else if(Convert.ToInt16(row.Cells["TYPE"].Value) == 1)
                        tuition+= Convert.ToDouble(row.Cells["AMOUNT"].Value);
                }

                foreach(DataGridViewRow row in dgvNS.Rows)
                    totalOF += Convert.ToDouble(row.Cells["AMOUNT"].Value);
             
                foreach(DataGridViewRow row in dgvOther.Rows)
                    totalOF += Convert.ToDouble(row.Cells["AMOUNT"].Value);

                double total = totalOF + tuition;

                tsslTotalOF.Text = "Total Other Fees: " + totalOF.ToString("N2");
                tsslTuition.Text = "Tuition: " + tuition.ToString("N2");
                tsslTotal.Text = "Total: " + total.ToString("N2");

                dgvFee.ClearSelection();
                dgvNS.ClearSelection();
                dgvOther.ClearSelection();
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

        private void RefreshGradeLevels()
        {
            try
            { 
                cmbGradeLevel.DisplayMember = "gradename";
                cmbGradeLevel.ValueMember = "idgradelevel";
                cmbGradeLevel.DataSource = new GradeLevelFees().GetAllGradeLevel();
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

        private void RefreshAllGradeLevelFees()
        {
            try
            { 
                cmbFeeName.DisplayMember = "FEE";
                cmbFeeName.ValueMember = "CODE";
                cmbFeeName.DataSource = new Fee().GetAllFees();
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

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbFeeName.SelectedValue == null || cmbFeeName.Text==string.Empty) throw new Exception("No fee selected.");

                Cursor.Current = Cursors.WaitCursor;
                foreach (DataGridViewRow row in dgvFee.Rows)
                { 
                    if (row.Cells[0].Value.Equals(cmbFeeName.SelectedValue))
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("This fee is already in the list");
                        return;
                    }
                }

                double result;
                if(!Double.TryParse(txtAmount.Text.Trim(), out result)) throw new Exception("Invalid amount");

                new GradeLevelFees().SaveFeeInList(Convert.ToInt16(cmbGradeLevel.SelectedValue), cmbFeeName.SelectedValue.ToString(), Convert.ToDouble(txtAmount.Text.Trim()));
                RefreshGradeLevelFees();
                cmbFeeName.SelectedIndex = 0;
                txtAmount.Text = string.Empty;
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Fee added to " + cmbGradeLevel.Text);
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
                MessageBox.Show(ex.Message);
            }          
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            btnAdd.PerformClick();
        }

        private void cmbGradeLevel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RefreshGradeLevelFees();
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            cmbGradeLevel.SelectedIndex = 0;
            cmbFeeName.SelectedIndex = 0;
            txtAmount.Text = string.Empty;
            RefreshGradeLevelFees();
            Cursor.Current = Cursors.Default;
        }

        private void tsbRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFee.SelectedRows.Count == 0 && dgvNS.SelectedRows.Count==0 && dgvOther.SelectedRows.Count==0)
                    throw new Exception("No fee is selected.");

                DialogResult dlg = MessageBox.Show("Are you sure to delete this fee?", "Delete Fee", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dlg == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    string code;
                    if(dgvFee.SelectedRows.Count>0)
                        code= dgvFee.SelectedRows[0].Cells["CODE"].Value.ToString();
                    else if(dgvNS.SelectedRows.Count>0)
                        code = dgvNS.SelectedRows[0].Cells["CODE"].Value.ToString();
                    else
                        code = dgvOther.SelectedRows[0].Cells["CODE"].Value.ToString();

                    new GradeLevelFees().DeleteFeeInGradeLevel(code, Convert.ToInt16(cmbGradeLevel.SelectedValue));
                    RefreshGradeLevelFees();
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Delete successful");
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
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvFee_Leave(object sender, EventArgs e)
        {
            dgvFee.ClearSelection();
        }

        private void dgvOther_Leave(object sender, EventArgs e)
        {
            dgvOther.ClearSelection();
        }

        private void dgvNS_Leave(object sender, EventArgs e)
        {
            dgvNS.ClearSelection();
        }

        private void frmFees_Activated(object sender, EventArgs e)
        {
            dgvFee.ClearSelection();
            dgvNS.ClearSelection();
            dgvOther.ClearSelection();
        }
    }   
}
