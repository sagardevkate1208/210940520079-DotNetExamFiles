using MVCApplicationExam.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApplicationExam.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            List<Product> list = new List<Product>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PracticeQQ3;Integrated Security=True;Connect Timeout=30";
            cn.Open();
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = System.Data.CommandType.Text;
            cmdInsert.CommandText = "select * from Products";
            SqlDataReader dr = cmdInsert.ExecuteReader();
            while (dr.Read())
            {
                Product p = new Product();
                p.ProductId = (int)dr["ProductId"];
                p.ProductName = dr["ProductName"].ToString(); 
                p.Rate = (decimal)dr["Rate"];
                p.Description = dr["Description"].ToString(); 
                p.CategoryName = dr["CategoryName"].ToString();
                list.Add(p);
            }
            cn.Close();
            return View(list);
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(Product pr)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = @"Data Source =(localdb)\MSSQLLocalDB;Initial Catalog=PracticeQQ3;Integrated Security=True;Connect Timeout=30";
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "addprod";
                cmd.Parameters.AddWithValue("@ProductId", pr.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", pr.ProductName);
                cmd.Parameters.AddWithValue("@Rate", pr.Rate);
                cmd.Parameters.AddWithValue("@Description", pr.Description);
                cmd.Parameters.AddWithValue("@CategoryName", pr.CategoryName);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PracticeQQ3;Integrated Security=True;Connect Timeout=30";
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from Products where ProductId=@ProductId";
            cmd.Parameters.AddWithValue("@ProductId", id);
            Product obj = null;
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
                obj = new Product { ProductId = id, ProductName = dr.GetString(1), Rate = dr.GetDecimal(2), Description = dr.GetString(1), CategoryName = dr.GetString(1) };
            else
            {
                //not found
                ViewBag.ErrorMessage = "Not found";
            }
            cn.Close();
            return View(obj);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                if (product.ProductId == id)
                {
                    SqlConnection cn = new SqlConnection();
                    cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDb;Initial Catalog=PracticeQQ3;Integrated Security=True;Connect Timeout=30";
                    cn.Open();
                    SqlCommand sqlcm = new SqlCommand();
                    sqlcm.Connection = cn;
                    sqlcm.CommandType = System.Data.CommandType.Text;
                    sqlcm.CommandText = "Update Products set ProductName=@ProductName,Rate=@Rate,Description=@Description,CategoryName=@CategoryName where ProductId=@ProductId";
                    sqlcm.Parameters.AddWithValue("@ProductId", id);
                    sqlcm.Parameters.AddWithValue("@ProductName", product.ProductName);
                    sqlcm.Parameters.AddWithValue("@Rate", product.Rate);
                    sqlcm.Parameters.AddWithValue("@Description", product.Description);
                    sqlcm.Parameters.AddWithValue("@CategoryName", product.CategoryName);
                    sqlcm.ExecuteNonQuery();
                    cn.Close();
                }
                else
                {
                    Console.WriteLine("Invalid Credentials");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
