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
    public partial class Updateaccount_popup : Form
    {

        public static string id;
        public static string user;
        public static string pass;
        public static string firstname;
        public static string lastname;
        public static string accountlvl;
        public static string dateadded;


        public static string levelcheck;
   

        IFirebaseClient client;


        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"


        };


        public Updateaccount_popup()
        {
            InitializeComponent();
        }

        private void Updateaccount_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            idtxt.Text = Accountmanagement_Module.User_ID;
            usertxt.Text = Accountmanagement_Module.Username;
            passtxt.Text = Accountmanagement_Module.Password;
            fnametxt.Text = Accountmanagement_Module.Firstname;
            lnametxt.Text = Accountmanagement_Module.Lastname;
            leveltxt.Text = Accountmanagement_Module.Account_Level.ToString();
            levelcheck = Accountmanagement_Module.Account_Level.ToString();


            namemetro(usertxt);
            namemetro(passtxt);
            namemetro(fnametxt);
            namemetro(lnametxt);

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (usertxt.Text != "" && passtxt.Text != "" && fnametxt.Text != "" && lnametxt.Text != "" && leveltxt.Text != "")
            {
                id = idtxt.Text;
                user = usertxt.Text;
                pass = passtxt.Text;
                firstname = fnametxt.Text;
                lastname = lnametxt.Text;
                accountlvl = leveltxt.Text;
                dateadded = Accountmanagement_Module.Date_Added;



                Updateaccountuseraccess a = new Updateaccountuseraccess();
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
