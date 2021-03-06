﻿using MyComicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Web.Helpers;
using System.Drawing;
using MyComicStore.DataAccess;

namespace MyComicStore.Controllers
{
    public class AdminController : Controller
    {
        ComicStoreEntities storeDB = new ComicStoreEntities();

        // GET: Admin
        public ActionResult AdminOrders()
        {
            if (Request.Cookies["login:cookie"] == null)
            {
                return RedirectToAction("DefaultView", "Home");
            }
            else
            {
                //string cookie = Request.Cookies["login:cookie"]["TypeOfUser"].ToString();
                //ViewBag.Cookie = cookie;
            }
            IEnumerable <OrderStatus> batchorders = AdminDataAccess.Getorderstatus();
            return PartialView(batchorders);

        }

        
        public ActionResult AdminBatchOrders(int orderStatusId)
        {

            IEnumerable<OrderDetails> batchorders = AdminDataAccess.Getorderdetails(orderStatusId).GroupBy(x => x.BatchNumber).Select(r => r.FirstOrDefault());
            foreach (var batch in batchorders)
            {
                var orderDetails = AdminDataAccess.GetOrderDeatilsByBatchNumber(batch.BatchNumber);
                
                batch.Quantity = orderDetails.Sum(x => x.Quantity);
                batch.UnitPrice = orderDetails.Sum(x => x.UnitPrice);
                //batch.UnitPrice = orderDetails.Sum(x => (x.Quantity * x.UnitPrice));
            }

            return PartialView(batchorders);
        }

        public ActionResult AdminOrderDetails(int bnum, int orderDetailsId, int custId, int regId)
        {
            ViewBag.batchnumber = AdminDataAccess.Batchnumber(orderDetailsId);

            var customer = AdminDataAccess.Getcustomer(custId);
            ViewBag.Name = customer.CompleteName;
            ViewBag.Contact = customer.ContactNumber;
            ViewBag.Address = customer.Address;
            ViewBag.Email = customer.Email;

            if (regId != 0)
            {
                ViewBag.Assigned = AdminDataAccess.Assigned(regId).CompleteName;
            }

            var orders = storeDB.OrderDetails.Include(o => o.Comics).OrderBy(o => o.OrderDetailsId).Where(o => o.BatchNumber == bnum);

            foreach (var batchitem in orders)
            {
                batchitem.IsView = true;
            }
            storeDB.SaveChanges();

            return PartialView(orders.ToList());
        }

        public ActionResult InTransit(int bnum, int cid)
        {
            string errorMessage = "";

            IEnumerable<OrderDetails> orders = AdminDataAccess.Orderstatus(bnum);

            foreach (var batchitem in orders)
            {
                batchitem.OrderStatusId = 2;
                batchitem.Assignedto_RegId = cid;
            }
            storeDB.SaveChanges();

            return this.Content(errorMessage);
        }

        public ActionResult Delivered(int bnum)
        {
            string errorMessage = "";

            IEnumerable<OrderDetails> orders = AdminDataAccess.Orderstatus(bnum);

            foreach (var batchitem in orders)
            {
                batchitem.OrderStatusId = 3;
            }
            storeDB.SaveChanges();

            return this.Content(errorMessage);
        }

