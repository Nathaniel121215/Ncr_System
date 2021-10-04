﻿using FireSharp.Config;
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
    public partial class InventoryArchive_Module : Form
    {
        public static string ID;
        public static string Product_Name;
        public static string Unit;
        public static string Brand;
        public static string Description;
        public static string Category;

        public static string Price;
        public static string Items_Sold;

        public static string Low;
        public static string High;


        int supressor = 1;

        public static string checker = "";
        DataTable dt = new DataTable();

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public static InventoryArchive_Module _instance;


        public InventoryArchive_Module()
        {
            InitializeComponent();
            _instance = this;
        }

        private void InventoryArchive_Module_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            this.Inventory_Datagrid.AllowUserToAddRows = false;

            dt.Columns.Add("Product ID");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Unit");
            dt.Columns.Add("Brand");
            dt.Columns.Add("Product Description");
            dt.Columns.Add("Category");
            dt.Columns.Add("Price");

          
        

            dt.Columns.Add("Date Archived");
            dt.Columns.Add("User");


            Inventory_Datagrid.DataSource = dt;

            DataGridViewImageColumn View = new DataGridViewImageColumn();
            Inventory_Datagrid.Columns.Add(View);
            View.HeaderText = "";
            View.Name = "";
            View.ImageLayout = DataGridViewImageCellLayout.Zoom;
            View.Image = Properties.Resources.View_Icon;

            DataGridViewImageColumn Restore = new DataGridViewImageColumn();
            Inventory_Datagrid.Columns.Add(Restore);
            Restore.HeaderText = "";
            Restore.Name = "";
            Restore.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Restore.Image = Properties.Resources.Restore_Icon;




            DataViewAll();
            

        }

        public async void DataViewAll()
        {
            foreach (DataGridViewColumn column in Inventory_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //Display settings for inventory

            DataGridViewColumn column0 = Inventory_Datagrid.Columns[0];
            column0.Width = 90;

            DataGridViewColumn column1 = Inventory_Datagrid.Columns[1];
            column1.Width = 220;

            DataGridViewColumn column2 = Inventory_Datagrid.Columns[2];
            column2.Width = 80;

            DataGridViewColumn column3 = Inventory_Datagrid.Columns[3];
            column3.Width = 100;

            DataGridViewColumn column4 = Inventory_Datagrid.Columns[4];
            column4.Width = 190;


            DataGridViewColumn column6 = Inventory_Datagrid.Columns[6];
            column6.Width = 85;


            DataGridViewColumn column7 = Inventory_Datagrid.Columns[7];
            column7.Width = 95;



            DataGridViewColumn column9 = Inventory_Datagrid.Columns[9];
            column9.Width = 80;

            DataGridViewColumn column10 = Inventory_Datagrid.Columns[10];
            column10.Width = 80;


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

                    FirebaseResponse resp1 = await client.GetTaskAsync("InventoryArchive/" + i);
                    InventoryArchive_Class obj1 = resp1.ResultAs<InventoryArchive_Class>();

                    DataRow r = dt.NewRow();
                    r["Product ID"] = obj1.ID;
                    r["Product Name"] = obj1.Product_Name;
                    r["Unit"] = obj1.Unit;
                    r["Brand"] = obj1.Brand;
                    r["Product Description"] = obj1.Description;
                    r["Category"] = obj1.Category;
                    r["Price"] = obj1.Price;

                    r["Date Archived"] = obj1.Date_Archived;
                    r["User"] = obj1.User;





                    dt.Rows.Add(r);


                    gettransactioncount();
                }

                catch
                {
                  
                }
            }
            gettransactioncount();
            checker = "allow";
            filterlabeltxt.Text = "";
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
          

            if (checker.Equals("allow"))
            {
                InventoryArchive_Filter_popup a = new InventoryArchive_Filter_popup();
                a.Show();

                checker = "dontallow";
            }
            else
            {
                MessageBox.Show("The tab is currently already open.");
            }
        }
        public void filter()
        {

            DataView dv = new DataView(dt);
            string date1 = InventoryArchive_Filter_popup.startdate;
            string date2 = InventoryArchive_Filter_popup.enddate;
            string user = InventoryArchive_Filter_popup.user;
          

            if (InventoryArchive_Filter_popup.user == "")
            {

                dv.RowFilter = "[Date Archived]  >='" + date1 + "'AND [Date Archived] <='" + date2 + "'";

                Inventory_Datagrid.DataSource = null;
                Inventory_Datagrid.Rows.Clear();
                Inventory_Datagrid.Columns.Clear();
                Inventory_Datagrid.DataSource = dv;



            }

        
            else
            {
                dv.RowFilter = "[Date Archived]  >='" + date1 + "'AND [Date Archived] <='" + date2 + "'" + " AND [User] LIKE '%" + user + "%'";

                Inventory_Datagrid.DataSource = null;
                Inventory_Datagrid.Rows.Clear();
                Inventory_Datagrid.Columns.Clear();
                Inventory_Datagrid.DataSource = dv;


            }



            DataGridViewImageColumn View = new DataGridViewImageColumn();
            Inventory_Datagrid.Columns.Add(View);
            View.HeaderText = "";
            View.Name = "";
            View.ImageLayout = DataGridViewImageCellLayout.Zoom;
            View.Image = Properties.Resources.View_Icon;

            DataGridViewImageColumn Restore = new DataGridViewImageColumn();
            Inventory_Datagrid.Columns.Add(Restore);
            Restore.HeaderText = "";
            Restore.Name = "";
            Restore.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Restore.Image = Properties.Resources.Restore_Icon;

            filterlabeltxt.Text = date1 + " to " + date2;

            searchupdate();
            gettransactioncount();

        }
        public void searchupdate()
        {
            foreach (DataGridViewColumn column in Inventory_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //Display settings for inventory

            DataGridViewColumn column0 = Inventory_Datagrid.Columns[0];
            column0.Width = 90;

            DataGridViewColumn column1 = Inventory_Datagrid.Columns[1];
            column1.Width = 220;

            DataGridViewColumn column2 = Inventory_Datagrid.Columns[2];
            column2.Width = 80;

            DataGridViewColumn column3 = Inventory_Datagrid.Columns[3];
            column3.Width = 100;

            DataGridViewColumn column4 = Inventory_Datagrid.Columns[4];
            column4.Width = 190;


            DataGridViewColumn column6 = Inventory_Datagrid.Columns[6];
            column6.Width = 85;


            DataGridViewColumn column7 = Inventory_Datagrid.Columns[7];
            column7.Width = 95;





            DataGridViewColumn column9 = Inventory_Datagrid.Columns[9];
            column9.Width = 80;

            DataGridViewColumn column10 = Inventory_Datagrid.Columns[10];
            column10.Width = 80;

            gettransactioncount();
        }

        public void gettransactioncount()
        {
            int transactioncount = 0;
            transactioncount = Inventory_Datagrid.Rows.Count;

            TransactionCounttxt.Text = transactioncount.ToString();


        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = "[" + bunifuDropdown1.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

            Inventory_Datagrid.DataSource = null;
            Inventory_Datagrid.Rows.Clear();
            Inventory_Datagrid.Columns.Clear();
            Inventory_Datagrid.DataSource = dv;


            DataGridViewImageColumn View = new DataGridViewImageColumn();
            Inventory_Datagrid.Columns.Add(View);
            View.HeaderText = "";
            View.Name = "";
            View.ImageLayout = DataGridViewImageCellLayout.Zoom;
            View.Image = Properties.Resources.View_Icon;

            DataGridViewImageColumn Restore = new DataGridViewImageColumn();
            Inventory_Datagrid.Columns.Add(Restore);
            Restore.HeaderText = "";
            Restore.Name = "";
            Restore.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Restore.Image = Properties.Resources.Restore_Icon;



            gettransactioncount();

            filterlabeltxt.Text = "";

            searchupdate();

        }

        private void bunifuImageButton19_Click(object sender, EventArgs e)
        {
            Dashboard_Module a = new Dashboard_Module();
            this.Hide();
            a.Show();
        }

        private void searchtxt_KeyUp(object sender, KeyEventArgs e)
        {
          

            if (searchtxt.Text == "" && supressor == 1)
            {
                supressor = 0;


                Inventory_Datagrid.DataSource = null;
                Inventory_Datagrid.Rows.Clear();
                Inventory_Datagrid.Columns.Clear();
                Inventory_Datagrid.DataSource = dt;


                DataGridViewImageColumn View = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(View);
                View.HeaderText = "";
                View.Name = "";
                View.ImageLayout = DataGridViewImageCellLayout.Zoom;
                View.Image = Properties.Resources.View_Icon;

                DataGridViewImageColumn Restore = new DataGridViewImageColumn();
                Inventory_Datagrid.Columns.Add(Restore);
                Restore.HeaderText = "";
                Restore.Name = "";
                Restore.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Restore.Image = Properties.Resources.Restore_Icon;



                DataViewAll();
                
                filterlabeltxt.Text = "";
            }

            if (searchtxt.Text != "")
            {
                supressor = 1;

            }


        }

        private void Inventory_Datagrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //restore
            string columnindex = "";

            try
            {
                if (e.ColumnIndex == Inventory_Datagrid.Columns[10].Index)
                {
                    columnindex = Inventory_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();




                    FirebaseResponse resp1 = client.Get("InventoryArchive/" + columnindex);
                    Product_class obj1 = resp1.ResultAs<Product_class>();

                    var data = new Product_class
                    {
                        ID = obj1.ID,
                        Product_Name = obj1.Product_Name,
                        Unit = obj1.Unit,
                        Brand = obj1.Brand,
                        Description = obj1.Description,
                        Category = obj1.Category,
                        Price = obj1.Price,
                        Items_Sold = obj1.Items_Sold,
                        Stock = "0",

                        Low = obj1.Low,
                        High = obj1.High,


                    };

                    FirebaseResponse response = client.Set("Inventory/" + data.ID, data);
                    Product_class result = response.ResultAs<Product_class>();
               



                    FirebaseResponse resp3 = client.Get("inventoryCounterExisting/node");
                    Counter_class gett = resp3.ResultAs<Counter_class>();
                    int exist = (Convert.ToInt32(gett.cnt) + 1);
                    var obj2 = new Counter_class
                    {
                        cnt = exist.ToString()
                    };


                    SetResponse response2 = client.Set("inventoryCounterExisting/node", obj2);



                    //get archive counter
                    FirebaseResponse resp = client.Get("InventoryArchiveCounter/node");
                    Counter_class get = resp.ResultAs<Counter_class>();

                    //update archive counter
                    var obj = new Counter_class
                    {
                        cnt = (Convert.ToInt32(get.cnt) - 1).ToString(),
                    };

                    SetResponse response4 = client.Set("InventoryArchiveCounter/node", obj);



                    //delete from current table

                    FirebaseResponse response5 = client.Delete("InventoryArchive/" + columnindex);

                    gettransactioncount();
                    filterlabeltxt.Text = "";
                    DataViewAll();


                }

                //view
                if (e.ColumnIndex == Inventory_Datagrid.Columns[9].Index)
                {
                    columnindex = Inventory_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();

                    FirebaseResponse resp1 = client.Get("InventoryArchive/" + columnindex);
                    Product_class obj1 = resp1.ResultAs<Product_class>();


                    ID = obj1.ID;
                    Product_Name = obj1.Product_Name;
                    Unit = obj1.Unit;
                    Brand = obj1.Brand;
                    Description = obj1.Description;
                    Category = obj1.Category;
                    Price = obj1.Price;
                    Items_Sold = obj1.Items_Sold;
                    Low = obj1.Low;
                    High = obj1.High;



                    InventoryArchiveView c = new InventoryArchiveView();
                    c.Show();
              



                }





            }
            catch
            {

            }



        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            AccountArchive_Module a = new AccountArchive_Module();
            this.Hide();
            a.Show();
        }
    }
}
