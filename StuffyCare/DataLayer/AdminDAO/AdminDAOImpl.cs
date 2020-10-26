using Microsoft.Extensions.Configuration;
using StuffyCare.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StuffyCare.DataLayer.AdminDAO
{
    public class AdminDAOImpl:IAdminDAO
    {
        //connection string initialization
        private readonly IConfiguration _configuration;
        string ConStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StuffyCare;Trusted_Connection=True;";
        //Function for Adding admin
        public string AddAdmin(string email, string pass, string pno)
        {
            var timer = new Stopwatch();
            var _result = String.Empty;
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConStr;
            sqlConnection.Open();
            using (var cmd = new SqlCommand("admin_create", sqlConnection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Pass", pass);
                cmd.Parameters.AddWithValue("@pno", pno);
                var _output = cmd.Parameters.Add("@ret", SqlDbType.VarChar, 100);
                _output.Direction = ParameterDirection.Output;
                timer.Start();
                cmd.ExecuteScalar();
                timer.Stop();
                _result = _output.Value.ToString();
            }
            return _result;
        }
        //Fuction for Authentication of Admin
        public string Auth(string email, string pass)
        {
            var timer = new Stopwatch();
            var _result = String.Empty;
            SqlConnection sqlConnection = new SqlConnection();
            //sqlConnection.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
            sqlConnection.ConnectionString = ConStr;
            sqlConnection.Open();
            using (var cmd = new SqlCommand("admin_auth", sqlConnection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Pass", pass);
                var _output = cmd.Parameters.Add("@Role", SqlDbType.VarChar, 20);
                _output.Direction = ParameterDirection.Output;
                timer.Start();
                cmd.ExecuteScalar();
                timer.Stop();
                _result = _output.Value.ToString();
            }
            sqlConnection.Close();
            return _result;
        }
        //Function for getting Appointments by category when category is all, all appointements are retrived else only the category appointments are retrieved
        public List<Appointments> GetAllAppointments(string category)
        {
            List<Appointments> obj= new List<Appointments>();
            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = new SqlCommand("get_allappointments", conn);
                    da.SelectCommand.Parameters.AddWithValue("@category", category);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    
                    DataSet ds = new DataSet();
                    da.Fill(ds, "user");
                    DataTable dt = ds.Tables["user"];
                    foreach (DataRow row in dt.Rows)
                    {
                        obj.Add(new Appointments(Convert.ToInt32(row["aptid"]), row["emailid"].ToString(), row["pno"].ToString(), row["dt"].ToString(), row["tm"].ToString(), row["servicetype"].ToString(), row["address"].ToString(), row["message"].ToString()));
                    }
                }
            }
            return obj;
        }
        //Function to add appointmets to the database
        public string AddAppointments(Appointments appointments)
        {
            string ret = "Could not Add Appointment";
            try
            {
                var timer = new Stopwatch();
                var res = String.Empty;
                using (SqlConnection sqlConnection = new SqlConnection())
                { 
                    //sqlConnection.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
                    sqlConnection.ConnectionString = ConStr;
                    sqlConnection.Open();
                    using (var cmd = new SqlCommand("add_appointment", sqlConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", appointments.Email);
                        cmd.Parameters.AddWithValue("@pno", appointments.Pno);
                        cmd.Parameters.AddWithValue("@dt", appointments.Dt);
                        cmd.Parameters.AddWithValue("@tm", appointments.Tm);
                        cmd.Parameters.AddWithValue("@servicetype", appointments.ServiceType);
                        cmd.Parameters.AddWithValue("@address", appointments.Address);
                        cmd.Parameters.AddWithValue("@message", appointments.Message);
                        var _output = cmd.Parameters.Add("@ret", SqlDbType.VarChar, 100);
                        _output.Direction = ParameterDirection.Output;
                        timer.Start();
                        cmd.ExecuteScalar();
                        timer.Stop();
                        ret = _output.Value.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                ret = "Could not Add Asppointment";
                throw e;
            }
            return ret;
        }

        //Function To get all the users in the database
        public List<Users> GetUser(string email)
        {
            Users obj = new Users();
            List<Users> retobj = new List<Users>();
            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = new SqlCommand("get_user", conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@email", email);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "user");
                    DataTable dt = ds.Tables["user"];
                    foreach (DataRow row in dt.Rows)
                    {
                        obj.Email = row["email"].ToString();
                        obj.Pass = row["pass"].ToString();
                        obj.Pno = row["pno"].ToString();
                        retobj.Add(obj);
                    }
                }
            }
            return retobj;
        }
        public List<Items> GetItem(string itemid)
        {
            Items obj = new Items();
            List<Items> retobj = new List<Items>();
            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = new SqlCommand("get_items", conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@itemid",Convert.ToInt32(itemid));
                    DataSet ds = new DataSet();
                    da.Fill(ds, "user");
                    DataTable dt = ds.Tables["user"];
                    foreach (DataRow row in dt.Rows)
                    {
                        obj.Itemid = Convert.ToInt32(row["itemid"]);
                        obj.Name = row["name"].ToString();
                        obj.Description = row["description"].ToString();
                        obj.Category = row["category"].ToString();
                        obj.Price = float.Parse(row["price"].ToString());
                        obj.Sku = row["sku"].ToString();
                        obj.Saleprice = float.Parse(row["saleprice"].ToString());
                        obj.Quantity = Convert.ToInt32(row["quantity"].ToString());
                        obj.Moa = Convert.ToInt32(row["moa"].ToString());
                        obj.Photo = row["photo"].ToString();
                        retobj.Add(obj);
                    }
                }
            }
            return retobj;
        }

        public string AddItem(Items items)
        {
            string ret = "Could not Add item";
            try
            {
                var timer = new Stopwatch();
                var res = String.Empty;
                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    //sqlConnection.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
                    sqlConnection.ConnectionString = ConStr;
                    sqlConnection.Open();
                    using (var cmd = new SqlCommand("add_item", sqlConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", items.Name);
                        cmd.Parameters.AddWithValue("@description", items.Description);
                        cmd.Parameters.AddWithValue("@category", items.Category);
                        cmd.Parameters.AddWithValue("@price", items.Price);
                        cmd.Parameters.AddWithValue("@sku", items.Sku);
                        cmd.Parameters.AddWithValue("@saleprice", items.Saleprice);
                        cmd.Parameters.AddWithValue("@quantity", items.Quantity);
                        cmd.Parameters.AddWithValue("@moa", items.Moa);
                        cmd.Parameters.AddWithValue("@photo", items.Photo);
                        var _output = cmd.Parameters.Add("@ret", SqlDbType.VarChar, 100);
                        _output.Direction = ParameterDirection.Output;
                        timer.Start();
                        cmd.ExecuteScalar();
                        timer.Stop();
                        ret = _output.Value.ToString();
                        sqlConnection.Close();
                    }
                }
                
            }
            catch (Exception e)
            {
                ret = "Could not Add Asppointment";
                throw e;
            }
            return ret;
        }
    }
    
}
