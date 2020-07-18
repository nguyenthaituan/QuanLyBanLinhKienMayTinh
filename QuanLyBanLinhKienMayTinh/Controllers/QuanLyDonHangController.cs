using QuanLyBanLinhKienMayTinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace QuanLyBanLinhKienMayTinh.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        QLBH_SQLEntities db = new QLBH_SQLEntities();
        // GET: QuanLyDonHang
        public ActionResult Index()
        {
            var listDDH = db.DONDATHANGs.Where(n => n.TinhTrangGiaoHang == "Đã đặt hàng");
            return View(listDDH.ToList());
        }

        public ActionResult Details(int id)
        {
            var listCTDH = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == id);
            return View(listCTDH.ToList());
        }

        public ActionResult Accept(int id)
        {
            DONDATHANG donhang = db.DONDATHANGs.Find(id);
            donhang.TinhTrangGiaoHang = "Đã duyệt đơn hàng";
            db.Entry(donhang).State = EntityState.Modified;

            var listCT = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == donhang.MaDDH);
            var sp = new SANPHAM();
            foreach (var CT in listCT)
            {
                sp = db.SANPHAMs.Find(CT.MaSP);
                sp.SoLuongTon = sp.SoLuongTon - CT.SoLuong;
                db.Entry(sp).State = EntityState.Modified;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}