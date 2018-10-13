using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;

namespace AccountingMgt
{
    public partial class frmReport : Form
    {
        ReportTypes ReportType = 0;
        Report report;
        public DateTime DailyReportSummaryDate { get; set; }
        public DateTime MonthlyReportDate { get; set; }
        public int PayType { get; set; }
        public MonthlyReportType MonthReportType { get; set; }
        public Student Payer { get; set; }
        public string ORNumber { get; set; }
        public Student SoAStudent { get; set; }
        public List<PaymentDetail> SoAItems { get; set; }

        public frmReport(ReportTypes ReportType)
        {
            InitializeComponent();
            this.ReportType = ReportType;
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            try
            {
                rvReport.SetDisplayMode(DisplayMode.PrintLayout);
                if (ReportType == ReportTypes.OR)
                {
                    report = new Receipt(rvReport, "AccountingMgt.rptReceipt.rdlc", Payer, ORNumber);
                    ((Receipt)report).RunReport();
                }
                else if(ReportType == ReportTypes.OFOR)
                {
                    report = new OFReceipt(rvReport, "AccountingMgt.rptReceiptOF.rdlc", Payer, ORNumber);
                    ((OFReceipt)report).RunReport();
                }
                else if (ReportType == ReportTypes.PTAOR)
                {
                    report = new PTAReceipt(rvReport, "AccountingMgt.rptReceiptPTA.rdlc", Payer, ORNumber);
                    ((PTAReceipt)report).RunReport();
                }
                else if (ReportType == ReportTypes.DailyCollection)
                {
                    report = new DailySummaryReport(rvReport, "AccountingMgt.rptDailySummary.rdlc", DailyReportSummaryDate);
                    ((DailySummaryReport)report).RunReport();
                }
                else if (ReportType == ReportTypes.SOA)
                {
                    report = new SoaReport(rvReport, "AccountingMgt.rptSOA.rdlc", SoAStudent, (MdiParent as frmMDI).LoginUser, SoAItems);
                    ((SoaReport)report).RunReport();
                }
                else if(ReportType == ReportTypes.Ledger)
                {
                    report = new LedgerReport(rvReport, "AccountingMgt.rptLedger.rdlc", SoAStudent);
                    ((LedgerReport)report).RunReport();
                }
                else if (ReportType == ReportTypes.MonthlyCollectionDetail)
                {
                    if (PayType == 1)
                        report = new MonthlyDetailReport(rvReport, "AccountingMgt.rptMonthlyDetail.rdlc", MonthlyReportDate, PayType, MonthReportType);
                    else if(PayType==2)
                        report = new MonthlyDetailReport(rvReport, "AccountingMgt.rptMonthlyDetailNS.rdlc", MonthlyReportDate, PayType, MonthReportType);
                    else
                        report = new MonthlyDetailReport(rvReport, "AccountingMgt.rptMonthlyDetailOF.rdlc", MonthlyReportDate, PayType, MonthReportType);
                    ((MonthlyDetailReport)report).RunReport();
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
            rvReport.RefreshReport();
        }

        private void rvReport_PrintingBegin(object sender, ReportPrintEventArgs e)
        {
            if (ReportType == ReportTypes.OR)
            {
                ((Receipt)report).PrintReport();
            }
            else if (ReportType == ReportTypes.OFOR)
            {
                ((OFReceipt)report).PrintReport();
            }
            else if (ReportType == ReportTypes.PTAOR)
            {
                ((PTAReceipt)report).PrintReport();
            }
        }
    }
}
