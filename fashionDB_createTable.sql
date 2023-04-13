use master
GO
IF EXISTS (SELECT * FROM sys.databases WHERE name='fashionDB')
BEGIN
	DROP DATABASE fashionDB
END
GO
CREATE DATABASE fashionDB
GO
USE fashionDB
GO

CREATE TABLE Users
(
  UserID CHAR(10) NOT NULL,
  Username CHAR(40) NOT NULL,
  UserPass CHAR(16) NOT NULL,
  Role CHAR(2) NOT NULL,
  TinhTrang FLOAT NOT NULL,
  PRIMARY KEY (UserID),
  UNIQUE (Username)
);

CREATE TABLE KhachHang
(
  MaKH CHAR(10) NOT NULL,
  HoTen NVARCHAR(100) NOT NULL,
  CCCD CHAR(12) NOT NULL,
  SDT CHAR(11) NOT NULL,
  Email CHAR(50) NOT NULL,
  NgaySinh DATE NOT NULL,
  GioiTinh INT NOT NULL,
  UserID CHAR(10) NOT NULL,
  PRIMARY KEY (MaKH),
  FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE LoaiSanPham
(
  MaLoaiSP CHAR(10) NOT NULL,
  TenLoaiSP NVARCHAR(50) NOT NULL,
  PRIMARY KEY (MaLoaiSP),
  UNIQUE (TenLoaiSP)
);

CREATE TABLE DonHang
(
  MaDH CHAR(10) NOT NULL,
  NgayDatHang DATE NOT NULL,
  NgayGiaoHang DATE NOT NULL,
  PTThanhToan NVARCHAR(50) NOT NULL,
  TongTien INT NOT NULL,
  MaKH CHAR(10) NOT NULL,
  PRIMARY KEY (MaDH),
  FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
);

CREATE TABLE HoaDon
(
  MaHD CHAR(10) NOT NULL,
  NgayLap INT NOT NULL,
  TongTien INT NOT NULL,
  MaKH CHAR(10) NOT NULL,
  MaDH CHAR(10) NOT NULL,
  PRIMARY KEY (MaHD),
  FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
  FOREIGN KEY (MaDH) REFERENCES DonHang(MaDH)
);

CREATE TABLE SanPham
(
  MaSP CHAR(10) NOT NULL,
  TenSP NVARCHAR(50) NOT NULL,
  MaLoaiSP CHAR(10) NOT NULL,
  PRIMARY KEY (MaSP),
  FOREIGN KEY (MaLoaiSP) REFERENCES LoaiSanPham(MaLoaiSP)
);

CREATE TABLE ChiTietSanPham
(
  MaCTSP CHAR(10) NOT NULL,
  GiaSP NUMERIC(12,0) NOT NULL,
  KichCoSP CHAR(10) NOT NULL,
  TinhTrangSP INT NOT NULL,
  SoLuongSP INT NOT NULL,
  MaSP CHAR(10) NOT NULL,
  PRIMARY KEY (MaCTSP),
  FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);

CREATE TABLE ChiTietDonHang
(
  MaCTDH CHAR(10) NOT NULL,
  Hinhanh CHAR(20) NULL,
  SoLuongDat INT NOT NULL,
  DonGia NUMERIC(12,0) NOT NULL,
  KichCoSP CHAR(10) NOT NULL,
  Mota NVARCHAR(500) NULL,
  MaSP CHAR(10) NOT NULL,
  MaDH CHAR(10) NOT NULL,
  PRIMARY KEY (MaCTDH),
  FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP),
  FOREIGN KEY (MaDH) REFERENCES DonHang(MaDH)
);

CREATE TABLE DoanhThu
(
  MaDT CHAR(10) NOT NULL,
  SoLuongBan INT NOT NULL,
  TongDoanhThu INT NOT NULL,
  Ngay DATE NOT NULL,
  MaHD CHAR(10) NOT NULL,
  PRIMARY KEY (MaDT),
  FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD)
);