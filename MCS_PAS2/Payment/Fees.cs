using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Payment
{
    public abstract class Fee : DBase_Acctng
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FeeType Type { get; set; }
        public double Paid { get; set; }

        public Fee() { }

        public DataTable GetFeeByName()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_fee_by_name";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", Name);

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
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
            cmd.CommandText = "get_all_fee";
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public bool IsNullOrEmpty()
        {
            bool b = false;
            if (this == null || Id == 0 || Name.Equals(""))
                b = true;

            return b;
        }

        public abstract void SaveFee();
        public abstract void UpdateFee();
    }

    class NormalFee: Fee
    {
        public double Amount { get; set; }

        public NormalFee() { }
        public NormalFee(string name, double amount)
        {
            Name = name;
            Amount = amount;
            Type = new FeeType().GetFeeTypeByName(FeeTypes.Miscellaneous.ToString());
        }

        public override void SaveFee()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "save_fee";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@amount", Amount);
            cmd.Parameters.AddWithValue("@idft", Type.FeeTypeId);
            cmd.ExecuteNonQuery();
            dbClose();
        }

        public override void UpdateFee()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "update_fee";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@amount", Amount);
            cmd.Parameters.AddWithValue("@idft", Type.FeeTypeId);
            cmd.ExecuteNonQuery();
            dbClose();
        }
    }

    class LaboratoryFee: Fee
    {
        public double Price { get; internal set; }

        public LaboratoryFee() { }
        public LaboratoryFee(string name, double price)
        {
            Name = name;
            Price = price;
            Type = new FeeType().GetFeeTypeByName(FeeTypes.Laboratory.ToString());
        }

        public double CalculatePrice(int numberoflab)
        {
            return Price * numberoflab;
        }

        public override void SaveFee()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "save_fee";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@amount", Price);
            cmd.Parameters.AddWithValue("@idft", Type.FeeTypeId);
            cmd.ExecuteNonQuery();
            dbClose();
        }

        public override void UpdateFee()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "update_fee";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@amount", Price);
            cmd.Parameters.AddWithValue("@idft", Type.FeeTypeId);
            cmd.ExecuteNonQuery();
            dbClose();
        }
    }

    class TuitionFee: Fee
    {
        public double PricePerUnit { get; set; }

        public TuitionFee() { }

        public TuitionFee(string name, double price)
        {
            Name = name;
            PricePerUnit = price;
            Type = new FeeType().GetFeeTypeByName(FeeTypes.Tuition.ToString());
        }

        public double CalculateTuition(int numbeofunits)
        {
            return PricePerUnit * numbeofunits;
        }

        public override void SaveFee()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "save_fee";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@amount", PricePerUnit);
            cmd.Parameters.AddWithValue("@idft", Type.FeeTypeId);
            cmd.ExecuteNonQuery();
            dbClose();
        }

        public override void UpdateFee()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "update_fee";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@amount", PricePerUnit);
            cmd.Parameters.AddWithValue("@idft", Type.FeeTypeId);
            cmd.ExecuteNonQuery();
            dbClose();
        }
    }

    class OtherFee : Fee
    {
        public double Price { get; set; }

        public OtherFee() { }

        public OtherFee(string name, double price)
        {
            Name = name;
            Price = price;
            Type = new FeeType().GetFeeTypeByName(FeeTypes.Other.ToString());
        }

        public override void SaveFee()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "save_fee";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@amount", Price);
            cmd.Parameters.AddWithValue("@idft", Type.FeeTypeId);
            cmd.ExecuteNonQuery();
            dbClose();
        }

        public override void UpdateFee()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "update_fee";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@amount", Price);
            cmd.Parameters.AddWithValue("@idft", Type.FeeTypeId);
            cmd.ExecuteNonQuery();
            dbClose();
        }

        public DataTable GetOtherFees()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_other_fees";
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }
    }

    class FeesGroup:DBase_Acctng
    {
        public int Id { get; set; }
        public string FeesName { get; set; }
        public byte Semester { get; set; }
        public string Description { get; set; }
        public List<Fee> Fees { get; set; }

        public FeesGroup() {
            Fees = new List<Fee>();
        }
        public FeesGroup(string name, string desc, byte semester, List<Fee> fees)
        {
            Fees = new List<Fee>();
            FeesName = name;
            Description = desc;
            Semester = semester;
            Fees = fees;
        }

        public void SaveFees()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            string fees_id="";

            foreach(Fee f in Fees)
            {
                if (fees_id.Equals(""))
                    fees_id = f.Id.ToString();
                else
                    fees_id += ", " + f.Id;
            }

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "save_fee_group";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", FeesName);
            cmd.Parameters.AddWithValue("@description", Description);
            cmd.Parameters.AddWithValue("@fees_id", fees_id);
            //cmd.Parameters.AddWithValue("@sem", Semester);
            cmd.ExecuteNonQuery();
            dbClose();
        }

        public void UpdateFees()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            string fees_id = "";

            foreach (Fee f in Fees)
            {
                if (fees_id.Equals(""))
                    fees_id = f.Id.ToString();
                else
                    fees_id += ", " + f.Id;
            }

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "update_fee_group";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.Parameters.AddWithValue("@name", FeesName);
            cmd.Parameters.AddWithValue("@description", Description);
            cmd.Parameters.AddWithValue("@fees_id", fees_id);
            cmd.ExecuteNonQuery();
            dbClose();
        }

        public DataTable GetAllFeesGroup()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_all_fee_groups";
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetAllFeesGroupMisc()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_all_fee_groups_misc";
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetAllFeesGroupEntrance()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_all_fee_groups_entrance";
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetAllFeesGroupsFee()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_all_fee_groups_fee";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", Id);
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetFeeGroup(int feegroupid)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_fee_group_by_id";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", feegroupid);
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);

            foreach(DataRow row in table.Rows)
            {
                Id = Convert.ToInt32(row["idgroup"]);
                FeesName = row["groupname"].ToString();
                Description = row["description"].ToString();

                Fee f;
                if (row["feetypename"].ToString().Equals(FeeTypes.Miscellaneous.ToString()))
                {
                    f = new NormalFee(row["feename"].ToString(), Convert.ToDouble(row["amount"].ToString()));
                } 
                else if (row["feetypename"].ToString().Equals(FeeTypes.Laboratory.ToString()))
                {
                    f = new LaboratoryFee(row["feename"].ToString(), Convert.ToDouble(row["amount"].ToString()));
                }
                else if (row["feetypename"].ToString().Equals(FeeTypes.Tuition.ToString()))
                {
                    f = new TuitionFee(row["feename"].ToString(), Convert.ToDouble(row["amount"].ToString()));
                }
                else
                {
                    f = new OtherFee(row["feename"].ToString(), Convert.ToDouble(row["amount"].ToString()));
                }

                f.Id = Convert.ToInt32(row["idfee"].ToString());
                Fees.Add(f);
            }
           
            dbClose();
            return table;
        }
    }


    enum FeeTypes { Miscellaneous, Tuition, Laboratory, Other };
    public class FeeType:DBase_Acctng
    {
        public int FeeTypeId { get; set; }
        public string FeeTypeName { get; set; }
        public FeeType() { }

        public FeeType(String name)
        {
            FeeTypeName = name;
        }

        public FeeType GetFeeTypeByName(string name)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_feetype_by_name";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", name);

            MySqlDataReader dbReader = cmd.ExecuteReader();
            if (dbReader.Read())
            {
                FeeTypeId = Convert.ToInt32(dbReader["idfeetype"]);
                FeeTypeName = dbReader["feetypename"].ToString();
            }
            //DataTable table = new DataTable();
            //table.Load(dbReader);
            dbClose();

            return this;
        }

        public DataTable GetAllFeeType()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_all_feetype";
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }
    }
}
