using SchoolProj.DLL.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SchoolProj.DLL.Services
{
    public class ClassService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        SqlConnection con;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataTable dt;
        DataRow row;
        ClassMaterialService classMaterialService=new ClassMaterialService();
        public List<ClassModel> GetClasses() 
        {
            List<ClassModel> classes = new List<ClassModel>();
            string command = "select*from Classes";
            using (con = new SqlConnection(connectionString))
            {
                adapter = new SqlDataAdapter(command, con);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    ClassModel classToAdd = new ClassModel
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        ClassNameAr = Convert.ToString(row["ClassNameAr"]),
                        ClassNameEn = row["ClassNameEn"].ToString(),
                    };
                    classes.Add(classToAdd);
                }
                foreach (var cl in classes)
                {
                    cl.Materials = classMaterialService.GetClassMaterials(cl.Id.Value);
                }
            }
            return classes;
        }
        public ClassModel GetClassById(int ClassId)
        {
            ClassModel classToRetrive = null;

            using (con= new SqlConnection(connectionString))
            {
                string commandText = "SELECT * FROM Classes WHERE Id = @classId";
                adapter = new SqlDataAdapter(commandText, con);
                adapter.SelectCommand.Parameters.AddWithValue("@classId", ClassId);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Classes");
                if (dataSet.Tables["Classes"].Rows.Count > 0)
                {
                    row = dataSet.Tables["Classes"].Rows[0];
                    classToRetrive = new ClassModel
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        ClassNameAr = Convert.ToString(row["ClassNameAr"]),
                        ClassNameEn = Convert.ToString(row["ClassNameEn"]),
                        Materials = classMaterialService.GetClassMaterials(ClassId)
                    };
                }
            }
            return classToRetrive;
        }
        public void UpdateClassMaterial(ClassModel classToUpdate)
        {
            string command = "update Classes set ClassNameAr=@classNameAr,ClassNameEn=@classNameEn where Id=@classId;";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.Parameters.AddWithValue("@classNameAr", classToUpdate.ClassNameAr);
                    cmd.Parameters.AddWithValue("@classNameEn", classToUpdate.ClassNameEn!=null?classToUpdate.ClassNameEn:DBNull.Value.ToString());
                    cmd.Parameters.AddWithValue("@classId", classToUpdate.Id.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            classMaterialService.DeleteClassMaterils(classToUpdate.Id.Value);
            classMaterialService.AddClassMaterials(classToUpdate.Id.Value,classToUpdate.MaterialsIds.ToList());
        }
        
        public void AddClass(ClassModel classToAdd)
        {
            string command = "insert into Classes(ClassNameAr,ClassNameEn) values(@classNameAr,@classNameEn); SELECT SCOPE_IDENTITY();";
           using(con= new SqlConnection(connectionString)) 
           {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.Parameters.AddWithValue("@classNameAr", classToAdd.ClassNameAr);
                    cmd.Parameters.AddWithValue("@classNameEn", classToAdd.ClassNameEn!=null?classToAdd.ClassNameEn:DBNull.Value.ToString());
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    classToAdd.Id = (result != null && result != DBNull.Value) ? Convert.ToInt32(result) :
                                    throw new Exception("Failed to retrieve classId after insertion.");
                    classToAdd.Id = Convert.ToInt32(result);
                }
            }
           classMaterialService.AddClassMaterials(classToAdd.Id.Value, classToAdd.MaterialsIds.ToList());
        }
        public void DeleteClass(int classId)
        {
            classMaterialService.DeleteClassMaterils(classId);
            string command = "Delete from Classes where Id=@classId";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.Parameters.AddWithValue("@classId", classId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
