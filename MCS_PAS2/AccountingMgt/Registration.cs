using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingMgt
{
    class Registration : DBase_Acctng
    {
        public long Id { get; set; }
        public Student StudentInfo { get; set; }
        public string GradeLevel { get; set; }
        public string Section { get; set; }
        public string SchoolYear { get; set; }
        public string Semester { get; set; }
        public string Status { get; set; }
        public string DateRegistered { get; set; }

        public Registration() { }

        public Registration GetRegistration(string idStudent)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT idregistration, schoolyear, idstudent, gradelevel, section, semester, status, dateregistered FROM registration WHERE idstudent='" + idStudent + "' AND (status='PENDING' OR status='ENROLLED')";
            MySqlDataReader dbReader = cmd.ExecuteReader();

            if (dbReader.HasRows)
            {
                StudentInfo = new Student().GetStudent(idStudent);
                dbReader.Read();
                Id = Convert.ToInt64(dbReader["idregistration"].ToString());
                GradeLevel = dbReader["gradelevel"].ToString();
                Section = dbReader["section"].ToString();
                SchoolYear = dbReader["schoolyear"].ToString();
                Semester = dbReader["semester"].ToString();
                Status = dbReader["status"].ToString();
                DateRegistered = dbReader["dateregistered"].ToString();
                dbClose();
                return this;
            }
            else
            {
                dbClose();
                return null;
            }
        }

        public Registration GetRegistration(long idreg)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT idregistration, schoolyear, idstudent, gradelevel, section, semester, status, dateregistered FROM registration WHERE idregistration=" + idreg;
            MySqlDataReader dbReader = cmd.ExecuteReader();

            if (dbReader.HasRows)
            { 
                dbReader.Read();
                Id = Convert.ToInt64(dbReader["idregistration"].ToString());
                GradeLevel = dbReader["gradelevel"].ToString();
                Section = dbReader["section"].ToString();
                SchoolYear = dbReader["schoolyear"].ToString();
                Semester = dbReader["semester"].ToString();
                Status = dbReader["status"].ToString();
                DateRegistered = dbReader["dateregistered"].ToString();
                StudentInfo = new Student().GetStudent(dbReader["idstudent"].ToString());
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
}
