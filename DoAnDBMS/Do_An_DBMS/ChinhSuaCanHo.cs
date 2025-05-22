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
    public partial class ChinhSuaCanHo : Form
    {
        //Singleton Pattern
        private static ChinhSuaCanHo instance;
        public static ChinhSuaCanHo Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new ChinhSuaCanHo();
                }
                return instance;
            }
        }
        private ChinhSuaCanHo()
        {
            InitializeComponent();
        }
    }
}
