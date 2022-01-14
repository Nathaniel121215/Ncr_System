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

        public static Dashboard_Module _instance;

        public Dashboard_Module()
        {
            InitializeComponent();
            _instance = this;
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
            dt.Columns.Add("MonthYear");
            dt.Columns.Add("Year");


            Sales_Datagrid.DataSource = dt;


            dataviewtransaction();



            dt2.Columns.Add("Product Name");
            dt2.Columns.Add("Stock");
            dt2.Columns.Add("Low");
            dt2.Columns.Add("High");
            dt2.Columns.Add("String Indicator");
            dt2.Columns.Add("Item Sold", typeof(Int32));
            dt2.Columns.Add("test");


            inventory.DataSource = dt2;


            dataviewstock();

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

                        r["MonthYear"] = date.ToString("MM/yyyy");
                        r["Year"] = date.ToString("yyyy");
       

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

            //////MONTHLY
            ///

            string datesearch2 = DateTime.Now.ToString("MM/yyyy");

            DataView dv2 = new DataView(dt);

            dv2.RowFilter = "[MonthYear] LIKE '%" + datesearch2 + "%'";

            Sales_Datagrid.DataSource = null;
            Sales_Datagrid.Rows.Clear();
            Sales_Datagrid.Columns.Clear();
            Sales_Datagrid.DataSource = dv2;

            decimal monthly = 0;


            for (int a = 0; a < Sales_Datagrid.Rows.Count; a++)
            {
                monthly += Convert.ToDecimal(Sales_Datagrid.Rows[a].Cells[2].Value);
            }

            month.Text = monthly.ToString();


            //////Yearly
            ///

            string datesearch3 = DateTime.Now.ToString("yyyy");

            DataView dv3 = new DataView(dt);

            dv3.RowFilter = "[Year] LIKE '%" + datesearch3 + "%'";

            Sales_Datagrid.DataSource = null;
            Sales_Datagrid.Rows.Clear();
            Sales_Datagrid.Columns.Clear();
            Sales_Datagrid.DataSource = dv3;

            decimal yearly = 0;


            for (int a = 0; a < Sales_Datagrid.Rows.Count; a++)
            {
                yearly += Convert.ToDecimal(Sales_Datagrid.Rows[a].Cells[2].Value);
            }

            year.Text = yearly.ToString();


            DateTime dateTime = DateTime.Now;
          
            List<string> list7days= new List<string>();

           
            for (int a = -1; a >= -7; a--)
            {
                    
                    DateTime otherDateTime = dateTime.AddDays(a);
                    string datesearch4 = otherDateTime.ToString("MM/dd/yyyy");

                    list7days.Add(datesearch4.ToString());  
     
            }






            //////day1
            ///
            decimal total = 0;

            DataView dv4 = new DataView(dt);

            dv4.RowFilter = "[Transaction Date] LIKE '%" + list7days[0] + "%'";

            Sales_Datagrid.DataSource = null;
            Sales_Datagrid.Rows.Clear();
            Sales_Datagrid.Columns.Clear();
            Sales_Datagrid.DataSource = dv4;


            for (int a = 0; a < Sales_Datagrid.Rows.Count; a++)
            {
                total += Convert.ToDecimal(Sales_Datagrid.Rows[a].Cells[2].Value);
            }

            //////day2
            ///


            DataView dv5 = new DataView(dt);

            dv5.RowFilter = "[Transaction Date] LIKE '%" + list7days[1] + "%'";

            Sales_Datagrid.DataSource = null;
            Sales_Datagrid.Rows.Clear();
            Sales_Datagrid.Columns.Clear();
            Sales_Datagrid.DataSource = dv5;


            for (int a = 0; a < Sales_Datagrid.Rows.Count; a++)
            {
                total += Convert.ToDecimal(Sales_Datagrid.Rows[a].Cells[2].Value);
            }

            //////day3
            ///


            DataView dv6 = new DataView(dt);

            dv6.RowFilter = "[Transaction Date] LIKE '%" + list7days[2] + "%'";

            Sales_Datagrid.DataSource = null;
            Sales_Datagrid.Rows.Clear();
            Sales_Datagrid.Columns.Clear();
            Sales_Datagrid.DataSource = dv6;


            for (int a = 0; a < Sales_Datagrid.Rows.Count; a++)
            {
                total += Convert.ToDecimal(Sales_Datagrid.Rows[a].Cells[2].Value);
            }

            //////day4
            ///


            DataView dv7 = new DataView(dt);

            dv7.RowFilter = "[Transaction Date] LIKE '%" + list7days[3] + "%'";

            Sales_Datagrid.DataSource = null;
            Sales_Datagrid.Rows.Clear();
            Sales_Datagrid.Columns.Clear();
            Sales_Datagrid.DataSource = dv7;


            for (int a = 0; a < Sales_Datagrid.Rows.Count; a++)
            {
                total += Convert.ToDecimal(Sales_Datagrid.Rows[a].Cells[2].Value);
            }


            //////day5
            ///


            DataView dv8 = new DataView(dt);

            dv8.RowFilter = "[Transaction Date] LIKE '%" + list7days[4] + "%'";

            Sales_Datagrid.DataSource = null;
            Sales_Datagrid.Rows.Clear();
            Sales_Datagrid.Columns.Clear();
            Sales_Datagrid.DataSource = dv8;


            for (int a = 0; a < Sales_Datagrid.Rows.Count; a++)
            {
                total += Convert.ToDecimal(Sales_Datagrid.Rows[a].Cells[2].Value);
            }


            //////day6
            ///


            DataView dv9 = new DataView(dt);

            dv9.RowFilter = "[Transaction Date] LIKE '%" + list7days[5] + "%'";

            Sales_Datagrid.DataSource = null;
            Sales_Datagrid.Rows.Clear();
            Sales_Datagrid.Columns.Clear();
            Sales_Datagrid.DataSource = dv9;


            for (int a = 0; a < Sales_Datagrid.Rows.Count; a++)
            {
                total += Convert.ToDecimal(Sales_Datagrid.Rows[a].Cells[2].Value);
            }


            //////day7
            ///


            DataView dv10 = new DataView(dt);

            dv10.RowFilter = "[Transaction Date] LIKE '%" + list7days[6] + "%'";

            Sales_Datagrid.DataSource = null;
            Sales_Datagrid.Rows.Clear();
            Sales_Datagrid.Columns.Clear();
            Sales_Datagrid.DataSource = dv10;


            for (int a = 0; a < Sales_Datagrid.Rows.Count; a++)
            {
                total += Convert.ToDecimal(Sales_Datagrid.Rows[a].Cells[2].Value);
            }

            week.Text = total.ToString();


            if(Form1.levelac=="Employee")
            {
                week.Text = "Hidden";
                month.Text = "Hidden";
                year.Text = "Hidden";
            }
            else
            {

            }


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
                    r["Product Name"] = obj1.Product_Name;
                    r["Stock"] = obj1.Stock;
                    r["Low"] = obj1.Low;
                    r["High"] = obj1.High;
                    r["Item Sold"] = obj1.Items_Sold;
                    r["test"] = "a";


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

            ///////////////////////
            ///
            dv4.RowFilter = "[test] LIKE '%" + "a" + "%'";

            inventory.DataSource = null;
            inventory.Rows.Clear();
            inventory.Columns.Clear();
            inventory.DataSource = dv4;

            try
            {
                DataView dv5 = new DataView(dt2);
                dv5.Sort = "Item Sold DESC";


                inventory.DataSource = null;
                inventory.Rows.Clear();
                inventory.Columns.Clear();
                inventory.DataSource = dv5;

                try
                {
                    List<string> list = new List<string>();
                    List<string> list2 = new List<string>();
                    List<string> list3 = new List<string>();
                    for (int a = 0; a < 5; a++)
                    {
                        list.Add(inventory.Rows[a].Cells[0].Value.ToString());
                        list2.Add(inventory.Rows[a].Cells[5].Value.ToString());
                        list3.Add(inventory.Rows[a].Cells[1].Value.ToString());
                    }


                    this.chart1.Series[0].Points.Clear();
                    this.chart1.Series["Product Sold"].Points.AddXY(list[0], list2[0]);
                    this.chart1.Series["Current Stock"].Points.AddXY(list[0], list3[0]);

                    this.chart1.Series["Product Sold"].Points.AddXY(list[1], list2[1]);
                    this.chart1.Series["Current Stock"].Points.AddXY(list[0], list3[1]);

                    this.chart1.Series["Product Sold"].Points.AddXY(list[2], list2[2]);
                    this.chart1.Series["Current Stock"].Points.AddXY(list[0], list3[2]);

                    this.chart1.Series["Product Sold"].Points.AddXY(list[3], list2[3]);
                    this.chart1.Series["Current Stock"].Points.AddXY(list[0], list3[3]);

                    this.chart1.Series["Product Sold"].Points.AddXY(list[4], list2[4]);
                    this.chart1.Series["Current Stock"].Points.AddXY(list[0], list3[4]);

                    chart1.Series["Product Sold"]["PixelPointWidth"] = "40";
                    chart1.Series["Current Stock"]["PixelPointWidth"] = "40";

               


                }
                catch
                {
                    Console.Write("ERROR");
                }
 

            }
            catch
            {

            }

            ///////////////////////////

      



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
                if(Form1.status=="true")
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

            if (Form1.status == "true" && Form1.levelac.Equals("Admin"))
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

        private void button1_Click_2(object sender, EventArgs e)
        {
            UserLoginLog_Module a = new UserLoginLog_Module();
            this.Hide();
            a.Show();
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
          

            if (Form1.status == "true" && (Form1.levelac.Equals("Admin")))
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

        private void button1_Click_3(object sender, EventArgs e)
        {
            Stockadjustmentrecord_module a = new Stockadjustmentrecord_module();
            this.Hide();
            a.Show();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public void close()
        {
            this.Hide();
        }
    }
}
