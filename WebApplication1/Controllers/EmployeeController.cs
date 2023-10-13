using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data;
using System.Data.SqlClient;
using System.Web.DynamicData;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        string Str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Project\WebApplication1\WebApplication1\App_Data\MYDB.mdf;Integrated Security=True";
        // GET: Employee
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Str)) 
            {
                con.Open();
                string q = "Select * from Employee";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                da.Fill(dt);
            }
                return View(dt);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View(new Employee());
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Str))
                {
                    con.Open();
                    string q = "Insert into Employee values('"+employee.Emp_Name+"','"+ employee.Emp_City+ "','"+ employee.Emp_Mobile+ "')";
                    SqlCommand cmd = new SqlCommand(q,con);
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = new Employee();
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(Str))
            {
                con.Open();
                String q = "Select * from Employee where id=" + id;
                SqlDataAdapter da = new SqlDataAdapter(q,con);
                da.Fill(dataTable);
            }
            if (dataTable.Rows.Count == 1)
            {
                employee.id = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                employee.Emp_Name = dataTable.Rows[0][1].ToString();
                employee.Emp_City = dataTable.Rows[0][2].ToString();
                employee.Emp_Mobile = dataTable.Rows[0][3].ToString();

                return View(employee);
            }
            return RedirectToAction("Index");
            
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee employee)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Str))
                {
                    con.Open();
                    String q = "Update Employee SET Emp_Name=@Emp_Name, Emp_City=@Emp_City, Emp_Mobile=@Emp_Mobile where id=@id";
                    SqlCommand cmd = new SqlCommand(q,con);

                    cmd.Parameters.AddWithValue("@id",employee.id);
                    cmd.Parameters.AddWithValue("Emp_Name", employee.Emp_Name);
                    cmd.Parameters.AddWithValue("Emp_City", employee.Emp_City);
                    cmd.Parameters.AddWithValue("Emp_Mobile", employee.Emp_Mobile);
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection con =new SqlConnection(Str)) 
            {
                con.Open();
                String q = "Delete from Employee where id=@id";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery ();
            }
            return RedirectToAction("Index");
        }

        // POST: Employee/Delete/5
       
    }
}
