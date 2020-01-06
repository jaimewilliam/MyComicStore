using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyComicStore.Models
{
    public class Comics
    {
        [Key]
        public int ComicId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public int TotalRatings { get; set; }
        public decimal TotalSellings { get; set; }
        public string Description { get; set; }
        public DateTime PubDate { get; set; }

    }
}