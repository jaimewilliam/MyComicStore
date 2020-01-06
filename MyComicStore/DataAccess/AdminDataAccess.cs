using MyComicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyComicStore.DataAccess
{
    public class AdminDataAccess
    {
        private static ComicStoreEntities storeDB = new ComicStoreEntities();

        public static IEnumerable<OrderStatus> Getorderstatus()
        {
            IEnumerable<OrderStatus> batchorders = storeDB.OrderStatus.ToList();
            return batchorders;
        }

        public static IEnumerable<OrderDetails> Getorderdetails(int orderStatusId)
        {
            var batchorders = storeDB.OrderDetails.Where(x => x.OrderStatusId == orderStatusId).ToList();
            return batchorders;
        }

        public static IEnumerable<OrderDetails> GetOrderDeatilsByBatchNumber(int BatchNumber)
        {
            var quantity = storeDB.OrderDetails.Where(x => x.BatchNumber == BatchNumber);
            return quantity;
        }

        public static int Batchnumber(int orderDetailsId)
        {
            int batchnum = storeDB.OrderDetails.Where(bn => bn.OrderDetailsId == orderDetailsId).Select(bn => bn.BatchNumber).FirstOrDefault();
            return batchnum;
        }

        public static Registration Getcustomer(int custId)
        {
            var customer = storeDB.Registrations.Where(c => c.RegId == custId).FirstOrDefault();
            return customer;
        }

        public static Registration Assigned(int regId)
        {
            var assigned = storeDB.Registrations.Where(c => c.RegId == regId).FirstOrDefault();
            return assigned;
        }

        //public static IEnumerable<OrderDetails> Getorders(int bnum)
        //{
        //    IEnumerable<OrderDetails> orders = storeDB.OrderDetails.Include(o => o.Comics).OrderBy(o => o.OrderDetailsId).Where(o => o.BatchNumber == bnum);
        //    return orders;
        //}

        public static IEnumerable<OrderDetails> Orderstatus(int bnum)
        {
            IEnumerable<OrderDetails> orders = storeDB.OrderDetails.Where(o => o.BatchNumber == bnum).ToList();
            return orders;
        }

        public static IEnumerable<Registration> Deliver(int typeid)
        {
            var deliver = storeDB.Registrations.Where(d => d.TypeOfUserId == typeid).ToList();
            return deliver;
        }

        public static Registration Deliveries(int chkcookie)
        {
            var chkUser = storeDB.Registrations.Where(x => x.RegId == chkcookie).FirstOrDefault();
            return chkUser;
        }

        public static IEnumerable<OrderDetails> Bonum1()
        {
            IEnumerable<OrderDetails> bonum = storeDB.OrderDetails.Where(s => s.OrderStatusId == 2).GroupBy(x => x.BatchNumber).Select(r => r.FirstOrDefault());
            return bonum;
        }

        public static IEnumerable<OrderDetails> Bonum2(int chkcookie)
        {
            IEnumerable<OrderDetails> bonum = storeDB.OrderDetails.Where(x => x.Assignedto_RegId == chkcookie).Where(s => s.OrderStatusId == 2).GroupBy(x => x.BatchNumber).Select(r => r.FirstOrDefault());
            return bonum;
        }

        public static Registration Regs(int delcustId)
        {
            var registration = storeDB.Registrations.Where(l => l.RegId == delcustId).FirstOrDefault();
            return registration;
        }

        //public static Registration City(Registration registration)
        //{
        //    string city = storeDB.Cities.Where(c => c.CityId == registration.City).FirstOrDefault().CityName;
        //    return city;
        //}

        public static Registration Reguser(int newcookie)
        {
            var regUser = storeDB.Registrations.Where(x => x.RegId == newcookie).FirstOrDefault();
            return regUser;
        }

        public static MapCoordinates Map()
        {
            var gmap = storeDB.MapCoordinates.FirstOrDefault();
            return gmap;
        }

        public static IEnumerable<Comics> Comics()
        {
            IEnumerable<Comics> displaycomics = storeDB.Comics.ToList();
            return displaycomics;
        }

        public static Comics AddEditcomic(int comicid)
        {
            var displaycomics = storeDB.Comics.Where(c => c.ComicId == comicid).FirstOrDefault();
            return displaycomics;
        }

        public static void SaveComics(Comics comics)
        {
            storeDB.Comics.Add(comics);
            storeDB.SaveChanges();
        }

        public static Comics Editcomics(int comicId)
        {
            var comic = storeDB.Comics.Where(i => i.ComicId == comicId).FirstOrDefault();
            return comic;
        }

        public static void UpdateComics(Comics comics)
        {
            storeDB.SaveChanges();
        }

        public static Comics Delcomics(int comicid)
        {
            var delcomic = storeDB.Comics.Where(c => c.ComicId == comicid).FirstOrDefault();
            return delcomic;
        }

        public static void Delsave(Comics delcomic)
        {
            storeDB.Comics.Remove(delcomic);
            storeDB.SaveChanges();
        }

        public static IEnumerable<Categories> SelectCategory()
        {
            IEnumerable<Categories> category = storeDB.Categories.Where(c => c.Name != "All").ToList();
            return category;
        }

        public static Registration Usertype(int newcookie)
        {
            var usertype = storeDB.Registrations.Where(ut => ut.RegId == newcookie).FirstOrDefault();
            return usertype;
        }

        public static IEnumerable<AdminMenu> Menus()
        {
            IEnumerable<AdminMenu> menus = storeDB.AdminMenus.ToList();
            return menus;
        }
    }
}