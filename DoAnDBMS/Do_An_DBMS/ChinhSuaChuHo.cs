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
    public partial class ChinhSuaChuHo : Form
    {
        //Singleton Pattern

        private static ChinhSuaChuHo instance;

        public static ChinhSuaChuHo Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new ChinhSuaChuHo();
                }
                return instance;
            }
        }
        private ChinhSuaChuHo()
        {
            InitializeComponent();
        }
    }
}
