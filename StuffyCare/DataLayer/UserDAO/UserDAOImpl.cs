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
        public string AddUser(Users user)
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
                    cmd.Parameters.AddWithValue("@first", user.Firstname);
                    cmd.Parameters.AddWithValue("@last", user.Lastname);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@pass", user.Pass);
                    cmd.Parameters.AddWithValue("@pno", user.Pno);
                    cmd.Parameters.AddWithValue("@image", user.Image);
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
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
            return _result;

            //  Users obj = new Users();
            //  obj.Firstname = user.Firstname;
            //  obj.Lastname = user.Lastname;
            //  obj.Email = user.Email;
            //  obj.Pno = user.Pno;
            //  obj.Pass = user.Pass;
            //  obj.Image = user.Image;
            //  obj.LoyaltyPoints = user.LoyaltyPoints;
            //      obj.Isdeleted = user.Isdeleted;
            //  context.Users.Add(obj);
            //int i=  context.SaveChanges();
            //  return "user adding sucessfull";

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
                throw e;
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
                throw e;
            }
            return retobj;
        }
        public string AddOrder(Orders order)
        {
            var str = "could not add pet";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    //sqlConnection.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
                    sqlConnection.ConnectionString = ConStr;
                    sqlConnection.Open();
                    using (var cmd = new SqlCommand("add_order", sqlConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userid", order.Userid);
                        cmd.Parameters.AddWithValue("@itemid", order.Itemid);
                        cmd.Parameters.AddWithValue("@quantity",order.Quantity);
                        cmd.Parameters.AddWithValue("@method", order.Method);
                        cmd.Parameters.AddWithValue("@sr_orderid",order.SrOrderid);
                        cmd.Parameters.AddWithValue("@sr_itemid",order.SrShipmentid);
                        var _output = cmd.Parameters.Add("@ret", SqlDbType.VarChar, 100);
                        _output.Direction = ParameterDirection.Output;
                        cmd.ExecuteScalar();
                        str = _output.Value.ToString();
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

        public Users GetUser(string userid)
        {
            Users obj = new Users();
            try
            {
                obj = (from user in context.Users
                       where user.Userid==userid && user.Isdeleted == false
                       select user).FirstOrDefault();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                obj = null;
                throw e;
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
                throw e;
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

        public string AddToCart(Cart cart)
        {
            throw new NotImplementedException();
        }

        public string AddPet(Pets pet)
        {
            var str = "could not add pet";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    //sqlConnection.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
                    sqlConnection.ConnectionString = ConStr;
                    sqlConnection.Open();
                    using (var cmd = new SqlCommand("add_pet", sqlConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userid", pet.Userid);
                        cmd.Parameters.AddWithValue("@name", pet.Name);
                        cmd.Parameters.AddWithValue("@type", pet.Type);
                        cmd.Parameters.AddWithValue("@size", pet.Size);
                        cmd.Parameters.AddWithValue("@gender", pet.Gender);
                        cmd.Parameters.AddWithValue("@breed", pet.Breed);
                        cmd.Parameters.AddWithValue("@allergies", pet.Allergies);
                        cmd.Parameters.AddWithValue("@age", pet.Age);
                        cmd.Parameters.AddWithValue("@moreinfo", pet.Moreinfo);

                        var _output = cmd.Parameters.Add("@ret", SqlDbType.VarChar, 100);
                        _output.Direction = ParameterDirection.Output;
                        cmd.ExecuteScalar();
                        str = _output.Value.ToString();
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

        public string DelPet(Pets pet)
        {
            var str = string.Empty;
            try
            {
                var dbpet = context.Pets.Find(pet.Petid);
                dbpet.Isdeleted = true;
                if (dbpet != null)
                {
                    context.Pets.Update(dbpet).Property(x=> x.Id).IsModified = false;
                    
                    context.SaveChanges();
                    str = "Sucessfully deleted";
                }
                else
                {
                    str = "Pet doesnt exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            };
            return str;
        }
        public string UpdatePet(Pets pet)
        {
            var str = string.Empty;
            try
            {
                var dbpet = context.Pets.Find(pet.Petid);
                if (dbpet != null)
                {
                    
                    dbpet.Userid = pet.Userid;
                    dbpet.Size = pet.Size;
                    dbpet.Moreinfo = pet.Moreinfo;
                    dbpet.Name = pet.Name;
                    dbpet.Type = pet.Type;
                    dbpet.Gender = pet.Gender;
                    dbpet.Breed = pet.Breed;
                    dbpet.Age = pet.Age;
                    dbpet.Allergies = pet.Allergies;

                    context.Pets.Update(dbpet).Property(x => x.Id).IsModified = false;

                    context.SaveChanges();
                    str = "Sucessfully Updated pet";
                }
                else
                {
                    str = "Pet doesnt exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            };
            return str;
        }
        public string AddToWishlist(Wishlist wishlist)
        {
            throw new NotImplementedException();
        }

        public List<Pets> GetPet(string userid)
        {
            List<Pets> listobj = new List<Pets>();
            try
            {
                listobj = (from pet in context.Pets
                           where pet.Userid == userid && pet.Isdeleted== false
                           select pet
                         ).ToList();
            }
            catch (Exception e)
            {
                listobj = null;
                throw e;
            }
            return listobj;
        }

        public List<Cart> GetCart(string userid)
        {
            List<Cart> retobj = new List<Cart>();
            try
            {

                retobj = (from cart in context.Cart
                          where cart.Userid == userid
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

        public string Addcart(Cart cart)
        {
            var str = string.Empty;
            try
            {
                var userobj = (from u in context.Users
                               where u.Userid == cart.Userid
                               select u).FirstOrDefault();
                var itemobj = (from i in context.Items
                               where i.Itemid == cart.Itemid
                               select i).FirstOrDefault();
                var wish = (from w in context.Cart
                            where w.Userid == cart.Userid && w.Itemid == cart.Itemid
                            select w).FirstOrDefault();
                if (userobj == null || itemobj == null || wish != null)
                {
                    return "enter valid data";
                }
                else
                {
                    context.Cart.Add(cart);
                    context.SaveChanges();
                    str = "sucessfully added";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw (e);
            }
            return str;
        }

        public string DelCart(Cart cart)
        {
            var str = string.Empty;
            try
            {
                var dbcart = (from c in context.Cart
                             where c.Itemid==cart.Itemid && c.Userid==cart.Userid
                             select c).FirstOrDefault();
                if (dbcart != null)
                {
                    context.Cart.Remove(dbcart);
                    context.SaveChanges();
                    str = "Sucessfully deleted";
                }
                else
                {
                    str = "cart item doesnt exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            };
            return str;
        }

        public List<Wishlist> GetWishlist(string userid)
        {
            List<Wishlist> retobj = new List<Wishlist>();
            try
            {
                retobj = (from wishlist in context.Wishlist
                          where wishlist.Userid == userid
                          select wishlist
                            ).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
            return retobj;
        }

        public string AddWishlist(Wishlist wishlist)
        {
            var str = string.Empty;
            try
            {
                var userobj = (from u in context.Users
                               where u.Userid == wishlist.Userid
                               select u).FirstOrDefault();
                var itemobj = (from i in context.Items
                               where i.Itemid == wishlist.Itemid
                               select i).FirstOrDefault();
                var wish = (from w in context.Wishlist
                            where w.Userid == wishlist.Userid && w.Itemid == wishlist.Itemid
                            select w).FirstOrDefault();
                if (userobj == null || itemobj == null || wish != null)
                {
                    return "enter valid data";
                }
                else
                {
                    var res = context.Wishlist.Add(wishlist);
                    context.SaveChanges();
                    str = "sucessfully added";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw (e);
            }
            return str;
        }

        public string DelWishlist(Wishlist wishlist)
        {
            var str = string.Empty;
            try
            {
                var dbwishlist = (from c in context.Wishlist
                              where c.Itemid == wishlist.Itemid && c.Userid == wishlist.Userid
                              select c).FirstOrDefault();
                if (dbwishlist != null)
                {
                    context.Wishlist.Remove(dbwishlist);
                    context.SaveChanges();
                    str = "Sucessfully deleted";
                }
                else
                {
                    str = "wishlist item doesnt exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            };
            return str;
        }

        public string UpdateCart(Cart cart)
        {
            string status = "could not update try again";
            try
            {
                Cart carti = (from c in context.Cart
                              where c.Userid == cart.Userid && c.Itemid == cart.Itemid
                              select c).FirstOrDefault();
                carti.Quantity = cart.Quantity;
                using (var newContext = new StuffyCareContext())
                {
                    newContext.Cart.Update(carti);
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

        public string ChangePassword(string emailorphone, string password)
        {
            string retstr = "user password could not be changed"; 
            try
            {
                //                Users user = (from c in context.Users
                //                              where c.Email == emailorphone 
                //                              select c).FirstOrDefault();
                //                if (user == null)
                //                {
                //                    user = (from c in context.Users
                //                            where c.Pno == emailorphone
                //                            select c).FirstOrDefault();
                //;               }
                //                if (user == null)
                //                {
                //                    retstr = "No user with that email or phone number";
                //                }
                //                else {
                //                    user.Pass = password;
                //                }
                //                using (var newContext = new StuffyCareContext())
                //                {
                //                    newContext.Users.Update(user);
                //                    newContext.SaveChanges();
                //                    retstr = "updated sucessfuly";
                //                }
                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    //sqlConnection.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
                    sqlConnection.ConnectionString = ConStr;
                    sqlConnection.Open();
                    using (var cmd = new SqlCommand("updatepassword", sqlConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@emailorpno", emailorphone);
                        cmd.Parameters.AddWithValue("@pass", password);
                        
                        var _output = cmd.Parameters.Add("@ret", SqlDbType.VarChar, 100);
                        _output.Direction = ParameterDirection.Output;
                        cmd.ExecuteScalar();
                        retstr = _output.Value.ToString();
                    }
                }

            }
            catch (Exception e)
            {
                retstr = e.Message;
                throw e;
            }
            return retstr;
        }

        public string GetUserId(string emailorphone)
        {
            
                string id;
                try
                {
                    var obj = (from c in context.Users
                               where c.Email == emailorphone || c.Pno == emailorphone
                               select c).FirstOrDefault();
                    id = obj.Userid;
                }
                catch (Exception e)
                {
                    throw e;
                }
                return id;
           
        }

        public string UpdateUser(Users user)
        {
            var str = string.Empty;
            try
            {
                var dbpet = context.Users.Find(user.Userid);
                if (dbpet != null)
                {

                    dbpet.Userid = user.Userid;
                    dbpet.Pass = user.Pass;
                    dbpet.Firstname = user.Firstname;
                    dbpet.Lastname = user.Lastname;
                    dbpet.LoyaltyPoints = user.LoyaltyPoints;
                    dbpet.Image = user.Image;
                    context.Users.Update(dbpet).Property(x => x.Id).IsModified = false;

                    context.SaveChanges();
                    str = "Sucessfully Updated User";
                }
                else
                {
                    str = "User doesnt exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            };
            return str;
        }
        public string AddAddress(Address address)
        {
            var str = "could not add address";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    //sqlConnection.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
                    sqlConnection.ConnectionString = ConStr;
                    sqlConnection.Open();
                    using (var cmd = new SqlCommand("add_address", sqlConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userid", address.Userid);
                        cmd.Parameters.AddWithValue("@firstname", address.Firstname);
                        cmd.Parameters.AddWithValue("@lastname", address.Lastname);
                        cmd.Parameters.AddWithValue("@address", address.Addresslineone);
                        cmd.Parameters.AddWithValue("@address2", address.Addresslinetwo);
                        cmd.Parameters.AddWithValue("@landmark", address.Landmark);
                        cmd.Parameters.AddWithValue("@city", address.City);
                        cmd.Parameters.AddWithValue("@pincode", address.Pincode);
                        cmd.Parameters.AddWithValue("@state", address.State);
                        cmd.Parameters.AddWithValue("@country", address.Country);
                        cmd.Parameters.AddWithValue("@email", address.Email);
                        cmd.Parameters.AddWithValue("@phone", address.Phone);
                        cmd.Parameters.AddWithValue("@isshippingaddress", address.Isshippingaddress);

                        var _output = cmd.Parameters.Add("@ret", SqlDbType.VarChar, 100);
                        _output.Direction = ParameterDirection.Output;
                        cmd.ExecuteScalar();
                        str = _output.Value.ToString();
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

        public string DelAddress(Address address)
        {
            var str = string.Empty;
            try
            {
                var dbaddress = context.Address.Find(address.Addressid);
                dbaddress.Isdeleted = true;
                if (dbaddress != null)
                {
                    context.Address.Update(dbaddress).Property(x => x.Id).IsModified = false;

                    context.SaveChanges();
                    str = "Sucessfully deleted";
                }
                else
                {
                    str = "address doesnt exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            };
            return str;
        }
        public string UpdateAddress(Address address)
        {
            var str = string.Empty;
            try
            {
                var dbaddress = context.Address.Find(address.Addressid);
                if (dbaddress != null)
                {


                    dbaddress.Firstname = address.Firstname;
                    dbaddress.Lastname = address.Lastname;
                    dbaddress.Addresslineone = address.Addresslineone;
                    dbaddress.Addresslinetwo = address.Addresslinetwo;
                    dbaddress.Landmark = address.Landmark;
                    dbaddress.City = address.City;
                    dbaddress.Pincode = address.Pincode;
                    dbaddress.State = address.State;
                    dbaddress.Country = address.Country;
                    dbaddress.Email = address.Email;
                    dbaddress.Phone = address.Phone;
                    dbaddress.Isshippingaddress = address.Isshippingaddress;


                    context.Address.Update(dbaddress).Property(x => x.Id).IsModified = false;

                    context.SaveChanges();
                    str = "Sucessfully Updated address";
                }
                else
                {
                    str = "address doesnt exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            }
            return str;
        }
        

        public List<Address> GetAddress(string userid)
        {
            List<Address> listobj = new List<Address>();
            try
            {
                listobj = (from address in context.Address
                           where address.Userid == userid && address.Isdeleted == false
                           select address
                         ).ToList();
            }
            catch (Exception e)
            {
                listobj = null;
                throw e;
            }
            return listobj;
        }
    }
}
