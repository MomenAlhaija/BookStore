using SchoolProj.DLL.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SchoolProj.DLL.Services
{

    public class StudentMarksService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        SqlConnection con;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataTable dt;
        StudentService studentService=new StudentService();
        public StudentMarksModel GetStudentMarks(int userId)
        {
            var markStudent=new StudentMarksModel();
            var getstudent=studentService.GetStudentByUserId(userId);
            markStudent.student=getstudent;
            if (getstudent == null)
            {
                throw new Exception("Not Found The Student");
            }
            string command = "SP_GetMarkForStudent";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studentId", getstudent.StudentId.Value);
                    cmd.Parameters.AddWithValue("@classId", getstudent.ClassId);
                    adapter = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    
                        adapter.Fill(dt);
                        DataRow commval = dt.Rows[0];
                        markStudent.ClasseId = getstudent.ClassId.Value;
                        markStudent.ClassNameAr = Convert.ToString(commval["ClassNameAr"]);
                        markStudent.ClassNameEn = Convert.ToString(commval["ClassNameEn"]);
                        List<MarkModel> marks = new List<MarkModel>();
                        foreach (DataRow row in dt.Rows)
                        {
                            var materialNameAr = Convert.ToString(row["MaterialNameAr"]);
                            var materialNameEn = Convert.ToString(row["MaterialNameEn"]);
                            var grade = row["Mark"] != DBNull.Value ? Convert.ToDecimal(row["Mark"]) : (decimal?)null;
                            if (grade.HasValue || grade == 0)
                            {
                                marks.Add(new MarkModel
                                {
                                    MaterialNameAr = materialNameAr,
                                    MaterialNameEn = materialNameEn,
                                    Grade = grade
                                });
                            }
                            else
                            {
                                marks.Add(new MarkModel
                                {
                                    MaterialNameAr = materialNameAr,
                                    MaterialNameEn = materialNameEn,
                                    Grade = null
                                });
                            }
                        }
                        markStudent.Marks = marks;
                        return markStudent;
                    
                }
            }
        }
    }
}
