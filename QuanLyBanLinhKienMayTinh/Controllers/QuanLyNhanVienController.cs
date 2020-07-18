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
    public class QuanLyNhanVienController : Controller
    {
        private QLBH_SQLEntities db = new QLBH_SQLEntities();

        // GET: QuanLyNhanVien
        public ActionResult Index()
        {
            var nHANVIENs = db.NHANVIENs.Include(n => n.CHUCVU);
            return View(nHANVIENs.ToList());
        }

        // GET: QuanLyNhanVien/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIEN);
        }

        // GET: QuanLyNhanVien/Create
        public ActionResult Create()
        {
            ViewBag.MaCV = new SelectList(db.CHUCVUs, "MaCV", "TenCV");
            return View();
        }

        // POST: QuanLyNhanVien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaiKhoanNV,MatKhau,HoTen,DiaChi,Email,DienThoai,MaCV")] NHANVIEN nHANVIEN)
        {
            if (ModelState.IsValid)
            {
                db.NHANVIENs.Add(nHANVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCV = new SelectList(db.CHUCVUs, "MaCV", "TenCV", nHANVIEN.MaCV);
            return View(nHANVIEN);
        }

        // GET: QuanLyNhanVien/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCV = new SelectList(db.CHUCVUs, "MaCV", "TenCV", nHANVIEN.MaCV);
            return View(nHANVIEN);
        }

        // POST: QuanLyNhanVien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaiKhoanNV,MatKhau,HoTen,DiaChi,Email,DienThoai,MaCV")] NHANVIEN nHANVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHANVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCV = new SelectList(db.CHUCVUs, "MaCV", "TenCV", nHANVIEN.MaCV);
            return View(nHANVIEN);
        }

        // GET: QuanLyNhanVien/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIEN);
        }

        // POST: QuanLyNhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            db.NHANVIENs.Remove(nHANVIEN);
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
