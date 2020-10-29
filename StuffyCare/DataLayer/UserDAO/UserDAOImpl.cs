using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using StuffyCare.Facade;
using StuffyCare.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StuffyCare.DataLayer.UserDAO
{
    public class UserDAOImpl:Connection,IUserDAO
    {
        
        string ConStr = GetConnectionString();
        private readonly StuffyCareContext context;
        public UserDAOImpl()
        {
            context = new StuffyCareContext();
        }
        public string AddUser(string email,string pass,string pno)
        {
            var timer = new Stopwatch();
            var _result = String.Empty;
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConStr;
            sqlConnection.Open();
            try
            {


                using (var cmd = new SqlCommand("user_create", sqlConnection))
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
            }
            catch (Exception e)
            {
                _result = e.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return _result;
        }

        public string Auth(string email, string pass)
        {
            var timer = new Stopwatch();
            var _result = String.Empty;
            SqlConnection sqlConnection = new SqlConnection();
            //sqlConnection.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
            sqlConnection.ConnectionString = ConStr;
            sqlConnection.Open();
            try
            {
                using (var cmd = new SqlCommand("user_auth", sqlConnection))
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
            }
            catch(Exception e)
            {
                _result = e.Message;
            }
            finally {
                sqlConnection.Close();
            }
            return _result;
        }

        public List<Orders> GetOrder(string userid)
        {
            List<Orders> retobj = new List<Orders>();
            try
            {
                
                retobj = (from user in context.Orders
                          where user.Userid == userid
                          select user
                            ).ToList();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retobj;
        }

        public Users GetUser(string emailid)
        {
            Users obj = new Users();
            try
            {
                obj = (from user in context.Users
                       where user.Email == emailid
                       select user).FirstOrDefault();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                obj = null;
            }
            return obj;
        }

        public List<Appointments> GetAppointments(string userid)
        {
            List<Appointments> retobj = new List<Appointments>();
            try
            {

                retobj = (from user in context.Appointments
                          where user.Userid == userid
                          select user
                            ).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retobj;
        }
    }
}
