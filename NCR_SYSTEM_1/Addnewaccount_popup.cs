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
                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    String date = DateTime.Now.ToString("MM/dd/yyyy");

                    id = idtxt.Text;
                    user = usertxt.Text;
                    pass = passtxt.Text;
                    firstname = fnametxt.Text;
                    lastname = lnametxt.Text;
                    accountlvl = leveltxt.Text;
                    dateadded = date;

                    FirebaseResponse resp = client.Get("AccountCounter/node");
                    Counter_class get = resp.ResultAs<Counter_class>();

                    var data = new User_class
                    {
                        User_ID = (Convert.ToInt32(get.cnt) + 1).ToString(),
                        Username = Addnewaccount_popup.user,
                        Password = Addnewaccount_popup.pass,
                        Firstname = Addnewaccount_popup.firstname,
                        Lastname = Addnewaccount_popup.lastname,
                        Account_Level = Addnewaccount_popup.accountlvl,
                        Date_Added = Addnewaccount_popup.dateadded,





                    };

                    SetResponse response = client.Set("Accounts/" + data.User_ID, data);
                    User_class result = response.ResultAs<User_class>();

                    var obj = new Counter_class
                    {
                        cnt = data.User_ID
                    };

                    SetResponse response1 = client.Set("AccountCounter/node", obj);

                    if (Addnewaccount_popup.accountlvl.Equals("Cashier") || Addnewaccount_popup.accountlvl.Equals("Manager"))
                    {


                        //add existing employee
                        FirebaseResponse resp2 = client.Get("employeeCounterExisting/node");
                        Counter_class get2 = resp2.ResultAs<Counter_class>();
                        string employee = (Convert.ToInt32(get2.cnt) + 1).ToString();
                        var obj2 = new Counter_class
                        {
                            cnt = employee
                        };

                        SetResponse response2 = client.Set("employeeCounterExisting/node", obj2);
                    }

                    //Activity Log ADDING ACCOUNT EVENT


                    FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                    Counter_class get4 = resp4.ResultAs<Counter_class>();
                    int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                    var data2 = new ActivityLog_Class
                    {
                        Event_ID = cnt4.ToString(),
                        Module = "Account Management Module",
                        Action = "Account-ID: " + data.User_ID + "   New Account Added",
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


                    this.Hide();
                    Accountmanagement_Module._instance.dataview();
                    Form1.status = "true";
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

        private void usertxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            
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
