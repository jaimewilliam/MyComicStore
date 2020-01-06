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
    }
}