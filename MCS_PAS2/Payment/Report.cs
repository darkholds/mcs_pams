using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment
{
    class Report
    {
        public ReportViewer RViewer { get; set; }
        public String EmbeddedResource { get; set; }

        public Report() { }
        public Report(ReportViewer rv, string report)
        {
            RViewer = rv;
            EmbeddedResource = report;
            RViewer.LocalReport.DataSources.Clear();
            RViewer.LocalReport.ReportEmbeddedResource = report;
        }
    }

    class SoaReport: Report
    {
        public SoaDetail SOAD { get; set; }

        public SoaReport(ReportViewer rv, string report, string sn, string sem, string sy)
        {
            RViewer = rv;
            EmbeddedResource = report;
            RViewer.LocalReport.DataSources.Clear();
            RViewer.LocalReport.ReportEmbeddedResource = report;

            SOAD = new SoaDetail(sn, sem, sy);

            List<Student> Stud = new List<Student>();
            Stud.Add(SOAD.Stud);

            List<Assessment> curass = new List<Assessment>();
            curass.Add(SOAD.CurrentAssessment);
            
            List<Payment> pay = new List<Payment>();
            pay.Add(SOAD.PaidAmount);

            ReportDataSource dsStud = new ReportDataSource();
            dsStud.Name = "dsStudent";
            dsStud.Value = Stud;
            RViewer.LocalReport.DataSources.Add(dsStud);

            ReportDataSource dsCurAss = new ReportDataSource();
            dsCurAss.Name = "dsAssess";
            dsCurAss.Value = curass;
            RViewer.LocalReport.DataSources.Add(dsCurAss);

            ReportDataSource dsBal = new ReportDataSource();
            dsBal.Name = "dsBalance";
            dsBal.Value = SOAD.OldBalance.Bal;
            RViewer.LocalReport.DataSources.Add(dsBal);

            ReportDataSource dsPayment = new ReportDataSource();
            dsPayment.Name = "dsPayment";
            dsPayment.Value = pay;
            RViewer.LocalReport.DataSources.Add(dsPayment);

            ReportDataSource dsPayments = new ReportDataSource();
            dsPayments.Name = "dsPayments";
            dsPayments.Value = SOAD.Payments;
            RViewer.LocalReport.DataSources.Add(dsPayments);
        }

        public void RunReport()
        {
            RViewer.LocalReport.Refresh();
            RViewer.RefreshReport();

            System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
            ps.Landscape = false;
            ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.Custom;
            ps.PaperSize = new System.Drawing.Printing.PaperSize("User-Defined", 850, 550);
            ps.Margins.Top = 2;
            ps.Margins.Bottom = 2;
            ps.Margins.Left = 2;
            ps.Margins.Right = 2;
            RViewer.ShowBackButton = false;
            RViewer.ShowFindControls = false;
            //RViewer.ShowPageNavigationControls = false;
            RViewer.ShowRefreshButton = false;
            RViewer.ShowStopButton = false;
            RViewer.SetPageSettings(ps);         
        }
    }

    class SoaDetail
    {
        public Student Stud { get; set; }
        public Assessment CurrentAssessment { get; set; }
        public BalanceList OldBalance { get; set; }
        public Payment PaidAmount { get; set; }
        public List<Payment> Payments { get; set; }

        public SoaDetail(string sn, string sem, string sy)
        {
            CurrentAssessment = new Assessment();
            OldBalance = new BalanceList();
            Stud = new Student();
            PaidAmount = new Payment();
            Payments = new List<Payment>();

            Stud.SearchStudentsById(sn);
            CurrentAssessment.GetAssessment(sn, sem, sy);
            OldBalance.GetOldBalance(sn, sem, sy);
            PaidAmount.GetPaymentSum(sn, sem, sy);

            DataTable table = new DataTable();
            table = PaidAmount.GetAllPayment(sn, CurrentAssessment.Id);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    Payment p = new Payment();
                    p.PaymentDate = Convert.ToDateTime(row["PAYMENT DATE"]).ToShortDateString();
                    p.ORNumber = row["OR NUMBER"].ToString();
                    p.Amount = Convert.ToDouble(row["AMOUNT"]);
                    Payments.Add(p);
                }
            }
        }
    }

    class Receipt: Report
    {
        Student Stud { get; set; }
        Assessment Assess { get; set; }
        PaymentBreakdown PaymentBreak { get; set; }

        public Receipt(ReportViewer rv, string report, string sn, int assessid, PaymentBreakdown pbreak)
        {
            Stud = new Student();
            Assess = new Assessment();
            PaymentBreak = new PaymentBreakdown();
            PaymentBreak.Fees = new List<Fee>();

            Stud.SearchStudentsById(sn);
            Assess.GetAssessment(sn, assessid);
            PaymentBreak = pbreak;

            RViewer = rv;
            EmbeddedResource = report;
            RViewer.LocalReport.DataSources.Clear();
            RViewer.LocalReport.ReportEmbeddedResource = report;

            List<PaymentBreakdown> pb = new List<PaymentBreakdown>();
            pb.Add(PaymentBreak);

            List<Student> stud = new List<Student>();
            stud.Add(Stud);

            List<Assessment> ass = new List<Assessment>();
            ass.Add(Assess);

            ReportDataSource dsAss = new ReportDataSource();
            dsAss.Name = "dsAssessment";
            dsAss.Value = ass;
            RViewer.LocalReport.DataSources.Add(dsAss);

            ReportDataSource dsStud = new ReportDataSource();
            dsStud.Name = "dsStudent";
            dsStud.Value = stud;
            RViewer.LocalReport.DataSources.Add(dsStud);

            ReportDataSource dsPayment = new ReportDataSource();
            dsPayment.Name = "dsPayment";
            dsPayment.Value = pb;
            RViewer.LocalReport.DataSources.Add(dsPayment);

            ReportDataSource dsBreakdown = new ReportDataSource();
            dsBreakdown.Name = "dsBreakdown";
            dsBreakdown.Value = PaymentBreak.Fees;
            RViewer.LocalReport.DataSources.Add(dsBreakdown);
        }

        public void RunReport()
        {
            RViewer.LocalReport.Refresh();
            RViewer.RefreshReport();

            System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
            ps.Landscape = false;
            ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.Custom;
            ps.PaperSize = new System.Drawing.Printing.PaperSize("User-Defined", 425, 850);
            ps.Margins.Top = 2;
            ps.Margins.Bottom = 2;
            ps.Margins.Left = 1;
            ps.Margins.Right = 1;
            RViewer.ShowBackButton = false;
            RViewer.ShowFindControls = false;
            RViewer.ShowPageNavigationControls = false;
            RViewer.ShowRefreshButton = false;
            RViewer.ShowStopButton = false;
            RViewer.SetPageSettings(ps);
        }
    }
}
