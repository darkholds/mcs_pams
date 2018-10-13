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
    class Assessment:DBase_Acctng
    {
        public int Id { get; set; }
        public string StudentNumber { get; set; }
        public string Semester { get; set; }
        public string SY { get; set; }
        public int Units { get; set; }
        public int SciLab { get; set; }
        public int ComLab { get; set; }
        public double Tuition { get; set; }
        public double Misc { get; set; }
        public double Total { get; set; } 
        public string AssessorId { get; set; }
        public double Balance { get; set; }
        public List<Fee> FeeBreakdown { get; set; }

        public Assessment() {
            FeeBreakdown = new List<Fee>();
        }

        public Assessment(string student, string sem, string sy, int units, int scilab, int comlab, List<Fee> fees)
        {
            FeeBreakdown = new List<Fee>();
            StudentNumber = student;
            Semester = sem;
            SY = sy;
            Units = units;
            SciLab = scilab;
            ComLab = comlab;
            FeeBreakdown = fees;
        }

        public void Save()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            string fees_id = "";

            foreach (Fee f in FeeBreakdown)
            {
                if (fees_id.Equals(""))
                    fees_id = f.Id.ToString();
                else
                    fees_id += ", " + f.Id;
            }

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "save_update_assessment_2";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sn", StudentNumber);
            cmd.Parameters.AddWithValue("@sy", SY);
            cmd.Parameters.AddWithValue("@sem", Semester);
            cmd.Parameters.AddWithValue("@units", Units);
            cmd.Parameters.AddWithValue("@reglab", SciLab);
            cmd.Parameters.AddWithValue("@comlab", ComLab);
            cmd.Parameters.AddWithValue("@assessorid", AssessorId);
            cmd.Parameters.AddWithValue("@tot", Total);
            cmd.Parameters.AddWithValue("@fees_id", fees_id);
            cmd.ExecuteNonQuery();
            dbClose();
        }

        public double Compute()
        {
            double result = 0;
            Misc = 0;
            List<Fee> fees=new List<Fee>();

            foreach (Fee f in FeeBreakdown)
            {
                if(f.Type.FeeTypeName.Equals(FeeTypes.Laboratory.ToString()))
                {
                    double r;
                    if (f.Name.Contains("Computer Lab"))
                    {
                        r = ((LaboratoryFee)f).CalculatePrice(ComLab);
                    }
                    else
                    {
                        r = ((LaboratoryFee)f).CalculatePrice(SciLab);
                    }                                          
                    result += r;
                    ((LaboratoryFee)f).Price = r;
                    Misc += r;
                }
                else if (f.Type.FeeTypeName.Equals(FeeTypes.Tuition.ToString()))
                {
                    double r = ((TuitionFee)f).CalculateTuition(Units);
                    result += r;
                    ((TuitionFee)f).PricePerUnit=r;
                    Tuition = r;
                }
                else if(f.Type.FeeTypeName.Equals(FeeTypes.Miscellaneous.ToString()))
                {
                    result += ((NormalFee)f).Amount;
                    Misc += ((NormalFee)f).Amount;
                }
                else
                {
                    result += ((OtherFee)f).Price;
                    Misc += ((OtherFee)f).Price;
                }

                fees.Add(f);
            }

            FeeBreakdown = fees;
            Total = result;
            return Total;
        }

        public bool GetAssessment(string student_number, string semester, string sy)
        {
            bool hasrecord = false;

            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_assessment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sn", student_number);
            cmd.Parameters.AddWithValue("@sem", semester);
            cmd.Parameters.AddWithValue("@sy", sy);
            MySqlDataReader dbReader = cmd.ExecuteReader();

            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    Fee f;
                    Id = Convert.ToInt32(dbReader["idassessment"]);
                    StudentNumber = dbReader["student_number"].ToString();
                    Semester = dbReader["semester"].ToString();
                    SY = dbReader["school_year"].ToString();
                    Units = Convert.ToInt16(dbReader["numberofunits"]);
                    SciLab = Convert.ToInt16(dbReader["numberofregularlab"]);
                    ComLab = Convert.ToInt16(dbReader["numberofcomputerlab"]);
                    Total = Convert.ToDouble(dbReader["total"]);
                    this.Balance = Convert.ToDouble(dbReader["balance"]);

                    if (dbReader["feetypename"].ToString().Equals(FeeTypes.Tuition.ToString()))
                    {
                        f = new TuitionFee();
                        f.Id = Convert.ToInt32(dbReader["idfee"]);
                    }
                    else if (dbReader["feetypename"].ToString().Equals(FeeTypes.Laboratory.ToString()))
                    {
                        f = new LaboratoryFee();
                        f.Id = Convert.ToInt32(dbReader["idfee"]);
                    }
                    else if (dbReader["feetypename"].ToString().Equals(FeeTypes.Miscellaneous.ToString()))
                    {
                        f = new NormalFee();
                        f.Id = Convert.ToInt32(dbReader["idfee"]);
                    }
                    else
                    {
                        f = new OtherFee();
                        f.Id = Convert.ToInt32(dbReader["idfee"]);
                    }

                    FeeBreakdown.Add(f);                  
                }
                hasrecord = true;
            }
           
            dbClose();
            return hasrecord;
        }

        public bool GetAssessment(string student_number, int assessid)
        {
            bool hasrecord = false;

            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_assessment_2";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sn", student_number);
            cmd.Parameters.AddWithValue("@assessid", assessid);
            MySqlDataReader dbReader = cmd.ExecuteReader();

            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    Fee f;
                    StudentNumber = dbReader["student_number"].ToString();
                    Semester = dbReader["semester"].ToString();
                    SY = dbReader["school_year"].ToString();
                    Units = Convert.ToInt16(dbReader["numberofunits"]);
                    SciLab = Convert.ToInt16(dbReader["numberofregularlab"]);
                    ComLab = Convert.ToInt16(dbReader["numberofcomputerlab"]);
                    Total = Convert.ToDouble(dbReader["total"]);
                    this.Balance = Convert.ToDouble(dbReader["balance"]);

                    if (dbReader["feetypename"].ToString().Equals(FeeTypes.Tuition.ToString()))
                    {
                        f = new TuitionFee(dbReader["feename"].ToString(), Convert.ToDouble(dbReader["amount"]));
                        f.Type = new FeeType().GetFeeTypeByName(FeeTypes.Tuition.ToString());
                    }
                    else if (dbReader["feetypename"].ToString().Equals(FeeTypes.Laboratory.ToString()))
                    {
                        f = new LaboratoryFee(dbReader["feename"].ToString(), Convert.ToDouble(dbReader["amount"]));
                        f.Type = new FeeType().GetFeeTypeByName(FeeTypes.Laboratory.ToString());
                    }
                    else if (dbReader["feetypename"].ToString().Equals(FeeTypes.Miscellaneous.ToString()))
                    {
                        f = new NormalFee(dbReader["feename"].ToString(), Convert.ToDouble(dbReader["amount"]));                      
                        f.Type = new FeeType().GetFeeTypeByName(FeeTypes.Miscellaneous.ToString());
                    }
                    else
                    {
                        f = new OtherFee(dbReader["feename"].ToString(), Convert.ToDouble(dbReader["amount"]));
                        f.Type = new FeeType().GetFeeTypeByName(FeeTypes.Other.ToString());
                    }
                    f.Id = Convert.ToInt32(dbReader["idfee"]);
                    f.Paid = Convert.ToDouble(dbReader["amount_paid"]);
                    FeeBreakdown.Add(f);
                }
                hasrecord = true;
            }

            dbClose();
            return hasrecord;
        }

        public bool GetOldAssessmentProfile(string student_number)
        {
            bool hasrecord = false;

            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_assessment_old";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sn", student_number);         
            MySqlDataReader dbReader = cmd.ExecuteReader();

            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    Fee f;
                   
                    if (dbReader["feetypename"].ToString().Equals(FeeTypes.Tuition.ToString()))
                    {
                        f = new TuitionFee();
                        f.Id = Convert.ToInt32(dbReader["idfee"]);
                    }
                    else if (dbReader["feetypename"].ToString().Equals(FeeTypes.Laboratory.ToString()))
                    {
                        f = new LaboratoryFee();
                        f.Id = Convert.ToInt32(dbReader["idfee"]);
                    }
                    else if (dbReader["feetypename"].ToString().Equals(FeeTypes.Miscellaneous.ToString()))
                    {
                        f = new NormalFee();
                        f.Id = Convert.ToInt32(dbReader["idfee"]);
                    }
                    else
                    {
                        f = new OtherFee();
                        f.Id = Convert.ToInt32(dbReader["idfee"]);
                    }

                    FeeBreakdown.Add(f);
                    hasrecord = true;
                }              
            }

            dbClose();
            return hasrecord;
        }

        public DataTable GetBalance(string student_number)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_balance";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sn", student_number);
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetBalanceCombo(string student_number)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_balance_combo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sn", student_number);
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable GetTotalAssessment()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_total_assessment";
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);

            return table;
        }

        public DataTable GetTotalAssessmentByDepartment()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_total_assessment_by_department";
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);

            return table;
        }

        public DataTable GetTotalFeeSummary()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_total_assessment_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sy", "");
            cmd.Parameters.AddWithValue("@sem", "");
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);

            return table;
        }

        public DataTable GetTotalFeeSummary(string sy)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_total_assessment_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sy", sy);
            cmd.Parameters.AddWithValue("@sem", "");
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);

            return table;
        }

        public DataTable GetTotalFeeSummary(string sem, string sy)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_total_assessment_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sy", sy);
            cmd.Parameters.AddWithValue("@sem", sem);
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);

            return table;
        }

        public DataTable GetSummary()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("FEE");
            table.Columns.Add("AMOUNT");
            foreach(Fee f in FeeBreakdown)
            {
                object[] r;
                if (f.Type.FeeTypeName.Equals(FeeTypes.Laboratory.ToString()))
                {
                    r = new object[3] { f.Id, f.Name, ((LaboratoryFee)f).Price };
                }
                else if (f.Type.FeeTypeName.Equals(FeeTypes.Tuition.ToString()))
                {
                    r = new object[3] { f.Id, f.Name, ((TuitionFee)f).PricePerUnit };
                }
                else if (f.Type.FeeTypeName.Equals(FeeTypes.Miscellaneous.ToString()))
                {
                    r = new object[3] { f.Id, f.Name, ((NormalFee)f).Amount };
                }
                else
                {
                    r = new object[3] { f.Id, f.Name, ((OtherFee)f).Price };
                }

                table.Rows.Add(r);
            }

            return table;
            //return ToDataTable(FeeBreakdown);
        }

        private DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            dataTable.Columns[0].SetOrdinal(2);
            //dataTable.Columns.RemoveAt(dataTable.Columns.Count - 1);
            
            return dataTable;
        }
    }

    class Balance
    {
        public int AssessmentNo { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }

        public Balance() { }
        public Balance(int assess, string desc, double amount)
        {
            AssessmentNo = assess;
            Description = desc;
            Amount = amount;
        }
    }

    class BalanceList: DBase_Acctng
    {
        public List<Balance> Bal { get; set; }
        public BalanceList() {
            Bal = new List<Balance>();
        }

        public void GetBalance(string student_number)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_balance";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sn", student_number);
            MySqlDataReader dbReader = cmd.ExecuteReader();
            
            while (dbReader.Read())
            {
                Balance bal = new Balance(Convert.ToInt32(dbReader["ASSESSMENT No."]), dbReader["FEES"].ToString(), Convert.ToDouble(dbReader["BALANCE"]));
                Bal.Add(bal);
            }    
            dbClose();
        }

        public void GetBalance(string student_number, string sem, string sy)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_balance_current";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sn", student_number);
            cmd.Parameters.AddWithValue("@sem", sem);
            cmd.Parameters.AddWithValue("@sy", sy);
            MySqlDataReader dbReader = cmd.ExecuteReader();

            while (dbReader.Read())
            {
                Balance bal = new Balance(Convert.ToInt32(dbReader["ASSESSMENT No."]), dbReader["FEES"].ToString(), Convert.ToDouble(dbReader["BALANCE"]));
                Bal.Add(bal);
            }
            dbClose();
        }

        public void GetOldBalance(string student_number, string sem, string sy)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_balance_old";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sn", student_number);
            cmd.Parameters.AddWithValue("@sem", sem);
            cmd.Parameters.AddWithValue("@sy", sy);
            MySqlDataReader dbReader = cmd.ExecuteReader();

            while (dbReader.Read())
            {
                Balance bal = new Balance(Convert.ToInt32(dbReader["ASSESSMENT No."]), dbReader["FEES"].ToString(), Convert.ToDouble(dbReader["BALANCE"]));
                Bal.Add(bal);
            }
            dbClose();
        }
    }
}
