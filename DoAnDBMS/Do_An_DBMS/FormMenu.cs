using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_An_DBMS
{
    public partial class FormMenu : Form
    {
        MyDB myDB = new MyDB();
        private static FormMenu instance;
        public static FormMenu Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new FormMenu();
                }
                return instance;
            }
        }

        //Child Form
        private Form childForm;
        public void OpenChildForm(Form childForm)
        {
            if (this.childForm != null)
            {
                this.childForm.Close();
            }
            this.childForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel3.Controls.Add(childForm);
            panel3.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        public FormMenu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(FormHoaDon.Instance);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            OpenChildForm(FormDoanhThu.Instance);
        }

        private void btnDanCu_Click(object sender, EventArgs e)
        {
            OpenChildForm(DanhSachCuDan.Instance);
        }

        private void btnCanHo_Click(object sender, EventArgs e)
        {
            OpenChildForm(DanhSachCanHo.Instance);
        }

        private void btnChuHo_Click(object sender, EventArgs e)
        {
            OpenChildForm(DanhSachChuHo.Instance);
        }

        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            try
            {
                myDB.openConnection();
                MessageBox.Show("Kết Nối Thành Công");
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

        private void FormMenu_Load(object sender, EventArgs e)
        {

        }

        private void FormMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            string filePath = GetCurrentFolderPath() + @"\role.txt";
            File.WriteAllText(filePath, string.Empty);
        }
        private string GetCurrentFolderPath()
        {
            string filePath = Assembly.GetExecutingAssembly().Location;
            return Path.GetDirectoryName(filePath);
        }

        private void btnKhuCanHo_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DanhSachKhuCanHo());
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            OpenChildForm(FormThongKe.Instance);
            FormThongKe.Instance.loaddata();
            FormThongKe.Instance.loaddata1();
        }

        private void btnThemTaiKhoan_Click(object sender, EventArgs e)
        {
            OpenChildForm(SignUpForm.Instance);
        }
    }
}
