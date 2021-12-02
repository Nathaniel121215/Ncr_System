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
    public partial class SalesReportExtractionFilter_popup : Form
    {
        public SalesReportExtractionFilter_popup()
        {
            InitializeComponent();
        }
        public static string startdate;
        public static string enddate;
        public static string transactiontype;
        public static string percentage;

        private void SalesReportExtractionFilter_popup_Load(object sender, EventArgs e)
        {
            endtxt.Value = DateTime.Today;
            starttxt.Value = DateTime.Today;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            startdate = starttxt.Value.ToString("MM/dd/yyyy");
            enddate = endtxt.Value.ToString("MM/dd/yyyy");
            transactiontype = transactiontypetxt.Text;
            percentage = percentagetxt.Text;


            Salesrecord_module._instance.extract();
            this.Hide();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide(); 
        }
    }
}
