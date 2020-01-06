using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyComicStore.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<ComicStoreEntities>
    {
        protected override void Seed(ComicStoreEntities context)
        {
            var categories = new List<Categories>
            {
                new Categories { Name = "All" },
                new Categories { Name = "Spider-Man" },
                new Categories { Name = "Iron Man" },
                new Categories { Name = "Capt. America" },
                new Categories { Name = "X-Men" },
            };



            new List<Comics>
            {
                new Comics { Title = "Marvel Comics", CategoryId = categories.Where(x => x.Name == "Spider-Man").Select(x => x.CategoryId).FirstOrDefault(),
                    Price = 8.99M, ImgUrl = "/Content/Images/spiderman.jpg" },
                new Comics { Title = "Phoenix", CategoryId = categories.Where(x => x.Name == "X-Men").Select(x => x.CategoryId).FirstOrDefault(),
                    Price = 10.99M, ImgUrl = "/Content/Images/Phoenix.jpg" },
                new Comics { Title = "Civil War", CategoryId = categories.Where(x => x.Name == "Capt. America").Select(x => x.CategoryId).FirstOrDefault(),
                    Price = 10.99M, ImgUrl = "/Content/Images/CaptAmer.jpg" }

            }.ForEach(a => context.Comics.Add(a));
        }
    }
}