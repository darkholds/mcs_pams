 using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountingMgt
{
    public class AssessmentDetail: DBase_Acctng
    {
        public string FeeCode { get; set; }
        public long AssessmentId { get; set; }
        public double Amount { get; set; }

        public AssessmentDetail() { }

        public DataTable GetAssessmentDetail(long assessmentId)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT assessment_detail.feecode as CODE, feename as FEE, amount as AMOUNT, feetype FROM assessment_detail INNER JOIN fee ON assessment_detail.feecode=fee.feecode WHERE idassessment=" + assessmentId + " ORDER BY feetype,feename ASC";
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();
            return table;
        }

        public bool AddAssessmentDetail(long idassessment, string feecode, double amount)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlTransaction trans = DBCon.BeginTransaction();
            try
            {               
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DBCon;
                cmd.Transaction = trans;
                cmd.CommandText = "INSERT INTO assessment_detail(idassessment, feecode, amount) VALUES (" + idassessment + ", '" + feecode + "', " + amount + ")";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "SELECT SUM(amount) FROM assessment_detail WHERE idassessment=" + idassessment;
                object res = cmd.ExecuteScalar();
                double total = 0;
                Double.TryParse(res.ToString(), out total);

                cmd.CommandText = "SELECT SUM(amount) FROM payment WHERE idassessment=" + idassessment;
                res = cmd.ExecuteScalar();
                double paid;
                double balance;
                if (Double.TryParse(res.ToString(), out paid))
                {
                    balance = total - paid;
                }
                else
                    balance = total;

                cmd.CommandText = "UPDATE assessment SET total=" + total + ", balance=" + balance + " WHERE idassessment=" + idassessment;
                cmd.ExecuteNonQuery();
                trans.Commit();
                dbClose();
                return true;
            }
            catch (Exception)
            {
                trans.Rollback();
                return false;
            }
        }

        public bool DeleteAssessmentDetail(long idassessment, string feecode)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlTransaction trans = DBCon.BeginTransaction();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DBCon;
                cmd.Transaction = trans;
                cmd.CommandText = "DELETE FROM assessment_detail WHERE idassessment=" + idassessment + " AND feecode='" + feecode + "'";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "SELECT SUM(amount) FROM assessment_detail WHERE idassessment=" + idassessment;
                object res = cmd.ExecuteScalar();
                double total=0;
                Double.TryParse(res.ToString(), out total);
                    
                cmd.CommandText = "SELECT SUM(amount) FROM payment WHERE idassessment=" + idassessment;
                res = cmd.ExecuteScalar();
                double paid;
                double balance;
                if (Double.TryParse(res.ToString(), out paid))
                {
                    balance = total - paid;
                }
                else
                    balance = total;

                cmd.CommandText = "UPDATE assessment SET total=" + total + ", balance=" + balance + " WHERE idassessment=" + idassessment;
                cmd.ExecuteNonQuery();
                trans.Commit();
                dbClose();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                trans.Rollback();
                return false;
            }          
        }

        public DataTable GetBalancePerAssessmentItem(long idassess, int feetype)
        {
            //feetype: 0=tuition and standard, 1= non-standard and other fees
            if (DBCon.State == ConnectionState.Open)
                dbClose();
            string query;

            if (feetype == 0) //tuition and standard fee
            {
                query= "SELECT assessment_detail.feecode as CODE, feename as FEE, assessment_detail.amount - IFNULL(paidtable.paidtotal, 0) as BALANCE FROM(assessment_detail INNER JOIN fee ON assessment_detail.feecode = fee.feecode) LEFT JOIN(SELECT payment_detail.feecode, SUM(payment_detail.amount) as paidtotal FROM payment_detail INNER JOIN payment ON payment_detail.idpayment=payment.idpayment WHERE payment.idassessment =" + idassess + " GROUP BY feecode) as paidtable ON assessment_detail.feecode = paidtable.feecode WHERE assessment_detail.idassessment = " + idassess + " AND (feetype=1 OR feetype=2) HAVING BALANCE>0";
            }
            else if(feetype == 1) //Non-standard and Other fee
            {
                query= "SELECT assessment_detail.feecode as CODE, feename as FEE, assessment_detail.amount - IFNULL(paidtable.paidtotal, 0) as BALANCE FROM(assessment_detail INNER JOIN fee ON assessment_detail.feecode = fee.feecode) LEFT JOIN(SELECT payment_detail.feecode, SUM(payment_detail.amount) as paidtotal FROM payment_detail INNER JOIN payment ON payment_detail.idpayment=payment.idpayment WHERE payment.idassessment =" + idassess + " GROUP BY feecode) as paidtable ON assessment_detail.feecode = paidtable.feecode WHERE assessment_detail.idassessment = " + idassess + " AND (feetype=3 OR feetype=4) HAVING BALANCE>0";
            }
            else if (feetype == 2) //PTA
            {
                query = "SELECT assessment_detail.feecode as CODE, feename as FEE, assessment_detail.amount - IFNULL(paidtable.paidtotal, 0) as BALANCE FROM(assessment_detail INNER JOIN fee ON assessment_detail.feecode = fee.feecode) LEFT JOIN(SELECT payment_detail.feecode, SUM(payment_detail.amount) as paidtotal FROM payment_detail INNER JOIN payment ON payment_detail.idpayment=payment.idpayment WHERE payment.idassessment =" + idassess + " GROUP BY feecode) as paidtable ON assessment_detail.feecode = paidtable.feecode WHERE assessment_detail.idassessment = " + idassess + " AND feetype=5 HAVING BALANCE>0";
            }
            else if (feetype == 3) //Alumni
            {
                query = "SELECT assessment_detail.feecode as CODE, feename as FEE, assessment_detail.amount - IFNULL(paidtable.paidtotal, 0) as BALANCE FROM(assessment_detail INNER JOIN fee ON assessment_detail.feecode = fee.feecode) LEFT JOIN(SELECT payment_detail.feecode, SUM(payment_detail.amount) as paidtotal FROM payment_detail INNER JOIN payment ON payment_detail.idpayment=payment.idpayment WHERE payment.idassessment =" + idassess + " GROUP BY feecode) as paidtable ON assessment_detail.feecode = paidtable.feecode WHERE assessment_detail.idassessment = " + idassess + " AND feetype=6 HAVING BALANCE>0";
            }
            else //All
            {
                query = "SELECT assessment_detail.feecode as CODE, feename as FEE, assessment_detail.amount - IFNULL(paidtable.paidtotal, 0) as BALANCE FROM(assessment_detail INNER JOIN fee ON assessment_detail.feecode = fee.feecode) LEFT JOIN(SELECT payment_detail.feecode, SUM(payment_detail.amount) as paidtotal FROM payment_detail INNER JOIN payment ON payment_detail.idpayment=payment.idpayment WHERE payment.idassessment =" + idassess + " GROUP BY feecode) as paidtable ON assessment_detail.feecode = paidtable.feecode WHERE assessment_detail.idassessment = " + idassess + " HAVING BALANCE>0";
            }

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = cmd.CommandText = query;
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();
            return table;
        }

        public bool UpdateTuition(long idassessment, double amount, double discount,int discounttype)
        {//0=discount, 1= subsidy
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlTransaction trans = DBCon.BeginTransaction();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DBCon;
                cmd.Transaction = trans;

                bool hasTuition = false;
                string feecode="";
                cmd.CommandText = "SELECT feetype, assessment_detail.feecode as code, amount FROM assessment_detail INNER JOIN fee ON assessment_detail.feecode=fee.feecode WHERE feetype=1 AND idassessment=" + idassessment;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        feecode = reader["code"].ToString();
                        hasTuition = true;
                    }
                }

                if (hasTuition)
                {
                    cmd.CommandText = "UPDATE assessment_detail SET amount=" + amount + " WHERE idassessment=" + idassessment + " AND feecode='" + feecode + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT SUM(amount) FROM assessment_detail WHERE idassessment=" + idassessment;
                    object res = cmd.ExecuteScalar();
                    double total = 0;
                    Double.TryParse(res.ToString(), out total);

                    cmd.CommandText = "SELECT SUM(amount) FROM payment WHERE idassessment=" + idassessment;
                    res = cmd.ExecuteScalar();
                    double paid;
                    double balance;
                    if (Double.TryParse(res.ToString(), out paid))
                    {
                        balance = total - paid;
                    }
                    else
                        balance = total;

                    if(discounttype==0)
                        cmd.CommandText = "UPDATE assessment SET total=" + total + ", balance=" + balance + ", discount=" + discount  + "  WHERE idassessment=" + idassessment;
                    else
                        cmd.CommandText = "UPDATE assessment SET total=" + total + ", balance=" + balance + ", subsidy=" + discount + "  WHERE idassessment=" + idassessment;
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                    dbClose();
                    return true;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                trans.Rollback();
                return false;
            }
        }
    }

    class Assessment : DBase_Acctng
    {
        public long Id { get; set; }
        public long RegistrationId { get; set; }
        public double Total { get; set; }
        public double Balance { get; set; }
        public double Discount { get; set; }
        public double Subsidy { get; set; }
        public DataTable AssessmentDetail { get; set; }

        public Assessment() { }

        public DataTable GetActiveAssessment(string student_number)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT assessment.idassessment as 'ASSESSMENT No.', CONCAT('Tuition & Other Fees (', schoolyear, ') - ', gradelevel) as FEES, total as 'ASSESSMENT AMOUNT', IFNULL(SUM(amount), 0) as 'AMOUNT PAID', balance as BALANCE FROM (assessment INNER JOIN registration ON assessment.idregistration=registration.idregistration) LEFT JOIN payment ON assessment.idassessment=payment.idassessment WHERE (status='ENROLLED' OR status='PENDING') AND idstudent ='" + student_number + "' GROUP BY assessment.idassessment ORDER BY schoolyear ASC";
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public Assessment GetAssessment(long regId)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT * FROM assessment WHERE idregistration=" + regId;
            MySqlDataReader dbReader = cmd.ExecuteReader();

            if (dbReader.HasRows)
            {
                dbReader.Read();
                Id = Convert.ToInt64(dbReader["idassessment"].ToString());
                RegistrationId = Convert.ToInt64(dbReader["idregistration"].ToString());
                Total = Convert.ToDouble(dbReader["total"].ToString());
                Balance = Convert.ToDouble(dbReader["balance"].ToString());
                Discount= Convert.ToDouble(dbReader["discount"].ToString());
                Subsidy = Convert.ToDouble(dbReader["subsidy"].ToString());
                AssessmentDetail = new AssessmentDetail().GetAssessmentDetail(Id);
                dbClose();
                return this;
            }
            else
            {
                dbClose();
                return null;
            }       
        }

        public DataTable GetAssessmentsWithBalance(string idstudent)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT assessment.idassessment, CONCAT('Tuition and Other Fee (', schoolyear, ') - ', gradelevel) as FEE FROM assessment INNER JOIN registration ON assessment.idregistration=registration.idregistration WHERE balance>0 AND idstudent=" + idstudent;
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();
            return table;
        }

        public void CreateBlankAssessmentFromRegistration(long regid)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "INSERT INTO assessment(idregistration, total, balance) VALUES (" + regid + ",0,0)";
            cmd.ExecuteNonQuery();
            dbClose();
        }

        public DataTable GetTotalAssessmentSummary()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT schoolyear as SY, SUM(total) as 'ASSESSMENT TOTAL', SUM(balance) as COLLECTIBLES, SUM(total) - SUM(balance) as COLLECTION FROM assessment INNER JOIN registration ON assessment.idregistration=registration.idregistration GROUP BY SY ORDER BY SY DESC";
   
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);

            return table;
        }

        public DataTable GetTotalAssessmentSummaryByGradeLevel(string sy)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT schoolyear as SY, gradelevel as 'GRADE LEVEL', SUM(total) as 'ASSESSMENT TOTAL', SUM(balance) as COLLECTIBLES, SUM(total)-SUM(balance) as COLLECTION FROM assessment INNER JOIN registration ON assessment.idregistration=registration.idregistration WHERE schoolyear='" + sy + "' GROUP BY gradelevel";
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);

            return table;
        }

        public DataTable GetTotalCollectionsSummary()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            //cmd.CommandText = "SELECT fee.feecode as CODE, feename as FEE, SUM(amount) as 'TOTAL COLLECTIONS' FROM fee LEFT JOIN payment_detail ON fee.feecode=payment_detail.feecode GROUP BY fee.feecode ORDER BY fee.feecode ASC";
            cmd.CommandText = "SELECT fee.feecode as CODE, feename as FEE, IFNULL(subamount,0) as 'TOTAL COLLECTIONS' FROM fee LEFT JOIN (SELECT payment_detail.feecode as detailcode, SUM(payment_detail.amount)-IFNULL(itemamount,0) as subamount FROM payment_detail LEFT JOIN (SELECT disbursement_detail.feecode, SUM(disbursement_detail.amount) as itemamount FROM disbursement INNER JOIN disbursement_detail ON disbursement.iddisbursement=disbursement_detail.iddisbursement GROUP BY feecode) as tbl1 ON payment_detail.feecode=tbl1.feecode GROUP BY payment_detail.feecode) as tbl2 ON fee.feecode=tbl2.detailcode GROUP BY fee.feecode ORDER BY fee.feetype, fee.feename";
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);

            return table;
        }

        public DataTable GetTotalCollectionsSummaryBySY(string sy)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT fee.feecode as CODE, feename as FEE, SUM(payment_detail.amount) as 'TOTAL COLLECTIONS' FROM (((assessment INNER JOIN registration ON assessment.idregistration=registration.idregistration) INNER JOIN payment ON assessment.idassessment=payment.idassessment) INNER JOIN payment_detail ON payment.idpayment=payment_detail.idpayment) INNER JOIN fee ON payment_detail.feecode=fee.feecode WHERE schoolyear='" + sy +"' GROUP BY fee.feecode ORDER BY fee.feetype, fee.feename ASC";
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);

            return table;
        }
    }
}
