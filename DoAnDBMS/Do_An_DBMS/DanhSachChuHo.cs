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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Do_An_DBMS
{
    public partial class DanhSachChuHo : Form
    {
        MyDB db = new MyDB();
        ChuHo chuho = new ChuHo();
        //Singleton pattern
        private static DanhSachChuHo instance;
        public static DanhSachChuHo Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new DanhSachChuHo();
                }
                return instance;
            }
        }
        private DanhSachChuHo()
        {
            InitializeComponent();
        }
        void load_datachuho()
        {
            SqlCommand command = new SqlCommand("Select * from view_danhsachchuho");
            data_ChuHo.RowTemplate.Height = 30;
            data_ChuHo.DataSource = chuho.getChuHo(command);
            data_ChuHo.AllowUserToAddRows = false;
        }
        private void DanhSachChuHo_Load(object sender, EventArgs e)
        {
            load_datachuho();
        }

        private void btb_reloaddata_Click(object sender, EventArgs e)
        {
            load_datachuho();
        }

        private void btn_ThemChuHo_Click(object sender, EventArgs e)
        {
            ThemChuHo themchuho = ThemChuHo.Instance;
            this.Hide();
            themchuho.Show();
            themchuho.FormClosed += (s, args) => {
                this.Show();
            };
        }

        private void data_ChuHo_DoubleClick(object sender, EventArgs e)
        {
            ChinhSuaChuHo chinhsuachuho = ChinhSuaChuHo.Instance;
            chinhsuachuho.txt_machuho.Text = data_ChuHo.CurrentRow.Cells[0].Value.ToString();
            staticdata.machuho = int.Parse(data_ChuHo.CurrentRow.Cells[0].Value.ToString());
            chinhsuachuho.ngaybatdau.Value = (DateTime)data_ChuHo.CurrentRow.Cells[1].Value;
            chinhsuachuho.ngayketthuc.Value = (DateTime)data_ChuHo.CurrentRow.Cells[2].Value;
            chinhsuachuho.txt_kieusohuu.Text = data_ChuHo.CurrentRow.Cells[3].Value.ToString();
            //chinhsuachuho.cbb_macanho.Text = data_ChuHo.CurrentRow.Cells[4].Value.ToString();
            staticdata.macanho = int.Parse(data_ChuHo.CurrentRow.Cells[4].Value.ToString());
            this.Hide();

            chinhsuachuho.Show();

            chinhsuachuho.FormClosed += (s, args) =>
            {
                this.Show();  // Hiển thị lại form gốc khi form mới bị đóng
            };
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_xoachuho_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(data_ChuHo.CurrentRow.Cells[0].Value.ToString());
            if ((MessageBox.Show("Bạn có chắc muốn xóa chủ hộ này ra khỏi danh sách không", "Xóa Chủ Hộ", MessageBoxButtons.YesNo
                    , MessageBoxIcon.Question) == DialogResult.Yes))
            {
                chuho.deletechuho(ma);
                load_datachuho();
            }
        }

        private void data_ChuHo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            staticdata.machuho = int.Parse(data_ChuHo.CurrentRow.Cells[0].Value.ToString());
        }
    }
}
