namespace Do_An_DBMS
{
    partial class DanhSachCanHo
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
            this.data_CanHo = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_ThemCanHo = new System.Windows.Forms.Button();
            this.btb_reloaddata = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_xoacanho = new System.Windows.Forms.Button();
            this.btnGiaThue = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_CanHo)).BeginInit();
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
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1072, 100);
            this.panel1.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(434, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "Danh Sách Căn Hộ";
            // 
            // data_CanHo
            // 
            this.data_CanHo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_CanHo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.data_CanHo.Location = new System.Drawing.Point(29, 106);
            this.data_CanHo.Name = "data_CanHo";
            this.data_CanHo.RowHeadersWidth = 51;
            this.data_CanHo.RowTemplate.Height = 24;
            this.data_CanHo.Size = new System.Drawing.Size(1031, 339);
            this.data_CanHo.TabIndex = 22;
            this.data_CanHo.DoubleClick += new System.EventHandler(this.data_CanHo_DoubleClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "MaCanHo";
            this.Column1.HeaderText = "Mã Căn Hộ";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.DataPropertyName = "TrangThaiSuDung";
            this.Column2.HeaderText = "Trạng Thái Sử Dụng";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.DataPropertyName = "MaKhuCanHo";
            this.Column3.HeaderText = "Mã Khu Căn Hộ";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.DataPropertyName = "MaLoaiCanHo";
            this.Column4.HeaderText = "Mã Loại Căn Hộ";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            // 
            // btn_ThemCanHo
            // 
            this.btn_ThemCanHo.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.btn_ThemCanHo.Location = new System.Drawing.Point(125, 469);
            this.btn_ThemCanHo.Name = "btn_ThemCanHo";
            this.btn_ThemCanHo.Size = new System.Drawing.Size(181, 55);
            this.btn_ThemCanHo.TabIndex = 27;
            this.btn_ThemCanHo.Text = "Thêm Căn Hộ";
            this.btn_ThemCanHo.UseVisualStyleBackColor = true;
            this.btn_ThemCanHo.Click += new System.EventHandler(this.btn_ThemCanHo_Click);
            // 
            // btb_reloaddata
            // 
            this.btb_reloaddata.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.btb_reloaddata.Location = new System.Drawing.Point(361, 469);
            this.btb_reloaddata.Name = "btb_reloaddata";
            this.btb_reloaddata.Size = new System.Drawing.Size(181, 55);
            this.btb_reloaddata.TabIndex = 26;
            this.btb_reloaddata.Text = "Làm mới";
            this.btb_reloaddata.UseVisualStyleBackColor = true;
            this.btb_reloaddata.Click += new System.EventHandler(this.btb_reloaddata_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.label2.Location = new System.Drawing.Point(-84, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 21);
            this.label2.TabIndex = 25;
            this.label2.Text = "Tìm kiếm cư dân:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.textBox1.Location = new System.Drawing.Point(82, 5);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(383, 35);
            this.textBox1.TabIndex = 24;
            // 
            // btn_xoacanho
            // 
            this.btn_xoacanho.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.btn_xoacanho.Location = new System.Drawing.Point(595, 469);
            this.btn_xoacanho.Name = "btn_xoacanho";
            this.btn_xoacanho.Size = new System.Drawing.Size(181, 55);
            this.btn_xoacanho.TabIndex = 23;
            this.btn_xoacanho.Text = "Xóa Căn Hộ";
            this.btn_xoacanho.UseVisualStyleBackColor = true;
            this.btn_xoacanho.Click += new System.EventHandler(this.btn_xoacanho_Click);
            // 
            // btnGiaThue
            // 
            this.btnGiaThue.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.btnGiaThue.Location = new System.Drawing.Point(831, 469);
            this.btnGiaThue.Name = "btnGiaThue";
            this.btnGiaThue.Size = new System.Drawing.Size(181, 55);
            this.btnGiaThue.TabIndex = 28;
            this.btnGiaThue.Text = "Check Gía Thuê";
            this.btnGiaThue.UseVisualStyleBackColor = true;
            this.btnGiaThue.Click += new System.EventHandler(this.btnGiaThue_Click);
            // 
            // DanhSachCanHo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1072, 558);
            this.Controls.Add(this.btnGiaThue);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.data_CanHo);
            this.Controls.Add(this.btn_ThemCanHo);
            this.Controls.Add(this.btb_reloaddata);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_xoacanho);
            this.Name = "DanhSachCanHo";
            this.Text = "DanhSachCanHo";
            this.Load += new System.EventHandler(this.DanhSachCanHo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_CanHo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView data_CanHo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Button btn_ThemCanHo;
        private System.Windows.Forms.Button btb_reloaddata;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_xoacanho;
        private System.Windows.Forms.Button btnGiaThue;
    }
}