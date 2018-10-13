using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace AccountingMgt
{
    public partial class frmSummary : Form
    {
        public frmSummary()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (txtSY.Text.Trim()!=string.Empty)
                {
                    dgvFeeSummarySY.DataSource = new Assessment().GetTotalCollectionsSummaryBySY(txtSY.Text.Trim());
                    dgvFeeSummarySY.Columns[0].Width = 75;
                    dgvFeeSummarySY.Columns[1].Width = 250;
                    dgvFeeSummarySY.Columns[2].Width = 135;
                    dgvFeeSummarySY.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvFeeSummarySY.Columns[2].DefaultCellStyle.Format = String.Format("N2");
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

        private void frmSummary_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                int year = Convert.ToInt16(DateTime.Now.Year);
                int month = Convert.ToInt16(DateTime.Now.Month);
                if (month > 4 && month < 10)
                {
                    //cmbSemester.SelectedIndex = 0;  //1st sem
                    txtSY.Text = year + "-" + (year + 1);
                    //txtSY2.Text = year + "-" + (year + 1);
                }
                else if (month > 9 && month <= 12)
                {
                    //cmbSemester.SelectedIndex = 1; //2nd sem
                    txtSY.Text = year + "-" + (year + 1);
                    //txtSY2.Text = year + "-" + (year + 1);
                }
                else if (month > 0 && month < 3)
                {
                    //cmbSemester.SelectedIndex = 1; //2nd sem
                    txtSY.Text = (year - 1) + "-" + year;
                    //txtSY2.Text = (year - 1) + "-" + year;
                }
                else
                {
                    //cmbSemester.SelectedIndex = 2; //"Summer";
                    txtSY.Text = (year - 1) + "-" + year;
                    //txtSY2.Text = (year - 1) + "-" + year;
                }

                cmbDepartment.Items.Add("JHS, Elementary, Pre-school");
                cmbDepartment.Items.Add("Senior High School");
                cmbDepartment.Items.Add("All Department");
                cmbDepartment.SelectedIndex = 0;

                cmbReportType.Items.Add("Standard Fee Details");
                cmbReportType.Items.Add("Non-standard Fee Details");
                cmbReportType.Items.Add("Other Fee Details");
                cmbReportType.SelectedIndex = 0;

                dgvTotal.DataSource = new Assessment().GetTotalAssessmentSummary();
                dgvTotal.Columns[0].Width = 165;
                dgvTotal.Columns[1].Width = 250;
                dgvTotal.Columns[2].Width = 250;
                dgvTotal.Columns[3].Width = 250;
                dgvTotal.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTotal.Columns[1].DefaultCellStyle.Format = String.Format("N2");
                dgvTotal.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTotal.Columns[2].DefaultCellStyle.Format = String.Format("N2");
                dgvTotal.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTotal.Columns[3].DefaultCellStyle.Format = String.Format("N2");
                dgvTotal.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvTotal.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvTotal.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvTotal.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;

                dgvFeeSummary.DataSource = new Assessment().GetTotalCollectionsSummary();
                dgvFeeSummary.Columns[0].Width = 85;
                dgvFeeSummary.Columns[1].Width = 200;
                dgvFeeSummary.Columns[2].Width = 150;
                dgvFeeSummary.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvFeeSummary.Columns[2].DefaultCellStyle.Format = String.Format("N2");

                if (dgvTotal.Rows.Count > 0)
                    dgvTotal.Rows[0].Selected = true;

                dtpSummary.Value = DateTime.Now;
                dtpDaily.Value = DateTime.Now;
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

        private void txtSY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (txtSY.Text.Trim().Length == 4)
                {
                    int yr;
                    int yrto;
                    if (Int32.TryParse(txtSY.Text.Trim(), out yr))
                    {
                        yrto = yr + 1;
                        txtSY.Text = yr + "-" + yrto;
                    }
                    else
                    {
                        txtSY.Text = string.Empty;
                        txtSY.Focus();
                        throw new Exception("Invalid input.");
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpSummary_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dgvMonthly.DataSource = new Payment().GetMonthlyCollection(dtpSummary.Value);
                dgvMonthly.Columns[0].Width = 100;
                dgvMonthly.Columns[1].Width = 138;
                dgvMonthly.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvMonthly.Columns[1].DefaultCellStyle.Format = String.Format("N2");

                double total = 0;
                foreach(DataGridViewRow row in dgvMonthly.Rows)
                {
                    total += Convert.ToDouble(row.Cells["AMOUNT"].Value);
                }

                tsslMonthlyCollection.Text = "Total Collection for the month of " + dtpSummary.Value.ToString("MMMM") + ", " + dtpSummary.Value.Year + ": ";
                tsslMonthlyCollectionAmount.Text = total.ToString("N2");

                dgvMonthlySummary.DataSource = new Payment().GetMonthlyCollectionSummary(dtpSummary.Value);
                dgvMonthlySummary.Columns[0].Width = 75;
                dgvMonthlySummary.Columns[1].Width = 130;
                dgvMonthlySummary.Columns[2].Width = 80;
                dgvMonthlySummary.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvMonthlySummary.Columns[2].DefaultCellStyle.Format = String.Format("N2");
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

        private void dtpDaily_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dgvDaily.DataSource = new Payment().GetDailyCollection(dtpDaily.Value);
                dgvDaily.Columns[0].Width = 100;
                dgvDaily.Columns[1].Width = 200;
                dgvDaily.Columns[2].Width = 100;
                dgvDaily.Columns[3].Width = 200;
                dgvDaily.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDaily.Columns[2].DefaultCellStyle.Format = String.Format("N2");

                double total = 0;
                foreach (DataGridViewRow row in dgvDaily.Rows)
                {
                    total += Convert.ToDouble(row.Cells["AMOUNT"].Value);
                }

                tsslDailyCollection.Text = "Total Collection for (" + dtpDaily.Value.ToShortDateString() +  "): ";
                tsslDailyCollectionAmount.Text = total.ToString("N2");

                dgvDailySummary.DataSource = new Payment().GetDailyCollectionSummary(dtpDaily.Value);
                dgvDailySummary.Columns[0].Width = 175;
                dgvDailySummary.Columns[1].Width = 100;
                dgvDailySummary.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDailySummary.Columns[1].DefaultCellStyle.Format = String.Format("N2");
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

        private void dgvTotal_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string sy = dgvTotal.Rows[0].Cells["SY"].Value.ToString();
                dgvTotalByGradeLevel.DataSource = new Assessment().GetTotalAssessmentSummaryByGradeLevel(sy);
                dgvTotalByGradeLevel.Columns[0].Width = 115;
                dgvTotalByGradeLevel.Columns[1].Width = 200;
                dgvTotalByGradeLevel.Columns[2].Width = 200;
                dgvTotalByGradeLevel.Columns[3].Width = 200;
                dgvTotalByGradeLevel.Columns[4].Width = 200;
                dgvTotalByGradeLevel.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTotalByGradeLevel.Columns[2].DefaultCellStyle.Format = String.Format("N2");
                dgvTotalByGradeLevel.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTotalByGradeLevel.Columns[3].DefaultCellStyle.Format = String.Format("N2");
                dgvTotalByGradeLevel.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTotalByGradeLevel.Columns[4].DefaultCellStyle.Format = String.Format("N2");
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

        private void tabSummary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvFeeSummarySY.Rows.Count == 0)
            {
                if (tabSummary.SelectedTab == tabSummary.TabPages["tabPage2"])
                {
                    btnRefresh.PerformClick();
                }
            }           
        }

        private void dgvDaily_SelectionChanged(object sender, EventArgs e)
        {
            dgvDaily.ClearSelection();
        }

        private void dgvMonthly_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMonthly.SelectedRows.Count > 0)
            {
                DateTime dateTime = Convert.ToDateTime(dgvMonthly.SelectedRows[0].Cells["DATE"].Value);
                dgvMonthlyDetail.DataSource = new Payment().GetMonthlyCollectionDetail(dateTime);
                dgvMonthlyDetail.Columns[0].Width = 75;
                dgvMonthlyDetail.Columns[1].Width = 151;
                dgvMonthlyDetail.Columns[2].Width = 110;
                dgvMonthlyDetail.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvMonthlyDetail.Columns[2].DefaultCellStyle.Format = String.Format("N2");
            }
            else
            {
                dgvMonthlyDetail.DataSource = null;
                dgvMonthlyDetail.Rows.Clear();
            }
        }

        private void dgvFeeSummary_SelectionChanged(object sender, EventArgs e)
        {
            dgvFeeSummary.ClearSelection();
        }

        private void dgvFeeSummarySY_SelectionChanged(object sender, EventArgs e)
        {
            dgvFeeSummarySY.ClearSelection();
        }

        private void dgvTotalByGradeLevel_SelectionChanged(object sender, EventArgs e)
        {
            dgvTotalByGradeLevel.ClearSelection();
        }

        private void dgvDailySummary_SelectionChanged(object sender, EventArgs e)
        {
            dgvDailySummary.ClearSelection();
        }

        private void btnPrintDailyCollection_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                frmReport frReport = new frmReport(ReportTypes.DailyCollection);
                frReport.MdiParent = MdiParent;
                frReport.DailyReportSummaryDate = dtpDaily.Value;
                frReport.Show();
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

        private void btnRefreshOverall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dgvFeeSummary.DataSource = new Assessment().GetTotalCollectionsSummary();
            dgvFeeSummary.Columns[0].Width = 85;
            dgvFeeSummary.Columns[1].Width = 200;
            dgvFeeSummary.Columns[2].Width = 150;
            dgvFeeSummary.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvFeeSummary.Columns[2].DefaultCellStyle.Format = String.Format("N2");
            Cursor.Current = Cursors.Default;
        }

        private void dgvMonthlyDetail_SelectionChanged(object sender, EventArgs e)
        {
            dgvMonthlyDetail.ClearSelection();
        }

        private void dgvMonthlySummary_SelectionChanged(object sender, EventArgs e)
        {
            dgvMonthlySummary.ClearSelection();
        }

        private void btnPrintMonthlyDetails_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                frmReport frReport = new frmReport(ReportTypes.MonthlyCollectionDetail);
                frReport.MdiParent = MdiParent;
                frReport.MonthlyReportDate = dtpSummary.Value;
                if (cmbDepartment.SelectedIndex==0 && cmbReportType.SelectedIndex == 0)
                {
                    frReport.PayType = 1; //standard
                    frReport.MonthReportType = MonthlyReportType.HSStandard;
                }
                else if(cmbDepartment.SelectedIndex==0 && cmbReportType.SelectedIndex==1)
                {
                    frReport.PayType = 2; //non-standard
                    frReport.MonthReportType = MonthlyReportType.HSNonStandard;
                }
                else if(cmbDepartment.SelectedIndex==1 && cmbReportType.SelectedIndex == 0)
                {
                    frReport.PayType = 1; //standard
                    frReport.MonthReportType = MonthlyReportType.SHSStandard;
                }
                else if(cmbDepartment.SelectedIndex==1 && cmbReportType.SelectedIndex == 1)
                {
                    frReport.PayType = 2; //non-standard
                    frReport.MonthReportType = MonthlyReportType.SHSNonStandard;
                }
                else if (cmbDepartment.SelectedIndex == 2 && cmbReportType.SelectedIndex == 0)
                {
                    frReport.PayType = 1; //standard
                    frReport.MonthReportType = MonthlyReportType.AllStandard;
                }
                else if (cmbDepartment.SelectedIndex == 2 && cmbReportType.SelectedIndex == 1)
                {
                    frReport.PayType = 2; //non-standard
                    frReport.MonthReportType = MonthlyReportType.AllNonStandard;
                }
                else if(cmbDepartment.SelectedIndex==0 && cmbReportType.SelectedIndex == 2)
                {
                    frReport.PayType = 3; //pta, alumni, other
                    frReport.MonthReportType = MonthlyReportType.HSOtherType;
                }
                else if (cmbDepartment.SelectedIndex == 1 && cmbReportType.SelectedIndex == 2)
                {
                    frReport.PayType = 3; //pta, alumni, other
                    frReport.MonthReportType = MonthlyReportType.SHSOtherType;
                }
                else
                {
                    frReport.PayType = 3; //pta, alumni, other
                    frReport.MonthReportType = MonthlyReportType.AllOtherType;
                }
                frReport.Show();
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
