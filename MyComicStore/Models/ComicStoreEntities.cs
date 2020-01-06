using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;

namespace MyComicStore.Models
{
    public class ComicStoreEntities : DbContext
    {
        public DbSet<Comics> Comics { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Logo> Logo { get; set; }
        public DbSet<DropList> DropList { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<Provinces> Provinces { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Registration> Registrations {get; set;}
        public DbSet<OrderDetails> OrderDetails { get; set; }

        public System.Data.Entity.DbSet<MyComicStore.Models.AdminMenu> AdminMenus { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<TypeOfUsers> TypeOfUsers { get; set; }
        public DbSet<MapCoordinates> MapCoordinates { get; set; }
    }
}