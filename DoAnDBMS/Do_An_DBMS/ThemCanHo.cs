using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_An_DBMS
{
    public partial class ThemCanHo : Form
    {
        CanHo canho = new CanHo();

        //Singleton pattern
        private static ThemCanHo instance;
        public static ThemCanHo Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new ThemCanHo();
                }
                return instance;
            }
        }
        private ThemCanHo()
        {
            InitializeComponent();
        }
        void loadcbb_makhucanho()
        {
            cbb_makhucanho.DataSource = canho.getMaKhuCanHo();
            cbb_makhucanho.DisplayMember = "MaKhuCanHo";
            cbb_makhucanho.ValueMember = "MaKhuCanHo";
            cbb_makhucanho.SelectedItem = null;
        }
        void loadcbb_maloaicanho()
        {
            cbb_maloaicanho.DataSource = canho.getMaLoaiCanHo();
            cbb_maloaicanho.DisplayMember = "MaLoai";
            cbb_maloaicanho.ValueMember = "MaLoai";
            cbb_maloaicanho.SelectedItem = null;
        }

        private void ThemCanHo_Load(object sender, EventArgs e)
        {
            loadcbb_makhucanho();
            loadcbb_maloaicanho();
        }

        private void btn_ThemCanHo_Click(object sender, EventArgs e)
        {
            try
            {
                string trangthaisudung = txt_trangthai.Text;
                int makhu = int.Parse(cbb_makhucanho.SelectedValue.ToString());
                int maloai = int.Parse(cbb_maloaicanho.SelectedValue.ToString());
                if (canho.addcanho(trangthaisudung, makhu, maloai))
                {
                    MessageBox.Show("Thêm căn hộ thành công", "Thêm Căn Hộ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xảy ra lỗi thêm căn hộ ", "Thêm Căn Hộ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi:" + ex.Message);
            }
        }
    }
}
