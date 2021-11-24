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
    public partial class Suppliermanagement_module : Form
    {
        public static string checker = "";

        int supressor = 1;

        public static string Supplier_ID = "";
        public static string Supplier_Name = "";
        public static string Supplier_Address = "";
        public static string Supplier_Number = "";
        public static string Last_Transaction = "";
        public static string Date_Added = "";
  


        DataTable dt = new DataTable();

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public static Suppliermanagement_module _instance;

        public Suppliermanagement_module()
        {
            _instance = this;
            InitializeComponent();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
          

            if (checker.Equals("allow"))
            {
                Addsupplier_popup a = new Addsupplier_popup();
                a.Show();

                checker = "dontallow";
            }
            else
            {
                MessageBox.Show("The tab is currently already open.");
            }
        }

        private void Suppliermanagement_module_Load(object sender, EventArgs e)
        {
            this.Supplier_Datagrid.AllowUserToAddRows = false;
            datedisplay.Text = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            datedisplay.Select();
            client = new FireSharp.FirebaseClient(config);

            dt.Columns.Add("Supplier ID");
            dt.Columns.Add("Supplier Name");
            dt.Columns.Add("Supplier Address");
            dt.Columns.Add("Supplier Number");
            dt.Columns.Add("Last Transaction");
            dt.Columns.Add("Date Added");
 



            Supplier_Datagrid.DataSource = dt;


            DataGridViewImageColumn update = new DataGridViewImageColumn();
            Supplier_Datagrid.Columns.Add(update);
            update.HeaderText = "";
            update.Name = "update";
            update.ImageLayout = DataGridViewImageCellLayout.Zoom;
            update.Image = Properties.Resources.Update_Icon;


            DataGridViewImageColumn Archive = new DataGridViewImageColumn();
            Supplier_Datagrid.Columns.Add(Archive);
            Archive.HeaderText = "";
            Archive.Name = "Archive";
            Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Archive.Image = Properties.Resources.Archive_Icon;


            dataview();
        }

        public async void dataview()
        {

            foreach (DataGridViewColumn column in Supplier_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //visual
            DataGridViewColumn column1 = Supplier_Datagrid.Columns[1];
            column1.Width = 210;

            DataGridViewColumn column2 = Supplier_Datagrid.Columns[2];
            column2.Width = 270;

            DataGridViewColumn column3 = Supplier_Datagrid.Columns[3];
            column3.Width = 220;

            DataGridViewColumn column4 = Supplier_Datagrid.Columns[4];
            column4.Width = 170;

            DataGridViewColumn column5 = Supplier_Datagrid.Columns[5];
            column5.Width = 170;


            DataGridViewColumn column6 = Supplier_Datagrid.Columns[6];
            column6.Width = 80;

            DataGridViewColumn column7 = Supplier_Datagrid.Columns[7];
            column7.Width = 80;




            dt.Rows.Clear();
            int i = 0;
            FirebaseResponse resp = await client.GetTaskAsync("SupplierCounter/node");
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
                    FirebaseResponse resp1 = await client.GetTaskAsync("Supplier/" + i);
                    Supplier_Class user = resp1.ResultAs<Supplier_Class>();

                 
                    DataRow r = dt.NewRow();

                    r["Supplier ID"] = user.Supplier_ID;
                    r["Supplier Name"] = user.Supplier_Name;
                    r["Supplier Address"] = user.Supplier_Address;
                    r["Supplier Number"] = user.Supplier_Number;
                    r["Last Transaction"] = user.Supplier_LastTransaction;

                    if(user.Supplier_LastTransaction==null)
                    {
                        r["Last Transaction"] = "None";
                    }
                    r["Date Added"] = user.Supplier_DateAdded;
               



                    dt.Rows.Add(r);
                }

                catch
                {

                }


            }

            checker = "allow";
        }

        private void Supplier_Datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnindex = "";

            try
            {
                //delete
                if (e.ColumnIndex == Supplier_Datagrid.Columns[7].Index)
                {





                    if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                    {

                        columnindex = Supplier_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();

                        FirebaseResponse response = client.Delete("Supplier/" + columnindex);


                       
                            FirebaseResponse resp2 = client.Get("SupplierCounterExisting/node");
                            Counter_class get2 = resp2.ResultAs<Counter_class>();
                            string employee = (Convert.ToInt32(get2.cnt) - 1).ToString();
                            var obj2 = new Counter_class
                            {
                                cnt = employee
                            };

                            SetResponse response2 = client.Set("SupplierCounterExisting/node", obj2);
                        


                        dataview();

                    }

                    else

                    {
                    }



                }
                //update
                if (e.ColumnIndex == Supplier_Datagrid.Columns[6].Index)
                {
                    columnindex = Supplier_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();


                    Supplier_Datagrid.Rows[e.RowIndex].Selected = true;

                    Supplier_ID = Supplier_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                    Supplier_Name = Supplier_Datagrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                    Supplier_Address = Supplier_Datagrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                    Supplier_Number = Supplier_Datagrid.Rows[e.RowIndex].Cells[3].Value.ToString();
                    Last_Transaction = Supplier_Datagrid.Rows[e.RowIndex].Cells[4].Value.ToString();
                    Date_Added = Supplier_Datagrid.Rows[e.RowIndex].Cells[5].Value.ToString();
           


                    Updatesupplier_popup update = new Updatesupplier_popup();

                    update.Show();


                }


            }
            catch
            {

            }
        }

        private void searchtxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (searchtxt.Text == "" && supressor == 1)
            {
                supressor = 0;

                Supplier_Datagrid.DataSource = null;
                Supplier_Datagrid.Rows.Clear();
                Supplier_Datagrid.Columns.Clear();
                Supplier_Datagrid.DataSource = dt;




                DataGridViewImageColumn update = new DataGridViewImageColumn();
                Supplier_Datagrid.Columns.Add(update);
                update.HeaderText = "";
                update.Name = "update";
                update.ImageLayout = DataGridViewImageCellLayout.Zoom;
                update.Image = Properties.Resources.Update_Icon;


                DataGridViewImageColumn Archive = new DataGridViewImageColumn();
                Supplier_Datagrid.Columns.Add(Archive);
                Archive.HeaderText = "";
                Archive.Name = "Archive";
                Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Archive.Image = Properties.Resources.Archive_Icon;

                dataview();

               

            }

            if (searchtxt.Text != "")
            {
                supressor = 1;

            }
        }

        private void a_Click(object sender, EventArgs e)
        {
            



            DataView dv = new DataView(dt);
            dv.RowFilter = "[" + combofilter.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

            Supplier_Datagrid.DataSource = null;
            Supplier_Datagrid.Rows.Clear();
            Supplier_Datagrid.Columns.Clear();
            Supplier_Datagrid.DataSource = dv;

            DataGridViewImageColumn update = new DataGridViewImageColumn();
            Supplier_Datagrid.Columns.Add(update);
            update.HeaderText = "";
            update.Name = "update";
            update.ImageLayout = DataGridViewImageCellLayout.Zoom;
            update.Image = Properties.Resources.Update_Icon;


            DataGridViewImageColumn Archive = new DataGridViewImageColumn();
            Supplier_Datagrid.Columns.Add(Archive);
            Archive.HeaderText = "";
            Archive.Name = "Archive";
            Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Archive.Image = Properties.Resources.Archive_Icon;


            foreach (DataGridViewColumn column in Supplier_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //visual
            DataGridViewColumn column1 = Supplier_Datagrid.Columns[1];
            column1.Width = 210;

            DataGridViewColumn column2 = Supplier_Datagrid.Columns[2];
            column2.Width = 270;

            DataGridViewColumn column3 = Supplier_Datagrid.Columns[3];
            column3.Width = 220;

            DataGridViewColumn column4 = Supplier_Datagrid.Columns[4];
            column4.Width = 170;

            DataGridViewColumn column5 = Supplier_Datagrid.Columns[5];
            column5.Width = 170;

            DataGridViewColumn column6 = Supplier_Datagrid.Columns[6];
            column6.Width = 80;

            DataGridViewColumn column7 = Supplier_Datagrid.Columns[7];
            column7.Width = 80;


        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton19_Click(object sender, EventArgs e)
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

        private void bunifuImageButton17_Click(object sender, EventArgs e)
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

        private void bunifuImageButton16_Click(object sender, EventArgs e)
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

        private void bunifuImageButton6_Click(object sender, EventArgs e)
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

        private void bunifuImageButton9_Click_1(object sender, EventArgs e)
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

        private void bunifuImageButton18_Click(object sender, EventArgs e)
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

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
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
                InventoryArchive_Module a = new InventoryArchive_Module();
                this.Hide();
                a.Show();

                Form1.loadingtime = 9000;
                Form1.status = "false";
                Loading_popup b = new Loading_popup();
                b.Show();
            }
            else if (Form1.levelac.Equals("Employee") && Form1.posac.Equals("Authorized") && Form1.status == "true")
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
                //MessageBox.Show("Your account do not have access on this Module.");
            }
        }
    }
}
