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
    public class QuanLyNhaSanXuatController : Controller
    {
        private QLBH_SQLEntities db = new QLBH_SQLEntities();

        // GET: QuanLyNhaSanXuat
        public ActionResult Index()
        {
            return View(db.NHASANXUATs.ToList());
        }

        // GET: QuanLyNhaSanXuat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHASANXUAT nHASANXUAT = db.NHASANXUATs.Find(id);
            if (nHASANXUAT == null)
            {
                return HttpNotFound();
            }
            return View(nHASANXUAT);
        }

        // GET: QuanLyNhaSanXuat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuanLyNhaSanXuat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNSX,TenNSX,ThongTin,Logo")] NHASANXUAT nHASANXUAT)
        {
            if (ModelState.IsValid)
            {
                db.NHASANXUATs.Add(nHASANXUAT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nHASANXUAT);
        }

        // GET: QuanLyNhaSanXuat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHASANXUAT nHASANXUAT = db.NHASANXUATs.Find(id);
            if (nHASANXUAT == null)
            {
                return HttpNotFound();
            }
            return View(nHASANXUAT);
        }

        // POST: QuanLyNhaSanXuat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNSX,TenNSX,ThongTin,Logo")] NHASANXUAT nHASANXUAT)
        {
            //System.Web.HttpPostedFileBase Avatar;
            var imgNV = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgNV.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("~/Content/HinhAnhSP/" + postedFileName);
            imgNV.SaveAs(path);

            if (ModelState.IsValid)
            {
                db.Entry(nHASANXUAT).State = EntityState.Modified;
                nHASANXUAT.Logo = postedFileName; //******************************************************888
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nHASANXUAT);
        }

        // GET: QuanLyNhaSanXuat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHASANXUAT nHASANXUAT = db.NHASANXUATs.Find(id);
            if (nHASANXUAT == null)
            {
                return HttpNotFound();
            }
            return View(nHASANXUAT);
        }

        // POST: QuanLyNhaSanXuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NHASANXUAT nHASANXUAT = db.NHASANXUATs.Find(id);
            db.NHASANXUATs.Remove(nHASANXUAT);
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
