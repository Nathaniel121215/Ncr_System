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
    public partial class Addbrand_pop : Form
    {

        IFirebaseClient client;


        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"


        };


        public Addbrand_pop()
        {
            InitializeComponent();
        }

        private void Addbrand_pop_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            FirebaseResponse resp = client.Get("BrandCounter/node");

            Counter_class get = resp.ResultAs<Counter_class>();

            var data = new Brand_Class
            {
                Brand_ID = (Convert.ToInt32(get.cnt) + 1).ToString(),

            };


            idtxt.Text = data.Brand_ID;
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

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (brname.Text != "")
            {
                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    String date = DateTime.Now.ToString("MM/dd/yyyy");


                    FirebaseResponse resp = client.Get("BrandCounter/node");

                    Counter_class get = resp.ResultAs<Counter_class>();

                    var data = new Brand_Class
                    {
                        Brand_ID = (Convert.ToInt32(get.cnt) + 1).ToString(),
                        Brand_Name = brname.Text,
                        Date_Added = date,

                    };

                    SetResponse response = client.Set("Brand/" + data.Brand_ID, data);


                    var obj = new Counter_class
                    {
                        cnt = data.Brand_ID
                    };

                    SetResponse response1 = client.Set("BrandCounter/node", obj);

                    this.Hide();



                    //default
                    try
                    {

                        Addbrand_module._instance.DataViewAll();
                        Form1.status = "true";

                    }
                    catch
                    {
                        Form1.status = "false";


                    }



                    //add
                    try
                    {
                        Addnewproduct_popup._instance.refreshbrand();
                    }
                    catch
                    {



                    }

                    //update

                    try
                    {
                        Updateproduct_popup._instance.refreshbrand();
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
    }
}
