using QuanLyBanLinhKienMayTinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanLinhKienMayTinh.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            NHANVIEN nv = Session["TaiKhoan"] as NHANVIEN;
            if (nv == null)
            {
                return Content("<script>alert(\"Bạn ơi, bạn không vào đươc trang này đâu, khi khác bạn nhé ahihi !!! \")</script>");
            }
            return View();
        }
    }
}