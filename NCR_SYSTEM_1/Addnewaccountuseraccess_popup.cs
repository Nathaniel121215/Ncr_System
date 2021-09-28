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
    public partial class Addnewaccountuseraccess_popup : Form
    {
        IFirebaseClient client;


        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"


        };


        public Addnewaccountuseraccess_popup()
        {
            InitializeComponent();
        }

        private void Addnewaccountuseraccess_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);


            if(Addnewaccount_popup.accountlvl.Equals("Admin"))
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
                inventorycb.Text = "Unauthorized";
                poscb.Text = "Unauthorized";
                recordcb.Text = "Unauthorized";
                supcb.Text = "Unauthorized";
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Please confirm before proceed" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {


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

                    Inventoryaccess = inventorycb.Text,
                    Posaccess = poscb.Text,
                    Recordaccess = recordcb.Text,
                    Supplieraccess = supcb.Text,



                };

                SetResponse response = client.Set("Accounts/" + data.User_ID, data);
                User_class result = response.ResultAs<User_class>();

                var obj = new Counter_class
                {
                    cnt = data.User_ID
                };

                SetResponse response1 = client.Set("AccountCounter/node", obj);

                if (inventorycb.SelectedItem.Equals("Employee"))
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




                this.Hide();
                Accountmanagement_Module._instance.dataview();
                Accountmanagement_Module.checker = "allow";
            }

            else

            {
                //do something if NO
            }
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
