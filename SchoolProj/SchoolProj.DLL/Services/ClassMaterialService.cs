using SchoolProj.DLL.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SchoolProj.DLL.Services
{
    public class ClassMaterialService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        SqlConnection con;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataTable dt;
        DataRow row;
        SqlParameter outputParameter;
        public List<MaterialModel> GetClassMaterials(int classId)
        {
            List<MaterialModel> materials = new List<MaterialModel>();
            using (con = new SqlConnection(connectionString))
            {
                string command = "SP_GetClassMaterials";
                cmd = new SqlCommand(command, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@classId", classId);
                dt = new DataTable();
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    MaterialModel material = new MaterialModel
                    {
                        Id = Convert.ToInt32(row["MaterialId"]),
                        MaterialNameAr = Convert.ToString(row["MaterialNameAr"]),
                        MaterialNameEn = Convert.ToString(row["MaterialNameEn"])
                    };
                    materials.Add(material);
                }
            }
            return materials;
        }

        public void AddClassMaterial(ClassMaterialModel classMaterial)
        {
            string command = "Insert Into ClassMaterials(ClassId,MaterialId) values(@classId,@materialId);";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.Parameters.AddWithValue("@classId", classMaterial.ClassId);
                    cmd.Parameters.AddWithValue("@materialId", classMaterial.MaterialId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddClassMaterials(int classId,List<int?> materilesIds)
        {
            foreach(int materialId in materilesIds)
            {
                AddClassMaterial(new ClassMaterialModel
                {
                    ClassId = classId,
                    MaterialId=materialId   
                });
            }
        }

        public void DeleteClassMaterils(int classId)
        {
            using (con = new SqlConnection(connectionString))
            {
                string command = "Delete from ClassMaterials where ClassId=@classId";
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.Parameters.AddWithValue("@classId", classId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<ClassModel> GetClassesByMaterialId(int materialId)
        {
            List<ClassModel> classes = new List<ClassModel>();
            using (con = new SqlConnection(connectionString))
            {
                string command = "SP_SelectClassByMaterialId";
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@materialId", materialId);
                    dt = new DataTable();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        ClassModel classToAdd = new ClassModel
                        {
                            Id = Convert.ToInt32(row["ClassId"]),
                            ClassNameAr = Convert.ToString(row["ClassNameAr"]),
                            ClassNameEn = Convert.ToString(row["ClassNameEn"])
                        };
                        classes.Add(classToAdd);
                    }
                }
            }
            return classes;
        }

    }
}
