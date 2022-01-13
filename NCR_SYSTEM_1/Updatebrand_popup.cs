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
    public partial class Updatebrand_popup : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public Updatebrand_popup()
        {
            InitializeComponent();
        }

        private void Updatebrand_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            idtxt.Text = Addbrand_module.Brand_ID.ToString();
            unitname.Text = Addbrand_module.Brand_Name.ToString();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (unitname.Text != "")
            {
                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    string date = Addbrand_module.Date_Added.ToString();
                    try
                    {
                        var data = new Brand_Class
                        {

                            Brand_ID = idtxt.Text,
                            Brand_Name = unitname.Text,
                            Date_Added = date,



                        };

                        FirebaseResponse response = client.Update("Brand/" + data.Brand_ID, data);
                        User_class result = response.ResultAs<User_class>();

                        this.Hide();
                        Addbrand_module._instance.DataViewAll();
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
    }
}
