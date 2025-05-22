use QuanLyChungCu;
--thêm cư dân
CREATE PROCEDURE sp_ThemCuDan
    @TenCuDan NVARCHAR(100),
    @SDT NVARCHAR(15),
    @CCCD NVARCHAR(20),
    @GioiTinh NVARCHAR(10),
    @MaChuHo INT
AS
BEGIN
        INSERT INTO CuDan (TenCuDan, SDT, CCCD, GioiTinh, MaChuHo)
        VALUES (@TenCuDan, @SDT, @CCCD, @GioiTinh, @MaChuHo);
END;
go

--------------------
--Lấy thông tin căn hộ
CREATE PROCEDURE sp_LayThongTinCanHo
    @MaCanHo INT = NULL  -- Tham số đầu vào, có thể để NULL để lấy tất cả căn hộ
AS
BEGIN
    -- Nếu tham số @MaCanHo được cung cấp, chỉ lấy thông tin căn hộ đó
    IF @MaCanHo IS NOT NULL
    BEGIN
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
        WHERE 
            c.MaCanHo = @MaCanHo;
    END
	ELSE
	BEGIN
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
        LoaiCanHo lc ON c.MaLoaiCanHo = lc.MaLoai;
	END
END;

exec sp_LayThongTinCanHo @MaCanHo = 1000
go

----------------------------------------------
--Thêm Hóa Đơn
CREATE PROCEDURE sp_TaoHoaDon
    @NguoiThanhToan INT,
    @TongTien DECIMAL(18, 2) = 1,      -- Giá trị mặc định là 1
    @TrangThai NVARCHAR(50) = 'Chua Thanh Toan', -- Giá trị mặc định là "Chua Thanh Toan"
    @NgayLapHoaDon DATE = NULL -- Giá trị mặc định sẽ được tính là ngày 26 hằng tháng
AS
BEGIN
    -- Nếu NgayLapHoaDon không được truyền vào, gán giá trị mặc định là ngày 26 của tháng hiện tại
    IF @NgayLapHoaDon IS NULL
    BEGIN
        SET @NgayLapHoaDon = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 26);
    END

    -- Kiểm tra nếu TongTien nhỏ hơn hoặc bằng 0
    IF @TongTien <= 0
    BEGIN
        RAISERROR('TongTien phải lớn hơn 0', 16, 1);
        RETURN;
    END

    -- Thêm hóa đơn vào bảng HoaDon
    INSERT INTO HoaDon (TongTien, TrangThai, NgayLapHoaDon, NguoiThanhToan)
    VALUES (@TongTien, @TrangThai, @NgayLapHoaDon, @NguoiThanhToan);

    -- Thông báo thành công
    PRINT 'Hoa Don Tao Thanh Cong!';
END;

exec sp_TaoHoaDon @NguoiThanhToan = 221110;
go

----------------------
--tìm kiếm hóa đơn theo tháng và năm
CREATE PROCEDURE sp_TimKiemHoaDonTheoThangNam
    @Month INT,  -- Tháng cần tìm kiếm
    @Year INT    -- Năm cần tìm kiếm
AS
BEGIN
    -- Tìm kiếm các hóa đơn theo tháng và năm
    SELECT *
    FROM HoaDon
    WHERE MONTH(NgayLapHoaDon) = @Month
      AND YEAR(NgayLapHoaDon) = @Year;
END;
GO

------------------------
--Lấy thông tin chi tiết hóa đơn theo mã hóa đơn
CREATE PROCEDURE sp_LayChiTietHoaDon
    @MaHoaDon INT
AS
BEGIN
    SELECT 
        ti.TenTienIch, 
        cth.SoLuong, 
        cth. 
    FROM ChiTietHoaDon cth
    JOIN TienIch ti ON cth.LoaiTienIch = ti.MaTienIch
    WHERE cth.MaHoaDon = @MaHoaDon;
END

exec sp_LayChiTietHoaDon @MaHoaDon = 2;
go
-------------------------
--function
CREATE FUNCTION fn_TimKiemHoaDonTheoThangNam
(
    @Month INT,
    @Year INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM HoaDon
    WHERE MONTH(NgayLapHoaDon) = @Month
      AND YEAR(NgayLapHoaDon) = @Year
);
go

SELECT *
FROM fn_TimKiemHoaDonTheoThangNam(10, 2024); 
go
-------------------------
--mới
CREATE OR ALTER PROCEDURE [dbo].[sp_TaoHoaDonChoChuHo]           
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Kiểm tra xem có chủ hộ nào không
        IF NOT EXISTS (SELECT 1 FROM ChuHo)
            THROW 50001, N'Không có chủ hộ nào trong hệ thống', 1;
            
        -- Khai báo biến
        DECLARE @TongTien DECIMAL(18, 2) = 1000;      
        DECLARE @TrangThai NVARCHAR(50) = N'Chua Thanh Toan';
        DECLARE @NgayLapHoaDon DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 26);
        DECLARE @MaChuHo INT;
        DECLARE @HoaDonCount INT = 0;
        
        BEGIN TRANSACTION;
            -- Sử dụng cursor để duyệt qua từng chủ hộ chưa có hóa đơn trong tháng này
            DECLARE cur CURSOR LOCAL FAST_FORWARD FOR 
            SELECT ch.MaChuHo 
            FROM ChuHo ch
            WHERE NOT EXISTS (
                SELECT 1 
                FROM HoaDon hd 
                WHERE hd.NguoiThanhToan = ch.MaChuHo
                AND MONTH(hd.NgayLapHoaDon) = MONTH(@NgayLapHoaDon)
                AND YEAR(hd.NgayLapHoaDon) = YEAR(@NgayLapHoaDon)
            );
            
            OPEN cur;
            FETCH NEXT FROM cur INTO @MaChuHo;
            
            WHILE @@FETCH_STATUS = 0
            BEGIN
                -- Tạo hóa đơn mới
                INSERT INTO HoaDon (TongTien, TrangThai, NgayLapHoaDon, NguoiThanhToan)
                VALUES (@TongTien, @TrangThai, @NgayLapHoaDon, @MaChuHo);
                
                SET @HoaDonCount = @HoaDonCount + 1;
                FETCH NEXT FROM cur INTO @MaChuHo;
            END
            
            CLOSE cur;
            DEALLOCATE cur;
                
        COMMIT TRANSACTION;
        
        -- Trả về số lượng hóa đơn đã tạo
        SELECT @HoaDonCount as SoHoaDonDaTao;
        RETURN 1; -- Thành công
        
    END TRY
    BEGIN CATCH
        -- Đóng và giải phóng cursor nếu đang mở
        IF CURSOR_STATUS('local', 'cur') >= 0
        BEGIN
            CLOSE cur;
            DEALLOCATE cur;
        END
        
        -- Rollback transaction nếu đang trong transaction
        IF @@TRANCOUNT > 0 
            ROLLBACK TRANSACTION;
            
        DECLARE @ErrorMessage NVARCHAR(MAX) = ERROR_MESSAGE();
        THROW 50000, @ErrorMessage, 1;
        RETURN 0;
    END CATCH;
