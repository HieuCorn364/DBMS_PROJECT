using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_An_DBMS
{
    public partial class ThemCuDan : Form
    {
        MyDB db = new MyDB();
        CuDan cudan = new CuDan();

        //Singleton Pattern
        private static ThemCuDan instance;
        public static ThemCuDan Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new ThemCuDan();
                }
                return instance;
            }
        }
        private ThemCuDan()
        {
            InitializeComponent();
        }

        
        private bool ContainsSpecialCharacters(string str)
        {
            // Biểu thức chính quy để kiểm tra các ký tự đặc biệt
            string pattern = @"[^a-zA-Z0-9\s]";
            Regex regex = new Regex(pattern);

            // Kiểm tra xem chuỗi có khớp với biểu thức chính quy hay không
            return regex.IsMatch(str);  //true là có kí tự đặc biệt 
        }
        private bool ContainsNumeric(string input)
        {
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }
        private bool ContainsChar(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

        private void btn_add_Click_1(object sender, EventArgs e)
        {
            try
            {
                String HoTen = txt_Hoten.Text;
                String Sdt = txt_Sdt.Text;
                String CCCD = txt_CCCD.Text;
                bool gioitinh = true;
                if (rbtn_nu.Checked)
                {
                    gioitinh = false;
                }
                int? machuho = string.IsNullOrEmpty(txt_MaChuHo.Text.Trim()) ? (int?)null : Convert.ToInt32(txt_MaChuHo.Text.Trim());

                if (cudan.addcudan(HoTen, Sdt, CCCD, gioitinh, machuho))
                {
                    MessageBox.Show("Thêm cư dân thành công", "Thêm Cư Dân", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xảy ra lỗi thêm cư dân ", "Thêm Cư Dân", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thêm cư dân", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
