using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_An_DBMS
{
    internal class CuDan
    {
        MyDB db = new MyDB();
        public bool addcudan(string hoten, string sdt, string cccd, bool gioitinh, int? machuho)
        {
            SqlCommand command = new SqlCommand("EXEC sp_ThemCuDan @TenCuDan, @SDT, @CCCD, @GioiTinh, @MaChuHo", db.SqlCon);
            command.Parameters.Add("@TenCuDan", SqlDbType.NVarChar, 100).Value = hoten;
            command.Parameters.Add("@SDT", SqlDbType.VarChar, 10).Value = sdt;
            command.Parameters.Add("@CCCD", SqlDbType.VarChar, 12).Value = cccd;
            command.Parameters.Add("@GioiTinh", SqlDbType.Bit).Value = gioitinh;
            if (machuho.HasValue)
            {
                command.Parameters.Add("@MaChuHo", SqlDbType.Int).Value = machuho.Value;
            }
            else
            {
                command.Parameters.Add("@MaChuHo", SqlDbType.Int).Value = DBNull.Value;
            }
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

        public DataTable getBienDongCuDan(SqlCommand command)
        {
            command.Connection = db.SqlCon;
            db.closeConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            db.closeConnection();
            return table;
        }
        public bool editcudan(int macudan, string hoten, string sdt, string cccd, bool gioitinh, int machuho)
        {
            SqlCommand command = new SqlCommand("EXEC sp_CapNhatCuDan @MaCuDan ,@TenCuDan, @SDT, @CCCD, @GioiTinh, @MaChuHo", db.SqlCon);
            command.Parameters.Add("@MaCuDan", SqlDbType.Int).Value = macudan;
            command.Parameters.Add("@TenCuDan", SqlDbType.NVarChar, 100).Value = hoten;
            command.Parameters.Add("@SDT", SqlDbType.VarChar, 10).Value = sdt;
            command.Parameters.Add("@CCCD", SqlDbType.VarChar, 12).Value = cccd;
            command.Parameters.Add("@GioiTinh", SqlDbType.Bit).Value = gioitinh;
            command.Parameters.Add("@MaChuHo", SqlDbType.Int).Value = machuho;
            db.openConnection();
            if ((command.ExecuteNonQuery() != 0))
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
        public bool deletecudan(int macudan)
        {
            SqlCommand command = new SqlCommand("EXEC sp_XoaCuDan @MaCuDan", db.SqlCon);
            command.Parameters.Add("@MaCuDan", SqlDbType.Int).Value = macudan;
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
        public DataTable getCuDan(SqlCommand command)
        {
            command.Connection = db.SqlCon;
            db.closeConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            db.closeConnection();
            return table;
        }

        public DataTable findCuDan(String TenCuDan)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemCuDanTheoTen(@TenCuDan)", db.SqlCon);
            command.Parameters.Add("@TenCuDan", SqlDbType.VarChar).Value = TenCuDan;
            db.openConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            db.closeConnection();
            return dataTable;
        }

    }
}
