using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Proiect.Models;
using Proiect.Models.MyModels;

namespace Proiect.Controllers.MyControllers
{
    public class ItemController : Controller
    {
        private DbCtx db = new DbCtx();
        // GET: Item

        [HttpGet]
        public ActionResult Index()
        {
            List<Item> items = db.Items.ToList();
            ViewBag.Items = items;

            return View();
        }

        [Authorize(Roles = "User, Admin")]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Item item = db.Items.Find(id);
                if (item != null)
                {
                    return View(item);
                }
                return HttpNotFound("Couldn't find the item with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing item id parameter!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult New()
        {
            Item item = new Item();
            item.WarrantyList = get_all_warr_types();
            item.CategoriesList = get_all_categories();
            item.Categories = new List<Category>();
            item.SellerList = get_all_sellers();


            return View(item);
        }

        [HttpPost]
        public ActionResult New(Item item_request)
        {
            try
            {
                item_request.WarrantyList = get_all_warr_types();
                item_request.SellerList = get_all_sellers();
                var selectedCategory = item_request.CategoriesList.Where(b => b.Checked).ToList();
                if (ModelState.IsValid)
                {
                    item_request.User_id = User.Identity.GetUserId();
                    item_request.Categories = new List<Category>();
                    for(int i = 0; i < selectedCategory.Count(); i++)
                    {
                        Category cat = db.Categories.Find(selectedCategory[i].Id);
                        item_request.Categories.Add(cat);
                    }
                    db.Items.Add(item_request);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(item_request);
            }
            catch(Exception e)
            {
                return View(item_request);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id.HasValue)
            {
                Item item = db.Items.Find(id);
                item.CategoriesList = get_all_categories();
                foreach(Category checkedCat in item.Categories)
                {
                    item.CategoriesList.FirstOrDefault(g => g.Id == checkedCat.category_id).Checked = true;
                }

                if(item == null)
                {
                    return HttpNotFound("Couldn't find item with id " + id.ToString());
                }
                item.WarrantyList = get_all_warr_types();
                item.SellerList = get_all_sellers();
                return View(item);
            }
            return HttpNotFound("Missing item id parameter");
        }

        [HttpPut]
        public ActionResult Edit(int id, Item item_request)
        {
            var selectedCategories = item_request.CategoriesList.Where(b => b.Checked).ToList();
            try
            {
                item_request.WarrantyList = get_all_warr_types();
                item_request.SellerList = get_all_sellers();
                if (ModelState.IsValid)
                {
                    Item item = db.Items.SingleOrDefault(b => b.Item_id.Equals(id));
                    
                    if(TryUpdateModel(item))
                    {
                        item.name = item_request.name;
                        item.price = item_request.price;
                        item.description = item_request.description;

                        //item.Categories.Clear();
                        item.Categories = new List<Category>();
                        for(int i = 0; i < selectedCategories.Count(); i++)
                        {
                            Category cat = db.Categories.Find(selectedCategories[i].Id);
                            item.Categories.Add(cat);
                        }

                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(item_request);
            }
            catch(Exception e)
            {
                return View(item_request);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Item item = db.Items.Find(id);

            if(item != null)
            {
                db.Items.Remove(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find item with id " + id.ToString());
        }

        [NonAction]
        public IEnumerable<SelectListItem> get_all_warr_types()
        {
            //generam o lista goala
            var select_list = new List<SelectListItem>();

            foreach(var warr in db.Warranties.ToList())
            {
                select_list.Add(new SelectListItem
                {
                    Value = warr.warranty_id.ToString(),
                    Text = warr.duration
                });
            }

            return select_list;
        }

        [NonAction]
        public IEnumerable<SelectListItem> get_all_sellers()
        {
            //generam o lista goala
            var select_list = new List<SelectListItem>();

            foreach (var v in db.Vanzares.ToList())
            {
                select_list.Add(new SelectListItem
                {
                    Value = v.Vanzare_id.ToString(),
                    Text = v.name
                });
            }

            return select_list;
        }

        [NonAction]
        public List<CheckBoxViewModel> get_all_categories()
        {
            var checkboxList = new List<CheckBoxViewModel>();
            foreach (var genre in db.Categories.ToList())
            {
                checkboxList.Add(new CheckBoxViewModel
                {
                    Id = genre.category_id,
                    Name = genre.name,
                    Checked = false
                });
            }
            return checkboxList;
        }

        [HttpPost]
        public ActionResult AddToCart(Item item_request)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    db.Items.Add(item_request);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(item_request);
            }
            catch (Exception e)
            {
                return View(item_request);
            }
        }

        
    }
}