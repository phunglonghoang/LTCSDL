using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTCSDL_pro.Models;
using PagedList;
using PagedList.Mvc;

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
        //chi tiet sach
        public ViewResult ChiTietSach(int MaSach =0)
        {
            Sach sach = db.Saches.SingleOrDefault(n=>n.MaSach == MaSach);
            if(sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        public PartialViewResult DetailsPartial()
        {
            var lstDetails = db.Saches.Take(1).ToList();
            return PartialView(lstDetails);
        }

        public PartialViewResult HomePartial( int? page )
        {
            //tao bien 

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            var lstHome = db.Saches.ToList().OrderBy(n=>n.GiaBan).ToPagedList(pageNumber,pageSize);
            return PartialView(lstHome);
        }
        public PartialViewResult Main1Partial()
        {
            var lstMain1 = db.ChuDes.Take(8).ToList();
            return PartialView(lstMain1);
        }
        //Sach theo chủ đè 
        public ViewResult SachChuDe(int MaChuDe = 0)
        {
            //kiem tra chu de 
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe == MaChuDe);
            if(cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            
           
            //truy xuất danh sach theo chu de \
            List<Sach> lstsach = db.Saches.Where(n => n.MaChuDe == MaChuDe).OrderBy(n => n.GiaBan).ToList();
            if (lstsach.Count == 0)
            {
                ViewBag.Sach = "tạm thời không có sách nào thuộc chủ đề này ";
            }
            return View(lstsach);
        }
        public ViewResult TongTacGia(int MaTacgia=0)
        {
            //kiem tra chu de 
            TacGia tg = db.TacGias.SingleOrDefault(n => n.MaTacGia == MaTacgia);
            if (tg == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //truy xuất danh sach theo tacgia 
            List<Sach> lsttg = db.Saches.Where(n => n.MaTacGia == MaTacgia).OrderBy(n=>n.GiaBan).ToList();
            if (lsttg.Count == 0)
            {
                ViewBag.Sach = "tạm thời không có sách nào thuộc Tác Giả này ";
            }
            return View(lsttg);
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