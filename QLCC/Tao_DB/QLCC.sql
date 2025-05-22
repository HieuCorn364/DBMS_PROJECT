CREATE DATABASE DOANDBMS;
GO

USE DOANDBMS;  -- Chuyển sang sử dụng cơ sở dữ liệu mới tạo
GO

-- Bảng KhuCanHo
CREATE TABLE KhuCanHo (
    MaKhuCanHo INT IDENTITY(1,1) PRIMARY KEY,
    TenKhuCanHo VARCHAR(100),
    SoLuong INT NOT NULL
);

-- Bảng LoaiCanHo
CREATE TABLE LoaiCanHo (
    MaLoai INT IDENTITY(200,1) PRIMARY KEY,
    TienThue DECIMAL(18, 2) NOT NULL,
    TenLoai NVARCHAR(100)
);

-- Bảng CanHo
CREATE TABLE CanHo (
    MaCanHo INT IDENTITY(1000,1) PRIMARY KEY,
    TrangThaiSuDung VARCHAR(50) NOT NULL,
	KieuSoHuu VARCHAR(50),
    MaKhuCanHo INT,
    MaLoaiCanHo INT,
    CONSTRAINT FK_CanHo_KhuCanHo FOREIGN KEY (MaKhuCanHo) REFERENCES KhuCanHo(MaKhuCanHo),
    CONSTRAINT FK_CanHo_LoaiCanHo FOREIGN KEY (MaLoaiCanHo) REFERENCES LoaiCanHo(MaLoai)
);

-- Bảng ChuHo
CREATE TABLE ChuHo (
    MaChuHo INT IDENTITY(100,1) PRIMARY KEY,
    NgayBatDau DATE,
    NgayKetThuc DATE,
	KieuSoHuu VARCHAR(50),
    MaCanHo INT,
    CONSTRAINT FK_ChuHo_CanHo FOREIGN KEY (MaCanHo) REFERENCES CanHo(MaCanHo) ON DELETE SET NULL,
);


-- Bảng CuDan
CREATE TABLE CuDan(
	MaCuDan INT IDENTITY(221100,1) PRIMARY KEY,
	TenCuDan NVARCHAR(100) NOT NULL,
	SDT VARCHAR(10) CHECK(LEN(SDT) = 10 AND SDT NOT LIKE '%[^0-9]%'),
	CCCD VARCHAR(12) UNIQUE NOT NULL CHECK(LEN(CCCD) = 12 AND CCCD NOT LIKE '%[^0-9]%'),
	GioiTinh BIT NOT NULL,
	MaChuHo INT FOREIGN KEY REFERENCES  ChuHo(MaChuHo)
);
-- Bảng TienIch
CREATE TABLE TienIch (
    MaTienIch INT IDENTITY(3000,1) PRIMARY KEY,
    TenTienIch NVARCHAR(100),
    DonGia DECIMAL(18, 2) NOT NULL,
);

-- Bảng HoaDon
CREATE TABLE HoaDon (
    MaHoaDon INT IDENTITY(1,1) PRIMARY KEY,
    TongTien DECIMAL(18, 2),
    TrangThai NVARCHAR(50),
    NgayLapHoaDon DATE,
    NguoiThanhToan INT,
    FOREIGN KEY (NguoiThanhToan) REFERENCES ChuHo(MaChuHo),
    CHECK (TongTien >= 0)
);

-- Bảng ChiTietHoaDon
CREATE TABLE ChiTietHoaDon (
    MaHoaDon INT,
    LoaiTienIch INT,
    SoLuong INT,
	GiaTien DECIMAL(18,2),
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon),
    FOREIGN KEY (LoaiTienIch) REFERENCES TienIch(MaTienIch),
    CHECK (SoLuong >= 0)
);
GO

-- thêm khu căn hộ
INSERT INTO KhuCanHo (TenKhuCanHo, SoLuong)
VALUES ('Khu A', 10);

INSERT INTO KhuCanHo (TenKhuCanHo, SoLuong)
VALUES ('Khu B', 15);

INSERT INTO KhuCanHo (TenKhuCanHo, SoLuong)
VALUES ('Khu C', 20);


 -- thêm loại căn hộ
INSERT INTO LoaiCanHo (TienThue, TenLoai)
VALUES (5000000, 'Co Ban');

INSERT INTO LoaiCanHo (TienThue, TenLoai)
VALUES (7000000, 'Trung Binh');

INSERT INTO LoaiCanHo (TienThue, TenLoai)
VALUES (12000000, 'Vip');

--thêm căn hộ
-- Chèn 10 căn hộ cho MaKhuCanHo = 1
DECLARE @i INT = 1;
WHILE @i <= 10
BEGIN
    INSERT INTO CanHo (TrangThaiSuDung, KieuSoHuu, MaKhuCanHo, MaLoaiCanHo)
    VALUES (
        'Trong',                -- Giá trị cố định cho TrangThaiSuDung
        'Khong',                -- Giá trị cố định cho KieuSoHuu
        1,                      -- Giá trị cố định cho MaKhuCanHo là 1
        FLOOR(RAND() * 3) + 200 -- Giá trị ngẫu nhiên cho MaLoaiCanHo (200 đến 202)
    );

    SET @i = @i + 1;
END;

-- Chèn 15 căn hộ cho MaKhuCanHo = 2
SET @i = 1;
WHILE @i <= 15
BEGIN
    INSERT INTO CanHo (TrangThaiSuDung, KieuSoHuu, MaKhuCanHo, MaLoaiCanHo)
    VALUES (
        'Trong',                -- Giá trị cố định cho TrangThaiSuDung
        'Khong',                -- Giá trị cố định cho KieuSoHuu
        2,                      -- Giá trị cố định cho MaKhuCanHo là 2
        FLOOR(RAND() * 3) + 200 -- Giá trị ngẫu nhiên cho MaLoaiCanHo (200 đến 202)
    );

    SET @i = @i + 1;
