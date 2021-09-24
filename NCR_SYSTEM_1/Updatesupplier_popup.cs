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


                    this.Hide();
                    Suppliermanagement_module._instance.dataview();

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

        private void Updatesupplier_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            supid.Text = Suppliermanagement_module.Supplier_ID;
            supname.Text = Suppliermanagement_module.Supplier_Name;
            supaddress.Text = Suppliermanagement_module.Supplier_Address;
            supnumber.Text = Suppliermanagement_module.Supplier_Number;
          
            
        }
    }
}
