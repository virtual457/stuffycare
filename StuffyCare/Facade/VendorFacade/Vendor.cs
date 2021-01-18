using StuffyCare.DataLayer;
using StuffyCare.EFModels;
using System;
using System.Collections.Generic;

namespace StuffyCare.Facade
{
    public class Vendor
    {
        private readonly DataLayer.VendorDAO.IVendorDAO VendorDao = DataAccess.VendorDAO;
        public string AuthVendor(string email, string pass)
        {
            try
            {
                return VendorDao.Auth(email, pass);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public Vendors GetVendor(string vendorid)
        {
            try
            {
                return VendorDao.GetVendor(vendorid);
            }
            catch (Exception e)
            {

                throw e;
            }        
        }
        public string Create(Vendors vendor)
        {
            try
            {
                return VendorDao.AddVendor(vendor);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public string AddVendorItem(Items item)
        {
            try
            {
                return VendorDao.VendorAddItem(item);

            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public string GetVendorId(string emailorphone)
        {
            try
            {
                return VendorDao.GetVendorId(emailorphone);

            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public List<Vendorservices> GetServices(string vendorid)
        {
            try
            {
                return VendorDao.GetServices(vendorid);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public string AddServices(Vendorservices services)
        {
            try
            {
                return VendorDao.AddServices(services);
            }
            catch (Exception e)
            {

                throw e;
            }
        
        }
        public string DelServices(Vendorservices services)
        {
            try
            {
                return VendorDao.DelServices(services);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public string UpdateServices(Vendorservices services)
        {
            try
            {
                return VendorDao.UpdateServices(services);
            }
            catch (Exception e)
            {

                throw e;
            }
        
        }
        public List<Vendorservices> GetVendorsByServiceName(string name)
        {
            try
            {
                return VendorDao.GetVendorByServices(name);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public List<Appointments> GetAppointmentsByVendorid(string vendorid)
        {
            try
            {
                return VendorDao.GetVendorAppointments(vendorid);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}

