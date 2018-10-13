using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace AccountingMgt
{
    public partial class frmPaymentMgt : Form
    {
        public frmPaymentMgt()
        {
            InitializeComponent();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPaymentMgt_Load(object sender, EventArgs e)
        {
            tsbSave.Enabled = false;
            tsbCancel.Enabled = false;
            tsbRefresh.PerformClick();
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dgvPayment.DataSource = new Payment().GetPaymentsInMonth(DateTime.Now);
                dgvPayment.Columns[0].Width = 100;
                dgvPayment.Columns[1].Width = 100;
                dgvPayment.Columns[2].Width = 200;
                dgvPayment.Columns[3].Width = 100;
                dgvPayment.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayment.Columns[3].DefaultCellStyle.Format = String.Format("N2");
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

        private void tsbClear_Click(object sender, EventArgs e)
        {
            txtOR.Tag = null;
            txtOR.Text = string.Empty;
            dtpPayDate.Value = DateTime.Now;
            tsbEdit.Enabled = true;
            tsbClear.Enabled = true;
            tsbRefresh.Enabled = true;
            tsbCancel.Enabled = false;
            tsbSave.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOR.Text.Trim().Equals("")) throw new Exception("Fill OR No. search field");
                Cursor.Current = Cursors.WaitCursor;
                dgvPayment.DataSource = new Payment().GetPaymentByORNumberSimple(txtOR.Text.Trim());
                dgvPayment.Columns[0].Width = 100;
                dgvPayment.Columns[1].Width = 100;
                dgvPayment.Columns[2].Width = 200;
                dgvPayment.Columns[3].Width = 100;
                dgvPayment.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayment.Columns[3].DefaultCellStyle.Format = String.Format("N2");
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

        private void txtOR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnSearch.PerformClick();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPayment.SelectedRows.Count == 0) throw new Exception("No payment is selected.");
                Cursor.Current = Cursors.WaitCursor;
                tsbCancel.Enabled = true;
                tsbSave.Enabled = true;
                tsbClear.Enabled = false;
                tsbRefresh.Enabled = false;
                tsbEdit.Enabled = false;
                txtOR.Tag = dgvPayment.SelectedRows[0].Cells["ID"].Value.ToString();
                txtOR.Text = dgvPayment.SelectedRows[0].Cells["OR No."].Value.ToString();
                dtpPayDate.Value = Convert.ToDateTime(dgvPayment.SelectedRows[0].Cells["DATE"].Value);
                Cursor.Current = Cursors.Default;
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
                if (txtOR.Text.Trim().Equals("")) throw new Exception("OR No. field is invalid.");
                Cursor.Current = Cursors.WaitCursor;
                if (new Payment().SavePaymentEdited(txtOR.Text.Trim(), dtpPayDate.Value, Convert.ToInt64(txtOR.Tag)))
                {
                    tsbCancel.PerformClick();
                    tsbRefresh.PerformClick();                 
                    MessageBox.Show("Changes to this payment saved successfully.");
                }                    
                else
                    throw new Exception("Changes not saved.");
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

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            txtOR.Tag = null;
            txtOR.Text = string.Empty;
            dtpPayDate.Value = DateTime.Now;
            tsbEdit.Enabled = true;
            tsbClear.Enabled = true;
            tsbRefresh.Enabled = true;
            tsbCancel.Enabled = false;
            tsbSave.Enabled = false;
        }
    }
}
