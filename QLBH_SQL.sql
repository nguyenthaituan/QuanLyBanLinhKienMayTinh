CREATE DATABASE QLBH_SQL
GO

USE QLBH_SQL
GO

CREATE TABLE NHASANXUAT
(
	MaNSX INT IDENTITY(1,1) PRIMARY KEY ,
	TenNSX NVARCHAR(100) NOT NULL,
	ThongTin NVARCHAR(100) NOT NULL,
	Logo VARCHAR(MAX) NOT NULL
)
GO

CREATE TABLE NHACUNGCAP
(
	MaNCC INT IDENTITY(1,1) PRIMARY KEY,
	TenNCC NVARCHAR(100) NOT NULL,
	DiaChi NVARCHAR(100) NOT NULL,
	Email VARCHAR(100) NOT NULL,
	SDTNCC VARCHAR(10) NOT NULL
)
GO


CREATE TABLE LOAISANPHAM
(
	MaLoai INT IDENTITY(1,1) PRIMARY KEY,
	TenLoai NVARCHAR(100) NOT NULL,
)

CREATE TABLE SANPHAM
(
	MaSP INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	TenSP NVARCHAR(100) NOT NULL,
	DonGia INT NOT NULL,
	NgayCapNhat DATE NOT NULL,
	CauHinh NVARCHAR(MAX) NOT NULL,
	HinhAnh VARCHAR(500) NOT NULL,
	SoLuongTon INT NOT NULL,
	SoLanMua INT DEFAULT 0,
	LuotXem INT DEFAULT 0,
	LuotBinhChon INT DEFAULT 0,
	Moi BIT DEFAULT 1,
	DaXoa BIT DEFAULT 0,
	MaNCC INT FOREIGN KEY REFERENCES dbo.NHACUNGCAP(MaNCC) ON DELETE CASCADE ON UPDATE CASCADE,
	MaNSX INT FOREIGN KEY REFERENCES dbo.NHASANXUAT(MaNSX) ON DELETE CASCADE ON UPDATE CASCADE,
	MaLoai INT FOREIGN KEY REFERENCES dbo.LOAISANPHAM(MaLoai) ON DELETE CASCADE ON UPDATE CASCADE
)
GO

CREATE TABLE LOAITHANHVIEN
(
	MaLoaiTV INT IDENTITY(1,1) PRIMARY KEY,
	TenLoai NVARCHAR(100) NOT NULL,
	UuDai DECIMAL DEFAULT 0,
)

CREATE TABLE THANHVIEN
(
	TaiKhoanTV VARCHAR(100) PRIMARY KEY NOT NULL,
	Matkhau VARCHAR(100) NOT NULL,
	HoTen NVARCHAR(500) NOT NULL,
	DiaChi NVARCHAR(500) NOT NULL,
	Email VARCHAR(200) NOT NULL,
	DienThoai VARCHAR(15) NOT NULL,
	MaLoaiTV INT FOREIGN KEY REFERENCES dbo.LOAITHANHVIEN(MaLoaiTV) ON UPDATE CASCADE ON DELETE CASCADE,
)

CREATE TABLE CHUCVU
(
	MaCV INT IDENTITY(1,1) PRIMARY KEY,
	TenCV NVARCHAR(300) NOT NULL,
)

CREATE TABLE NHANVIEN
(
	TaiKhoanNV VARCHAR(200) PRIMARY KEY,
	MatKhau VARCHAR(200) NOT NULL,
	HoTen NVARCHAR(500) NOT NULL,
	DiaChi NVARCHAR(500) NOT NULL,
	Email VARCHAR(200) NOT NULL,
	DienThoai VARCHAR(15) NOT NULL,
	MaCV INT FOREIGN KEY REFERENCES dbo.CHUCVU(MaCV) ON DELETE CASCADE ON UPDATE CASCADE
)


