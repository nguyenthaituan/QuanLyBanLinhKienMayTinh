using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyBanLinhKienMayTinh.Models;

namespace QuanLyBanLinhKienMayTinh.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        private QLBH_SQLEntities db = new QLBH_SQLEntities();

        // GET: QuanLySanPham
        public ActionResult Index()
        {
            var sANPHAMs = db.SANPHAMs.Include(s => s.LOAISANPHAM).Include(s => s.NHACUNGCAP).Include(s => s.NHASANXUAT);
            return View(sANPHAMs.ToList());
        }

        // GET: QuanLySanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // GET: QuanLySanPham/Create
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LOAISANPHAMs, "MaLoai", "TenLoai");
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MaNCC", "TenNCC");
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs, "MaNSX", "TenNSX");
            return View();
        }

        // POST: QuanLySanPham/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,DonGia,NgayCapNhat,CauHinh,HinhAnh,SoLuongTon,SoLanMua,LuotXem,LuotBinhChon,Moi,DaXoa,MaNCC,MaNSX,MaLoai")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = new SelectList(db.LOAISANPHAMs, "MaLoai", "TenLoai", sANPHAM.MaLoai);
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MaNCC", "TenNCC", sANPHAM.MaNCC);
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs, "MaNSX", "TenNSX", sANPHAM.MaNSX);
            return View(sANPHAM);
        }

        // GET: QuanLySanPham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoai = new SelectList(db.LOAISANPHAMs, "MaLoai", "TenLoai", sANPHAM.MaLoai);
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MaNCC", "TenNCC", sANPHAM.MaNCC);
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs, "MaNSX", "TenNSX", sANPHAM.MaNSX);
            return View(sANPHAM);
        }

        // POST: QuanLySanPham/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,DonGia,NgayCapNhat,CauHinh,HinhAnh,SoLuongTon,SoLanMua,LuotXem,LuotBinhChon,Moi,DaXoa,MaNCC,MaNSX,MaLoai")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.LOAISANPHAMs, "MaLoai", "TenLoai", sANPHAM.MaLoai);
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MaNCC", "TenNCC", sANPHAM.MaNCC);
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs, "MaNSX", "TenNSX", sANPHAM.MaNSX);
            return View(sANPHAM);
        }

        // GET: QuanLySanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: QuanLySanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sANPHAM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
