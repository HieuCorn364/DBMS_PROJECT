using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_An_DBMS
{
    internal class KhuCanHo
    {
        MyDB mydb = new MyDB();

        public DataTable GetDanhSachCuDan(int maKhuCanHo)
        {
            SqlCommand command = new SqlCommand("Select * from dbo.fn_DanhSachCuDanTrongKhuCH(@MaKhuCanHo)", mydb.SqlCon);
            command.Parameters.Add("@MaKhuCanHo", SqlDbType.Int).Value = maKhuCanHo;
            SqlDataAdapter Adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            Adapter.Fill(dataTable);
            return dataTable;
        }
    }
}
