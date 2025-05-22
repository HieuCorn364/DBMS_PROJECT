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
    public partial class DanhSachCanHo : Form
    {
        MyDB MyDB = new MyDB();
        //Đối tượng kết nối
        SqlConnection sqlConnection = null;
        //Đối tượng thực thi câu lệnh
        SqlCommand sqlCommand = null;
        SqlDataAdapter sqlDataAdapter = null;
        //Chứa dữ liệu đổ vào
        DataTable dt = new DataTable();
        CanHo canho = new CanHo();
        //Singleton pattern
        private static DanhSachCanHo instance;
        public static DanhSachCanHo Instance
        {
            get
            {
                if (instance == null||instance.IsDisposed)
                {
                    instance = new DanhSachCanHo();
                }
                return instance;
            }
        }
        private DanhSachCanHo()
        {
            InitializeComponent();
        }

        private void btn_ThemCanHo_Click(object sender, EventArgs e)
        {
            ThemCanHo themCanHo = ThemCanHo.Instance;
            this.Hide();
            themCanHo.Show();
            themCanHo.FormClosed += (s, args) => {
                this.Show();
            };
        }
        void loaddata_canho()
        {
            SqlCommand command = new SqlCommand("Select * from view_danhsachcanho");
            data_CanHo.RowTemplate.Height = 30;
            data_CanHo.DataSource = canho.getCanHo(command);
            data_CanHo.AllowUserToAddRows = false;
        }

        private void DanhSachCanHo_Load(object sender, EventArgs e)
        {
            loaddata_canho();
        }

        private void btn_xoacanho_Click(object sender, EventArgs e)
        {
            int macanho = int.Parse(data_CanHo.CurrentRow.Cells[0].Value.ToString());
            if ((MessageBox.Show("Bạn có chắc muốn xóa chủ hộ này ra khỏi danh sách không", "Xóa Chủ Hộ", MessageBoxButtons.YesNo
                    , MessageBoxIcon.Question) == DialogResult.Yes))
            {
                canho.deletecanho(macanho);
                loaddata_canho();
            }
        }

        private void btb_reloaddata_Click(object sender, EventArgs e)
        {
            loaddata_canho();
        }

        private void data_CanHo_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btnGiaThue_Click(object sender, EventArgs e)
        {
            int maCanHo = int.Parse(data_CanHo.CurrentRow.Cells[0].Value.ToString());
            string query = "SELECT dbo.fn_LayTienThueCanHo(@MaCanHo) AS TienThue";
            try
            {
                MyDB.openConnection();
                using (sqlCommand = new SqlCommand(query, MyDB.GetConnection()))
                {
                    sqlCommand.Parameters.Add("@MaCanHo", SqlDbType.Int).Value = maCanHo;
                    sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    decimal tienThue = (decimal)sqlCommand.ExecuteScalar();
                    Console.WriteLine($"Tiền thuê của căn hộ {maCanHo} là: {tienThue}");
                    MessageBox.Show("TIỀN THUÊ CỦA CĂN HỘ NÀY LÀ: " + tienThue);                }
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
    }
}
