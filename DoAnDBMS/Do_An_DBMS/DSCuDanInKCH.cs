using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_An_DBMS
{
    public partial class DSCuDanInKCH : Form
    {
        KhuCanHo khucanho = new KhuCanHo();
        public DSCuDanInKCH()
        {
            InitializeComponent();
        }
        void loaddata()
        {
            int makhucanho = staticdata.makhucanho;
            dgv_DS.DataSource = khucanho.GetDanhSachCuDan(makhucanho);

        }
        private void DSCuDanInKCH_Load(object sender, EventArgs e)
        {
            loaddata();
        }
    }
}
