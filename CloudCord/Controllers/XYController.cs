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
    [Authorize]
    public class XYController : Controller
    {

        private CloudCord_DB db = new CloudCord_DB();


        //
        // GET: /XY/

        public ActionResult Index(string searchTag = null, DateTime? date1 = null, DateTime? date2 = null, int page = 1)
        {
            IEnumerable<string> fParam = new string[] { "Select filter parametar:", "Tag", "Date" };
            ViewData["myList"] = new SelectList(fParam.Select(x => new { text = x }), null, "text", "Select filter parametar:");

            if (searchTag == null) searchTag = "";

            if (searchTag.Length > 0)
            {
                var model = db.XYcalculation
                            .OrderByDescending(r => r.tagIz)
                            .Where(r => r.tagIz == searchTag)
                            .Select(r => new XYListViewModel
                            {
                                izracunId = r.izracunId,
                                tagIz = r.tagIz,
                                xCoord = r.xCoord,
                                yCoord = r.yCoord,
                                ECoord = r.ECoord,
                                NCoord = r.NCoord,
                                createdIz = r.createdIz
                            }).ToPagedList(page, 10);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Calculations", model);
                }

                return View(model);
            }
            else if (date1 != null || date2 != null)
            {
                if (date1 == null) date1 = DateTime.Parse("1/1/0001");
                if (date2 == null) date2 = DateTime.Parse("1/1/9999");
                var model = db.XYcalculation
                            .OrderByDescending(r => r.tagIz)
                            .Where(r => r.createdIz >= date1 && r.createdIz < date2)
                            .Select(r => new XYListViewModel
                            {
                                izracunId = r.izracunId,
                                tagIz = r.tagIz,
                                xCoord = r.xCoord,
                                yCoord = r.yCoord,
                                ECoord = r.ECoord,
                                NCoord = r.NCoord,
                                createdIz = r.createdIz
                            }).ToPagedList(page, 10);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Calculations", model);
                }

                return View(model);
            }
            else
            {
                var model = db.XYcalculation
                            .OrderByDescending(r => r.createdIz)
                            .Select(r => new XYListViewModel
                            {
                                izracunId = r.izracunId,
                                tagIz = r.tagIz,
                                xCoord = r.xCoord,
                                yCoord = r.yCoord,
                                ECoord = r.ECoord,
                                NCoord = r.NCoord,
                                createdIz = r.createdIz
                            }).ToPagedList(page, 10);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Calculations", model);
                }

                return View(model);

            }



        }



        //
        // GET: /XY/Create

        public ActionResult Create()
        {
            XYconversion model = new XYconversion();
            model.createdIz = DateTime.Today;
            return View(model);
        }

        //
        // POST: /XY/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOption(string submitButton, XYconversion xyconversion)
        {
            switch (submitButton)
            {
                case "Calculate":
                    return (Calculate(xyconversion));

                case "Save":
                    return (Save(xyconversion));

                default:
                    return View("Create");
            }
        }

        private ActionResult Calculate(XYconversion xyconversion)
        {

            if (ModelState.IsValid)
            {
                xyconversion.izracun();
            }
            return View("Create", xyconversion);
        }


        private ActionResult Save(XYconversion xyconversion)
        {
            if (ModelState.IsValid)
            {
                xyconversion.izracun();
                db.XYcalculation.Add(xyconversion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Create", xyconversion);
        }


        //
        // GET: /XY/Delete/5

        public ActionResult Delete(int id = 0)
        {
            XYconversion xyconversion = db.XYcalculation.Find(id);
            if (xyconversion == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.XYcalculation.Remove(xyconversion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}