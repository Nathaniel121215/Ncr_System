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
    public partial class Updatecategory_popup : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public Updatecategory_popup()
        {
            InitializeComponent();
        }

        private void Updatecategory_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            idtxt.Text = Addcategory_module.Category_ID.ToString();
            unitname.Text = Addcategory_module.Category_Name.ToString();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                string date = Addcategory_module.Date_Added.ToString();
                try
                {
                    var data = new Category_Class
                    {

                        Category_ID = idtxt.Text,
                        Category_Name = unitname.Text,
                        Date_Added = date,



                    };

                    FirebaseResponse response = client.Update("Category/" + data.Category_ID, data);
                    User_class result = response.ResultAs<User_class>();

                    this.Hide();
                    Addcategory_module._instance.DataViewAll();


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