END;

---------------
CREATE PROCEDURE sp_TaoHoaDonChoChuHo           
AS
BEGIN
	DECLARE @TongTien DECIMAL(18, 2) = 1000,      
    @TrangThai NVARCHAR(50) = 'Chua Thanh Toan', 
    @NgayLapHoaDon DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 26);

    DECLARE @MaChuHo INT;

    DECLARE cur CURSOR FOR 
    SELECT MaChuHo FROM ChuHo;
    OPEN cur;
    FETCH NEXT FROM cur INTO @MaChuHo;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        INSERT INTO HoaDon (TongTien, TrangThai, NgayLapHoaDon, NguoiThanhToan)
        VALUES (@TongTien, @TrangThai, @NgayLapHoaDon, @MaChuHo);
        FETCH NEXT FROM cur INTO @MaChuHo;
    END

    CLOSE cur;
    DEALLOCATE cur;
END;

----------------
exec sp_TaoHoaDonChoChuHo;
go


----------
CREATE PROCEDURE tg_CapNhatTrangThaiHoaDon
    @MaHoaDon INT,             
    @TrangThaiMoi NVARCHAR(50) 
AS
BEGIN
        UPDATE HoaDon
        SET TrangThai = @TrangThaiMoi
        WHERE MaHoaDon = @MaHoaDon;  
END;
go

--------------------------
--xóa cư dân
CREATE PROCEDURE sp_XoaCuDan
    @MaCuDan INT
AS
BEGIN
    DELETE FROM CuDan
    WHERE MaCuDan = @MaCuDan;
END
go

CREATE PROCEDURE sp_CapNhatCuDan
    @MaCuDan INT,
    @TenCuDan NVARCHAR(100),
    @SDT VARCHAR(10),
    @CCCD VARCHAR(12),
    @GioiTinh BIT,
    @MaChuHo INT = NULL
AS
BEGIN
    UPDATE CuDan
    SET TenCuDan = @TenCuDan, SDT = @SDT, CCCD = @CCCD, GioiTinh = @GioiTinh, MaChuHo = @MaChuHo
    WHERE MaCuDan = @MaCuDan;
END
go

CREATE PROCEDURE sp_TaoChiTietHoaDon
    @MaHoaDon INT,
    @SoDien INT,
    @SoKhoiNuoc INT,
    @SoXe INT
AS
BEGIN
    DECLARE @SoLuong INT = 1;
    DECLARE @MaCanHo INT;
    DECLARE @KieuSoHuu NVARCHAR(50);
    DECLARE @TienThue DECIMAL(18,2) = 0;

    SELECT @MaCanHo = MaCanHo, @KieuSoHuu = KieuSoHuu
    FROM ChuHo
    WHERE MaChuHo = (SELECT NguoiThanhToan FROM HoaDon WHERE MaHoaDon = @MaHoaDon);

    INSERT INTO ChiTietHoaDon (MaHoaDon, LoaiTienIch, SoLuong)
    VALUES (@MaHoaDon, 3000, @SoLuong);

    INSERT INTO ChiTietHoaDon (MaHoaDon, LoaiTienIch, SoLuong)
    VALUES (@MaHoaDon, 3001, @SoDien);

    INSERT INTO ChiTietHoaDon (MaHoaDon, LoaiTienIch, SoLuong)
    VALUES (@MaHoaDon, 3002, @SoKhoiNuoc);

    INSERT INTO ChiTietHoaDon (MaHoaDon, LoaiTienIch, SoLuong)
    VALUES (@MaHoaDon, 3003, @SoLuong);

    INSERT INTO ChiTietHoaDon (MaHoaDon, LoaiTienIch, SoLuong)
    VALUES (@MaHoaDon, 3004, @SoLuong);

    INSERT INTO ChiTietHoaDon (MaHoaDon, LoaiTienIch, SoLuong)
    VALUES (@MaHoaDon, 3005, @SoXe);

    INSERT INTO ChiTietHoaDon (MaHoaDon, LoaiTienIch, SoLuong)
    VALUES (@MaHoaDon, 3006, @SoLuong);
    IF @KieuSoHuu = 'Thue'
    BEGIN  
        SELECT @TienThue = TienThue
        FROM LoaiCanHo L
        JOIN CanHo C ON C.MaLoaiCanHo = L.MaLoai
        WHERE C.MaCanHo = @MaCanHo;

        UPDATE TienIch
		SET DonGia = @tienthue
		WHERE MaTienIch = 3012;

        INSERT INTO ChiTietHoaDon (MaHoaDon, LoaiTienIch, SoLuong, GiaTien)
        VALUES (@MaHoaDon, 3012, 1, @TienThue);
    END