CREATE TABLE PHIEUNHAP
(
	MaPN INT IDENTITY(1,1) PRIMARY KEY,
	TaiKhoanNV VARCHAR(200) FOREIGN KEY REFERENCES dbo.NHANVIEN(TaiKhoanNV) ON UPDATE CASCADE ON DELETE CASCADE,
	MaNCC INT FOREIGN KEY REFERENCES dbo.NHACUNGCAP(MaNCC) ON UPDATE CASCADE ON DELETE CASCADE, 
	NgayNhap DATE NOT NULL,
	DaXoa BIT DEFAULT 0
)
GO

CREATE TABLE CHITIETPHIEUNHAP
(
	MaPN INT FOREIGN KEY REFERENCES dbo.PHIEUNHAP(MaPN),
	MaSP INT FOREIGN KEY REFERENCES dbo.SANPHAM(MaSP),
	DonGiaNhap INT NOT NULL,
	SoLuongNhap INT NOT NULL,
	CONSTRAINT ChiTietPhieuNhap_PK PRIMARY KEY(MaPN,MaSP)
)
GO


CREATE TABLE DONDATHANG
(
	MaDDH INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	NgayDat DATE NOT NULL,
	TinhTrangGiaoHang NVARCHAR(500),
	NgayGiao DATE NULL,
	DaGiao BIT DEFAULT 0,
	DaThanhToan BIT DEFAULT 0,
	TaiKhoanTV VARCHAR(100) FOREIGN KEY REFERENCES dbo.THANHVIEN(TaiKhoanTV) ON UPDATE CASCADE ON DELETE CASCADE
)
GO

CREATE TABLE CHITIETDONDATHANG
(
	MaDDH INT FOREIGN KEY REFERENCES dbo.DONDATHANG(MaDDH) ON UPDATE CASCADE ON DELETE CASCADE,
	MaSP INT FOREIGN KEY REFERENCES dbo.SANPHAM(MaSP) ON UPDATE CASCADE ON DELETE CASCADE,
	SoLuong INT NOT NULL,
	CONSTRAINT ChiTietDonDatHang_PK PRIMARY KEY(MaDDH,MaSP)
)
GO



-------------------------------------------Không cần thiết--------------------------
CREATE TABLE QuangCao
(
	MaQC VARCHAR(10) PRIMARY KEY NOT NULL,
	TenQC NVARCHAR(100) NOT NULL,
	ThoiGianBatDau DATETIME NOT NULL,
	ThoiGianKetThuc DATETIME NOT NULL,
	DuongDan VARCHAR(100) NOT NULL,
	HinhAnh IMAGE NOT NULL,
	ViTri VARCHAR(100) NOT NULL
)
GO

CREATE TABLE HoiDap
(
	MaHoiDap VARCHAR(10) PRIMARY KEY NOT NULL,
	MaTaiKhoan VARCHAR(10) NOT NULL,
	TieuDe NVARCHAR(100) NOT NULL,
	NoiDung NVARCHAR(MAX) NOT NULL,
	TrangThai NVARCHAR(100) NOT NULL,
)
GO

CREATE TABLE TinTuc
(
	MaTinTuc VARCHAR(10) PRIMARY KEY NOT NULL,
	TieuDe NVARCHAR(100) NOT NULL,
	NoiDung NVARCHAR (MAX) NOT NULL,
	NgayDang DATE NOT NULL
)
-----------------------------------------------------------------------------------------------------------------
------------------------------Nhập thông tin nhà cung cấp-------------------------------
INSERT INTO	dbo.NhaCungCap
        ( TenNCC, DiaChi, Email, SDTNCC )
