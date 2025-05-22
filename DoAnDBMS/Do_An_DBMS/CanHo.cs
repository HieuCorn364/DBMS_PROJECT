using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_An_DBMS
{
    internal class CanHo
    {
        MyDB db = new MyDB();


        public DataTable getMaKhuCanHo()
        {
            SqlCommand command = new SqlCommand("Select * from V_TimMaKhuCanHo", db.SqlCon);

            SqlDataAdapter Adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            Adapter.Fill(dataTable);
            return dataTable;
        }
        public DataTable getMaLoaiCanHo()
        {
            SqlCommand command = new SqlCommand("Select * from V_TimMaLoaiCanHo", db.SqlCon);
            SqlDataAdapter Adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            Adapter.Fill(dataTable);
            return dataTable;
        }
        public DataTable getCanHo(SqlCommand command)
        {
            command.Connection = db.SqlCon;
            db.openConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            db.closeConnection();
            return table;
        }
        public bool addcanho(string trangthai, int makhucanho, int maloaicanho)
        {
            SqlCommand command = new SqlCommand("EXEC sp_ThemCanHo @TrangThaiSuDung, @MaKhuCanHo, @MaLoaiCanHo", db.SqlCon);
            command.Parameters.Add("@TrangThaiSuDung", SqlDbType.VarChar, 100).Value = trangthai;
            command.Parameters.Add("@MaKhuCanHo", SqlDbType.Int).Value = makhucanho;
            command.Parameters.Add("@MaLoaiCanHo", SqlDbType.Int).Value = maloaicanho;
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
        public bool editcanho(int macanho, string trangthai, int makhucanho, int maloaicanho)
        {
            SqlCommand command = new SqlCommand("EXEC sp_CapNhatCanHo @MaCanHo, @TrangThaiSuDung, @MaKhuCanHo, @MaLoaiCanHo", db.SqlCon);
            command.Parameters.Add("@MaCanHo", SqlDbType.Int).Value = macanho;
            command.Parameters.Add("@TrangThaiSuDung", SqlDbType.VarChar, 100).Value = trangthai;
            command.Parameters.Add("@MaKhuCanHo", SqlDbType.Int).Value = makhucanho;
            command.Parameters.Add("@MaLoaiCanHo", SqlDbType.Int).Value = maloaicanho;
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
        public bool deletecanho(int macanho)
        {
            SqlCommand command = new SqlCommand("EXEC sp_XoaCanHo @MaCanHo", db.SqlCon);
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
        public DataTable getKhuCanHo(SqlCommand command)
        {
            command.Connection = db.SqlCon;
            db.openConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            db.closeConnection();
            return table;
        }
    }
}
