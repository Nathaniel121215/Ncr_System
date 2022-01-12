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
    public partial class ChangePassword_popup : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };


        IFirebaseClient client;


        public ChangePassword_popup()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

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

        private void ChangePassword_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);


           
            FirebaseResponse resp1 = client.Get("Accounts/" + Form1.userid);
            User_class obj2 = resp1.ResultAs<User_class>();


            usertxt.Text = obj2.Username;
            passtxt1.Text = "Password";
            passtxt2.Text = "Password";

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (usertxt.Text != "" && passtxt1.Text != "" && passtxt2.Text != "" && passtxt1.Text == passtxt2.Text && passtxt1.Text !="Password" && passtxt2.Text != "Password")
            {
                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    FirebaseResponse resp1 = client.Get("Accounts/" + Form1.userid);
                    User_class obj2 = resp1.ResultAs<User_class>();


                    var data = new User_class
                    {

                        User_ID = Form1.userid,
                        Username = usertxt.Text,
                        Password = passtxt2.Text,
                        Firstname = obj2.Firstname,
                        Lastname = obj2.Lastname,
                        Account_Level = Form1.levelac,
                        Date_Added = obj2.Date_Added,


                    };

                    FirebaseResponse response = client.Update("Accounts/" + data.User_ID, data);
                    User_class result = response.ResultAs<User_class>();




                    //Activity Log UPDATING ACCOUNT EVENT


                    FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                    Counter_class get4 = resp4.ResultAs<Counter_class>();
                    int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                    var data2 = new ActivityLog_Class
                    {
                        Event_ID = cnt4.ToString(),
                        Module = "Account Setup Module",
                        Action = "Account-ID: " + data.User_ID + "   Account Password Reset",
                        Date = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                        User = Form1.username,
                        Accountlvl = Form1.levelac,

                    };



                    FirebaseResponse response5 = client.Set("ActivityLog/" + data2.Event_ID, data2);



                    var obj4 = new Counter_class
                    {
                        cnt = data2.Event_ID

                    };

                    SetResponse response6 = client.Set("ActivityLogCounter/node", obj4);




                    if (Form1.userid == data.User_ID)
                    {

                        //TIMEOUT LOG

                        try
                        {

                            var data10 = new Timeout_Class
                            {
                                Event_ID = Form1.session,
                                Timeout = DateTime.Now.ToString("hh:mm tt"),
                            };

                            FirebaseResponse response10 = client.Update("UserLoginLog/" + data10.Event_ID, data10);


                        }

                        catch (Exception b)
                        {
                            MessageBox.Show(b.ToString());
                        }

                        if (Form1.levelac == "Admin" || Form1.levelac == "Manager")
                        {
                            Dashboard_Module._instance.close();
                        }
                        else
                        {
                            POS_module._instance.close();
                        }


                        Form1 a = new Form1();
                        this.Hide();
                        a.Show();

                       
                        

                    }
                    else
                    {

                    }


                }
                else
                {
                    
                }
            }
                else
                {
                MessageBox.Show("Fill up all necessary fields correctly.");
            }
            
                   
        }

        private void passtxt1_Enter(object sender, EventArgs e)
        {
            if (passtxt1.Text == "Password")
            {
                passtxt1.Text = "";

                passtxt1.ForeColor = Color.Black;

            }
        }

        private void passtxt1_Leave(object sender, EventArgs e)
        {
            if (passtxt1.Text == "")
            {
                passtxt1.Text = "Password";
                passtxt1.ForeColor = Color.Gray;


            }
        }

        private void passtxt2_Enter(object sender, EventArgs e)
        {
            if (passtxt2.Text == "Password")
            {
                passtxt2.Text = "";

                passtxt2.ForeColor = Color.Black;

            }
        }

        private void passtxt2_Leave(object sender, EventArgs e)
        {
            if (passtxt2.Text == "")
            {
                passtxt2.Text = "Password";
                passtxt2.ForeColor = Color.Gray;


            }
        }
    }
}
