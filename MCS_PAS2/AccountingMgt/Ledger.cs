using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingMgt
{
    class Ledger:DBase_Acctng
    {
        public Ledger() { }

        public DataTable GetAllPaymentPerAccount(long idassess)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT idpayment as ID, ornumber as 'OR', amount as TOTAL, DATE(dateofpayment) as 'PAYMENT DATE' FROM payment WHERE idassessment=" + idassess + " ORDER BY dateofpayment ASC";
            DataTable table = new DataTable();
            using (MySqlDataReader dbReader = cmd.ExecuteReader())
            {
                table.Load(dbReader);
            }
            dbClose();
            return table;
        }

        public DataTable GetPaymentDetailById(long payid)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT feecode as CODE, amount as AMOUNT FROM payment_detail WHERE idpayment=" + payid;
            DataTable table = new DataTable();
            using (MySqlDataReader dbReader = cmd.ExecuteReader())
            {
                table.Load(dbReader);
            }
            dbClose();
            return table;
        }
    }

    class LedgerItem:DBase_Acctng
    {    
        public long IdPayment { get; set; }
        public DateTime PaymentDate { get; set; }
        public string OrNumber { get; set; }
        public string FeeCode { get; set; }
        public string FeeName { get; set; }      
        public double PaidAmount { get; set; }
        public double AssessmentAmount { get; set; }
        public double AssessmentTotal { get; set; }

        public LedgerItem() { }
    }

    class MonthlyDetail:DBase_Acctng
    {
        public MonthlyDetail() { }

        public DataTable GetAllPaymentsInMonth(DateTime monthyear, int paytype, MonthlyReportType monthlyReportType)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            if((paytype==1 || paytype==2) && (monthlyReportType==MonthlyReportType.HSStandard || monthlyReportType==MonthlyReportType.HSNonStandard))
                cmd.CommandText = "SELECT idpayment as ID, DATE(dateofpayment) as 'DATE', ornumber as 'OR', amount as 'TOTAL' FROM (((payment INNER JOIN assessment ON payment.idassessment=assessment.idassessment) INNER JOIN registration ON assessment.idregistration=registration.idregistration) INNER JOIN gradelevel ON registration.gradelevel=gradelevel.gradename) INNER JOIN department ON gradelevel.iddepartment=department.iddepartment WHERE department.division=1 AND MONTH(dateofpayment)=" + monthyear.Month + " AND YEAR(dateofpayment)=" + monthyear.Year + " AND paymenttype=" + paytype + " ORDER BY DATE(dateofpayment), ornumber";
            else if((paytype == 1 || paytype == 2) && (monthlyReportType == MonthlyReportType.SHSStandard || monthlyReportType == MonthlyReportType.SHSNonStandard))
                cmd.CommandText = "SELECT idpayment as ID, DATE(dateofpayment) as 'DATE', ornumber as 'OR', amount as 'TOTAL' FROM (((payment INNER JOIN assessment ON payment.idassessment=assessment.idassessment) INNER JOIN registration ON assessment.idregistration=registration.idregistration) INNER JOIN gradelevel ON registration.gradelevel=gradelevel.gradename) INNER JOIN department ON gradelevel.iddepartment=department.iddepartment WHERE department.division=2 AND MONTH(dateofpayment)=" + monthyear.Month + " AND YEAR(dateofpayment)=" + monthyear.Year + " AND paymenttype=" + paytype + " ORDER BY DATE(dateofpayment), ornumber";
            else if (paytype == 1 || paytype == 2 && (monthlyReportType == MonthlyReportType.AllStandard || monthlyReportType == MonthlyReportType.AllNonStandard))
                cmd.CommandText = "SELECT idpayment as ID, DATE(dateofpayment) as 'DATE', ornumber as 'OR', amount as 'TOTAL' FROM payment WHERE MONTH(dateofpayment)=" + monthyear.Month + " AND YEAR(dateofpayment)=" + monthyear.Year + " AND paymenttype=" + paytype + " ORDER BY DATE(dateofpayment), ornumber";
            else
            {
                if(monthlyReportType==MonthlyReportType.HSOtherType)
                    cmd.CommandText = "SELECT idpayment as ID, DATE(dateofpayment) as 'DATE', ornumber as 'OR', amount as 'TOTAL' FROM payment WHERE payment.division=1 AND MONTH(dateofpayment)=" + monthyear.Month + " AND YEAR(dateofpayment)=" + monthyear.Year + " AND (paymenttype=3 OR paymenttype=4 OR paymenttype=5) ORDER BY DATE(dateofpayment), ornumber";
                else if(monthlyReportType==MonthlyReportType.SHSOtherType)
                    cmd.CommandText = "SELECT idpayment as ID, DATE(dateofpayment) as 'DATE', ornumber as 'OR', amount as 'TOTAL' FROM payment WHERE payment.division=2 AND MONTH(dateofpayment)=" + monthyear.Month + " AND YEAR(dateofpayment)=" + monthyear.Year + " AND (paymenttype=3 OR paymenttype=4 OR paymenttype=5) ORDER BY DATE(dateofpayment), ornumber";
                else
                    cmd.CommandText = "SELECT idpayment as ID, DATE(dateofpayment) as 'DATE', ornumber as 'OR', amount as 'TOTAL' FROM payment WHERE MONTH(dateofpayment)=" + monthyear.Month + " AND YEAR(dateofpayment)=" + monthyear.Year + " AND (paymenttype=3 OR paymenttype=4 OR paymenttype=5) ORDER BY DATE(dateofpayment), ornumber";
            }
                
            DataTable table = new DataTable();
            using (MySqlDataReader dbReader = cmd.ExecuteReader())
            {
                table.Load(dbReader);
            }
            dbClose();
            return table;
        }

        public DataTable GetPaymentDetailById(long payid)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT feecode as CODE, amount as AMOUNT FROM payment_detail WHERE idpayment=" + payid;
            DataTable table = new DataTable();
            using (MySqlDataReader dbReader = cmd.ExecuteReader())
            {
                table.Load(dbReader);
            }
            dbClose();
            return table;
        }

        public DataTable GetAllNonStandardFees()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT feecode as CODE, feename as FEE, shortname as 'SHORT NAME', feetype as 'FEE TYPE' FROM fee WHERE feetype=3 ORDER BY feetype, feename";

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetAllOtherFees()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT feecode as CODE, feename as FEE, shortname as 'SHORT NAME', feetype as 'FEE TYPE' FROM fee WHERE (feetype=4 OR feetype=5 OR feetype=6) ORDER BY feetype, feename";

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }
    }

}
