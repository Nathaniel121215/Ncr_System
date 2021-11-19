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
        public static string transactiontype;
        public static string user;

        private void ActivityLog_Filter_popup_Load(object sender, EventArgs e)
        {
            endtxt.Value = DateTime.Today;
            starttxt.Value = DateTime.Today;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            startdate = starttxt.Value.ToString("MM/dd/yyyy");
            enddate = endtxt.Value.ToString("MM/dd/yyyy");


            user = usertxt.Text;


            ActivityLog_Module._instance.filter();
            ActivityLog_Module.checker = "allow";
            this.Hide();
            
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            ActivityLog_Module.checker = "allow";
            this.Hide();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            ActivityLog_Module.checker = "allow";
            this.Hide();
        }
    }
}
