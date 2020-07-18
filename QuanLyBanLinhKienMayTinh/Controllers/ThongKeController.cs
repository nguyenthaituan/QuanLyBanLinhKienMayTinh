using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanLinhKienMayTinh.Models;

namespace QuanLyBanLinhKienMayTinh.Controllers
{
    public class ThongKeController : Controller
    {
        private QLBH_SQLEntities db = new QLBH_SQLEntities();
        // GET: ThongKe
        public ActionResult Index()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();//so luong nguoi truy cap tu application duoc tao
            ViewBag.SoNguoiDangOnline = HttpContext.Application["SoNguoiDangOnline"].ToString();
            ViewBag.TongDoanhThu = ThongkeTongDoanhThu();
            ViewBag.TongDDH = ThongKeDonHang();
            ViewBag.TongThanhVien = ThongKeThanhVien();
            ViewBag.ThongkeDoanhThuTheoThoiGian = ThongkeDoanhThuTheoThoiGian();
            return View();
        }

        public decimal ThongkeTongDoanhThu()
        {
            var tong = 0;
            var listCT = db.CHITIETDONDATHANGs;
            foreach(var i in listCT)
            {
                tong += i.SoLuong * i.SANPHAM.DonGia;
            }       
            return tong;
        }
        
        public decimal ThongkeDoanhThuTheoThoiGian()
        {
            var tong = 0;
            var listCT = db.CHITIETDONDATHANGs;

            var listHD = db.DONDATHANGs;
            foreach (var i in listHD)
            {
                //i.TinhTrangGiaoHang == "Đã duyệt đơn hàng"
                if (i.DaThanhToan == true && i.NgayGiao > ViewBag.TuNgay && i.NgayGiao < ViewBag.DenNgay)
                {
                    foreach (var j in listCT)
                    {
                        tong += j.SoLuong * j.SANPHAM.DonGia;
                    }
                   
                }    
            }
            return tong;
        }

        //public decimal ThongkeTongDoanhThuThang(int Thang, int Nam)
        //{
        //    var listDDH = db.DONDATHANGs.Where(n => n.NgayDat.Month == Thang && n.NgayDat.Year == Nam);

        //    decimal TongTien = 0;
        //    foreach (var item in listDDH)
        //    {
        //        TongTien += decimal.Parse(item.CHITIETDONDATHANGs.Sum(n => n.SoLuong * n.SANPHAM.DonGia).ToString());
        //    }
        //    return TongTien;
        //}

        public double ThongKeDonHang()
        {
            double slddh = db.DONDATHANGs.Count();
            return slddh;
        }

        public double ThongKeThanhVien()
        {
            double sltv = db.THANHVIENs.Count();
            return sltv;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();

            }
            base.Dispose(disposing);
        }
    }
}