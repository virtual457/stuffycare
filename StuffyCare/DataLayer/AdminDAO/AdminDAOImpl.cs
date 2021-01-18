using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StuffyCare.EFModels;
using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StuffyCare.DataLayer.AdminDAO
{
    public class AdminDAOImpl:Connection,IAdminDAO
    {
        //connection string initialization
        
        private readonly Connection con =new Connection();
        StuffyCareContext context = new StuffyCareContext();
        string ConStr = GetConnectionString();
        //Function for Adding admin
        public string AddAdmin(string email, string pass, string pno)
        {
            var timer = new Stopwatch();
            var _result = String.Empty;
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
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
            }
            catch (Exception e)
            {
                throw e;
                //_result = e.Message;
                //Console.WriteLine(e.Message);
            }
            return _result;
        }
        //Fuction for Authentication of Admin
        public string Auth(string email, string pass)
        {
            SqlConnection sqlConnection = new SqlConnection();
            var _result = String.Empty;
            try
            {
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
                    cmd.ExecuteScalar();
                    _result = _output.Value.ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _result = e.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return _result;
        }
        //Function for getting Appointments by category when category is all, all appointements are retrived else only the category appointments are retrieved
        public List<Appointments> GetAllAppointments(string category)
        {
            List<Appointments> obj= new List<Appointments>();
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(ConStr))
            //    {
            //        using (SqlDataAdapter da = new SqlDataAdapter())
            //        {
            //            da.SelectCommand = new SqlCommand("get_allappointments", conn);
            //            da.SelectCommand.Parameters.AddWithValue("@category", category);
            //            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            //            DataSet ds = new DataSet();
            //            da.Fill(ds, "user");
            //            DataTable dt = ds.Tables["user"];
            //            foreach (DataRow row in dt.Rows)
            //            {
            //                obj.Add(new Appointments(row["aptid"].ToString(), row["userid"].ToString(), row["pno"].ToString(), row["dt"].ToString(), row["servicetype"].ToString(), row["address"].ToString(), row["message"].ToString()));
            //            }
            //        }
            //    }
            //}
            try 
            {
                obj=context.Appointments.Where(c => c.Category == category).AsNoTracking().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                        cmd.Parameters.AddWithValue("@userid", appointments.Userid);
                        cmd.Parameters.AddWithValue("@petid", appointments.Petid);
                        cmd.Parameters.AddWithValue("@phonenumber", appointments.Phonenumber);
                        cmd.Parameters.AddWithValue("@vendorid", appointments.Vendorid);
                        cmd.Parameters.AddWithValue("@category", appointments.Category);
                        cmd.Parameters.AddWithValue("@servicedatetime", appointments.Servicedatetime);
                        cmd.Parameters.AddWithValue("@servicefees", appointments.Servicefees);
                        cmd.Parameters.AddWithValue("@address", appointments.Address);
                        cmd.Parameters.AddWithValue("@message", appointments.Message);
                        cmd.Parameters.AddWithValue("@ishomeservice", appointments.Ishomeservice);
                        cmd.Parameters.AddWithValue("@ispaid", appointments.Ispaid);
                        
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

        //Function To get all the users in the database
        public List<Users> GetUser(string userid)
        {
            Users obj = new Users();
            List<Users> retobj = new List<Users>();
            try
            {
                if (userid == "all")
                {
                    retobj = (from user in context.Users
                              select user
                            ).ToList();
                }
                else
                {
                    retobj = (from user in context.Users
                              where user.Userid == userid
                              select user
                            ).ToList();
                }


            }
            catch (Exception e)
            {
                retobj = null;
                Console.WriteLine(e.Message);
            }
            return retobj;
        }
        public List<Vendors> GetVendors(string vendorid,string category,string city)
        {
            Users obj = new Users();
            List<Vendors> retobj = new List<Vendors>();
            try
            {
                if (category != "all")
                {
                    retobj = (from user in context.Vendors
                              join service in context.Vendorservices on user.Vendorid equals service.Vendorid
                              where service.Name == category && user.Isauthorized==true
                              select user
                                ).ToList();

                }
                else
                {
                    retobj = (from user in context.Vendors
                                  //join service in context.Vendorservices on user.Vendorid equals service.Vendorid
                              where user.Isauthorized==true
                              select user
                                ).ToList();
                }
                if (vendorid != "all")
                {
                    retobj.RemoveAll(x => x.Vendorid != vendorid);
                }
                if (city != "all")
                {
                    retobj.RemoveAll(x => !x.City.Contains(city));
                }
            }
            catch (Exception e)
            {
                retobj = null;
                Console.WriteLine(e.Message);
            }
            return retobj;
        }
        public List<Items> GetItem(string itemid, string foranimal, string category, string subcategory,string name)
        {
            Items obj = new Items();
            List<Items> retobj = new List<Items>();
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(ConStr))
            //    {
            //        using (SqlDataAdapter da = new SqlDataAdapter())
            //        {
            //            da.SelectCommand = new SqlCommand("get_items", conn);
            //            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //            da.SelectCommand.Parameters.AddWithValue("@itemid",(itemid));
            //            DataSet ds = new DataSet();
            //            da.Fill(ds, "user");
            //            DataTable dt = ds.Tables["user"];
            //            foreach (DataRow row in dt.Rows)
            //            { 
            //                obj.Itemid = (row["itemid"]).ToString();
            //                obj.Name = row["name"].ToString();
            //                obj.Description = row["description"].ToString();
            //                obj.Category = row["category"].ToString();
            //                obj.Price = float.Parse(row["price"].ToString());
            //                obj.Sku = row["sku"].ToString();
            //                obj.Saleprice = float.Parse(row["saleprice"].ToString());
            //                obj.Quantity = Convert.ToInt32(row["quantity"].ToString());
            //                obj.Moa = Convert.ToInt32(row["moa"].ToString());
            //                obj.Photo = row["photo"].ToString();
            //                retobj.Add(obj);
            //            }
            //        }
            //    }
            //}
            try {
                if (itemid == "all")
                {
                    retobj = (from item in context.Items
                              where item.Authorizedstatus== "authorized" && item.Deletedstatus != "approved"
                              select item
                            ).ToList();
                    if (foranimal != "all")
                    {
                        retobj.RemoveAll(x => x.Foranimal != foranimal);
                    }
                    if (name != "all")
                    {
                        retobj.RemoveAll(x => !x.Name.Contains(name));
                    }
                    if (category != "all")
                    {
                        retobj.RemoveAll(x => x.Category != category);
                    }
                    if (subcategory != "all")
                    {
                        retobj.RemoveAll(x => x.Subcategory != subcategory);
                    }
                }
                else
                {
                    retobj = (from item in context.Items
                              where item.Itemid==itemid
                              select item
                            ).ToList();
                }

            }
            catch (Exception e)
            {
                retobj = null;
                Console.WriteLine(e.Message);
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
                        cmd.Parameters.AddWithValue("@breadth",Convert.ToDouble( items.Breadth));
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
                        cmd.Parameters.AddWithValue("@authorizedby", items.Addedby);
                        cmd.Parameters.AddWithValue("@authorizedstatus", "authorized");
                        cmd.Parameters.AddWithValue("@deletedstatus", "notrequested");
                        
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
            //int result;
            //string returnresult="could not add item";
            //try
            //{
            //    SqlParameter prmName = new SqlParameter("@name", items.Name);
            //    SqlParameter prmDescription = new SqlParameter("@description", items.Description);
            //    SqlParameter prmCategory = new SqlParameter("@category", items.Category);
            //    SqlParameter prmPrice = new SqlParameter("@price", items.Price);
            //    SqlParameter prmSku = new SqlParameter("@sku", items.Sku);
            //    SqlParameter prmSaleprice = new SqlParameter("@saleprice", items.Saleprice);
            //    SqlParameter prmQuantity = new SqlParameter("@quantity", items.Quantity);
            //    SqlParameter prmMoa = new SqlParameter("@moa", items.Moa);
            //    SqlParameter prmOwn = new SqlParameter("@own", items.Own);
            //    SqlParameter prmPhoto = new SqlParameter("@photo", items.Photo);
            //    SqlParameter prmLength = new SqlParameter("@length", items.Length);
            //    SqlParameter prmBreadth = new SqlParameter("@breadth", items.Breadth);
            //    SqlParameter prmHeight = new SqlParameter("@height", items.Height);
            //    SqlParameter prmWeight = new SqlParameter("@weight", items.Weight);
            //    SqlParameter prmRet = new SqlParameter("@ret", System.Data.SqlDbType.VarChar,100);
            //    prmRet.Direction = System.Data.ParameterDirection.Output;

            //    result = context.Database.ExecuteSqlCommand("EXEC add_item @name,@description,@category,@price,@sku,@saleprice,@quantity,@moa,@own,@photo,@length,@breadth,@height,@weight,@ret OUT", new[] { prmName, prmDescription,prmCategory,prmPrice,prmSku,prmSaleprice,prmQuantity,prmMoa,prmOwn,prmPhoto,prmLength,prmBreadth,prmHeight,prmWeight,prmRet });

            //    returnresult = Convert.ToString(prmRet.Value);

            //}
            catch (Exception e)
            {
                ret = e.Message;
                Console.WriteLine(e.Message);
            }
            return ret;
        }

        public List<Orders> GetOrders(string vendorid)
        {
            List<Orders> listobj = new List<Orders>();
            try
            {
                if (vendorid == "all")
                {
                    listobj = (from order in context.Orders
                               select order).ToList();
                }
                else
                {
                    listobj = (from order in context.Orders
                               join item in context.Items
                               on order.Itemid equals item.Itemid
                               where item.Addedby == vendorid
                               select order
                             ).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                listobj = null;
            }
            return listobj;
        }

        public List<Orders> GetRecentOrders(int num)
        {
            var Listobj = new List<Orders>();
            try
            {
                Listobj = (from order in context.Orders
                           orderby order.Dt
                           select order).Take(num).ToList();
            }
            catch (Exception e)
            {

                throw;
            }
            return Listobj;
        }

        public string AuthVendor(string vendorid)
        {
            string ret = "Could not Authorise vendor";
            try
            {
                var vendor = context.Vendors.Find(vendorid);
                if (vendor != null)
                {
                    vendor.Isauthorized = true;
                    context.Vendors.Update(vendor).Property(x => x.Id).IsModified = false;

                    context.SaveChanges();
                    ret = "Sucessfully authorized vendor "+vendor.Vendorid;
                }
                else
                {
                    ret = "vendor doesnt exist";
                }

            }
            catch (Exception e)
            {

                throw e; 
            }
            return ret;
        }

        public List<Vendors> GetAllVendorIdRequests()
        {
            var listobj = new List<Vendors>();
            try
            {
                 listobj = (from req in context.Vendors
                            where req.Isauthorized==false
                               select req).ToList();

            }
            catch (Exception e)
            {

                throw e;
            }
            return listobj;
        }

        public List<Items> GetAllVendorItemsByVendorId(string vendorid)
        {
            var listobj = new List<Items>();
            try
            {
                if (vendorid == "all")
                {
                    listobj = (from item in context.Items
                               where item.Authorizedstatus=="pending"
                               select item).ToList();
                }
                else
                {
                    listobj = (from item in context.Items
                               where item.Addedby == vendorid && item.Authorizedstatus == "pending"
                               select item
                             ).ToList();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return listobj;
        }

        public string AuthVendorItem(string itemid, string adminid)
        {
            var ret = "Could not Authorize Vendor Item";
            try
            {
                var admincheck = context.Admins.Find(adminid);
                if (admincheck == null)
                {
                    return "You cant authorize item";
                }
                var item = context.Items.Find(itemid);
                if (item != null)
                {
                    item.Authorizedstatus = "authorized";
                    item.Authorizedby =adminid;
                    context.Items.Update(item).Property(x => x.Id).IsModified = false;
                    context.SaveChanges();
                    ret = "Sucessfully authorized vendor " + item.Addedby + "'s item " + item.Itemid ;
                }
                else
                {
                    ret = "item doesnt exist";
                }


            }
            catch (Exception e)
            {
                ret = e.Message;
                throw e;
            }
            return ret;
        }

        public string VerifyOtp(string phonenum, string otp)
        {
            var findobj = (from c in context.Otp
                           where c.Phoneno == phonenum
                           select c).FirstOrDefault();
            if (findobj.Otpstring == otp )
            {
                DateTime before5mins = DateTime.Now.AddMinutes(-5);
                
                if (findobj.CreatedDate > before5mins)
                {
                    return "Otp Verified sucessfully";
                }
                else 
                {
                    return "Otp is delayed";
                }
            }
            return "Otp is wrong";
        }

        public string AddOtp(string phonenum, string otp)
        {
            var str = "could not add otp";
            try
            {
                var findobj = (from c in context.Otp
                               where c.Phoneno == phonenum
                               select c).FirstOrDefault();
                if (findobj == null)
                {


                    var obj = new Otp();
                    obj.Otpstring = otp;
                    obj.Phoneno = phonenum;
                    obj.CreatedDate = DateTime.Now;
                    context.Otp.Add(obj);
                    context.SaveChanges();
                    str = "sucessfully added otp";
                }
                else
                {
                    findobj.Otpstring = otp;
                    findobj.CreatedDate = DateTime.Now;
                    using (var newContext = new StuffyCareContext())
                    {
                        newContext.Otp.Update(findobj);
                        newContext.SaveChanges();
                        str = "OTP updated sucessfully";
                    }
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            }
            return str;
        }

        public string GetAdminId(string emailorphone)
        {
            string id;
            try
            {
                var obj = (from c in context.Admins
                           where c.Email == emailorphone || c.Pno == emailorphone
                           select c).FirstOrDefault();
                id = obj.Adminid;
            }
            catch(Exception e)
            {
                throw e;
            }
            return id;
        }

        public string LoginbyOTP(string phone, string otp)
        {
            var finduser = (from c in context.Users
                            where c.Pno == phone
                            select c).FirstOrDefault();
            if (finduser == null)
            {
                return "Signup first";
            }
            var findobj = (from c in context.Otp
                           where c.Phoneno == phone
                           select c).FirstOrDefault();
            if (findobj.Otpstring == otp)
            {
                DateTime before5mins = DateTime.Now.AddMinutes(-5);

                if (findobj.CreatedDate > before5mins)
                {
                    var userobj = (from c in context.Users
                                   where c.Pno == phone && c.Isdeleted==false
                                   select c).FirstOrDefault();
                    return "Otp Verified sucessfully for "+ userobj.Userid;
                }
                else
                {
                    return "Otp is delayed";
                }
            }
            return "Otp is wrong";
        }
    }
    
}
