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
    public partial class Updateaccountuseraccess : Form
    {
        IFirebaseClient client;


        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"


        };

        public Updateaccountuseraccess()
        {
            InitializeComponent();
        }

        private void Updateaccountuseraccess_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

           

            if (Updateaccount_popup.accountlvl.Equals("Admin"))
            {
                inventorycb.Text = "Authorized";
                poscb.Text = "Authorized";
                recordcb.Text = "Authorized";
                supcb.Text = "Authorized";

                inventorycb.Enabled = false;
                poscb.Enabled = false;
                recordcb.Enabled = false;
                supcb.Enabled = false;


            }
            else
            {
                inventorycb.Text = Accountmanagement_Module.inventory;
                poscb.Text = Accountmanagement_Module.Pos;
                supcb.Text = Accountmanagement_Module.Supplier;
                recordcb.Text = Accountmanagement_Module.Records;
            }



        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (inventorycb.Text != "" && poscb.Text != "" && recordcb.Text != "" && supcb.Text != "")
            {
                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    try
                    {
                        var data = new User_class
                        {

                            User_ID = Updateaccount_popup.id,
                            Username = Updateaccount_popup.user,
                            Password = Updateaccount_popup.pass,
                            Firstname = Updateaccount_popup.firstname,
                            Lastname = Updateaccount_popup.lastname,
                            Account_Level = Updateaccount_popup.accountlvl,
                            Date_Added = Updateaccount_popup.dateadded,

                            Inventoryaccess = inventorycb.Text,
                            Posaccess = poscb.Text,
                            Recordaccess = recordcb.Text,
                            Supplieraccess = supcb.Text,


                        };

                        FirebaseResponse response = client.Update("Accounts/" + data.User_ID, data);
                        User_class result = response.ResultAs<User_class>();

                        //deduct employee count
                        if (Updateaccount_popup.levelcheck.Equals("Employee") || Updateaccount_popup.accountlvl.Equals("Admin"))
                        {
                            FirebaseResponse resp2 = client.Get("employeeCounterExisting/node");
                            Counter_class get2 = resp2.ResultAs<Counter_class>();
                            string employee = (Convert.ToInt32(get2.cnt) - 1).ToString();
                            var obj2 = new Counter_class
                            {
                                cnt = employee
                            };

                            SetResponse response2 = client.Set("employeeCounterExisting/node", obj2);


                        }
                        //Add employee count
                        if (Updateaccount_popup.levelcheck.Equals("Admin") || Updateaccount_popup.accountlvl.Equals("Employee"))
                        {
                            FirebaseResponse resp2 = client.Get("employeeCounterExisting/node");
                            Counter_class get2 = resp2.ResultAs<Counter_class>();
                            string employee = (Convert.ToInt32(get2.cnt) + 1).ToString();
                            var obj2 = new Counter_class
                            {
                                cnt = employee
                            };

                            SetResponse response2 = client.Set("employeeCounterExisting/node", obj2);


                        }


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


                        

                        if(Form1.userid == data.User_ID)
                        {
                            Application.Exit();
                        }
                        else
                        {
                            this.Hide();
                            Accountmanagement_Module._instance.dataview();
                            Form1.status = "true";
                        }

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
    }
}
