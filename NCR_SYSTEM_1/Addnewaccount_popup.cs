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
    public partial class Addnewaccount_popup : Form
    {
        IFirebaseClient client;


        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"


        };



        public static string id;
        public static string user;
        public static string pass;
        public static string firstname;
        public static string lastname;
        public static string accountlvl;
        public static string dateadded;



        public Addnewaccount_popup()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox3_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox2_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void Addnewaccount_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            FirebaseResponse resp = client.Get("AccountCounter/node");

            Counter_class get = resp.ResultAs<Counter_class>();

            var data = new User_class
            {
                User_ID = (Convert.ToInt32(get.cnt) + 1).ToString(),

            };


            idtxt.Text = data.User_ID;

            namemetro(usertxt);
            namemetro(passtxt);
            namemetro(fnametxt);
            namemetro(lnametxt);
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if(usertxt.Text!="" && passtxt.Text != "" && fnametxt.Text != "" && lnametxt.Text != "" && leveltxt.Text != "")
            {
                String date = DateTime.Now.ToString("MM/dd/yyyy");

                id = idtxt.Text;
                user = usertxt.Text;
                pass = passtxt.Text;
                firstname = fnametxt.Text;
                lastname = lnametxt.Text;
                accountlvl = leveltxt.Text;
                dateadded = date;

                Addnewaccountuseraccess_popup a = new Addnewaccountuseraccess_popup();
                a.Show();
                this.Hide();
                
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

        private void usertxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void fnametxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void lnametxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void namemetro(Bunifu.Framework.UI.BunifuMetroTextbox metroTextbox)

        {
            foreach (var ctl in metroTextbox.Controls)
            {

                if (ctl.GetType() == typeof(TextBox))

                {
                    var txt = (TextBox)ctl;
                    txt.MaxLength = 25;

                }

            }

        }

    }
}
