using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace AccountingMgt
{
    public partial class frmFee : Form
    {
        bool feeUpdate = false; //flag to indicate whether update or normal save

        public frmFee()
        {
            InitializeComponent();
        }

        private void frmFees_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cmbFeeType.Items.Add(FeeTypes.TuitionFee.ToString());
                cmbFeeType.Items.Add(FeeTypes.StandardFees.ToString());
                cmbFeeType.Items.Add(FeeTypes.NonStandardFees.ToString());
                cmbFeeType.Items.Add(FeeTypes.Other.ToString());
                cmbFeeType.Items.Add(FeeTypes.PTA.ToString());
                cmbFeeType.Items.Add(FeeTypes.Alumni.ToString());
                cmbFeeType.SelectedIndex = 0;
                RefreshFee();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshFee()
        {
            try
            {
                dgvFee.DataSource = new Fee().GetAllStandardFees();
                dgvFee.Columns[0].Width = 75;
                dgvFee.Columns[1].Width = 240;
                dgvFee.Columns[2].Width = 150;
                dgvFee.Columns[3].Visible = false;

                dgvNS.DataSource = new Fee().GetAllNonStandardFees();
                dgvNS.Columns[0].Width = 75;
                dgvNS.Columns[1].Width = 240;
                dgvNS.Columns[2].Width = 150;
                dgvNS.Columns[3].Visible = false;

                dgvOther.DataSource = new Fee().GetAllOtherFees();
                dgvOther.Columns[0].Width = 75;
                dgvOther.Columns[1].Width = 240;
                dgvOther.Columns[2].Width = 150;
                dgvOther.Columns[3].Visible = false;

                tsslItem.Text = (dgvFee.RowCount + dgvNS.RowCount + dgvOther.RowCount).ToString() + " items";

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

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            try
            {
                //save and update fee
                if (txtFeeName.Text.Trim()==string.Empty || txtFeeCode.Text.Trim()==string.Empty || txtFeeShortName.Text.Trim() == string.Empty || string.IsNullOrEmpty(cmbFeeType.Text.Trim()))
                {
                    txtFeeCode.Focus();
                    throw new Exception("Please fill important fields ");
                }

                Cursor.Current = Cursors.WaitCursor;
                Fee fee = new Fee();
                if (!feeUpdate)
                    fee.SaveFee(txtFeeCode.Text.Trim(), txtFeeName.Text.Trim(), txtFeeShortName.Text.Trim() , fee.GetFeeTypeNumeric(cmbFeeType.SelectedItem.ToString()));
                else
                    fee.UpdateFee(txtFeeCode.Tag.ToString(), txtFeeCode.Text.Trim(), txtFeeName.Text.Trim(), txtFeeShortName.Text.Trim(), fee.GetFeeTypeNumeric(cmbFeeType.SelectedItem.ToString()));

                Cursor.Current = Cursors.Default;
                MessageBox.Show("Save successful.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshFee();
                tsbCancel.PerformClick();       
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

        private void tsbUpdate_Click(object sender, EventArgs e)
        {       
            try
            {
                if (dgvFee.SelectedRows.Count == 0 && dgvNS.SelectedRows.Count==0 && dgvOther.SelectedRows.Count==0) throw new Exception("No record selected");

                DialogResult dlg = MessageBox.Show("Are you sure to update this fee?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dlg == DialogResult.Yes)
                {
                    if (feeUpdate == false)
                    {
                        if (dgvFee.SelectedRows.Count > 0)
                        {
                            txtFeeCode.Text = dgvFee.SelectedRows[0].Cells["CODE"].Value.ToString();
                            txtFeeCode.Tag = txtFeeCode.Text;
                            txtFeeName.Text = dgvFee.SelectedRows[0].Cells["FEE"].Value.ToString();
                            txtFeeShortName.Text = dgvFee.SelectedRows[0].Cells["SHORT NAME"].Value.ToString();
                            cmbFeeType.SelectedIndex = Convert.ToInt16(dgvFee.SelectedRows[0].Cells["FEE TYPE"].Value) - 1;
                        }
                        else if (dgvNS.SelectedRows.Count > 0)
                        {
                            txtFeeCode.Text = dgvNS.SelectedRows[0].Cells["CODE"].Value.ToString();
                            txtFeeCode.Tag = txtFeeCode.Text;
                            txtFeeName.Text = dgvNS.SelectedRows[0].Cells["FEE"].Value.ToString();
                            txtFeeShortName.Text = dgvNS.SelectedRows[0].Cells["SHORT NAME"].Value.ToString();
                            cmbFeeType.SelectedIndex = 2;
                        }
                        else
                        {
                            txtFeeCode.Text = dgvOther.SelectedRows[0].Cells["CODE"].Value.ToString();
                            txtFeeCode.Tag = txtFeeCode.Text;
                            txtFeeName.Text = dgvOther.SelectedRows[0].Cells["FEE"].Value.ToString();
                            txtFeeShortName.Text = dgvOther.SelectedRows[0].Cells["SHORT NAME"].Value.ToString();
                            cmbFeeType.SelectedIndex = 3;
                        }
                  
                        tsbUpdate.Enabled = false;
                        tsbDelete.Enabled = false;
                        feeUpdate = true;
                    }
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

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            txtFeeCode.Tag = null;
            txtFeeCode.Text = string.Empty;
            txtFeeName.Text = string.Empty;
            txtFeeShortName.Text = string.Empty;
            cmbFeeType.SelectedIndex = 0;
            tsbUpdate.Enabled = true;
            tsbDelete.Enabled = true;
            feeUpdate = false;
            dgvFee.ClearSelection();
            dgvNS.ClearSelection();
            dgvOther.ClearSelection();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFee.SelectedRows.Count == 0 && dgvNS.SelectedRows.Count == 0 && dgvOther.SelectedRows.Count == 0)
                {
                    throw new Exception("No fee is selected.");
                }

                DialogResult dlg = MessageBox.Show("Are you sure to delete this fee?", "Delete Fee", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dlg == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    string code;
                    if (dgvFee.SelectedRows.Count>0)
                        code = dgvFee.SelectedRows[0].Cells["CODE"].Value.ToString();
                    else if(dgvNS.SelectedRows.Count>0)
                        code = dgvNS.SelectedRows[0].Cells["CODE"].Value.ToString();
                    else
                        code = dgvOther.SelectedRows[0].Cells["CODE"].Value.ToString();

                    new Fee().DeleteFee(code);
                    Cursor.Current = Cursors.Default;

                    MessageBox.Show("Delete successful");
                    RefreshFee();
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

        private void frmFee_Activated(object sender, EventArgs e)
        {          
            dgvFee.ClearSelection();
            dgvNS.ClearSelection();
            dgvOther.ClearSelection();
        }
    }
}
