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

            inventorycb.Text = Accountmanagement_Module.inventory;
            poscb.Text = Accountmanagement_Module.Pos;
            supcb.Text = Accountmanagement_Module.Supplier;
            recordcb.Text = Accountmanagement_Module.Records;



            

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
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



                    this.Hide();
                    Accountmanagement_Module._instance.dataview();

                }

                catch (Exception b)
                {

                }
            }

            else

            {
                //do something if NO
            }
        }
    }
}
