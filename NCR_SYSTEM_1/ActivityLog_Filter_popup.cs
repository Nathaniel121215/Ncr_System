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
    public partial class ActivityLog_Filter_popup : Form
    {
        public ActivityLog_Filter_popup()
        {
            InitializeComponent();
        }

        public static string startdate;
        public static string enddate;
        public static string module;
        public static string user;

        private void ActivityLog_Filter_popup_Load(object sender, EventArgs e)
        {
            endtxt.Value = DateTime.Today;
            starttxt.Value = DateTime.Today;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if(starttxt.Value.ToString() != "" && endtxt.Value.ToString() != "" && moduletxt.Text!="")
            {
                startdate = starttxt.Value.ToString("MM/dd/yyyy");
                enddate = endtxt.Value.ToString("MM/dd/yyyy");


                user = usertxt.Text;
                module = moduletxt.Text;


                ActivityLog_Module._instance.filter();
                ActivityLog_Module.checker = "allow";
                this.Hide();
                Form1.status = "true";

            }
            else
            {
                MessageBox.Show("Fill up all necessary fields.");
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

        private void usertxt_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
