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

    public partial class ChinhSuaCuDan : Form
    {
        CuDan cudan = new CuDan();
        MyDB db = new MyDB();

        //Singleton pattern
        private static ChinhSuaCuDan instance;
        public static ChinhSuaCuDan Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new ChinhSuaCuDan();
                }
                return instance;
            }
        }
        private ChinhSuaCuDan()
        {
            InitializeComponent();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Hoten.Text.Trim() == "")
                {
                    throw new Exception("Họ tên cư dân không được để trống!");
                }
                if (ContainsNumeric(txt_Hoten.Text))
                {
                    throw new Exception("Họ tên không có số");
                }
                if (ContainsSpecialCharacters(txt_Hoten.Text))
                {
                    throw new Exception("Họ tên không được có chữ cái đặc biệt");
                }
                if (txt_Sdt.Text.Trim() == "")
                {
                    throw new Exception("Số điện thoại không được để trống");
                }
                if (txt_Sdt.Text.Length != 10)
                {
                    throw new Exception("Số điện thoại phải 10 số");
                }
                if (ContainsChar(txt_Sdt.Text))
                {
                    throw new Exception("Số điện thoại không được có chữ");
                }
                if (txt_CCCD.Text.Trim() == "")
                {
                    throw new Exception("Số căn cước công dân không được để trống");
                }
                if (ContainsChar(txt_CCCD.Text))
                {
                    throw new Exception("Số căn cước công dân không được có chữ");
                }
                if (txt_CCCD.Text.Length != 12)
                {
                    throw new Exception("Số căn cước công dân phải có 12 số");
                }
                if (ContainsSpecialCharacters(txt_Sdt.Text))
                {
                    throw new Exception("Số điện thoại không có kí tự đặc biệt");
                }
                if (ContainsSpecialCharacters(txt_CCCD.Text))
                {
                    throw new Exception("Số CCCD không có kí tự đặc biệt");
                }

                String HoTen = txt_Hoten.Text;
                String Sdt = txt_Sdt.Text;
                String CCCD = txt_CCCD.Text;
                bool gioitinh = true;
                if (rbtn_nu.Checked)
                {
                    gioitinh = false;
                }
                int machuho = int.Parse(txt_MaChuHo.Text);

                if (cudan.editcudan(staticdata.Macudan, HoTen, Sdt, CCCD, gioitinh, machuho))
                {
                    MessageBox.Show("Chỉnh sửa cư dân thành công", "Chỉnh Sửa Cư Dân", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
                else
                {
                    MessageBox.Show("Xảy ra lỗi khi chỉnh sửa cư dân ", "Chỉnh Sửa Cư Dân", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Chỉnh Sửa Cư Dân", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
    }
}
