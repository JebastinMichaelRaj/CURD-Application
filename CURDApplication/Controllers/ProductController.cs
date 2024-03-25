using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CURDApplication.Models;

namespace CURDApplication.Controllers
{
    public class ProductController : Controller
    {
        string Cont = "Data Source=RRR_2013_1;Initial Catalog=MvcCurd;Integrated Security=True";
        // GET: Product
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dt_product = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(Cont))
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM mcProduct", sqlcon);
                sqlda.Fill(dt_product);
            }
            return View(dt_product);
        }

        [HttpGet]

        // GET: Product/Create
        public ActionResult Create()
        {

            return View(new Product());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(Cont))
                {
                    sqlcon.Open();
                    string qy = "INSERT INTO mcProduct VALUES (@Pro_Name,@Pro_Price,@Pro_Count)";
                    SqlCommand sqlcmd = new SqlCommand(qy, sqlcon);
                    sqlcmd.Parameters.AddWithValue("@Pro_Name", product.Pro_Name);
                    sqlcmd.Parameters.AddWithValue("@Pro_Price", product.Pro_Price);
                    sqlcmd.Parameters.AddWithValue("@Pro_Count", product.Pro_Count);
                    sqlcmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = new Product();
            DataTable dt_product = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(Cont))
            {
                sqlcon.Open();
                string qy = "SELECT * FROM mcProduct WHERE Pro_ID = @Pro_ID";
                SqlDataAdapter sqlda = new SqlDataAdapter(qy, sqlcon);
                sqlda.SelectCommand.Parameters.AddWithValue("@Pro_ID", id);
                sqlda.Fill(dt_product);
            }
            if (dt_product.Rows.Count == 1)
            {
                product.Pro_ID = Convert.ToInt32(dt_product.Rows[0][0].ToString());
                product.Pro_Name = dt_product.Rows[0][1].ToString();
                product.Pro_Price = dt_product.Rows[0][2].ToString();
                product.Pro_Count = dt_product.Rows[0][3].ToString();
                return View(product);

            }
            else
            {
                return RedirectToAction("Index");

            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(Cont))
                {
                    sqlcon.Open();
                    string qy = "UPDATE mcProduct SET Pro_Name = @Pro_Name, Pro_Price = @Pro_Price, Pro_Count = @Pro_Count WHERE Pro_ID = @Pro_ID";
                    SqlCommand sqlcmd = new SqlCommand(qy, sqlcon);
                    sqlcmd.Parameters.AddWithValue("@Pro_ID", product.Pro_ID);
                    sqlcmd.Parameters.AddWithValue("@Pro_Name", product.Pro_Name);
                    sqlcmd.Parameters.AddWithValue("@Pro_Price", product.Pro_Price);
                    sqlcmd.Parameters.AddWithValue("@Pro_Count", product.Pro_Count);
                    sqlcmd.ExecuteNonQuery();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(Cont))
            {
                sqlcon.Open();
                string qy = "DELETE FROM mcProduct WHERE Pro_ID = @Pro_ID";
                SqlCommand sqlcmd = new SqlCommand(qy, sqlcon);
                sqlcmd.Parameters.AddWithValue("@Pro_ID", id);
                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }


    }
}