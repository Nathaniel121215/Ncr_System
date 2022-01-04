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
    public partial class Updatesupplier_popup : Form
    {
        IFirebaseClient client;


        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"


        };

        public Updatesupplier_popup()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (supname.Text != "" && supaddress.Text != "" && supnumber.Text != "")
            {
                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    try
                    {
                        var data = new Supplier_Class
                        {

                            Supplier_ID = supid.Text,
                            Supplier_Name = supname.Text,
                            Supplier_Address = supaddress.Text,
                            Supplier_Number = supnumber.Text,
                            Supplier_LastTransaction = Suppliermanagement_module.Last_Transaction,
                            Supplier_DateAdded = Suppliermanagement_module.Date_Added,



                        };

                        FirebaseResponse response = client.Update("Supplier/" + data.Supplier_ID, data);
                        User_class result = response.ResultAs<User_class>();


                        //SUPPLIER MODULE UPDATE EVENT

                        FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                        Counter_class get4 = resp4.ResultAs<Counter_class>();
                        int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                        var data3 = new ActivityLog_Class
                        {
                            Event_ID = cnt4.ToString(),
                            Module = "Supplier Mangement Module",
                            Action = "Supplier-ID: " + data.Supplier_ID + "   Supplier Details Updated",
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
                        Suppliermanagement_module._instance.dataview();
                        Form1.status = "true";

                    }

                    catch
                    {

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

        private void Updatesupplier_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            supid.Text = Suppliermanagement_module.Supplier_ID;
            supname.Text = Suppliermanagement_module.Supplier_Name;
            supaddress.Text = Suppliermanagement_module.Supplier_Address;
            supnumber.Text = Suppliermanagement_module.Supplier_Number;
          
            
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

        private void supnumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
