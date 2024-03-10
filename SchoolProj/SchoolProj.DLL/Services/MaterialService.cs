using SchoolProj.DLL.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SchoolProj.DLL.Services
{
    public class MaterialService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        SqlConnection con;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataTable dt;
        DataRow row;
        SqlParameter outputParameter;
        public List<MaterialModel> GetMaterials() {

            List<MaterialModel> materials = new List<MaterialModel>();
            string command = "SELECT * FROM Materials";
            using (con = new SqlConnection(connectionString))
            {
                adapter = new SqlDataAdapter(command, con);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    MaterialModel material = new MaterialModel
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        MaterialNameAr = Convert.ToString(row["MaterialNameAr"]),
                        MaterialNameEn = row["MaterialNameEn"].ToString(),
                    };

                    materials.Add(material);
                }
            }
            return materials;
        }
        public MaterialModel GetMaterial(int id)
        {
            string command = "SELECT * FROM Materials where Id=@materialId";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.Parameters.AddWithValue("@materialId", id);
                    adapter = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adapter.Fill(dt);
                    row = dt.Rows[0];
                    if (dt.Rows.Count > 0)
                    {
                        return new MaterialModel
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            MaterialNameAr = Convert.ToString(row["MaterialNameAr"]),
                            MaterialNameEn = row["MaterialNameEn"].ToString(),
                        };
                    }
                    return null;
                }
            }
        }
        public void AddMaterial(MaterialModel material)
        {
            string command = "Insert Into Materials(MaterialNameAr,MaterialNameEn) values(@materialNameAr,@materialNameEn)";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.Parameters.AddWithValue("@materialNameAr", material.MaterialNameAr);
                    cmd.Parameters.AddWithValue("@materialNameEn", material.MaterialNameEn!=null?material.MaterialNameEn:DBNull.Value.ToString());
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void UpdateMaterial(MaterialModel material)
        {
            string command = "UPDATE Materials SET MaterialNameAr=@materialNameAr, MaterialNameEn= @materialNameEn WHERE Id=@id";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.Parameters.AddWithValue("@materialNameAr", material.MaterialNameAr);
                    cmd.Parameters.AddWithValue("@materialNameEn", material.MaterialNameEn!=null?material.MaterialNameEn:DBNull.Value.ToString());
                    cmd.Parameters.AddWithValue("@id", material.Id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DeleteMaterial(int materialId)
        {
            string command = "Delete from Materials where Id=@materialId";
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand(command, con))
                {
                    cmd.Parameters.AddWithValue("@materialId", materialId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
