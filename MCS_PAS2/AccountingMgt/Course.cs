using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace AccountingMgt
{
    class Department : DBase_Acctng
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        
        public Department() { }

        public Department GetDepartMentByName(string deptname)
        {
            string query = "SELECT * FROM department WHERE departmentname LIKE '" + deptname + "'";
            if (dbCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand(query, dbCon);
            MySqlDataReader dbReader = cmd.ExecuteReader();

            if (dbReader.HasRows)
            {
                dbReader.Read();
                Id =  Convert.ToInt16(dbReader["iddepartment"].ToString()); 
                DepartmentName = dbReader["departmentname"].ToString();
                
                dbClose();
                return this;
            }
            else
            {
                dbClose();
                return null;
            }        
        }

        public DataTable GetAllDepartment()
        {
            if (dbCon.State == ConnectionState.Open)
                dbClose();

            string query = "SELECT * FROM department";
            dbOpen();
            MySqlCommand cmd = new MySqlCommand(query, dbCon);
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }
    }

    class Course: DBase_Acctng
    {
        protected string Id { get; set; }
        protected string CollegeID { get; set; }
        protected string Name { get; set; }
        protected string Description { get; set; }
        
        public Course(){}

        public Course(string id, string collegeID, string name)
        {
            Id = id;
            CollegeID = collegeID;
            Name = name;
        }

        public Course(string id, string collegeID, string name, string description)
        {
            Id = id;
            CollegeID = collegeID;
            Name = name;
            Description = description;
        }

        //public override void getRecord() 
        //{
        //    string query = "SELECT * FROM course WHERE courseName LIKE '" + name + "'";
        //    if (dbCon.State == System.Data.ConnectionState.Open)
        //        dbClose();

        //    dbOpen();
        //    MySqlCommand cmd = new MySqlCommand(query, dbCon);
        //    MySqlDataReader dbReader = cmd.ExecuteReader();
        //    if (dbReader.HasRows)
        //    {
        //        dbReader.Read();
        //        id = dbReader["courseID"] + "";
        //        collegeID = dbReader["collegeID"] + "";
        //        name = dbReader["courseName"] + "";
        //        description = dbReader["courseDescription"] + "";
        //    }
        //    else
        //    {
        //        id = "";
        //        collegeID = "";
        //        name = "";
        //        description = "";
        //    }
        //    dbClose();
        //}

        //public override void updateRecord(string id) 
        //{
        //    string query = "UPDATE course SET courseID='" + this.id + "', courseName='" + name + "', courseDescription='" + description + "', collegeID='" + collegeID + "' WHERE courseID='" + id + "'";
        //    dbOpen();
        //    MySqlCommand cmd = new MySqlCommand(query, dbCon);
        //    cmd.ExecuteNonQuery();
        //    dbClose();
        //}
        
        //public override void removeRecord()
        //{
        //    string query = "DELETE FROM course WHERE courseID='" + id + "'";
        //    dbOpen();
        //    MySqlCommand cmd = new MySqlCommand(query, dbCon);
        //    cmd.ExecuteNonQuery();
        //    dbClose();
        //}

        //public void dgvView(ref System.Windows.Forms.DataGridView dgv, string query)
        //{
        //    if (dbCon.State == System.Data.ConnectionState.Open)
        //        dbClose();

        //    dbOpen();
        //    MySqlCommand cmd = new MySqlCommand(query, dbCon);
        //    MySqlDataReader dbReader = cmd.ExecuteReader();
        //    System.Data.DataTable table = new System.Data.DataTable();
        //    table.Load(dbReader);
        //    dgv.DataSource = table;
        //    dbClose();
        //}

        public void comboView(System.Windows.Forms.ComboBox cmb)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_all_course";
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader dbReader = cmd.ExecuteReader();
            if (dbReader.HasRows)
            {
                DataTable table = new DataTable();
                table.Load(dbReader);
                cmb.DisplayMember = "coursename";
                cmb.ValueMember = "idcourse";
                cmb.DataSource = table;               
            }
            dbClose();
        }

        //public void tsComboView(System.Windows.Forms.ToolStripComboBox cmb, string query)
        //{
        //    if (dbCon.State == System.Data.ConnectionState.Open)
        //        dbClose();

        //    dbOpen();
        //    MySqlCommand cmd = new MySqlCommand(query, dbCon);
        //    MySqlDataReader dbReader = cmd.ExecuteReader();
        //    if (dbReader.HasRows)
        //    {              
        //        System.Data.DataTable table = new System.Data.DataTable();
        //        table.Load(dbReader);
        //        foreach (System.Data.DataRow row in table.Rows)
        //        {
        //            cmb.Items.Add(row["courseName"].ToString());
        //        }
        //    }
        //    dbClose();
        //}

        //public void ModifiedComboView(System.Windows.Forms.ComboBox cmb, string query)
        //{
        //    if (dbCon.State == System.Data.ConnectionState.Open)
        //        dbClose();

        //    dbOpen();
        //    MySqlCommand cmd = new MySqlCommand(query, dbCon);
        //    MySqlDataReader dbReader = cmd.ExecuteReader();
        //    if (dbReader.HasRows)
        //    {
        //        System.Data.DataTable table = new System.Data.DataTable();
        //        table.Load(dbReader);
        //        foreach (System.Data.DataRow row in table.Rows)
        //        {
        //            cmb.Items.Add(row["courseName"].ToString());
        //        }
        //        cmb.Items.Add("All");
        //        cmb.SelectedIndex = cmb.Items.Count - 1;
        //    }
        //    dbClose();
        //}

        //public void GetCourseByID()
        //{
        //    string query = "SELECT * FROM course WHERE courseID LIKE '" + id + "'";
        //    if (dbCon.State == System.Data.ConnectionState.Open)
        //        dbClose();

        //    dbOpen();
        //    MySqlCommand cmd = new MySqlCommand(query, dbCon);
        //    MySqlDataReader dbReader = cmd.ExecuteReader();
        //    if (dbReader.HasRows)
        //    {
        //        dbReader.Read();
        //        id = dbReader["courseID"].ToString();
        //        collegeID = dbReader["collegeID"].ToString();
        //        name = dbReader["courseName"].ToString();
        //        description = dbReader["courseDescription"].ToString();
        //    }
        //    else
        //    {
        //        id = "";
        //        collegeID = "";
        //        name = "";
        //        description = "";
        //    }
        //    dbClose();
        //}

        //public void GetCourseByName()
        //{
        //    getRecord();
        //}
    }
}
