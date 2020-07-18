using QuanLyBanLinhKienMayTinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanLinhKienMayTinh.Controllers
{
    public class HomeController : Controller
    {
        private QLBH_SQLEntities db = new QLBH_SQLEntities();
        [HttpGet]
        public ActionResult Index()
        {
            var lstSSD = db.SANPHAMs.Where(n => n.MaLoai == 1 && n.Moi == true && n.DaXoa == false);
            var lstCPU = db.SANPHAMs.Where(n => n.MaLoai == 2 && n.Moi == true && n.DaXoa == false);
            var lstHDD = db.SANPHAMs.Where(n => n.MaLoai == 3 && n.Moi == true && n.DaXoa == false);
            var lstRAM = db.SANPHAMs.Where(n => n.MaLoai == 4 && n.Moi == true && n.DaXoa == false);
            var lstBANPHIM = db.SANPHAMs.Where(n => n.MaLoai == 5 && n.Moi == true && n.DaXoa == false);

            //gắn vào ViewBag
            ViewBag.lstSSD = lstSSD;
            ViewBag.lstCPU = lstCPU;
            ViewBag.lstHDD = lstHDD;
            ViewBag.lstRAM = lstRAM;
            ViewBag.lstBANPHIM = lstBANPHIM;
            return View();
        }

        public ActionResult MenuPartial()
        {
            //Truy vấn lấy về 1 list các sản phẩm
            var lstSP = db.SANPHAMs;
            return PartialView(lstSP);
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(THANHVIEN tv, FormCollection f)
        {

            THANHVIEN thanhvien = db.THANHVIENs.SingleOrDefault(n => n.TaiKhoanTV == tv.TaiKhoanTV);
            if(thanhvien == null)
            {
                tv.MaLoaiTV = 1;
                db.THANHVIENs.Add(tv);
                db.SaveChanges();
                ViewBag.Message = "Đăng ký thành công";

            }
            else
            {
                ViewBag.Message = "Tên tài khoản đã tồn tại";
            }

            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }


        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            //Kiểm tra tên đăng nhập và mật khẩu
            string sTaiKhoan = f["TaiKhoanTV"].ToString();
            string sMatKhau = f["MatKhau"].ToString();
            THANHVIEN tv = db.THANHVIENs.SingleOrDefault(n => n.TaiKhoanTV == sTaiKhoan && n.Matkhau == sMatKhau);
            if (tv != null)
            {
                Session["TaiKhoan"] = tv;
                return RedirectToAction("index");
            }
            else
            {
                NHANVIEN nv = db.NHANVIENs.SingleOrDefault(n => n.TaiKhoanNV == sTaiKhoan && n.MatKhau == sMatKhau && n.MaCV == 2);
                if (nv != null)
                {
                    Session["TaiKhoan"] = nv;
                    return RedirectToAction("Index", "Admin");
                }
            }

            return View();
        }

        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("index");
        }

    }
}