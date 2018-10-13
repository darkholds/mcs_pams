using System;
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;
using System.Configuration;


namespace Payment
{
    enum UserTypes { Admin, Cashier, Accountant };

    public class Person: DBase_Acctng
    {
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
       

        public Person() { }

        public Person(string firstName, string lastName, string middleName)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }
    }

    public class Student : Person
    {   
        public string Id { get; set; }
        public string Course { get; set; }
        public int YearLevel { get; set; }

        public Student() { }

        public Student(string id, string firstname, string lastname, string middlename, string course)
        {
            Id = id;
            FirstName = firstname;
            LastName = lastname;
            MiddleName = middlename;
            Course = course;
        }

        public void SaveStudent()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "save_student";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.Parameters.AddWithValue("@fname", FirstName);
            cmd.Parameters.AddWithValue("@lname", LastName);
            cmd.Parameters.AddWithValue("@mname", MiddleName);
            cmd.Parameters.AddWithValue("@course", Course);
            cmd.ExecuteNonQuery();
            dbClose();
        }

        public DataTable GetAllStudent()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_all_student";
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public void UpdateStudent(string previousId)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "update_student";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.Parameters.AddWithValue("@fname", FirstName);
            cmd.Parameters.AddWithValue("@lname", LastName);
            cmd.Parameters.AddWithValue("@mname", MiddleName);
            cmd.Parameters.AddWithValue("@course", Course);
            cmd.Parameters.AddWithValue("@pid", previousId);
            cmd.ExecuteNonQuery();
            dbClose();
        }

        //public void RemoveStudent()
        //{
        //    if (DBCon.State == ConnectionState.Open)
        //        dbClose();

        //    dbOpen();
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.Connection = DBCon;
        //    cmd.CommandText = "remove_student";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@p_id",id);
        //    cmd.ExecuteNonQuery();
        //    dbClose();
        //}

        public DataTable SearchStudentsById(string searchId)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "search_students_by_id";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", searchId);
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            foreach (DataRow row in table.Rows)
            {
                Id = row["SN"].ToString();
                FirstName = row["FIRST NAME"].ToString();
                LastName = row["LAST NAME"].ToString();
                MiddleName = row["MIDDLE NAME"].ToString();
                Course = row["COURSE"].ToString();
                YearLevel = Convert.ToInt16(row["YEAR LEVEL"]);
            }

            return table;
        }

        public DataTable SearchStudentsByLastName(string searchName)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "search_students_by_last_name";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", searchName);
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();
            return table;
        }
        //public DataTable SearchByLastName()
        //{
        //    if (DBCon.State == ConnectionState.Open)
        //        dbClose();

        //    dbOpen();
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.Connection = DBCon;
        //    cmd.CommandText = "search_student_lastname";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@lname", LastName + "%");
        //    MySqlDataReader dbReader = cmd.ExecuteReader();
        //    DataTable table = new DataTable();
        //    table.Load(dbReader);
        //    dbClose();
        //    return table;
        //}

        //public Student SearchByRFID()
        //{
        //    if (DBCon.State == ConnectionState.Open)
        //        dbClose();

        //    dbOpen();
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.Connection = DBCon;
        //    cmd.CommandText = "search_student_id";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@p_id", ID);
        //    MySqlDataReader dbReader = cmd.ExecuteReader();
        //   // DataTable table = new DataTable();
        //    //table.Load(dbReader);
        //    //dbClose();

        //    Student student = new Student();

        //    return student;
        //}

        public void LogStudent()
        {

        }

       
    }

    public class User : Person
    {
        //public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public User() { }
        public User(string username, string password, string role, string fname, string lname, string mname)
        {
            FirstName = fname;
            LastName = lname;
            MiddleName = mname;
            Username = username;
            Password = password;
            Role = role;
        }

        public bool LoginUser(string username, string password)
        {
            bool auth = false;

            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "get_user";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@un", username);
            cmd.Parameters.AddWithValue("@pw", password);
            MySqlDataReader dbReader = cmd.ExecuteReader();
            if (dbReader.HasRows)
            {
                dbReader.Read();
                Username = dbReader["username"].ToString();
                Password = dbReader["pword"].ToString();
                Role = dbReader["role"].ToString();
                FirstName = dbReader["firstname"].ToString();
                LastName = dbReader["lastname"].ToString();
                MiddleName = dbReader["middlename"].ToString();
                auth = true;
            }
            dbClose();
            return auth;
        }

        public void UserLogin(string username, string source)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "save_user_log";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@un", username);
            cmd.Parameters.AddWithValue("@src", source);
            cmd.ExecuteNonQuery();
            dbClose();
        }

        public void UserLogout(string username, string source)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "save_user_logout";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@un", username);
            cmd.Parameters.AddWithValue("@src", source);
            cmd.ExecuteNonQuery();
            dbClose();
        }
    }
}
