using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanLinhKienMayTinh.Models;
namespace QuanLyBanLinhKienMayTinh.Controllers
{
    public class GioHangController : Controller
    {

        QLBH_SQLEntities db = new QLBH_SQLEntities();

        public List<ItemGioHang> layGioHang()
        {
            //Giỏ hàng đã tồn tại
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                //Nếusession giỏ hàng chưa tồn tại thì khởi tạo giỏ hàng
                lstGioHang = new List<ItemGioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }


        //Thêm giỏ hàng thông thường (Load lại trang)
        public ActionResult ThemGioHang(int MaSP, String url)
        {
            //Kiểm tra sản phẩm có tồn tại trong CSDL hay không
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                //Trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }

            List<ItemGioHang> lstGioHang = layGioHang();

            //Trường hợp 1 sản phẩm đã tồn tại trong giỏ hàng
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck != null)
            {
                if (sp.SoLuongTon < spCheck.SoLuong)
                {
                    return View("ThongBao");
                }
                spCheck.SoLuong++;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                return Redirect(url);
            }


            ItemGioHang itemGH = new ItemGioHang(MaSP);
            if (sp.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            lstGioHang.Add(itemGH);
            return Redirect(url);

        }

        public int TinhTongSoLuong()
        {
            //Lấy giỏ hàng
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.SoLuong);
        }

        public int TinhTongTien()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.ThanhTien);
        }

        // GET: GioHang
        public ActionResult XemGioHang()
        {
            List<ItemGioHang> lstGioHang = layGioHang();
            return View(lstGioHang);
        }

        public ActionResult GioHangPartial()
        {
            if (TinhTongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            return PartialView();

        }
        [HttpGet]
        public ActionResult SuaGioHang(int MaSP)
        {
            //Kiểm tra xem giỏ hàng tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                //Trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }

            // Lấy list giỏ hàng từ session
            List<ItemGioHang> listGioHang = layGioHang();
            // Kiểm tra giỏ hàng có tồn tại trong giỏ hàng không
            ItemGioHang spcheck = listGioHang.SingleOrDefault(n => n.MaSP == MaSP);

            if (spcheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Lấy list giỏ hảng
            ViewBag.GioHang = listGioHang;

            return View(spcheck);
        }

        [HttpPost]
        public ActionResult CapNhatGioHang(int MaSP, int SoLuong)
        {
            //Kiểm tra số lượng tồn
            SANPHAM spcheck = db.SANPHAMs.Single(n => n.MaSP == MaSP);
            if (spcheck.SoLuongTon < SoLuong)
            {
                return View("ThongBao");
            }

            //Cập nhật số lượng
            List<ItemGioHang> listGH = layGioHang();
            ItemGioHang itemGHUpdate = listGH.Find(n => n.MaSP == MaSP);
            itemGHUpdate.SoLuong = SoLuong;
            itemGHUpdate.ThanhTien = itemGHUpdate.SoLuong * itemGHUpdate.DonGia;

            return RedirectToAction("XemGioHang");

        }

        [HttpGet]
        public ActionResult XoaGioHang(int MaSP)
        {
            //Kiểm tra xem giỏ hàng tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                //Trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }

            // Lấy list giỏ hàng từ session
            List<ItemGioHang> listGioHang = layGioHang();
            // Kiểm tra giỏ hàng có tồn tại trong giỏ hàng không
            ItemGioHang spcheck = listGioHang.SingleOrDefault(n => n.MaSP == MaSP);

            listGioHang.Remove(spcheck);

            return RedirectToAction("XemGioHang");
        }

        public ActionResult DatHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            THANHVIEN tv = Session["TaiKhoan"] as THANHVIEN;
            if (tv == null)
            {
                return RedirectToAction("XemGioHang");
            }

            //Thêm đơn hàng
            DONDATHANG ddh = new DONDATHANG();
            ddh.NgayDat = DateTime.Now;
            ddh.TinhTrangGiaoHang = "Đã đặt hàng";
            ddh.TaiKhoanTV = tv.TaiKhoanTV;
            ddh.DaGiao = false;
            ddh.DaThanhToan = false;
            db.DONDATHANGs.Add(ddh);
            db.SaveChanges();

            List<ItemGioHang> listGioHang = layGioHang();
            foreach (var item in listGioHang)
            {
                CHITIETDONDATHANG ctdh = new CHITIETDONDATHANG();
                ctdh.MaDDH = db.DONDATHANGs.Max(n => n.MaDDH);
                ctdh.MaSP = item.MaSP;
                ctdh.SoLuong = item.SoLuong;
                db.CHITIETDONDATHANGs.Add(ctdh);
                db.SaveChanges();
            }
            listGioHang.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult MuaTiep()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}