END;


exec sp_TaoChiTietHoaDon @MaHoaDon = 5,  @SoDien = 60,
    @SoKhoiNuoc = 30,
    @SoXe = 2;


	CREATE PROCEDURE sp_ThemChuHo
    @MaChuHo INT,
    @NgayBatDau DATE,
    @NgayKetThuc DATE,
    @KieuSoHuu VARCHAR(50),
    @MaCanHo INT
AS
BEGIN
    INSERT INTO ChuHo (MaChuHo, NgayBatDau, NgayKetThuc, KieuSoHuu, MaCanHo)
    VALUES (@MaChuHo, @NgayBatDau, @NgayKetThuc, @KieuSoHuu, @MaCanHo);
END;

CREATE PROCEDURE sp_CapNhatChuHo
    @MaChuHo INT,
    @NgayBatDau DATE,
    @NgayKetThuc DATE,
    @KieuSoHuu VARCHAR(50),
    @MaCanHo INT
AS
BEGIN
    UPDATE ChuHo
    SET NgayBatDau = @NgayBatDau,
        NgayKetThuc = @NgayKetThuc,
        KieuSoHuu = @KieuSoHuu,
        MaCanHo = @MaCanHo
    WHERE MaChuHo = @MaChuHo;
END;

CREATE PROCEDURE sp_XoaCanHo
    @MaCanHo INT
AS
BEGIN
    DELETE FROM CanHo
    WHERE MaCanHo = @MaCanHo;
END;

CREATE PROCEDURE sp_ThemCanHo
    @TrangThaiSuDung VARCHAR(50),
    @MaKhuCanHo INT,
    @MaLoaiCanHo INT
AS
BEGIN
    INSERT INTO CanHo (TrangThaiSuDung, MaKhuCanHo, MaLoaiCanHo)
    VALUES (@TrangThaiSuDung, @MaKhuCanHo, @MaLoaiCanHo);
END;


CREATE PROCEDURE sp_CapNhatCanHo
    @MaCanHo INT,
    @TrangThaiSuDung VARCHAR(50),
    @MaKhuCanHo INT,
    @MaLoaiCanHo INT
AS
BEGIN
    UPDATE CanHo
    SET TrangThaiSuDung = @TrangThaiSuDung,
        MaKhuCanHo = @MaKhuCanHo,
        MaLoaiCanHo = @MaLoaiCanHo
    WHERE MaCanHo = @MaCanHo;
END;


go
CREATE FUNCTION fn_TinhDoanhThuTienIch(
    @Thang INT,
    @Nam INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        TI.MaTienIch AS MaTienIch,
        TI.TenTienIch AS TenTienIch,
        SUM(CTHD.GiaTien) AS TongDoanhThu
    FROM 
        ChiTietHoaDon AS CTHD
    INNER JOIN 
        TienIch AS TI ON CTHD.LoaiTienIch = TI.MaTienIch
    INNER JOIN 
        HoaDon AS HD ON CTHD.MaHoaDon = HD.MaHoaDon
    WHERE 
        MONTH(HD.NgayLapHoaDon) = @Thang
        AND YEAR(HD.NgayLapHoaDon) = @Nam
    GROUP BY 
        TI.MaTienIch, TI.TenTienIch
);
GO
------------
CREATE FUNCTION fn_TimKiemCuDanTheoMa
(
    @MaCuDan INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        MaCuDan,
        TenCuDan,
        SDT,
        CCCD,
        CASE 
            WHEN GioiTinh = 1 THEN 'Nam'
            ELSE 'Nu'
        END AS GioiTinh,
        MaChuHo
    FROM 
        CuDan
    WHERE 
        MaCuDan = @MaCuDan
);
go

--------------------



CREATE FUNCTION fn_LayTienThueCanHo
(
    @MaCanHo INT
)
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @TienThue DECIMAL(18, 2);

    SELECT 
        @TienThue = lc.TienThue
    FROM 
        CanHo c
        JOIN LoaiCanHo lc ON c.MaLoaiCanHo = lc.MaLoai
    WHERE 
        c.MaCanHo = @MaCanHo;

    RETURN ISNULL(@TienThue, 0);
END
GO
drop function fn_LayTienThueCanHo
use Quanlychungcu 

SELECT dbo.fn_LayTienThueCanHo(1001) AS TienThue;

