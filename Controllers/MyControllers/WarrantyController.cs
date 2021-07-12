using Proiect.Models.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect.Controllers.MyControllers
{
    public class WarrantyController : Controller
    {
        private DbCtx db = new DbCtx();
        // GET: Warranty
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Warranty warr = db.Warranties.Find(id);
                if (warr != null)
                {
                    return View(warr);
                }
                return HttpNotFound("Couldn't find the item with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing item id parameter!");
        }

        [HttpGet]
        public ActionResult New()
        {
            Warranty warr = new Warranty();
            //warr.WarrantyList = get_all_warr_types();

            return View(warr);
        }

        [HttpPost]
        public ActionResult New(Warranty warr_request)
        {
            try
            {
                //item_request.WarrantyList = get_all_warr_types();
                if (ModelState.IsValid)
                {
                    db.Warranties.Add(warr_request);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(warr_request);
            }
            catch (Exception e)
            {
                return View(warr_request);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Warranty warr = db.Warranties.Find(id);

                if (warr == null)
                {
                    return HttpNotFound("Couldn't find item with id " + id.ToString());
                }
                //item.WarrantyList = get_all_warr_types();
                return View(warr);
            }
            return HttpNotFound("Missing item id parameter");
        }

        [HttpPut]
        public ActionResult Edit(int id, Warranty warr_request)
        {
            try
            {
                //item_request.WarrantyList = get_all_warr_types();
                if (ModelState.IsValid)
                {
                    Warranty warr = db.Warranties.SingleOrDefault(b => b.warranty_id.Equals(id));

                    if (TryUpdateModel(warr))
                    {
                        warr.duration = warr_request.duration;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(warr_request);
            }
            catch (Exception e)
            {
                return View(warr_request);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Warranty warr = db.Warranties.Find(id);

            if (warr != null)
            {
                db.Warranties.Remove(warr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find item with id " + id.ToString());
        }
    }
}