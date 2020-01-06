using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyComicStore.Models
{
    public class Registration
    {
        [Key]
        public int RegId { get; set; }
        public string CompleteName { get; set; }
        public string ContactNumber { get; set; }
        public string BirthDate { get; set; }
        public int Country { get; set; }
        public int Province { get; set; }
        public int City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public int TypeOfUserId { get; set; }
    }
}