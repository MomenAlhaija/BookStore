using SchoolProj.DLL.Model;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SchoolProj.DLL.Services
{
    public class StaticsService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        SqlConnection con;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataTable dt;
        DataRow row;
        TeacherService teacherService=new TeacherService();
        public StaticsModel GetStatics()
        {
            string command = "SP_Statics";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    adapter = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        row = dt.Rows[0];
                        var statics = new StaticsModel
                        {
                            TotalUsers = Convert.ToInt32(row["CountUsers"]),
                            TotalStudents = Convert.ToInt32(row["CountStudents"]),
                            TotalTeachers = Convert.ToInt32(row["CountTeachers"]),
                            TotalClasses = Convert.ToInt32(row["CountClasses"])
                        };
                        statics.Teachers = teacherService.GetTeachers();
                        return statics;
                    }
                    return null;
                }
            }
        }
    }
}
