using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;

namespace AccountingMgt
{
    public enum ReportTypes:int { OR=1, OFOR, PTAOR, DailyCollection,SOA, Ledger, MonthlyCollectionDetail}
    public enum MonthlyReportType { HSStandard, HSNonStandard, SHSStandard, SHSNonStandard, HSOtherType, SHSOtherType, AllOtherType, AllStandard, AllNonStandard}
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
            RViewer.ShowBackButton = false;
            RViewer.ShowFindControls = false;
            RViewer.ShowPageNavigationControls = false;
            RViewer.ShowRefreshButton = false;
            RViewer.ShowStopButton = false;
        }
    }

    class Receipt : Report
    {
        public Receipt(ReportViewer rv, string report, Student student, string or_number):base(rv,report)
        {
            //Stud = new Student().GetStudent(sn);
            Registration Reg = new Registration().GetRegistration(student.Id);
            Payment PayDetail = new Payment().GetPaymentByORNumber(or_number);

            List<Payment> pb = new List<Payment>();
            pb.Add(PayDetail);

            List<Student> stud = new List<Student>();
            stud.Add(student);

            List<Registration> reg = new List<Registration>();
            reg.Add(Reg);

            ReportDataSource dsReg = new ReportDataSource();
            dsReg.Name = "dsRegistration";
            dsReg.Value = reg;
            RViewer.LocalReport.DataSources.Add(dsReg);

            ReportDataSource dsStud = new ReportDataSource();
            dsStud.Name = "dsStudent";
            dsStud.Value = stud;
            RViewer.LocalReport.DataSources.Add(dsStud);

            ReportDataSource dsPayment = new ReportDataSource();
            dsPayment.Name = "dsPayment";
            dsPayment.Value = pb;
            RViewer.LocalReport.DataSources.Add(dsPayment);

            ReportDataSource dsDetail = new ReportDataSource();
            dsDetail.Name = "dsPayDetail";
            dsDetail.Value = PayDetail.PaymentDetail;
            RViewer.LocalReport.DataSources.Add(dsDetail);
        }

        public void RunReport()
        {
            RViewer.LocalReport.Refresh();
            RViewer.RefreshReport();

            PageSettings ps = new PageSettings();
            ps.Margins.Top = 25;
            ps.Margins.Bottom = 25;
            ps.Margins.Left = 25;
            ps.Margins.Right = 25;
            ps.Landscape = false;
            ps.PaperSize.RawKind = (int)PaperKind.Custom;
            ps.PaperSize = new PaperSize("User-Defined", 425, 550); 
            RViewer.SetPageSettings(ps);
        }

        public void PrintReport()
        {
            ReportParameter[] param = new ReportParameter[1];
            param[0] = new ReportParameter("show_item", "true");
            RViewer.LocalReport.SetParameters(param);
            RViewer.LocalReport.Refresh();
            RViewer.RefreshReport();

            PageSettings ps = new PageSettings();
            ps.Margins.Top = 25;
            ps.Margins.Bottom = 25;
            ps.Margins.Left = 25;
            ps.Margins.Right = 25;
            ps.Landscape = false;
            ps.PaperSize.RawKind = (int)PaperKind.Custom;
            ps.PaperSize = new PaperSize("User-Defined", 425, 550);
            RViewer.SetPageSettings(ps);        
        }
    }

    class OFReceipt: Receipt
    {
        public OFReceipt(ReportViewer rv, string report, Student student, string or_number):base(rv, report, student, or_number){}
        public new void RunReport()
        {
            RViewer.LocalReport.Refresh();
            RViewer.RefreshReport();

            PageSettings ps = new PageSettings();
            ps.Margins.Top = 25;
            ps.Margins.Bottom = 25;
            ps.Margins.Left = 25;
            ps.Margins.Right = 25;
            ps.Landscape = false;
            ps.PaperSize.RawKind = (int)PaperKind.Custom;
            ps.PaperSize = new PaperSize("User-Defined", 425, 650);
            RViewer.SetPageSettings(ps);
        }
        public new void PrintReport()
        {
            ReportParameter[] param = new ReportParameter[1];
            param[0] = new ReportParameter("show_item", "true");
            RViewer.LocalReport.SetParameters(param);
            RViewer.LocalReport.Refresh();
            RViewer.RefreshReport();

            PageSettings ps = new PageSettings();
            ps.Margins.Top = 25;
            ps.Margins.Bottom = 25;
            ps.Margins.Left = 25;
            ps.Margins.Right = 25;
            ps.Landscape = false;
            ps.PaperSize.RawKind = (int)PaperKind.Custom;
            ps.PaperSize = new PaperSize("User-Defined", 425, 650);
            RViewer.SetPageSettings(ps);
        }
    }
    class PTAReceipt : OFReceipt
    {
        public PTAReceipt(ReportViewer rv, string report, Student student, string or_number) : base(rv, report, student, or_number) { }
    }

    class DailySummaryDetail
    {
        public double TF_SHS { get; set; }
        public double TF_JHS { get; set; }
        public double OF_SHS { get; set; }
        public double OF_JHS { get; set; }
        public double PTA { get; set; }
        public double Alumni { get; set; }
        public double Other { get; set; }
        //public double OtherFee { get; set; }
        //public double StandardFee { get; set; }
        public string DayOfSummary { get; set; }

        public DailySummaryDetail(DateTime day)
        {
            DayOfSummary = day.ToShortDateString();
            DataTable table = new Payment().GetDailyCollectionSummary(day);
            foreach (DataRow row in table.Rows)
            {
                if (row["PARTICULARS"].ToString().Equals("TF-SHS OR#"))
                {
                    TF_SHS = Convert.ToDouble(row["AMOUNT"].ToString());
                }
                else if (row["PARTICULARS"].ToString().Equals("TF-Elem./JHS OR#"))
                {
                    TF_JHS = Convert.ToDouble(row["AMOUNT"].ToString());
                }
                else if (row["PARTICULARS"].ToString().Equals("OF-SHS OR#"))
                {
                    OF_SHS = Convert.ToDouble(row["AMOUNT"].ToString());
                }
                else if (row["PARTICULARS"].ToString().Equals("OF-Elem./JHS OR#"))
                {
                    OF_JHS = Convert.ToDouble(row["AMOUNT"].ToString());
                }
                else if (row["PARTICULARS"].ToString().Equals("PTA OR#"))
                {
                    PTA = Convert.ToDouble(row["AMOUNT"].ToString());
                }
                else if (row["PARTICULARS"].ToString().Equals("Alumni OR#"))
                {
                    Alumni = Convert.ToDouble(row["AMOUNT"].ToString());
                }
                else if (row["PARTICULARS"].ToString().Equals("Other OR#"))
                {
                    Other = Convert.ToDouble(row["AMOUNT"].ToString());
                }
            }
        }
    }

    class DailySummaryReport: Report
    {
        public DailySummaryReport(ReportViewer rv, string report, DateTime day):base(rv, report)
        {
            List<DailySummaryDetail> daily = new List<DailySummaryDetail>();
            daily.Add(new DailySummaryDetail(day));

            ReportDataSource dsDaily = new ReportDataSource();
            dsDaily.Name = "dsDaily";
            dsDaily.Value = daily;
            RViewer.LocalReport.DataSources.Add(dsDaily);
        }

        public new void RunReport()
        {
            RViewer.LocalReport.Refresh();
            RViewer.RefreshReport();

            PageSettings ps = new PageSettings();
            ps.Margins.Top = 25;
            ps.Margins.Bottom = 25;
            ps.Margins.Left = 25;
            ps.Margins.Right = 25;
            ps.Landscape = false;
            ps.PaperSize.RawKind = (int)PaperKind.Custom;
            ps.PaperSize = new PaperSize("User-Defined", 425, 650);
            RViewer.SetPageSettings(ps);
        }
    }

    class SoaReport : Report
    {
        public SoaReport(ReportViewer rv, string report, Student student, User user, List<PaymentDetail> soadetail):base(rv,report)
        {
            Registration Reg = new Registration().GetRegistration(student.Id);
            Assessment Assess = new Assessment().GetAssessment(Reg.Id);

            DataTable table = new DataTable(); 
            table = new Payment().GetAllPaymentPerAssessment(student.Id, Assess.Id);

            List<Payment> Payments = new List<Payment>();
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    Payment payment = new Payment();
                    payment.ORNumber = row["OR NUMBER"].ToString();
                    payment.Amount = Convert.ToDouble(row["AMOUNT"].ToString());
                    payment.PaymentDate = Convert.ToDateTime(row["PAYMENT DATE"]).ToShortDateString();

                    Payments.Add(payment);
                }
            }
            else
                Payments.Add(new Payment());

            List<Student> studList = new List<Student>();
            studList.Add(student);

            List<Assessment> assList = new List<Assessment>();
            assList.Add(Assess);

            List<Registration> regList = new List<Registration>();
            regList.Add(Reg);

            List<User> userList = new List<User>();
            userList.Add(user);

            ReportDataSource dsStud = new ReportDataSource();
            dsStud.Name = "dsStudent";
            dsStud.Value = studList;
            RViewer.LocalReport.DataSources.Add(dsStud);

            ReportDataSource dsAssess = new ReportDataSource();
            dsAssess.Name = "dsAssess";
            dsAssess.Value = assList;
            RViewer.LocalReport.DataSources.Add(dsAssess);

            ReportDataSource dsReg = new ReportDataSource();
            dsReg.Name = "dsRegistration";
            dsReg.Value = regList;
            RViewer.LocalReport.DataSources.Add(dsReg);

            ReportDataSource dsPay = new ReportDataSource();
            dsPay.Name = "dsPayment";
            dsPay.Value = Payments;
            RViewer.LocalReport.DataSources.Add(dsPay);

            ReportDataSource dsUser = new ReportDataSource();
            dsUser.Name = "dsUser";
            dsUser.Value = userList;
            RViewer.LocalReport.DataSources.Add(dsUser);

            ReportDataSource dsPayDet = new ReportDataSource();
            dsPayDet.Name = "dsPayDetail";
            dsPayDet.Value = soadetail;
            RViewer.LocalReport.DataSources.Add(dsPayDet);
        }

        public void RunReport()
        {
            RViewer.LocalReport.Refresh();
            RViewer.RefreshReport();

            PageSettings ps = new PageSettings();
            ps.Landscape = false;
            ps.PaperSize.RawKind = (int)PaperKind.Custom;
            ps.PaperSize = new PaperSize("User-Defined", 850, 1100);
            ps.Margins.Top = 25;
            ps.Margins.Bottom = 25;
            ps.Margins.Left = 25;
            ps.Margins.Right = 25;
            RViewer.SetPageSettings(ps);
        }
    }

    class LedgerReport: Report
    {
        public LedgerReport(ReportViewer rv, string report, Student student) : base(rv, report)
        {
            Registration Reg = new Registration().GetRegistration(student.Id);
            Assessment Assess = new Assessment().GetAssessment(Reg.Id);

            DataTable payments = new Ledger().GetAllPaymentPerAccount(Assess.Id);
            DataTable payDetail = new DataTable();
            
            List<LedgerItem> ledger = new List<LedgerItem>();
            foreach (DataRow prow in payments.Rows)
            {
                long id = Convert.ToInt64(prow["ID"]);
                payDetail = new Ledger().GetPaymentDetailById(id);
                List<PaymentDetail> paylist = new List<PaymentDetail>();
                foreach (DataRow plist in payDetail.Rows)
                {
                    PaymentDetail pd = new PaymentDetail();
                    pd.Code = plist["CODE"].ToString();
                    pd.Amount = Convert.ToDouble(plist["AMOUNT"]);
                    paylist.Add(pd);
                }

                foreach (DataRow row in Assess.AssessmentDetail.Rows)
                {
                    LedgerItem ledgerItem = new LedgerItem();
                    ledgerItem.FeeCode = row["CODE"].ToString();
                    ledgerItem.FeeName = row["FEE"].ToString();
                    ledgerItem.OrNumber = prow["OR"].ToString();
                    ledgerItem.PaymentDate = Convert.ToDateTime(prow["PAYMENT DATE"]);
                    ledgerItem.AssessmentAmount = Convert.ToDouble(row["AMOUNT"]);
                    ledgerItem.AssessmentTotal = Assess.Total;

                    foreach(PaymentDetail det in paylist)
                    {
                        if (ledgerItem.FeeCode.Equals(det.Code))
                        {
                            ledgerItem.PaidAmount = det.Amount;
                            break;
                        }
                        else
                            ledgerItem.PaidAmount = 0;
                    }
                    ledger.Add(ledgerItem);
                }
            }


            List<PaymentDetail> assList = new List<PaymentDetail>();
            foreach (DataRow row in Assess.AssessmentDetail.Rows)
            {
                PaymentDetail pd = new PaymentDetail();
                pd.Code = row["CODE"].ToString();
                pd.Name = row["FEE"].ToString();
                pd.Amount = Convert.ToDouble(row["AMOUNT"]);
                assList.Add(pd);
            }

            List<Student> studList = new List<Student>();
            studList.Add(student);
            ReportDataSource dsStud = new ReportDataSource();
            dsStud.Name = "dsStudent";
            dsStud.Value = studList;
            RViewer.LocalReport.DataSources.Add(dsStud);

            ReportDataSource dsLedger = new ReportDataSource();
            dsLedger.Name = "dsPayment";
            dsLedger.Value = ledger;
            RViewer.LocalReport.DataSources.Add(dsLedger);
        }

        public void RunReport()
        {
            PageSettings ps = new PageSettings();
            ps.Landscape = true;
            ps.PaperSize.RawKind = (int)PaperKind.Custom;
            ps.PaperSize = new PaperSize("User-Defined", 850, 1400);
            ps.Margins.Top = 25;
            ps.Margins.Bottom = 25;
            ps.Margins.Left = 25;
            ps.Margins.Right = 25;
            RViewer.SetPageSettings(ps);

            RViewer.LocalReport.Refresh();
            RViewer.RefreshReport();
        }
    }

    class MonthlyDetailReport: Report
    {
        public MonthlyDetailReport(ReportViewer rv, string report, DateTime dateTime, int paytype, MonthlyReportType monthlyReportType) : base(rv, report)
        {         
            List<LedgerItem> ledgerItems = new List<LedgerItem>();
            DataTable fees;
            if (paytype == 1)
                fees = new Fee().GetAllStandardFees();
            else if (paytype == 2)
                fees = new MonthlyDetail().GetAllNonStandardFees();
            else
                fees = new MonthlyDetail().GetAllOtherFees();

            DataTable payments = new MonthlyDetail().GetAllPaymentsInMonth(dateTime, paytype, monthlyReportType);
            DataTable payDetails = new DataTable();

            List<LedgerItem> ledger = new List<LedgerItem>();
            foreach (DataRow prow in payments.Rows)
            {
                long id = Convert.ToInt64(prow["ID"]);
                payDetails = new MonthlyDetail().GetPaymentDetailById(id);
                List<PaymentDetail> paylist = new List<PaymentDetail>();
                foreach (DataRow plist in payDetails.Rows)
                {
                    PaymentDetail pd = new PaymentDetail();
                    pd.Code = plist["CODE"].ToString();
                    pd.Amount = Convert.ToDouble(plist["AMOUNT"]);
                    paylist.Add(pd);
                }

                //check if item is present in pay type, this is good for fees that have categories transfered
                foreach(PaymentDetail pitem in paylist)
                {
                    bool present = false;
                    foreach(DataRow row in fees.Rows)
                    {
                        if (row["CODE"].ToString().Equals(pitem.Code))
                        {
                            present = true;
                            break;
                        }                 
                    }

                    if (!present)
                    {
                        DataRow dr = fees.NewRow();
                        DataTable dt = new DataTable();
                        dt = new Fee().GetFeeByCode(pitem.Code);
                        foreach(DataRow drow in dt.Rows)
                        {
                            dr[0] = drow["feecode"].ToString();                          
                            dr[1] = drow["feename"].ToString();
                            dr[2] = drow["shortname"].ToString();
                            dr[3] = Convert.ToInt16(drow["feetype"]);
                        }
                       
                        fees.Rows.Add(dr);
                    }
                }

                foreach(DataRow row in fees.Rows)
                {
                    LedgerItem ledgerItem = new LedgerItem();
                    ledgerItem.FeeCode = row["CODE"].ToString();
                    ledgerItem.FeeName = row["SHORT NAME"].ToString();
                    ledgerItem.OrNumber = prow["OR"].ToString();
                    ledgerItem.PaymentDate = Convert.ToDateTime(prow["DATE"]);
                    //ledgerItem.AssessmentAmount = Convert.ToDouble(row["AMOUNT"]);
                    //ledgerItem.AssessmentTotal = Assess.Total;

                    foreach (PaymentDetail det in paylist)
                    {
                        if (ledgerItem.FeeCode.Equals(det.Code))
                        {
                            ledgerItem.PaidAmount = det.Amount;
                            break;
                        }
                        else
                            ledgerItem.PaidAmount = 0;
                    }

                    ledger.Add(ledgerItem);
                }
            }

            MonthlyReportTitle title;
            if (monthlyReportType == MonthlyReportType.HSStandard)
                title = new MonthlyReportTitle("STANDARD SCHOOL FEES: JHS, Elem, Pre-school");
            else if (monthlyReportType == MonthlyReportType.HSNonStandard)
                title = new MonthlyReportTitle("NON-STANDARD SCHOOL FEES: JHS, Elem, Pre-school");
            else if (monthlyReportType == MonthlyReportType.SHSStandard)
                title = new MonthlyReportTitle("STANDARD SCHOOL FEES: Senior High School");
            else if (monthlyReportType == MonthlyReportType.SHSNonStandard)
                title = new MonthlyReportTitle("NON-STANDARD SCHOOL FEES: Senior High School");
            else if (monthlyReportType == MonthlyReportType.AllStandard)
                title = new MonthlyReportTitle("STANDARD SCHOOL FEES: All Department");
            else if (monthlyReportType == MonthlyReportType.AllNonStandard)
                title = new MonthlyReportTitle("NON-STANDARD SCHOOL FEES: All Department");
            else if(monthlyReportType==MonthlyReportType.HSOtherType)
                title = new MonthlyReportTitle("OTHER SCHOOL FEES: JHS, Elem, Pre-school");
            else if (monthlyReportType == MonthlyReportType.SHSOtherType)
                title = new MonthlyReportTitle("OTHER SCHOOL FEES: Senior High School");
            else
                title = new MonthlyReportTitle("OTHER SCHOOL FEES: All Department");

            List<MonthlyReportTitle> titles = new List<MonthlyReportTitle>();
            titles.Add(title);

            ReportDataSource dsReport = new ReportDataSource();
            dsReport.Name = "dsPayment";
            dsReport.Value = ledger;
            RViewer.LocalReport.DataSources.Add(dsReport);

            ReportDataSource dsTitle = new ReportDataSource();
            dsTitle.Name = "dsTitle";
            dsTitle.Value = titles;
            RViewer.LocalReport.DataSources.Add(dsTitle);
        }

        public void RunReport()
        {
            RViewer.LocalReport.Refresh();
            RViewer.RefreshReport();

            PageSettings ps = new PageSettings();
            ps.Landscape = true;
            ps.PaperSize.RawKind = (int)PaperKind.Custom;
            ps.PaperSize = new PaperSize("User-Defined", 850, 1400);
            ps.Margins.Top = 25;
            ps.Margins.Bottom = 25;
            ps.Margins.Left = 25;
            ps.Margins.Right = 25;
            RViewer.SetPageSettings(ps);
        }
    }

    class MonthlyReportTitle
    {
        public string Title { get; set; }
        public MonthlyReportTitle(string title) { Title = title; }
    }

    class AssessmentReport : Report
    {
        public List<Fee> AssessmentFees { get; set; }
        public Student Stud { get; set; }

        public AssessmentReport(ReportViewer rv, string report, string sn, string sem, string sy)
        {
            RViewer = rv;
            EmbeddedResource = report;
            RViewer.LocalReport.DataSources.Clear();
            RViewer.LocalReport.ReportEmbeddedResource = report;

            //Assessment assess = new Assessment();
            //assess.GetAssessment(sn, sem, sy);
            Stud = new Student();
            Stud.SearchStudentsById(sn);

            AssessmentFees = new List<Fee>();
            //AssessmentFees = assess.GetStudentAssessmentDetailsSimple(assess.Id);

            List<Student> student = new List<Student>();
            student.Add(Stud);

            //List<Assessment> Assess = new List<Assessment>();
            //Assess.Add(assess);

            ReportDataSource dsAssess = new ReportDataSource();
            dsAssess.Name = "dsAssess";
            //dsAssess.Value = Assess;
            RViewer.LocalReport.DataSources.Add(dsAssess);

            ReportDataSource dsStud = new ReportDataSource();
            dsStud.Name = "dsStud";
            dsStud.Value = student;
            RViewer.LocalReport.DataSources.Add(dsStud);

            ReportDataSource dsFees = new ReportDataSource();
            dsFees.Name = "dsFees";
            dsFees.Value = AssessmentFees;
            RViewer.LocalReport.DataSources.Add(dsFees);
        }

        public void RunReport()
        {
            RViewer.LocalReport.Refresh();
            RViewer.RefreshReport();

            System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
            ps.Landscape = false;
            ps.PaperSize.RawKind = (int)PaperKind.Custom;
            ps.PaperSize = new PaperSize("User-Defined", 420, 550);
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
