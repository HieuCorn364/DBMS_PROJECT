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
    public partial class FormChiTietHoaDon : Form
    {
        MyDB myDB = new MyDB();
        int maHoaDon;
        string trangThai;
        //Đối tượng thực thi câu lệnh
        SqlCommand sqlCommand = null;
        SqlDataAdapter sqlDataAdapter = null;
        //Chứa dữ liệu đổ vào
        DataTable dt = new DataTable();
        public FormChiTietHoaDon()
        {
            InitializeComponent();
        }

        public FormChiTietHoaDon(int maHoaDon, string trangThaiHoaDon)
        {
            InitializeComponent();
            this.maHoaDon = maHoaDon;
            this.trangThai = trangThaiHoaDon;
            LayChiTietHoaDon(maHoaDon, trangThaiHoaDon);
        }

        private void LayChiTietHoaDon(int maHoaDon, string trangThaiHoaDon)
        {
            try
            {
                myDB.openConnection();
                using (sqlCommand = new SqlCommand("sp_LayChiTietHoaDon", myDB.GetConnection()))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@MaHoaDon", SqlDbType.Int)).Value = maHoaDon;
                    sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    lblMaHoaDon.Text = "Mã hóa đơn:   " + maHoaDon.ToString();
                    dgvChiTiet.DataSource = dataTable;
                    lblTrangThai.Text = "Trạng Thái Thanh Toán:   " + trangThaiHoaDon.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                myDB.closeConnection();
            }
        }

        private void FormChiTietHoaDon_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XacNhanHoaDon();
        }

        private void XacNhanHoaDon()
        {
            try
            {
                myDB.openConnection();
                using (sqlCommand = new SqlCommand("tg_CapNhatTrangThaiHoaDon", myDB.GetConnection()))
                {
                    trangThai = "Da Thanh Toan";
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@MaHoaDon", SqlDbType.Int)).Value = maHoaDon;
                    sqlCommand.Parameters.Add(new SqlParameter("@TrangThaiMoi", SqlDbType.NVarChar)).Value = trangThai;
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã thanh toán thành công mã hóa đơn " + maHoaDon.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                myDB.closeConnection();
                LayChiTietHoaDon(maHoaDon, trangThai);
            }
        }
    }
}
