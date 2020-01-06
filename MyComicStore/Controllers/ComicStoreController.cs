using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyComicStore.DataAccess;
using MyComicStore.Models;

namespace MyComicStore.Controllers
{
    public class ComicStoreController : Controller
    {
        ComicStoreEntities storeDB = new ComicStoreEntities();
        //AllModels allModels = new AllModels();

        // GET: ComicStore
        //[ChildActionOnly]
        public ActionResult Index()
        {
            if (Request.Cookies["login:cookie"] != null)
            {
                var newcookie = int.Parse(Request.Cookies["login:cookie"]["RegId"].ToString());
                var regUser = ComicStoreDataAccess.reguser(newcookie);

                if(regUser != null)
                {
                    ViewBag.Cookie = regUser.TypeOfUserId.ToString();
                }
            }

            IEnumerable<Categories> categories = ComicStoreDataAccess.GetCategories();
            return PartialView(categories);
        }

        //
        // GET: /ComicStore/Browse
        //[HttpGet]
        public ActionResult Browse(int categoryId, int listId, string comTitle)
        {
            IEnumerable<Comics> comicsModel = ComicStoreDataAccess.GetComics(categoryId, listId, comTitle);
            return PartialView(comicsModel);
        }

        //
        // GET: /ComicStore/Details/5
        public ActionResult Details(int id, int categoryId)
        {
            var comics = ComicStoreDataAccess.GetComicDetails(id);
            ViewBag.categoryName = ComicStoreDataAccess.CategoryName(comics.CategoryId);

            return PartialView(comics);
        }

        public ActionResult Reviews(int Id)
        {
            IEnumerable<Reviews> reviews = ComicStoreDataAccess.GetReviews(Id);
            return PartialView(reviews);
        }

        [ChildActionOnly]
        public ActionResult DropdownList()
        {
            
            if (Request.Cookies["login:cookie"] != null)
            {
                var newcookie = int.Parse(Request.Cookies["login:cookie"]["RegId"].ToString());
                var regUser = ComicStoreDataAccess.reguser(newcookie);

                if (regUser != null)
                {
                    ViewBag.Cookie = regUser.TypeOfUserId.ToString();
                }
            }

            IEnumerable<DropList> ddlist = ComicStoreDataAccess.DropLists();
            return PartialView(ddlist);
        }

        
        //public ActionResult ComicList(int listId)
        //{
        //    IEnumerable<Comics> comicList = ComicStoreDataAccess.Ddlist(listId);
        //    return PartialView(comicList);
        //}

        public ActionResult SearchBar()
        {
            //var searchTitle = storeDB.Comics.ToList();
            return PartialView();
        }

        public JsonResult AutoCompleteSearch(string comTitle)
        {
            var availableComics = storeDB.Comics.Where(x => x.Title.Contains(comTitle)).Select(x => x.Title).ToList();
            return Json(availableComics, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult SearchView(string comTitle)
        //{
        //    // TODO: LIKE query for Entity Framework ".Contains(key)"
        //    var searchTitle = storeDB.Comics.Where(ct => ct.Title.Contains(comTitle)).ToList();
        //    return PartialView(searchTitle);
        //}

        [HttpPost]
        public ActionResult LogOff()
        {
            string msg = "LOGOFF";

            HttpCookie mycookie = new HttpCookie("login:cookie");
            mycookie.Expires = DateTime.Now.AddDays(-10);
            Response.Cookies.Add(mycookie);

            return this.Content(msg);
        }
    }
}