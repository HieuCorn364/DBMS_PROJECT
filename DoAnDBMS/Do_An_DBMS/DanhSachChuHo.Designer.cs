namespace Do_An_DBMS
{
    partial class DanhSachChuHo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.data_ChuHo = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_ThemChuHo = new System.Windows.Forms.Button();
            this.btb_reloaddata = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_xoachuho = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_ChuHo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1117, 100);
            this.panel1.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(429, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "Danh Sách Chủ Hộ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // data_ChuHo
            // 
            this.data_ChuHo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_ChuHo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.data_ChuHo.Location = new System.Drawing.Point(62, 123);
            this.data_ChuHo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.data_ChuHo.Name = "data_ChuHo";
            this.data_ChuHo.RowHeadersWidth = 51;
            this.data_ChuHo.RowTemplate.Height = 24;
            this.data_ChuHo.Size = new System.Drawing.Size(1007, 338);
            this.data_ChuHo.TabIndex = 15;
            this.data_ChuHo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.data_ChuHo_CellClick);
            this.data_ChuHo.DoubleClick += new System.EventHandler(this.data_ChuHo_DoubleClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "MaChuHo";
            this.Column1.HeaderText = "Mã Chủ Hộ";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.DataPropertyName = "NgayBatDau";
            this.Column2.HeaderText = "Ngày Bắt Đầu";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.DataPropertyName = "NgayKetThuc";
            this.Column3.HeaderText = "Ngày Kết Thúc";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.DataPropertyName = "KieuSoHuu";
            this.Column4.HeaderText = "Kiếu Sở Hữu";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.DataPropertyName = "MaCanHo";
            this.Column5.HeaderText = "Mã Căn Hộ";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            // 
            // btn_ThemChuHo
            // 
            this.btn_ThemChuHo.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.btn_ThemChuHo.Location = new System.Drawing.Point(176, 477);
            this.btn_ThemChuHo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ThemChuHo.Name = "btn_ThemChuHo";
            this.btn_ThemChuHo.Size = new System.Drawing.Size(181, 55);
            this.btn_ThemChuHo.TabIndex = 20;
            this.btn_ThemChuHo.Text = "Thêm Chủ Hộ";
            this.btn_ThemChuHo.UseVisualStyleBackColor = true;
            this.btn_ThemChuHo.Click += new System.EventHandler(this.btn_ThemChuHo_Click);
            // 
            // btb_reloaddata
            // 
            this.btb_reloaddata.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.btb_reloaddata.Location = new System.Drawing.Point(439, 477);
            this.btb_reloaddata.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btb_reloaddata.Name = "btb_reloaddata";
            this.btb_reloaddata.Size = new System.Drawing.Size(181, 55);
            this.btb_reloaddata.TabIndex = 19;
            this.btb_reloaddata.Text = "Làm mới";
            this.btb_reloaddata.UseVisualStyleBackColor = true;
            this.btb_reloaddata.Click += new System.EventHandler(this.btb_reloaddata_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.label2.Location = new System.Drawing.Point(4, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 21);
            this.label2.TabIndex = 18;
            this.label2.Text = "Tìm kiếm cư dân:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.textBox1.Location = new System.Drawing.Point(171, 4);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(383, 35);
            this.textBox1.TabIndex = 17;
            // 
            // btn_xoachuho
            // 
            this.btn_xoachuho.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.btn_xoachuho.Location = new System.Drawing.Point(702, 477);
            this.btn_xoachuho.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_xoachuho.Name = "btn_xoachuho";
            this.btn_xoachuho.Size = new System.Drawing.Size(181, 55);
            this.btn_xoachuho.TabIndex = 16;
            this.btn_xoachuho.Text = "Xóa Chủ Hộ";
            this.btn_xoachuho.UseVisualStyleBackColor = true;
            this.btn_xoachuho.Click += new System.EventHandler(this.btn_xoachuho_Click);
            // 
            // DanhSachChuHo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1117, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.data_ChuHo);
            this.Controls.Add(this.btn_ThemChuHo);
            this.Controls.Add(this.btb_reloaddata);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_xoachuho);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "DanhSachChuHo";
            this.Text = "DanhSachChuHo";
            this.Load += new System.EventHandler(this.DanhSachChuHo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_ChuHo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView data_ChuHo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button btn_ThemChuHo;
        private System.Windows.Forms.Button btb_reloaddata;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_xoachuho;
    }
}