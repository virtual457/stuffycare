using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoofyTailsDALLayer.EFModels;

namespace WoofyTailsBusinessLayer.Repository
{
    public class VendorRepository: BaseRepository
    {
        //vendordetails part 
        #region Vendor details(CRUD operations)

        /// <summary>
        /// used to add the deatils of an vendor 
        /// </summary>
        /// <param name="vendor">vendor object having the details to be added for a vendor</param>
        /// <returns>a string signifying the status of the operation</returns>
        public string AddVendorDetails(Vendor vendor)
        {
            var str = "";
            try
            {
                User vendorobj = (from c in context.Users
                               where c.UserId == vendor.Vendorid
                               select c).AsNoTracking().FirstOrDefault();


                if (vendorobj == null)
                {
                    return "vendor id doesnt exist(not registered/vendor id is wrong)";
                }
                else if (vendorobj.RoleId != 3)
                {
                    return "The Id Entered is of non-vendor cannot add vendor details to a non-vendor";
                }

                
                vendor.Isdeleted = 0;
                vendor.Isauthorized = 0;
                vendor.Authorizedstatus = "pending";
                context.Vendors.Add(vendor);
                if (this.save() > 0)
                {
                    return "vendor added sucessfully";
                }
            }

            catch (Exception e)
            {
                str = e.Message;
                throw e;

            }
            return str;
        }
        /// <summary>
        /// to Delete the details of the vendor 
        /// </summary>
        /// <param name="vendor"> vendor object whose details have to be deleted </param>
        /// <returns> a string signifying the status</returns>
        public string DelVendorDetails(Vendor vendor)
        {
            var str = string.Empty;
            try
            {
                var dbvendor = context.Vendors.Find(vendor.Vendorid);

                if (dbvendor != null)
                {
                    var dbuser = context.Users.Find(vendor.Vendorid);
                    dbuser.IsDeleted = 1;
                    dbvendor.Isdeleted = 1;
                    // context.Vendors.Update(dbvendor);

                    if (context.SaveChanges() > 0)
                    {
                        str = "Sucessfully deleted";
                    }
                }
                else
                {
                    str = "Vendor doesnt exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            }
            return str;
        }
        /// <summary>
        /// Update the available vendor details of an vendor
        /// 
        /// </summary>
        /// <param name="vendor">vendor object containg the details of the vendor which is to be updated</param>
        /// <returns> a string signifying the status</returns>
        public string UpdateVendorDetails(Vendor vendor)
        {
            var str = string.Empty;
            try
            {
                var dbvendor = context.Vendors.Find(vendor.Vendorid);
                if (dbvendor != null)
                {
                    
                    dbvendor.Description = vendor.Description;
                    dbvendor.StoreName = vendor.StoreName;
                    dbvendor.City = vendor.City;
                    dbvendor.Location = vendor.Location;
                    dbvendor.Yearsofexperience = vendor.Yearsofexperience;
                    dbvendor.Monfrom = vendor.Monfrom;
                    dbvendor.Monto = vendor.Monto;
                    dbvendor.Tuefrom = vendor.Tuefrom;
                    dbvendor.Tueto = vendor.Tueto;
                    dbvendor.Wedfrom = vendor.Wedfrom;
                    dbvendor.Wedto = vendor.Wedto;
                    dbvendor.Thurfrom = vendor.Thurfrom;
                    dbvendor.Thurto = vendor.Thurto;
                    dbvendor.Frifrom = vendor.Frifrom;
                    dbvendor.Frito = vendor.Frito;
                    dbvendor.Satfrom = vendor.Satfrom;
                    dbvendor.Satto = vendor.Satto;
                    dbvendor.Sunfrom = vendor.Sunfrom;
                    dbvendor.Sunto = vendor.Sunto;
                    dbvendor.Photo = vendor.Photo;
                    dbvendor.Photoidproof = vendor.Photoidproof;
                   
                    dbvendor.Issellingitem = vendor.Issellingitem;
                    dbvendor.Homeservice = vendor.Homeservice;
                    
                    
                    context.Vendors.Update(dbvendor);
                    if (context.SaveChanges() > 0)
                    {
                        str = "Sucessfully Updated vendor";
                    }
                }
                else
                {
                    str = "Vendor details doesnt exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            };
            return str;
        }
        /// <summary>
        /// used to get the deatils of an vendor id
        /// </summary>
        /// <param name="vendorId">vendor id of an vendor</param>
        /// <returns>a list of vendors</returns>
        public List<Vendor> GetVendorDetails(string vendorId)
        {
            List<Vendor> listobj = new List<Vendor>();
            try
            {
                listobj = (from vendor in context.Vendors
                           where vendor.Vendorid == vendorId && vendor.Isdeleted == 0
                           select vendor
                         ).ToList();
            }
            catch (Exception e)
            {
                listobj = null;
                throw e;
            }
            return listobj;
        }
        #endregion

        //vendor service part 
        #region Vendor Service(CRUD operations)
        /// <summary>
        /// used to add a service provided by an vendor
        /// </summary>
        /// <param name="vendorservice">object containing the details to be added</param>
        /// <returns>a string signifying the status</returns>
        public string AddVendorService(Vendorservice vendorservice)
        {
            var str = "";
            try
            {
                Vendorservice vendorserviceobj = (from c in context.Vendorservices
                                  where c.Vendorid == vendorservice.Vendorid && c.Nameofservice== vendorservice.Nameofservice
                                  select c).AsNoTracking().FirstOrDefault();


                if (vendorserviceobj != null)
                {
                    return "vendor has the service alredy registered(if u want to change price try update vendorservice)";
                }
                


                
                context.Vendorservices.Add(vendorservice);
                if (this.save() > 0)
                {
                    return "vendor's service added sucessfully";
                }
            }

            catch (Exception e)
            {
                str = e.Message;
                throw e;

            }
            return str;
        }
        /// <summary>
        /// used to delete a service provided by an vendor
        /// </summary>
        /// <param name="vendorservice">object containing the details to be added</param>
        /// <returns>a string signifying the status</returns>
        public string DelVendorService(Vendorservice vendorservice)
        {
            var str = string.Empty;
            try
            {
                var dbvendorservice = (from venservice in context.Vendorservices
                                where venservice.Vendorid==vendorservice.Vendorid && venservice.Nameofservice == vendorservice.Nameofservice
                                select venservice).FirstOrDefault();

                if (dbvendorservice != null)
                {
                    context.Vendorservices.Remove(dbvendorservice);
                    // context.Vendors.Update(dbvendor);

                    if (context.SaveChanges() > 0)
                    {
                        str = "Sucessfully deleted";
                    }
                }
                else
                {
                    str = "Vendor with that service doesnt exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            }
            return str;
        }
        /// <summary>
        /// used to update a service provided by an vendor
        /// </summary>
        /// <param name="vendorservice">object containing the details to be added</param>
        /// <returns>a string signifying the status</returns>
        public string UpdateVendorserviceDetails(Vendorservice vendorservice)
        {
            var str = string.Empty;
            try
            {
                var dbvendorservice = (from venservice in context.Vendorservices
                                       where venservice.Vendorid == vendorservice.Vendorid && venservice.Nameofservice == vendorservice.Nameofservice
                                       select venservice).FirstOrDefault();

                if (dbvendorservice != null)
                {

                    dbvendorservice.Price = vendorservice.Price;

                    context.Vendorservices.Update(dbvendorservice);
                    if (context.SaveChanges() > 0)
                    {
                        str = "Sucessfully Updated vendor service";
                    }
                }
                else
                {
                    str = "Vendor with that service doesnt exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            };
            return str;
        }
        /// <summary>
        /// used to get all services provided by an vendor
        /// </summary>
        /// <param name="vendorId">Vendorid of the vendor whose services is to be fetched</param>
        /// <returns>a lsit of services provided by that vendor</returns>
        public List<Vendorservice> GetVendorservices(string vendorId)
        {
            List<Vendorservice> listobj = new List<Vendorservice>();
            try
            {
                listobj = (from vendorservice in context.Vendorservices
                           where vendorservice.Vendorid == vendorId 
                           select vendorservice
                         ).ToList();
            }
            catch (Exception e)
            {
                listobj = null;
                throw e;
            }
            return listobj;
        }
        #endregion
        
        //items part of vendor
        #region item region(add/update/delete/get)
        /// <summary>
        /// item to add a new item details
        /// </summary>
        /// <param name="item">item object containing details of the item</param>
        /// <returns>string signifying the outcome/result</returns>
        public string AddItem(Item item)
        {
            var str = "";
            try
            {
                var vendorobk = (from c in context.Users
                                 join d in context.Vendors on c.UserId equals d.Vendorid
                                 where c.UserId == item.Addedby && c.IsDeleted==0 && d.Isauthorized == 1
                                 select c).AsNoTracking().FirstOrDefault();
                if (vendorobk == null)
                {
                    return "Only registered and authorized vendors can add items";
                }
                item.Itemid = Guid.NewGuid().ToString();
                item.Deletedstatus = 0;
                item.Authorizedstatus = "pending";
                item.Authorizedby = item.Addedby;
                context.Items.Add(item);
                if (this.save() > 0)
                {
                    return "item added sucessfully as " + item.Itemid;
                }
            }

            catch (Exception e)
            {
                str = e.Message;
                throw e;

            }
            return str;
        }
        /// <summary>
        /// To delete item details from the database(modify the isdeleted column)
        /// </summary>
        /// <param name="item">item object containing itemid of the item to be deleted</param>
        /// <returns> a string signifying the status</returns>
        public string DelItem(Item item)
        {
            var str = string.Empty;
            try
            {
                var dbitem = context.Items.Find(item.Itemid);

                if (dbitem != null)
                {
                    dbitem.Deletedstatus = 2;
                    // context.Items.Update(dbitem);

                    if (context.SaveChanges() > 0)
                    {
                        str = "Sucessfully deleted";
                    }
                }
                else
                {
                    str = "Item doesnt exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            }
            return str;
        }
        /// <summary>
        /// To Update the details of the item
        /// </summary>
        /// <param name="item">item obecjt containing the new data to be modified</param>
        /// <returns>string signifying the status</returns>
        public string UpdateItem(Item item)
        {
            var str = string.Empty;
            try
            {
                var dbitem = context.Items.Find(item.Itemid);
                if (dbitem != null)
                {

                    dbitem.Name = item.Name;
                    dbitem.Description = item.Description;
                    dbitem.Foranimal = item.Foranimal;
                    dbitem.Category = item.Category;
                    dbitem.Subcategory = item.Subcategory;
                    dbitem.Price = item.Price;
                    dbitem.Saleprice = item.Saleprice;
                    dbitem.Sku = item.Sku;
                    dbitem.Quantity = item.Quantity;
                    dbitem.Moa = item.Moa;
                    dbitem.Photo = item.Photo;
                    dbitem.Length = item.Length;
                    dbitem.Breadth = item.Breadth;
                    dbitem.Height = item.Height;
                    dbitem.Weight = item.Weight;
                    dbitem.Shippingclass = item.Shippingclass;
                    dbitem.Processingtime = item.Processingtime;
                    dbitem.Mililitres = item.Mililitres;
                    dbitem.Packsizeingrams = item.Packsizeingrams;
                    dbitem.Unitcount = item.Unitcount;
                    dbitem.Upsells = item.Upsells;
                    dbitem.Crosssells = item.Crosssells;
                    dbitem.Policylabel = item.Policylabel;
                    dbitem.Shippingpolicy = item.Shippingpolicy;
                    dbitem.Refundpolicy = item.Refundpolicy;
                    dbitem.Cancelationpolicy = item.Cancelationpolicy;
                    dbitem.Exchangepolicy = item.Exchangepolicy;
                    dbitem.Storename = item.Storename;
                    dbitem.Commissionfor = item.Commissionfor;
                    dbitem.Commissionmode = item.Commissionmode;
                    
                    context.Items.Update(dbitem);
                    if (context.SaveChanges() > 0)
                    {
                        str = "Sucessfully Updated item";
                    }
                }
                else
                {
                    str = "Item doesnt exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            };
            return str;
        }


        /// <summary>
        /// used to get items wrt to the params passed
        /// </summary>
        /// <param name="itemId">item id of the item for whom the details has to be fetched</param>
        /// <param name="foranimal">for which animal the item for whom the details has to be fetched</param>
        /// <param name="category">category of the item for whom the details has to be fetched</param>
        /// <param name="subcayegory">subcategory of the item for whom the details has to be fetched</param>
        /// <param name="name">name of the item for whom the details has to be fetched</param>
        /// <returns>a list of item object containing the details of the item of the given item id</returns>
        public List<Item> GetItem(string itemId,string foranimal,string category,string subcayegory,string name)
        {
            List<Item> listobj = new List<Item>();
            try
            {
                if (itemId != "all")
                {
                    listobj = (from item in context.Items
                               where item.Itemid == itemId && item.Deletedstatus == 0 && item.Authorizedstatus == "authorized"
                               select item
                             ).ToList();
                }
                else
                {
                    listobj = (from item in context.Items
                               where item.Deletedstatus == 0 && item.Authorizedstatus == "authorized"
                               select item
                             ).ToList();
                    if (foranimal != "all")
                    {
                        listobj.RemoveAll(x => x.Foranimal != foranimal);
                    }
                    if (category != "all")
                    {
                        listobj.RemoveAll(x => x.Category != category);
                    }
                    if (subcayegory != "all")
                    {
                        listobj.RemoveAll(x => x.Subcategory != subcayegory);
                    }
                    if (name != "all")
                    {
                        listobj.RemoveAll(x => !x.Name.Contains(name));
                    }


                }

            }
            catch (Exception e)
            {
                listobj = null;
                throw e;
            }
            return listobj;
        }
        #endregion

    }
}
