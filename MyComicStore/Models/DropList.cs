using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyComicStore.Models
{
    public class DropList
    {
        [Key]
        public int ListId { get; set; }
        public string ListName { get; set; }
        public int SortOrder { get; set; }
    }
}