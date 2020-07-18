using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyBanLinhKienMayTinh.Models
{
    public class ItemGioHang
    {
        public string TenSP { get; set; }
        public int MaSP { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }

        public int ThanhTien { get; set; }
        public string HinhAnh { get; set; }

        public ItemGioHang(int iMaSP)
        {
            using(QLBH_SQLEntities db = new QLBH_SQLEntities())
            {
                this.MaSP = iMaSP;
                SANPHAM sp = db.SANPHAMs.Single(n => n.MaSP == iMaSP);
                TenSP = sp.TenSP;
                DonGia = sp.DonGia;
                HinhAnh = sp.HinhAnh;
                SoLuong = 1;
                ThanhTien = SoLuong * DonGia;
            }
        }

        public ItemGioHang(int iMaSP, int sl)
        {
            using (QLBH_SQLEntities db = new QLBH_SQLEntities())
            {
                this.MaSP = iMaSP;
                SANPHAM sp = db.SANPHAMs.Single(n => n.MaSP == iMaSP);
                this.TenSP = sp.TenSP;
                this.DonGia = sp.DonGia;
                this.HinhAnh = sp.HinhAnh;
                this.SoLuong = sl;
                this.ThanhTien = SoLuong * sp.DonGia;
            }
        }
    }
}