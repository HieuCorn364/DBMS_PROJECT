using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_An_DBMS
{
    internal class ChuHo
    {
        MyDB db = new MyDB();
        public DataTable getcanhotrong()
        {
            SqlCommand command = new SqlCommand("Select * from dbo.GetCanHoTrong()", db.SqlCon);
            SqlDataAdapter Adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            Adapter.Fill(dataTable);
            return dataTable;
        }
        public bool addchuho(int machuho, string ngaybatdau, string ngayketthuc, string kieusohuu, int macanho)
        {
            SqlCommand command = new SqlCommand("EXEC sp_ThemChuHo @MaChuHo, @NgayBatDau, @NgayKetThuc, @KieuSoHuu, @MaCanHo", db.SqlCon);
            command.Parameters.Add("@MaChuHo", SqlDbType.Int).Value = machuho;
            command.Parameters.Add("@NgayBatDau", SqlDbType.Date).Value = ngaybatdau;
            command.Parameters.Add("@NgayKetThuc", SqlDbType.Date).Value = ngayketthuc;
            command.Parameters.Add("@KieuSoHuu", SqlDbType.VarChar, 50).Value = kieusohuu;
            command.Parameters.Add("@MaCanHo", SqlDbType.Int).Value = macanho;
            db.openConnection();
            if (command.ExecuteNonQuery() != 0)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }
        public bool chinhsuachuho(int machuho, string ngaybatdau, string ngayketthuc, string kieusohuu, int macanho)
        {
            SqlCommand command = new SqlCommand("EXEC sp_CapNhatChuHo @MaChuHo, @NgayBatDau, @NgayKetThuc, @KieuSoHuu, @MaCanHo", db.SqlCon);
            command.Parameters.Add("@MaChuHo", SqlDbType.Int).Value = machuho;
            command.Parameters.Add("@NgayBatDau", SqlDbType.Date).Value = ngaybatdau;
            command.Parameters.Add("@NgayKetThuc", SqlDbType.Date).Value = ngayketthuc;
            command.Parameters.Add("@KieuSoHuu", SqlDbType.VarChar, 50).Value = kieusohuu;
            command.Parameters.Add("@MaCanHo", SqlDbType.Int).Value = macanho;
            db.openConnection();
            if ((command.ExecuteNonQuery() == 1))
            {
                db.closeConnection();

                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }
        public DataTable getChuHo(SqlCommand command)
        {
            command.Connection = db.SqlCon;
            db.closeConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            db.closeConnection();
            return table;
        }
        public bool deletechuho(int ma)
        {
            SqlCommand command = new SqlCommand("EXEC sp_XoaChuHo @MaChuHo", db.SqlCon);
            command.Parameters.Add("@MaChuHo", SqlDbType.Int).Value = ma;
            db.openConnection();
            if (command.ExecuteNonQuery() != 0)
            {
                db.closeConnection();

                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }

        }
    }
}
