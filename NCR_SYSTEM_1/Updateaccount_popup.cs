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

            if(Accountmanagement_Module.Account_Level.ToString() == "Admin")
            {
                leveltxt.Items.Add("Admin");
                leveltxt.Text = "Admin";
                leveltxt.Enabled = false;

            }
            else
            {

            }

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (usertxt.Text != "" && passtxt.Text != "" && fnametxt.Text != "" && lnametxt.Text != "" && leveltxt.Text != "")
            {
                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    id = idtxt.Text;
                    user = usertxt.Text;
                    pass = passtxt.Text;
                    firstname = fnametxt.Text;
                    lastname = lnametxt.Text;
                    accountlvl = leveltxt.Text;
                    dateadded = Accountmanagement_Module.Date_Added;


                    var data = new User_class
                    {

                        User_ID = Updateaccount_popup.id,
                        Username = Updateaccount_popup.user,
                        Password = Updateaccount_popup.pass,
                        Firstname = Updateaccount_popup.firstname,
                        Lastname = Updateaccount_popup.lastname,
                        Account_Level = Updateaccount_popup.accountlvl,
                        Date_Added = Updateaccount_popup.dateadded,




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
                        Module = "Account Management Module",
                        Action = "Account-ID: " + data.User_ID + "   Account Details Updated",
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


                        Form1 a = new Form1();
                        this.Hide();
                        a.Show();
                    }
                    else
                    {
                        this.Hide();
                        Accountmanagement_Module._instance.dataview();
                        Form1.status = "true";
                    }
                }
                else
                {

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

        private void fnametxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                base.OnKeyPress(e);
            }
        }

        private void lnametxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                base.OnKeyPress(e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (usertxt.Text != "" && passtxt.Text != "" && fnametxt.Text != "" && lnametxt.Text != "" && leveltxt.Text != "")
            {
                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    id = idtxt.Text;
                    user = usertxt.Text;
                    pass = passtxt.Text;
                    firstname = fnametxt.Text;
                    lastname = lnametxt.Text;
                    accountlvl = leveltxt.Text;
                    dateadded = Accountmanagement_Module.Date_Added;


                    var data = new User_class
                    {

                        User_ID = Updateaccount_popup.id,
                        Username = Updateaccount_popup.user,
                        Password = "Password",
                        Firstname = Updateaccount_popup.firstname,
                        Lastname = Updateaccount_popup.lastname,
                        Account_Level = Updateaccount_popup.accountlvl,
                        Date_Added = Updateaccount_popup.dateadded,




                    };

                    FirebaseResponse response = client.Update("Accounts/" + data.User_ID, data);
                    User_class result = response.ResultAs<User_class>();

                    ////deduct employee count
                    //if (Updateaccount_popup.levelcheck.Equals("Employee") || Updateaccount_popup.accountlvl.Equals("Admin"))
                    //{
                    //    FirebaseResponse resp2 = client.Get("employeeCounterExisting/node");
                    //    Counter_class get2 = resp2.ResultAs<Counter_class>();
                    //    string employee = (Convert.ToInt32(get2.cnt) - 1).ToString();
                    //    var obj2 = new Counter_class
                    //    {
                    //        cnt = employee
                    //    };

                    //    SetResponse response2 = client.Set("employeeCounterExisting/node", obj2);


                    //}
                    ////Add employee count
                    //if (Updateaccount_popup.levelcheck.Equals("Admin") || Updateaccount_popup.accountlvl.Equals("Employee"))
                    //{
                    //    FirebaseResponse resp2 = client.Get("employeeCounterExisting/node");
                    //    Counter_class get2 = resp2.ResultAs<Counter_class>();
                    //    string employee = (Convert.ToInt32(get2.cnt) + 1).ToString();
                    //    var obj2 = new Counter_class
                    //    {
                    //        cnt = employee
                    //    };

                    //    SetResponse response2 = client.Set("employeeCounterExisting/node", obj2);


                    //}


                    //Activity Log UPDATING ACCOUNT EVENT


                    FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                    Counter_class get4 = resp4.ResultAs<Counter_class>();
                    int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                    var data2 = new ActivityLog_Class
                    {
                        Event_ID = cnt4.ToString(),
                        Module = "Account Management Module",
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


                        Form1 a = new Form1();
                        this.Hide();
                        a.Show();
                    }
                    else
                    {
                        this.Hide();
                        Accountmanagement_Module._instance.dataview();
                        Form1.status = "true";
                    }
                }
                else
                {

                }
                  

            }
            else
            {
                MessageBox.Show("Fill up all necessary fields.");
            }
        }
    }
}
