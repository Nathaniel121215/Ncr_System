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
    public partial class Supplierrecord_Filter_popup : Form
    {
        public static string startdate;
        public static string enddate;
        public static string supplier;
        public static string assistedby;

        IFirebaseClient client;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };



        public Supplierrecord_Filter_popup()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            startdate = starttxt.Value.ToString("MM/dd/yyyy");
            enddate = endtxt.Value.ToString("MM/dd/yyyy");
            supplier = suppliertxt.Text;
            assistedby = assistedtxt.Text;


            Supplierrecord_module._instance.filter();
        }

        private void Supplierrecord_Filter_popup_Load(object sender, EventArgs e)
        {

            client = new FireSharp.FirebaseClient(config);

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
    }
}
