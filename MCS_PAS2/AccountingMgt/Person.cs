using System;
using MySql.Data.MySqlClient;
using System.Data;


namespace AccountingMgt
{
    enum UserTypes { Admin, Cashier, Accountant, Registrar, Treasurer };

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
        public string CurrentYearLevel { get; set; }
        public string PresentAddress { get; set; }
        public string ContactNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string Religion { get; set; }

        public Student() { }

        public Student GetStudent(string student_number)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT * FROM student WHERE idstudent='" + student_number + "'";
            MySqlDataReader dbReader = cmd.ExecuteReader();

            if (dbReader.HasRows)
            {
                dbReader.Read();
                Id = dbReader["idstudent"].ToString();
                FirstName = dbReader["firstname"].ToString();
                LastName = dbReader["lastname"].ToString();
                MiddleName = dbReader["middlename"].ToString();
                PresentAddress = dbReader["presentaddress"].ToString();
                ContactNumber = dbReader["contactno"].ToString();
                DateOfBirth = Convert.ToDateTime(dbReader["dateofbirth"].ToString());
                Sex = dbReader["sex"].ToString();
                Religion = dbReader["religion"].ToString();
                CurrentYearLevel = dbReader["currentyearlevel"].ToString();

                dbClose();
                return this;
            }
            else
            {
                dbClose();
                return null;
            }
        }

        public DataTable SearchStudentsById(string searchId)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT idstudent as 'STUDENT NUMBER', lastname as 'LAST NAME', firstname as 'FIRST NAME', middlename as 'MIDDLE NAME', currentyearlevel as 'GRADE LEVEL' FROM student WHERE idstudent LIKE '"+ searchId +"%'";
            MySqlDataReader dbReader = cmd.ExecuteReader();          
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();

            return table;
        }

        public DataTable SearchStudentsByLastName(string searchName)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT idstudent as 'STUDENT NUMBER', lastname as 'LAST NAME', firstname as 'FIRST NAME', middlename as 'MIDDLE NAME', currentyearlevel as 'GRADE LEVEL' FROM student WHERE lastname LIKE '" + searchName + "%'";
            MySqlDataReader dbReader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dbReader);
            dbClose();
            return table;
        }
    }

    public class User : Person
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }

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
            cmd.CommandText = "SELECT * FROM user WHERE username = '" + username + "' AND password = '" + password + "'";
            MySqlDataReader dbReader = cmd.ExecuteReader();
            if (dbReader.HasRows)
            {
                dbReader.Read();
                Username = dbReader["username"].ToString();
                Password = dbReader["password"].ToString();
                Role = dbReader["usertype"].ToString();
                Name = dbReader["name"].ToString();
                IsActive = Convert.ToBoolean(dbReader["isactive"]);
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
            cmd.CommandText = "INSERT INTO userlog(username, sourcemodule) VALUES('" + username + "', '" + source +"')";
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
            cmd.CommandText = "UPDATE userlog SET timelogout = CURRENT_TIMESTAMP WHERE username='" + username + "' AND sourcemodule='" + source + "' AND timelogout IS null";
            cmd.ExecuteNonQuery();
            dbClose();
        }

        public bool ChangePassword(string username, string newpassword)
        {
            try
            {
                if (DBCon.State == ConnectionState.Open)
                    dbClose();

                dbOpen();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DBCon;
                cmd.CommandText = "UPDATE user SET password='" + newpassword + "' WHERE username='" + username + "'";
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
}
