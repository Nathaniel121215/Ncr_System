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
    public partial class Addsupplier_popup : Form
    {
        IFirebaseClient client;


        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"


        };

        public Addsupplier_popup()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                String date = DateTime.Now.ToString("MM/dd/yyyy");


                FirebaseResponse resp = client.Get("SupplierCounter/node");

                Counter_class get = resp.ResultAs<Counter_class>();

                var data = new Supplier_Class
                {
                    Supplier_ID = (Convert.ToInt32(get.cnt) + 1).ToString(),
                    Supplier_Name = supname.Text,
                    Supplier_Address = supaddress.Text,
                    Supplier_Number = supnumber.Text,    
                    Supplier_DateAdded = date,

                };

                SetResponse response = client.Set("Supplier/" + data.Supplier_ID, data);
                Supplier_Class result = response.ResultAs<Supplier_Class>();

                var obj = new Counter_class
                {
                    cnt = data.Supplier_ID
                };

                SetResponse response1 = client.Set("SupplierCounter/node", obj);






                FirebaseResponse resp3 = client.Get("SupplierCounterExisting/node");
                Counter_class gett = resp3.ResultAs<Counter_class>();
                int exist = (Convert.ToInt32(gett.cnt) + 1);
                var obj2 = new Counter_class
                {
                    cnt = exist.ToString()
                };


                SetResponse response2 = client.Set("SupplierCounterExisting/node", obj2);







                this.Hide();


                try
                {
                    Suppliermanagement_module._instance.dataview();
                }
                catch
                {
                    ProcessOrder_popup._instance.refreshsupplier();
                }
                
            }

            else

            {
                //do something if NO
            }
        }

        private void Addsupplier_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            FirebaseResponse resp = client.Get("SupplierCounter/node");

            Counter_class get = resp.ResultAs<Counter_class>();

            var data = new Supplier_Class
            {
                Supplier_ID = (Convert.ToInt32(get.cnt) + 1).ToString(),

            };


            supid.Text = data.Supplier_ID;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
