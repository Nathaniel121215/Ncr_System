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
        int salescount;
        int inventorycount;

        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();


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

            dt.Columns.Add("Purchase ID");
            dt.Columns.Add("Transaction Date");
            dt.Columns.Add("Transaction Payment");


            Sales_Datagrid.DataSource = dt;


            dataviewtransaction();



            dt2.Columns.Add("Product ID");
            dt2.Columns.Add("Stock");
            dt2.Columns.Add("Low");
            dt2.Columns.Add("High");
            dt2.Columns.Add("String Indicator");


            inventory.DataSource = dt2;


            dataviewstock();

        }

        public async void dataviewtransaction()
        {
            foreach (DataGridViewColumn column in Sales_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            this.Sales_Datagrid.AllowUserToAddRows = false;

            try
            {
                int i = 0;
                FirebaseResponse resp = await client.GetTaskAsync("SalesCount/node");
                Counter_class obj = resp.ResultAs<Counter_class>();
                int cnt = Convert.ToInt32(obj.cnt);

                while (true)
                {
                    if (i == cnt)
                    {
                        break;
                    }

                    i++;
                    try
                    {

                        FirebaseResponse resp1 = await client.GetTaskAsync("CompanySales/" + i);
                        Purchase_Class obj1 = resp1.ResultAs<Purchase_Class>();

                        DataRow r = dt.NewRow();
                        r["Purchase ID"] = obj1.Purchase_ID;
                        DateTime date = Convert.ToDateTime(obj1.Date_Of_Transaction);
                        r["Transaction Payment"] = obj1.Order_Total;
                        r["Transaction Date"] = date.ToString("MM/dd/yyyy");

                        dt.Rows.Add(r);
                    }

                    catch
                    {

                    }
                }
            }
            catch
            {

            }

            string datesearch  = DateTime.Now.ToString("MM/dd/yyyy");

            DataView dv = new DataView(dt);

            dv.RowFilter = "[Transaction Date] LIKE '%" + datesearch + "%'";

            Sales_Datagrid.DataSource = null;
            Sales_Datagrid.Rows.Clear();
            Sales_Datagrid.Columns.Clear();
            Sales_Datagrid.DataSource = dv;

            salescount = Sales_Datagrid.RowCount;

            trasaccount.Text = salescount.ToString();

            decimal totalsales = 0;


            for (int a = 0; a < Sales_Datagrid.Rows.Count; a++)
            {
                totalsales += Convert.ToDecimal(Sales_Datagrid.Rows[a].Cells[2].Value);
            }

            todaysales.Text = totalsales.ToString();

        }

        public async void dataviewstock()
        {

            foreach (DataGridViewColumn column in inventory.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            this.inventory.AllowUserToAddRows = false;


            dt2.Rows.Clear();

            int i = 0;
            FirebaseResponse resp = await client.GetTaskAsync("Counter2/node");
            Counter_class obj = resp.ResultAs<Counter_class>();
            int cnt = Convert.ToInt32(obj.cnt);

            while (true)
            {
                if (i == cnt)
                {
                    break;
                }

                i++;
                try
                {

                    FirebaseResponse resp1 = await client.GetTaskAsync("Inventory/" + i);
                    Product_class obj1 = resp1.ResultAs<Product_class>();

                    DataRow r = dt2.NewRow();
                    r["Product ID"] = obj1.ID;
                    r["Stock"] = obj1.Stock;
                    r["Low"] = obj1.Low;
                    r["High"] = obj1.High;


                    dt2.Rows.Add(r);



                }

                catch
                {

                }
            }


            try
            {

                foreach (DataGridViewRow row in inventory.Rows)
                {

                    try
                    {


                        if (Convert.ToInt32(row.Cells[1].Value) <= Convert.ToInt32(row.Cells[2].Value) && Convert.ToInt32(row.Cells[1].Value) != 0) //Low on stock
                        {

                            row.Cells[4].Value = "low on stock";

                        }

                        if (Convert.ToInt32(row.Cells[1].Value) > Convert.ToInt32(row.Cells[2].Value) && Convert.ToInt32(row.Cells[1].Value) < Convert.ToInt32(row.Cells[3].Value))
                        {
                    
                            row.Cells[4].Value = "in stock";
                        }

                        if (Convert.ToInt32(row.Cells[1].Value) >= Convert.ToInt32(row.Cells[3].Value))
                        {
                           
                            row.Cells[4].Value = "high on stock";
                        }

                        if (Convert.ToInt32(row.Cells[1].Value).Equals(0)) //out of stock
                        {
                      
                            row.Cells[4].Value = "out of stock";
                        }


                    }
                    catch
                    {

                    }

                }
            }
            catch
            {

            }


///////////////////////////////////////////////////////
            DataView dv = new DataView(dt2);

            dv.RowFilter = "[String Indicator] LIKE '%" + "low on stock" + "%'";

            inventory.DataSource = null;
            inventory.Rows.Clear();
            inventory.Columns.Clear();
            inventory.DataSource = dv;

            inventorycount = inventory.RowCount;

            low.Text = inventorycount.ToString();

////////////////////////////////////////////////////////////////

            DataView dv2 = new DataView(dt2);

            dv2.RowFilter = "[String Indicator] LIKE '%" + "in stock" + "%'";

            inventory.DataSource = null;
            inventory.Rows.Clear();
            inventory.Columns.Clear();
            inventory.DataSource = dv2;

            inventorycount = inventory.RowCount;

            instock.Text = inventorycount.ToString();



            ////////////////////////////////////////////////////////////////

            DataView dv3 = new DataView(dt2);

            dv3.RowFilter = "[String Indicator] LIKE '%" + "high on stock" + "%'";

            inventory.DataSource = null;
            inventory.Rows.Clear();
            inventory.Columns.Clear();
            inventory.DataSource = dv3;

            inventorycount = inventory.RowCount;

            highstock.Text = inventorycount.ToString();


            ////////////////////////////////////////////////////////////////

            DataView dv4 = new DataView(dt2);

            dv4.RowFilter = "[String Indicator] LIKE '%" + "out of stock" + "%'";

            inventory.DataSource = null;
            inventory.Rows.Clear();
            inventory.Columns.Clear();
            inventory.DataSource = dv4;

            inventorycount = inventory.RowCount;

            outstock.Text = inventorycount.ToString();

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
            
                if (Form1.levelac.Equals("Admin") && Form1.status == "true")
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
                else if (Form1.levelac.Equals("Employee") && Form1.posac.Equals("Authorized") && Form1.status == "true")
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
                    //MessageBox.Show("Your account do not have access on this Module.");
                }
            
            

        }

        private void bunifuImageButton17_Click(object sender, EventArgs e)
        {


            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
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
            else if (Form1.levelac.Equals("Employee") && Form1.inventoryac.Equals("Authorized") && Form1.status == "true")
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
                //MessageBox.Show("Your account do not have access on this Module.");
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
                //MessageBox.Show("Your account do not have access on this Module.");
            }
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {


            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
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
            else if (Form1.levelac.Equals("Employee") && Form1.supplierac.Equals("Authorized") && Form1.status == "true")
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

            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
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
            else if (Form1.levelac.Equals("Employee") && Form1.recordsac.Equals("Authorized") && Form1.status == "true")
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
                //MessageBox.Show("Your account do not have access on this Module.");
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
                //MessageBox.Show("Your account do not have access on this Module.");
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            UserLoginLog_Module a = new UserLoginLog_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton8_Click(object sender, EventArgs e)
        {


            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
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
                //MessageBox.Show("Your account do not have access on this Module.");
            }
        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {

            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
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
                //MessageBox.Show("Your account do not have access on this Module.");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            Stockadjustmentrecord_module a = new Stockadjustmentrecord_module();
            this.Hide();
            a.Show();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }
    }
}
