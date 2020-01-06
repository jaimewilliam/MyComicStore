using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyComicStore.Models
{
    [Bind(Exclude = "OrderDetailsId")]
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsId { get; set; }
        public int ComicId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public int BatchNumber { get; set; }
        public int OrderStatusId { get; set; }
        public bool IsView { get; set; }
        public int CustId { get; set; }
        public int Assignedto_RegId { get; set; }
        //public int CityId { get; set; }
        //public int CountryId { get; set; }
        //public int ProvinceId { get; set; }

        //public virtual Registration Registration { get; set; }
        public virtual Comics Comics { get; set; }
        
    }
}