VALUES  ( N'Công ty Vĩnh Thái',N'24/10 Võ Trứ, Nha Trang','CTVT@gmail.com','0369337366'),
		( N'Công ty Vĩnh Hòa',N'22/10 Võ Thị Sáu, Nha Trang','CTVH@gmail.com','0586134487'),
		( N'Công ty Vĩnh Lộc',N'23/10 Trần Phú, Nha Trang','CTVL@gmail.com','0987806758'),
		( N'Công ty Vĩnh Nguyên',N'25/10 Nguyễn Thiện Thuật, Nha Trang','CTVN@gmail.com','0979540819'),
		( N'Công ty Vĩnh Trường',N'1 Nguyễn Đình chiểu, Nha Trang','CTVTR@gmail.com','0695356332'),
		( N'Công ty Vĩnh Phước',N'24 Mai Xuân Thưởng, Nha Trang','CTVP@gmail.com','0367891230'),
		( N'Công ty Diên Khánh',N'231 Nguyễn Thị Minh Khai, Nha Trang','CTDK@gmail.com','0987801234'),
		( N'Công ty Diên Lộc',N'19 Phạm Văn Đồng, Nha Trang','CTDL@gmail.com','0979541003'),
		( N'Công ty Ninh Thượng',N'50 Phạm Văn Đồng, Nha Trang','CTNP@gmail.com','0375256337'),
		( N'Công ty Ninh Hòa',N'24/10 Trần Quý Cáp, Nha Trang','CTNH@gmail.com','0390045578')
GO

---------------------------Nhập thông tin nhà sản xuất--------------------------------------
INSERT INTO dbo.NhaSanXuat
        ( TenNSX, ThongTin,Logo)
VALUES  (N'Intel',N'Chuyên sản xuất chip','logo.jpg'),
		( N'Snapdragon',N'Chuyên sản xuất chip','logo.jpg'),
		( N'AMD',N'Chuyên sản xuất chip','logo.jpg'),
		( N'Apple',N'Chuyên sản xuất chip','logo.jpg'),
		( N'SamSung',N'Chuyên sản xuất chip','logo.jpg'),
		( N'Asus',N'Chuyên sản xuất chip','logo.jpg'),
		( N'HP',N'Chuyên sản xuất chip','logo.jpg'),
		( N'DELL',N'Chuyên sản xuất chip','logo.jpg'),
		( N'Zowei',N'Chuyên sản xuất chip','logo.jpg'),
		( N'Xiaomi',N'Chuyên sản xuất chip','logo.jpg'),
		( N'Sony',N'Chuyên sản xuất chip','logo.jpg'),
		( N'Predator',N'Chuyên sản xuất chip','logo.jpg'),
		( N'Huawei',N'Chuyên sản xuất chip','logo.jpg'),
		( N'Kingston',N'Chuyên sản xuất chip','logo.jpg'),
		( N'WD',N'Chuyên sản xuất chip','logo.jpg')

--------------------------------------Nhập thông tin loại sản phẩm-----------------------------------------------
INSERT INTO	dbo.LoaiSanPham
        (TenLoai)
VALUES  (N'SSD'),
		(N'CPU'),
		(N'HDD'),
		(N'RAM'),
		(N'BÀN PHÍM')

------------------------------------Nhập thông tin sản phẩm----------------------------------------------------
INSERT INTO dbo.SANPHAM
        (TenSP ,DonGia , NgayCapNhat ,CauHinh ,HinhAnh ,SoLuongTon ,MaNCC ,MaNSX , MaLoai)
