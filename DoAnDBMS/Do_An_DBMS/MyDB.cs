using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Do_An_DBMS
{
    internal class MyDB
    {
        private string connectionString;
        public SqlConnection SqlCon;
        public MyDB()
        {
            string filePath = GetCurrentFolderPath() + @"\role.txt"; //lấy file này ra
            string content = File.ReadAllText(filePath).Trim(); //xong rồi đọc nó
            string serverName = "DESKTOP-TPG17OF"; //tên server
            string databaseName = @"DADBMS"; //tên DB mà project sử dụng
            string userName = ""; 
            string password = "";
            if (content.Equals("sysadmin")) //nè nha. chỗ này nếu nó đọc role là ADMIN thì nó đăng nhập dô DB này login này
            {
                userName = "admin_login3";
                password = "123";
                this.connectionString = ConnectionSqlAuthentication(serverName, databaseName, userName, password);
            }
            else if (content.Equals("Employee")) //nếu nó là staff thì đăng nhập DB này
            {
                userName = "staff_login1";
                password = "123";
                this.connectionString = ConnectionSqlAuthentication(serverName, databaseName, userName, password);
            }
            else
            {
                this.connectionString = ConnectionWindowAuthentication(serverName, databaseName);
            }
            this.SqlCon = new SqlConnection(this.connectionString);
        }
        private string GetCurrentFolderPath()
        {
            string filePath = Assembly.GetExecutingAssembly().Location;
            return Path.GetDirectoryName(filePath);
        }
        private string CreateConnectionString(string filePath)
        {
            return $@"Data Source=DESKTOP-TPG17OF;AttachDbFilename=" + @filePath + ";Integrated Security=True";
        }
        private string ConnectionSqlAuthentication(string server, string database, string user, string pass)
        {
            return $"Server={server};Database={database};User Id={user};Password={pass};";
        }

        private String ConnectionWindowAuthentication(string server, string database)
        {
            return $"Server={server};Database={database};Integrated Security=True;";
        }
        public void openConnection()
        {
            //this.SqlCon.Open();
            if (SqlCon.State == System.Data.ConnectionState.Closed)
                SqlCon.Open();
            else return;
        }
        public void closeConnection()
        {
            if (SqlCon.State == System.Data.ConnectionState.Open)
            {
                SqlCon.Close();
            }
            else return;
        }
        public SqlConnection GetConnection()
        {
            return this.SqlCon;
        }
    }
}