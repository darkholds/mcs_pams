using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingMgt
{
    class Account : DBase_Acctng
    {
        public Registration StudentRegistration { get; set; }
        public Assessment StudentAssessment { get; set; }
        public DataTable StudentPayment { get; set; }

        public Account() {
            StudentPayment = new DataTable();
        }

        public void GetAccountByRegistrationId(long regid)
        {
            StudentRegistration = new Registration().GetRegistration(regid);
            StudentAssessment = new Assessment().GetAssessment(regid);
            StudentPayment = new Payment().GetAllPaymentPerAssessment(StudentRegistration.StudentInfo.Id, StudentAssessment.Id);
        }

        public DataTable GetAllStudentAccount(string idStudent)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT registration.idregistration as 'REG. No.', DATE(dateregistered) as 'DATE OF REG.', schoolyear as 'SCHOOL YEAR', semester as SEMESTER, gradelevel as 'GRADE LEVEL', status as STATUS, balance as BALANCE FROM registration INNER JOIN assessment ON registration.idregistration=assessment.idregistration WHERE idstudent='" + idStudent + "' ORDER BY dateregistered DESC";
            using (MySqlDataReader dbReader = cmd.ExecuteReader())
            {
                DataTable table = new DataTable();
                table.Load(dbReader);
                dbClose();
                return table;
            }
        }

        public bool CloseAccount(long idreg)
        {
            try
            {
                if (DBCon.State == ConnectionState.Open)
                    dbClose();

                dbOpen();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DBCon;
                cmd.CommandText = "UPDATE registration SET status='CLOSED' WHERE idregistration=" + idreg;
                cmd.ExecuteNonQuery();
                dbClose();
                return true;
            }
            catch
            {
                dbClose();
                return false;
            }             
        }

        public bool OpenAccount(long idreg)
        {
            try
            {
                if (DBCon.State == ConnectionState.Open)
                    dbClose();

                dbOpen();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DBCon;
                cmd.CommandText = "UPDATE registration SET status='ENROLLED' WHERE idregistration=" + idreg;
                cmd.ExecuteNonQuery();
                dbClose();
                return true;
            }
            catch
            {
                dbClose();
                return false;
            }

        }
    }

    class AccountList
    {
        List<Account> Accounts { get; set; }

    }
}
