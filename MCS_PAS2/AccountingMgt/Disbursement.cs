using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingMgt
{
    class Disbursement:DBase_Acctng
    {
        public Disbursement() { }

        public DataTable GetAllDisbursement()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT iddisbursement as ID, DATE(dateofdisbursement) as 'DATE', amount as AMOUNT, purpose as PURPOSE FROM disbursement ORDER BY dateofdisbursement, iddisbursement DESC";
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);

            return table;
        }

        public bool SaveDisbursement(string purpose, double amount, List<PaymentDetail> disburseDetails)
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
                cmd.CommandText = "INSERT INTO disbursement(purpose, amount) VALUES ('" + purpose + "', " +  amount + ")";
                cmd.ExecuteNonQuery();
                long id = cmd.LastInsertedId;

                foreach(PaymentDetail dd in disburseDetails)
                {
                    cmd.CommandText = "INSERT INTO disbursement_detail(iddisbursement, feecode, amount) VALUES (" + id + ", '" + dd.Code + "', " + dd.Amount + ")";
                    cmd.ExecuteNonQuery();
                }

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

        public DataTable GetDisbursementDetails(long id)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT feename as 'SOURCE FUND', amount as AMOUNT FROM disbursement_detail INNER JOIN fee ON disbursement_detail.feecode=fee.feecode WHERE iddisbursement=" + id +" ORDER BY feename, disbursement_detail.feecode";
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);

            return table;
        }
    }
}
