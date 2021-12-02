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

        bool a = true;
        public static string username;
        public static string inventoryac;
        public static string posac;
        public static string supplierac;
        public static string recordsac;
        public static string levelac;


        public static int loadingtime = 5000;
        public static string status;
        public static string session;

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
                   

                    usertxt.LineIdleColor = Color.Red;
                    passtxt.LineIdleColor = Color.Red;

                    break;
                }

                i++;
                try
                {
                    //normal account
                    FirebaseResponse resp1 = client.Get("Accounts/" + i);
                    User_class obj2 = resp1.ResultAs<User_class>();


                    //Perma account
                    FirebaseResponse resp2 = client.Get("PermanentAccount/" + 1);
                    User_class obj3 = resp2.ResultAs<User_class>();


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


                            //LOGIN LOG

                            FirebaseResponse resp7 = client.Get("UserLoginLogCounter/node");
                            Counter_class get7 = resp7.ResultAs<Counter_class>();
                            int cnt7 = (Convert.ToInt32(get7.cnt) + 1);



                            var data7 = new LoginLog_Class
                            {
                                Event_ID = cnt7.ToString(),
                                Date = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                                Timein = DateTime.Now.ToString("hh:mm tt"),
                                Timeout = "None",
                                User = Form1.username,
                                Accountlvl = Form1.levelac,

                            };



                            FirebaseResponse response7 = client.Set("UserLoginLog/" + data7.Event_ID, data7);



                            var obj7 = new Counter_class
                            {
                                cnt = data7.Event_ID

                            };

                            SetResponse response8 = client.Set("UserLoginLogCounter/node", obj7);





                            Dashboard_Module a = new Dashboard_Module();
                            this.Hide();
                            a.Show();

                            loadingtime = 5000;
                            status = "false";
                            session = data7.Event_ID;
                            Loading_popup b = new Loading_popup();
                            b.Show();
                            

                            break;


                       
                        }

                        else
                        {
                           
                            
                           
                        }
                    }
                    else if(usertxt.Text == obj3.Username )
                    {
                        if (passtxt.Text == obj3.Password)
                        {

                            inventoryac = obj3.Inventoryaccess;
                            posac = obj3.Posaccess;
                            supplierac = obj3.Supplieraccess;
                            recordsac = obj3.Recordaccess;

                            levelac = obj3.Account_Level;


                            MessageBox.Show("Logged in successfully");
                            username = obj3.Firstname + " " + obj3.Lastname;
                            Dashboard_Module a = new Dashboard_Module();
                            this.Hide();
                            a.Show();
                            break;

                        }

                        else
                        {


                        
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
            usertxt.LineIdleColor = Color.FromArgb(49, 129, 255);
            passtxt.LineIdleColor = Color.FromArgb(49, 129, 255);


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
            usertxt.LineIdleColor = Color.FromArgb(49, 129, 255);
            passtxt.LineIdleColor = Color.FromArgb(49, 129, 255);

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

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
           if( a ==true)
            {
                bunifuImageButton2.Image = Properties.Resources.eye_3;
                a = false;
                passtxt.isPassword = false;
            }
            else
            {
                bunifuImageButton2.Image = Properties.Resources.eye_1;
                a = true;
                passtxt.isPassword = true;
            }

           


        }

   

        private void bunifuFlatButton2_Click_1(object sender, EventArgs e)
        {
            Salesrecord_module a = new Salesrecord_module();
            this.Hide();
            a.Show();
        }
    }
}
