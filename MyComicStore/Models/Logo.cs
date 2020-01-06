using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyComicStore.Models
{
    public class Logo
    {
        [Key]
        public int LogoId { get; set; }
        public string ImgLogo { get; set; }
        public string AppName { get; set; }
    }
    
}