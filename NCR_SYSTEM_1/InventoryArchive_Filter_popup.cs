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
    public partial class InventoryArchive_Filter_popup : Form
    {
        public static string startdate;
        public static string enddate;
        public static string user;


        public InventoryArchive_Filter_popup()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if(starttxt.Value.ToString() !="" & endtxt.Value.ToString() != "")
            {
                startdate = starttxt.Value.ToString("MM/dd/yyyy");
                enddate = endtxt.Value.ToString("MM/dd/yyyy");


                user = usertxt.Text;


                InventoryArchive_Module._instance.filter();
                InventoryArchive_Module._instance.printfilter();
                Form1.status = "true";
                this.Hide();
            }
            else
            {

            }
            

        }

        private void InventoryArchive_Filter_popup_Load(object sender, EventArgs e)
        {
            

            endtxt.Value = DateTime.Today;
            starttxt.Value = DateTime.Today;

            
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

        private void usertxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                base.OnKeyPress(e);
            }
        }
    }
}
