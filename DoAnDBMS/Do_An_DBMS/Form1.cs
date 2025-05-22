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
    public partial class Form1 : Form
    {
        string con = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DADBMS;Integrated Security=True";
        //Đối tượng kết nối
        SqlConnection sqlConnection = null;
        //Đối tượng thực thi câu lệnh
        SqlCommand sqlCommand = null;
        SqlDataAdapter sqlDataAdapter = null;
        //Chứa dữ liệu đổ vào
        DataTable dt = new DataTable();

        private void btnKetNoi_Click_1(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();
                MessageBox.Show("Kết Nối Thành Công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            HienThiCanHo();
        }

        private void HienThiCanHo()
        {
            
            try
            {
                sqlConnection.Open();
                using (sqlCommand = new SqlCommand("sp_LayThongTinCanHo", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@MaCanHo", SqlDbType.Int)).Value = DBNull.Value;
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable); 
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(con);
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            FormHoaDon formHoaDon = FormHoaDon.Instance;
            formHoaDon.ShowDialog();
        }

        private void btnCanHo_Click(object sender, EventArgs e)
        {
            DanhSachCuDan danhSachCuDan = DanhSachCuDan.Instance;
            danhSachCuDan.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DanhSachChuHo danhSachChuHo = DanhSachChuHo.Instance;
            danhSachChuHo.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DanhSachCanHo danhSachCanHo = DanhSachCanHo.Instance;
            danhSachCanHo.ShowDialog();
        }
    }
}
