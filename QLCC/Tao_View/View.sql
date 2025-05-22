use QuanLyChungCu;
go
--View lấy thông tin các cư dân sống trong từng căn hộ
CREATE VIEW DanhSachCuDanVaChuHo AS
SELECT 
    cd.MaCuDan, 
    cd.TenCuDan, 
    cd.SDT, 
    cd.CCCD, 
    cd.GioiTinh, 
    ch.MaChuHo,
    ch.NgayBatDau,
    ch.NgayKetThuc,
    ch.KieuSoHuu,
    ch.MaCanHo,
    kc.TenKhuCanHo 
FROM 
    CuDan cd
JOIN 
    ChuHo ch ON cd.MaChuHo = ch.MaChuHo
JOIN 
    CanHo c ON ch.MaCanHo = c.MaCanHo
JOIN 
    KhuCanHo kc ON c.MaKhuCanHo = kc.MaKhuCanHo;

select * from DanhSachCuDanVaChuHo
where MaChuHo = 221101;
GO

-----------
CREATE VIEW ThongTinCacCanHo AS
SELECT 
    c.MaCanHo,
    c.TrangThaiSuDung,
    kc.TenKhuCanHo,
    lc.TenLoai,
    lc.TienThue
FROM 
    CanHo c
JOIN 
    KhuCanHo kc ON c.MaKhuCanHo = kc.MaKhuCanHo
JOIN 
    LoaiCanHo lc ON c.MaLoaiCanHo = lc.MaLoai

SELECT * FROM ThongTinCacCanHo
where Tenkhucanho = 'Khu A';
go
---------------

CREATE VIEW [dbo].[view_danhsachcudan] AS
SELECT MaCuDan, TenCuDan, SDT, CCCD, CASE WHEN GioiTinh = 1 THEN N'Nam' ELSE N'Nu' END AS GioiTinh, MaChuHo		
FROM dbo.CuDan
go

CREATE VIEW V_TimCanHoTrong
AS
SELECT 
    MaCanHo, 
    TrangThaiSuDung,  
    MaKhuCanHo, 
    MaLoaiCanHo
FROM 
    QuanLyChungCu.dbo.CanHo
WHERE 
    TrangThaiSuDung = N'Trong';
go
use DADBMS;
go
Create view [dbo].[view_danhsachcanho] AS
Select MaCanHo, TrangThaiSuDung, MaKhuCanHo, MaLoaiCanHo
From dbo.CanHo


CREATE VIEW V_TimMaKhuCanHo
AS
SELECT 
    MaKhuCanHo, 
    TenKhuCanHo, 
    SoLuong
FROM 
    KhuCanHo;

CREATE VIEW V_TimMaLoaiCanHo
AS
SELECT 
    MaLoai, 
    TienThue, 
    TenLoai
FROM 
    LoaiCanHo;

use DADBMS;
CREATE VIEW View_HoaDon AS
SELECT 
    MaHoaDon,
    TongTien,
    TrangThai,
    NgayLapHoaDon,
    NguoiThanhToan
FROM HoaDon;
