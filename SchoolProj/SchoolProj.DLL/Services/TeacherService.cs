using SchoolProj.DLL.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SchoolProj.DLL.Services
{
    public class TeacherService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        SqlConnection con;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataTable dt;
        DataRow row;
        SqlParameter outputParameter;
        public List<TeacherModel> GetTeachers()
        {
            List<TeacherModel> teachers = new List<TeacherModel>();
            string command = "SELECT * FROM Teachers INNER JOIN Users ON  Teachers.UserId = Users.UserId INNER JOIN Materials ON Teachers.MaterialId = Materials.Id";
            using (con = new SqlConnection(connectionString))
            {
                adapter = new SqlDataAdapter(command, con);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    TeacherModel teacher = new TeacherModel
                    {
                        UserId = Convert.ToInt32(row["UserId"]),
                        TeacherId = Convert.ToInt32(row["TeacherId"]),
                        FullNameAr = row["FullNameAr"].ToString(),
                        FullNameEn = row["FullNameEn"].ToString(),
                        MaterialId = Convert.ToInt32(row["Id"]),
                        Gender = Convert.ToInt32(row["Gender"]),
                        MaterialName = row["MaterialNameAr"].ToString(),
                        UserType = Convert.ToInt32(row["UserType"]),
                        UserName = row["UserName"].ToString(),
                        Password = row["Password"].ToString()
                    };

                    teachers.Add(teacher);
                }
                return teachers;
            }
        }
        public void AddTeacher(TeacherModel teacher)
        {
            string command = "SP_CreateTeacher";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userName", teacher.UserName);
                    cmd.Parameters.Add("@password", teacher.Password);
                    cmd.Parameters.Add("@fullNameAr", teacher.FullNameAr);
                    cmd.Parameters.Add("@fullNameEn", teacher.FullNameEn);
                    cmd.Parameters.Add("@gender", teacher.Gender);
                    cmd.Parameters.Add("@materialId", teacher.MaterialId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public TeacherModel GetTeacherById(int teacherId)
        {
            string command = "SELECT * FROM Teachers INNER JOIN Users ON  Teachers.UserId = Users.UserId INNER JOIN Materials ON Teachers.MaterialId = Materials.Id where Teachers.TeacherId=@teacherId;";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.Parameters.AddWithValue("@teacherId", teacherId);
                    adapter = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        row = dt.Rows[0];
                        return new TeacherModel
                        {
                            UserId = Convert.ToInt32(row["UserId"]),
                            TeacherId = Convert.ToInt32(row["TeacherId"]),
                            FullNameAr = row["FullNameAr"].ToString(),
                            FullNameEn = row["FullNameEn"].ToString(),
                            MaterialId = Convert.ToInt32(row["Id"]),
                            Gender = Convert.ToInt32(row["Gender"]),
                            MaterialName = row["MaterialNameAr"].ToString(),
                            UserType = Convert.ToInt32(row["UserType"]),
                            UserName = row["UserName"].ToString(),
                            Password = row["Password"].ToString()
                        };
                    }
                    return null;
                }
            }
        }
        public TeacherModel GetTeacherByUserId(int userId)
        {
            string command = "SELECT * FROM Teachers INNER JOIN Users ON Teachers.UserId = Users.UserId where Teachers.UserId = @userId;";
            using(con=new SqlConnection(connectionString)) 
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    adapter = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        row = dt.Rows[0];
                        return new TeacherModel
                        {
                            UserId = Convert.ToInt32(row["UserId"]),
                            TeacherId = Convert.ToInt32(row["TeacherId"]),
                            FullNameAr = row["FullNameAr"].ToString(),
                            FullNameEn = row["FullNameEn"].ToString(),
                            Gender = Convert.ToInt32(row["Gender"]),
                            UserType = Convert.ToInt32(row["UserType"]),
                            UserName = row["UserName"].ToString(),
                            Password = row["Password"].ToString()
                        };
                    }
                    return null;
                }
            }
        }
        public void UpdateTeacher(TeacherModel teacher)
        {
            string command = "SP_UpdateTeacher";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@teacherId", teacher.TeacherId);
                    cmd.Parameters.Add("@fullNameAr", teacher.FullNameAr);
                    cmd.Parameters.Add("@fullNameEn", teacher.FullNameEn);
                    cmd.Parameters.Add("@userName", teacher.UserName);
                    cmd.Parameters.Add("@userId", teacher.UserId);
                    cmd.Parameters.Add("@gender", teacher.Gender);
                    cmd.Parameters.Add("@materialId", teacher.MaterialId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTeacher(int teacherId,int userId)
        {
            string command = "Delete from Teachers where TeacherId=@teacherId;" +
                             "Delete from Users where UserId=@userId";
            using (con = new SqlConnection(connectionString)) 
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.Parameters.AddWithValue("@teacherId", teacherId);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
