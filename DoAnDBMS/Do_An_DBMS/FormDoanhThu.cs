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
    public partial class FormDoanhThu : Form
    {
        MyDB myDB = new MyDB();
        SqlCommand sqlCommand = null;
        SqlDataAdapter sqlDataAdapter = null;
        DataTable dt = new DataTable();
        private static FormDoanhThu instance;
        public static FormDoanhThu Instance
        {
            get
            {
                if (instance == null||instance.IsDisposed)
                {
                    instance = new FormDoanhThu();
                }
                return instance;
            }
        }
        private FormDoanhThu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TongDoanhThu();
        }
        private void TongDoanhThu()
        {
            string query = "SELECT * FROM fn_TinhDoanhThuTienIch(@Month, @Year)";
            try
            {
                // Mở kết nối
                myDB.openConnection();

                // Tạo SqlCommand để thực thi truy vấn
                using (sqlCommand = new SqlCommand(query, myDB.GetConnection()))
                {
                    // Thêm tham số cho function
                    sqlCommand.Parameters.AddWithValue("@Month", cbThang.SelectedItem);
                    sqlCommand.Parameters.AddWithValue("@Year", txtNam.Text);
                    sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dt = new DataTable();
                    sqlDataAdapter.Fill(dt);
                    dgvDoanhThu.DataSource = dt;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            finally
            {
                myDB.closeConnection();
            }
        }

        private void FormDoanhThu_Load(object sender, EventArgs e)
        {
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            DoanhThuTienIchForm doanhThuTienIchForm = new DoanhThuTienIchForm(); 
            doanhThuTienIchForm.ShowDialog();
        }
    }
}
