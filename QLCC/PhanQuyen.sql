
use master
go
create login admin_login with password = '123';
create login staff_login with password = '123';
use DADBMS;
CREATE ROLE ADMINS;
CREATE ROLE STAFFS;
create user admin_user for login admin_login;
create user staff_user for login staff_login;


--add user dô role
EXEC sp_addrolemember 'ADMINS', admin_user;
EXEC sp_addrolemember 'STAFFS', staff_user;

--control là toàn quyền DB
GRANT CONTROL ON DATABASE::DADBMS TO ADMINS
GRANT EXECUTE ON SCHEMA::dbo TO STAFFS;
GRANT SELECT ON SCHEMA::dbo TO STAFFS;
GO

use DADBMS
DENY SELECT ON HoaDon TO STAFFS
DENY SELECT ON OBJECT::dbo.fn_TimKiemHoaDonTheoThangNam TO STAFFS;
Deny EXec on Object::dbo.sp_TaoHoaDon to STAFFS;
DENY EXec on Object::dbo.sp_TaoChiTietHoaDon to STAFFS;

CREATE ROLE STAFF
Grant execute on schema::dbo to STAFF;
Grant select on schema::dbo to STAFF;
go
Deny Select on HoaDon to STAFF
Deny select on object::dbo.fn_TimKiemHoaDonTheoThangNam to STAFF
DENY INSERT ON Users TO STAFF;
DENY 

create table TaiKhoan(
	id int identity primary key,
	username varchar(50) unique not null,
	pass varchar(50) not null,
	role varchar(50)
);

create login staff_login1 with password = '123'
create user staff_user1 for login staff_login1;
EXEC sp_addrolemember 'STAFF', staff_user1;


DENY EXECUTE ON OBJECT::[dbo].[sp_TaoHoaDonChoChuHo] TO [STAFF];
SELECT * 
FROM sys.database_permissions p
JOIN sys.database_principals r ON p.grantee_principal_id = r.principal_id
WHERE r.name = 'STAFF' AND p.major_id = OBJECT_ID('dbo.sp_TaoChiTietHoaDon');

---------------------
CREATE VIEW dbo.UserView AS
SELECT user_id, password, role_id
FROM dbo.Users;

DENY SELECT ON dbo.UserView TO STAFF;