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
                        cmd.Parameters.AddWithValue("@dob", pet.Dob);
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
                throw;
                
            }
            return str;
        }

        public string DelPet(Pets pet)
        {
            var str = string.Empty;
            try
            {
                var dbpet = context.Pets.Find(pet.Petid);
                if (dbpet != null)
                {
                    context.Pets.Remove(dbpet);
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
                throw;
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
                           where pet.Userid == userid
                           select pet
                         ).ToList();
            }
            catch (Exception)
            {
                listobj = null;
                throw;
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
            }
            return retobj;
        }

        public string Addcart(Cart cart)
        {
            var str = string.Empty;
            try
            {
                context.Cart.Add(cart);
                context.SaveChangesAsync();
                str = "sucessfully added";
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
                throw;
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
            }
            return retobj;
        }

        public string AddWishlist(Wishlist wishlist)
        {
            var str = string.Empty;
            try
            {
                context.Wishlist.Add(wishlist);
                context.SaveChangesAsync();
                str = "sucessfully added";
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
                throw;
            };
            return str;
        }
    }
}
