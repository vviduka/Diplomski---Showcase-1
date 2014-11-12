using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudCord.Models;
using System.Data;

namespace CloudCord.Controllers
{
    public class CalculateController : Controller
    {
        //
        // GET: /Calculate/

        CloudCord_DB modelDb = new CloudCord_DB();

        public ActionResult Intro()
        {
            return View("Intro");
        }
        
        [HttpGet]
        public ActionResult ENCalculate()
        {
            var model = new ENconversion();
            return View("ENCalc");
            
        }

        public ActionResult XYCalulate(){
            return View("XYCalc");
        }

    }
}
