using MyComicStore.DataAccess;
using MyComicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyComicStore.Controllers
{
    public class CartController : Controller
    {
        ComicStoreEntities storeDB = new ComicStoreEntities();

        // GET: Cart
        public ActionResult Cart()
        {
            return PartialView();
        }

        // Checkout
        public ActionResult Checkout()
        {
            var country = CartDataAccess.GetCountries();
            return PartialView(country);
        }

        public JsonResult SearchProvinces(int countryid)
        {
            //var provinces = storeDB.Provinces.Where(p => p.CountryId == countryid).Select(p => p.ProvinceName).ToList();
            var provinces = CartDataAccess.searchprovince(countryid);
            provinces.Select(p => new { p.ProvinceName, p.ProvinceId }).ToList();
            return Json(provinces, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchCities(int provinceid)
        {
            var cities = CartDataAccess.searchcity(provinceid);
            cities.Select(c => new { c.CityName, c.CityId }).ToList();
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult WithSignUp(string name, string contact, string bday, string country, string province, string city, string address, string email, string password, string verpass, string[] comicdetails)
        {
            string errorMessage = "";

            if (!string.IsNullOrWhiteSpace(name) &&
                !string.IsNullOrWhiteSpace(contact) &&
                !string.IsNullOrWhiteSpace(country) &&
                !string.IsNullOrWhiteSpace(province) &&
                !string.IsNullOrWhiteSpace(city) &&
                !string.IsNullOrWhiteSpace(address) &&
                !string.IsNullOrWhiteSpace(email) &&
                !string.IsNullOrWhiteSpace(password) &&
                (comicdetails != null || comicdetails.Length > 0))
            {
                if (password.Equals(verpass))
                {
                    var dateTime = DateTime.Now;

                    var customerReg = CartDataAccess.Chkemail(email);
                    if (customerReg != null)
                    {
                        if (customerReg.Country == int.Parse(country) &&
                            customerReg.Province == int.Parse(province) &&
                            customerReg.City == int.Parse(city))
                        {
                            //update password
                            customerReg.Password = password;
                            CartDataAccess.UpdateCustomer(customerReg);
                        }
                        else
                        {
                            errorMessage = "ERROR: This Email already exists";
                        }
                    }
                    else
                    {
                        customerReg = new Registration
                        {
                            CompleteName = name,
                            ContactNumber = contact,
                            BirthDate = bday,
                            Country = int.Parse(country),
                            Province = int.Parse(province),
                            City = int.Parse(city),
                            Address = address,
                            Email = email,
                            Password = password,
                            DateCreated = dateTime
                        };

                        CartDataAccess.SaveCustomer(customerReg);
                    }

                    var batchNumber = (0 + CartDataAccess.Orderdetails()).ToString().PadLeft(10, '0');
                    foreach (var comic in comicdetails)
                    {
                        var comicId = Convert.ToInt32(comic.Split('!')[0]);
                        var comicQty = Convert.ToInt32(comic.Split('!')[1]);

                        var order = new OrderDetails
                        {
                            ComicId = comicId,
                            Quantity = comicQty,
                            UnitPrice = CartDataAccess.Unitprice(comicId),
                            DateCreated = dateTime,
                            CustId = customerReg.RegId,
                            BatchNumber = int.Parse(batchNumber),
                            OrderStatusId = 1,
                            IsView = false

                        };

                        CartDataAccess.SaveOrder(order);
                    };
                }
            }
            else
            {
                errorMessage = "ERROR: Please fill in the required fields";
            }

            return this.Content(errorMessage);
        }

        [HttpPost]
        public ActionResult WithoutSignUp(string name, string contact, string bday, string country, string province, string city, string address, string email, string[] comicdetails)
        {
            string errorMessage = "";

            if (!string.IsNullOrWhiteSpace(name) &&
                !string.IsNullOrWhiteSpace(contact) &&
                !string.IsNullOrWhiteSpace(country) &&
                !string.IsNullOrWhiteSpace(province) &&
                !string.IsNullOrWhiteSpace(city) &&
                !string.IsNullOrWhiteSpace(address) &&
                !string.IsNullOrWhiteSpace(email) &&
                (comicdetails != null || comicdetails.Length > 0))
            {

                var dateTime = DateTime.Now;
                var customerReg = CartDataAccess.Chkemail(email);

                if (customerReg != null)
                {

                }
                else
                {

                    customerReg = new Registration
                    {
                        CompleteName = name,
                        ContactNumber = contact,
                        BirthDate = bday,
                        Country = int.Parse(country),
                        Province = int.Parse(province),
                        City = int.Parse(city),
                        Address = address,
                        Email = email,
                        DateCreated = dateTime
                    };

                    CartDataAccess.SaveCustomer(customerReg);
                }

                var batchNumber = (0 + CartDataAccess.Orderdetails()).ToString().PadLeft(10,'0');
                foreach (var comic in comicdetails)
                {
                    var comicId = Convert.ToInt32(comic.Split('!')[0]);
                    var comicQty = Convert.ToInt32(comic.Split('!')[1]);
                    

                    var order = new OrderDetails
                    {
                        ComicId = comicId,
                        Quantity = comicQty,
                        UnitPrice = CartDataAccess.Unitprice(comicId),
                        DateCreated = dateTime,
                        CustId = customerReg.RegId,
                        BatchNumber = int.Parse(batchNumber),
                        OrderStatusId = 1,
                        IsView = false
                    };

                    CartDataAccess.SaveOrder(order);
                };
            }
            else
            {
                errorMessage = "Please fill in the required fields";
            }
            return this.Content(errorMessage);
        }
    }
}