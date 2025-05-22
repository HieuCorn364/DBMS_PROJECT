using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_An_DBMS
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = this.txtUserName.Text.Trim();
            string password = this.txtPass.Text.Trim();
            MyDB myDB = new MyDB();
            myDB.openConnection();
            SqlCommand cmd = new SqlCommand("SELECT role_id FROM Users WHERE user_id = @username AND  password = @pass", myDB.GetConnection());
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@pass", password);
            SqlDataReader reader = cmd.ExecuteReader();
            int role = -1;
            //sau khi cái reader lấy được thông tin role từ bảng TaiKhoan thì ta sẽ gán vào biến role
            while (reader.Read())
            {
                role = reader.GetInt32(0);
                staticdata.role = role;
            }
            myDB.closeConnection();
            if (role != -1)
            {
                //sau khi lấy biến role ta sẽ đưa cái role đó vào file role.txt, cái file này được lưu trong folder debug của bin trong folder dự án
                FormMenu home = new FormMenu();
                string content = "";
                string filePath = GetCurrentFolderPath() + @"\role.txt";
                if (role == 1)
                {
                    content = "sysadmin"; //kiểm tra thử role có phải admin hay không
                }
                else
                {
                    content = "Employee"; //kiểm tra role có phải staff không
                }
                WriteToFile(filePath, content); // xong rồi ghi nó vào file
                home.Show();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GetCurrentFolderPath()
        {
            string filePath = Assembly.GetExecutingAssembly().Location;
            return Path.GetDirectoryName(filePath);
        }
        private void WriteToFile(string filePath, string content) //hàm viết vào file đây
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine(content);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
