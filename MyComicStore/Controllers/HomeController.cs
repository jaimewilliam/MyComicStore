using MyComicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyComicStore.Controllers
{
    public class HomeController : Controller
    {
        ComicStoreEntities storeDB = new ComicStoreEntities();

        
        
        public ActionResult DefaultView()
        {
            
            return View();
        }

        [ChildActionOnly]
        public ActionResult Logo()
        {
            var logo = storeDB.Logo.ToList();
            return PartialView(logo);
        }


    }
}