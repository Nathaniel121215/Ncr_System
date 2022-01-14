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
    public partial class Utility_settings_module : Form
    {
        public Utility_settings_module()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;


        public async void Utility_settings_module_Load(object sender, EventArgs e)
        {
            datedisplay.Text = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            datedisplay.Select();

            client = new FireSharp.FirebaseClient(config);



            //getting free item goal




            FirebaseResponse resp2 = await client.GetTaskAsync("FreeGoal/" + 1);
            FreeGoal obj2 = resp2.ResultAs<FreeGoal>();

            goaltxt.Text = obj2.Goal.ToString();


            FirebaseResponse resp3 = await client.GetTaskAsync("FreeLimit/" + 1);
            FreeLimit obj3 = resp3.ResultAs<FreeLimit>();

            limittxt.Text = obj3.Limit.ToString();

            //accountlvldisplay

            if (Form1.levelac == "Admin")
            {
                accountinfolvl.Text = "Login as Administrator";
            }
            else if (Form1.levelac == "Manager")
            {
                accountinfolvl.Text = "Login as Manager";
            }
            else
            {
                accountinfolvl.Text = "Login as Cashier";
            }


        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if(Form1.status=="true" & goaltxt.Text!="" && limittxt.Text != "")
            {
                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    int goal = Convert.ToInt32(goaltxt.Text);
                    int limit = Convert.ToInt32(limittxt.Text);

                    var data = new FreeGoal
                    {

                        FreeGID = "1",
                        Goal = goal,


                    };

                    FirebaseResponse response = client.Update("FreeGoal/" + data.FreeGID, data);
                    FreeGoal result = response.ResultAs<FreeGoal>();


                    var data2 = new FreeLimit
                    {

                        FreeLID = "1",
                        Limit = limit,


                    };

                    FirebaseResponse response2 = client.Update("FreeLimit/" + data2.FreeLID, data2);
                    FreeLimit result2 = response2.ResultAs<FreeLimit>();


                    //activity MODULE UPDATE EVENT

                    FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                    Counter_class get4 = resp4.ResultAs<Counter_class>();
                    int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                    var data3 = new ActivityLog_Class
                    {
                        Event_ID = cnt4.ToString(),
                        Module = "Utility Settings Module",
                        Action = "Goal and Limit Details Updated",
                        Date = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                        User = Form1.username,
                        Accountlvl = Form1.levelac,

                    };



                    FirebaseResponse response5 = client.Set("ActivityLog/" + data3.Event_ID, data3);



                    var obj4 = new Counter_class
                    {
                        cnt = data3.Event_ID

                    };

                    SetResponse response6 = client.Set("ActivityLogCounter/node", obj4);


                    MessageBox.Show("Goal and Limit Updated");
                }
                else
                {

                }
                    
            }
            else
            {
                MessageBox.Show("The Module is still loading or a window is currently open.");
            }
            

        }

        public async void bunifuFlatButton2_Click(object sender, EventArgs e)
        {

            FirebaseResponse resp2 = await client.GetTaskAsync("FreeGoal/" + 1);
            FreeGoal obj2 = resp2.ResultAs<FreeGoal>();

            goaltxt.Text = obj2.Goal.ToString();


            FirebaseResponse resp3 = await client.GetTaskAsync("FreeLimit/" + 1);
            FreeLimit obj3 = resp3.ResultAs<FreeLimit>();

            limittxt.Text = obj3.Limit.ToString();
        }

        public async void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if(Form1.status=="true" && bunifuMetroTextbox2.Text == "DATABASERESET")
            {
                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    //reset inventory
                    FirebaseResponse response = await client.DeleteTaskAsync("Inventory");

                    var obj1 = new Counter_class
                    {
                        cnt = "0"

                    };

                    SetResponse response1 = client.Set("Counter2/node", obj1);


                    var obj2 = new Counter_class
                    {
                        cnt = "0"

                    };

                    SetResponse response2 = client.Set("inventoryCounterExisting/node", obj2);


                    //reset Accounts
                    FirebaseResponse response3 = await client.DeleteTaskAsync("Accounts");

                    var obj3 = new Counter_class
                    {
                        cnt = "0"

                    };

                    SetResponse response4 = client.Set("AccountCounter/node", obj3);


                    var obj4 = new Counter_class
                    {
                        cnt = "0"

                    };

                    SetResponse response5 = client.Set("employeeCounterExisting/node", obj4);

                    //add account

                    FirebaseResponse resp = client.Get("AccountCounter/node");
                    Counter_class get = resp.ResultAs<Counter_class>();

                    var data = new User_class
                    {
                        User_ID = (Convert.ToInt32(get.cnt) + 1).ToString(),
                        Username = "Admin",
                        Password = "Admin",
                        Firstname = "Admin",
                        Lastname = "Admin",
                        Account_Level = "Admin",
                        Date_Added = DateTime.Now.ToString("MM/dd/yyyy"),

                     

                    };

                    SetResponse response40 = client.Set("Accounts/" + data.User_ID, data);
                    User_class result = response40.ResultAs<User_class>();

                    var obj50 = new Counter_class
                    {
                        cnt = data.User_ID
                    };

                    SetResponse response50 = client.Set("AccountCounter/node", obj50);






                    //reset Supplier
                    FirebaseResponse response6 = await client.DeleteTaskAsync("Supplier");

                    var obj6 = new Counter_class
                    {
                        cnt = "0"

                    };

                    SetResponse response7 = client.Set("SupplierCounter/node", obj6);


                    var obj88 = new Counter_class
                    {
                        cnt = "0"

                    };

                    SetResponse response99 = client.Set("SupplierCounterExisting/node", obj88);



                    //reset Unit
                    FirebaseResponse response8 = await client.DeleteTaskAsync("Unit");

                    var obj8 = new Counter_class
                    {
                        cnt = "0"

                    };

                    SetResponse response9 = client.Set("UnitCounter/node", obj8);


                    //reset Category
                    FirebaseResponse response10 = await client.DeleteTaskAsync("Category");

                    var obj10 = new Counter_class
                    {
                        cnt = "0"

                    };

                    SetResponse response11 = client.Set("CategoryCounter/node", obj10);


                    //reset Accountarchive
                    FirebaseResponse response12 = await client.DeleteTaskAsync("AccountArchive");

                    var obj12 = new Counter_class
                    {
                        cnt = "0"

                    };

                    SetResponse response13 = client.Set("AccountArchiveCounter/node", obj12);


                    //reset inventoryarchive
                    FirebaseResponse response14 = await client.DeleteTaskAsync("InventoryArchive");

                    var obj14 = new Counter_class
                    {
                        cnt = "0"

                    };

                    SetResponse response15 = client.Set("InventoryArchiveCounter/node", obj14);


                    //reset supplierarchive
                    FirebaseResponse response144 = await client.DeleteTaskAsync("SupplierArchive");

                    var obj144 = new Counter_class
                    {
                        cnt = "0"

                    };

                    SetResponse response154 = client.Set("SupplierArchiveCounter/node", obj144);

                    //reset Companysales
                    FirebaseResponse response16 = await client.DeleteTaskAsync("CompanySales");

                    var obj16 = new Counter_class
                    {
                        cnt = "0"

                    };

                    SetResponse response17 = client.Set("SalesCount/node", obj16);

                    //reset RestockLog
                    FirebaseResponse response18 = await client.DeleteTaskAsync("ReStockLog");

                    var obj18 = new Counter_class
                    {
                        cnt = "0"

                    };

                    SetResponse response19 = client.Set("ReStockCounter/node", obj18);

                    //reset StockAdjustment
                    FirebaseResponse response20 = await client.DeleteTaskAsync("StockAdjustment");

                    var obj20 = new Counter_class
                    {
                        cnt = "0"

                    };

                    SetResponse response21 = client.Set("StockAdjustmentCounter/node", obj20);

                    //reset Userlogin
                    FirebaseResponse response22 = await client.DeleteTaskAsync("UserLoginLog");

                    var obj22 = new Counter_class
                    {
                        cnt = "0"

                    };

                    SetResponse response23 = client.Set("UserLoginLogCounter/node", obj22);

                    //reset ActivityLog
                    FirebaseResponse response24 = await client.DeleteTaskAsync("ActivityLog");

                    var obj24 = new Counter_class
                    {
                        cnt = "0"

                    };

                    SetResponse response25 = client.Set("ActivityLogCounter/node", obj24);




                    //reset Brand
                    FirebaseResponse response67 = await client.DeleteTaskAsync("Brand");

                    var obj67 = new Counter_class
                    {
                        cnt = "0"

                    };

                    SetResponse response77 = client.Set("BrandCounter/node", obj67);





                    MessageBox.Show("DATABASE RESET DONE");
                    Application.Exit();

                }
                else
                {

                }
                    
            }
            else
            {
                if (Form1.status == "false")
                {
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }

                if (bunifuMetroTextbox2.Text != "DATABASERESET")
                {
                    MessageBox.Show("Wrong input detected.");
                }
                
               
            }
           


        }

        private void bunifuImageButton18_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    POS_module a = new POS_module();
                    this.Hide();
                    a.Show();

                    Form1.loadingtime = 9000;
                    Form1.status = "false";
                    Loading_popup b = new Loading_popup();
                    b.Show();
                }
                else
                {

                }


            }
            else if (Form1.levelac.Equals("Cashier") && Form1.status == "true")
            {
                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    POS_module a = new POS_module();
                    this.Hide();
                    a.Show();

                    Form1.loadingtime = 9000;
                    Form1.status = "false";
                    Loading_popup b = new Loading_popup();
                    b.Show();
                }
                else
                {

                }
            }
            else
            {
                if (Form1.status == "true")
                {
                    MessageBox.Show("Your Account do not have access in this module.");
                }
                else
                {
                    MessageBox.Show("The Module is still loading.");
                }
            }
        }

        private void goaltxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void limittxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void bunifuImageButton19_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true")
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    Dashboard_Module a = new Dashboard_Module();
                    this.Hide();
                    a.Show();

                    Form1.loadingtime = 9000;
                    Form1.status = "false";
                    Loading_popup b = new Loading_popup();
                    b.Show();
                }
                else
                {

                }


            }

            else
            {
                if (Form1.status == "true")
                {
                    MessageBox.Show("Your Account do not have access in this module.");
                }
                else
                {
                    MessageBox.Show("The Module is still loading.");
                }
            }
        }

        private void bunifuImageButton17_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    Inventory_Module a = new Inventory_Module();
                    this.Hide();
                    a.Show();

                    Form1.loadingtime = 9000;
                    Form1.status = "false";
                    Loading_popup b = new Loading_popup();
                    b.Show();
                }
                else
                {

                }


            }
       
            else
            {
                if (Form1.status == "true")
                {
                    MessageBox.Show("Your Account do not have access in this module.");
                }
                else
                {
                    MessageBox.Show("The Module is still loading.");
                }
            }
        }

        private void bunifuImageButton16_Click(object sender, EventArgs e)
        {
            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    Accountmanagement_Module a = new Accountmanagement_Module();
                    this.Hide();
                    a.Show();

                    Form1.loadingtime = 9000;
                    Form1.status = "false";
                    Loading_popup b = new Loading_popup();
                    b.Show();
                }
                else
                {

                }


            }

            else
            {
                if (Form1.status == "true")
                {
                    MessageBox.Show("Your Account do not have access in this module.");
                }
                else
                {
                    MessageBox.Show("The Module is still loading.");
                }
            }
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    Suppliermanagement_module a = new Suppliermanagement_module();
                    this.Hide();
                    a.Show();

                    Form1.loadingtime = 9000;
                    Form1.status = "false";
                    Loading_popup b = new Loading_popup();
                    b.Show();
                }
                else
                {

                }


            }
        
            else
            {
                //MessageBox.Show("Your account do not have access on this Module.");
            }
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    Salesrecord_module a = new Salesrecord_module();
                    this.Hide();
                    a.Show();

                    Form1.loadingtime = 9000;
                    Form1.status = "false";
                    Loading_popup b = new Loading_popup();
                    b.Show();
                }
                else
                {

                }


            }
          
            else
            {
                if (Form1.status == "true")
                {
                    MessageBox.Show("Your Account do not have access in this module.");
                }
                else
                {
                    MessageBox.Show("The Module is still loading.");
                }
            }
        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    ActivityLog_Module a = new ActivityLog_Module();
                    this.Hide();
                    a.Show();

                    Form1.loadingtime = 9000;
                    Form1.status = "false";
                    Loading_popup b = new Loading_popup();
                    b.Show();
                }
                else
                {

                }


            }

            else
            {
                if (Form1.status == "true")
                {
                    MessageBox.Show("Your Account do not have access in this module.");
                }
                else
                {
                    MessageBox.Show("The Module is still loading.");
                }
            }
        }

        private void bunifuImageButton8_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    InventoryArchive_Module a = new InventoryArchive_Module();
                    this.Hide();
                    a.Show();

                    Form1.loadingtime = 9000;
                    Form1.status = "false";
                    Loading_popup b = new Loading_popup();
                    b.Show();
                }
                else
                {

                }


            }

            else
            {
                if (Form1.status == "true")
                {
                    MessageBox.Show("Your Account do not have access in this module.");
                }
                else
                {
                    MessageBox.Show("The Module is still loading.");
                }
            }
        }

        private void bunifuImageButton10_Click(object sender, EventArgs e)
        {
            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    Utility_settings_module a = new Utility_settings_module();
                    this.Hide();
                    a.Show();

                    Form1.loadingtime = 9000;
                    Form1.status = "false";
                    Loading_popup b = new Loading_popup();
                    b.Show();
                }
                else
                {

                }


            }

            else
            {
                if (Form1.status == "true")
                {
                    MessageBox.Show("Your Account do not have access in this module.");
                }
                else
                {
                    MessageBox.Show("The Module is still loading.");
                }
            }
        }

        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

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

            }
        }
    }
}
