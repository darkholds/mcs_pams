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
    public partial class frmStudent : Form
    {
        bool updateStudent = false;
        public frmStudent()
        {
            InitializeComponent();
        }

        private void frmStudent_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Course c = new Course();
                c.comboView(cmbCourse);
                RefreshStudents();              
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshStudents()
        {
            try
            {
                Student s = new Student();
                dgvStudent.DataSource = s.GetAllStudent();

                dgvStudent.Columns[0].Width = 120;
                dgvStudent.Columns[1].Width = 120;
                dgvStudent.Columns[2].Width = 120;
                dgvStudent.Columns[3].Width = 120;
                dgvStudent.Columns[4].Width = 75;
                dgvStudent.Columns[5].Width = 50;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }         
        }

        private void cmbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(cmbCourse.SelectedValue.ToString());
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtMiddleName.Text = "";
            txtSN.Text = "";
            txtSN.Tag = "";
            cmbCourse.SelectedIndex = 0;
            updateStudent = false;
            tsbUpdate.Enabled = true;
            tsbDelete.Enabled = true;
            RefreshStudents();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            try
            {//save and update student
                if (txtSN.Text.Trim().Equals("") || txtFirstName.Text.Trim().Equals("") || txtLastName.Text.Trim().Equals("") || txtMiddleName.Text.Trim().Equals("")) throw new Exception("Please fill important fields");

                Cursor.Current = Cursors.WaitCursor;
                Student s = new Student(txtSN.Text.Trim(), txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtMiddleName.Text.Trim(), cmbCourse.SelectedValue.ToString());

                if (updateStudent == false)
                    s.SaveStudent();
                else
                    s.UpdateStudent(txtSN.Tag.ToString());

                RefreshStudents();
                Cursor.Current = Cursors.Default;

                MessageBox.Show("Save successful.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tsbCancel.PerformClick();
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

        private void tsbUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStudent.SelectedRows.Count > 0)
                {
                    txtSN.Tag = dgvStudent.SelectedRows[0].Cells["SN"].Value.ToString();
                    txtSN.Text = dgvStudent.SelectedRows[0].Cells["SN"].Value.ToString();
                    txtFirstName.Text = dgvStudent.SelectedRows[0].Cells["FIRST NAME"].Value.ToString();
                    txtLastName.Text = dgvStudent.SelectedRows[0].Cells["LAST NAME"].Value.ToString();
                    txtMiddleName.Text = dgvStudent.SelectedRows[0].Cells["MIDDLE NAME"].Value.ToString();
                    cmbCourse.SelectedValue = dgvStudent.SelectedRows[0].Cells["COURSE"].Value.ToString();
                    updateStudent = true;
                    tsbUpdate.Enabled = false;
                    tsbDelete.Enabled = false;
                }
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
                if (!txtSN.Text.Trim().Equals(""))
                {
                    dgvStudent.DataSource = s.SearchStudentsById(txtSN.Text.Trim());
                }
                else if (!txtLastName.Text.Trim().Equals(""))
                {
                    dgvStudent.DataSource = s.SearchStudentsByLastName(txtLastName.Text.Trim());
                }

                dgvStudent.Columns[0].Width = 120;
                dgvStudent.Columns[1].Width = 120;
                dgvStudent.Columns[2].Width = 120;
                dgvStudent.Columns[3].Width = 120;
                dgvStudent.Columns[4].Width = 75;
                dgvStudent.Columns[5].Width = 50;

                Cursor.Current = Cursors.Default;

                if (dgvStudent.Rows.Count == 0)
                {
                    throw new Exception("Student not found.");
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
                btnSearch.PerformClick();
        }

        private void txtLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnSearch.PerformClick();
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvStudent.SelectedRows.Count > 0)
                {
                    dgvAssess.DataSource = new Assessment().GetStudentAssessment(dgvStudent.SelectedRows[0].Cells[0].Value.ToString());
                    dgvAssess.Columns[0].Width = 55;
                    dgvAssess.Columns[1].Width = 200;
                    dgvAssess.Columns[2].Width = 200;
                    dgvAssess.Columns[3].Width = 75;
                    dgvAssess.Columns[4].Width = 75;
                    dgvAssess.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvAssess.Columns[3].DefaultCellStyle.Format = string.Format("N2");
                    dgvAssess.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvAssess.Columns[4].DefaultCellStyle.Format = string.Format("N2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvAssess_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvAssess.SelectedRows.Count > 0)
                {
                    dgvDetails.DataSource = new Assessment().GetStudentAssessmentDetails((int)dgvAssess.SelectedRows[0].Cells[0].Value);
                    dgvDetails.Columns[0].Width = 275;
                    dgvDetails.Columns[1].Width = 100;
                    dgvDetails.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //dgvDetails.Columns[1].DefaultCellStyle.Format = String.Format("N2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStudent.SelectedRows.Count == 0)
                {
                    throw new Exception("No student is selected.");
                }

                string id = dgvStudent.SelectedRows[0].Cells["SN"].Value.ToString();

                DialogResult dlg = MessageBox.Show("Are you sure to delete this student?", "Delete Student", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dlg == DialogResult.Yes)
                {
                    Student s = new Student();
                    s.RemoveStudent(id);

                    MessageBox.Show("Delete successful");
                    RefreshStudents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
