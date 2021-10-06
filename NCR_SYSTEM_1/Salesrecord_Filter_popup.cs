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
    public partial class Salesrecord_Filter_popup : Form
    {
        public static string startdate;
        public static string enddate;
        public static string transactiontype;
        public static string assistedby;

        public Salesrecord_Filter_popup()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            startdate = starttxt.Value.ToString("MM/dd/yyyy");
            enddate = endtxt.Value.ToString("MM/dd/yyyy");
            transactiontype = transactiontypetxt.Text;
            assistedby = assistedtxt.Text;


            Salesrecord_module._instance.filter();
            Salesrecord_module.checker = "allow";
            this.Hide();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

            this.Hide();
            Salesrecord_module.checker = "allow";
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Salesrecord_Filter_popup_Load(object sender, EventArgs e)
        {
            endtxt.Value = DateTime.Today;
            starttxt.Value = DateTime.Today;
        }
    }
}
