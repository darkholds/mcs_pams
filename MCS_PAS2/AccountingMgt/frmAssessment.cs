using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace AccountingMgt
{
    public partial class frmAssessment : Form
    {
        public frmAssessment()
        {
            InitializeComponent();
        }

        private void frmAssessment_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                scAssessment.Panel1Collapsed = true;
                lblTotal.Text = 0.ToString("N2");
                dgvFees.DataSource = new Fee().GetAllFees();
                dgvFees.Columns[0].Width = 75;
                dgvFees.Columns[1].Width = 250;
                dgvFees.Columns[2].Visible = false; //feetype
                rbDiscount.Checked = true;
                gbDiscount.Enabled = false;
                ttDiscount.SetToolTip(btnSubtract, "Subtract Discount");
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                 Student s = new Student();
                if (txtSN.Text.Trim()!=string.Empty)
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
                    scAssessment.Panel1Collapsed = false;
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

        private void dgvStudent_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (dgvStudent.SelectedRows.Count > 0)
                {
                    txtSN.Text = dgvStudent.SelectedRows[0].Cells["STUDENT NUMBER"].Value.ToString();
                    txtName.Text = dgvStudent.SelectedRows[0].Cells["LAST NAME"].Value.ToString() + ", " + dgvStudent.SelectedRows[0].Cells["FIRST NAME"].Value.ToString() + " " + dgvStudent.SelectedRows[0].Cells["MIDDLE NAME"].Value.ToString();

                    Registration reg = new Registration().GetRegistration(txtSN.Text.Trim());
                    if (reg == null) throw new Exception("Student/Pupil has no active registration");
                    lblRegId.Text = reg.Id.ToString();
                    lblSchoolYear.Text = reg.SchoolYear;
                    lblSem.Text = reg.Semester;
                    lblGradeLevel.Text = reg.GradeLevel;
                    lblSection.Text = reg.Section;
                    lblStatus.Text = reg.Status;
                    lblDateReg.Text = reg.DateRegistered;

                    Assessment asses = new Assessment().GetAssessment(reg.Id);
                    if (asses == null) {
                        asses = new Assessment();
                        asses.CreateBlankAssessmentFromRegistration(reg.Id);
                        asses.GetAssessment(reg.Id);
                        MessageBox.Show("Student is registered but has no initial assessment. Select fees to add assessment details", "No Assessment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //throw new Exception("Student/Pupil has no active assessment");
                    }

                    ttDiscount.SetToolTip(lblTuition, "SCHOLARSHIP INFORMATION\nDiscount: " + asses.Discount.ToString("N2") + "\nSubsidy: " + asses.Subsidy.ToString("N2"));
                    lblRegId.Tag = asses.Id;
                    dgvAssessDetail.DataSource = asses.AssessmentDetail;
                    dgvAssessDetail.Columns[0].Width = 75;
                    dgvAssessDetail.Columns[1].Width = 300;
                    dgvAssessDetail.Columns[2].Width = 75;
                    dgvAssessDetail.Columns[3].Visible = false; //feetype
                    dgvAssessDetail.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvAssessDetail.Columns[2].DefaultCellStyle.Format = string.Format("N2");               

                    RefreshDetails();

                    scAssessment.Panel1Collapsed = true;
                    txtSN.ReadOnly = true;
                    txtName.ReadOnly = true;
                    gbDiscount.Enabled = true;
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
                tsbClear.PerformClick();
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

        private void tsbClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            scAssessment.Panel1Collapsed = true;
            lblRegId.Tag = null;
            txtSN.Text = string.Empty;
            txtName.Text = string.Empty;
            lblRegId.Text = string.Empty;
            lblSchoolYear.Text = string.Empty;
            lblGradeLevel.Text = string.Empty;
            lblSection.Text = string.Empty;
            lblSem.Text = string.Empty;
            lblStatus.Text = string.Empty;
            lblDateReg.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            txtSN.ReadOnly = false;
            txtName.ReadOnly = false;
            lblTotal.Text = 0.ToString("N2");
            lblTuition.Text = 0.ToString("N2");
            lblSF.Text = 0.ToString("N2");
            lblNSF.Text = 0.ToString("N2");
            lblOF.Text = 0.ToString("N2");
            ttDiscount.SetToolTip(lblTuition, "");
            gbDiscount.Enabled = false;

            dgvStudent.DataSource = null;
            dgvStudent.Rows.Clear();
            dgvAssessDetail.DataSource = null;
            dgvAssessDetail.Rows.Clear();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshDetails()
        {
            double tuition = 0;
            double sf = 0;
            double nonsf = 0;
            double of = 0;
            double total = 0;

            foreach (DataGridViewRow row in dgvAssessDetail.Rows)
            {
                if (Convert.ToInt16(row.Cells["feetype"].Value) == 1)
                    tuition += Convert.ToDouble(row.Cells["AMOUNT"].Value);
                else if (Convert.ToInt16(row.Cells["feetype"].Value) == 2)
                    sf += Convert.ToDouble(row.Cells["AMOUNT"].Value);
                else if (Convert.ToInt16(row.Cells["feetype"].Value) == 3)
                    nonsf += Convert.ToDouble(row.Cells["AMOUNT"].Value);
                else
                    of += Convert.ToDouble(row.Cells["AMOUNT"].Value);

                total += Convert.ToDouble(row.Cells["AMOUNT"].Value);
            }

            lblTuition.Text = tuition.ToString("N2");
            lblSF.Text = sf.ToString("N2");
            lblNSF.Text = nonsf.ToString("N2");
            lblOF.Text = of.ToString("N2");
            lblTotal.Text = total.ToString("N2");
        }

        private void dgvAssessDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvAssessDetail.SelectedRows.Count == 0) throw new Exception("No data is selected");

                DialogResult dlg = MessageBox.Show("Are you sure to delete this fee?", "Delete Fee", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dlg == DialogResult.Yes)
                {
                    if (lblStatus.Text.Equals("ENROLLED"))
                    {
                        dlg=MessageBox.Show("Student is officially enrolled, delete may cause inconsistency of record associations to payment. Are you sure?", "Delete Fee", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dlg == DialogResult.No)
                            return;
                    }
                    Cursor.Current = Cursors.WaitCursor;
                    string code = dgvAssessDetail.SelectedRows[0].Cells["CODE"].Value.ToString();
                    long idass = Convert.ToInt64(lblRegId.Tag);

                    AssessmentDetail assdetail = new AssessmentDetail();
                    bool deletesuccess = assdetail.DeleteAssessmentDetail(idass, code);
                    dgvAssessDetail.DataSource = assdetail.GetAssessmentDetail(idass);
                    RefreshDetails();

                    Cursor.Current = Cursors.Default;
                    if(deletesuccess)
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvFees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvFees.SelectedRows.Count == 0 || string.IsNullOrEmpty(lblRegId.Text)) throw new Exception("No data is selected");

                string code = dgvFees.SelectedRows[0].Cells["CODE"].Value.ToString();
                string name = dgvFees.SelectedRows[0].Cells["FEE"].Value.ToString();
                long idass = Convert.ToInt64(lblRegId.Tag);

                double amount  = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox("Enter amount for " + name, "Input Amount","0"));
                Cursor.Current = Cursors.WaitCursor;
                
                AssessmentDetail assdetail= new AssessmentDetail();
                bool success = assdetail.AddAssessmentDetail(idass, code, amount);
                if (!success)
                    throw new Exception("Duplicate fee in assessment");
                else
                {
                    dgvAssessDetail.DataSource = assdetail.GetAssessmentDetail(idass);
                    RefreshDetails();
                    MessageBox.Show("Add successful");
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

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch.PerformClick();
            }
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblRegId.Text.Trim().Equals("") || txtDiscount.Text.Trim().Equals(""))
                    throw new Exception("Nothing to update");

                double discount = 0;
                if (!Double.TryParse(txtDiscount.Text.Trim(), out discount))
                {
                    throw new Exception("Invalid discount or subsidy value.");
                }

                int discounttype = 0;
                if (rbSubsidy.Checked)
                    discounttype = 1;

                long id = new Assessment().GetAssessment(Convert.ToInt64(lblRegId.Text.Trim())).Id;
                double tuition = 0;
                bool tbool = Double.TryParse(lblTuition.Text.Trim(), out tuition);
                if (!tbool || tuition == 0)
                    throw new Exception("Invalid tuition amount");

                bool success = new AssessmentDetail().UpdateTuition(id, tuition - discount, discount, discounttype);
                if (success)
                {
                    Assessment asses = new Assessment().GetAssessment(Convert.ToInt64(lblRegId.Text.Trim()));
                    dgvAssessDetail.DataSource = asses.AssessmentDetail;
                    dgvAssessDetail.Columns[0].Width = 75;
                    dgvAssessDetail.Columns[1].Width = 300;
                    dgvAssessDetail.Columns[2].Width = 75;
                    dgvAssessDetail.Columns[3].Visible = false; //feetype
                    dgvAssessDetail.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvAssessDetail.Columns[2].DefaultCellStyle.Format = string.Format("N2");

                    RefreshDetails();

                    MessageBox.Show("Assessment update successful!");
                }
                else
                {
                    throw new Exception("Updating this assessment failed, server might be offline or nothing to update.");
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
                txtDiscount.Text = string.Empty;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
