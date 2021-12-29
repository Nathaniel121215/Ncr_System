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



        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
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

                Inventoryaccess = "Authorized",
                Posaccess = "Authorized",
                Recordaccess = "Authorized",
                Supplieraccess = "Authorized",



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



            MessageBox.Show("DATABASE RESET DONE");


        }
    }
}
