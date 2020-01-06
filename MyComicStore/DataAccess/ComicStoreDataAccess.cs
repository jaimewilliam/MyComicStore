using MyComicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyComicStore.DataAccess
{
    public class ComicStoreDataAccess
    {
        private static ComicStoreEntities storeDB = new ComicStoreEntities();

        public static IEnumerable<Categories> GetCategories()
        {
            IEnumerable<Categories> categories = storeDB.Categories.ToList();
            return categories;
        }


        public static string CategoryName(int categoryId)
        {
            string categoryName = storeDB.Categories.Where(cn => cn.CategoryId == categoryId).Select(cn => cn.Name).FirstOrDefault();
            return categoryName;
        }
        


        public static IEnumerable<Comics> GetComics(int categoryId, int listId, string comTitle)
        {

            IEnumerable<Comics> comicsModel;
            if (categoryId == 1)
            {
                if (listId == 1)
                {
                    comicsModel = storeDB.Comics.Where(x => x.Title.Contains(comTitle)).OrderBy(c => c.Title).ToList();
                }
                else if (listId == 2)
                {
                    comicsModel = storeDB.Comics.Where(x => x.Title.Contains(comTitle)).OrderByDescending(c => c.TotalRatings).ToList();
                }
                else if (listId == 3)
                {
                    comicsModel = storeDB.Comics.Where(x => x.Title.Contains(comTitle)).OrderByDescending(c => c.TotalSellings).ToList();
                }
                else if (listId == 4)
                {
                    comicsModel = storeDB.Comics.Where(x => x.Title.Contains(comTitle)).OrderBy(c => c.TotalRatings).ToList();
                }
                else if (listId == 5)
                {
                    comicsModel = storeDB.Comics.Where(x => x.Title.Contains(comTitle)).OrderBy(c => c.TotalSellings).ToList();
                }
                else if (listId == 6)
                {
                    comicsModel = storeDB.Comics.Where(x => x.Title.Contains(comTitle)).OrderBy(c => c.TotalSellings).ToList();
                }
                else if (listId == 7)
                {
                    comicsModel = storeDB.Comics.Where(x => x.Title.Contains(comTitle)).OrderByDescending(c => c.PubDate).ToList();
                }
                else
                {
                    comicsModel = storeDB.Comics.Where(x => x.Title.Contains(comTitle)).OrderBy(c => c.PubDate).ToList();
                }
            }
            else
            {
                if (listId == 1)
                {
                    comicsModel = storeDB.Comics.Where(x => x.CategoryId == categoryId && x.Title.Contains(comTitle)).OrderBy(c => c.Title).ToList();
                }
                else if (listId == 2)
                {
                    comicsModel = storeDB.Comics.Where(x => x.CategoryId == categoryId && x.Title.Contains(comTitle)).OrderByDescending(c => c.TotalRatings).ToList();
                }
                else if (listId == 3)
                {
                    comicsModel = storeDB.Comics.Where(x => x.CategoryId == categoryId && x.Title.Contains(comTitle)).OrderByDescending(c => c.TotalSellings).ToList();
                }
                else if (listId == 4)
                {
                    comicsModel = storeDB.Comics.Where(x => x.CategoryId == categoryId && x.Title.Contains(comTitle)).OrderBy(c => c.TotalRatings).ToList();
                }
                else if (listId == 5)
                {
                    comicsModel = storeDB.Comics.Where(x => x.CategoryId == categoryId && x.Title.Contains(comTitle)).OrderBy(c => c.TotalSellings).ToList();
                }
                else if (listId == 6)
                {
                    comicsModel = storeDB.Comics.Where(x => x.CategoryId == categoryId && x.Title.Contains(comTitle)).OrderBy(c => c.TotalSellings).ToList();
                }
                else if (listId == 7)
                {
                    comicsModel = storeDB.Comics.Where(x => x.CategoryId == categoryId && x.Title.Contains(comTitle)).OrderByDescending(c => c.PubDate).ToList();
                }
                else if (listId == 8)
                {
                    comicsModel = storeDB.Comics.Where(x => x.CategoryId == categoryId && x.Title.Contains(comTitle)).OrderBy(c => c.PubDate).ToList();
                }
                else
                {
                    comicsModel = storeDB.Comics.ToList();
                }
            }

            return comicsModel;
        }

        public static Comics GetComicDetails(int id)
        {
            var comics = storeDB.Comics.Find(id);
            return comics;
        }

        public static Comics SaveCustomer(int id)
        {
            var comics = storeDB.Comics.Find(id);
            return comics;
        }

        public static Registration reguser(int newcookie)
        {
            var regUser = storeDB.Registrations.Where(x => x.RegId == newcookie).FirstOrDefault();
            return regUser;
        }

        public static IEnumerable<Reviews> GetReviews(int Id)
        {
            IEnumerable<Reviews> reviews = storeDB.Reviews.Where(r => r.ComicId == Id).ToList();
            return reviews;
        }

        public static IEnumerable<DropList> DropLists()
        {
            IEnumerable<DropList> ddlist = storeDB.DropList.OrderBy(ddl => ddl.SortOrder).ToList();
            return ddlist;
        }



        //public static IEnumerable<Comics> Ddlist(int listId)
        //{
        //    IEnumerable<Comics> comicList;
        //    if (listId == 1)
        //    {
        //        comicList = storeDB.Comics.OrderBy(c => c.Title).ToList();
        //    }
        //    else if (listId == 2)
        //    {
        //        comicList = storeDB.Comics.OrderByDescending(c => c.TotalRatings).ToList();
        //    }
        //    else if (listId == 3)
        //    {
        //        comicList = storeDB.Comics.OrderByDescending(c => c.TotalSellings).ToList();
        //    }
        //    else if (listId == 4)
        //    {
        //        comicList = storeDB.Comics.OrderBy(c => c.TotalRatings).ToList();
        //    }
        //    else if (listId == 5)
        //    {
        //        comicList = storeDB.Comics.OrderBy(c => c.TotalSellings).ToList();
        //    }
        //    else if (listId == 6)
        //    {
        //        comicList = storeDB.Comics.OrderBy(c => c.TotalSellings).ToList();
        //    }
        //    else if (listId == 7)
        //    {
        //        comicList = storeDB.Comics.OrderByDescending(c => c.PubDate).ToList();
        //    }
        //    else 
        //    {
        //        comicList = storeDB.Comics.OrderBy(c => c.PubDate).ToList();
        //    }

        //    return comicList;
        //}
    }
}