use DADBMS;
Create FUNCTION GetCanHoTrong()
RETURNS TABLE
AS
RETURN
(
    SELECT 
        MaCanHo,
        TrangThaiSuDung,
        MaKhuCanHo,
        MaLoaiCanHo
    FROM 
        CanHo
    WHERE 
        TrangThaiSuDung = N'Trong'
)
CREATE OR ALTER PROCEDURE sp_XoaChuHo
    @MaChuHo INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Khai báo biến
    DECLARE @KieuSoHuu NVARCHAR(10);
    DECLARE @MaCanHo INT;
    DECLARE @SoCuDan INT = 0;
    DECLARE @SoHoaDon INT = 0;
    
    BEGIN TRY
        -- Kiểm tra tồn tại chủ hộ
        SELECT 
            @KieuSoHuu = KieuSoHuu,
            @MaCanHo = MaCanHo
        FROM ChuHo 
        WHERE MaChuHo = @MaChuHo;

        IF @MaChuHo IS NULL
            THROW 50001, N'Không tìm thấy chủ hộ', 1;

        -- Đếm số cư dân và hóa đơn
        SELECT @SoCuDan = COUNT(*) FROM CuDan WHERE MaChuHo = @MaChuHo;
        SELECT @SoHoaDon = COUNT(*) FROM HoaDon WHERE NguoiThanhToan = @MaChuHo;

        BEGIN TRANSACTION;
            -- Lưu thông tin cư dân và hóa đơn bị xóa để báo cáo
            SELECT *
            INTO #DeletedCuDan
            FROM CuDan
            WHERE MaChuHo = @MaChuHo;

            SELECT *
            INTO #DeletedHoaDon
            FROM HoaDon
            WHERE NguoiThanhToan = @MaChuHo;

            -- Xóa chi tiết hóa đơn trước
            DELETE FROM ChiTietHoaDon 
            WHERE MaHoaDon IN (SELECT MaHoaDon FROM HoaDon WHERE NguoiThanhToan = @MaChuHo);

            -- Xóa hóa đơn
            DELETE FROM HoaDon 
            WHERE NguoiThanhToan = @MaChuHo;

            -- Xóa cư dân
            DELETE FROM CuDan 
            WHERE MaChuHo = @MaChuHo;

            -- Xóa chủ hộ
            DELETE FROM ChuHo 
            WHERE MaChuHo = @MaChuHo;

            -- Cập nhật trạng thái căn hộ
            UPDATE CanHo 
            SET TrangThaiSuDung = 'Trong'
            WHERE MaCanHo = @MaCanHo;

        COMMIT TRANSACTION;

        -- Thông báo kết quả
        SELECT 
            N'Đã xóa thành công:' as ThongTin,
            @MaChuHo as MaChuHo,
            @MaCanHo as MaCanHo,
            @KieuSoHuu as KieuSoHuu,
            @SoCuDan as SoCuDanDaXoa,
            @SoHoaDon as SoHoaDonDaXoa;

        -- Hiển thị danh sách cư dân đã xóa nếu có
        IF @SoCuDan > 0
        BEGIN
            SELECT 
                N'Danh sách cư dân đã xóa:' as ThongTin,
                * 
            FROM #DeletedCuDan;
        END

        -- Hiển thị danh sách hóa đơn đã xóa nếu có
        IF @SoHoaDon > 0
        BEGIN
            SELECT 
                N'Danh sách hóa đơn đã xóa:' as ThongTin,
                * 
            FROM #DeletedHoaDon;
        END

        DROP TABLE IF EXISTS #DeletedCuDan;
        DROP TABLE IF EXISTS #DeletedHoaDon;
        RETURN 1; -- Thành công
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DROP TABLE IF EXISTS #DeletedCuDan;
        DROP TABLE IF EXISTS #DeletedHoaDon;

        DECLARE @ErrorMessage NVARCHAR(MAX) = ERROR_MESSAGE();
        THROW 50000, @ErrorMessage, 1;
        RETURN 0;
    END CATCH;
END;


drop proc sp_XoaChuHo
-----------------
CREATE OR ALTER PROCEDURE sp_XoaChuHo
   @MaChuHo INT
AS
BEGIN
   SET NOCOUNT ON;
   
   -- Khai báo biến
   DECLARE @KieuSoHuu NVARCHAR(10);
   DECLARE @MaCanHo INT;
   DECLARE @SoCuDan INT = 0;
   
   BEGIN TRY
       -- Kiểm tra tồn tại chủ hộ
       SELECT 
           @KieuSoHuu = KieuSoHuu,
           @MaCanHo = MaCanHo
       FROM ChuHo 
       WHERE MaChuHo = @MaChuHo;
       IF @MaChuHo IS NULL
           THROW 50001, N'Không tìm thấy chủ hộ', 1;
       -- Đếm số cư dân và hóa đơn trước khi xóa
       SELECT @SoCuDan = COUNT(*) FROM CuDan WHERE MaChuHo = @MaChuHo;
       BEGIN TRANSACTION;
           -- 1. Xóa chủ hộ trong bảng ChuHo
           DELETE FROM ChuHo 
           WHERE MaChuHo = @MaChuHo;
           -- 4. Xóa tất cả cư dân có mã chủ hộ vừa xóa
           DELETE FROM CuDan 
           WHERE MaChuHo = @MaChuHo;

           -- 5. Xóa chủ hộ trong bảng cư dân (nếu có)
           DELETE FROM CuDan 
           WHERE MaCuDan = @MaChuHo;

           -- 6. Cập nhật trạng thái căn hộ
           UPDATE CanHo 
           SET TrangThaiSuDung = 'Trong'
           WHERE MaCanHo = @MaCanHo;

       COMMIT TRANSACTION;

       -- Thông báo kết quả chi tiết
       SELECT 
           @MaChuHo as MaChuHo,
           @MaCanHo as MaCanHo,
           @KieuSoHuu as KieuSoHuu,
           @SoCuDan as N'Số cư dân đã xóa',
           N'Đã xóa thành công' as ThongBao;

       RETURN 1;
   END TRY
   BEGIN CATCH
       IF @@TRANCOUNT > 0
           ROLLBACK TRANSACTION;
       DECLARE @ErrorMessage NVARCHAR(MAX) = ERROR_MESSAGE();
       THROW 50000, @ErrorMessage, 1;
       RETURN 0;
   END CATCH;
