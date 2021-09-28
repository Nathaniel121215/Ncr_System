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
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
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

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Accountmanagement_Module.checker = "allow";
            this.Hide();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Accountmanagement_Module.checker = "allow";
            this.Hide();
        }
    }
}
