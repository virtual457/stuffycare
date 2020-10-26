using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace StuffyCare.DataLayer.VendorDAO
{
    public class VendorDAOImpl:IVendorDAO
    {
        private readonly IConfiguration _configuration;
        string ConStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StuffyCare;Trusted_Connection=True;";
        string IVendorDAO.AddVendor(string email, string pass, string pno)
        {
            var timer = new Stopwatch();
            var _result = String.Empty;
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConStr;
            sqlConnection.Open();
            using (var cmd = new SqlCommand("add_vendor", sqlConnection))
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

        string IVendorDAO.Auth(string email, string pass)
        {
            var timer = new Stopwatch();
            var _result = String.Empty;
            SqlConnection sqlConnection = new SqlConnection();
            //sqlConnection.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
            sqlConnection.ConnectionString = ConStr;
            sqlConnection.Open();
            using (var cmd = new SqlCommand("vendor_auth", sqlConnection))
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
    }
}
