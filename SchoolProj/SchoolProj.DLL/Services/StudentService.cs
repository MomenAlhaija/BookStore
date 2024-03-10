using SchoolProj.DLL.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SchoolProj.DLL.Services
{
    public class StudentService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        SqlConnection con;
        SqlDataAdapter adaptar;
        SqlCommand cmd;
        DataTable dt;
        DataRow row;
        public List<StudentModel> GetStudents()
        {
            List<StudentModel> students = new List<StudentModel>();
            string command = "SELECT * FROM Students INNER JOIN Users ON Students.UserId = Users.UserId INNER JOIN Classes ON Students.ClassId = Classes.Id";
            using (con = new SqlConnection(connectionString))
            {
                adaptar = new SqlDataAdapter(command, con);
                dt = new DataTable();
                adaptar.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    StudentModel student = new StudentModel
                    {
                        UserId = Convert.ToInt32(row["UserId"]),
                        StudentId = Convert.ToInt32(row["StudentId"]),
                        FullNameAr = row["FullNameAr"].ToString(),
                        FullNameEn = row["FullNameEn"].ToString(),
                        ClassId = Convert.ToInt32(row["ClassId"]),
                        Age = Convert.ToInt32(row["Age"]),
                        Gender = Convert.ToInt32(row["Gender"]),
                        ClassName = row["ClassNameAr"].ToString(),
                        UserType = Convert.ToInt32(row["UserType"]),
                        UserName = row["UserName"].ToString(),
                        Password = row["Password"].ToString()
                    };

                    students.Add(student);
                }
                return students;
            }
        }

        public StudentModel GetStudentByID(int studentId)
        {
            string command = "SP_SelectUser";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    adaptar = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adaptar.Fill(dt);
                    if (dt.Rows.Count > 0) 
                    {
                        row = dt.Rows[0];
                        return new StudentModel
                        {
                            Password = Convert.ToString(row["Password"]),
                            UserId = Convert.ToInt32(row["UserId"]),
                            StudentId = Convert.ToInt32(row["StudentId"]),
                            FullNameAr = row["FullNameAr"].ToString(),
                            FullNameEn = row["FullNameEn"].ToString(),
                            ClassId = Convert.ToInt32(row["ClassId"]),
                            Age = Convert.ToInt32(row["Age"]),
                            Gender = Convert.ToInt32(row["Gender"]),
                            ClassName = (row["ClassNameAr"]).ToString(),
                            UserType = Convert.ToInt32(row["UserType"]),
                            UserName = (row["UserName"]).ToString()
                        };
                    }
                    return null;
                }
            }
        }
        public void AddStudent(StudentModel student)
        {
            string command = "SP_CreateStudent";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userName", student.UserName);
                    cmd.Parameters.Add("@password", student.Password);
                    cmd.Parameters.Add("@fullNameAr", student.FullNameAr);
                    cmd.Parameters.Add("@fullNameEn", student.FullNameEn);
                    cmd.Parameters.Add("@gender", student.Gender);
                    cmd.Parameters.Add("@age", student.Age);
                    cmd.Parameters.Add("@classId", student.ClassId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void UpdateStudent(StudentModel student)
        {
            string command = "SP_UpdateStudent";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@studentId", student.StudentId);
                    cmd.Parameters.Add("@fullNameAr", student.FullNameAr);
                    cmd.Parameters.Add("@fullNameEn", student.FullNameEn);
                    cmd.Parameters.Add("@userName", student.UserName);
                    cmd.Parameters.Add("@userId", student.UserId);
                    cmd.Parameters.Add("@age", student.Age);
                    cmd.Parameters.Add("@gender", student.Gender);
                    cmd.Parameters.Add("@classId", student.ClassId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DeleteStudent(int studentId)
        {
            string command = "SP_DeleteStudent";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@studentId", studentId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public StudentModel GetStudentByUserId(int userId)
        {
            string command = "SELECT * FROM Students INNER JOIN Users ON Students.UserId = Users.UserId INNER JOIN Classes ON Students.ClassId = Classes.Id WHERE Students.UserId = @userId;";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    adaptar = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adaptar.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        row = dt.Rows[0];
                        return new StudentModel
                        {
                            Password = Convert.ToString(row["Password"]),
                            UserId = Convert.ToInt32(row["UserId"]),
                            StudentId = Convert.ToInt32(row["StudentId"]),
                            FullNameAr = row["FullNameAr"].ToString(),
                            FullNameEn = row["FullNameEn"].ToString(),
                            ClassId = Convert.ToInt32(row["ClassId"]),
                            Age = Convert.ToInt32(row["Age"]),
                            Gender = Convert.ToInt32(row["Gender"]),
                            ClassName = (row["ClassNameAr"]).ToString(),
                            UserType = Convert.ToInt32(row["UserType"]),
                            UserName = (row["UserName"]).ToString()
                        };
                    }
                    return null;
                }
            }
        }
    }
}
