USE [master]
GO
IF EXISTS (SELECT * FROM SYS.DATABASES WHERE NAME = 'fashionDB')
DROP DATABASE [fashionDB]
GO
/****** Object:  Database [fashionDB]    Script Date: 5/5/2023 12:48:46 AM ******/
CREATE DATABASE [fashionDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'fashionDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\fashionDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'fashionDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\fashionDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [fashionDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [fashionDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [fashionDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [fashionDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [fashionDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [fashionDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [fashionDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [fashionDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [fashionDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [fashionDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [fashionDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [fashionDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [fashionDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [fashionDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [fashionDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [fashionDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [fashionDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [fashionDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [fashionDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [fashionDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [fashionDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [fashionDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [fashionDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [fashionDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [fashionDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [fashionDB] SET  MULTI_USER 
GO
ALTER DATABASE [fashionDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [fashionDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [fashionDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [fashionDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [fashionDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [fashionDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [fashionDB] SET QUERY_STORE = OFF
GO
USE [fashionDB]
GO
/****** Object:  Table [dbo].[ADMIN]    Script Date: 5/9/2023 11:53:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADMIN](
	[MaAD] [char](10) NOT NULL,
	[HoTen] [nvarchar](100) NOT NULL,
	[SDT] [char](11) NULL,
	[Email] [char](100) NULL,
	[NgaySinh] [date] NOT NULL,
	[GioiTinh] [char](1) NOT NULL,
	[DiaChi] [nvarchar](200) NOT NULL,
	[Username] [char](50) NULL,
	[UserPass] [char](30) NULL,
	[TinhTrang] [int] NULL,
	[ChucVu] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ADMIN] PRIMARY KEY CLUSTERED 
(
	[MaAD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTDONHANG]    Script Date: 5/9/2023 11:53:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTDONHANG](
	[MACTDH] [char](10) NOT NULL,
	[SoLuongDat] [int] NOT NULL,
	[DonGia] [numeric](18, 0) NOT NULL,
	[TenSP] [nvarchar](100) NOT NULL,
	[KichCo] [char](10) NOT NULL,
	[MaDH] [char](10) NOT NULL,
	[MaSP] [char](10) NULL,
 CONSTRAINT [PK_CTDONHANG] PRIMARY KEY CLUSTERED 
(
	[MACTDH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTHOADON]    Script Date: 5/9/2023 11:53:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTHOADON](
	[MACTHD] [char](10) NOT NULL,
	[TenSP] [nvarchar](100) NOT NULL,
	[DonGia] [numeric](18, 0) NOT NULL,
	[SoLuong] [int] NOT NULL,
	[KichCo] [char](10) NOT NULL,
	[ThanhTien] [decimal](18, 2) NOT NULL,
	[MaHD] [char](10) NOT NULL,
	[MaSP] [char](10) NOT NULL,
 CONSTRAINT [PK_CTHOADON] PRIMARY KEY CLUSTERED 
(
	[MACTHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DONHANG]    Script Date: 5/9/2023 11:53:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DONHANG](
	[MaDH] [char](10) NOT NULL,
	[NgayDatHang] [date] NOT NULL,
	[NgayGiaoHang] [date] NOT NULL,
	[PTThanhToan] [nvarchar](50) NULL,
	[TrangThaiDH] [int] NOT NULL,
	[TongTien] [decimal](18, 2) NOT NULL,
	[MaKH] [char](10) NULL,
 CONSTRAINT [PK_DONHANG] PRIMARY KEY CLUSTERED 
(
	[MaDH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HOADON]    Script Date: 5/9/2023 11:53:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOADON](
	[MaHD] [char](10) NOT NULL,
	[NgayLap] [date] NOT NULL,
	[TongTien] [decimal](18, 2) NOT NULL,
	[MaKH] [char](10) NOT NULL,
	[MaDH] [char](10) NOT NULL,
	[MaAD] [char](10) NOT NULL,
 CONSTRAINT [PK_HOADON] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KHACHHANG]    Script Date: 5/9/2023 11:53:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHACHHANG](
	[MaKH] [char](10) NOT NULL,
	[HoTen] [nvarchar](100) NOT NULL,
	[SDT] [char](11) NULL,
	[Email] [char](100) NULL,
	[NgaySinh] [date] NOT NULL,
	[GioiTinh] [char](1) NOT NULL,
	[DiaChi] [nvarchar](200) NOT NULL,
	[Username] [char](50) NULL,
	[UserPass] [char](30) NULL,
	[TinhTrang] [int] NULL,
 CONSTRAINT [PK_KHACHHANG] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KICHCOSP]    Script Date: 5/9/2023 11:53:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KICHCOSP](
	[MaKichCo] [char](10) NOT NULL,
	[KichCo] [char](10) NOT NULL,
	[SoLuong] [int] NOT NULL,
	[MaSP] [char](10) NOT NULL,
 CONSTRAINT [PK_KICHCOSP] PRIMARY KEY CLUSTERED 
(
	[MaKichCo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LOAISANPHAM]    Script Date: 5/9/2023 11:53:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOAISANPHAM](
	[MaLoaiSP] [char](10) NOT NULL,
	[TenLoaiSP] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_LOAISANPHAM] PRIMARY KEY CLUSTERED 
(
	[MaLoaiSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SANPHAM]    Script Date: 5/9/2023 11:53:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SANPHAM](
	[MaSP] [char](10) NOT NULL,
	[TenSP] [nvarchar](100) NOT NULL,
	[HinhSP] [char](30) NULL,
	[MoTa] [nvarchar](255) NULL,
	[GiaSP] [numeric](18, 0) NOT NULL,
	[TinhTrangSP] [int] NOT NULL,
	[MaLoaiSP] [char](10) NOT NULL,
 CONSTRAINT [PK_SANPHAM] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CTDONHANG]  WITH CHECK ADD  CONSTRAINT [FK_CTDONHANG_DONHANG] FOREIGN KEY([MaDH])
REFERENCES [dbo].[DONHANG] ([MaDH])
GO
ALTER TABLE [dbo].[CTDONHANG] CHECK CONSTRAINT [FK_CTDONHANG_DONHANG]
GO
ALTER TABLE [dbo].[CTDONHANG]  WITH CHECK ADD  CONSTRAINT [FK_CTDONHANG_SANPHAM] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SANPHAM] ([MaSP])
GO
ALTER TABLE [dbo].[CTDONHANG] CHECK CONSTRAINT [FK_CTDONHANG_SANPHAM]
GO
ALTER TABLE [dbo].[CTHOADON]  WITH CHECK ADD  CONSTRAINT [FK_CTHOADON_HOADON] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HOADON] ([MaHD])
GO
ALTER TABLE [dbo].[CTHOADON] CHECK CONSTRAINT [FK_CTHOADON_HOADON]
GO
ALTER TABLE [dbo].[CTHOADON]  WITH CHECK ADD  CONSTRAINT [FK_CTHOADON_SANPHAM] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SANPHAM] ([MaSP])
GO
ALTER TABLE [dbo].[CTHOADON] CHECK CONSTRAINT [FK_CTHOADON_SANPHAM]
GO
--ALTER TABLE [dbo].[DONHANG]  WITH CHECK ADD  CONSTRAINT [FK_DONHANG_KHACHHANG] FOREIGN KEY([MaKH])
--REFERENCES [dbo].[KHACHHANG] ([MaKH])
--GO
--ALTER TABLE [dbo].[DONHANG] CHECK CONSTRAINT [FK_DONHANG_KHACHHANG]
--GO
ALTER TABLE [dbo].[HOADON]  WITH CHECK ADD  CONSTRAINT [FK_HOADON_ADMIN] FOREIGN KEY([MaAD])
REFERENCES [dbo].[ADMIN] ([MaAD])
GO
ALTER TABLE [dbo].[HOADON] CHECK CONSTRAINT [FK_HOADON_ADMIN]
GO
ALTER TABLE [dbo].[HOADON]  WITH CHECK ADD  CONSTRAINT [FK_HOADON_DONHANG] FOREIGN KEY([MaDH])
REFERENCES [dbo].[DONHANG] ([MaDH])
GO
ALTER TABLE [dbo].[HOADON] CHECK CONSTRAINT [FK_HOADON_DONHANG]
GO
ALTER TABLE [dbo].[HOADON]  WITH CHECK ADD  CONSTRAINT [FK_HOADON_KHACHHANG] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KHACHHANG] ([MaKH])
GO
ALTER TABLE [dbo].[HOADON] CHECK CONSTRAINT [FK_HOADON_KHACHHANG]
GO
ALTER TABLE [dbo].[KICHCOSP]  WITH CHECK ADD  CONSTRAINT [FK_KICHCOSP_SANPHAM] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SANPHAM] ([MaSP])
GO
ALTER TABLE [dbo].[KICHCOSP] CHECK CONSTRAINT [FK_KICHCOSP_SANPHAM]
GO
ALTER TABLE [dbo].[SANPHAM]  WITH CHECK ADD  CONSTRAINT [FK_SANPHAM_LOAISANPHAM] FOREIGN KEY([MaLoaiSP])
REFERENCES [dbo].[LOAISANPHAM] ([MaLoaiSP])
GO
ALTER TABLE [dbo].[SANPHAM] CHECK CONSTRAINT [FK_SANPHAM_LOAISANPHAM]
GO
USE [master]
GO
ALTER DATABASE [fashionDB] SET  READ_WRITE 
GO

USE [fashionDB]
GO

ALTER TABLE [dbo].[ADMIN]
ADD avatar varchar(255);

ALTER TABLE [dbo].[KHACHHANG]
ADD avatar varchar(255);

insert into KHACHHANG values ('KH001', N'Nguyễn Hoàng Kha', '0977216038', 'hkha928@gmail.com', '12/02/2002', 'M', N'221/ 81 Đông Thạnh 4', 'hkha928', '123', 1, 'avartar-01.jpg')


insert into ADMIN values ('AD001', N'Trần Lê Duy', '0904689418', 'trn.duyle@gmail.com', '02/26/2002', 'M', N'368/25A Tôn Đản', 'admin', '123', 1, 'quan_tri_vien', 'avartar-02.jpg')


insert into LOAISANPHAM values('LSP001', N'Áo thun')
insert into LOAISANPHAM values('LSP002', N'Áo polo')


insert into SANPHAM values('SP001', N'Áo trắng trơn', 'img6.jpg', N'Đây là mô tả', 100000, 1, 'LSP001')
insert into SANPHAM values('SP002', N'Áo đỏ trơn', 'img7.jpg', N'Đây là mô tả', 150000, 1, 'LSP001')


insert into KICHCOSP values('1', 'S', 100, 'SP001')
insert into KICHCOSP values('2', 'M', 100, 'SP001')
insert into KICHCOSP values('3', 'L', 100, 'SP001')
insert into KICHCOSP values('4', 'S', 100, 'SP002')
insert into KICHCOSP values('5', 'M', 100, 'SP002')
insert into KICHCOSP values('6', 'L', 100, 'SP002')

insert into DONHANG values('DH001', '05/05/2023', '05/06/2023', N'Chuyển khoản', 0, 200000, 'KH001')

insert into CTDONHANG values('CTDH001', 1, 100000, N'Áo trắng trơn', 'S', 'DH001', 'SP001')
insert into CTDONHANG values('CTDH002', 1, 100000, N'Áo trắng trơn', 'M', 'DH001', 'SP001')

insert into HOADON values('HD001', '05/05/2023', 200000, 'KH001', 'DH001', 'AD001')

insert into CTHOADON values('CTHD001', N'Áo trắng trơn', 100000, 1, 'S', 100000, 'HD001', 'SP001')
insert into CTHOADON values('CTHD002', N'Áo trắng trơn', 100000, 1, 'M', 100000, 'HD001', 'SP001')

select * from KHACHHANG
select * from ADMIN 
select * from LOAISANPHAM
select * from SANPHAM
select * from KICHCOSP
select * from DONHANG
select * from CTDONHANG
select * from HOADON
select * from CTHOADON
