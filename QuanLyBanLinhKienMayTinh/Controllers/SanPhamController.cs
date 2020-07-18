using QuanLyBanLinhKienMayTinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Web.UI;
using PagedList;
namespace QuanLyBanLinhKienMayTinh.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        private QLBH_SQLEntities db = new QLBH_SQLEntities();
        
        //Tạo 2 partial view sản phẩm 1 và 2 để hiển thị sản phẩm theo 2 style khác nhau
        [ChildActionOnly]
        public ActionResult SanPhamStyle1Partial()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult SanPhamStyle2Partial()
        {
            return PartialView();
        }

        public ActionResult XemChiTiet(int? id)
        {
            //Kiểm tra tham số truyền vào có rỗng hay không ?
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Nếu không thì truy xuất csdl lấy ra sản phẩm tương ứng 
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id && n.DaXoa == false );
            if(sp == null)
            {
                //Thông báo nếu như không có sản phẩm đó
                return HttpNotFound();
            }
            return View(sp);
        }

        public ActionResult SanPham(int? MaLoaiSP, int MaNSX, int? page)
        {
            /*Load sản phẩm dựa theo 2 tiêu chí là mã loại sản phẩm và mã nhà sản xuất*/
            if(MaLoaiSP == null || MaNSX == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lstSP = db.SANPHAMs.Where(n => n.MaLoai == MaLoaiSP && n.MaNSX == MaNSX);
            if(lstSP.Count() == 0)
            {
                return HttpNotFound();
            }

            int PageSize = 3;
            //Tạo biến thứ 2: Số trang hiện tại
            int PageNumber = (page ?? 1);
            ViewBag.MaLoaiSP = MaLoaiSP;
            ViewBag.MaNSX = MaNSX;
            return View(lstSP.OrderBy(n => n.MaSP).ToPagedList(PageNumber, PageSize));
        }

        public ActionResult SanPhamALL(int? MaLoaiSP, int? page)
        {
            /*Load sản phẩm dựa theo 1 tieu chi la loai sp*/
            if (MaLoaiSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lstSP = db.SANPHAMs.Where(n => n.MaLoai == MaLoaiSP);
            if (lstSP.Count() == 0)
            {
                return HttpNotFound();
            }
            //Thực hiện chức năng phân trang
            //Tạo biến số sản phẩm trên trang
            int PageSize = 3;
            //Tạo biến thứ 2: Số trang hiện tại
            int PageNumber = (page ?? 1 );
            ViewBag.MaLoaiSP = MaLoaiSP;
            return View(lstSP.OrderBy(n=>n.MaSP).ToPagedList(PageNumber,PageSize));
        }

        [HttpGet]

        public ActionResult SanPhamTimKiem(String TenSP, int? page)
        {
            /*Load sản phẩm dựa theo 1 tieu chi la ten sp*/
            if (TenSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lstSP = db.SANPHAMs.Where(n => n.TenSP.Contains(TenSP));
            if (lstSP.Count() == 0)
            {
                ViewBag.Message = "Không tìm thấy sản phẩm";
            }
            //Thực hiện chức năng phân trang
            //Tạo biến số sản phẩm trên trang
            int PageSize = 6;
            //Tạo biến thứ 2: Số trang hiện tại
            int PageNumber = (page ?? 1);
            if(TenSP == "")
            {
                ViewBag.TenSP = " ";
            }
            else
            {
                ViewBag.TenSP = TenSP;
            }
            return View(lstSP.OrderBy(n => n.MaSP).ToPagedList(PageNumber, PageSize));
        }
    }
}