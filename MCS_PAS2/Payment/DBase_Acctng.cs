using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Payment
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
