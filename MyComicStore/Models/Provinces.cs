using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyComicStore.Models
{
    public class Provinces
    {
        [Key]
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int CountryId { get; set; }
    }
}