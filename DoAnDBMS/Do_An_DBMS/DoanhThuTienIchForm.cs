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
    public partial class DoanhThuTienIchForm : Form
    {
        MyDB mydb = new MyDB();
        public DoanhThuTienIchForm()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            try
            {
                using (SqlConnection conn = mydb.GetConnection())
                {
                    DataTable dt = new DataTable();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandType = CommandType.Text;
                        command.CommandText = @"SELECT * 
                           FROM fn_BaoCaoDoanhThuTienIch(@Thang, @Nam)
                           ORDER BY [Khu căn hộ], [Tổng doanh thu (VNĐ)] DESC";
                        command.Parameters.AddWithValue("@Thang", DateTime.Now.Month);
                        command.Parameters.AddWithValue("@Nam", DateTime.Now.Year);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                throw new Exception("Lỗi khi lấy báo cáo doanh thu: " + ex.Message);
            }

        }

        private void DoanhThuTienIchForm_Load(object sender, EventArgs e)
        {
           
        }
    }
}
