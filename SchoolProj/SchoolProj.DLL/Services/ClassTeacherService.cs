using SchoolProj.DLL.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SchoolProj.DLL.Services
{
    public class ClassTeacherService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        SqlConnection con;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataTable dt;
        DataRow row;
        SqlParameter outputParameter;
        ClassMaterialService classMaterialService = new ClassMaterialService();
        TeacherService teacherService = new TeacherService();
        MarkService markService = new MarkService();
        public ClassTeacherModel GetClassesForTeacher(int userId)
        {
            var teacher = teacherService.GetTeacherByUserId(userId);
            string command = "SP_SelectClassesForTeacher";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@teacherId", teacher.TeacherId);
                    adapter = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adapter.Fill(dt);
                    row = dt.Rows[0];
                    if (dt.Rows.Count > 0)
                    {
                        var classTeacher = new ClassTeacherModel
                        {
                            TeacherId = Convert.ToInt32(row["TeacherId"]),
                            FullNameAr = Convert.ToString(row["FullNameAr"]),
                            FullNameEn = Convert.ToString(row["FullNameEn"]),
                            MaterialFullNameAr = row["MaterialNameAr"].ToString(),
                            MaterialFullNameEn = row["MaterialNameEn"].ToString(),
                            MaterialId = Convert.ToInt32(row["MaterialId"]),
                            Gender = Convert.ToInt32(row["Gender"]),
                        };
                        classTeacher.Classes = classMaterialService.GetClassesByMaterialId(classTeacher.MaterialId);
                        return classTeacher;
                    }
                    return null;
                }
            }
        }
        public List<StudentsInClassModel> GetStudentsinClass(int classId)
        {
            List<StudentsInClassModel> students = new List<StudentsInClassModel>();
            string command = "SP_SelectStudentsInClass";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@classId", classId);
                    adapter = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            StudentsInClassModel student = new StudentsInClassModel
                            {
                                StudentId = Convert.ToInt32(row["StudentId"]),
                                StudentNameAr = Convert.ToString(row["FullNameAr"]),
                                StudentNameEn = Convert.ToString(row["FullNameEn"]),
                                ClassNameEn = row["ClassNameEn"].ToString(),
                                ClassNemeAr = row["ClassNameAr"].ToString(),
                                ClassId = Convert.ToInt32(row["ClassId"]),
                                Gender = Convert.ToInt32(row["Gender"]),
                                Age = Convert.ToInt32(row["Age"])
                            };
                            students.Add(student);
                        }
                        return students;
                    }
                    return null;
                }
            }
        }
    }
}
      