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
    public partial class Dashboard_Module : Form
    {

        IFirebaseClient client;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };


        public Dashboard_Module()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            Inventory_Module a = new Inventory_Module();
            this.Hide();
            a.Show();
        }

        private void Dashboard_Module_Load(object sender, EventArgs e)
        {
            

            datedisplay.Text = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            client = new FireSharp.FirebaseClient(config);

            //existing product
            FirebaseResponse resp3 = client.Get("inventoryCounterExisting/node");
            Counter_class gett = resp3.ResultAs<Counter_class>();
            string products = gett.cnt;

            numberofproductlabel.Text = products;

            //existing employee
            FirebaseResponse resp2 = client.Get("employeeCounterExisting/node");
            Counter_class gett2 = resp2.ResultAs<Counter_class>();
            string employee = gett2.cnt;

            existingemployee.Text = employee;









        }

        private void bunifuImageButton12_Click(object sender, EventArgs e)
        {
            Supplierrecord_module a = new Supplierrecord_module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton15_Click(object sender, EventArgs e)
        {
            Accountmanagement_Module a = new Accountmanagement_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {

        }

        private void existingemployee_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                System.Windows.Forms.Application.Exit();
            }

            else

            {
                //do something if NO
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            POS_module a = new POS_module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton18_Click(object sender, EventArgs e)
        {
           
            if(Form1.levelac.Equals("Admin") && Form1.status == "true")
            {
                POS_module a = new POS_module();
                this.Hide();
                a.Show();

                Form1.loadingtime = 9000;
                Form1.status = "false";
                Loading_popup b = new Loading_popup();
                b.Show();
            }
            else if(Form1.levelac.Equals("Employee") && Form1.posac.Equals("Authorized")  && Form1.status == "true")
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
                //MessageBox.Show("Your account do not have access on this Module.");
            }

        }

        private void bunifuImageButton17_Click(object sender, EventArgs e)
        {

            if(Form1.status=="true")
            {
                this.Hide();
                Inventory_Module a = new Inventory_Module();

                a.Show();
            }


           
        }

        private void bunifuImageButton16_Click(object sender, EventArgs e)
        {
            Accountmanagement_Module a = new Accountmanagement_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            Suppliermanagement_module a = new Suppliermanagement_module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            Supplierrecord_module a = new Supplierrecord_module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {
            //TIMEOUT LOG

            try
            {

                FirebaseResponse resp10 = client.Get("UserLoginLogCounter/node");
                Counter_class get10 = resp10.ResultAs<Counter_class>();
                int cnt10 = (Convert.ToInt32(get10.cnt));


                var data10 = new Timeout_Class
                {
                    Event_ID = cnt10.ToString(),
                    Timeout = DateTime.Now.ToString("hh:mm tt"),
                };

                FirebaseResponse response10 = client.Update("UserLoginLog/" + data10.Event_ID, data10);
                

            }

            catch (Exception b)
            {
                MessageBox.Show(b.ToString());
            }


            Application.Exit();
        }

        private void bunifuImageButton19_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            UserLoginLog_Module a = new UserLoginLog_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton8_Click(object sender, EventArgs e)
        {

            if (Form1.levelac.Equals("Admin"))
            {
                InventoryArchive_Module a = new InventoryArchive_Module();
                this.Hide();
                a.Show();
            }
          
            else
            {
                MessageBox.Show("Your account do not have access on this Module.");
            }
        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            if (Form1.levelac.Equals("Admin"))
            {
                ActivityLog_Module a = new ActivityLog_Module();
                this.Hide();
                a.Show();
            }
         
            else
            {
                MessageBox.Show("Your account do not have access on this Module.");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
