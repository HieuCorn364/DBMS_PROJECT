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

    public partial class DanhSachKhuCanHo : Form
    {
        MyDB db = new MyDB();
        CanHo CanHo = new CanHo();
        public DanhSachKhuCanHo()
        {
            InitializeComponent();
            loaddata();
        }
        void loaddata()
        {
            SqlCommand command = new SqlCommand("Select * from KhuCanHo");
            data_KhuCanHo.RowTemplate.Height = 30;
            data_KhuCanHo.DataSource = CanHo.getKhuCanHo(command);
            data_KhuCanHo.AllowUserToAddRows = false;
        }

        private void DanhSachKhuCanHo_Load(object sender, EventArgs e)
        {
            loaddata();
        }

        private void data_KhuCanHo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            staticdata.makhucanho = int.Parse(data_KhuCanHo.CurrentRow.Cells[0].Value.ToString());
        }

        private void data_KhuCanHo_DoubleClick(object sender, EventArgs e)
        {
            staticdata.makhucanho = int.Parse(data_KhuCanHo.CurrentRow.Cells[0].Value.ToString());
            DSCuDanInKCH dSCuDanInKCH = new DSCuDanInKCH();
            this.Hide();
            dSCuDanInKCH.Show();
            dSCuDanInKCH.FormClosed += (s, args) =>
            {
                this.Show(); 
            };
        }
    }
}
