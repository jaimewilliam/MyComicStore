using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyComicStore.Models
{
    public class TypeOfUsers
    {
        [Key]
        public int TypeOfUserId { get; set; }
        public string TypeOfUser { get; set; }
    }
}