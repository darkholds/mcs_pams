using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Payment
{
    public partial class frmReport : Form
    {
        //int ReportType = 0;
        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            try
            {
                Receipt r = new Receipt(rvReport, "Payment.rptReceipt.rdlc", ((frmPayment)Owner).student_number, ((frmPayment)Owner).id_assessment, ((frmPayment)Owner).payment);
                r.RunReport();
                //ReportType = (MdiParent as frmMDI).ReportTYpe;
                //if (ReportType == 1)
                //{
                //    SoaReport r = new SoaReport(rvReport, "AccountingMgt.rptSOA.rdlc", (MdiParent as frmMDI).AssessedStudent.Id, (MdiParent as frmMDI).AssessedSem, (MdiParent as frmMDI).AssessedSY);
                //    r.RunReport();
                //}             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
