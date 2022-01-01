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
            Form1.status = "true";
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            

            if (low.Text != "" && high.Text != "" && Convert.ToInt32(low.Text) < Convert.ToInt32(high.Text) && low.Text != "0" && high.Text != "0")
            {
                lowtxt = low.Text;
                hightxt = high.Text;
                this.Hide();
                Updateproduct_popup._instance.update();
                Form1.status = "true";

            }
            else
            {
                if (low.Text == "0" && high.Text == "0")
                {
                    MessageBox.Show("Invalid input.");
                }
                else
                {
                    MessageBox.Show("Fill up all necessary fields.");
                }

            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1.status = "true";
        }

        private void low_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void high_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
