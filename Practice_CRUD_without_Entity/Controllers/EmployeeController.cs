using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Practice_CRUD_without_Entity.Models;

namespace Practice_CRUD_without_Entity.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee

            //Babar Ayaz
        string connectionstring = @"Data Source=(local);Initial Catalog=MVCDB;Integrated Security=True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dttable = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                SqlDataAdapter sqldb = new SqlDataAdapter("Select * from Employee",con);
                sqldb.Fill(dttable);

            }
                return View(dttable);
        }

        
        // GET: Employee/Create

        [HttpGet]
        public ActionResult Create()
        {
            return View(new EmployeeModel());
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(EmployeeModel Employeemodel)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                string query = "Insert into Employee values (@Name,@Designation,@Gender)";
                SqlCommand cmd = new SqlCommand(query,con);
                cmd.Parameters.AddWithValue("@Name",Employeemodel.Name);
                cmd.Parameters.AddWithValue("@Designation", Employeemodel.Designation);
                cmd.Parameters.AddWithValue("@Gender", Employeemodel.Gender);
                cmd.ExecuteNonQuery();
            }
                return RedirectToAction("Index");
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            EmployeeModel empmodel = new EmployeeModel();
            DataTable dt = new DataTable();
            using (SqlConnection coon = new SqlConnection(connectionstring))
            {
                coon.Open();
                string query = "Select * from Employee where Id = @Id";
                SqlDataAdapter sqldb = new SqlDataAdapter(query, coon);
                sqldb.SelectCommand.Parameters.AddWithValue("@Id", id);
                sqldb.Fill(dt);
            }
            if (dt.Rows.Count == 1)
            {
                empmodel.Id = Convert.ToInt16(dt.Rows[0][0].ToString());
                empmodel.Name = dt.Rows[0][1].ToString();
                empmodel.Designation = dt.Rows[0][2].ToString();
                empmodel.Gender = dt.Rows[0][3].ToString();
                return View(empmodel);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(EmployeeModel Employeemodel)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                string query = "Update Employee set Name = @Name , Designation = @Designation , Gender = @Gender where Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", Employeemodel.Id);
                cmd.Parameters.AddWithValue("@Name", Employeemodel.Name);
                cmd.Parameters.AddWithValue("@Designation", Employeemodel.Designation);
                cmd.Parameters.AddWithValue("@Gender", Employeemodel.Gender);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                string query = "Delete Employee where Id = @Id ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id",id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // POST: Employee/Delete/5
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
