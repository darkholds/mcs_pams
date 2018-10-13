using System.Configuration;
using MySql.Data.MySqlClient;

namespace AccountingMgt
{
    public class DBase_Acctng
    {
        private string con = ConfigurationManager.ConnectionStrings["acctng"].ConnectionString;
        protected MySqlConnection dbCon;

        public DBase_Acctng()
        {
            dbCon = new MySqlConnection(con);
        }

        public MySqlConnection DBCon
        {
            get
            {
                return dbCon;
            }
        }

        public void dbOpen()
        {
            dbCon.Open();
        }
        public void dbClose()
        {
            dbCon.Close();
        }
    }
}
