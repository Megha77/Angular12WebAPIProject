using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"SELECT [EmployeeId]
                              ,[EmployeeName]
                              ,[Department]
                              ,convert(varchar(10),DateOfJoining,120) as DateOfJoining
                              ,[PhotoFileName]
                          FROM [dbo].[Employee] ";

            DataTable table = new DataTable();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd)) {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK,table);
        }

        public string Post(Employee emp)
        {
            try
            {
                string query = @"Insert into dbo.Employee values('"+emp.EmployeeName+@"','"+emp.Department+@"','"+emp.DateOfJoining+@"','"+emp.PhotoFileName+@"')";

                DataTable table = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added Sucessfully!!";
            }
            catch (Exception)
            {
                return "Failed to Add!!";
            }
        }

        public string Put(Employee emp)
        {           
            try
            {
                string query = @"Update dbo.Employee set EmployeeName='"+emp.EmployeeName+"',Department='"+emp.Department+"',DateOfJoining='"+emp.DateOfJoining+"',PhotoFileName='"+emp.PhotoFileName+"' where EmployeeId='"+emp.EmployeeId+"'";

                DataTable table = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Update Sucessfully!!";
            }
            catch (Exception)
            {
                return "Failed to Update!!";
            }

        }

        public string Delete(int Id)
        {
            try
            {
                string query = @"Delete from dbo.Employee where EmployeeId='"+Id+"'";

                DataTable table = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Delete Sucessfully!!";
            }
            catch (Exception)
            {
                return "Failed to Delete!!";
            }
        }

        [Route("api/Employee/GetAllDepartmentName")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartmentName()
        {
            string query = @"Select DepartmentName from dbo.Department";

            DataTable table = new DataTable();

            using (var con=new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd=new SqlCommand(query,con))
            using (var da= new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/Employee/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                var fileName = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + fileName);

                postedFile.SaveAs(physicalPath);
                return fileName;
            }
            catch (Exception)
            {
                return "ananomous.jpg";
            }
        
        }
    }
}
