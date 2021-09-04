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
    public partial class Updateunit_popup : Form
    {

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;


        public Updateunit_popup()
        {
            InitializeComponent();
        }

        private void Updateunit_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            idtxt.Text = Addunit_module.Unit_ID.ToString();
            unitname.Text = Addunit_module.Unit_Name.ToString();
           

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                string date = Addunit_module.Date_Added.ToString();
                try
                {
                    var data = new Unit_Class
                    {

                        Unit_ID = idtxt.Text,
                        Unit_Name = unitname.Text,
                        Date_Added = date,



                    };

                    FirebaseResponse response = client.Update("Unit/" + data.Unit_ID, data);
                    User_class result = response.ResultAs<User_class>();

                    this.Hide();
                    Addunit_module._instance.DataViewAll();


                }

                catch (Exception b)
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void unitname_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void idtxt_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
