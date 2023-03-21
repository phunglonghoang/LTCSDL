using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTCSDL_pro.Models;

namespace LTCSDL_pro.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult SachMoiPartial()
        {
            var lstSachMoi = db.Saches.Take(4).ToList();
            return PartialView(lstSachMoi);

        }
        public PartialViewResult DetailsPartial()
        {
            var lstDetails = db.Saches.Take(1).ToList();
            return PartialView(lstDetails);
        }
        public PartialViewResult HomePartial()
        {
            var lstHome = db.Saches.Take(4).ToList();
            return PartialView(lstHome);
        }
        public PartialViewResult Main1Partial()
        {
            var lstMain1 = db.ChuDes.Take(8).ToList();
            return PartialView(lstMain1);
        }
        public PartialViewResult Main2Partial()
        {
            var lstMain2 = db.TacGias.Take(8).ToList();
            return PartialView(lstMain2);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}