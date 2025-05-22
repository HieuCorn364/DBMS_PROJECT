namespace SQL_KETNOI
{
    partial class Form1
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
            this.openbtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.soLuongbtn = new System.Windows.Forms.Button();
            this.yesbtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // openbtn
            // 
            this.openbtn.Location = new System.Drawing.Point(74, 12);
            this.openbtn.Name = "openbtn";
            this.openbtn.Size = new System.Drawing.Size(197, 71);
            this.openbtn.TabIndex = 0;
            this.openbtn.Text = "Mở Kết Nối";
            this.openbtn.UseVisualStyleBackColor = true;
            this.openbtn.Click += new System.EventHandler(this.openbtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(74, 99);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(642, 265);
            this.dataGridView1.TabIndex = 2;
            // 
            // soLuongbtn
            // 
            this.soLuongbtn.Location = new System.Drawing.Point(74, 382);
            this.soLuongbtn.Name = "soLuongbtn";
            this.soLuongbtn.Size = new System.Drawing.Size(150, 41);
            this.soLuongbtn.TabIndex = 3;
            this.soLuongbtn.Text = "SỐ LƯỢNG";
            this.soLuongbtn.UseVisualStyleBackColor = true;
            this.soLuongbtn.Click += new System.EventHandler(this.soLuongbtn_Click);
            // 
            // yesbtn
            // 
            this.yesbtn.Location = new System.Drawing.Point(536, 29);
            this.yesbtn.Name = "yesbtn";
            this.yesbtn.Size = new System.Drawing.Size(75, 23);
            this.yesbtn.TabIndex = 4;
            this.yesbtn.Text = "yes";
            this.yesbtn.UseVisualStyleBackColor = true;
            this.yesbtn.Click += new System.EventHandler(this.yesbtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(327, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(180, 22);
            this.textBox1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.yesbtn);
            this.Controls.Add(this.soLuongbtn);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.openbtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openbtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button soLuongbtn;
        private System.Windows.Forms.Button yesbtn;
        private System.Windows.Forms.TextBox textBox1;
    }
}

