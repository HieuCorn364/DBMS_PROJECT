using System;
using System.Collections;
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
    public partial class DanhSachCuDan : Form
    {
        CuDan cudan = new CuDan();
        //Singleton pattern
        private static DanhSachCuDan instance;
        public static DanhSachCuDan Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new DanhSachCuDan();
                }
                return instance;
            }
        }
        private DanhSachCuDan()
        {
            InitializeComponent();
        }

        private void btb_reloaddata_Click(object sender, EventArgs e)
        {
            loaddatacudan();
        }

        private void loaddatacudan()
        {
            SqlCommand command = new SqlCommand("Select * from view_danhsachcudan");
            data_CuDan.RowTemplate.Height = 30;
            data_CuDan.DataSource = cudan.getCuDan(command);
            data_CuDan.AllowUserToAddRows = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ThemCuDan themCuDan = ThemCuDan.Instance;
            themCuDan.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int macudan = int.Parse(data_CuDan.CurrentRow.Cells[0].Value.ToString());
            if ((MessageBox.Show("Bạn có chắc muốn xóa cư dân này ra khỏi danh sách không", "Xóa Cư Dân", MessageBoxButtons.YesNo
                    , MessageBoxIcon.Question) == DialogResult.Yes))
            {
                cudan.deletecudan(macudan);
                loaddatacudan();
            }
        }

        private void DanhSachCuDan_Load(object sender, EventArgs e)
        {
            loaddatacudan();
        }

        private void data_CuDan_DoubleClick(object sender, EventArgs e)
        {
            ChinhSuaCuDan chinhsuacudan = ChinhSuaCuDan.Instance;
            staticdata.Macudan = int.Parse(data_CuDan.CurrentRow.Cells[0].Value.ToString());
            chinhsuacudan.txt_Hoten.Text = data_CuDan.CurrentRow.Cells[1].Value.ToString();
            chinhsuacudan.txt_Sdt.Text = data_CuDan.CurrentRow.Cells[2].Value.ToString();
            chinhsuacudan.txt_CCCD.Text = data_CuDan.CurrentRow.Cells[3].Value.ToString();
            String gioitinh = data_CuDan.CurrentRow.Cells[4].Value.ToString();
            if (gioitinh == "Nam")
            {
                chinhsuacudan.rbtn_nam.Checked = true;
                chinhsuacudan.rbtn_nu.Checked = false;
            }
            if (gioitinh == "Nu")
            {
                chinhsuacudan.rbtn_nam.Checked = false;
                chinhsuacudan.rbtn_nu.Checked = true;
            }
            chinhsuacudan.txt_MaChuHo.Text = data_CuDan.CurrentRow.Cells[5].Value.ToString();
            this.Hide();

            chinhsuacudan.Show();

            chinhsuacudan.FormClosed += (s, args) =>
            {
                this.Show();  // Hiển thị lại form gốc khi form mới bị đóng
            };
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            data_CuDan.DataSource = cudan.findCuDan(txtTim.Text);
            data_CuDan.AllowUserToAddRows = false;
        }
    }
}
