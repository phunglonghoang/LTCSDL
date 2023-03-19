using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTCSDL_pro.Controllers
{
    public class BookShopController : Controller
    {
        // GET: BookShop
        public ActionResult Index()
        {
            return View();
        }

        // GET: BookShop/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookShop/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookShop/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BookShop/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookShop/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BookShop/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookShop/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
