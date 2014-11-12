using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudCord.Models;
using PagedList;


namespace CloudCord.Controllers
{
    public class ENController : Controller
    {
        private CloudCord_DB db = new CloudCord_DB();

        //
        // GET: /EN/

        public ActionResult Index(string searchTag = null, DateTime? date1 = null, DateTime? date2 = null, int page = 1)
        {
            IEnumerable<string> fParam = new string[] { "Select filter parametar:", "Tag", "Date" };
            ViewData["myList"] = new SelectList(fParam.Select(x => new { text = x }), null, "text", "Select filter parametar:");

            if (searchTag == null) searchTag = "";

            if (searchTag.Length > 0)
            {
                var model = db.ENcalculation
                            .OrderByDescending(r => r.tagIz)
                            .Where(r => r.tagIz == searchTag)
                            .Select(r => new ENListViewModel
                                     {
                                         izracunId = r.izracunId,
                                         tagIz = r.tagIz,
                                         Eistok = r.Eistok,
                                         Nsjever = r.Nsjever,
                                         xp = r.xp,
                                         yp = r.yp,
                                         createdIz = r.createdIz
                                     }).ToPagedList(page, 10);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ENCalculations", model);
                }

                return View(model);
            }
            else if (date1 != null || date2 != null)
            {
                if (date1 == null) date1 = DateTime.Parse("1/1/0001");
                if (date2 == null) date2 = DateTime.Parse("1/1/9999");
                var model = db.ENcalculation
                            .OrderByDescending(r => r.tagIz)
                            .Where(r => r.createdIz >= date1 && r.createdIz < date2)
                            .Select(r => new ENListViewModel
                            {
                                izracunId = r.izracunId,
                                tagIz = r.tagIz,
                                Eistok = r.Eistok,
                                Nsjever = r.Nsjever,
                                xp = r.xp,
                                yp = r.yp,
                                createdIz = r.createdIz
                            }).ToPagedList(page, 10);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ENCalculations", model);
                }

                return View(model);

            }
            else
            {
                var model = db.ENcalculation
                            .OrderByDescending(r => r.tagIz)
                            .Select(r => new ENListViewModel
                            {
                                izracunId = r.izracunId,
                                tagIz = r.tagIz,
                                Eistok = r.Eistok,
                                Nsjever = r.Nsjever,
                                xp = r.xp,
                                yp = r.yp,
                                createdIz = r.createdIz
                            }).ToPagedList(page, 10);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ENCalculations", model);
                }

                return View(model);
            }
        }


        //
        // GET: /EN/Create

        public ActionResult Create()
        {
            ENconversion model = new ENconversion();
            model.createdIz = DateTime.Today;
            return View(model);
        }

        //
        // POST: /EN/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOption(string submitButton, ENconversion enconversion)
        {
            switch (submitButton)
            {
                case "Calculate":
                    return (Calculate(enconversion));

                case "Save":
                    return (Save(enconversion));

                default:
                    return View("Create");
            }
        }

        private ActionResult Calculate(ENconversion enconversion)
        {

            if (ModelState.IsValid)
            {
                enconversion.Izracun();
            }
            return View("Create", enconversion);
        }


        private ActionResult Save(ENconversion enconversion)
        {
            if (ModelState.IsValid)
            {
                enconversion.Izracun();
                db.ENcalculation.Add(enconversion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Create", enconversion);
        }

        //
        // GET: /EN/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ENconversion enconversion = db.ENcalculation.Find(id);
            if (enconversion == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.ENcalculation.Remove(enconversion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}