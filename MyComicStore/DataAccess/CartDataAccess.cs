﻿using MyComicStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyComicStore.DataAccess
{
    public class CartDataAccess
    {
        private static ComicStoreEntities storeDB = new ComicStoreEntities();

        public static IEnumerable<Countries> GetCountries()
        {
            IEnumerable<Countries> country = storeDB.Countries.ToList();
            return country;
        }

        public static Registration Chkemail(string email)
        {
            var customerReg = storeDB.Registrations.Where(e => e.Email == email).FirstOrDefault();
            return customerReg;
        }

        public static int Orderdatails()
        {
            return storeDB.OrderDetails.Count();
        }

        public static void SaveCustomer(Registration customerReg)
        {
            storeDB.Registrations.Add(customerReg);
            storeDB.SaveChanges();
        }

        public static void UpdateCustomer(Registration customerReg)
        {
            storeDB.SaveChanges();
        }

        public static void SaveOrder(OrderDetails order)
        {
            storeDB.OrderDetails.Add(order);
            storeDB.SaveChanges();
        }

        public static IEnumerable<Provinces> searchprovince(int countryid)
        {
            var provinces = storeDB.Provinces.Where(p => p.CountryId == countryid).ToList();
            return provinces;
        }

        public static IEnumerable<Cities> searchcity(int provinceid)
        {
            var cities = storeDB.Cities.Where(c => c.ProvinceId == provinceid).ToList();
            return cities;
        }
    }
}