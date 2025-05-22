using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Do_An_DBMS
{
    public partial class ThemChuHo : Form
    {
        ChuHo chuho = new ChuHo();
        //Singleton Pattern
        private static ThemChuHo instance;
        public static ThemChuHo Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new ThemChuHo();
                }
                return instance;
            }
        }
        private ThemChuHo()
        {
            InitializeComponent();
        }
        void load_cbbmacanho()
        {
            cbb_macanho.DataSource = chuho.getcanhotrong();
            cbb_macanho.DisplayMember = "MaCanHo";
            cbb_macanho.ValueMember = "MaCanHo";
            cbb_macanho.SelectedItem = null;
        }

        private void btn_ThemChuHo_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_machuho.Text.Trim() == "")
                {
                    throw new Exception("Không được để trống mã chủ hộ");
                }
                if (txt_kieusohuu.Text.Trim() == "")
                {
                    throw new Exception("Không được để trống kiểu sở hữu");
                }
                if (DateTime.Compare(ngaybatdau.Value, ngayketthuc.Value) >= 0)
                {
                    throw new Exception("Ngày bắt đầu không được sớm hơn ngày kết thúc");
                }
                if (cbb_macanho.Text.Trim() == "")
                {
                    throw new Exception("Không được để trống mã căn hộ");
                }
                int machuho = int.Parse(txt_machuho.Text);
                DateTime ngaystart = ngaybatdau.Value;
                DateTime ngayend = ngayketthuc.Value;
                string kieusohuu = txt_kieusohuu.Text;
                int macanho = int.Parse(cbb_macanho.SelectedValue.ToString());
                if (chuho.addchuho(machuho, ngaystart.ToString(), ngayend.ToString(), kieusohuu, macanho))
                {
                    MessageBox.Show("Thêm chủ hộ thành công", "Thêm Chủ Hộ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xảy ra lỗi thêm chủ hộ ", "Thêm Chủ Hộ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thêm chủ hộ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ThemChuHo_Load(object sender, EventArgs e)
        {
            load_cbbmacanho();
        }
    }
}
