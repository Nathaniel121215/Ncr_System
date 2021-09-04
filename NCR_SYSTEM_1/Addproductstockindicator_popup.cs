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
    public partial class Addproductstockindicator_popup : Form
    {
        public static string lowtxt;
        public static string hightxt;

        public Addproductstockindicator_popup()
        {
            InitializeComponent();
        }


        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            lowtxt = low.Text;
            hightxt = high.Text;
            this.Hide();
            Addnewproduct_popup._instance.save();

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Addproductstockindicator_popup_Load(object sender, EventArgs e)
        {

        }
    }
}
