//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyBanLinhKienMayTinh.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CHITIETPHIEUNHAP
    {
        public int MaPN { get; set; }
        public int MaSP { get; set; }
        public int DonGiaNhap { get; set; }
        public int SoLuongNhap { get; set; }
    
        public virtual PHIEUNHAP PHIEUNHAP { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
    }
}
