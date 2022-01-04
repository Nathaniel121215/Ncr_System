using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
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
    public partial class StockAdjustmentReportExtractionFilter_popup : Form
    {
        public StockAdjustmentReportExtractionFilter_popup()
        {
            InitializeComponent();
        }

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public static string startdate;
        public static string enddate;
        public static string reason;
        public static string percentage;

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if(starttxt.Value.ToString() != "" && endtxt.Value.ToString() != "" && reasontxt.Text != "" && percentagetxt.Text != "")
            {
                startdate = starttxt.Value.ToString("MM/dd/yyyy");
                enddate = endtxt.Value.ToString("MM/dd/yyyy");
                reason = reasontxt.Text;
                percentage = percentagetxt.Text;

                //Activity Log STOCK PURCHASE EVENT

                FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                Counter_class get4 = resp4.ResultAs<Counter_class>();
                int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                var data3 = new ActivityLog_Class
                {
                    Event_ID = cnt4.ToString(),
                    Module = "Stock Adjustment Record Module",
                    Action = "Stock Adjustment Report Extracted",
                    Date = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                    User = Form1.username,
                    Accountlvl = Form1.levelac,

                };



                FirebaseResponse response5 = client.Set("ActivityLog/" + data3.Event_ID, data3);



                var obj4 = new Counter_class
                {
                    cnt = data3.Event_ID

                };

                SetResponse response6 = client.Set("ActivityLogCounter/node", obj4);


                Stockadjustmentrecord_module._instance.extract();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Fill up all necessary fields.");
            }
            
        }

        private void StockAdjustmentReportExtractionFilter_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

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
    }
}
