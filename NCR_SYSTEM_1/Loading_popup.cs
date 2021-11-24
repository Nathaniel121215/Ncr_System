using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_SYSTEM_1
{
    public partial class Loading_popup : Form
    {
        public Loading_popup()
        {
            InitializeComponent();
        }

        private void Loading_popup_Load(object sender, EventArgs e)
        {
            timer1.Interval = Form1.loadingtime;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
