using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LTCSDL_pro.Models;


namespace LTCSDL_pro.Controllers
{
    public class NguoiDungController : Controller
    {
        QuanLyBanSachEntities2 db = new QuanLyBanSachEntities2();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult DangKy(KhachHang kh)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.KhachHangs.Add(kh);
        //        db.SaveChanges();
        //    }
        //    return View();
        //}
        //public ActionResult DangKy(KhachHang kh)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Kiểm tra dữ liệu đã tồn tại
        //        if (kiemTraTonTai(kh.TaiKhoan))
        //        {
        //            ModelState.AddModelError("TaiKhoan", "Tên tài khoản đã tồn tại!");
        //            return View(kh);
        //        }
        //        else
        //        {
        //            // Thêm sản phẩm vào cơ sở dữ liệu
        //            // ...
        //            return RedirectToAction("DangNhap");
        //        }
        //    }
        //    else
        //    {
        //        return View(kh);
        //    }
        //}
        public ActionResult DangKy(KhachHang kh)
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            string connectionString = "Data Source=DINHLUAN;Initial Catalog=QuanLyBanSach;Integrated Security=True";

            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Câu truy vấn kiểm tra tên đăng nhập
                string query = "SELECT * FROM KhachHang WHERE TaiKhoan=@TaiKhoan";

                // Tạo đối tượng SqlCommand để thực hiện câu truy vấn
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm tham số vào câu truy vấn
                    command.Parameters.AddWithValue("@TaiKhoan", kh.TaiKhoan);

                    // Mở kết nối đến cơ sở dữ liệu
                    connection.Open();

                    // Thực hiện câu truy vấn và lấy kết quả trả về
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // Tên đăng nhập đã tồn tại, trả về một thông báo lỗi hoặc yêu cầu người dùng nhập tên đăng nhập khác
                            ViewBag.ThongBao = "Tên đăng nhập này đã tồn tại!";
                            return View();
                        }
                        else
                        {
                            // Thực hiện đăng ký tài khoản cho khách hàng
                            // ...
                            if (ModelState.IsValid)
                            {
                                db.KhachHangs.Add(kh);
                                db.SaveChanges();
                            }
                            ViewBag.ThongBao = "Đăng ký thông tin khách hàng đã thành công.";
                            return View();
                        }
                    }
                }
            }
        }

        


        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection k)
        {
            string sTaiKhoan = k["txtTaiKhoan"].ToString();
            string sMatKhau = k.Get("txtMatKhau").ToString();
            KhachHang kh = db.KhachHangs.FirstOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            if (kh != null)
            {
                //ViewBag.ThongBao = "Chúc mừng bạn đã đăng nhập thành công!";
                Session["TaiKhoan"] = kh;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không chính xác!";
            return View();
        }


        public ActionResult DangXuat()
        {
            // Clear the user's session and authentication cookies
            Session.Clear();
            FormsAuthentication.SignOut();

            // Redirect the user to the login page
            return RedirectToAction("Index", "Home");
        }


    }
}