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
    public partial class Addcategory_popup : Form
    {

        IFirebaseClient client;


        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"


        };
        public Addcategory_popup()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                String date = DateTime.Now.ToString("MM/dd/yyyy");


                FirebaseResponse resp = client.Get("CategoryCounter/node");

                Counter_class get = resp.ResultAs<Counter_class>();

                var data = new Category_Class
                {
                    Category_ID = (Convert.ToInt32(get.cnt) + 1).ToString(),
                    Category_Name = unitname.Text,
                    Date_Added = date,

                };

                SetResponse response = client.Set("Category/" + data.Category_ID, data);


                var obj = new Counter_class
                {
                    cnt = data.Category_ID
                };

                SetResponse response1 = client.Set("CategoryCounter/node", obj);

                this.Hide();



                //default
                try
                {
                    Addcategory_module._instance.DataViewAll();
                }
                catch
                {



                }

               

                //add
                try
                {
                    Addnewproduct_popup._instance.refreshcategory();
                }
                catch
                {
                    


                }

                //update

                try
                {
                    Updateproduct_popup._instance.refreshcategory();
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

        private void Addcategory_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            FirebaseResponse resp = client.Get("CategoryCounter/node");

            Counter_class get = resp.ResultAs<Counter_class>();

            var data = new Category_Class
            {
                Category_ID = (Convert.ToInt32(get.cnt) + 1).ToString(),

            };


            idtxt.Text = data.Category_ID;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
