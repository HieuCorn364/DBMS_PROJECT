USE QuanLyChungCu;  
GO

CREATE TRIGGER trg_UpdateTrangThaiCanHo
ON ChuHo
AFTER INSERT
AS
BEGIN
	DECLARE @type VARCHAR(50); 
	SELECT @type = CD.KieuSoHuu
    FROM ChuHo CH
    JOIN inserted CD ON CD.MaChuHo = CH.MaChuHo;

    UPDATE CanHo
    SET TrangThaiSuDung = 'Dang su dung', KieuSoHuu = @type
    FROM CanHo c
    INNER JOIN Inserted i ON c.MaCanHo = i.MaCanHo;
END;
GO

CREATE TRIGGER trg_UpdateCanHo_AfterDeleteChuHo
ON ChuHo
AFTER DELETE
AS
BEGIN
    UPDATE CanHo
    SET TrangThaiSuDung = 'Trong', 
        KieuSoHuu = 'Khong'
    WHERE MaCanHo IN (SELECT MaCanHo FROM deleted);
END;
GO

CREATE TRIGGER trg_CapNhatTongTien
ON ChiTietHoaDon
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        UPDATE HoaDon
        SET TongTien = (
            SELECT SUM(cthd.GiaTien)
            FROM ChiTietHoaDon cthd
            WHERE cthd.MaHoaDon = HoaDon.MaHoaDon
        )
        FROM HoaDon
        INNER JOIN inserted i ON HoaDon.MaHoaDon = i.MaHoaDon;
    END

    IF EXISTS (SELECT 1 FROM deleted)
    BEGIN
        UPDATE HoaDon
        SET TongTien = (
            SELECT SUM(cthd.GiaTien)
            FROM ChiTietHoaDon cthd
            WHERE cthd.MaHoaDon = HoaDon.MaHoaDon
        )
        FROM HoaDon
        INNER JOIN deleted d ON HoaDon.MaHoaDon = d.MaHoaDon;
    END
END;
GO

CREATE TRIGGER trg_CheckCuDanData
ON CuDan
BEFORE INSERT, UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1 FROM Inserted WHERE LEN(SDT) != 10 OR SDT LIKE '%[^0-9]%'
    )
    BEGIN
        RAISERROR ('Số điện thoại không hợp lệ, phải gồm 10 số.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;

    IF EXISTS (
        SELECT 1 FROM Inserted WHERE LEN(CCCD) != 12 OR CCCD LIKE '%[^0-9]%'
    )
    BEGIN
        RAISERROR ('CCCD không hợp lệ, phải gồm 12 số.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;
END;
GO

CREATE TRIGGER trg_UpdateSoLuongKhuCanHo
ON CanHo
AFTER INSERT, DELETE
AS
BEGIN
    UPDATE KhuCanHo
    SET SoLuong = (
        SELECT COUNT(*)
        FROM CanHo
        WHERE CanHo.MaKhuCanHo = KhuCanHo.MaKhuCanHo
    )
    FROM KhuCanHo k
    INNER JOIN Inserted i ON k.MaKhuCanHo = i.MaKhuCanHo;
END;
GO

CREATE TRIGGER trg_UpdateTrangThaiHoaDon
ON HoaDon
AFTER UPDATE
AS
BEGIN
    UPDATE HoaDon
    SET TrangThai = 'Đã thanh toán'
    WHERE TongTien = 0;
END;
GO

CREATE TRIGGER tg_TinhGiaTienChiTietHoaDon
ON ChiTietHoaDon
AFTER INSERT
AS
BEGIN
    UPDATE ChiTietHoaDon
    SET ChiTietHoaDon.GiaTien = i.SoLuong * t.DonGia
    FROM ChiTietHoaDon cthd
    INNER JOIN inserted i ON cthd.MaHoaDon = i.MaHoaDon AND cthd.LoaiTienIch = i.LoaiTienIch
    INNER JOIN TienIch t ON i.LoaiTienIch = t.MaTienIch;
END;

INSERT INTO ChiTietHoaDon (MaHoaDon, LoaiTienIch, SoLuong)
VALUES (2, 3000, 1), (2, 3001, 90), (2, 3002, 30), (2, 3003, 1), (2, 3004, 1), (2, 3005, 3), (2, 3006, 1);     

INSERT INTO ChiTietHoaDon (MaHoaDon, LoaiTienIch, SoLuong)
VALUES (3, 3000, 1), (3, 3001, 100), (3, 3002, 50), (3, 3003, 1), (3, 3004, 1), (3, 3005, 1), (3, 3006, 1);     

INSERT INTO ChiTietHoaDon (MaHoaDon, LoaiTienIch, SoLuong)
VALUES (2, 3000, 1), (2, 3001, 90), (2, 3002, 30), (2, 3003, 1), (2, 3004, 1), (2, 3005, 3), (2, 3006, 1);
GO

CREATE TRIGGER tg_KiemTraHoaDonTrung
ON HoaDon
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @NguoiThanhToan INT, @NgayLapHoaDon DATE, @Thang INT, @Nam INT;

    SELECT @NguoiThanhToan = i.NguoiThanhToan, @NgayLapHoaDon = i.NgayLapHoaDon
    FROM inserted i;

    SET @Thang = MONTH(@NgayLapHoaDon);
    SET @Nam = YEAR(@NgayLapHoaDon);

    IF NOT EXISTS (
        SELECT 1
        FROM HoaDon
        WHERE NguoiThanhToan = @NguoiThanhToan
        AND MONTH(NgayLapHoaDon) = @Thang
        AND YEAR(NgayLapHoaDon) = @Nam
    )
    BEGIN
         INSERT INTO HoaDon (TongTien, TrangThai, NgayLapHoaDon, NguoiThanhToan)
        SELECT TongTien, TrangThai, NgayLapHoaDon, NguoiThanhToan
        FROM inserted;
    END
END;
GO

CREATE TRIGGER trg_UpdateSoLuongCanHo
ON CanHo
AFTER INSERT
AS
BEGIN
    UPDATE KhuCanHo
    SET SoLuong = (SELECT COUNT(*) FROM CanHo WHERE MaKhuCanHo = inserted.MaKhuCanHo)
    FROM KhuCanHo
    INNER JOIN inserted ON KhuCanHo.MaKhuCanHo = inserted.MaKhuCanHo;
END;
GO
