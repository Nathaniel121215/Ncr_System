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
    public partial class Addcategory_module : Form
    {
        public static string checker = "";

        int supressor = 1;

        public static string Category_ID = "";
        public static string Category_Name = "";
        public static string Date_Added = "";


        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;
        DataTable dt = new DataTable();
        public static Addcategory_module _instance;
        public Addcategory_module()
        {
            InitializeComponent();
            _instance = this;
        }

        private void Addcategory_module_Load(object sender, EventArgs e)
        {
            datedisplay.Text = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            datedisplay.Select();
            client = new FireSharp.FirebaseClient(config);

            this.Category_datagrid_stocks.AllowUserToAddRows = false;
            dt.Columns.Add("Category ID");
            dt.Columns.Add("Category Name");
            dt.Columns.Add("Date Added");



            Category_datagrid_stocks.DataSource = dt;

            DataGridViewImageColumn update = new DataGridViewImageColumn();
            Category_datagrid_stocks.Columns.Add(update);
            update.HeaderText = "";
            update.Name = "update";
            update.ImageLayout = DataGridViewImageCellLayout.Zoom;
            update.Image = Properties.Resources.Update_Icon;


            DataGridViewImageColumn Archive = new DataGridViewImageColumn();
            Category_datagrid_stocks.Columns.Add(Archive);
            Archive.HeaderText = "";
            Archive.Name = "Archive";
            Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Archive.Image = Properties.Resources.Archive_Icon;

            DataViewAll();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
           

            if (checker.Equals("allow"))
            {
                Addcategory_popup a = new Addcategory_popup();
                a.Show();

                checker = "dontallow";
            }
            else
            {
                MessageBox.Show("The tab is currently already open.");
            }
        }
        public async void DataViewAll()
        {
            //visual
            DataGridViewColumn column2 = Category_datagrid_stocks.Columns[2];
            column2.Width = 600;

            DataGridViewColumn column3 = Category_datagrid_stocks.Columns[3];
            column3.Width = 80;

            DataGridViewColumn column4 = Category_datagrid_stocks.Columns[4];
            column4.Width = 80;


            foreach (DataGridViewColumn column in Category_datagrid_stocks.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            dt.Rows.Clear();

            int i = 0;
            FirebaseResponse resp = await client.GetTaskAsync("CategoryCounter/node");
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

                    FirebaseResponse resp1 = await client.GetTaskAsync("Category/" + i);
                    Category_Class obj1 = resp1.ResultAs<Category_Class>();

                    DataRow r = dt.NewRow();
                    r["Category ID"] = obj1.Category_ID;
                    r["Category Name"] = obj1.Category_Name;
                    r["Date Added"] = obj1.Date_Added;


                    dt.Rows.Add(r);
                }

                catch
                {

                }
            }
            checker = "allow";
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            


            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
            {
                Dashboard_Module a = new Dashboard_Module();
                this.Hide();
                a.Show();

                Form1.loadingtime = 9000;
                Form1.status = "false";
                Loading_popup b = new Loading_popup();
                b.Show();
            }
            else if (Form1.levelac.Equals("Employee") && Form1.posac.Equals("Authorized") && Form1.status == "true")
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
                //MessageBox.Show("Your account do not have access on this Module.");
            }
        }

        private void Category_datagrid_stocks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnindex = "";

            try
            {


                if (e.ColumnIndex == Category_datagrid_stocks.Columns[3].Index)
                {
                  
                    columnindex = Category_datagrid_stocks.Rows[e.RowIndex].Cells[0].Value.ToString();


                    Category_datagrid_stocks.Rows[e.RowIndex].Selected = true;

                    Category_ID = Category_datagrid_stocks.Rows[e.RowIndex].Cells[0].Value.ToString();
                    Category_Name = Category_datagrid_stocks.Rows[e.RowIndex].Cells[1].Value.ToString();
                    Date_Added = Category_datagrid_stocks.Rows[e.RowIndex].Cells[2].Value.ToString();



                    Updatecategory_popup a = new Updatecategory_popup();

                    a.Show();


                }


            }
            catch
            {

            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            



            DataView dv = new DataView(dt);
            dv.RowFilter = "[" + combofilter.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

            Category_datagrid_stocks.DataSource = null;
            Category_datagrid_stocks.Rows.Clear();
            Category_datagrid_stocks.Columns.Clear();
            Category_datagrid_stocks.DataSource = dv;


            DataGridViewImageColumn update = new DataGridViewImageColumn();
            Category_datagrid_stocks.Columns.Add(update);
            update.HeaderText = "";
            update.Name = "update";
            update.ImageLayout = DataGridViewImageCellLayout.Zoom;
            update.Image = Properties.Resources.Update_Icon;


            DataGridViewImageColumn Archive = new DataGridViewImageColumn();
            Category_datagrid_stocks.Columns.Add(Archive);
            Archive.HeaderText = "";
            Archive.Name = "Archive";
            Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Archive.Image = Properties.Resources.Archive_Icon;

            //visual
            DataGridViewColumn column2 = Category_datagrid_stocks.Columns[2];
            column2.Width = 600;

            DataGridViewColumn column3 = Category_datagrid_stocks.Columns[3];
            column3.Width = 80;

            DataGridViewColumn column4 = Category_datagrid_stocks.Columns[4];
            column4.Width = 80;


            foreach (DataGridViewColumn column in Category_datagrid_stocks.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        private void searchtxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (searchtxt.Text == "" && supressor == 1)
            {
                supressor = 0;

                Category_datagrid_stocks.DataSource = null;
                Category_datagrid_stocks.Rows.Clear();
                Category_datagrid_stocks.Columns.Clear();
                Category_datagrid_stocks.DataSource = dt;




                DataGridViewImageColumn update = new DataGridViewImageColumn();
                Category_datagrid_stocks.Columns.Add(update);
                update.HeaderText = "";
                update.Name = "update";
                update.ImageLayout = DataGridViewImageCellLayout.Zoom;
                update.Image = Properties.Resources.Update_Icon;


                DataGridViewImageColumn Archive = new DataGridViewImageColumn();
                Category_datagrid_stocks.Columns.Add(Archive);
                Archive.HeaderText = "";
                Archive.Name = "Archive";
                Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Archive.Image = Properties.Resources.Archive_Icon;

                DataViewAll();

               

            }

            if (searchtxt.Text != "")
            {
                supressor = 1;

            }
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {

            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
            {
                POS_module a = new POS_module();
                this.Hide();
                a.Show();

                Form1.loadingtime = 9000;
                Form1.status = "false";
                Loading_popup b = new Loading_popup();
                b.Show();
            }
            else if (Form1.levelac.Equals("Employee") && Form1.posac.Equals("Authorized") && Form1.status == "true")
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

        private void bunifuImageButton14_Click(object sender, EventArgs e)
        {
            


            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
            {
                Inventory_Module a = new Inventory_Module();
                this.Hide();
                a.Show();

                Form1.loadingtime = 9000;
                Form1.status = "false";
                Loading_popup b = new Loading_popup();
                b.Show();
            }
            else if (Form1.levelac.Equals("Employee") && Form1.posac.Equals("Authorized") && Form1.status == "true")
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
                //MessageBox.Show("Your account do not have access on this Module.");
            }
        }

        private void bunifuImageButton12_Click(object sender, EventArgs e)
        {
            


            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
            {
                stockpurchase_Module a = new stockpurchase_Module();
                this.Hide();
                a.Show();

                Form1.loadingtime = 9000;
                Form1.status = "false";
                Loading_popup b = new Loading_popup();
                b.Show();
            }
            else if (Form1.levelac.Equals("Employee") && Form1.posac.Equals("Authorized") && Form1.status == "true")
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
                //MessageBox.Show("Your account do not have access on this Module.");
            }
        }

        private void bunifuImageButton13_Click(object sender, EventArgs e)
        {
            


            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
            {
                Addunit_module a = new Addunit_module();
                this.Hide();
                a.Show();

                Form1.loadingtime = 9000;
                Form1.status = "false";
                Loading_popup b = new Loading_popup();
                b.Show();
            }
            else if (Form1.levelac.Equals("Employee") && Form1.posac.Equals("Authorized") && Form1.status == "true")
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
                //MessageBox.Show("Your account do not have access on this Module.");
            }
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            Supplierrecord_module a = new Supplierrecord_module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            Accountmanagement_Module a = new Accountmanagement_Module();
            this.Hide();
            a.Show();
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

        private void bunifuImageButton9_Click_1(object sender, EventArgs e)
        {
            


            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
            {
                Accountmanagement_Module a = new Accountmanagement_Module();
                this.Hide();
                a.Show();

                Form1.loadingtime = 9000;
                Form1.status = "false";
                Loading_popup b = new Loading_popup();
                b.Show();
            }
            else if (Form1.levelac.Equals("Employee") && Form1.posac.Equals("Authorized") && Form1.status == "true")
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
                //MessageBox.Show("Your account do not have access on this Module.");
            }
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            


            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
            {
                Suppliermanagement_module a = new Suppliermanagement_module();
                this.Hide();
                a.Show();

                Form1.loadingtime = 9000;
                Form1.status = "false";
                Loading_popup b = new Loading_popup();
                b.Show();
            }
            else if (Form1.levelac.Equals("Employee") && Form1.posac.Equals("Authorized") && Form1.status == "true")
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
                //MessageBox.Show("Your account do not have access on this Module.");
            }
        }

        private void bunifuImageButton6_Click_1(object sender, EventArgs e)
        {
            


            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
            {
                Salesrecord_module a = new Salesrecord_module();
                this.Hide();
                a.Show();

                Form1.loadingtime = 9000;
                Form1.status = "false";
                Loading_popup b = new Loading_popup();
                b.Show();
            }
            else if (Form1.levelac.Equals("Employee") && Form1.posac.Equals("Authorized") && Form1.status == "true")
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
                //MessageBox.Show("Your account do not have access on this Module.");
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
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


            Application.Exit();
        }

        private void bunifuImageButton7_Click_1(object sender, EventArgs e)
        {

            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
            {
                ActivityLog_Module a = new ActivityLog_Module();
                this.Hide();
                a.Show();

                Form1.loadingtime = 9000;
                Form1.status = "false";
                Loading_popup b = new Loading_popup();
                b.Show();
            }
            else if (Form1.levelac.Equals("Employee") && Form1.posac.Equals("Authorized") && Form1.status == "true")
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
                //MessageBox.Show("Your account do not have access on this Module.");
            }
        }

        private void bunifuImageButton8_Click(object sender, EventArgs e)
        {

            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
            {
                Inventory_Module a = new Inventory_Module();
                this.Hide();
                a.Show();

                Form1.loadingtime = 9000;
                Form1.status = "false";
                Loading_popup b = new Loading_popup();
                b.Show();
            }
            else if (Form1.levelac.Equals("Employee") && Form1.posac.Equals("Authorized") && Form1.status == "true")
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
                //MessageBox.Show("Your account do not have access on this Module.");
            }
        }
    }
}
