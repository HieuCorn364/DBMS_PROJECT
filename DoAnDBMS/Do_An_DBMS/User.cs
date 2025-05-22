using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_An_DBMS
{
    internal class User
    {
        MyDB db = new MyDB();
        public void adduser(string username, string password, int roleid)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Users] (user_id, password, role_id) VALUES (@UserId, @Password, @RoleId)", db.GetConnection());
            try
            {
                command.Parameters.AddWithValue("@UserId", username);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@RoleId", roleid);

                db.openConnection();
                if (command.ExecuteNonQuery() != 0)
                {
                    MessageBox.Show("Thêm user thành công", "Thêm User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    db.closeConnection();
                }
                else
                {
                    MessageBox.Show("Xảy ra lỗi thêm User ", "Thêm User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    db.closeConnection();

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Bạn không có quyền", "Notification",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void deleteuser(string userid)
        {
            SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Users] WHERE user_id = @userId", db.GetConnection());
            try
            {
                command.Parameters.AddWithValue("@userId", userid);
                db.openConnection();
                if (command.ExecuteNonQuery() != 0)
                {
                    MessageBox.Show("Xóa User Thành Công", "Xóa User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    db.closeConnection();
                }
                else
                {
                    MessageBox.Show("Xảy ra lỗi khi xóa User ", "Xóa User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    db.closeConnection();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Bạn không có quyền", "Notification",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        public DataTable getUser(SqlCommand command)
        {
            command.Connection = db.GetConnection();
            db.closeConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            db.closeConnection();
            return table;
        }
    }
}
