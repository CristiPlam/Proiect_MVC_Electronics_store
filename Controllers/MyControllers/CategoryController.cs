using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proiect.Models.MyModels;

namespace Proiect.Controllers.MyControllers
{
    public class CategoryController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Category
        public ActionResult Index()
        {
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }

        public ActionResult New()
        {
            Category category = new Category();
            return View(category);
        }

        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Category cat = db.Categories.Find(id);
                if (cat != null)
                {
                    return View(cat);
                }
                return HttpNotFound("Couldn't find the item with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing item id parameter!");
        }
    }
}