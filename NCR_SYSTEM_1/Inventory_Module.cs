using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
namespace NCR_SYSTEM_1
{
    public partial class Inventory_Module : Form
    {


        int supressor = 1;

        private Image[] StatusImgs;

        public static string name = "";
        public static string category = "";
        public static string desc = "";
        public static string brand = "";
        public static string unit = "";
        public static string id = "";
        public static string low = "";
        public static string high = "";
        public static decimal price = 0;

        DataTable dt = new DataTable();

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public static Inventory_Module _instance;

        public Inventory_Module()
        {
           
            InitializeComponent();
            _instance = this;
        }

        private void bunifuImageButton15_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    StockAdjustment_Module a = new StockAdjustment_Module();
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }
            }
        }

        private void Inventory_Module_Load(object sender, EventArgs e)
        {
            
            datedisplay.Text = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            datedisplay.Select();

            client = new FireSharp.FirebaseClient(config);
            this.Inventory_Datagrid.AllowUserToAddRows = false;

            dt.Columns.Add("Product ID");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Unit");
            dt.Columns.Add("Brand");
            dt.Columns.Add("Product Description");
            dt.Columns.Add("Category");
            dt.Columns.Add("Price");
            dt.Columns.Add("Items Sold");
            dt.Columns.Add("Stock");
            dt.Columns.Add("Low");
            dt.Columns.Add("High");
            dt.Columns.Add("String Indicator");

            Inventory_Datagrid.DataSource = dt;

            

            DataGridViewImageColumn update = new DataGridViewImageColumn();
            Inventory_Datagrid.Columns.Add(update);
            update.HeaderText = "";
            update.Name = "update";
            update.ImageLayout = DataGridViewImageCellLayout.Zoom;
            update.Image = Properties.Resources.Update_Icon;


            DataGridViewImageColumn Archive = new DataGridViewImageColumn();
            Inventory_Datagrid.Columns.Add(Archive);
            Archive.HeaderText = "";
            Archive.Name = "Archive";
            Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Archive.Image = Properties.Resources.Archive_Icon;


            DataGridViewImageColumn Indicator = new DataGridViewImageColumn();
            Inventory_Datagrid.Columns.Add(Indicator);
            Indicator.HeaderText = "Indicator";
            Indicator.Name = "Indicator";
            Indicator.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Indicator.Image = Properties.Resources.loading;


          

            DataViewAll();


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
        public async void DataViewAll()
        {
            foreach (DataGridViewColumn column in Inventory_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            //Display settings for inventory

            DataGridViewColumn column0 = Inventory_Datagrid.Columns[0];
            column0.Width = 80;

            DataGridViewColumn column1 = Inventory_Datagrid.Columns[1];
            column1.Width = 220;

            DataGridViewColumn column2 = Inventory_Datagrid.Columns[2];
            column2.Width = 80;

            DataGridViewColumn column3 = Inventory_Datagrid.Columns[3];
            column3.Width = 100;

            DataGridViewColumn column4 = Inventory_Datagrid.Columns[4];
            column4.Width = 220;

            DataGridViewColumn column14 = Inventory_Datagrid.Columns[14];
            column14.Width = 120;

            DataGridViewColumn column12 = Inventory_Datagrid.Columns[12];
            column12.Width = 80;

            DataGridViewColumn column13 = Inventory_Datagrid.Columns[13];
            column13.Width = 80;

            Inventory_Datagrid.Columns[14].DisplayIndex = 8;
            Inventory_Datagrid.Columns[9].Visible = false;
            Inventory_Datagrid.Columns[10].Visible = false;
            Inventory_Datagrid.Columns[11].Visible = false;

            dt.Rows.Clear();

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

                    DataRow r = dt.NewRow();
                    r["Product ID"] = obj1.ID;
                    r["Product Name"] = obj1.Product_Name;
                    r["Unit"] = obj1.Unit;
                    r["Brand"] = obj1.Brand;
                    r["Product Description"] = obj1.Description;
                    r["Category"] = obj1.Category;
                    r["Price"] = obj1.Price;
                    r["Items Sold"] = obj1.Items_Sold;
                    r["Stock"] = obj1.Stock;
                    r["Low"] = obj1.Low;
                    r["High"] = obj1.High;
                   
         


                    dt.Rows.Add(r);

                 

                }

                catch
                {

                }
            }

         




            try
            {

               



                foreach (DataGridViewRow row in Inventory_Datagrid.Rows)
                {

                    try
                    {
                      

                        StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.new_low_on_stock, NCR_SYSTEM_1.Properties.Resources.new_in_stock, NCR_SYSTEM_1.Properties.Resources.new_high_on_stock, NCR_SYSTEM_1.Properties.Resources.new_out_of_stock };

                     



                        if (Convert.ToInt32(row.Cells[8].Value) <= Convert.ToInt32(row.Cells[9].Value) && Convert.ToInt32(row.Cells[8].Value) !=0) //Low on stock
                        {

                      

                            row.Cells[14].Value = StatusImgs[0];
                            row.Cells[11].Value = "low on stock";

                      
                        }
                       
                        if (Convert.ToInt32(row.Cells[8].Value) > Convert.ToInt32(row.Cells[9].Value) && Convert.ToInt32(row.Cells[8].Value) < Convert.ToInt32(row.Cells[10].Value))
                        {
                            row.Cells[14].Value = StatusImgs[1];
                            row.Cells[11].Value = "in stock";
                        }

                        if (Convert.ToInt32(row.Cells[8].Value) >= Convert.ToInt32(row.Cells[10].Value))
                        {
                            row.Cells[14].Value = StatusImgs[2];
                            row.Cells[11].Value = "high on stock";
                        }

                        if (Convert.ToInt32(row.Cells[8].Value).Equals(0)) //out of stock
                        {
                            row.Cells[14].Value = StatusImgs[3];
                            row.Cells[11].Value = "out of stock";
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



        }

 


        private void Inventory_Datagrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(Form1.status=="true")
            {
                string columnindex = "";

                try
                {


                    //Archive
                    if (e.ColumnIndex == Inventory_Datagrid.Columns[13].Index)
                    {
                        int stocks = Convert.ToInt32(Inventory_Datagrid.Rows[e.RowIndex].Cells[8].Value);
                        if (stocks==0)
                        {
                            if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                            {
                                columnindex = Inventory_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();



                                //new archive code

                                var data = new InventoryArchive_Class
                                {
                                    ID = Inventory_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString(),
                                    Product_Name = Inventory_Datagrid.Rows[e.RowIndex].Cells[1].Value.ToString(),
                                    Unit = Inventory_Datagrid.Rows[e.RowIndex].Cells[2].Value.ToString(),
                                    Brand = Inventory_Datagrid.Rows[e.RowIndex].Cells[3].Value.ToString(),
                                    Description = Inventory_Datagrid.Rows[e.RowIndex].Cells[4].Value.ToString(),
                                    Category = Inventory_Datagrid.Rows[e.RowIndex].Cells[5].Value.ToString(),
                                    Price = Inventory_Datagrid.Rows[e.RowIndex].Cells[6].Value.ToString(),
                                    Items_Sold = Inventory_Datagrid.Rows[e.RowIndex].Cells[7].Value.ToString(),
                                    Stock = Inventory_Datagrid.Rows[e.RowIndex].Cells[8].Value.ToString(),
                                    Low = Inventory_Datagrid.Rows[e.RowIndex].Cells[9].Value.ToString(),
                                    High = Inventory_Datagrid.Rows[e.RowIndex].Cells[10].Value.ToString(),

                                    Date_Archived = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                                    User = Form1.username,

                                };


                                //add to archive 
                                FirebaseResponse response3 = client.Set("InventoryArchive/" + data.ID, data);
                                Product_class result = response3.ResultAs<Product_class>();

                                //get archive counter
                                FirebaseResponse resp = client.Get("InventoryArchiveCounter/node");
                                Counter_class get = resp.ResultAs<Counter_class>();

                                //update archive counter
                                var obj = new Counter_class
                                {
                                    cnt = (Convert.ToInt32(get.cnt) + 1).ToString(),
                                };

                                SetResponse response4 = client.Set("InventoryArchiveCounter/node", obj);



                                //delete from current table

                                FirebaseResponse response = client.Delete("Inventory/" + columnindex);




                                //Minus existing
                                FirebaseResponse resp3 = client.Get("inventoryCounterExisting/node");
                                Counter_class gett = resp3.ResultAs<Counter_class>();
                                int exist = (Convert.ToInt32(gett.cnt) - 1);

                                var obj2 = new Counter_class
                                {
                                    cnt = exist.ToString()
                                };
                                SetResponse response2 = client.Set("inventoryCounterExisting/node", obj2);



                                //Activity Log ARCHIVING PRODUCT EVENT


                                FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                                Counter_class get4 = resp4.ResultAs<Counter_class>();
                                int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                                var data2 = new ActivityLog_Class
                                {
                                    Event_ID = cnt4.ToString(),
                                    Module = "Inventory Management Module",
                                    Action = "Product-ID: " + data.ID + "   Moved to Archive Module",
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



                                DataViewAll();
                            }

                            else

                            {
                                //do something if NO
                            }
                        }
                        else
                        {
                            MessageBox.Show("The item select still have stock.");
                        }
                        

                    }




                    //update
                    if (e.ColumnIndex == Inventory_Datagrid.Columns[12].Index)
                    {
                        columnindex = Inventory_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();


                        Inventory_Datagrid.Rows[e.RowIndex].Selected = true;

                        id = Inventory_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                        name = Inventory_Datagrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                        unit = Inventory_Datagrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                        brand = Inventory_Datagrid.Rows[e.RowIndex].Cells[3].Value.ToString();
                        desc = Inventory_Datagrid.Rows[e.RowIndex].Cells[4].Value.ToString();
                        category = Inventory_Datagrid.Rows[e.RowIndex].Cells[5].Value.ToString();
                        price = Convert.ToDecimal(Inventory_Datagrid.Rows[e.RowIndex].Cells[6].Value);

                        low = Inventory_Datagrid.Rows[e.RowIndex].Cells[9].Value.ToString();
                        high = Inventory_Datagrid.Rows[e.RowIndex].Cells[10].Value.ToString();

                        Updateproduct_popup c = new Updateproduct_popup();
                        c.Show();
                        Form1.status = "false";

                    }
                }
                catch
                {

                }
            }
            else
            {
                MessageBox.Show("The Module is still loading or a window is currently open.");
            }
            
        }

        private void bunifuImageButton12_Click(object sender, EventArgs e)
        {

            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    stockpurchase_Module a = new stockpurchase_Module();
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }
            }

        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {

            Accountmanagement_Module a = new Accountmanagement_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }
            }
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            Supplierrecord_module a = new Supplierrecord_module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton13_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    Addunit_module a = new Addunit_module();
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton11_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    Addcategory_module a = new Addcategory_module();
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }
            }

        }

        private void bunifuMetroTextbox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (searchtxt.Text == "" && supressor == 1)
            {
                supressor = 0;

                Inventory_Datagrid.DataSource = null;
                Inventory_Datagrid.Rows.Clear();
                Inventory_Datagrid.Columns.Clear();
                Inventory_Datagrid.DataSource = dt;




                DataGridViewImageColumn update = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(update);
                update.HeaderText = "";
                update.Name = "update";
                update.ImageLayout = DataGridViewImageCellLayout.Zoom;
                update.Image = Properties.Resources.Update_Icon;


                DataGridViewImageColumn Archive = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(Archive);
                Archive.HeaderText = "";
                Archive.Name = "Archive";
                Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Archive.Image = Properties.Resources.Archive_Icon;


                DataGridViewImageColumn Indicator = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(Indicator);
                Indicator.HeaderText = "Indicator";
                Indicator.Name = "Indicator";
                Indicator.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Indicator.Image = Properties.Resources.loading;

                DataViewAll();

            }

            if (searchtxt.Text != "")
            {
                supressor = 1;
                
            }
        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
            if (searchtxt.Text != "" & Form1.status == "true")
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "[" + combofilter.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

                Inventory_Datagrid.DataSource = null;
                Inventory_Datagrid.Rows.Clear();
                Inventory_Datagrid.Columns.Clear();
                Inventory_Datagrid.DataSource = dv;



                DataGridViewImageColumn update = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(update);
                update.HeaderText = "";
                update.Name = "update";
                update.ImageLayout = DataGridViewImageCellLayout.Zoom;
                update.Image = Properties.Resources.Update_Icon;


                DataGridViewImageColumn Archive = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(Archive);
                Archive.HeaderText = "";
                Archive.Name = "Archive";
                Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Archive.Image = Properties.Resources.Archive_Icon;

                DataGridViewImageColumn Indicator = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(Indicator);
                Indicator.HeaderText = "Indicator";
                Indicator.Name = "Indicator";
                Indicator.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Indicator.Image = Properties.Resources.loading;



                searchupdate();
            }
            else
            {

            }


            

        }

        public void searchupdate()
        {
            foreach (DataGridViewColumn column in Inventory_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            try
            {


                DataGridViewColumn column0 = Inventory_Datagrid.Columns[0];
                column0.Width = 80;

                DataGridViewColumn column1 = Inventory_Datagrid.Columns[1];
                column1.Width = 220;

                DataGridViewColumn column2 = Inventory_Datagrid.Columns[2];
                column2.Width = 80;

                DataGridViewColumn column3 = Inventory_Datagrid.Columns[3];
                column3.Width = 100;

                DataGridViewColumn column4 = Inventory_Datagrid.Columns[4];
                column4.Width = 220;

                DataGridViewColumn column14 = Inventory_Datagrid.Columns[14];
                column14.Width = 120;

                DataGridViewColumn column12 = Inventory_Datagrid.Columns[12];
                column12.Width = 80;

                DataGridViewColumn column13 = Inventory_Datagrid.Columns[13];
                column13.Width = 80;


                Inventory_Datagrid.Columns[14].DisplayIndex = 8;
                Inventory_Datagrid.Columns[9].Visible = false;
                Inventory_Datagrid.Columns[10].Visible = false;
                Inventory_Datagrid.Columns[11].Visible = false;



                foreach (DataGridViewRow row in Inventory_Datagrid.Rows)
                {

                    try
                    {


                        StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.new_low_on_stock, NCR_SYSTEM_1.Properties.Resources.new_in_stock, NCR_SYSTEM_1.Properties.Resources.new_high_on_stock, NCR_SYSTEM_1.Properties.Resources.new_out_of_stock };





                        if (Convert.ToInt32(row.Cells[8].Value) <= Convert.ToInt32(row.Cells[9].Value) && Convert.ToInt32(row.Cells[8].Value) != 0) //Low on stock
                        {



                            row.Cells[14].Value = StatusImgs[0];
                            row.Cells[11].Value = "low on stock";


                        }

                        if (Convert.ToInt32(row.Cells[8].Value) > Convert.ToInt32(row.Cells[9].Value) && Convert.ToInt32(row.Cells[8].Value) < Convert.ToInt32(row.Cells[10].Value))
                        {
                            row.Cells[14].Value = StatusImgs[1];
                            row.Cells[11].Value = "in stock";
                        }

                        if (Convert.ToInt32(row.Cells[8].Value) > Convert.ToInt32(row.Cells[10].Value))
                        {
                            row.Cells[14].Value = StatusImgs[2];
                            row.Cells[11].Value = "high on stock";
                        }

                        if (Convert.ToInt32(row.Cells[8].Value).Equals(0)) //out of stock
                        {
                            row.Cells[14].Value = StatusImgs[3];
                            row.Cells[11].Value = "out of stock";
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
        }

        private void bunifuImageButton9_Click(object sender, EventArgs e)
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

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton8_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton9_Click_1(object sender, EventArgs e)
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }
            }
        }

        private void bunifuImageButton5_Click_1(object sender, EventArgs e)
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }
            }
        }

        private void bunifuImageButton6_Click_1(object sender, EventArgs e)
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }
            }

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
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

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            if(Form1.status=="true")
            {
                //instock button

                showall.Normalcolor = Color.FromArgb(116, 170, 255);
                instock.Normalcolor = Color.FromArgb(49, 129, 255);
                highonstock.Normalcolor = Color.FromArgb(116, 170, 255);
                lowonstock.Normalcolor = Color.FromArgb(116, 170, 255);
                outofstock.Normalcolor = Color.FromArgb(116, 170, 255);

                DataView dv = new DataView(dt);
                dv.RowFilter = "[" + "String Indicator" + "]" + "LIKE '%" + "in stock" + "%'";

                Inventory_Datagrid.DataSource = null;
                Inventory_Datagrid.Rows.Clear();
                Inventory_Datagrid.Columns.Clear();
                Inventory_Datagrid.DataSource = dv;



                DataGridViewImageColumn update = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(update);
                update.HeaderText = "";
                update.Name = "update";
                update.ImageLayout = DataGridViewImageCellLayout.Zoom;
                update.Image = Properties.Resources.Update_Icon;


                DataGridViewImageColumn Archive = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(Archive);
                Archive.HeaderText = "";
                Archive.Name = "Archive";
                Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Archive.Image = Properties.Resources.Archive_Icon;


                DataGridViewImageColumn Indicator = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(Indicator);
                Indicator.HeaderText = "Indicator";
                Indicator.Name = "Indicator";
                Indicator.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Indicator.Image = Properties.Resources.loading;

                searchupdate();
            }
            else
            {
                MessageBox.Show("Module is still loading.");
            }
           


        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if(Form1.status=="true")
            {
                //view all

                showall.Normalcolor = Color.FromArgb(49, 129, 255);
                instock.Normalcolor = Color.FromArgb(116, 170, 255);
                highonstock.Normalcolor = Color.FromArgb(116, 170, 255);
                lowonstock.Normalcolor = Color.FromArgb(116, 170, 255);
                outofstock.Normalcolor = Color.FromArgb(116, 170, 255);


                Inventory_Datagrid.DataSource = null;
                Inventory_Datagrid.Rows.Clear();
                Inventory_Datagrid.Columns.Clear();
                Inventory_Datagrid.DataSource = dt;

                DataGridViewImageColumn update = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(update);
                update.HeaderText = "";
                update.Name = "update";
                update.ImageLayout = DataGridViewImageCellLayout.Zoom;
                update.Image = Properties.Resources.Update_Icon;


                DataGridViewImageColumn Archive = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(Archive);
                Archive.HeaderText = "";
                Archive.Name = "Archive";
                Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Archive.Image = Properties.Resources.Archive_Icon;


                DataGridViewImageColumn Indicator = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(Indicator);
                Indicator.HeaderText = "Indicator";
                Indicator.Name = "Indicator";
                Indicator.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Indicator.Image = Properties.Resources.loading;

                DataViewAll();
            }
            else
            {
                MessageBox.Show("Module is still loading.");
            }
            
           
        }

        private void outofstock_Click(object sender, EventArgs e)
        {

            if (Form1.status == "true")
            {
                //out of stock button

                showall.Normalcolor = Color.FromArgb(116, 170, 255);
                instock.Normalcolor = Color.FromArgb(116, 170, 255);
                highonstock.Normalcolor = Color.FromArgb(116, 170, 255);
                lowonstock.Normalcolor = Color.FromArgb(116, 170, 255);
                outofstock.Normalcolor = Color.FromArgb(49, 129, 255);

                DataView dv = new DataView(dt);
                dv.RowFilter = "[" + "String Indicator" + "]" + "LIKE '%" + "out of stock" + "%'";

                Inventory_Datagrid.DataSource = null;
                Inventory_Datagrid.Rows.Clear();
                Inventory_Datagrid.Columns.Clear();
                Inventory_Datagrid.DataSource = dv;



                DataGridViewImageColumn update = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(update);
                update.HeaderText = "";
                update.Name = "update";
                update.ImageLayout = DataGridViewImageCellLayout.Zoom;
                update.Image = Properties.Resources.Update_Icon;


                DataGridViewImageColumn Archive = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(Archive);
                Archive.HeaderText = "";
                Archive.Name = "Archive";
                Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Archive.Image = Properties.Resources.Archive_Icon;


                DataGridViewImageColumn Indicator = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(Indicator);
                Indicator.HeaderText = "Indicator";
                Indicator.Name = "Indicator";
                Indicator.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Indicator.Image = Properties.Resources.loading;

                searchupdate();
            }
            else
            {
                MessageBox.Show("Module is still loading.");
            }

            
        }

    

     

        private void lowonstock_Click(object sender, EventArgs e)
        {

            if (Form1.status == "true")
            {
                //low on stock button

                showall.Normalcolor = Color.FromArgb(116, 170, 255);
                instock.Normalcolor = Color.FromArgb(116, 170, 255);
                highonstock.Normalcolor = Color.FromArgb(116, 170, 255);
                lowonstock.Normalcolor = Color.FromArgb(49, 129, 255);
                outofstock.Normalcolor = Color.FromArgb(116, 170, 255);

                DataView dv = new DataView(dt);
                dv.RowFilter = "[" + "String Indicator" + "]" + "LIKE '%" + "low on stock" + "%'";

                Inventory_Datagrid.DataSource = null;
                Inventory_Datagrid.Rows.Clear();
                Inventory_Datagrid.Columns.Clear();
                Inventory_Datagrid.DataSource = dv;



                DataGridViewImageColumn update = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(update);
                update.HeaderText = "";
                update.Name = "update";
                update.ImageLayout = DataGridViewImageCellLayout.Zoom;
                update.Image = Properties.Resources.Update_Icon;


                DataGridViewImageColumn Archive = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(Archive);
                Archive.HeaderText = "";
                Archive.Name = "Archive";
                Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Archive.Image = Properties.Resources.Archive_Icon;


                DataGridViewImageColumn Indicator = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(Indicator);
                Indicator.HeaderText = "Indicator";
                Indicator.Name = "Indicator";
                Indicator.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Indicator.Image = Properties.Resources.loading;

                searchupdate();
            }
            else
            {
                MessageBox.Show("Module is still loading.");
            }

            
        }

        private void highonstock_Click(object sender, EventArgs e)
        {

            if (Form1.status == "true")
            {
                //high on stock button

                showall.Normalcolor = Color.FromArgb(116, 170, 255);
                instock.Normalcolor = Color.FromArgb(116, 170, 255);
                highonstock.Normalcolor = Color.FromArgb(49, 129, 255);
                lowonstock.Normalcolor = Color.FromArgb(116, 170, 255);
                outofstock.Normalcolor = Color.FromArgb(116, 170, 255);

                DataView dv = new DataView(dt);
                dv.RowFilter = "[" + "String Indicator" + "]" + "LIKE '%" + "high on stock" + "%'";

                Inventory_Datagrid.DataSource = null;
                Inventory_Datagrid.Rows.Clear();
                Inventory_Datagrid.Columns.Clear();
                Inventory_Datagrid.DataSource = dv;



                DataGridViewImageColumn update = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(update);
                update.HeaderText = "";
                update.Name = "update";
                update.ImageLayout = DataGridViewImageCellLayout.Zoom;
                update.Image = Properties.Resources.Update_Icon;


                DataGridViewImageColumn Archive = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(Archive);
                Archive.HeaderText = "";
                Archive.Name = "Archive";
                Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Archive.Image = Properties.Resources.Archive_Icon;


                DataGridViewImageColumn Indicator = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(Indicator);
                Indicator.HeaderText = "Indicator";
                Indicator.Name = "Indicator";
                Indicator.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Indicator.Image = Properties.Resources.loading;

                searchupdate();
            }
            else
            {
                MessageBox.Show("Module is still loading.");
            }


            
        }

        private void Inventory_Datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }
            }
        }

        private void bunifuImageButton7_Click_1(object sender, EventArgs e)
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }
            }
        }

        private void bunifuImageButton8_Click_1(object sender, EventArgs e)
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }
            }
        }

        private void bunifuImageButton14_Click(object sender, EventArgs e)
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }
            }
        }

        private void bunifuImageButton10_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            copyAlltoClipboard();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }

        private void copyAlltoClipboard()
        {
            Inventory_Datagrid.SelectAll();
            DataObject dataObj = Inventory_Datagrid.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            if (Form1.status == "true")
            {
                copyAlltoClipboard();
                Microsoft.Office.Interop.Excel.Application xlexcel;
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                xlexcel = new Excel.Application();
                xlexcel.Visible = true;
                xlWorkBook = xlexcel.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            }
            else
            {
                MessageBox.Show("The Module is still loading or a window is currently open.");
            }
            
        }

        private void bunifuFlatButton2_Click_1(object sender, EventArgs e)
        {
            if (Form1.status=="true")
            {
                Addnewproduct_popup a = new Addnewproduct_popup();
                a.Show();

                Form1.status = "false";
            }
            else
            {
                MessageBox.Show("The Module is still loading or a window is currently open.");
            }
        }

        private void searchtxt_Enter(object sender, EventArgs e)
        {
            searchtxt.Text = "";
        }

        private void searchtxt_Leave(object sender, EventArgs e)
        {
            if (searchtxt.Text == "")
            {
                searchtxt.Text = "Type here to filter Inventory Content";
            }
            else
            {

            }
        }

        private void Inventory_Datagrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
