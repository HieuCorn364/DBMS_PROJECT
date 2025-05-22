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
    public partial class SignUpForm : Form
    {
        MyDB mydb = new MyDB();
        User user = new User();
        public SignUpForm()
        {
            InitializeComponent();
        }
        private static SignUpForm instance;
        public static SignUpForm Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new SignUpForm();
                }
                return instance;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txt_user.Text;
            string password = txt_password.Text;
            int roleid;
            if (rdb_admin.Checked)
            {
                roleid = 1;
            }
            else
            {
                roleid = 2;
            }
            user.adduser(username, password, roleid);
        }
        void loaddata()
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT user_id, password, role_id FROM dbo.UserView");
                dgv_user.DataSource = user.getUser(command);
                dgv_user.AllowUserToAddRows = false;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Bạn không có quyền", "Notification",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (staticdata.role == 1)
            {
                string userid = dgv_user.CurrentRow.Cells[0].Value.ToString();
                if ((MessageBox.Show("Bạn có chắc muốn xóa người dùng này ra khỏi danh sách không", "Xóa Người Dùng", MessageBoxButtons.YesNo
                        , MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    user.deleteuser(userid);
                    loaddata();
                }
            }
            else
            {
                MessageBox.Show("Bạn không có quyền", "Notification",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loaddata();
        }
    }
}
