using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WoofyTailsBusinessLayer.APIModels;
using WoofyTailsBusinessLayer.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WoofyTailsServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        // GET: api/<VendorController>
        private readonly VendorRepository _vendor;
        private readonly IMapper _mapper;
        public VendorController(IMapper mapper)
        {
            _vendor = new VendorRepository();
            _mapper = mapper;
        }
        // GET: api/<VendorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VendorController>/5
        [HttpGet("GetVendor")]
        public JsonResult GetVendor(string vendorid)
        {
            List<Vendor> listObj = new List<Vendor>();

            var replistObj = _vendor.GetVendorDetails(vendorid);

            if (replistObj != null)
            {
                foreach (var obj in replistObj)
                {
                    Vendor vendorObj = _mapper.Map<Vendor>(obj);
                    listObj.Add(vendorObj);
                }
            }

            return new JsonResult(listObj);
        }

        // POST api/<VendorController>
        [HttpPost("AddVendor")]
        public JsonResult AddVendor([FromBody] Vendor vendor)
        {

            var str = _vendor.AddVendorDetails(_mapper.Map<WoofyTailsDALLayer.EFModels.Vendor>(vendor));
            bool status = false;
            if (str == "vendor added sucessfully")
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? "vendor added sucessfully" : str });
        }

        // PUT api/<VendorController>/5
        [HttpPut("UpdateVendor")]
        public JsonResult UpdateVendor([FromBody] Vendor vendor)
        {

            var str = _vendor.UpdateVendorDetails(_mapper.Map<WoofyTailsDALLayer.EFModels.Vendor>(vendor));
            bool status = false;
            if (str == "Sucessfully Updated vendor")
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? "vendor updated sucessfully" : str });

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("DeleteVendor")]
        public JsonResult DeleteVendor(Vendor vendor)
        {
            var str = _vendor.DelVendorDetails(_mapper.Map<WoofyTailsDALLayer.EFModels.Vendor>(vendor));

            bool status = false;
            if (str == "Sucessfully deleted")
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? "vendor deleted sucessfully" : str });
        }
        [HttpGet("GetVendorServices")]
        public JsonResult GetVendorServices(string vendorid)
        {
            List<Vendorservice> listObj = new List<Vendorservice>();

            var replistObj = _vendor.GetVendorservices(vendorid);

            if (replistObj != null)
            {
                foreach (var obj in replistObj)
                {
                    Vendorservice vendorObj = _mapper.Map<Vendorservice>(obj);
                    listObj.Add(vendorObj);
                }
            }

            return new JsonResult(listObj);
        }

        // POST api/<VendorController>
        [HttpPost("AddVendorservice")]
        public JsonResult AddVendorservice([FromBody] Vendorservice vendorservice)
        {

            var str = _vendor.AddVendorService(_mapper.Map<WoofyTailsDALLayer.EFModels.Vendorservice>(vendorservice));
            bool status = false;
            if (str == "Sucessfully Updated vendor service")
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? "vendor service added sucessfully" : str });
        }

        
        [HttpPut("UpdateVendorservice")]
        public JsonResult UpdateVendorservice([FromBody] Vendorservice vendorservice)
        {

            var str = _vendor.UpdateVendorserviceDetails(_mapper.Map<WoofyTailsDALLayer.EFModels.Vendorservice>(vendorservice));
            bool status = false;
            if (str == "Sucessfully Updated vendor service")
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? "vendor service updated sucessfully" : str });

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("DeleteVendorservice")]
        public JsonResult DeleteVendorservice(Vendorservice vendorservice)
        {
            var str = _vendor.DelVendorService(_mapper.Map<WoofyTailsDALLayer.EFModels.Vendorservice>(vendorservice));

            bool status = false;
            if (str == "Sucessfully deleted vendor service")
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? "vendor service deleted sucessfully" : str });
        }

        #region Item(Get/post/update/delete)

        #region GetItem
        /// <summary>
        /// used to get items wrt to the params passed
        /// </summary>
        /// <param name="itemid">item id of the item for whom the details has to be fetched</param>
        /// <param name="foranimal">for which animal the item for whom the details has to be fetched</param>
        /// <param name="category">category of the item for whom the details has to be fetched</param>
        /// <param name="subcategory">subcategory of the item for whom the details has to be fetched</param>
        /// <param name="name">name of the item for whom the details has to be fetched</param>
        /// <returns>a list of item object containing the details of the item of the given item id</returns>
        [HttpGet("GetItem")]
        public JsonResult GetItem(string itemid,string foranimal,string category,string subcategory,string name)
        {
            List<Item> listObj = new List<Item>();

            var replistObj = _vendor.GetItem(itemid,foranimal,category,subcategory,name);

            if (replistObj != null)
            {
                foreach (var obj in replistObj)
                {
                    Item itemObj = _mapper.Map<Item>(obj);
                    listObj.Add(itemObj);
                }
            }

            return new JsonResult(listObj);
        }
        #endregion

        #region AddItem
        /// <summary>
        /// item to add a new item details
        /// </summary>
        /// <remarks>
        /// Instructions:
        /// 
        ///Items can only be added by authorized vendors
        ///Sample request:
        ///
        ///     {
        ///         
        ///     }
        /// </remarks>
        /// <param name="item">item object containing details of the item</param>
        /// <returns>string signifying the outcome/result</returns>
        [HttpPost("AddItem")]
        public JsonResult AddUSer([FromBody] Item item)
        {

            var str = _vendor.AddItem(_mapper.Map<WoofyTailsDALLayer.EFModels.Item>(item));
            bool status = false;
            if (str.Contains("item added sucessfully"))
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? str.Substring(0, str.Length - 36) : str, itemid = status ? str.Substring(str.Length - 36) : "0" });

            //else
            //{
            //    return new JsonResult(new { Message = "Enter Valid Data", ReceivedData = ModelState });
            //}
        }
        #endregion

        #region UpdateItem
        /// <summary>
        /// To Update the details of the item
        /// </summary>
        /// <param name="item">item obect containing the new data to be modified</param>
        /// <returns>Json object  signifying the status</returns>
        [HttpPut("UpdateItem")]
        public JsonResult UpdateItem([FromBody] Item item)
        {

            var str = _vendor.UpdateItem(_mapper.Map<WoofyTailsDALLayer.EFModels.Item>(item));
            bool status = false;
            if (str == "Sucessfully Updated item")
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? "item updated sucessfully" : str });

        }
        #endregion

        #region Deleteitem
        /// <summary>
        /// To delete item details from the database(modify the isdeleted column)
        /// </summary>
        /// <param name="item">item object containing itemid of the item to be deleted</param>
        /// <returns> Json object  signifying the status</returns>
        [HttpDelete("DeleteItem")]
        public JsonResult DeleteItem(Item item)
        {
            var str = _vendor.DelItem(_mapper.Map<WoofyTailsDALLayer.EFModels.Item>(item));

            bool status = false;
            if (str == "Sucessfully deleted")
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? "item deleted sucessfully" : str });
        }
        #endregion

        #endregion
    }
}
