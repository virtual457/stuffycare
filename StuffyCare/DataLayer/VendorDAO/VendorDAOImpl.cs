using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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

namespace StuffyCare.DataLayer.VendorDAO
{
    public class VendorDAOImpl:Connection,IVendorDAO
    {
        
        string ConStr = GetConnectionString();
        private readonly StuffyCareContext context;
        public VendorDAOImpl()
        {
            context = new StuffyCareContext();
        }

        public string GetVendorId(string emailorphone)
        {
            string id;
            try
            {
                var obj = (from c in context.Vendors
                           where c.Email == emailorphone || c.Pno == emailorphone
                           select c).FirstOrDefault();
                id = obj.Vendorid;
            }
            catch (Exception e)
            {
                throw e;
            }
            return id;
        }

        public string VendorAddItem(Items items)
        {
            string returnstring;
            var timer = new Stopwatch();
            try
            {
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
                        cmd.Parameters.AddWithValue("@subdescription", items.Subdescription);
                        cmd.Parameters.AddWithValue("@foranimal", items.Foranimal);
                        cmd.Parameters.AddWithValue("@category", items.Category);
                        cmd.Parameters.AddWithValue("@subcategory", items.Subcategory);
                        cmd.Parameters.AddWithValue("@price", items.Price);
                        cmd.Parameters.AddWithValue("@saleprice", items.Saleprice);
                        cmd.Parameters.AddWithValue("@sku", items.Sku);
                        cmd.Parameters.AddWithValue("@quantity", items.Quantity);
                        cmd.Parameters.AddWithValue("@moa", items.Moa);
                        cmd.Parameters.AddWithValue("@addedby", items.Addedby);
                        cmd.Parameters.AddWithValue("@photo", items.Photo);
                        cmd.Parameters.AddWithValue("@length", Convert.ToDouble(items.Length));
                        cmd.Parameters.AddWithValue("@breadth", Convert.ToDouble(items.Breadth));
                        cmd.Parameters.AddWithValue("@height", Convert.ToDouble(items.Height));
                        cmd.Parameters.AddWithValue("@weight", Convert.ToDouble(items.Weight));
                        cmd.Parameters.AddWithValue("@shippingclass", items.Shippingclass);
                        cmd.Parameters.AddWithValue("@processingtime", items.Processingtime);
                        cmd.Parameters.AddWithValue("@mililitres", items.Mililitres);
                        cmd.Parameters.AddWithValue("@packsizeingrams", items.Packsizeingrams);
                        cmd.Parameters.AddWithValue("@unitcount", items.Unitcount);
                        cmd.Parameters.AddWithValue("@upsells", items.Upsells);
                        cmd.Parameters.AddWithValue("@crosssells", items.Crosssells);
                        cmd.Parameters.AddWithValue("@policylabel", items.Policylabel);
                        cmd.Parameters.AddWithValue("@shippingpolicy", items.Shippingpolicy);
                        cmd.Parameters.AddWithValue("@refundpolicy", items.Refundpolicy);
                        cmd.Parameters.AddWithValue("@cancelationpolicy", items.Cancelationpolicy);
                        cmd.Parameters.AddWithValue("@exchangepolicy", items.Exchangepolicy);
                        cmd.Parameters.AddWithValue("@storename", items.Storename);
                        cmd.Parameters.AddWithValue("@commissionfor", items.Commissionfor);
                        cmd.Parameters.AddWithValue("@commissionmode", items.Commissionmode);
                        cmd.Parameters.AddWithValue("@authorizedby", "A0000000001");
                        cmd.Parameters.AddWithValue("@authorizedstatus", "pending");
                        cmd.Parameters.AddWithValue("@deletedstatus", "notrequested");


                        var _output = cmd.Parameters.Add("@ret", SqlDbType.VarChar, 100);
                        _output.Direction = ParameterDirection.Output;
                        timer.Start();
                        cmd.ExecuteScalar();
                        timer.Stop();
                        returnstring = _output.Value.ToString();
                        sqlConnection.Close();
                    }
                }

            }
            catch (Exception e)
            {
                returnstring = e.Message;
                throw e;
            }
            return returnstring;
        }

        string IVendorDAO.AddVendor(Vendors vendor)
        {
            var timer = new Stopwatch();
            var _result = String.Empty;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    sqlConnection.ConnectionString = ConStr;
                    sqlConnection.Open();
                    using (var cmd = new SqlCommand("vendor_create", sqlConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", vendor.Name);
                        cmd.Parameters.AddWithValue("@description", vendor.Description);
                        cmd.Parameters.AddWithValue("@email", vendor.Email);
                        cmd.Parameters.AddWithValue("@pass", vendor.Pass);
                        cmd.Parameters.AddWithValue("@pno", vendor.Pno);
                        cmd.Parameters.AddWithValue("@gender", vendor.Gender);
                        cmd.Parameters.AddWithValue("@storename", vendor.StoreName);
                        cmd.Parameters.AddWithValue("@city", vendor.City);
                        cmd.Parameters.AddWithValue("@location", vendor.Location);
                        cmd.Parameters.AddWithValue("@yearsofexperience", vendor.Yearsofexperience);
                        cmd.Parameters.AddWithValue("@monfrom", vendor.Monfrom);
                        cmd.Parameters.AddWithValue("@monto", vendor.Monto);
                        cmd.Parameters.AddWithValue("@tuefrom", vendor.Tuefrom);
                        cmd.Parameters.AddWithValue("@tueto", vendor.Tueto);
                        cmd.Parameters.AddWithValue("@wedfrom", vendor.Wedfrom);
                        cmd.Parameters.AddWithValue("@wedto", vendor.Wedto);
                        cmd.Parameters.AddWithValue("@thurfrom", vendor.Thurfrom);
                        cmd.Parameters.AddWithValue("@thurto", vendor.Thurto);
                        cmd.Parameters.AddWithValue("@frifrom", vendor.Frifrom);
                        cmd.Parameters.AddWithValue("@frito", vendor.Frito);
                        cmd.Parameters.AddWithValue("@satfrom", vendor.Satfrom);
                        cmd.Parameters.AddWithValue("@satto", vendor.Satto);
                        cmd.Parameters.AddWithValue("@sunfrom", vendor.Sunfrom);
                        cmd.Parameters.AddWithValue("@sunto", vendor.Sunto);
                        cmd.Parameters.AddWithValue("@photo", vendor.Photo);
                        cmd.Parameters.AddWithValue("@homeservice", vendor.Homeservice);
                        cmd.Parameters.AddWithValue("@issellingitem", vendor.Issellingitem);
                        cmd.Parameters.AddWithValue("@photoidproof", vendor.Photoidproof);
                        var _output = cmd.Parameters.Add("@ret", SqlDbType.VarChar, 100);
                        _output.Direction = ParameterDirection.Output;
                        timer.Start();
                        cmd.ExecuteScalar();
                        timer.Stop();
                        _result = _output.Value.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                _result = e.Message;
                throw e;
                
            }
            return _result;
        }

        string IVendorDAO.Auth(string email, string pass)
        {
            var timer = new Stopwatch();
            var _result = String.Empty;
           
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection())
                {
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
                }
            }
            catch (Exception e)
            {

                _result = e.Message;
                throw e;
            }
            
            return _result;
        }

        public Vendors GetVendor(string vendorid)
        {
            Vendors retobj = new Vendors();
            try
            {
                retobj = (from vendor in context.Vendors
                          where vendor.Vendorid == vendorid && vendor.Isdeleted == false
                          select vendor).FirstOrDefault();
            }
            catch ( Exception e)
            {
                retobj = null;
                throw e;
               
            }
            return retobj;
        }
        public List<Vendorservices> GetServices(string vendorid)
        {
            List<Vendorservices> retobj = new List<Vendorservices>();
            try
            {

                retobj = (from cart in context.Vendorservices
                          where cart.Vendorid == vendorid
                          select cart
                            ).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
            return retobj;
        }

        public string AddServices(Vendorservices service)
        {
            var str = string.Empty;
            try
            {
                var userobj = (from u in context.Vendors
                               where u.Vendorid == service.Vendorid
                               select u).FirstOrDefault();
                
                var wish = (from w in context.Vendorservices
                            where w.Vendorid == service.Vendorid && w.Name == service.Name
                            select w).FirstOrDefault();
                if (userobj == null)
                {
                    return "enter valid data vendor doesnt exist";
                }
                else if (wish != null)
                {
                    return "Vendor has a same service alredy registered";
                }
                else
                {
                    context.Vendorservices.Add(service);
                    context.SaveChanges();
                    str = "sucessfully added service";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw (e);
            }
            return str;
        }

        public string DelServices(Vendorservices services)
        {
            var str = string.Empty;
            try
            {
                var dbcart = (from c in context.Vendorservices
                              where c.Vendorid == services.Vendorid && c.Name == services.Name
                              select c).FirstOrDefault();
                if (dbcart != null)
                {
                    context.Vendorservices.Remove(dbcart);
                    context.SaveChanges();
                    str = "Sucessfully deleted";
                }
                else
                {
                    str = "service doesnt exist for that vendor";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            };
            return str;
        }
        public string UpdateServices(Vendorservices services)
        {
            string status = "could not update try again";
            try
            {
                Vendorservices carti = (from c in context.Vendorservices
                              where c.Vendorid == services.Vendorid && c.Name == services.Name
                              select c).FirstOrDefault();
                carti.Price = services.Price;
                using (var newContext = new StuffyCareContext())
                {
                    newContext.Vendorservices.Update(carti);
                    newContext.SaveChanges();
                    status = "updated sucessfuly";
                }
            }
            catch (Exception e)
            {
                status = "exception occured in Dal";
                throw e;
            }
            return status;
        }

        public List<Vendorservices> GetVendorByServices(string name)
        {
            List<Vendorservices> listobj = new List<Vendorservices>();
            try
            {
                listobj = context.Vendorservices.Where(c => EF.Functions.Like(c.Name,"%"+name+"%")).ToList();
            }
            catch (Exception e)
            {

                throw e;
            }
            return listobj;
        }

        public List<Appointments> GetVendorAppointments(string vendorid)
        {

            List<Appointments> listobj = new List<Appointments>();
            try
            {
                listobj = (from c in context.Appointments
                           where c.Vendorid == vendorid
                           select c).ToList();
            }
            catch (Exception e)
            {

                throw e;
            }
            return listobj;
        }
    }
}
