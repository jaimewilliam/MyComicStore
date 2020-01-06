using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyComicStore.Models
{
    public class Cities
    {
        [Key]
        public int CityId { get; set; }
        public String CityName { get; set; }
        public int ProvinceId { get; set; }
    }
}