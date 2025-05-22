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
    public partial class FormHoaDon : Form
    {
        MyDB MyDB = new MyDB();
        SqlCommand sqlCommand = null;
        SqlDataAdapter sqlDataAdapter = null;
        //Singleton Pattern
        private static FormHoaDon instance;
        public static FormHoaDon Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new FormHoaDon();
                }
                return instance;
            }
        }

        private FormHoaDon()
        {
            InitializeComponent();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemHoaDon();
        }

        private void TimKiemHoaDon()
        {
            string query = "SELECT * FROM fn_TimKiemHoaDonTheoThangNam(@Month, @Year)";
            DataTable dt = new DataTable();
            try
            {
                MyDB.openConnection();
                sqlCommand = new SqlCommand(query, MyDB.GetConnection());
                sqlCommand.Parameters.AddWithValue("@Month", cbThang.SelectedItem);
                sqlCommand.Parameters.AddWithValue("@Year", txtNam.Text);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dt);
                dgvHoaDon.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Bạn không có quyền", "Notification",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MyDB.closeConnection();
            }
        }

        private void FormHoaDon_Load(object sender, EventArgs e)
        {
            LoadHoaDonData();
        }

        private void LoadHoaDonData()
        {
            string query = "SELECT * FROM View_HoaDon";
            SqlCommand command = new SqlCommand(query);
            dgvHoaDon.RowTemplate.Height = 30;
            command.Connection = MyDB.SqlCon;
            MyDB.closeConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dgvHoaDon.DataSource = table;
            MyDB.closeConnection();
            dgvHoaDon.AllowUserToAddRows = false;
        }

        private void dgvHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = dgvHoaDon.Rows[e.RowIndex];
            string maHoaDon = selectedRow.Cells["MaHoaDon"].Value.ToString();
            string trangThai = selectedRow.Cells["TrangThai"].Value.ToString();
            FormChiTietHoaDon formChiTietHoaDon = new FormChiTietHoaDon(int.Parse(maHoaDon), trangThai);
            formChiTietHoaDon.ShowDialog();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            TaoHoaDonMoi();
        }
        private void TaoHoaDonMoi()
        {
            try
            {
                MyDB.openConnection();
                using (sqlCommand = new SqlCommand("sp_TaoHoaDonChoChuHo", MyDB.GetConnection()))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã tạo thành công các hóa đơn cho tháng " + DateTime.Now.Month);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn không có quyền", "Notification",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MyDB.closeConnection();
            }
        }
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvHoaDon.CurrentRow == null)
                {
                    MessageBox.Show("Chọn hóa đơn");
                    return; 
                }
                if (dgvHoaDon.CurrentRow.Cells[0].Value == null)
                {
                    MessageBox.Show("Giá trị hóa đơn không hợp lệ.");
                    return; 
                }
                int maHoaDon = int.Parse(dgvHoaDon.CurrentRow.Cells[0].Value.ToString());
                FormTaoHoaDonMoi formTaoHoaDonMoi = new FormTaoHoaDonMoi(maHoaDon);
                formTaoHoaDonMoi.ShowDialog();
            }
            catch (Exception ex)
            {  
                MessageBox.Show($"Đã có lỗi xảy ra: {ex.Message}");
            }

        }

        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            TongDoanhThu();
        }
        private void TongDoanhThu()
        {
            string query = "SELECT * FROM fn_TinhDoanhThuTienIch(@Month, @Year)";
            try
            {
                // Mở kết nối
                MyDB.openConnection();

                // Tạo SqlCommand để thực thi truy vấn
                using (sqlCommand = new SqlCommand(query, MyDB.GetConnection()))
                {
                    // Thêm tham số cho function
                    sqlCommand.Parameters.AddWithValue("@Month", cbThang.SelectedItem);
                    sqlCommand.Parameters.AddWithValue("@Year", txtNam.Text);

                    sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                    // Tạo DataTable để chứa dữ liệu từ SQL Server
                    DataTable dt = new DataTable();

                    // Đổ dữ liệu từ SQL Server vào DataTable
                    sqlDataAdapter.Fill(dt);

                    dgvHoaDon.DataSource = dt;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            finally
            {
                MyDB.closeConnection();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadHoaDonData();
        }
    }
}
