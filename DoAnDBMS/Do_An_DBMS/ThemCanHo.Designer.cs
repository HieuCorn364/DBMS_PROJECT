namespace Do_An_DBMS
{
    partial class ThemCanHo
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
            this.btn_ThemCanHo = new System.Windows.Forms.Button();
            this.cbb_maloaicanho = new System.Windows.Forms.ComboBox();
            this.cbb_makhucanho = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_trangthai = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(496, 100);
            this.panel1.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(222, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thêm Căn Hộ";
            // 
            // btn_ThemCanHo
            // 
            this.btn_ThemCanHo.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btn_ThemCanHo.Location = new System.Drawing.Point(205, 324);
            this.btn_ThemCanHo.Name = "btn_ThemCanHo";
            this.btn_ThemCanHo.Size = new System.Drawing.Size(144, 49);
            this.btn_ThemCanHo.TabIndex = 28;
            this.btn_ThemCanHo.Text = "Thêm Căn Hộ";
            this.btn_ThemCanHo.UseVisualStyleBackColor = true;
            this.btn_ThemCanHo.Click += new System.EventHandler(this.btn_ThemCanHo_Click);
            // 
            // cbb_maloaicanho
            // 
            this.cbb_maloaicanho.FormattingEnabled = true;
            this.cbb_maloaicanho.Location = new System.Drawing.Point(205, 248);
            this.cbb_maloaicanho.Name = "cbb_maloaicanho";
            this.cbb_maloaicanho.Size = new System.Drawing.Size(236, 24);
            this.cbb_maloaicanho.TabIndex = 27;
            // 
            // cbb_makhucanho
            // 
            this.cbb_makhucanho.FormattingEnabled = true;
            this.cbb_makhucanho.Location = new System.Drawing.Point(205, 188);
            this.cbb_makhucanho.Name = "cbb_makhucanho";
            this.cbb_makhucanho.Size = new System.Drawing.Size(236, 24);
            this.cbb_makhucanho.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(55, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 22);
            this.label4.TabIndex = 25;
            this.label4.Text = "Mã Loại Căn Hộ:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(55, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 22);
            this.label3.TabIndex = 24;
            this.label3.Text = "Mã Khu Căn Hộ:";
            // 
            // txt_trangthai
            // 
            this.txt_trangthai.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_trangthai.Location = new System.Drawing.Point(205, 125);
            this.txt_trangthai.Multiline = true;
            this.txt_trangthai.Name = "txt_trangthai";
            this.txt_trangthai.Size = new System.Drawing.Size(236, 28);
            this.txt_trangthai.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 22);
            this.label2.TabIndex = 22;
            this.label2.Text = "Trạng Thái Sử Dụng:";
            // 
            // ThemCanHo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 414);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_ThemCanHo);
            this.Controls.Add(this.cbb_maloaicanho);
            this.Controls.Add(this.cbb_makhucanho);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_trangthai);
            this.Controls.Add(this.label2);
            this.Name = "ThemCanHo";
            this.Text = "ThemCanHo";
            this.Load += new System.EventHandler(this.ThemCanHo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ThemCanHo;
        private System.Windows.Forms.ComboBox cbb_maloaicanho;
        private System.Windows.Forms.ComboBox cbb_makhucanho;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_trangthai;
        private System.Windows.Forms.Label label2;
    }
}