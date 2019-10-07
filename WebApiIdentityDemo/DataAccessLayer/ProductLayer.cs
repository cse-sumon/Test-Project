using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApiIdentityDemo.Models;

namespace WebApiIdentityDemo.DataAccessLayer
{
    public class ProductLayer
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        public DataTable getProduct()
        {
            SqlCommand cmd = new SqlCommand("Sp_GetProducts", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }

        public DataTable getProductById(int id)
        {
            SqlCommand cmd = new SqlCommand("Sp_GetProductById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id",id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }

        public void addProduct(Product model)
        {
            SqlCommand com = new SqlCommand("Sp_AddProducts", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@CategoryId", model.CategoryId);
            com.Parameters.AddWithValue("@Name", model.Name);
            com.Parameters.AddWithValue("@PurchasePrice", model.PurchasePrice);
            com.Parameters.AddWithValue("@SalesPrice", model.SalesPrice);
            com.Parameters.AddWithValue("@Description", model.Description);
            com.Parameters.AddWithValue("@Image", model.Image);
            com.Parameters.AddWithValue("@Status", model.Status);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }

        public DataTable editProduct(int id)
        {
            SqlCommand cmd = new SqlCommand("Sp_EditProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }

        public void updateProduct(Product model)
        {
            SqlCommand com = new SqlCommand("Sp_UpdateProducts", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", model.Id);
            com.Parameters.AddWithValue("@CategoryId", model.CategoryId);
            com.Parameters.AddWithValue("@Name", model.Name);
            com.Parameters.AddWithValue("@PurchasePrice", model.PurchasePrice);
            com.Parameters.AddWithValue("@SalesPrice", model.SalesPrice);
            com.Parameters.AddWithValue("@Description", model.Description);
            com.Parameters.AddWithValue("@Image", model.Image);
            com.Parameters.AddWithValue("@Status", model.Status);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }

        public void deleteProduct(int id)
        {
            SqlCommand com = new SqlCommand("Sp_DeleteProducts", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", id);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            
        }

        public int totalProductCount()
        {
            SqlCommand com = new SqlCommand("Sp_TotalProduct", con);
            com.CommandType = CommandType.StoredProcedure;
            con.Open();
            Int32 totalProduct = (Int32)com.ExecuteScalar();
            con.Close();
            return totalProduct;

        }


        public string getProductImage(int id)
        {
            SqlCommand cmd = new SqlCommand("Sp_GetProductImage", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            string image = cmd.ExecuteScalar().ToString();
            con.Close();
            return image;
        }

        public DataTable getProductByCategoryId(int id)
        {
            SqlCommand cmd = new SqlCommand("Sp_GetProdctByCategoryId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CategoryId", id);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            
            da.Fill(dt);
            con.Close();
            return dt;

        }
    }
}
