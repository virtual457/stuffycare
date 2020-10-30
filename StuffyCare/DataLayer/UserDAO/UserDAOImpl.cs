using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using StuffyCare.EFModels;
using StuffyCare.Facade;

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
                        cmd.Parameters.AddWithValue("@userid", appointments.Userid);
                        cmd.Parameters.AddWithValue("@pno", appointments.Pno);
                        

                        cmd.Parameters.AddWithValue("@servicetype", appointments.Servicetype);
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
                ret = e.Message;
            }
            return ret;
        }

        public List<Appointments> GetAppointments(string userid, string petid)
        {
            var listobj = new List<Appointments>();
            try
            {
                if (petid == "all")
                {
                    listobj = (from apt in context.Appointments
                               where apt.Userid == userid
                               select apt).ToList();
                }
                else
                {
                    listobj = (from apt in context.Appointments
                               where apt.Userid == userid && apt.Petid==petid
                               select apt).ToList();
                }
            }
            catch(Exception e)
            {
                listobj = null;
                throw e;
            }
            return listobj;
        }
    }
}
