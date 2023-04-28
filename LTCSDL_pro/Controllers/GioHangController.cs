using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Tree;
using LTCSDL_pro.Models;

namespace LTCSDL_pro.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        #region Giỏ Hàng
        //lấy giỏ hàng 
        public List<GioHang> LayGioHang()
        {
            //nếu giỏ hàng chưa có thì tiến hành khởi tạo
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //them gio hang 
        public ActionResult ThemGioHang(int iMaSach, string strURL)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy asen ra gio hang
            List<GioHang> lstGioHang = LayGioHang();
            //kiem tra sach có ton tai trong gio hang chưa 
            GioHang sanPham = lstGioHang.Find(n => n.iMaSach == iMaSach);
            //neu tồn tại thì cho sửa 
            if (sanPham == null)
            {
                sanPham = new GioHang(iMaSach);
                //add sp mới vào LIst
                lstGioHang.Add(sanPham);
                return Redirect(strURL);
            }
            else
            {
                sanPham.iSoLuong++;
                return Redirect(strURL);

            }

        }
        // cap nhat gio hang 
        public ActionResult CapNhatGioHang(int iMaSanPham, FormCollection f)
        {
            //kiem tra masanphamm
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSanPham);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanPham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSanPham);
            if (sanPham != null)
            {
                sanPham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction("GioHang");

        }
        //xoa gio hang
        public ActionResult XoaGioHang(int iMaSanPham)
        {
            //kiem tra masanphamm
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSanPham);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanPham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSanPham);
            if (sanPham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSach == iMaSanPham);
            }
            if (lstGioHang.Count==0)
            {
                return RedirectToAction("index","Home");
            }    
            return RedirectToAction("GioHang");
        }
        //Trang gio hang
        public ActionResult GioHang()
        {
            if (Session["GioHang"]==null)
            {
                return RedirectToAction("index", "Home");
            }    
            List<GioHang> lstGioHang = LayGioHang();
            
            return View(lstGioHang);

        }
        //tinh so luong va gia
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang !=null)
            {
                iTongSoLuong =lstGioHang.Sum(n=>n.iSoLuong); 
            } 
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }
        //tao partial Giỏ Hàng
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien=TongTien();
            return PartialView();
        }
        //view user chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();

            return View(lstGioHang);
        }
        #endregion
        #region đạt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            //Kiểm Tra đăng nhập 

            return View();
        }
        #endregion
    }


}