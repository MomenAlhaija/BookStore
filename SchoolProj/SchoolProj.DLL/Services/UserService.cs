using SchoolProj.DLL.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SchoolProj.DLL.Services
{
    public class UserService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        SqlConnection con;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataTable dt;
        DataRow row;
        SqlParameter outputParameter;
        public List<UserModel> GetUsers()
        {
            using(con=new SqlConnection(connectionString)) {
                using (cmd = new SqlCommand("SP_Select_Users", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    adapter = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adapter.Fill(dt);
                    List<UserModel> users = new List<UserModel>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        users.Add(new UserModel
                        {
                            UserId = Convert.ToInt32(dr["UserId"]),
                            FullName = dr["FullName"].ToString(),
                            UserType = Convert.ToInt32(dr["UserType"]),
                            UserName = (dr["UserName"]).ToString(),
                            Password = dr["Password"].ToString(),
                        });
                    }

                    return users;
                }
            }
        }
        public UserModel GetUserByID(int userId)
        {
            string command = "Select*from Users where UserId=@userId";
            using (con = new SqlConnection(connectionString))
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
                        return new UserModel()
                        {
                            UserId = Convert.ToInt32(row["UserId"]),
                            FullName = row["FullName"].ToString(),
                            UserType = Convert.ToInt32(row["UserType"]),
                            UserName = (row["UserName"]).ToString(),
                            Password = row["Password"].ToString(),
                        };
                    }
                    return null;
                }
            }
        }
        public bool AddOrUpdateUser(UserModel user) 
        {
            string command = "SP_AddOrUpdateUser";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userId", user.UserId);
                    cmd.Parameters.Add("@userName", user.UserName);
                    cmd.Parameters.Add("@password", user.Password);
                    cmd.Parameters.Add("@fullName", user.FullName);
                    cmd.Parameters.Add("@UserType", user.UserType);
                    outputParameter = new SqlParameter("@isUpdate", SqlDbType.Bit);
                    outputParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParameter);
                    adapter = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adapter.Fill(dt);
                    return (bool)cmd.Parameters["@isUpdate"].Value;
                }
            }
        }
        public void DeleteUser(int userId)
        {
            string command = "Delete from Users where UserId=@userId";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public bool ResetPassword(ResetPasswordModel resetPassword)
        {
            var user = GetUserByID(resetPassword.UserId.Value);
            if (user != null)
            {
                if (resetPassword.NewPassword == resetPassword.ConfirmNewPassword)
                {
                    AddOrUpdateUser(new UserModel
                    {
                        UserName=user.UserName, 
                        Password=resetPassword.NewPassword,
                        UserType=user.UserType, 
                        UserId=user.UserId
                    });
                    return true;
                }
                throw new Exception("Password and Confirm Password Must By Match");
            }
            else
                throw new Exception("Not Found The User");
        }
    }
}
