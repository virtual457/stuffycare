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
    public class UserDAOImpl:IUserDAO
    {
        private readonly IConfiguration _configuration;
        string ConStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StuffyCare;Trusted_Connection=True;";
        public string AddUser(string email,string pass,string pno)
        {
            var timer = new Stopwatch();
            var _result = String.Empty;
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConStr;
            sqlConnection.Open();
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
            return _result;
        }
        public Users GetUser(string email)
        {
            Users obj = new Users();
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
                    }
                }
            }
                return obj;
        }
    }
}
