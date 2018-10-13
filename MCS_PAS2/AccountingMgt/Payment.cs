using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace AccountingMgt
{
    class Payment: DBase_Acctng
    {
        public long IdPayment { get; set; }
        public long AssessmentId { get; set; }
        public string ORNumber { get; set; }
        public double Amount { get; set; }
        public int PaymentType { get; set; }
        public string CashierId { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentInWords { get; set; }
        public string Payer { get; set; }
        public int Division { get; set; }
        public List<PaymentDetail> PaymentDetail { get; set; }

        public Payment() {
            PaymentDetail = new List<PaymentDetail>();
        }
        public Payment(long assessid, string ornum, double amount, string cashier, int paymenttype)
        {
            PaymentDetail = new List<PaymentDetail>();
            AssessmentId = assessid;
            ORNumber = ornum;
            Amount = amount;
            CashierId = cashier;
            PaymentType = paymenttype;
        }

        public bool SavePayment()
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

                //insert payment to database
                cmd.CommandText = "INSERT INTO payment(idcashier, idassessment, ornumber, amount, paymenttype) VALUES ('" + CashierId + "'," +  AssessmentId + ", '" + ORNumber + "', " + Amount + ", "+ PaymentType + ")";
                cmd.ExecuteNonQuery();
                long lastpaymentId = cmd.LastInsertedId; //get last payment id inserted

                //get sum of items in the assessment
                cmd.CommandText = "SELECT SUM(amount) FROM assessment_detail WHERE idassessment=" + AssessmentId;
                object res = cmd.ExecuteScalar();
                double total = 0;
                Double.TryParse(res.ToString(), out total);

                //get sum of payment on same assessment
                cmd.CommandText = "SELECT SUM(amount) FROM payment WHERE idassessment=" + AssessmentId;
                res = cmd.ExecuteScalar();
                double paid;
                double balance;
                if (Double.TryParse(res.ToString(), out paid))
                {
                    balance = total - paid;
                }
                else
                    balance = total;

                //update assessment details for balance and total
                cmd.CommandText = "UPDATE assessment SET total=" + total + ", balance=" + balance + " WHERE idassessment=" + AssessmentId;
                cmd.ExecuteNonQuery();

                //insert payment details        
                foreach (PaymentDetail fee in PaymentDetail)
                {
                    cmd.CommandText = "INSERT INTO payment_detail(feecode,idpayment,amount) VALUES('" + fee.Code + "'," + lastpaymentId + "," + fee.Amount + ")";
                    cmd.ExecuteNonQuery();
                }   

                //change pending to enrolled on first payment
                cmd.CommandText = "SELECT registration.idregistration as regid, status FROM registration INNER JOIN assessment ON registration.idregistration=assessment.idregistration WHERE idassessment=" + AssessmentId + " AND status='PENDING'";
                string status=string.Empty;
                long idreg=0;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        status = reader["status"].ToString();
                        idreg = Convert.ToUInt32(reader["regid"].ToString());
                    }
                }

                if (status.Equals("PENDING"))
                {
                    cmd.CommandText = "UPDATE registration SET status='ENROLLED' WHERE idregistration=" + idreg;
                    cmd.ExecuteNonQuery();
                }            
                
                trans.Commit();
                dbClose();
                return true;
            }
            catch (Exception)
            {
                trans.Rollback();
                dbClose();
                return false;
            }
        }

        public bool SavePaymentEdited(string ORNumber, DateTime dateTime, long idpayment)
        {
            try
            {
                if (DBCon.State == ConnectionState.Open)
                    dbClose();

                dbOpen();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DBCon;
                cmd.Parameters.Add("@paydate", MySqlDbType.DateTime).Value=dateTime;
                cmd.CommandText = "UPDATE payment SET ornumber='" + ORNumber + "', dateofpayment=@paydate WHERE idpayment=" + idpayment;

                cmd.ExecuteNonQuery();
                dbClose();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SavePaymentOther()
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

                //insert payment to database
                cmd.CommandText = "INSERT INTO payment(idcashier, idassessment, ornumber, amount, paymenttype, payer, payment.division) VALUES ('" + CashierId + "'," + AssessmentId + ", '" + ORNumber + "', " + Amount + ", " + PaymentType + ", '" + Payer + "', " + Division + ")";
                cmd.ExecuteNonQuery();
                long lastpaymentId = cmd.LastInsertedId; //get last payment id inserted

                //insert payment details        
                foreach (PaymentDetail fee in PaymentDetail)
                {
                    cmd.CommandText = "INSERT INTO payment_detail(feecode,idpayment,amount) VALUES('" + fee.Code + "'," + lastpaymentId + "," + fee.Amount + ")";
                    cmd.ExecuteNonQuery();
                }

                trans.Commit();
                dbClose();
                return true;
            }
            catch (Exception)
            {
                trans.Rollback();
                dbClose();
                return false;
            }
        }

        public DataTable GetAllPaymentPerAssessment(string student_number, long idassess)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT ornumber as 'OR NUMBER', payment.amount as AMOUNT, DATE(dateofpayment) as 'PAYMENT DATE', CONCAT(schoolyear, ' (', gradelevel, ')') as TERM FROM (payment INNER JOIN assessment ON payment.idassessment=assessment.idassessment) INNER JOIN registration ON assessment.idregistration=registration.idregistration WHERE idstudent='" + student_number + "' AND payment.idassessment=" + idassess + " ORDER BY dateofpayment DESC";
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetPaymentDetailByORNumber(string orNumber)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT feename as FEE, payment_detail.amount as AMOUNT FROM (payment_detail INNER JOIN fee ON payment_detail.feecode=fee.feecode) INNER JOIN payment ON payment_detail.idpayment=payment.idpayment WHERE ornumber ='" + orNumber + "'";
      
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public Payment GetPaymentByORNumber(string orNumber)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT payment.idpayment as idpay, payment.idassessment as idassess, ornumber, payment.amount as payamount, idcashier, dateofpayment," +
                "feename, payment_detail.feecode as code, payment_detail.amount as amountdetail, feetype FROM (payment INNER JOIN payment_detail ON payment.idpayment=payment_detail.idpayment) INNER JOIN fee ON payment_detail.feecode=fee.feecode WHERE ornumber ='" + orNumber + "'";

            using (MySqlDataReader dbReader = cmd.ExecuteReader())
            {
                while (dbReader.Read())
                {
                    IdPayment = Convert.ToInt64(dbReader["idpay"].ToString());
                    AssessmentId = Convert.ToInt64(dbReader["idassess"].ToString());
                    ORNumber = dbReader["ornumber"].ToString();
                    Amount = Convert.ToDouble(dbReader["payamount"].ToString());
                    CashierId = dbReader["idcashier"].ToString();
                    PaymentDate = dbReader["dateofpayment"].ToString();
                    PaymentInWords = AmountInWords(Convert.ToInt32(Amount));

                    PaymentDetail fee = new PaymentDetail();
                    fee.IdPayment = IdPayment;
                    //fee.IdAssessment = AssessmentId;
                    fee.Name = dbReader["feename"].ToString();
                    fee.Code = dbReader["code"].ToString();
                    fee.Amount = Convert.ToDouble(dbReader["amountdetail"].ToString());
                    fee.FeeType = Convert.ToInt16(dbReader["feetype"]);

                    PaymentDetail.Add(fee);
                }
            }
            dbClose();
            return this;
        }

        public DataTable GetPaymentByORNumberSimple(string ORNumber)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT idpayment as ID, ornumber as 'OR No.', dateofpayment as 'DATE', amount as AMOUNT FROM payment WHERE ornumber LIKE '" + ORNumber + "%'";

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetPaymentsInMonth(DateTime monthyear)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT idpayment as ID, ornumber as 'OR No.', dateofpayment as 'DATE', amount as AMOUNT FROM payment WHERE MONTH(dateofpayment) = " + monthyear.Month + " AND YEAR(dateofpayment)=" + monthyear.Year + " ORDER BY DATE(dateofpayment) DESC";
            
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetMonthlyCollection(DateTime monthyear)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT DATE(dateofpayment) as 'DATE', SUM(amount) as AMOUNT FROM payment WHERE MONTH(dateofpayment) = " + monthyear.Month + " AND YEAR(dateofpayment)=" + monthyear.Year + " GROUP BY DATE(dateofpayment) ASC";
            //cmd.CommandText = "SELECT DATE(dateofpayment) as 'PAYMENT DATE', SUM(payment.amount) as AMOUNT FROM ((payment INNER JOIN assessment ON payment.idassessment=assessment.idassessment) INNER JOIN registration ON assessment.idregistration=registration.idregistration) INNER JOIN student ON registration.idstudent=student.idstudent WHERE MONTH(dateofpayment) = " + monthyear.Month + " AND YEAR(dateofpayment) = " + monthyear.Year + " ORDER BY dateofpayment DESC";
           
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetMonthlyCollectionDetail(DateTime completedate)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT feecode as CODE, shortname as FEE, IFNULL(SUM(tbl1.paidamount),0) as TOTAL FROM fee LEFT JOIN (SELECT payment_detail.feecode as paidcode, payment_detail.amount as paidamount, dateofpayment FROM payment INNER JOIN payment_detail ON payment.idpayment=payment_detail.idpayment WHERE MONTH(dateofpayment) = " + completedate.Month + " AND DAY(dateofpayment)=" + completedate.Day + " AND YEAR(dateofpayment)=" + completedate.Year + ") as tbl1 ON fee.feecode=tbl1.paidcode GROUP BY feecode ORDER BY feetype, feename";

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetDailyCollection(DateTime completedate)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT ornumber as 'OR No.', IF(paymenttype=1 AND department.division=1,'TF-Elem./JHS OR', IF(paymenttype=1 AND department.division=2,'TF-SHS OR', IF(paymenttype=2 AND department.division=1,'OF-Elem./JHS OR', IF(paymenttype=2 AND department.division=2,'OF-SHS OR', IF(paymenttype=3,'PTA OR', IF(paymenttype=4,'Alumni OR','Other OR')))))) as 'OR TYPE', payment.amount as AMOUNT, IFNULL(payer,CONCAT(lastname, ', ',firstname)) as PAYER FROM ((((payment LEFT JOIN assessment ON payment.idassessment=assessment.idassessment) LEFT JOIN registration ON assessment.idregistration=registration.idregistration) LEFT JOIN student ON registration.idstudent=student.idstudent) LEFT JOIN gradelevel ON registration.gradelevel=gradelevel.gradename) LEFT JOIN department ON gradelevel.iddepartment=department.iddepartment WHERE DAY(dateofpayment) = " + completedate.Day + " AND MONTH(dateofpayment) = " + completedate.Month + " AND YEAR(dateofpayment) = " + completedate.Year + " ORDER BY dateofpayment DESC";

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetDailyCollectionSummary(DateTime completedate)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            //cmd.CommandText = "SELECT if(feetype=1,'Tuition',if(feetype=2,'Standard Fees', 'Other Fees')) as CATEGORY, SUM(payment_detail.amount) as 'TOTAL AMOUNT' FROM (payment INNER JOIN payment_detail ON payment.idpayment=payment_detail.idpayment) INNER JOIN fee ON payment_detail.feecode=fee.feecode WHERE DAY(dateofpayment) = " + completedate.Day + " AND MONTH(dateofpayment) = " + completedate.Month + " AND YEAR(dateofpayment) = " + completedate.Year + " GROUP BY feetype";
            cmd.CommandText = "SELECT IF((feetype=1 OR feetype=2) AND department.division=1,'TF-Elem./JHS OR#',IF((feetype=1 OR feetype=2) AND department.division=2,'TF-SHS OR#', IF((feetype=3 OR feetype=4) AND department.division=1,'OF-Elem./JHS OR#', IF((feetype=3 OR feetype=4) AND department.division=2,'OF-SHS OR#', IF(feetype=5, 'PTA OR#', IF(feetype=4, 'Other OR#','Alumni OR#')))))) as PARTICULARS, SUM(payment_detail.amount) as 'AMOUNT' FROM (((((payment INNER JOIN payment_detail ON payment.idpayment=payment_detail.idpayment) INNER JOIN fee ON payment_detail.feecode=fee.feecode) LEFT JOIN assessment ON payment.idassessment=assessment.idassessment) LEFT JOIN registration ON assessment.idregistration=registration.idregistration) LEFT JOIN gradelevel ON registration.gradelevel=gradelevel.gradename) LEFT JOIN department ON gradelevel.iddepartment=department.iddepartment WHERE DAY(dateofpayment) = " + completedate.Day + " AND MONTH(dateofpayment) = " + completedate.Month + " AND YEAR(dateofpayment) = " + completedate.Year + " GROUP BY PARTICULARS ORDER BY PARTICULARS DESC";
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetMonthlyCollectionSummary(DateTime monthyear)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT feecode as CODE, shortname as FEE, IFNULL(SUM(tbl1.paidamount),0) as TOTAL FROM fee LEFT JOIN (SELECT payment_detail.feecode as paidcode, payment_detail.amount as paidamount, dateofpayment FROM payment INNER JOIN payment_detail ON payment.idpayment=payment_detail.idpayment WHERE MONTH(dateofpayment) = " + monthyear.Month + " AND YEAR(dateofpayment)=" + monthyear.Year + ") as tbl1 ON fee.feecode=tbl1.paidcode GROUP BY feecode ORDER BY feetype, feename";

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public string AmountInWords(int amount)
        {
            if (amount == 0)
                return "Zero";

            if (amount < 0)
                return "Minus " + AmountInWords(Math.Abs(amount));

            string words = "";

            if ((amount / 1000000) > 0)
            {
                words += AmountInWords(amount / 1000000) + " Million ";
                amount %= 1000000;
            }

            if ((amount / 1000) > 0)
            {
                words += AmountInWords(amount / 1000) + " Thousand ";
                amount %= 1000;
            }

            if ((amount / 100) > 0)
            {
                words += AmountInWords(amount / 100) + " Hundred ";
                amount %= 100;
            }

            if (amount > 0)
            {
                //if (words != "")
                //    words += " ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (amount < 20)
                    words += unitsMap[amount];
                else
                {
                    words += tensMap[amount / 10];
                    if ((amount % 10) > 0)
                        words += " " + unitsMap[amount % 10];
                }
            }
            return words;
        }

        public DataTable GetAllPayment(string student_number)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_all_payments";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sn", student_number);
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }   

        public void GetPaymentSum(string student_number, string sem, string sy)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_payments_sum";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sn", student_number);
            cmd.Parameters.AddWithValue("@sem", sem);
            cmd.Parameters.AddWithValue("@sy", sy);
            MySqlDataReader dbReader = cmd.ExecuteReader();

            if (dbReader.HasRows)
            {
                dbReader.Read();
                if(dbReader["AMOUNT"]!=DBNull.Value)
                    Amount = Convert.ToDouble(dbReader["AMOUNT"]);
            }
           
            dbClose();
        }      
    }

    public class PaymentDetail: Fee
    {
        public long IdPayment { get; set; }
        //public long IdAssessment { get; set; }    
        public double Amount { get; set; }  

        public PaymentDetail() { }

        public PaymentDetail getPaymentDetail(long idpayment, string code)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT * FROM payment_detail INNER JOIN fee ON payment_detail.feecode=fee.feecode WHERE payment_detail.feecode='" + code + "' AND idpayment=" + idpayment;
            using (MySqlDataReader dbReader = cmd.ExecuteReader())
            {
                while (dbReader.Read())
                {
                    Code = dbReader["payment_detail.feecode"].ToString();
                    Amount = Convert.ToDouble(dbReader["amount"].ToString());
                    IdPayment = Convert.ToInt64(dbReader["idpayment"]);
                    //IdAssessment = Convert.ToInt64(dbReader["idassessment"]);
                    Name = dbReader["feename"].ToString();
                    FeeType = Convert.ToInt16(dbReader["feetype"]);
                }
            }

            dbClose();
            return this;
        }

        public DataTable GetAllPaymentDetails(string ornumber)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT feename as FEE, payment_detail.amount as AMOUNT FROM (payment INNER JOIN payment_detail ON payment.idpayment=payment_detail.idpayment) LEFT JOIN fee ON payment_detail.feecode=fee.feecode WHERE ornumber='" + ornumber + "'";
            DataTable table = new DataTable();
            using (MySqlDataReader dbReader = cmd.ExecuteReader())
            {
                table.Load(dbReader);
            }
            dbClose();
            return table;
        }
    }
}
