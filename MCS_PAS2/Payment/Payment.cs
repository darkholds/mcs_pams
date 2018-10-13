using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payment
{
    public class Payment: DBase_Acctng
    {
        public int AssessmentId { get; set; }
        public string ORNumber { get; set; }
        public double Amount { get; set; }
        public string CashierId { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentInWords { get; set; }

        public Payment() { }
        public Payment(int assessid, string ornum, double amount, string cashier)
        {
            AssessmentId = assessid;
            ORNumber = ornum;
            Amount = amount;
            CashierId = cashier;
        }

        public void SavePayment()
        {
            //if (DBCon.State == ConnectionState.Open)
            //    dbClose();

            //dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "save_payment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@assessid", AssessmentId);
            cmd.Parameters.AddWithValue("@ornum", ORNumber);
            cmd.Parameters.AddWithValue("@amount", Amount);
            cmd.Parameters.AddWithValue("@cashierid", CashierId);
            cmd.ExecuteNonQuery();
            //dbClose();
        }

        public DataTable GetAllPayment(string student_number, int assessid)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_payments_per_assessment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sn", student_number);
            cmd.Parameters.AddWithValue("@idassess", assessid);
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetMonthlyPayment(DateTime monthyear)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_payments_by_month";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@my", monthyear);
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
                if (dbReader["AMOUNT"] != DBNull.Value)
                    Amount = Convert.ToDouble(dbReader["AMOUNT"]);
            }

            dbClose();
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
    }

    public class PaymentBreakdown: Payment
    {
        public List<Fee> Fees { get; set; }

        public PaymentBreakdown()
        {
            Fees = new List<Fee>();
        }

        public bool SavePaymentBreakdown()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();
            //bool b = false;
            dbOpen();
            MySqlTransaction trans = DBCon.BeginTransaction();
            try
            { 
                foreach(Fee f in Fees)
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = DBCon;
                    cmd.CommandText = "UPDATE assessment_detail SET amount_paid=amount_paid+" + f.Paid + " WHERE idfee=" + f.Id + " AND idassessment=" + AssessmentId;
                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();
                }

                SavePayment();
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
    }
}
