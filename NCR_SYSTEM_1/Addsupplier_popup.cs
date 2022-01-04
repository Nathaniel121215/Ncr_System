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
            if(supname.Text!="" && supaddress.Text != "" && supnumber.Text != "")
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


                    //Supplier Module ADD SUPPLIER EVENT

                    FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                    Counter_class get4 = resp4.ResultAs<Counter_class>();
                    int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                    var data3 = new ActivityLog_Class
                    {
                        Event_ID = cnt4.ToString(),
                        Module = "Supplier Management Module",
                        Action = "Supplier-ID: " + data.Supplier_ID + "   New Supplier Added",
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




                    this.Hide();


                    try
                    {
                        Suppliermanagement_module._instance.dataview();
                        Form1.status = "true";
                    }
                    catch
                    {
                        Form1.status = "false";
                        ProcessOrder_popup._instance.refreshsupplier();
                    }

                }

                else

                {
                    //do something if NO
                }
            }
            else
            {
                MessageBox.Show("Fill up all necessary fields.");
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

            namemetro(supname);
            namemetro(supaddress);
            namemetro(supnumber);
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

        private void namemetro(Bunifu.Framework.UI.BunifuMetroTextbox metroTextbox)

        {
            foreach (var ctl in metroTextbox.Controls)
            {

                if (ctl.GetType() == typeof(TextBox))

                {
                    var txt = (TextBox)ctl;
                    txt.MaxLength = 30;

                }

            }

        }

        private void supnumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
