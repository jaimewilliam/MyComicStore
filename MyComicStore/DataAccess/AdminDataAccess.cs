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
    }
}