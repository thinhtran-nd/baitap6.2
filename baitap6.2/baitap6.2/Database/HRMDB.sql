IF DB_ID(N'HRMDB') IS NULL
BEGIN
	CREATE DATABASE HRMDB;
END;
GO

USE HRMDB;
GO

IF OBJECT_ID(N'dbo.PhongBan', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.PhongBan
	(
		MaPB int IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhongBan PRIMARY KEY,
		TenPB nvarchar(100) NOT NULL,
		Mota nvarchar(255) NULL
	);
END;
GO

IF OBJECT_ID(N'dbo.NhanVien', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.NhanVien
	(
		MaNV int IDENTITY(1,1) NOT NULL CONSTRAINT PK_NhanVien PRIMARY KEY,
		HoTen nvarchar(100) NOT NULL,
		NgaySinh datetime NULL,
		CCCD varchar(20) NULL,
		MaPB int NULL,
		LuongCoBan decimal(18,2) NOT NULL,
		HeSoLuong float NOT NULL CONSTRAINT DF_NhanVien_HeSoLuong DEFAULT (1.0),
		CONSTRAINT CK_NhanVien_LuongCoBan CHECK (LuongCoBan > 0),
		CONSTRAINT FK_NhanVien_PhongBan FOREIGN KEY (MaPB) REFERENCES dbo.PhongBan(MaPB)
	);
END;
GO

IF EXISTS (SELECT 1 FROM sys.key_constraints WHERE name = N'UQ_NhanVien_CCCD')
	ALTER TABLE dbo.NhanVien DROP CONSTRAINT UQ_NhanVien_CCCD;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.NhanVien') AND name = N'UX_NhanVien_CCCD')
	CREATE UNIQUE INDEX UX_NhanVien_CCCD ON dbo.NhanVien(CCCD) WHERE CCCD IS NOT NULL;
GO

IF OBJECT_ID(N'dbo.BangChamCong', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.BangChamCong
	(
		MaCong int IDENTITY(1,1) NOT NULL CONSTRAINT PK_BangChamCong PRIMARY KEY,
		MaNV int NOT NULL,
		ThangNam varchar(7) NOT NULL,
		SoNgayCong float NOT NULL CONSTRAINT DF_BangChamCong_SoNgayCong DEFAULT (0),
		SoGioOT float NOT NULL CONSTRAINT DF_BangChamCong_SoGioOT DEFAULT (0),
		DaChot bit NOT NULL CONSTRAINT DF_BangChamCong_DaChot DEFAULT (0),
		CONSTRAINT FK_BangChamCong_NhanVien FOREIGN KEY (MaNV) REFERENCES dbo.NhanVien(MaNV),
		CONSTRAINT UQ_BangChamCong_MaNV_ThangNam UNIQUE (MaNV, ThangNam),
		CONSTRAINT CK_BangChamCong_SoNgayCong CHECK (SoNgayCong >= 0 AND SoNgayCong <= 31),
		CONSTRAINT CK_BangChamCong_SoGioOT CHECK (SoGioOT >= 0)
	);
END;
GO

IF OBJECT_ID(N'dbo.BangLuong', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.BangLuong
	(
		MaLuong int IDENTITY(1,1) NOT NULL CONSTRAINT PK_BangLuong PRIMARY KEY,
		MaNV int NOT NULL,
		ThangNam varchar(7) NOT NULL,
		ThucLinh decimal(18,2) NOT NULL,
		CONSTRAINT FK_BangLuong_NhanVien FOREIGN KEY (MaNV) REFERENCES dbo.NhanVien(MaNV),
		CONSTRAINT UQ_BangLuong_MaNV_ThangNam UNIQUE (MaNV, ThangNam)
	);
END;
GO

IF OBJECT_ID(N'dbo.TR_BangChamCong_KhoaSauKhiChot', N'TR') IS NULL
	EXEC(N'
		CREATE TRIGGER dbo.TR_BangChamCong_KhoaSauKhiChot
		ON dbo.BangChamCong
		AFTER UPDATE, DELETE
		AS
		BEGIN
			SET NOCOUNT ON;

			IF EXISTS (SELECT 1 FROM deleted WHERE DaChot = 1)
			BEGIN
				THROW 51000, ''Attendance for a closed payroll month cannot be changed or deleted.'', 1;
			END;
		END;
	');
GO
