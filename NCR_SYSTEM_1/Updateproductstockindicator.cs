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
    public partial class Updateproductstockindicator : Form
    {
        public static string lowtxt;
        public static string hightxt;

        public Updateproductstockindicator()
        {
            InitializeComponent();
        }

        private void Updateproductstockindicator_Load(object sender, EventArgs e)
        {


            low.Text = Inventory_Module.low;
            high.Text = Inventory_Module.high;


           
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            lowtxt = low.Text;
            hightxt = high.Text;
            this.Hide();
            Updateproduct_popup._instance.update();
        }
    }
}