END;

-- Chèn 20 căn hộ cho MaKhuCanHo = 3
SET @i = 1;
WHILE @i <= 20
BEGIN
    INSERT INTO CanHo (TrangThaiSuDung, KieuSoHuu, MaKhuCanHo, MaLoaiCanHo)
    VALUES (
        'Trong',                -- Giá trị cố định cho TrangThaiSuDung
        'Khong',                -- Giá trị cố định cho KieuSoHuu
        3,                      -- Giá trị cố định cho MaKhuCanHo là 3
        FLOOR(RAND() * 3) + 200 -- Giá trị ngẫu nhiên cho MaLoaiCanHo (200 đến 202)
    );

    SET @i = @i + 1;
END;


-- thêm cư dân
INSERT INTO CuDan (TenCuDan, SDT, CCCD, GioiTinh, MaChuHo)
VALUES ('Ngo Trung Hieu', '0912345678', '123456789012', 1, 100);

INSERT INTO CuDan (TenCuDan, SDT, CCCD, GioiTinh, MaChuHo)
        VALUES ('Ánh Loan', '0902982356', 1, 100);

INSERT INTO CuDan (TenCuDan, SDT, CCCD, GioiTinh)
VALUES ('Binh Minh', '0912340009', '023450009012', 1);

INSERT INTO CuDan (TenCuDan, SDT, CCCD, GioiTinh)
VALUES ('Ly Nha Ky', '0912340008', '123450009012', 1);

INSERT INTO CuDan (TenCuDan, SDT, CCCD, GioiTinh)
VALUES ('Pham Nuong', '0935625849', '077986789012', 0);

INSERT INTO CuDan (TenCuDan, SDT, CCCD, GioiTinh)
VALUES ('Ly Manh Tuan', '0923489858', '074367890125', 1);

INSERT INTO CuDan (TenCuDan, SDT, CCCD, GioiTinh, MaChuHo)
VALUES ('Hoang Thi B', '0987654321', '077654321098', 0, 221100);

INSERT INTO CuDan (TenCuDan, CCCD, GioiTinh, MaChuHo)
VALUES ('Nguy Phat', '077654721098', 0, 221100);

INSERT INTO CuDan (TenCuDan, SDT, CCCD, GioiTinh, MaChuHo)
VALUES ('Hoang Thien Hanh', '0987999321', '087654321098', 0, 221101);

INSERT INTO CuDan (TenCuDan, SDT, CCCD, GioiTinh, MaChuHo)
VALUES ('Thien Vuong Le', '0987693221', '087654329998', 0, 221101);

INSERT INTO CuDan (TenCuDan, SDT, CCCD, GioiTinh)
VALUES ('Tuan Huy', '0989952421', '087654328298', 0);


INSERT INTO CuDan (TenCuDan, SDT, CCCD, GioiTinh, MaChuHo)
VALUES ('Ngo Trung Hieu', '0987999321', '087654321078', 0, 221110);

INSERT INTO CuDan (TenCuDan, SDT, CCCD, GioiTinh, MaChuHo)
VALUES ('Le Hoang Danh', '0987693221', '087876029998', 0, 221110);

INSERT INTO CuDan (TenCuDan, CCCD, GioiTinh, MaChuHo)
VALUES ('Trong Nghia', '087651239998', 0, 221110);

--thêm chủ hộ
INSERT INTO ChuHo (NgayBatDau, NgayKetThuc, MaCanHo, KieuSoHuu)
VALUES ( '2024-01-01', '2084-01-01', 1001, 'Mua');

INSERT INTO ChuHo (NgayBatDau, NgayKetThuc, MaCanHo, KieuSoHuu)
VALUES ('2024-12-09', '2025-07-01', 1002, 'Thue');

INSERT INTO ChuHo (NgayBatDau, NgayKetThuc, MaCanHo, KieuSoHuu)
VALUES ('2024-09-01', '2029-01-01', 1040, 'Thue');

INSERT INTO ChuHo (MaChuHo, NgayBatDau, NgayKetThuc, MaCanHo, KieuSoHuu)
VALUES (221114, '2024-09-01', '2028-09-01', 1041, 'Thue');

--Xóa chủ hộ
DELETE FROM ChuHo
WHERE MaChuHo = 221100;

--Thêm tiện ích
INSERT INTO TienIch(TenTienIch, DonGia) VALUES ('Tien Rac', 50000);
INSERT INTO TienIch(TenTienIch, DonGia) VALUES ('Tien Dien', 2000);
INSERT INTO TienIch(TenTienIch, DonGia) VALUES ('Tien Nuoc', 10000);
INSERT INTO TienIch(TenTienIch, DonGia) VALUES ('Tien Mang', 300000);
INSERT INTO TienIch(TenTienIch, DonGia) VALUES ('Phi Quan Ly', 500000);
INSERT INTO TienIch(TenTienIch, DonGia) VALUES ('Phi Gui Xe', 150000);
INSERT INTO TienIch(TenTienIch, DonGia) VALUES ('Phi Moi Truong', 70000);
INSERT INTO TienIch(TenTienIch, DonGia) VALUES ('Tien Thue Can Ho', 0);

ALTER TABLE TienIch
DROP CONSTRAINT chk_DonGia;


--Thêm chi tiết hóa đơn
use QuanLyChungCu
