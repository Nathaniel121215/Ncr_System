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
    public partial class Addunit_popup : Form
    {
        IFirebaseClient client;


        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"


        };

        public Addunit_popup()
        {
            InitializeComponent();
        }

        private void Addunit_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            FirebaseResponse resp = client.Get("UnitCounter/node");

            Counter_class get = resp.ResultAs<Counter_class>();

            var data = new Unit_Class
            {
                Unit_ID = (Convert.ToInt32(get.cnt) + 1).ToString(),

            };


            idtxt.Text = data.Unit_ID;

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                String date = DateTime.Now.ToString("MM/dd/yyyy");


                FirebaseResponse resp = client.Get("UnitCounter/node");

                Counter_class get = resp.ResultAs<Counter_class>();

                var data = new Unit_Class
                {
                    Unit_ID = (Convert.ToInt32(get.cnt) + 1).ToString(),
                    Unit_Name = unitname.Text,
                    Date_Added = date,

                };

                SetResponse response = client.Set("Unit/" + data.Unit_ID, data);
                User_class result = response.ResultAs<User_class>();

                var obj = new Counter_class
                {
                    cnt = data.Unit_ID
                };

                SetResponse response1 = client.Set("UnitCounter/node", obj);

                this.Hide();



                //default
                try
                {
                    Addunit_module._instance.DataViewAll();
                }
                catch
                {



                }
                



                //add
                try
                {
                    Addnewproduct_popup._instance.refreshunit();
                }
                catch
                {



                }

                //update

                try
                {
                    Updateproduct_popup._instance.refreshunit();
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

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