END;
go
------------------
CREATE FUNCTION dbo.fn_DanhSachCuDanTrongKhuCH
(
    @MaKhuCanHo INT = NULL    -- Mã khu căn hộ (có thể NULL nếu muốn lấy danh sách cho tất cả các khu căn hộ)
)
RETURNS TABLE
AS
RETURN
(
    -- Tạo bảng trả về
    SELECT 
        c.MaCuDan,
        c.TenCuDan,
        c.SDT,
        c.CCCD,
        CASE 
            WHEN c.GioiTinh = 1 THEN N'Nam'
            WHEN c.GioiTinh = 0 THEN N'Nữ'
            ELSE N'Không xác định'
        END AS GioiTinh,
        ch.MaChuHo,
        chh.MaCanHo,
        chh.MaKhuCanHo,
        chuho.TenCuDan AS TenChuHo,
        kh.TenKhuCanHo
    FROM 
        CuDan c
    INNER JOIN 
        ChuHo ch ON c.MaChuHo = ch.MaChuHo  -- Liên kết với bảng ChuHo để lấy thông tin chủ hộ
    INNER JOIN 
        CanHo chh ON ch.MaCanHo = chh.MaCanHo  -- Liên kết với bảng CanHo để lấy thông tin căn hộ
    INNER JOIN 
        KhuCanHo kh ON chh.MaKhuCanHo = kh.MaKhuCanHo  -- Liên kết với bảng KhuCanHo để lấy thông tin khu căn hộ
    LEFT JOIN 
        CuDan chuho ON ch.MaChuHo = chuho.MaCuDan  -- Liên kết với bảng CuDan để lấy tên chủ hộ
    WHERE 
        (@MaKhuCanHo IS NULL OR chh.MaKhuCanHo = @MaKhuCanHo)
);



CREATE FUNCTION [dbo].[TimKiemCuDanTheoTen]
(
    @TenCuDan NVARCHAR(100)
)
RETURNS @Result TABLE (
    MaCuDan INT,
    TenCuDan NVARCHAR(100),
    SDT VARCHAR(10),
    CCCD VARCHAR(12),
    GioiTinh NVARCHAR(10),  -- Thay đổi kiểu của cột GioiTinh thành NVARCHAR
    MaChuHo INT
)
AS
BEGIN
    -- Tìm kiếm gần đúng bằng LIKE và gán giới tính Nam/Nữ
    INSERT INTO @Result
    SELECT 
        MaCuDan, 
        TenCuDan, 
        SDT, 
        CCCD, 
        CASE 
            WHEN GioiTinh = 1 THEN N'Nam'  -- Nếu GioiTinh = 1 thì là Nam
            WHEN GioiTinh = 0 THEN N'Nữ'  -- Nếu GioiTinh = 0 thì là Nữ
            ELSE N'Không xác định'        -- Nếu GioiTinh không phải 0 hoặc 1 thì là Không xác định
        END AS GioiTinh, 
        MaChuHo
    FROM CuDan
    WHERE TenCuDan LIKE '%' + @TenCuDan + '%';  -- Sử dụng LIKE để tìm kiếm gần đúng

    RETURN;
END;


---------------
CREATE PROCEDURE sp_BaoCaoDoanhThuTienIch
    @Thang INT = NULL,
    @Nam INT = NULL
AS
BEGIN
    -- Nếu không truyền tham số thì lấy tháng hiện tại
    SET @Thang = ISNULL(@Thang, MONTH(GETDATE()));
    SET @Nam = ISNULL(@Nam, YEAR(GETDATE()));

    WITH DoanhThuChiTiet AS (
        SELECT 
            kch.MaKhuCanHo,
            kch.TenKhuCanHo,
            lch.MaLoai,
            lch.TenLoai,
            ti.MaTienIch,
            ti.TenTienIch,
            COUNT(DISTINCT hd.MaHoaDon) as SoLuotSuDung,
            SUM(cthd.SoLuong) as TongSoLuong,
            SUM(cthd.SoLuong * ti.DonGia) as DoanhThu
        FROM KhuCanHo kch
        JOIN CanHo c ON kch.MaKhuCanHo = c.MaKhuCanHo
        JOIN LoaiCanHo lch ON c.MaLoaiCanHo = lch.MaLoai
        JOIN ChuHo ch ON c.MaCanHo = ch.MaCanHo
        JOIN HoaDon hd ON ch.MaChuHo = hd.NguoiThanhToan
        JOIN ChiTietHoaDon cthd ON hd.MaHoaDon = cthd.MaHoaDon
        JOIN TienIch ti ON cthd.LoaiTienIch = ti.MaTienIch
        WHERE MONTH(hd.NgayLapHoaDon) = @Thang
        AND YEAR(hd.NgayLapHoaDon) = @Nam
        GROUP BY 
            kch.MaKhuCanHo, 
            kch.TenKhuCanHo,
            lch.MaLoai,
            lch.TenLoai,
            ti.MaTienIch,
            ti.TenTienIch
    )

    SELECT 
        dtct.TenKhuCanHo as N'Khu căn hộ',
        dtct.TenLoai as N'Loại căn hộ',
        
        -- Tổng doanh thu
        FORMAT(SUM(dtct.DoanhThu), 'N0') as N'Tổng doanh thu (VNĐ)',
        
        -- Tiện ích được sử dụng nhiều nhất
        (
            SELECT TOP 1 TenTienIch + N' (' + FORMAT(SUM(TongSoLuong), 'N0') + N' lần)'
            FROM DoanhThuChiTiet dt
            WHERE dt.MaKhuCanHo = dtct.MaKhuCanHo 
            AND dt.MaLoai = dtct.MaLoai
            GROUP BY TenTienIch, MaTienIch
            ORDER BY SUM(TongSoLuong) DESC
        ) as N'Tiện ích phổ biến nhất',
        
        -- Số lượng tiện ích đã sử dụng
        COUNT(DISTINCT dtct.MaTienIch) as N'Số tiện ích sử dụng',
        
        -- Số lượt sử dụng
        SUM(dtct.SoLuotSuDung) as N'Tổng lượt sử dụng',
        
        -- Doanh thu trung bình/căn hộ
        FORMAT(SUM(dtct.DoanhThu) / (
            SELECT COUNT(DISTINCT c.MaCanHo)
            FROM CanHo c 
            WHERE c.MaKhuCanHo = dtct.MaKhuCanHo
            AND c.MaLoaiCanHo = dtct.MaLoai
        ), 'N0') as N'Doanh thu TB/căn (VNĐ)',
        
        -- So sánh với tháng trước
        FORMAT((SUM(dtct.DoanhThu) - (
            SELECT ISNULL(SUM(cthd2.SoLuong * ti2.DonGia), 0)
            FROM HoaDon hd2
            JOIN ChiTietHoaDon cthd2 ON hd2.MaHoaDon = cthd2.MaHoaDon
            JOIN TienIch ti2 ON cthd2.LoaiTienIch = ti2.MaTienIch
            JOIN ChuHo ch2 ON hd2.NguoiThanhToan = ch2.MaChuHo
            JOIN CanHo c2 ON ch2.MaCanHo = c2.MaCanHo
            WHERE c2.MaKhuCanHo = dtct.MaKhuCanHo
            AND c2.MaLoaiCanHo = dtct.MaLoai
			AND MONTH(hd2.NgayLapHoaDon) = @Thang - 1
            AND YEAR(hd2.NgayLapHoaDon) = @Nam
        )) / 1000000.0, 'N1') as N'Chênh lệch với tháng trước (triệu)',
        
        -- Trạng thái
        CASE 
            WHEN SUM(dtct.DoanhThu) > (
                SELECT ISNULL(SUM(cthd2.SoLuong * ti2.DonGia), 0)
                FROM HoaDon hd2
                JOIN ChiTietHoaDon cthd2 ON hd2.MaHoaDon = cthd2.MaHoaDon
                JOIN TienIch ti2 ON cthd2.LoaiTienIch = ti2.MaTienIch
                JOIN ChuHo ch2 ON hd2.NguoiThanhToan = ch2.MaChuHo
                JOIN CanHo c2 ON ch2.MaCanHo = c2.MaCanHo
                WHERE c2.MaKhuCanHo = dtct.MaKhuCanHo
                AND c2.MaLoaiCanHo = dtct.MaLoai
                AND MONTH(hd2.NgayLapHoaDon) = @Thang - 1
                AND YEAR(hd2.NgayLapHoaDon) = @Nam
            ) THEN N'Tăng'
            ELSE N'Giảm'
        END as N'Xu hướng'

    FROM DoanhThuChiTiet dtct
    GROUP BY 
        dtct.MaKhuCanHo,
        dtct.TenKhuCanHo,
        dtct.MaLoai,
        dtct.TenLoai
    ORDER BY 
        dtct.TenKhuCanHo,
        SUM(dtct.DoanhThu) DESC
