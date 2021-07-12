using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proiect.Models;
using Proiect.Models.MyValidation;

namespace Proiect.Models.MyModels
{
    public class Item
    {
        [Key]
        public int Item_id { get; set; }

        [NameValidator]
        [MinLength(2, ErrorMessage = "Item name cannot be less than 2!"),
            MaxLength(200, ErrorMessage = "Item name cannot be more than 200!")]
        public string name { get; set; }

        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Item price cannot be 0 or less than 0!")]
        public double price { get; set; }

        [MinLength(10, ErrorMessage = "Item description cannot be less than 10!"),
            MaxLength(500, ErrorMessage = "Item name cannot be more than 500!")]
        public string description { get; set; }

        //many to many
        public virtual ICollection<Category> Categories { get; set; }

        //many to one
        [ForeignKey("Warranty")]
        public int warranty_id { get; set; }
        public virtual Warranty Warranty { get; set; }

        public int Vanzare_id { get; set; }
        public virtual Vanzare Vanzare { get; set; }


        [NotMapped]
        public IEnumerable<SelectListItem> WarrantyList { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> SellerList { get; set; }

        [NotMapped]
        public List<CheckBoxViewModel> CategoriesList { get; set; }

        public string User_id { get; set; }
    }

    public class DbCtx : DbContext
    {
        public DbCtx() : base("DbConnectionString")
        {
            // set the initializer here
            Database.SetInitializer<DbCtx>(new Initp());
            //Database.SetInitializer<DbCtx>(new CreateDatabaseIfNotExists<DbCtx>());
            //Database.SetInitializer<DbCtx>(new DropCreateDatabaseIfModelChanges<DbCtx>());
            //Database.SetInitializer<DbCtx>(new DropCreateDatabaseAlways<DbCtx>());
        }
        // link Book and Publisher models to the DB (create tabels in the databse for the specified models)
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Warranty> Warranties { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Vanzare> Vanzares { get; set; }
    }

    public class Initp : DropCreateDatabaseAlways<DbCtx>
    {
        protected override void Seed(DbCtx context)
        {
            Warranty warr1 = new Warranty { warranty_id = 60, duration = "6 months" };
            Warranty warr2 = new Warranty { warranty_id = 61, duration = "12 months" };
            Warranty warr3 = new Warranty { warranty_id = 62, duration = "18 months" };
            Warranty warr4 = new Warranty { warranty_id = 63, duration = "24 months" };

            Category cat1 = new Category { category_id = 201, name = "Monitoare" };
            Category cat2 = new Category { category_id = 202, name = "Monitoare Gaming" };
            Category cat3 = new Category { category_id = 203, name = "Gadget-uri" };
            Category cat4 = new Category { category_id = 204, name = "Smartwatch" };
            Category cat5 = new Category { category_id = 204, name = "Componente PC" };
            Category cat6 = new Category { category_id = 204, name = "Placi grafice" };


            Vanzare v1 = new Vanzare { Vanzare_id = 101, name = "Altex" };
            Vanzare v2 = new Vanzare { Vanzare_id = 102, name = "Flanco" };
            Vanzare v3 = new Vanzare { Vanzare_id = 103, name = "CatMobile" };

            context.Warranties.Add(warr1);
            context.Warranties.Add(warr2);
            context.Warranties.Add(warr3);
            context.Warranties.Add(warr4);
            context.SaveChanges();
            /*
            context.Categories.Add(cat1);
            context.Categories.Add(cat2);
            context.Categories.Add(cat3);
            context.Categories.Add(cat4);
            context.Categories.Add(cat5);
            context.Categories.Add(cat6);
            */

            context.Vanzares.Add(v1);
            context.Vanzares.Add(v2);
            context.Vanzares.Add(v3);
            context.SaveChanges();
            context.Items.Add(new Item
            {
                name = "Laptop Lenovo",
                price = 2399.99,
                description = "Laptop Lenovo Y16, Intel I5 7600x, Nvidia Geforce 1050 4GB, 8GB RAM, 15,6",
                Warranty = warr2,
                Categories = new List<Category>
                {
                    new Category { name = "Laptop" },
                    new Category { name = "Laptop Gaming" },
                },
                Vanzare = v1

            });
            context.SaveChanges();
            context.Items.Add(new Item
            {
                name = "Smartphone Samsung Galaxy S20",
                price = 3059.99,
                description = "Smartphone Samsung Galaxy S20, Snapdragon Edition, Octa Core, 256 GB, 6GB RAM, Dual SIM, 5G, 4 Camere, Black",
                Warranty = warr3,
                Categories = new List<Category>
                {
                    new Category { name = "Mobile" },
                    new Category { name = "Smartphone" }
                },
                Vanzare = v3
            });
            context.SaveChanges();
            context.Items.Add(new Item
            {
                name = "Televizor Samsung Smart TV",
                price = 4398.99,
                description = "Televizor LED Samsung Smart TV QLED 55Q80TA Seria Q80T 140cm gri 4K UHD HDR",
                Warranty = warr1,
                Categories = new List<Category>
                {
                    new Category { name = "Televizoare" },
                    new Category { name = "Televizoare LED"},
                    new Category { name = "Smart TV"}
                },
                Vanzare = v2
            });
            context.SaveChanges();


            context.Items.Add(new Item
            {
                name = "Televizor LG Smart TV",
                price = 3699.99,
                description = "Televizor LED LG Smart TV QLED 55Q80TA Seria Q80T 140cm gri 4K UHD HDR",
                Warranty = warr1,
                Categories = new List<Category>
                {
                    new Category { name = "Televizoare" },
                    new Category { name = "Televizoare LED"},
                    new Category { name = "Smart TV"}
                },
                Vanzare = v2
            });
            /*
            context.Items.Add(new Item
            {
                name = "Smartphone Nokia 1.3",
                price = 399.99,
                description = "Smartphone Nokia 1.3, Quad Core, 16GB, 1GB RAM, Dual SIM, 4G, Cyan",
                Warranty = warr1,
                Categories = new List<Category>
                {
                    new Category { name = "Mobile" },
                    new Category { name = "Smartphone" }
                },
                Vanzare = v3
            });*/

            context.SaveChanges();
            context.Items.Add(new Item
            {
                name = "Tableta Amazon All-New Fire HD",
                price = 538.59,
                description = "Tableta Amazon All-new Fire HD, 8th generation (2018) cu reclame, Touch Screen, 8 inch, 16GB, Wi-Fi, built-in Alexa, Black",
                Warranty = warr1,
                Categories = new List<Category>
                {
                    new Category { name = "Televizoare" },
                    new Category { name = "Televizoare LED"},
                    new Category { name = "Smart TV"}
                },
                Vanzare = v1
            });
            context.SaveChanges();

            context.SaveChanges();
            base.Seed(context);
        }
    }
}