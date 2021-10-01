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


        

            //Testing
            int i = 0;
            FirebaseResponse resp = client.Get("AccountCounter/node");
            Counter_class obj = resp.ResultAs<Counter_class>();
            int cnt = Convert.ToInt32(obj.cnt);


            while (i <= cnt)
            {
                if (i == cnt)
                {
                    
                  
                    panel3.Visible = true;
                    errormessage.Visible = true;
                    panel2.Visible = true;
                    bunifuCustomLabel1.Select();
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
                           
                            
                            panel3.Visible = true;
                            errormessage.Visible = true;
                            panel2.Visible = true;
                            bunifuCustomLabel1.Select();
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
            bunifuCustomLabel1.Select();
         

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void usertxt_MouseEnter(object sender, EventArgs e)
        {
        
        }

        private void usertxt_MouseClick(object sender, MouseEventArgs e)
        {
        
        }

        private void usertxt_Click(object sender, EventArgs e)
        {
          
        }

        private void usertxt_Enter(object sender, EventArgs e)
        {
            errormessage.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            if (usertxt.Text == "Username")
            {
                usertxt.Text = "";


                usertxt.ForeColor = Color.Black;

            }
        }

        private void usertxt_Leave(object sender, EventArgs e)
        {
            if (usertxt.Text == "")
            {
                usertxt.Text = "Username";
                usertxt.ForeColor = Color.Gray;


            }
        }

        private void passtxt_Enter(object sender, EventArgs e)
        {
            errormessage.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            if (passtxt.Text == "Password")
            {
                passtxt.Text = "";

                passtxt.ForeColor = Color.Black;

            }
        }

        private void passtxt_Leave(object sender, EventArgs e)
        {
            if (passtxt.Text == "")
            {
                passtxt.Text = "Password";
                passtxt.ForeColor = Color.Gray;


            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