END

exec sp_BaoCaoDoanhThuTienIch 
--------------------
CREATE PROCEDURE sp_ThongKeTienIch
    @Thang INT = NULL,
    @Nam INT = NULL
AS
BEGIN
    -- Nếu không truyền tham số thì lấy tháng hiện tại
    SET @Thang = ISNULL(@Thang, MONTH(GETDATE()));
    SET @Nam = ISNULL(@Nam, YEAR(GETDATE()));

    WITH ThongKeCoBan AS (
        SELECT 
            ti.MaTienIch,
            ti.TenTienIch,
            ti.DonGia,
            COUNT(DISTINCT hd.MaHoaDon) as SoLuotSuDung,
            COUNT(DISTINCT ch.MaCanHo) as SoCanHoSuDung,
            SUM(cthd.SoLuong) as TongSoLuong,
            SUM(cthd.SoLuong * ti.DonGia) as DoanhThu
        FROM TienIch ti
        LEFT JOIN ChiTietHoaDon cthd ON ti.MaTienIch = cthd.LoaiTienIch
        LEFT JOIN HoaDon hd ON cthd.MaHoaDon = hd.MaHoaDon 
                           AND MONTH(hd.NgayLapHoaDon) = @Thang 
                           AND YEAR(hd.NgayLapHoaDon) = @Nam
        LEFT JOIN ChuHo ch ON hd.NguoiThanhToan = ch.MaChuHo
        GROUP BY ti.MaTienIch, ti.TenTienIch, ti.DonGia
    ),
    -- Tính tổng số căn hộ
    TongCanHo AS (
        SELECT COUNT(*) as SoLuong FROM CanHo
    )

    SELECT 
        -- Thông tin cơ bản
        tk.TenTienIch as N'Tên tiện ích',
        FORMAT(tk.DonGia, 'N0') as N'Đơn giá (VNĐ)',
        
        -- Thống kê sử dụng
        tk.SoLuotSuDung as N'Số lượt sử dụng',
        tk.TongSoLuong as N'Tổng số lượng',
        tk.SoCanHoSuDung as N'Số căn hộ sử dụng',
        
        -- Tỷ lệ sử dụng
        CAST(ROUND(tk.SoCanHoSuDung * 100.0 / tch.SoLuong, 1) as DECIMAL(5,1)) 
            as N'Tỷ lệ sử dụng (%)',
        
        -- Doanh thu
        FORMAT(tk.DoanhThu, 'N0') as N'Doanh thu (VNĐ)',
        
        -- Mức độ sử dụng trung bình
        CAST(ROUND(tk.TongSoLuong * 1.0 / 
            NULLIF(tk.SoCanHoSuDung, 0), 1) as DECIMAL(5,1)) 
            as N'Số lần/căn hộ',
        
        -- So sánh với tháng trước
        FORMAT((tk.DoanhThu - (
            SELECT ISNULL(SUM(cthd2.SoLuong * ti2.DonGia), 0)
            FROM ChiTietHoaDon cthd2
            JOIN HoaDon hd2 ON cthd2.MaHoaDon = hd2.MaHoaDon
            JOIN TienIch ti2 ON cthd2.LoaiTienIch = ti2.MaTienIch
            WHERE ti2.MaTienIch = tk.MaTienIch
            AND MONTH(hd2.NgayLapHoaDon) = @Thang - 1
            AND YEAR(hd2.NgayLapHoaDon) = @Nam
        )) / 1000000.0, 'N1') as N'Chênh lệch DT (triệu)',
        
        -- Xếp hạng theo doanh thu
        DENSE_RANK() OVER (ORDER BY tk.DoanhThu DESC) as N'Xếp hạng DT',
        
        -- Xếp hạng theo số lượt sử dụng
        DENSE_RANK() OVER (ORDER BY tk.SoCanHoSuDung DESC) as N'Xếp hạng phổ biến',

        -- Đánh giá
        CASE 
            WHEN tk.SoCanHoSuDung * 100.0 / tch.SoLuong >= 70 THEN N'Rất phổ biến'
            WHEN tk.SoCanHoSuDung * 100.0 / tch.SoLuong >= 40 THEN N'Phổ biến'
			WHEN tk.SoCanHoSuDung * 100.0 / tch.SoLuong >= 20 THEN N'Trung bình'
            ELSE N'Ít sử dụng'
        END as N'Đánh giá'

    FROM ThongKeCoBan tk
    CROSS JOIN TongCanHo tch
    ORDER BY 
        tk.DoanhThu DESC,
        tk.SoCanHoSuDung DESC;
