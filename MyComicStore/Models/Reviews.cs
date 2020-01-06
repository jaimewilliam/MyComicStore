using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyComicStore.Models
{
    public class Reviews
    {
        [Key]
        public int ReviewId { get; set; }
        public int ComicId { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewMessage { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
        
    }
}