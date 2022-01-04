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
          
            if (low.Text!="" && high.Text!="" && Convert.ToInt32(low.Text) < Convert.ToInt32(high.Text) && low.Text != "0" && high.Text != "0")
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    lowtxt = low.Text;
                    hightxt = high.Text;
                    this.Hide();
                    Form1.status = "true";
                    Addnewproduct_popup._instance.save();
                }
                else
                {

                }
              
            }
            else
            {
                if(low.Text == "0" && high.Text == "0")
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
            if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                this.Hide();
                Form1.status = "true";
            }
            else
            {

            }

        }

        private void Addproductstockindicator_popup_Load(object sender, EventArgs e)
        {
            numberlimit(low);
            numberlimit(high);
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                this.Hide();
                Form1.status = "true";
            }
            else
            {

            }

        }

        private void low_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void high_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void numberlimit(Bunifu.Framework.UI.BunifuMetroTextbox metroTextbox)

        {
            foreach (var ctl in metroTextbox.Controls)
            {

                if (ctl.GetType() == typeof(TextBox))

                {
                    var txt = (TextBox)ctl;
                    txt.MaxLength = 10;

                }

            }

        }

    }
}
