using SchoolProj.DLL.Model;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SchoolProj.DLL.Services
{
    public class MarkService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        SqlConnection con;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataTable dt;
        DataRow row;
        public void AddOrUpdateMark(MarkModel mark)
        {
            string command = "SP_AddMark";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@classId", mark.ClassId);
                    cmd.Parameters.Add("@materialId", mark.MaterialId);
                    cmd.Parameters.Add("@studentId", mark.StudentId);
                    cmd.Parameters.Add("@mark", mark.Grade);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public decimal? GetMarkInMaterialForStudent(GetMarkInMaterialModel model)
        {
            string command = "select*from Marks where Marks.ClassId=@classId and Marks.MaterialId=@materialId and Marks.StudentId=@studentId";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.Parameters.AddWithValue("@classId", model.ClassId);
                    cmd.Parameters.AddWithValue("@materialId", model.MaterialId);
                    cmd.Parameters.AddWithValue("@studentId", model.StudentId);
                    adapter = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        row = dt.Rows[0];
                        return Convert.ToDecimal(row["MarkInPercent"]);
                    }
                    return null;
                }
            }
        }
    }
}
