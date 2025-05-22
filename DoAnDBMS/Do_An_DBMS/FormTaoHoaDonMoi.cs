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
    public partial class FormTaoHoaDonMoi : Form
    {
        MyDB myDB = new MyDB();
        int maHD;
        //Đối tượng thực thi câu lệnh
        SqlCommand sqlCommand = null;
        SqlDataAdapter sqlDataAdapter = null;
        //Chứa dữ liệu đổ vào
        DataTable dt = new DataTable();
        public FormTaoHoaDonMoi(int maHoaDon)
        {
            InitializeComponent();
            maHD = maHoaDon;
            lblTitle.Text = "Cập nhật số liệu cho mã hóa đơn: " + maHoaDon.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormTaoHoaDonMoi_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                myDB.openConnection();
                using (sqlCommand = new SqlCommand("sp_TaoChiTietHoaDon", myDB.GetConnection()))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@MaHoaDon", SqlDbType.Int)).Value = maHD;
                    sqlCommand.Parameters.Add(new SqlParameter("@SoDien", SqlDbType.Int)).Value = int.Parse(txtDien.Text);
                    sqlCommand.Parameters.Add(new SqlParameter("@SoKhoiNuoc", SqlDbType.Int)).Value = int.Parse(txtNuoc.Text);
                    sqlCommand.Parameters.Add(new SqlParameter("@SoXe", SqlDbType.Int)).Value = int.Parse(txtXe.Text);
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã cập nhật dữ liệu hóa đơn thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn không có quyền", "Notification",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                myDB.closeConnection();
            }
        }
    }
}