END

drop proc sp_ThongKeTienIch
drop proc sp_BaoCaoDoanhThuTienIch


CREATE FUNCTION fn_BaoCaoDoanhThuTienIch
(
    @Thang INT,
    @Nam INT
)
RETURNS TABLE
AS
RETURN
(
    WITH DoanhThuChiTiet AS (
        SELECT 
            kch.MaKhuCanHo,
            kch.TenKhuCanHo,
            lch.MaLoai,
            lch.TenLoai,
            ti.MaTienIch,
            ti.TenTienIch,
            COUNT(DISTINCT hd.MaHoaDon) as SoLuotSuDung,
            SUM(cthd.SoLuong) as TongSoLuong,
            SUM(cthd.SoLuong * ti.DonGia) as DoanhThu
        FROM KhuCanHo kch
        JOIN CanHo c ON kch.MaKhuCanHo = c.MaKhuCanHo
        JOIN LoaiCanHo lch ON c.MaLoaiCanHo = lch.MaLoai
        JOIN ChuHo ch ON c.MaCanHo = ch.MaCanHo
        JOIN HoaDon hd ON ch.MaChuHo = hd.NguoiThanhToan
        JOIN ChiTietHoaDon cthd ON hd.MaHoaDon = cthd.MaHoaDon
        JOIN TienIch ti ON cthd.LoaiTienIch = ti.MaTienIch
        WHERE MONTH(hd.NgayLapHoaDon) = @Thang
        AND YEAR(hd.NgayLapHoaDon) = @Nam
        GROUP BY 
            kch.MaKhuCanHo, 
            kch.TenKhuCanHo,
            lch.MaLoai,
            lch.TenLoai,
            ti.MaTienIch,
            ti.TenTienIch
    )

    SELECT 
        dtct.TenKhuCanHo as [Khu căn hộ],
        dtct.TenLoai as [Loại căn hộ],
        
        -- Tổng doanh thu
        FORMAT(SUM(dtct.DoanhThu), 'N0') as [Tổng doanh thu (VNĐ)],
        
        -- Tiện ích được sử dụng nhiều nhất
        (
            SELECT TOP 1 TenTienIch + N' (' + FORMAT(SUM(TongSoLuong), 'N0') + N' lần)'
            FROM DoanhThuChiTiet dt
            WHERE dt.MaKhuCanHo = dtct.MaKhuCanHo 
            AND dt.MaLoai = dtct.MaLoai
            GROUP BY TenTienIch, MaTienIch
            ORDER BY SUM(TongSoLuong) DESC
        ) as [Tiện ích phổ biến nhất],
        
        -- Số lượng tiện ích đã sử dụng
        COUNT(DISTINCT dtct.MaTienIch) as [Số tiện ích sử dụng],
        
        -- Số lượt sử dụng
        SUM(dtct.SoLuotSuDung) as [Tổng lượt sử dụng],
        
        -- Doanh thu trung bình/căn hộ
        FORMAT(SUM(dtct.DoanhThu) / (
            SELECT COUNT(DISTINCT c.MaCanHo)
            FROM CanHo c 
            WHERE c.MaKhuCanHo = dtct.MaKhuCanHo
            AND c.MaLoaiCanHo = dtct.MaLoai
        ), 'N0') as [Doanh thu TB/căn (VNĐ)],
        
        -- So sánh với tháng trước
        FORMAT((SUM(dtct.DoanhThu) - (
            SELECT ISNULL(SUM(cthd2.SoLuong * ti2.DonGia), 0)
            FROM HoaDon hd2
            JOIN ChiTietHoaDon cthd2 ON hd2.MaHoaDon = cthd2.MaHoaDon
            JOIN TienIch ti2 ON cthd2.LoaiTienIch = ti2.MaTienIch
            JOIN ChuHo ch2 ON hd2.NguoiThanhToan = ch2.MaChuHo
            JOIN CanHo c2 ON ch2.MaCanHo = c2.MaCanHo
            WHERE c2.MaKhuCanHo = dtct.MaKhuCanHo
            AND c2.MaLoaiCanHo = dtct.MaLoai
            AND MONTH(hd2.NgayLapHoaDon) = @Thang - 1
            AND YEAR(hd2.NgayLapHoaDon) = @Nam
        )) / 1000000.0, 'N1') as [Chênh lệch với tháng trước (triệu)],
		-- Trạng thái
        CASE 
            WHEN SUM(dtct.DoanhThu) > (
                SELECT ISNULL(SUM(cthd2.SoLuong * ti2.DonGia), 0)
                FROM HoaDon hd2
                JOIN ChiTietHoaDon cthd2 ON hd2.MaHoaDon = cthd2.MaHoaDon
                JOIN TienIch ti2 ON cthd2.LoaiTienIch = ti2.MaTienIch
                JOIN ChuHo ch2 ON hd2.NguoiThanhToan = ch2.MaChuHo
                JOIN CanHo c2 ON ch2.MaCanHo = c2.MaCanHo
                WHERE c2.MaKhuCanHo = dtct.MaKhuCanHo
                AND c2.MaLoaiCanHo = dtct.MaLoai
                AND MONTH(hd2.NgayLapHoaDon) = @Thang - 1
                AND YEAR(hd2.NgayLapHoaDon) = @Nam
            ) THEN N'Tăng'
            ELSE N'Giảm'
        END as [Xu hướng]

    FROM DoanhThuChiTiet dtct
    GROUP BY 
        dtct.MaKhuCanHo,
        dtct.TenKhuCanHo,
        dtct.MaLoai,
        dtct.TenLoai
)