        public JsonResult Deliverby(int typeid)
        {
            //var deliver = storeDB.Registrations.Where(d => d.TypeOfUserId == typeid).Select(d => d.CompleteName).ToList();
            var deliver = AdminDataAccess.Deliver(typeid);
            deliver.Select(d => new { d.CompleteName, d.RegId}).ToList();
            return Json(deliver, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dashboard()
        {
            return PartialView();
        }

        public ActionResult Deliveries()
        {
            if (Request.Cookies["login:cookie"] == null)
            {
                return RedirectToAction("DefaultView", "Home");
            }
            else
            {
                var chkcookie = int.Parse(Request.Cookies["login:cookie"]["RegId"].ToString());
                var chkUser = AdminDataAccess.Deliveries(chkcookie);
                IEnumerable<OrderDetails> bonum = AdminDataAccess.Bonum1();

                if (chkUser.TypeOfUserId == 2)
                {
                    bonum = AdminDataAccess.Bonum2(chkcookie);

                }

                return PartialView(bonum);
            }
        }


        public JsonResult Findloc(int delcustId)
        {
            
            var registration = AdminDataAccess.Regs(delcustId);
            
            string name = registration.CompleteName;
            string email = registration.Email;
            string phone = registration.ContactNumber;
            string city = storeDB.Cities.Where(c => c.CityId == registration.City).FirstOrDefault().CityName;
            string province = storeDB.Provinces.Where(p => p.ProvinceId == registration.Province).FirstOrDefault().ProvinceName;
            string country = storeDB.Countries.Where(c => c.CountryId == registration.Country).FirstOrDefault().CountryName;
            string fulladdress = registration.Address + ", " + city + ", " + province + ", " + country;
            decimal latitude = storeDB.MapCoordinates.Where(c => c.CityId == registration.City).Where(p => p.ProvinceId == registration.Province).FirstOrDefault().Latitude;
            decimal longitude = storeDB.MapCoordinates.Where(c => c.CityId == registration.City).Where(p => p.ProvinceId == registration.Province).FirstOrDefault().Longitude;

            return Json(new { name = name, fulladdress = fulladdress, email = email, phone = phone, lat = latitude, lon = longitude }, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult Gmap()
        {
            if (Request.Cookies["login:cookie"] != null)
            {
                var newcookie = int.Parse(Request.Cookies["login:cookie"]["RegId"].ToString());
                var regUser = AdminDataAccess.Reguser(newcookie);

                if (regUser != null)
                {
                    ViewBag.Cookie = regUser.TypeOfUserId.ToString();
                }
            }
            var gmap = AdminDataAccess.Map();
            return PartialView(gmap);
        }

        public ActionResult Comics()
        {
            if (Request.Cookies["login:cookie"] == null)
            {
                return RedirectToAction("DefaultView", "Home");
            }
            IEnumerable<Comics> displaycomics = AdminDataAccess.Comics();
            return PartialView(displaycomics);
        }

        public ActionResult AddEditComics(int comicid)
        {
            var displaycomics = AdminDataAccess.AddEditcomic(comicid);
            return PartialView(displaycomics);
        }
        

        [HttpPost]
        public ActionResult SaveComic(string Base, string Price, string Title, string Categ, string Descript, int comicId)
        {
            string errorMessage = "";

            if (comicId == 0)
            {
                if (!string.IsNullOrWhiteSpace(Base) &&
                !string.IsNullOrWhiteSpace(Price) &&
                !string.IsNullOrWhiteSpace(Title) &&
                !string.IsNullOrWhiteSpace(Categ) &&
                !string.IsNullOrWhiteSpace(Descript))
                {

                    string strm = Base;
                    var myfilename = string.Format(@"{0}", Guid.NewGuid());

                    //Generate unique filename
                    string filefolder = "~/Content/Images/" + myfilename + ".jpeg";
                    string filepath = Server.MapPath(filefolder);
                    var bytes = Convert.FromBase64String(strm);
                    using (var imageFile = new FileStream(filepath, FileMode.Create))
                    {
                        imageFile.Write(bytes, 0, bytes.Length);
                        imageFile.Flush();
                    }
                    var dateTime = DateTime.Now;

                    var comics = new Comics
                    {
                        ImgUrl = filefolder.Replace("~", ""),
                        Price = int.Parse(Price),
                        Title = Title,
                        CategoryId = int.Parse(Categ),
                        Description = Descript,
                        PubDate = dateTime
                    };

                    AdminDataAccess.SaveComics(comics);

                    var id = comics.ComicId;

                    errorMessage = id.ToString();
                }
                else
                {
                    errorMessage = "Please fill in the required fields";
                }
            }
            else
            {
                var comic = AdminDataAccess.Editcomics(comicId);
                if (comic != null)
                {
                    if (comic.ComicId == comicId)
                    {
                        if (!string.IsNullOrEmpty(Base))
                        {
                            string strm = Base;
                            var myfilename = string.Format(@"{0}", Guid.NewGuid());

                            //Generate unique filename
                            string filefolder = "~/Content/Images/" + myfilename + ".jpeg";
                            string filepath = Server.MapPath(filefolder);
                            var bytes = Convert.FromBase64String(strm);
                            using (var imageFile = new FileStream(filepath, FileMode.Create))
                            {
                                imageFile.Write(bytes, 0, bytes.Length);
                                imageFile.Flush();
                            }

                            comic.ImgUrl = filefolder.Replace("~", "");
                        }

                        comic.Price = decimal.Parse(Price);
                        comic.Title = Title;
                        comic.CategoryId = int.Parse(Categ);
                        comic.Description = Descript;

                        AdminDataAccess.UpdateComics(comic);

                        errorMessage = "Updated!";
                    }

                }

            }

            return this.Content(errorMessage);
        }

        public JsonResult DeleteComics(int comicid)
        {
            
            var delcomic = AdminDataAccess.Delcomics(comicid);
            if (delcomic != null)
            {
                AdminDataAccess.Delsave(delcomic);
            }
            return Json(delcomic, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectCategory()
        {

            IEnumerable<Categories> category = AdminDataAccess.SelectCategory();
            return PartialView(category);
        }

        public ActionResult Home()
        {
            if (Request.Cookies["login:cookie"] == null)
            {
                return RedirectToAction("DefaultView", "Home");
            }

            return View();
        }

        
        public ActionResult AdminSidebarL()
        {
            var menus = storeDB.AdminMenus.ToList();

            //***fetch the cookies information into a HttpCookie!
            HttpCookie cookie = Request.Cookies["login:cookie"];
            if (cookie != null)
            {
                var newcookie = int.Parse(cookie["RegId"].ToString());
                var usertype = AdminDataAccess.Usertype(newcookie);

                if (usertype != null)
                {
                    if (usertype.TypeOfUserId == 2)
                    {
                        menus = AdminDataAccess.Menus().Where(i => i.MenuId != 2).ToList();
                    }
                }
            }
            
            return PartialView(menus);
        }

    }
}