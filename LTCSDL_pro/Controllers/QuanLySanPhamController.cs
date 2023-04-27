using System;
using System.Web.UI.WebControls;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTCSDL_pro.Models;
using System.Collections.Generic;
using PagedList.Mvc;
using PagedList;
using System.Configuration;

namespace LTCSDL_pro.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        QuanLyBanSachEntities2 db = new QuanLyBanSachEntities2();
        // GET: QuanLySanPham
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.Saches.ToList().OrderBy(n =>n.MaSach).ToPagedList(pageNumber,pageSize));
        }
        //Thêm mới sách
        [HttpGet]
        public ActionResult ThemMoi()
        {
            //Đưa dữ liệu vào DropdownList
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n=>n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXBs.ToList().OrderBy(n=>n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoi(Sach sach)
        {
            ////Lưu tên của file
            //var fileName = Path.GetFileName(fileUpload.FileName);
            ////string fileUpLoad = ConfigurationManager.AppSettings["fileUpload"];
            ////QuanLyBanSachEntities1 imageModel = GetImageModelFromDatabase(Sach);
            //var extention = Path.GetExtension(fileUpload.FileName);
            //fileName = fileName + extention;
            //sach.AnhBia = "~/Asset/img/" + fileName;
            ////Lưu đường dẫn của file
            //var path = Path.Combine(Server.MapPath("~/Asset/img"), sach.AnhBia);
            ////string imageName = imageModel.FileName;
            ////string imagePathAandName = Path.Combine(fileUpLoad, imageName);
            ////Kiểm tra hình ảnh đã tồn tại chưa
            //if (System.IO.File.Exists(path))
            //{
            //    ViewBag.ThongBao = "Ảnh đã tồn tại";

            //}
            //else
            //{
            //    fileUpload.SaveAs(path);
                
            //}
            if(ModelState.IsValid)
            {
                db.Saches.Add(sach);
                db.SaveChanges();
            }
            //Đưa dữ liệu vào DropdownList
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXBs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }

        //Chỉnh sửa sản phẩm
        [HttpGet]
        public ActionResult ChinhSua(int MaSach)
        {
            //Lấy ra đối trượng sách theo mã sách
            Sach sach = db.Saches.SingleOrDefault(n=>n.MaSach == MaSach);
            if (sach == null)
            { 
                return null;
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXBs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View(sach);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua (Sach sach, FormCollection f)
        {
            Sach sach1 = db.Saches.SingleOrDefault(n => n.MaSach == sach.MaSach);
            sach1.MoTa = sach.MoTa;
            sach1.MoTa = f.Get("abc").ToString();
            sach1.MoTa = f["abc"].ToString();
            db.SaveChanges();
            if(ModelState.IsValid)
            {
                //Thực hiện cập nhập trong Model
                db.Entry(sach).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXBs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            return View(sach);

        }

        //Hiển thị sản phẩm
        public ActionResult HienThi(int MaSach)
        {
            //Lấy ra đối trượng sách theo mã sách
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                return null;
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXBs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View(sach);
        }

        //Xóa sản phẩm
        [HttpGet]//Dùng để đọc dữ liệu, xuất ra 1 trang để hiển thị thông tin sản phẩm phải xóa     
        public ActionResult Xoa(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }

        [HttpPost, ActionName("Xoa")]

        public ActionResult XacNhanXoa(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}