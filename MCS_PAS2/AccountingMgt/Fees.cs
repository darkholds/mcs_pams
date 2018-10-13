using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace AccountingMgt
{
    public enum FeeTypes:int {TuitionFee=1, StandardFees, NonStandardFees, Other, PTA, Alumni};
    /* 1 = Tuition Fee
     * 2 = Standard
     * 3 = Non-Standard
     */

    public class Fee: DBase_Acctng
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int FeeType { get; set; }

        public Fee() { }

        public Fee(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public int GetFeeTypeNumeric(string feetype)
        {
            if (feetype.Equals(FeeTypes.TuitionFee.ToString()))
                return 1;
            else if (feetype.Equals(FeeTypes.StandardFees.ToString()))
                return 2;
            else if (feetype.Equals(FeeTypes.NonStandardFees.ToString()))
                return 3;
            else if (feetype.Equals(FeeTypes.Other.ToString()))
                return 4;
            else if (feetype.Equals(FeeTypes.PTA.ToString()))
                return 5;
            else if (feetype.Equals(FeeTypes.Alumni.ToString()))
                return 6;
            else
                return 0;
        }

        public DataTable GetFeeByName(string name)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT * FROM fee WHERE feename='"+ name + "'";
            MySqlDataReader dbReader = cmd.ExecuteReader();

            //load values to table
            DataTable table = new DataTable();
            table.Load(dbReader);

            //put values to attributes
            if (dbReader.HasRows)
            {
                dbReader.Read();
                Code = dbReader["feecode"].ToString();
                Name = dbReader["feename"].ToString();
                ShortName = dbReader["shortname"].ToString();
                FeeType = Convert.ToInt16(dbReader["feetype"]);
            }

            dbClose();
            return table;
        }

        public DataTable GetFeeByCode(string code)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT * FROM fee WHERE feecode='"+ code + "'";
            MySqlDataReader dbReader = cmd.ExecuteReader();

            //load values to table
            DataTable table = new DataTable();
            table.Load(dbReader);

            //put values to attributes
            if (dbReader.HasRows)
            {
                dbReader.Read();
                Code = dbReader["feecode"].ToString();
                Name = dbReader["feename"].ToString();
                ShortName = dbReader["shortname"].ToString();
                FeeType = Convert.ToInt16(dbReader["feetype"]);
            }
            dbClose();

            return table;
        }

        public DataTable GetAllFees()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT feecode as CODE, feename as FEE, feetype as 'FEE TYPE' FROM fee ORDER BY feetype, feecode ASC";
          
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetAllStandardFees()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT feecode as CODE, feename as FEE, shortname as 'SHORT NAME', feetype as 'FEE TYPE' FROM fee WHERE feetype=1 OR feetype=2 ORDER BY feetype, feename";

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
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
            cmd.CommandText = "SELECT feecode as CODE, feename as FEE, shortname as 'SHORT NAME', feetype as 'FEE TYPE' FROM fee WHERE feetype=4 OR feetype=5 OR feetype=6 ORDER BY feename, feecode";

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetAllOtherFeesStrict()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT feetype as 'FEE TYPE', feecode as CODE, feename as FEE FROM fee WHERE feetype=4 ORDER BY feename, feecode";

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public bool IsNullOrEmpty()
        {
            bool b = false;
            if (this == null || Code.Equals("") || Name.Equals(""))
                b = true;

            return b;
        }

        public void SaveFee(string code, string name, string shortname, int type)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "INSERT INTO fee(feecode, feename, shortname, feetype) VALUES ('" + code + "', '" + name + "', '" + shortname + "', " + type + ")";
            cmd.ExecuteNonQuery();
            dbClose();
        }

        public void DeleteFee(string code)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "DELETE FROM fee WHERE feecode='" + code + "'";
            cmd.ExecuteNonQuery();
            dbClose();
        }

        public void UpdateFee(string oldcode, string code, string name, string shortname, int type)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "UPDATE fee SET feecode='" + code +"', feename='" + name +  "', shortname='" + shortname + "', feetype=" + type + " WHERE feecode='" + oldcode + "'";
            cmd.ExecuteNonQuery();
            dbClose();
        }
    }

    class FeeObject: Fee
    {
        public double Amount { get; set; }

        public FeeObject() { }

        public FeeObject GetFeeObjectByCode(string code, long assessid)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT * FROM fee WHERE feecode='" + code + "'";
            MySqlDataReader dbReader = cmd.ExecuteReader();

            //put values to attributes
            if (dbReader.HasRows)
            {
                dbReader.Read();
                Code = dbReader["feecode"].ToString();
                Name = dbReader["feename"].ToString();
                FeeType = Convert.ToInt16(dbReader["feetype"]);
                dbClose();
                return this;
            }
            else
            {
                dbClose();
                return null;
            }
        }
    }

    class GradeLevelFees: DBase_Acctng {
        public string GradeLevel { get; set; }
        public string Description { get; set; }
        public List<Fee> FeeList { get; set; }

        public GradeLevelFees()
        {
            FeeList = new List<Fee>();
        }
        public GradeLevelFees(string gradelevel, string description)
        {
            FeeList = new List<Fee>();
            GradeLevel = gradelevel;
            Description = description;
        }

        public DataTable GetAllGradeLevel()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT * FROM gradelevel";

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public void SaveFeeInList(int idgradelevel, string code, double amount)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "INSERT INTO gradelevelfees VALUES (" + idgradelevel + ", '" + code + "', " + amount + ")";
            cmd.ExecuteNonQuery();
            dbClose();
        }

        public DataTable GetAllFeesInGradeLevel(int idgradelevel)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT fee.feecode as CODE, feename as FEE, amount as AMOUNT, feetype as TYPE FROM gradelevelfees INNER JOIN fee ON gradelevelfees.feecode=fee.feecode WHERE idgradelevel=" + idgradelevel + " ORDER BY feetype, fee.feecode ASC";

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetAllStandardFeesInGradeLevel(int idgradelevel)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT fee.feecode as CODE, feename as FEE, amount as AMOUNT, feetype as TYPE FROM gradelevelfees INNER JOIN fee ON gradelevelfees.feecode=fee.feecode WHERE idgradelevel=" + idgradelevel + " AND (feetype=1 OR feetype=2) ORDER BY feetype, feename";

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetAllNonStandardFeesInGradeLevel(int idgradelevel)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT fee.feecode as CODE, feename as FEE, amount as AMOUNT, feetype as TYPE FROM gradelevelfees INNER JOIN fee ON gradelevelfees.feecode=fee.feecode WHERE idgradelevel=" + idgradelevel + " AND feetype=3 ORDER BY feetype, feename";

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetAllOtherFeesInGradeLevel(int idgradelevel)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT fee.feecode as CODE, feename as FEE, amount as AMOUNT, feetype as TYPE FROM gradelevelfees INNER JOIN fee ON gradelevelfees.feecode=fee.feecode WHERE idgradelevel=" + idgradelevel + " AND (feetype=4 OR feetype=5 OR feetype=6) ORDER BY feetype, feename";

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public void DeleteFeeInGradeLevel(string code, int idgradelevel)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "DELETE FROM gradelevelfees WHERE feecode='" + code + "' AND idgradelevel=" + idgradelevel;
            cmd.ExecuteNonQuery();
            dbClose();
        }
    }
}
