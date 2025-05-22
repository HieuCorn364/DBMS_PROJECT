using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_An_DBMS
{
    public partial class FormThongKe : Form
    {
        CuDan cuDan = new CuDan();
        //Singleton pattern
        private static FormThongKe instance;
        public static FormThongKe Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new FormThongKe();
                }
                return instance;
            }
        }
        MyDB mydb = new MyDB();
        private FormThongKe()
        {
            InitializeComponent();
        }

        public void loaddata()
        {
            dgvThongKe.DataSource = getData();
            dgvThongKe.Refresh();
        }

        private DataTable getData()
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand("sp_PhanTichTyLeSuDungCanHo", mydb.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;

        }

        public void loaddata1()
        {
            SqlCommand command = new SqlCommand("Select * FROM fn_ThongKeDanCu()");
            dgvBienDong.DataSource = cuDan.getBienDongCuDan(command);
            dgvBienDong.Refresh();
        }
    }
}
