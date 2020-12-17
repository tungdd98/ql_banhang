USE master
GO
IF  EXISTS (
	SELECT name 
		FROM sys.databases 
		WHERE name = 'QLBanHang'
)
DROP DATABASE QLBanHang
GO

CREATE DATABASE QLBanHang
GO

USE QLBanHang
GO

-- LoaiSanPham
Create table LoaiSanPham (
	MaLSP int IDENTITY(1,1) PRIMARY KEY,
	TenLSP nvarchar(100) 
)

Insert into LoaiSanPham values 
	('Nike'),
	('Adidas'),
	('Bitis')

-- NhaCungCap
Create table NhaCungCap (
	MaNCC int IDENTITY(1,1) PRIMARY KEY,
	TenNCC nvarchar(100),
	DiaChi nvarchar(100) null,
	SoDienThoai char(20) null
)

Insert into NhaCungCap values
	(N'Nhà cung cấp 01', N'Hà Nội', '0973793711'),
	(N'Nhà cung cấp 02', N'Hải Phòng', '0973793712'),
	(N'Nhà cung cấp 03', N'Đà Nẵng', '0973793713')

-- SanPham
Create table SanPham (
	MaSP int IDENTITY(1,1) PRIMARY KEY,
	TenSP nvarchar(100),
	DonGia int default 0,
	KhuyenMai float null,
	SoLuong int default 0,
	MaLSP int foreign key references LoaiSanPham(MaLSP) on delete cascade on update cascade
)

Insert into SanPham values
	('Adidas 01', 100000, 0, 10, 1),
	('Adidas 02', 200000, 0, 20, 2),
	('Adidas 03', 300000, 0, 30, 3)

-- PhieuNhap
Create table PhieuNhap (
	MaPN int IDENTITY(1,1) PRIMARY KEY,
	NgayLap date DEFAULT GETDATE(),
	MaNCC int foreign key references NhaCungCap(MaNCC) on delete cascade on update cascade
)

Insert into PhieuNhap values 
	('2020/3/10', 1),
	('2020/3/15', 2),
	('2020/3/20', 3)

-- ChiTietPhieuNhap
Create table ChiTietPhieuNhap (
	MaPN int foreign key references PhieuNhap(MaPN) on delete cascade on update cascade,
	MaSP int foreign key references SanPham(MaSP) on delete cascade on update cascade,
	DonGiaNhap int default 0,
	SoLuongNhap int default 0,
	PRIMARY KEY (MaPN, MaSP)
)

Insert into ChiTietPhieuNhap values 
	(1, 1, 90000, 10),
	(1, 3, 90000, 20),
	(1, 2, 90000, 30)

-- KhachHang
Create table KhachHang (
	MaKH int IDENTITY(1,1) PRIMARY KEY,
	TenKH nvarchar(100),
	SoDienThoai char(20) unique,
	DiemTichLuy int default 0
)

Insert into KhachHang values 
	(N'Khách hàng 1', '0973793711', 0),
	(N'Khách hàng 2', '0973793712', 0),
	(N'Khách hàng 3', '0973793713', 1)

-- NhanVien
Create table NhanVien (
	MaNV int IDENTITY(1,1) PRIMARY KEY,
	Username varchar(50) unique,
	Password varchar(50),
	TenNV nvarchar(100),
	SoDienThoai char(20) null,
	NgaySinh date null,
	QueQuan nvarchar(255) null,
	ChucVu tinyint default 0
	-- 0: nhân viên
	-- 1: quản lý
)

Insert into NhanVien values 
	('admin', '123456789', 'Nhan vien A', '0973793711', '1998/1/9', N'Hà Nội', 1),
	('admin1', '123456789', 'Nhan vien A', '0973793711', '1998/1/9', N'Hà Nội', 1),
	('admin2', '123456789', 'Nhan vien A', '0973793711', '1998/1/9', N'Hà Nội', 1)

-- HoaDon
Create table HoaDon (
	MaHD int IDENTITY(1,1) PRIMARY KEY,
	NgayLap date default GetDate(),
	GhiChu nvarchar(100) null,
	TongTien int null,
	MaKH int FOREIGN KEY REFERENCES KhachHang(MaKH) on delete cascade on update cascade,
	MaNV int FOREIGN KEY REFERENCES NhanVien(MaNV) on delete cascade on update cascade
)

Insert into HoaDon values 
	('2020/12/14', N'Live', 100000, 1, 1),
	('2020/12/15', N'Live', 100000, 2, 1),
	('2020/12/16', N'Live', 100000, 3, 1)

-- ChiTietHoaDon
Create table ChiTietHoaDon (
	MaHD int FOREIGN KEY REFERENCES HoaDon(MaHD) on delete cascade on update cascade,
	MaSP int FOREIGN KEY REFERENCES SanPham(MaSP) on delete cascade on update cascade,
	SoLuongMua int default 1,
	PRIMARY KEY (MaHD, MaSP)
)

Insert into ChiTietHoaDon values 
	(1, 1, 5),
	(1, 2, 5),
	(1, 3, 6)

Select * from HoaDon