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
    public partial class SupplierReportExtractionFilter_popup : Form
    {
        public SupplierReportExtractionFilter_popup()
        {
            InitializeComponent();
        }

        public static string startdate;
        public static string enddate;
        public static string supplier;


        IFirebaseClient client;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };


        private void SupplierReportExtractionFilter_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            endtxt.Value = DateTime.Today;
            starttxt.Value = DateTime.Today;



            ////adding supplier combobox
            try
            {
                FirebaseResponse resp3 = client.Get("SupplierCounter/node");
                Counter_class obj = resp3.ResultAs<Counter_class>();
                int cnt = Convert.ToInt32(obj.cnt);

                List<string> supplier = new List<string>();
                for (int runs = 0; runs <= cnt; runs++)
                {
                    try
                    {
                        FirebaseResponse resp1 = client.Get("Supplier/" + runs);
                        Supplier_Class obj2 = resp1.ResultAs<Supplier_Class>();

                        supplier.Add(obj2.Supplier_Name);
                    }
                    catch
                    {

                    }
                }
                //ADD supplier to combobox
                for (int i = 0; i <= cnt; i++)
                {
                    suppliertxt.Items.Add(supplier[i].ToString());
                }
            }
            catch
            {
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if(starttxt.Value.ToString() != "" && endtxt.Value.ToString() != "")
            {
                startdate = starttxt.Value.ToString("MM/dd/yyyy");
                enddate = endtxt.Value.ToString("MM/dd/yyyy");
                supplier = suppliertxt.Text;

                //Activity Log STOCK PURCHASE EVENT

                FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                Counter_class get4 = resp4.ResultAs<Counter_class>();
                int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                var data3 = new ActivityLog_Class
                {
                    Event_ID = cnt4.ToString(),
                    Module = "Stock Replenishment Record",
                    Action = "Stock Replenishment Report Extracted",
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

                Supplierrecord_module._instance.extract();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Fill up all necessary fields.");
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
    }
}
