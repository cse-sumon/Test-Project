using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using WebApiIdentityDemo.Models;

namespace WebApiIdentityDemo.DataAccessLayer
{
    public class CategoryLayer
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        public DataTable getCategory()
        {
            SqlCommand cmd = new SqlCommand("Sp_Get_Category", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }

        public void addCategory(Category model)
        {
            SqlCommand com = new SqlCommand("Sp_Add_Category", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", model.Name);
            com.Parameters.AddWithValue("@Status", model.Status);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }

        public void updateCategory(Category category)
        {
            SqlCommand com = new SqlCommand("Sp_Update_Category",con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", category.Id);
            com.Parameters.AddWithValue("@Name", category.Name);
            com.Parameters.AddWithValue("@Status", category.Status);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }

        public void deleteCategory(int id)
        {
            SqlCommand com = new SqlCommand("Sp_Delete_Category", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", id);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            
        }

        public int totalCategoryCount()
        {
            SqlCommand com = new SqlCommand("Sp_TotalCategory", con);
            com.CommandType = CommandType.StoredProcedure;
            con.Open();
            Int32 totalCategory = (Int32)com.ExecuteScalar();
            con.Close();
            return totalCategory;
        }


    }
}