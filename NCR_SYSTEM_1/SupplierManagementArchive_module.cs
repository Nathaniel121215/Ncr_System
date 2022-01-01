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
    public partial class SupplierManagementArchive_module : Form
    {
        int supressor = 1;

        DataTable dt = new DataTable();

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;


        public static SupplierManagementArchive_module _instance;

        public SupplierManagementArchive_module()
        {
            _instance = this;
            InitializeComponent();
        }

        private void SupplierManagementArchive_module_Load(object sender, EventArgs e)
        {

            datedisplay.Text = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            datedisplay.Select();

            this.Supplier_Datagrid.AllowUserToAddRows = false;
            client = new FireSharp.FirebaseClient(config);


            dt.Columns.Add("Supplier ID");
            dt.Columns.Add("Supplier Name");
            dt.Columns.Add("Supplier Address");
            dt.Columns.Add("Supplier Number");
            dt.Columns.Add("Last Transaction");
            dt.Columns.Add("Date Added");

            dt.Columns.Add("Date Archived");
            dt.Columns.Add("User");

            dt.Columns.Add("Date Archived Searcher");

      


            Supplier_Datagrid.DataSource = dt;



            DataGridViewImageColumn Restore = new DataGridViewImageColumn();
            Supplier_Datagrid.Columns.Add(Restore);
            Restore.HeaderText = "";
            Restore.Name = "";
            Restore.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Restore.Image = Properties.Resources.Restore_Icon;

            Dataviewall();

        }

        public async void Dataviewall()
        {
            foreach (DataGridViewColumn column in Supplier_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


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
                    FirebaseResponse resp1 = await client.GetTaskAsync("SupplierArchive/" + i);
                    SupplierArchive_Class user = resp1.ResultAs<SupplierArchive_Class>();

                    DataRow r = dt.NewRow();
                    r["Supplier ID"] = user.Supplier_ID;
                    r["Supplier Name"] = user.Supplier_Name;
                    r["Supplier Address"] = user.Supplier_Address;
                    r["Supplier Number"] = user.Supplier_Number;
                    r["Last Transaction"] = user.Last_Transaction;
                    r["Date Added"] = user.Supplier_DateAdded;


                    r["Date Archived"] = user.Date_Archive;
                    r["User"] = user.User;

                    DateTime date = Convert.ToDateTime(user.Date_Archive);




                    r["Date Archived Searcher"] = date.ToString("MM/dd/yyyy");



                    dt.Rows.Add(r);
                    gettransactioncount();
                }

                catch
                {

                }

                gettransactioncount();
              
            }
        }

        public void gettransactioncount()
        {
            int transactioncount = 0;
            transactioncount = Supplier_Datagrid.Rows.Count;

            TransactionCounttxt.Text = transactioncount.ToString();


        }

        private void Supplier_Datagrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //restore
            string columnindex = "";

            try
            {
                if (e.ColumnIndex == Supplier_Datagrid.Columns[9].Index)
                {
                    columnindex = Supplier_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();

                    FirebaseResponse resp1 = client.Get("SupplierArchive/" + columnindex);
                    SupplierArchive_Class obj1 = resp1.ResultAs<SupplierArchive_Class>();

                    var data = new SupplierArchive_Class
                    {
                        Supplier_ID = obj1.Supplier_ID,
                        Supplier_Name = obj1.Supplier_Name,
                        Supplier_Address = obj1.Supplier_Address,
                        Supplier_Number = obj1.Supplier_Number,
                        Last_Transaction = obj1.Last_Transaction,
                        Supplier_DateAdded = obj1.Supplier_DateAdded,




                    };

                    FirebaseResponse response = client.Set("Supplier/" + data.Supplier_ID, data);
                    User_class result = response.ResultAs<User_class>();




                    FirebaseResponse resp2 = client.Get("SupplierCounterExisting/node");
                    Counter_class get2 = resp2.ResultAs<Counter_class>();
                    string employee = (Convert.ToInt32(get2.cnt) + 1).ToString();
                    var obj2 = new Counter_class
                    {
                        cnt = employee
                    };

                    SetResponse response2 = client.Set("SupplierCounterExisting/node", obj2);




                    //get archive counter
                    FirebaseResponse resp = client.Get("SupplierArchiveCounter/node");
                    Counter_class get = resp.ResultAs<Counter_class>();

                    //update archive counter
                    var obj = new Counter_class
                    {
                        cnt = (Convert.ToInt32(get.cnt) - 1).ToString(),
                    };

                    SetResponse response4 = client.Set("SupplierArchiveCounter/node", obj);



                    //delete from current table

                    FirebaseResponse response5 = client.Delete("SupplierArchive/" + columnindex);




                    //SUPPLIER ARCHIVE RESTORE EVENT

                    FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                    Counter_class get4 = resp4.ResultAs<Counter_class>();
                    int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                    var data3 = new ActivityLog_Class
                    {
                        Event_ID = cnt4.ToString(),
                        Module = "Supplier Archive Module",
                        Action = "Supplier-ID: " + data.Supplier_ID + "   Supplier Restored",
                        Date = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                        User = Form1.username,
                        Accountlvl = Form1.levelac,

                    };



                    FirebaseResponse response6 = client.Set("ActivityLog/" + data3.Event_ID, data3);



                    var obj4 = new Counter_class
                    {
                        cnt = data3.Event_ID

                    };

                    SetResponse response7 = client.Set("ActivityLogCounter/node", obj4);






                    Dataviewall();
                }
            }
            catch
            {

            }
        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = "[" + combofilter.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

            Supplier_Datagrid.DataSource = null;
            Supplier_Datagrid.Rows.Clear();
            Supplier_Datagrid.Columns.Clear();
            Supplier_Datagrid.DataSource = dv;




            DataGridViewImageColumn Restore = new DataGridViewImageColumn();
            Supplier_Datagrid.Columns.Add(Restore);
            Restore.HeaderText = "";
            Restore.Name = "";
            Restore.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Restore.Image = Properties.Resources.Restore_Icon;


            gettransactioncount();

            filterlabeltxt.Text = "";
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


           

                DataGridViewImageColumn Restore = new DataGridViewImageColumn();
                Supplier_Datagrid.Columns.Add(Restore);
                Restore.HeaderText = "";
                Restore.Name = "";
                Restore.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Restore.Image = Properties.Resources.Restore_Icon;



                Dataviewall();

                filterlabeltxt.Text = "";



            }

            if (searchtxt.Text != "")
            {
                supressor = 1;

            }
        }

        public void filter()
        {
            DataView dv = new DataView(dt);
            string date1 = SupplierArchiveFilter_popup.startdate;
            string date2 = SupplierArchiveFilter_popup.enddate;
            string user = SupplierArchiveFilter_popup.user;

            if (SupplierArchiveFilter_popup.user == "")
            {

                dv.RowFilter = "[Date Archived Searcher]  >='" + date1 + "'AND [Date Archived Searcher] <='" + date2 + "'";

                Supplier_Datagrid.DataSource = null;
                Supplier_Datagrid.Rows.Clear();
                Supplier_Datagrid.Columns.Clear();
                Supplier_Datagrid.DataSource = dv;



            }


            else
            {
                dv.RowFilter = "[Date Archived Searcher]  >='" + date1 + "'AND [Date Archived Searcher] <='" + date2 + "'" + " AND [User] LIKE '%" + user + "%'";

                Supplier_Datagrid.DataSource = null;
                Supplier_Datagrid.Rows.Clear();
                Supplier_Datagrid.Columns.Clear();
                Supplier_Datagrid.DataSource = dv;


            }

            DataGridViewImageColumn Restore = new DataGridViewImageColumn();
            Supplier_Datagrid.Columns.Add(Restore);
            Restore.HeaderText = "";
            Restore.Name = "";
            Restore.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Restore.Image = Properties.Resources.Restore_Icon;

            filterlabeltxt.Text = date1 + " to " + date2;

            gettransactioncount();

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            SupplierArchiveFilter_popup c = new SupplierArchiveFilter_popup();
            c.Show();
        }
    }
}