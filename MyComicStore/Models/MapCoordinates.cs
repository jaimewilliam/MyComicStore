using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyComicStore.Models
{
    public class MapCoordinates
    {
        [Key]
        public int CoordinatesId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int CityId { get; set; }
        public int ProvinceId { get; set; }
    }
}