SELECT * FROM fn_BaoCaoDoanhThuTienIch(MONTH(GETDATE()), YEAR(GETDATE()))
ORDER BY [Khu căn hộ], [Tổng doanh thu (VNĐ)] DESC

-------------------
CREATE OR ALTER FUNCTION fn_ThongKeDanCu()
RETURNS @ThongKeTable TABLE (
    TenKhuCanHo NVARCHAR(255),
    TongCanHo INT,
    CanHoTrong INT,
    TongCuDan INT,
    MatDo DECIMAL(10, 1),
    SoNam INT,
    SoNu INT,
    HoDenMoi INT,
    HoChuyenDi INT,
    CanHoMatDoCao INT,
    TyLeLapDay DECIMAL(10, 1),
    TrangThaiDanCu NVARCHAR(50)
)
AS
BEGIN
    -- Biến động 6 tháng gần nhất
    WITH BienDong AS (
        SELECT 
            kch.MaKhuCanHo,
            COUNT(CASE WHEN ch.NgayBatDau >= DATEADD(MONTH, -6, GETDATE()) THEN 1 END) as HoDenMoi,
            COUNT(CASE WHEN ch.NgayKetThuc >= DATEADD(MONTH, -6, GETDATE()) THEN 1 END) as HoChuyenDi
        FROM KhuCanHo kch
        LEFT JOIN CanHo c ON kch.MaKhuCanHo = c.MaKhuCanHo
        LEFT JOIN ChuHo ch ON c.MaCanHo = ch.MaCanHo
        GROUP BY kch.MaKhuCanHo
    ),
    -- Đếm số người trong mỗi căn hộ
    MatDoCao AS (
        SELECT 
            ch.MaCanHo,
            COUNT(cd.MaCuDan) as SoNguoi
        FROM ChuHo ch
        JOIN CuDan cd ON ch.MaChuHo = cd.MaChuHo
        GROUP BY ch.MaCanHo
        HAVING COUNT(cd.MaCuDan) > 4
    )

    -- Chèn dữ liệu vào bảng trả về
    INSERT INTO @ThongKeTable
    SELECT 
        kch.TenKhuCanHo as TenKhuCanHo,
        COUNT(DISTINCT c.MaCanHo) as TongCanHo,
        SUM(CASE WHEN c.TrangThaiSuDung = N'Trong' THEN 1 ELSE 0 END) as CanHoTrong,
        COUNT(DISTINCT cd.MaCuDan) as TongCuDan,
        CAST(COUNT(DISTINCT cd.MaCuDan) * 1.0 / NULLIF(COUNT(DISTINCT c.MaCanHo), 0) as DECIMAL(10,1)) as MatDo,
        SUM(CASE WHEN cd.GioiTinh = 1 THEN 1 ELSE 0 END) as SoNam,
        SUM(CASE WHEN cd.GioiTinh = 0 THEN 1 ELSE 0 END) as SoNu,
        bd.HoDenMoi as HoDenMoi,
        bd.HoChuyenDi as HoChuyenDi,
        COUNT(DISTINCT mdc.MaCanHo) as CanHoMatDoCao,
        CAST((COUNT(DISTINCT c.MaCanHo) - SUM(CASE WHEN c.TrangThaiSuDung = N'Trong' THEN 1 ELSE 0 END)) * 100.0 / 
            NULLIF(COUNT(DISTINCT c.MaCanHo), 0) as DECIMAL(10,1)) as TyLeLapDay,
        CASE 
            WHEN bd.HoDenMoi > bd.HoChuyenDi THEN N'Đang tăng dân'
            WHEN bd.HoDenMoi < bd.HoChuyenDi THEN N'Đang giảm dân'
            ELSE N'Ổn định'
        END as TrangThaiDanCu
    FROM KhuCanHo kch
    LEFT JOIN CanHo c ON kch.MaKhuCanHo = c.MaKhuCanHo
    LEFT JOIN ChuHo ch ON c.MaCanHo = ch.MaCanHo
    LEFT JOIN CuDan cd ON ch.MaChuHo = cd.MaChuHo
    LEFT JOIN BienDong bd ON kch.MaKhuCanHo = bd.MaKhuCanHo
    LEFT JOIN MatDoCao mdc ON c.MaCanHo = mdc.MaCanHo
    GROUP BY 
        kch.TenKhuCanHo,
        bd.HoDenMoi,
        bd.HoChuyenDi;

    RETURN;
END;

SELECT * FROM fn_ThongKeDanCu();