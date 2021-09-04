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
    public partial class Form1 : Form
    {

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };


        IFirebaseClient client;

        public static string username;
        public static string inventoryac;
        public static string posac;
        public static string supplierac;
        public static string recordsac;
        public static string levelac;

        public Form1()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

            int i = 0;
            FirebaseResponse resp = client.Get("AccountCounter/node");
            Counter_class obj = resp.ResultAs<Counter_class>();
            int cnt = Convert.ToInt32(obj.cnt);


            while (i <= cnt)
            {
                if (i == cnt)
                {
                    MessageBox.Show("Invalid login");
                    break;
                }

                i++;
                try
                {

                    FirebaseResponse resp1 = client.Get("Accounts/" + i);
                    User_class obj2 = resp1.ResultAs<User_class>();




                    if (usertxt.Text == obj2.Username)
                    {
                        if (passtxt.Text == obj2.Password)
                        {

                            inventoryac = obj2.Inventoryaccess;
                            posac = obj2.Posaccess;
                            supplierac = obj2.Supplieraccess;
                            recordsac = obj2.Recordaccess;

                            levelac = obj2.Account_Level;


                            MessageBox.Show("Logged in successfully");
                            username = obj2.Firstname + " " + obj2.Lastname;
                            Dashboard_Module a = new Dashboard_Module();
                            this.Hide();
                            a.Show();
                            break;
                            
                        }
                        else
                        {
                            MessageBox.Show("Invalid login");
                            break;
                        }
                    }
                    else
                    {

                    }


                }

                catch
                {

                }
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
