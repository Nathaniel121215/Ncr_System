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
    public partial class SalesReportExtractionFilter_popup : Form
    {
        public SalesReportExtractionFilter_popup()
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
        public static string transactiontype;
        public static string percentage;

        private void SalesReportExtractionFilter_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            endtxt.Value = DateTime.Today;
            starttxt.Value = DateTime.Today;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            startdate = starttxt.Value.ToString("MM/dd/yyyy");
            enddate = endtxt.Value.ToString("MM/dd/yyyy");
            transactiontype = transactiontypetxt.Text;
            percentage = percentagetxt.Text;

            //Activity Log STOCK PURCHASE EVENT

            FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
            Counter_class get4 = resp4.ResultAs<Counter_class>();
            int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



            var data3 = new ActivityLog_Class
            {
                Event_ID = cnt4.ToString(),
                Module = "Sales Record Module",
                Action = "Sales Report Extracted",
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