VALUES  
------------------------------------SSD------------------------------------------------------------------------
(N'Ổ cứng SSD Western Digital Green 240GB 2.5" SATA 3' ,930000 ,GETDATE() ,
N'- Dung lượng: 240GB
- Kích thước: 2.5"
- Kết nối: SATA 3
- Tốc độ đọc / ghi (tối đa): 545MB/s' ,'sp.jpg' ,100,1,1,1),
(N'Ổ cứng SSD Western Digital Blue 500GB 2.5" SATA 3' ,1790000 ,GETDATE() ,
N'- Dung lượng: 500GB
- Kích thước: 2.5"
- Kết nối: SATA 3
- Tốc độ đọc / ghi (tối đa): 560MB/s / 530MB/s' ,'sp.jpg' ,100,2,2,1),
(N'Ổ cứng SSD Samsung 860 Evo 250GB 2.5" SATA 3' ,1450000 ,GETDATE() ,
N'- Dung lượng: 250GB
- Kích thước: 2.5"
- Kết nối: SATA 3
- Tốc độ đọc / ghi (tối đa): 550MB/s / 520MB/s' ,'sp.jpg' ,100,3,3,1),
(N'Ổ cứng SSD Western Digital Blue 250GB 2.5" SATA 3' ,1190000 ,GETDATE() ,
N'- Dung lượng: 250GB
- Kích thước: 2.5"
- Kết nối: SATA 3
- Tốc độ đọc / ghi (tối đa): 540MB/s / 525MB/s' ,'sp.jpg' ,100,4,4,1),
(N'Ổ cứng SSD Kingston A400 120GB M.2 2280 SATA 3' ,790000 ,GETDATE() ,
N'- Dung lượng: 120GB
- Kích thước: M.2 2280
- Kết nối: M.2 SATA
- Tốc độ đọc / ghi (tối đa): 500MB/s / 320MB/s' ,'sp.jpg' ,100,5,5,1),
-----------------------------------------------CPU-------------------------------------------------------------
(N'CPU INTEL Core i3-9100 (4C/4T, 3.60 GHz - 4.20 GHz, 6MB) - 1151-v2', 3190000, GETDATE(),
N'-Socket: 1151-v2, Intel Core thế hệ thứ 9
-Tốc độ: 3.60 GHz - 4.20 GHz (4nhân, 4 luồng)
-Bộ nhớ đệm: 6MB
-Chip đồ họa tích hợp: Intel UHD Graphics 630' ,'sp.jpg' ,100,6,6,2),
(N'CPU Intel Core i5-9400 (6C/6T, 2.90 GHz - 4.10 GHz, 9MB) - LGA 1151-v2', 4490000, GETDATE(),
N'- Socket: LGA 1151-v2 , Intel Core thế hệ thứ 9
- Tốc độ xử lý: 2.90 GHz - 4.10 GHz ( 6 nhân, 6 luồng)
- Bộ nhớ đệm: 9MB
- Đồ họa tích hợp: Intel UHD Graphics 630' ,'sp.jpg' ,100,7,7,2),
(N'CPU Intel Pentium G5400 (2C/4T, 3.7 GHz, 4MB) - LGA 1151-v2', 1490000, GETDATE(),
N'- Socket: LGA 1151-v2 , Intel Pentium Gold
- Tốc độ xử lý: 3.7 GHz ( 2 nhân, 4 luồng)
- Bộ nhớ đệm: 4MB
- Đồ họa tích hợp: Intel UHD Graphics 610' ,'sp.jpg' ,100,8,8,2),
(N'CPU AMD Ryzen 3 3200G (4C/4T, 3.6 GHz - 4.0 GHz, 4MB) - AM4', 3490000, GETDATE(),
N'- Socket: AM4 , AMD Ryzen thế hệ thứ 3
- Tốc độ xử lý: 3.6 GHz up to 4GHz ( 4 nhân, 4 luồng)
- Bộ nhớ đệm: 4MB
- Đồ họa tích hợp: AMD Vega 8 Graphics' ,'sp.jpg' ,100,9,9,2),
(N'CPU Intel Core I5-8600K (3.6GHz)', 5990000, GETDATE(),
N'- Socket: LGA 1151-v2 , Intel Core thế hệ thứ 8
- Tốc độ xử lý: 3.6 GHz ( 6 nhân, 6 luồng)
- Bộ nhớ đệm: 9MB
- Đồ họa tích hợp: Intel UHD Graphics 630' ,'sp.jpg' ,100,10,10,2),
--------------------------------------------------HDD----------------------------------------------------------
(N'Ổ cứng HDD Seagate 2TB 3.5" SATA 3', 1650000, GETDATE(),
N'- Dung lượng: 2TB
- Kích thước: 3.5"
- Kết nối: SATA 3
- Tốc độ vòng quay: 7200RPM
- Cache: 256MB' ,'sp.jpg' ,100,10,11,3),
(N'Ổ cứng HDD Seagate 1TB 2.5" SATA 3', 1230000, GETDATE(),
N'- Dung lượng: 1TB
- Kích thước: 2.5"
- Kết nối: SATA 3
- Tốc độ vòng quay: 5400RPM' ,'sp.jpg' ,100,1,12,3),
(N'Ổ cứng HDD Western Digital Blue 2TB 3.5" SATA 3', 1490000, GETDATE(),
N'- Dung lượng: 2TB
- Kích thước: 3.5"
- Kết nối: SATA 3
- Tốc độ vòng quay: 5400RPM
- Cache: 256MB' ,'sp.jpg' ,100,2,13,3),
(N'Ổ cứng di động HDD Western Digital Elements Portable 500GB 2.5" USB 3.0 ', 1250000, GETDATE(),
N'- Dung lượng: 500GB
- Kích thước: 2.5"
- Kết nối: USB 3.0' ,'sp.jpg' ,100,3,14,3),
(N'Ổ cứng HDD Seagate 2TB 3.5" SATA 3 ', 1650000, GETDATE(),
N'- Dung lượng: 2TB
- Kích thước: 3.5"
- Kết nối: SATA 3
- Tốc độ vòng quay: 7200RPM
- Cache: 256MB' ,'sp.jpg' ,100,4,10,3),
--------------------------------------------------RAM----------------------------------------------------------
(N'RAM desktop G.SKILL Aegis F4-2666C19S-8GIS (1x8GB) DDR4 2666MHz', 850000, GETDATE(),
N'- Dung lượng: 1 x 8GB
- Thế hệ: DDR4
- Bus: 2666MHz
- Cas: 19' ,'sp.jpg' ,100,5,1,4),
(N'RAM desktop KINGSTON Fury Black (1 x 8GB) DDR4 2666MHz', 890000, GETDATE(),
N'- Dung lượng: 1 x 8GB
- Thế hệ: DDR4
- Bus: 2666MHz
- Cas: 16' ,'sp.jpg' ,100,6,2,4),
(N'RAM desktop CRUCIAL Ballistix Sport LT (1 x 8GB) DDR4 2666MHz ', 890000, GETDATE(),
N'- Dung lượng: 1 x 8GB
- Thế hệ: DDR4
- Bus: 2666MHz
- Cas: 16' ,'sp.jpg' ,100,7,3,4),
(N'RAM laptop KINGSTON (1 x 8GB) DDR4 2400MHz', 1050000, GETDATE(),
N'- Dung lượng: 1 x 8GB
- Thế hệ: DDR4
- Bus: 2400MHz
- Cas: 17' ,'sp.jpg' ,100,8,4,4),
(N'RAM desktop CORSAIR Vengeance LPX CMK16GX4M2A2666C16 (2x8GB) DDR4 2666MHz', 2050000, GETDATE(),
N'- Dung lượng: 2 x 8GB
- Thế hệ: DDR4
- Bus: 2666MHz
- Cas: 16' ,'sp.jpg' ,100,9,5,4),
------------------------------------------------LAPTOP---------------------------------------------------------

----------------------------------------------------USB--------------------------------------------------------
-----------------------------------------------Màn hình--------------------------------------------------------
-----------------------------------------------Bàn phím-------------------------------------------------------
(N'Bà̀n phím giả cơ Dareu LK145 Gaming', 289000, GETDATE(),
N'- Bà̀n phím giả cơ
- Kết nối USB 2.0
- Kiểu switch Membrane' ,'sp.jpg' ,100,4,7,5),
(N'Bàn phím Newmen E340', 160000, GETDATE(),
N'- Bàn phím thường
- Kết nối USB 2.0' ,'sp.jpg' ,100,1,1,5),
(N'Bàn phím cơ Dareu EK87', 479000, GETDATE(),
N'- Bàn phím cơ
- Kết nối USB 2.0
- Kiểu switch Red D switch' ,'sp.jpg' ,100,5,8,5),
(N'Bàn phím Cơ Quang Học Newmen GM550', 1190000, GETDATE(),
N'- Kiểu: Bàn phím cơ
- Kiểu kết nối: Không dây
- Switch: Optical Switch
- Kích thước: Full size
- Đèn nền: RGB' ,'sp.jpg' ,100,6,9,5),
(N'Bàn phím cơ Gaming DAREU EK1280 RGB Red D Switch (Đen)', 879000, GETDATE(),
N'- Kiểu: Bàn phím cơ 
- Kiểu kết nối: Có dây
- Switch: Red D switch
- Đèn nền: RGB
- Kích thước: Full size' ,'sp.jpg' ,100,7,10,5)


------------------------------------Nhập thông tin loại thành viên---------------------------------------------
INSERT INTO dbo.LOAITHANHVIEN
        ( TenLoai, UuDai )
VALUES  ( N'Thành viên thường',0),
		( N'Thành viên vip',0.1)		
GO

-----------------------------------Nhập thông tin thành viên---------------------------------------------------
INSERT INTO dbo.THANHVIEN
        ( TaiKhoanTV ,Matkhau ,HoTen ,DiaChi ,Email ,DienThoai ,MaLoaiTV )
VALUES  ( 'nhatduy' ,'nduy' ,N'Đỗ Hoàng Nhật Duy' ,N'Nha Trang' ,'nhatduy@gmail.com' ,'012356789' ,1),
		( 'thaituan' ,'ttuan' ,N'Nguyễn Thái Tuấn' ,N'Phú Yên' ,'thaituan@gmail.com' ,'012356789' ,1),
		( 'hoangkhoa' ,'hkhoa' ,N'Trương Hoàng Khoa' ,N'Nha Trang' ,'hoangkhoa@gmail.com' ,'012356789' ,1),
		( 'phuocthai' ,'pthai' ,N'Tô Phước Thái' ,N'Nha Trang' ,'phuocthai@gmail.com' ,'012356789' ,2),
		( 'thedung' ,'tdung' ,N'Lê Thế Dũng' ,N'Nha Trang' ,'thedung@gmail.com' ,'012356789' ,2)
GO

----------------------------------Nhập thông tin chức vụ-------------------------------------------------------
INSERT INTO dbo.CHUCVU
        (TenCV )
VALUES  (N'Nhân viên'),
		(N'Admin')
GO
----------------------------------Nhập thông tin nhân viên-----------------------------------------------------
INSERT INTO dbo.NHANVIEN
        ( TaiKhoanNV ,MatKhau ,HoTen ,DiaChi ,Email ,DienThoai , MaCV)
VALUES  ( 'bichvy' ,'bvy' ,N'Văn Phan Bích Vy' ,N'Nha Trang' ,'bichvy@gmail.com' ,'1234567890' ,1),
		( 'tankiet' ,'tkiet' ,N'Nguyễn Tấn Kiệt' ,N'Ninh Hòa' ,'tankiet@gmail.com' ,'1234567890' ,1),
		( 'hoanglan' ,'hlan' ,N'Nguyễn Đình Hoàng Lân' ,N'Nha Trang' ,'hoanglan@gmail.com' ,'1234567890' ,1),
		( 'thanhluan' ,'tluan' ,N'Võ Thành Luân' ,N'Nha Trang' ,'thanhluan@gmail.com' ,'1234567890' ,2),
		( 'quangnghia' ,'qnghia' ,N'Quang Nghĩa' ,N'Ninh Hòa' ,'quangnghia@gmail.com' ,'1234567890' ,1)
GO
---------------------------------Nhập thông tin phiếu nhập-----------------------------------------------------
INSERT INTO dbo.PHIEUNHAP
        ( TaiKhoanNV , MaNCC ,NgayNhap)
VALUES  ('bichvy' ,5 ,GETDATE()),
		('quangnghia' ,7 ,GETDATE()),
		('hoanglan' ,9,GETDATE())
GO
--------------------------------Nhập thông tin chi tiết phiếu nhập---------------------------------------------
INSERT INTO dbo.CHITIETPHIEUNHAP
        ( MaPN, MaSP, DonGiaNhap, SoLuongNhap )
VALUES  ( 1, 187, 830000, 10),
		( 2, 187, 830000, 10),
		( 3, 187, 830000, 10)
