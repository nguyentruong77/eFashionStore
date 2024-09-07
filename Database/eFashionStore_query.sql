USE [master]
GO
/****** Object:  Database [eFashionStore]    Script Date: 7/9/2024 22:49:34 ******/
CREATE DATABASE [eFashionStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'eFashionStore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.STARTINHS\MSSQL\DATA\eFashionStore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'eFashionStore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.STARTINHS\MSSQL\DATA\eFashionStore_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [eFashionStore] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [eFashionStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [eFashionStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [eFashionStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [eFashionStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [eFashionStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [eFashionStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [eFashionStore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [eFashionStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [eFashionStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [eFashionStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [eFashionStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [eFashionStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [eFashionStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [eFashionStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [eFashionStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [eFashionStore] SET  ENABLE_BROKER 
GO
ALTER DATABASE [eFashionStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [eFashionStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [eFashionStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [eFashionStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [eFashionStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [eFashionStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [eFashionStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [eFashionStore] SET RECOVERY FULL 
GO
ALTER DATABASE [eFashionStore] SET  MULTI_USER 
GO
ALTER DATABASE [eFashionStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [eFashionStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [eFashionStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [eFashionStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [eFashionStore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [eFashionStore] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'eFashionStore', N'ON'
GO
ALTER DATABASE [eFashionStore] SET QUERY_STORE = ON
GO
ALTER DATABASE [eFashionStore] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [eFashionStore]
GO
/****** Object:  Table [dbo].[ChiTietGioHang]    Script Date: 7/9/2024 22:49:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietGioHang](
	[MaGH] [int] NOT NULL,
	[MaSP] [char](10) NOT NULL,
	[SoLuong] [int] NULL,
 CONSTRAINT [PK_ChiTietGioHang_1] PRIMARY KEY CLUSTERED 
(
	[MaGH] ASC,
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietHoaDon]    Script Date: 7/9/2024 22:49:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDon](
	[MaSP] [char](10) NOT NULL,
	[MaHD] [char](10) NOT NULL,
	[SoLuongDatHang] [int] NULL,
	[DonGia] [money] NULL,
 CONSTRAINT [PK_ChiTietHoaDon_1] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC,
	[MaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coupon]    Script Date: 7/9/2024 22:49:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupon](
	[MaCoupon] [nchar](10) NOT NULL,
	[GiamGia] [int] NOT NULL,
 CONSTRAINT [PK_Coupon] PRIMARY KEY CLUSTERED 
(
	[MaCoupon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DanhGia]    Script Date: 7/9/2024 22:49:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanhGia](
	[MaSP] [char](10) NOT NULL,
	[MaKH] [int] NOT NULL,
	[SoSao] [int] NULL,
	[BinhLuan] [nvarchar](50) NULL,
 CONSTRAINT [PK_DanhGia] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC,
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GioHang]    Script Date: 7/9/2024 22:49:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GioHang](
	[MaGH] [int] IDENTITY(1,1) NOT NULL,
	[MaKH] [int] NULL,
 CONSTRAINT [PK_GioHang] PRIMARY KEY CLUSTERED 
(
	[MaGH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 7/9/2024 22:49:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[MaHD] [char](10) NOT NULL,
	[MaKH] [int] NULL,
	[MaNV] [int] NULL,
	[NgayDatHang] [datetime] NULL,
	[DiaChiGiaoHang] [nvarchar](50) NULL,
	[TongGiaTri] [money] NULL,
	[TrangThaiTT] [bit] NULL,
	[TrangThaiDH] [int] NULL,
	[NgayNhanHang] [datetime] NULL,
	[MaCoupon] [nchar](10) NULL,
 CONSTRAINT [PK_HoaDon] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiSP]    Script Date: 7/9/2024 22:49:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiSP](
	[MaLoai] [char](10) NOT NULL,
	[TenLoai] [nvarchar](30) NULL,
	[Hinh] [varchar](50) NULL,
 CONSTRAINT [PK_LoaiSP] PRIMARY KEY CLUSTERED 
(
	[MaLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NguoiDung]    Script Date: 7/9/2024 22:49:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguoiDung](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](30) NULL,
	[Email] [varchar](30) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[SDT] [varchar](10) NULL,
	[TenTaiKhoan] [varchar](20) NULL,
	[MatKhau] [varchar](100) NULL,
	[IsAdmin] [bit] NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 7/9/2024 22:49:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSP] [char](10) NOT NULL,
	[TenSP] [nvarchar](50) NOT NULL,
	[Hinh] [varchar](150) NULL,
	[MoTa] [nvarchar](max) NULL,
	[TonKho] [bit] NOT NULL,
	[SoLuong] [smallint] NOT NULL,
	[Gia] [decimal](18, 0) NOT NULL,
	[GiamGia] [decimal](18, 0) NOT NULL,
	[MaLoai] [char](10) NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[ChiTietGioHang] ([MaGH], [MaSP], [SoLuong]) VALUES (34, N'1         ', 1)
INSERT [dbo].[ChiTietGioHang] ([MaGH], [MaSP], [SoLuong]) VALUES (34, N'5         ', 3)
GO
INSERT [dbo].[ChiTietHoaDon] ([MaSP], [MaHD], [SoLuongDatHang], [DonGia]) VALUES (N'1         ', N'LF30BS    ', 10, 300000.0000)
INSERT [dbo].[ChiTietHoaDon] ([MaSP], [MaHD], [SoLuongDatHang], [DonGia]) VALUES (N'10        ', N'B2TSZY    ', 1, 490000.0000)
INSERT [dbo].[ChiTietHoaDon] ([MaSP], [MaHD], [SoLuongDatHang], [DonGia]) VALUES (N'3         ', N'MEMUNP    ', 1, 151700.0000)
INSERT [dbo].[ChiTietHoaDon] ([MaSP], [MaHD], [SoLuongDatHang], [DonGia]) VALUES (N'6         ', N'X7KNF3    ', 1, 155400.0000)
INSERT [dbo].[ChiTietHoaDon] ([MaSP], [MaHD], [SoLuongDatHang], [DonGia]) VALUES (N'7         ', N'M0LLK1    ', 1, 450000.0000)
INSERT [dbo].[ChiTietHoaDon] ([MaSP], [MaHD], [SoLuongDatHang], [DonGia]) VALUES (N'7         ', N'T62HYF    ', 1, 450000.0000)
INSERT [dbo].[ChiTietHoaDon] ([MaSP], [MaHD], [SoLuongDatHang], [DonGia]) VALUES (N'7         ', N'WOV5BF    ', 1, 450000.0000)
INSERT [dbo].[ChiTietHoaDon] ([MaSP], [MaHD], [SoLuongDatHang], [DonGia]) VALUES (N'8         ', N'0OHERK    ', 1, 390000.0000)
INSERT [dbo].[ChiTietHoaDon] ([MaSP], [MaHD], [SoLuongDatHang], [DonGia]) VALUES (N'8         ', N'JOJV3X    ', 1, 390000.0000)
INSERT [dbo].[ChiTietHoaDon] ([MaSP], [MaHD], [SoLuongDatHang], [DonGia]) VALUES (N'8         ', N'MEMUNP    ', 1, 234000.0000)
INSERT [dbo].[ChiTietHoaDon] ([MaSP], [MaHD], [SoLuongDatHang], [DonGia]) VALUES (N'8         ', N'OU289Z    ', 1, 390000.0000)
INSERT [dbo].[ChiTietHoaDon] ([MaSP], [MaHD], [SoLuongDatHang], [DonGia]) VALUES (N'9         ', N'J3QZWX    ', 1, 420000.0000)
INSERT [dbo].[ChiTietHoaDon] ([MaSP], [MaHD], [SoLuongDatHang], [DonGia]) VALUES (N'9         ', N'JOJV3X    ', 1, 420000.0000)
GO
INSERT [dbo].[Coupon] ([MaCoupon], [GiamGia]) VALUES (N'FLASH99   ', 15)
GO
INSERT [dbo].[DanhGia] ([MaSP], [MaKH], [SoSao], [BinhLuan]) VALUES (N'1         ', 20, 5, N'a')
INSERT [dbo].[DanhGia] ([MaSP], [MaKH], [SoSao], [BinhLuan]) VALUES (N'9         ', 20, 5, N'beautiful')
GO
SET IDENTITY_INSERT [dbo].[GioHang] ON 

INSERT [dbo].[GioHang] ([MaGH], [MaKH]) VALUES (34, 20)
SET IDENTITY_INSERT [dbo].[GioHang] OFF
GO
INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayDatHang], [DiaChiGiaoHang], [TongGiaTri], [TrangThaiTT], [TrangThaiDH], [NgayNhanHang], [MaCoupon]) VALUES (N'0OHERK    ', 20, NULL, CAST(N'2024-09-04T13:30:11.237' AS DateTime), N'123 Nguyễn Hữu Thọ', 234000.0000, 0, 0, NULL, NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayDatHang], [DiaChiGiaoHang], [TongGiaTri], [TrangThaiTT], [TrangThaiDH], [NgayNhanHang], [MaCoupon]) VALUES (N'B2TSZY    ', 20, NULL, CAST(N'2024-09-03T23:27:02.563' AS DateTime), N'123 Nguyễn Hữu Thọ', 200900.0000, 0, 0, NULL, NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayDatHang], [DiaChiGiaoHang], [TongGiaTri], [TrangThaiTT], [TrangThaiDH], [NgayNhanHang], [MaCoupon]) VALUES (N'J3QZWX    ', 20, NULL, CAST(N'2024-09-04T01:01:54.883' AS DateTime), N'123 Nguyễn Hữu Thọ', 189000.0000, 0, 0, NULL, NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayDatHang], [DiaChiGiaoHang], [TongGiaTri], [TrangThaiTT], [TrangThaiDH], [NgayNhanHang], [MaCoupon]) VALUES (N'JOJV3X    ', 20, NULL, CAST(N'2024-09-04T13:34:36.990' AS DateTime), N'123 Nguyễn Hữu Thọ', 423000.0000, 0, 0, NULL, NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayDatHang], [DiaChiGiaoHang], [TongGiaTri], [TrangThaiTT], [TrangThaiDH], [NgayNhanHang], [MaCoupon]) VALUES (N'LF30BS    ', 20, NULL, CAST(N'2024-09-04T01:02:11.120' AS DateTime), N'123 Nguyễn Hữu Thọ', 1500000.0000, 0, 0, NULL, NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayDatHang], [DiaChiGiaoHang], [TongGiaTri], [TrangThaiTT], [TrangThaiDH], [NgayNhanHang], [MaCoupon]) VALUES (N'M0LLK1    ', 20, NULL, CAST(N'2024-09-04T13:33:32.797' AS DateTime), N'123 Nguyễn Hữu Thọ', 234000.0000, 0, 0, NULL, NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayDatHang], [DiaChiGiaoHang], [TongGiaTri], [TrangThaiTT], [TrangThaiDH], [NgayNhanHang], [MaCoupon]) VALUES (N'MEMUNP    ', 20, NULL, CAST(N'2024-09-04T13:40:58.920' AS DateTime), N'123 Nguyễn Hữu Thọ', 385700.0000, 0, 0, NULL, NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayDatHang], [DiaChiGiaoHang], [TongGiaTri], [TrangThaiTT], [TrangThaiDH], [NgayNhanHang], [MaCoupon]) VALUES (N'OU289Z    ', 20, NULL, CAST(N'2024-09-03T23:27:58.903' AS DateTime), N'123 Nguyễn Hữu Thọ', 234000.0000, 0, 0, NULL, NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayDatHang], [DiaChiGiaoHang], [TongGiaTri], [TrangThaiTT], [TrangThaiDH], [NgayNhanHang], [MaCoupon]) VALUES (N'T62HYF    ', 20, NULL, CAST(N'2024-09-04T01:08:20.817' AS DateTime), N'123 Nguyễn Hữu Thọ', 234000.0000, 0, 0, NULL, NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayDatHang], [DiaChiGiaoHang], [TongGiaTri], [TrangThaiTT], [TrangThaiDH], [NgayNhanHang], [MaCoupon]) VALUES (N'WOV5BF    ', 20, NULL, CAST(N'2024-09-04T00:48:55.293' AS DateTime), N'123 Nguyễn Hữu Thọ', 234000.0000, 0, 0, NULL, NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayDatHang], [DiaChiGiaoHang], [TongGiaTri], [TrangThaiTT], [TrangThaiDH], [NgayNhanHang], [MaCoupon]) VALUES (N'X7KNF3    ', 20, NULL, CAST(N'2024-09-04T13:40:06.407' AS DateTime), N'123 Nguyễn Hữu Thọ', 155400.0000, 0, 0, NULL, NULL)
GO
INSERT [dbo].[LoaiSP] ([MaLoai], [TenLoai], [Hinh]) VALUES (N'HOODIES   ', N'HOODIES', N'')
INSERT [dbo].[LoaiSP] ([MaLoai], [TenLoai], [Hinh]) VALUES (N'JACKET    ', N'JACKET', N'')
INSERT [dbo].[LoaiSP] ([MaLoai], [TenLoai], [Hinh]) VALUES (N'POLO      ', N'POLO', N'')
INSERT [dbo].[LoaiSP] ([MaLoai], [TenLoai], [Hinh]) VALUES (N'SHIRTS    ', N'SHIRTS', N'')
INSERT [dbo].[LoaiSP] ([MaLoai], [TenLoai], [Hinh]) VALUES (N'SWEATERS  ', N'SWEATERS', N'')
INSERT [dbo].[LoaiSP] ([MaLoai], [TenLoai], [Hinh]) VALUES (N'T-SHIRTS  ', N'T-SHIRTS', N'')
GO
SET IDENTITY_INSERT [dbo].[NguoiDung] ON 

INSERT [dbo].[NguoiDung] ([UserID], [HoTen], [Email], [DiaChi], [SDT], [TenTaiKhoan], [MatKhau], [IsAdmin]) VALUES (7, N'Admin', N'tinhcv181gmail.com', N'', N'0987654321', N'admin', N'$2a$10$iQ1oaYF/tb2ZI3UG.EgldOU9zSqKRBP7SvENq/iPEcNyxZlvfztke', 1)
INSERT [dbo].[NguoiDung] ([UserID], [HoTen], [Email], [DiaChi], [SDT], [TenTaiKhoan], [MatKhau], [IsAdmin]) VALUES (20, N'Le Minh Tinh', N'2154050301tinh@ou.edu.vn', N'123 Nguyễn Hữu Thọ', N'1223', N'tinh', N'$2a$11$jXbKvJLBXGOvzjuFjGhpjuky8TwNjd6BUEVRsSYXbBNcIctjWejPi', 0)
INSERT [dbo].[NguoiDung] ([UserID], [HoTen], [Email], [DiaChi], [SDT], [TenTaiKhoan], [MatKhau], [IsAdmin]) VALUES (21, N'aa', N'aa@a.a', N'a', N'123', N'aa', N'$2a$11$U8HuQpdwUuAu6sPLGvOKVeEPCS0JhKQwsgotQ8fjyYmVoq7YfVph.', 0)
SET IDENTITY_INSERT [dbo].[NguoiDung] OFF
GO
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Hinh], [MoTa], [TonKho], [SoLuong], [Gia], [GiamGia], [MaLoai]) VALUES (N'1         ', N'Simple T-Shirt/Cream', N'https://product.hstatic.net/200000305259/product/tee_solid_beige_1_7de152a789824544a8fb0eab449e57e6_master.jpg', N'Vải áo mềm mại hơn, thấm hút mồ hôi tốt, giúp người mặc cảm thấy thoải mái. Có độ bền và khả năng kháng khuẩn cao. Chống được nấm móc và bụi bẩn. Áo lâu sờn cũ và không bị xù lông.', 1, 990, CAST(300000 AS Decimal(18, 0)), CAST(50 AS Decimal(18, 0)), N'T-SHIRTS  ')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Hinh], [MoTa], [TonKho], [SoLuong], [Gia], [GiamGia], [MaLoai]) VALUES (N'10        ', N'Simple Hoodie/Gray', N'https://product.hstatic.net/200000305259/product/mockup_hoodie_grey_1_4bd9eda5ee8d4a96a9dbd904dc099d6e_master.jpg', N'Vải áo mềm mại hơn, thấm hút mồ hôi tốt, giúp người mặc cảm thấy thoải mái. Có độ bền và khả năng kháng khuẩn cao. Chống được nấm móc và bụi bẩn. Áo lâu sờn cũ và không bị xù lông.', 1, 999, CAST(490000 AS Decimal(18, 0)), CAST(59 AS Decimal(18, 0)), N'HOODIES   ')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Hinh], [MoTa], [TonKho], [SoLuong], [Gia], [GiamGia], [MaLoai]) VALUES (N'2         ', N'Sune Slickster T-Shirt/Black', N'https://product.hstatic.net/200000305259/product/tee_sune_slickster_blk_1_26abaa08a9644b10bcb5e65aee7897ce_master.jpg', N'Vải áo mềm mại hơn, thấm hút mồ hôi tốt, giúp người mặc cảm thấy thoải mái. Có độ bền và khả năng kháng khuẩn cao. Chống được nấm móc và bụi bẩn. Áo lâu sờn cũ và không bị xù lông.', 1, 1000, CAST(440000 AS Decimal(18, 0)), CAST(49 AS Decimal(18, 0)), N'T-SHIRTS  ')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Hinh], [MoTa], [TonKho], [SoLuong], [Gia], [GiamGia], [MaLoai]) VALUES (N'3         ', N'Simple Shirt/Gray', N'https://product.hstatic.net/200000305259/product/shirt_grey_model_1_ae27fb4fb26f4b71920aa75f6b2c19d1_master.jpg', N'Vải áo mềm mại hơn, thấm hút mồ hôi tốt, giúp người mặc cảm thấy thoải mái. Có độ bền và khả năng kháng khuẩn cao. Chống được nấm móc và bụi bẩn. Áo lâu sờn cũ và không bị xù lông.', 1, 999, CAST(370000 AS Decimal(18, 0)), CAST(59 AS Decimal(18, 0)), N'SHIRTS    ')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Hinh], [MoTa], [TonKho], [SoLuong], [Gia], [GiamGia], [MaLoai]) VALUES (N'4         ', N'Simple Shirt/Pink', N'https://product.hstatic.net/200000305259/product/shirt_pnk_1_1499b38f511d45798bace09a53a2f51c_master.jpg', N'Vải áo mềm mại hơn, thấm hút mồ hôi tốt, giúp người mặc cảm thấy thoải mái. Có độ bền và khả năng kháng khuẩn cao. Chống được nấm móc và bụi bẩn. Áo lâu sờn cũ và không bị xù lông.', 1, 1000, CAST(370000 AS Decimal(18, 0)), CAST(45 AS Decimal(18, 0)), N'SHIRTS    ')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Hinh], [MoTa], [TonKho], [SoLuong], [Gia], [GiamGia], [MaLoai]) VALUES (N'5         ', N'Simple Sweater/Navy', N'https://product.hstatic.net/200000305259/product/vgc-mockup_sweater_dark_navy_1_a31e2655788d4f64b129835d15a9e2d4_master.jpg', N'Vải áo mềm mại hơn, thấm hút mồ hôi tốt, giúp người mặc cảm thấy thoải mái. Có độ bền và khả năng kháng khuẩn cao. Chống được nấm móc và bụi bẩn. Áo lâu sờn cũ và không bị xù lông.', 1, 1000, CAST(420000 AS Decimal(18, 0)), CAST(39 AS Decimal(18, 0)), N'SWEATERS  ')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Hinh], [MoTa], [TonKho], [SoLuong], [Gia], [GiamGia], [MaLoai]) VALUES (N'6         ', N'Simple Sweater/White', N'https://product.hstatic.net/200000305259/product/vgc-mockup_sweater_wht_1_443ed7dfb5014890a878eb87cd298eed_master.jpg', N'Vải áo mềm mại hơn, thấm hút mồ hôi tốt, giúp người mặc cảm thấy thoải mái. Có độ bền và khả năng kháng khuẩn cao. Chống được nấm móc và bụi bẩn. Áo lâu sờn cũ và không bị xù lông.', 1, 999, CAST(420000 AS Decimal(18, 0)), CAST(63 AS Decimal(18, 0)), N'SWEATERS  ')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Hinh], [MoTa], [TonKho], [SoLuong], [Gia], [GiamGia], [MaLoai]) VALUES (N'7         ', N'Mix Line Polo/White-Black', N'https://product.hstatic.net/200000305259/product/polo_mix_line_wht-blk_1_5d9dcb44092b4e538cb132074b847837_master.jpg', N'Vải áo mềm mại hơn, thấm hút mồ hôi tốt, giúp người mặc cảm thấy thoải mái. Có độ bền và khả năng kháng khuẩn cao. Chống được nấm móc và bụi bẩn. Áo lâu sờn cũ và không bị xù lông.', 1, 997, CAST(450000 AS Decimal(18, 0)), CAST(48 AS Decimal(18, 0)), N'POLO      ')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Hinh], [MoTa], [TonKho], [SoLuong], [Gia], [GiamGia], [MaLoai]) VALUES (N'8         ', N'Simple Polo/Black', N'https://product.hstatic.net/200000305259/product/polo_blk_1_b8c6b48cfef2408191a794c7216f3e3f_master.jpg', N'Vải áo mềm mại hơn, thấm hút mồ hôi tốt, giúp người mặc cảm thấy thoải mái. Có độ bền và khả năng kháng khuẩn cao. Chống được nấm móc và bụi bẩn. Áo lâu sờn cũ và không bị xù lông.', 1, 996, CAST(390000 AS Decimal(18, 0)), CAST(40 AS Decimal(18, 0)), N'POLO      ')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Hinh], [MoTa], [TonKho], [SoLuong], [Gia], [GiamGia], [MaLoai]) VALUES (N'9         ', N'Simple Jacket/Black', N'https://product.hstatic.net/200000305259/product/vgc-jacket_blk_1_f2d04b3dd35542019d428797399b1a35_master.jpg', N'Vải áo mềm mại hơn, thấm hút mồ hôi tốt, giúp người mặc cảm thấy thoải mái. Có độ bền và khả năng kháng khuẩn cao. Chống được nấm móc và bụi bẩn. Áo lâu sờn cũ và không bị xù lông.', 1, 998, CAST(420000 AS Decimal(18, 0)), CAST(55 AS Decimal(18, 0)), N'JACKET    ')
GO
ALTER TABLE [dbo].[ChiTietGioHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietGioHang_GioHang1] FOREIGN KEY([MaGH])
REFERENCES [dbo].[GioHang] ([MaGH])
GO
ALTER TABLE [dbo].[ChiTietGioHang] CHECK CONSTRAINT [FK_ChiTietGioHang_GioHang1]
GO
ALTER TABLE [dbo].[ChiTietGioHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietGioHang_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[ChiTietGioHang] CHECK CONSTRAINT [FK_ChiTietGioHang_SanPham]
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDon_HoaDon] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HoaDon] ([MaHD])
GO
ALTER TABLE [dbo].[ChiTietHoaDon] CHECK CONSTRAINT [FK_ChiTietHoaDon_HoaDon]
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDon_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[ChiTietHoaDon] CHECK CONSTRAINT [FK_ChiTietHoaDon_SanPham]
GO
ALTER TABLE [dbo].[DanhGia]  WITH CHECK ADD  CONSTRAINT [FK_DanhGia_NguoiDung] FOREIGN KEY([MaKH])
REFERENCES [dbo].[NguoiDung] ([UserID])
GO
ALTER TABLE [dbo].[DanhGia] CHECK CONSTRAINT [FK_DanhGia_NguoiDung]
GO
ALTER TABLE [dbo].[GioHang]  WITH CHECK ADD  CONSTRAINT [FK_GioHang_NguoiDung] FOREIGN KEY([MaKH])
REFERENCES [dbo].[NguoiDung] ([UserID])
GO
ALTER TABLE [dbo].[GioHang] CHECK CONSTRAINT [FK_GioHang_NguoiDung]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_Coupon] FOREIGN KEY([MaCoupon])
REFERENCES [dbo].[Coupon] ([MaCoupon])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_Coupon]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_NguoiDung] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NguoiDung] ([UserID])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_NguoiDung]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_NguoiDung1] FOREIGN KEY([MaKH])
REFERENCES [dbo].[NguoiDung] ([UserID])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_NguoiDung1]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_LoaiSP] FOREIGN KEY([MaLoai])
REFERENCES [dbo].[LoaiSP] ([MaLoai])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_LoaiSP]
GO
/****** Object:  StoredProcedure [dbo].[GetSanPhamByMaSP]    Script Date: 7/9/2024 22:49:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSanPhamByMaSP]
    @MaSP char(10)
AS
BEGIN
    SELECT *       
    FROM dbo.SanPham
    WHERE MaSP = @MaSP;
END;

exec GetSanPhamByMaSP "AA01"
GO
USE [master]
GO
ALTER DATABASE [eFashionStore] SET  READ_WRITE 
GO
