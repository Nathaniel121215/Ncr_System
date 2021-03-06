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
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Image = System.Drawing.Image;

namespace NCR_SYSTEM_1
{
    public partial class Stockadjustmentrecord_module : Form
    {
        public static string remarks;


        public static Stockadjustmentrecord_module _instance;

        public Stockadjustmentrecord_module()
        {
            _instance = this;
            InitializeComponent();
        }
        

        private Image[] StatusImgs;

        int supressor = 1;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;


        DataTable dt = new DataTable();
        DataTable printer = new DataTable();


        //data for extract

        public static string grosssales2;
        public static string trasacntioncount2;
        public static string reason2;
        public static string percentage2;
        public static string date3;


        private void Stockadjustmentrecord_module_Load(object sender, EventArgs e)
        {

            datedisplay.Text = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            datedisplay.Select();
            client = new FireSharp.FirebaseClient(config);

            this.StockAdjustment_Datagrid.AllowUserToAddRows = false;
       


            dt.Columns.Add("Event ID");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Action");
            dt.Columns.Add("Value");
            dt.Columns.Add("User");
            dt.Columns.Add("Date");
            dt.Columns.Add("Reason");
            dt.Columns.Add("Remarks");

            dt.Columns.Add("Date Searcher");

            printer.Columns.Add("Event ID");
            printer.Columns.Add("Product Name");
            printer.Columns.Add("Action");
            printer.Columns.Add("Value");
            printer.Columns.Add("User");
            printer.Columns.Add("Date");
            printer.Columns.Add("Reason");



            StockAdjustment_Datagrid.DataSource = dt;
            printtable.DataSource = printer;


            DataGridViewImageColumn Reason = new DataGridViewImageColumn();
            StockAdjustment_Datagrid.Columns.Add(Reason);
            Reason.HeaderText = "Reason";
            Reason.Name = "Reason";
            Reason.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Reason.Image = Properties.Resources.loading;

            DataGridViewImageColumn remarks = new DataGridViewImageColumn();
            StockAdjustment_Datagrid.Columns.Add(remarks);
            remarks.HeaderText = "";
            remarks.Name = "";
            remarks.ImageLayout = DataGridViewImageCellLayout.Zoom;
            remarks.Image = Properties.Resources.Group_203;


            DataViewAll();
            printdata();

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

        public async void printdata()
        {
            printer.Rows.Clear();

            int i = 0;
            FirebaseResponse resp = await client.GetTaskAsync("StockAdjustmentCounter/node");
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

                    FirebaseResponse resp1 = await client.GetTaskAsync("StockAdjustment/" + i);
                    StockAdjustment_Class obj1 = resp1.ResultAs<StockAdjustment_Class>();

                    DataRow r = printer.NewRow();
                    r["Event ID"] = obj1.Event_ID;
                    r["Product Name"] = obj1.Product_Name;
                    r["Action"] = obj1.Action;
                    r["Value"] = obj1.Value;
                    r["User"] = obj1.User;
                    r["Reason"] = obj1.Reason;

                    DateTime date = Convert.ToDateTime(obj1.Date);

                    r["Date"] = date.ToString("MM/dd/yyyy");

                    printer.Rows.Add(r);
                }

                catch
                {

                }
            }
        }

        public async void DataViewAll()
        {

            foreach (DataGridViewColumn column in StockAdjustment_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            StockAdjustment_Datagrid.Columns[6].Visible = false;
            StockAdjustment_Datagrid.Columns[7].Visible = false;
            StockAdjustment_Datagrid.Columns[8].Visible = false;

            DataGridViewColumn column0 = StockAdjustment_Datagrid.Columns[0];
            column0.Width = 100;

            DataGridViewColumn column1 = StockAdjustment_Datagrid.Columns[1];
            column1.Width = 280;

            DataGridViewColumn column2 = StockAdjustment_Datagrid.Columns[2];
            column2.Width = 280;

            DataGridViewColumn column3 = StockAdjustment_Datagrid.Columns[3];
            column3.Width = 150;

            DataGridViewColumn column4 = StockAdjustment_Datagrid.Columns[4];
            column4.Width = 190;

            DataGridViewColumn column5 = StockAdjustment_Datagrid.Columns[5];
            column5.Width = 170;

          


            StockAdjustment_Datagrid.Columns[9].DefaultCellStyle.Padding = new Padding(0, 0, 20, 0);

            dt.Rows.Clear();

            int i = 0;
            FirebaseResponse resp = await client.GetTaskAsync("StockAdjustmentCounter/node");
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
                
                    FirebaseResponse resp1 = await client.GetTaskAsync("StockAdjustment/" + i);
                    StockAdjustment_Class obj1 = resp1.ResultAs<StockAdjustment_Class>();

                    DataRow r = dt.NewRow();
                    r["Event ID"] = obj1.Event_ID;
                    r["Product Name"] = obj1.Product_Name;
                    r["Action"] = obj1.Action;
                    r["Value"] = obj1.Value;
                    r["User"] = obj1.User;
                    r["Date"] = obj1.Date;
                    r["Reason"] = obj1.Reason;
                    r["Remarks"] = obj1.Remarks;

                    DateTime date = Convert.ToDateTime(obj1.Date);

                    r["Date Searcher"] = date.ToString("MM/dd/yyyy");

                   
                    dt.Rows.Add(r);
                }

                catch
                {

                }
            }


            try
            {





                foreach (DataGridViewRow row in StockAdjustment_Datagrid.Rows)
                {

                    try
                    {


                        StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.damage_item, NCR_SYSTEM_1.Properties.Resources.lost_item, NCR_SYSTEM_1.Properties.Resources.wrong_input, NCR_SYSTEM_1.Properties.Resources.others, NCR_SYSTEM_1.Properties.Resources.Group_204 };



                        if (row.Cells[6].Value.ToString() == "Other") 
                        {
                            row.Cells[9].Value = StatusImgs[3];   
                        }

                        if (row.Cells[6].Value.ToString() == "Wrong Input")
                        {
                            row.Cells[9].Value = StatusImgs[2];
                        }

                        if (row.Cells[6].Value.ToString() == "Lost Item")
                        {
                            row.Cells[9].Value = StatusImgs[1];
                        }

                        if (row.Cells[6].Value.ToString() == "Damaged Item") 
                        {
                            row.Cells[9].Value = StatusImgs[0];
                        }

                        if (row.Cells[6].Value.ToString() == "Replacement")
                        {
                            row.Cells[9].Value = StatusImgs[4];
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

            getvalue();
            gettransactioncount();
        }

        public void printfilter()
        {


            DataView dv = new DataView(printer);
            string date1 = StockAdjustmentFilter_popup.startdate;
            string date2 = StockAdjustmentFilter_popup.enddate;
            string reason = StockAdjustmentFilter_popup.reason;
            string user = StockAdjustmentFilter_popup.user;


            if (StockAdjustmentFilter_popup.reason != "" && StockAdjustmentFilter_popup.user == "" && StockAdjustmentFilter_popup.reason == "All")
            {

                dv.RowFilter = "[Date]  >='" + date1 + "'AND [Date] <='" + date2 + "'";

                printtable.DataSource = null;
                printtable.Rows.Clear();
                printtable.Columns.Clear();
                printtable.DataSource = dv;


            }

            else if (StockAdjustmentFilter_popup.reason != "" && StockAdjustmentFilter_popup.user != "" && StockAdjustmentFilter_popup.reason != "All")
            {

                dv.RowFilter = "[Date]  >='" + date1 + "'AND [Date] <='" + date2 + "' " + " AND [Reason] LIKE '%" + reason + "%'" + " AND [User] LIKE '%" + user + "%'";


                printtable.DataSource = null;
                printtable.Rows.Clear();
                printtable.Columns.Clear();
                printtable.DataSource = dv;
            }

            else if (StockAdjustmentFilter_popup.reason != "" && StockAdjustmentFilter_popup.user != "" && StockAdjustmentFilter_popup.reason == "All")
            {

                dv.RowFilter = "[Date]  >='" + date1 + "'AND [Date] <='" + date2 + "' " + " AND [User] LIKE '%" + user + "%'";

                printtable.DataSource = null;
                printtable.Rows.Clear();
                printtable.Columns.Clear();
                printtable.DataSource = dv;
            }

            else if (StockAdjustmentFilter_popup.reason != "" && StockAdjustmentFilter_popup.user == "" && StockAdjustmentFilter_popup.reason != "All")
            {
                dv.RowFilter = "[Date]  >='" + date1 + "'AND [Date] <='" + date2 + "' " + " AND [Reason] LIKE '%" + reason + "%'";

                printtable.DataSource = null;
                printtable.Rows.Clear();
                printtable.Columns.Clear();
                printtable.DataSource = dv;
            }
            else
            {


            }
        }

        public void getvalue()
        {
            decimal totalsales = 0;


            for (int a = 0; a < StockAdjustment_Datagrid.Rows.Count; a++)
            {
                totalsales += Convert.ToDecimal(StockAdjustment_Datagrid.Rows[a].Cells[3].Value);
            }

            Salestxt.Text = totalsales.ToString();



        }

        public void gettransactioncount()
        {
            int transactioncount = 0;
            transactioncount = StockAdjustment_Datagrid.Rows.Count;

            TransactionCounttxt.Text = transactioncount.ToString();


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void searchtxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (searchtxt.Text == "" && supressor == 1)
            {
                supressor = 0;

                StockAdjustment_Datagrid.DataSource = null;
                StockAdjustment_Datagrid.Rows.Clear();
                StockAdjustment_Datagrid.Columns.Clear();
                StockAdjustment_Datagrid.DataSource = dt;


                DataGridViewImageColumn Reason = new DataGridViewImageColumn();
                StockAdjustment_Datagrid.Columns.Add(Reason);
                Reason.HeaderText = "Reason";
                Reason.Name = "Reason";
                Reason.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Reason.Image = Properties.Resources.loading;

                DataGridViewImageColumn remarks = new DataGridViewImageColumn();
                StockAdjustment_Datagrid.Columns.Add(remarks);
                remarks.HeaderText = "";
                remarks.Name = "";
                remarks.ImageLayout = DataGridViewImageCellLayout.Zoom;
                remarks.Image = Properties.Resources.Group_203;

                DataViewAll();

                filterlabeltxt.Text = "";

            }

            if (searchtxt.Text != "")
            {
                supressor = 1;

            }
        }

 

        public void searchupdate()
        {
            foreach (DataGridViewColumn column in StockAdjustment_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            StockAdjustment_Datagrid.Columns[6].Visible = false;
            StockAdjustment_Datagrid.Columns[7].Visible = false;
            StockAdjustment_Datagrid.Columns[8].Visible = false;

            DataGridViewColumn column0 = StockAdjustment_Datagrid.Columns[0];
            column0.Width = 100;

            DataGridViewColumn column1 = StockAdjustment_Datagrid.Columns[1];
            column1.Width = 280;

            DataGridViewColumn column2 = StockAdjustment_Datagrid.Columns[2];
            column2.Width = 280;

            DataGridViewColumn column3 = StockAdjustment_Datagrid.Columns[3];
            column3.Width = 150;

            DataGridViewColumn column4 = StockAdjustment_Datagrid.Columns[4];
            column4.Width = 190;

            DataGridViewColumn column5 = StockAdjustment_Datagrid.Columns[5];
            column5.Width = 170;




            StockAdjustment_Datagrid.Columns[9].DefaultCellStyle.Padding = new Padding(0, 0, 20, 0);




            try
            {





                foreach (DataGridViewRow row in StockAdjustment_Datagrid.Rows)
                {

                    try
                    {


                        StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.damage_item, NCR_SYSTEM_1.Properties.Resources.lost_item, NCR_SYSTEM_1.Properties.Resources.wrong_input, NCR_SYSTEM_1.Properties.Resources.others, NCR_SYSTEM_1.Properties.Resources.Group_204 };



                        if (row.Cells[6].Value.ToString() == "Other")
                        {
                            row.Cells[9].Value = StatusImgs[3];
                        }

                        if (row.Cells[6].Value.ToString() == "Wrong Input")
                        {
                            row.Cells[9].Value = StatusImgs[2];
                        }

                        if (row.Cells[6].Value.ToString() == "Lost Item")
                        {
                            row.Cells[9].Value = StatusImgs[1];
                        }

                        if (row.Cells[6].Value.ToString() == "Damaged Item")
                        {
                            row.Cells[9].Value = StatusImgs[0];
                        }

                        if (row.Cells[6].Value.ToString() == "Replacement")
                        {
                            row.Cells[9].Value = StatusImgs[4];
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
            getvalue();
            gettransactioncount();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if (searchtxt.Text != "" & Form1.status == "true")
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "[" + bunifuDropdown1.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

                StockAdjustment_Datagrid.DataSource = null;
                StockAdjustment_Datagrid.Rows.Clear();
                StockAdjustment_Datagrid.Columns.Clear();
                StockAdjustment_Datagrid.DataSource = dv;



                DataGridViewImageColumn Reason = new DataGridViewImageColumn();
                StockAdjustment_Datagrid.Columns.Add(Reason);
                Reason.HeaderText = "Reason";
                Reason.Name = "Reason";
                Reason.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Reason.Image = Properties.Resources.loading;

                DataGridViewImageColumn remarks = new DataGridViewImageColumn();
                StockAdjustment_Datagrid.Columns.Add(remarks);
                remarks.HeaderText = "";
                remarks.Name = "";
                remarks.ImageLayout = DataGridViewImageCellLayout.Zoom;
                remarks.Image = Properties.Resources.Group_203;

                foreach (DataGridViewColumn column in StockAdjustment_Datagrid.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }


                searchupdate();
            }
            else
            {

            }
                


        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true")
            {
                StockAdjustmentFilter_popup a = new StockAdjustmentFilter_popup();
                a.Show();
                Form1.status = "false";
            }
            else
            {
                MessageBox.Show("The Module is still loading or a window is currently open.");
            }
                
        }

        public void filter()
        {
            DataView dv = new DataView(dt);
            string date1 = StockAdjustmentFilter_popup.startdate;
            string date2 = StockAdjustmentFilter_popup.enddate;
            string reason = StockAdjustmentFilter_popup.reason;
            string user = StockAdjustmentFilter_popup.user;

     


            if (StockAdjustmentFilter_popup.reason != "" && StockAdjustmentFilter_popup.user == "" && StockAdjustmentFilter_popup.reason == "All")
            {

                dv.RowFilter = "[Date Searcher]  >='" + date1 + "'AND [Date Searcher] <='" + date2 + "'";

                StockAdjustment_Datagrid.DataSource = null;
                StockAdjustment_Datagrid.Rows.Clear();
                StockAdjustment_Datagrid.Columns.Clear();
                StockAdjustment_Datagrid.DataSource = dv;


            }

            else if (StockAdjustmentFilter_popup.reason != "" && StockAdjustmentFilter_popup.user != "" && StockAdjustmentFilter_popup.reason != "All")
            {

                dv.RowFilter = "[Date Searcher]  >='" + date1 + "'AND [Date Searcher] <='" + date2 + "' " + " AND [Reason] LIKE '%" + reason + "%'" + " AND [User] LIKE '%" + user + "%'";


                StockAdjustment_Datagrid.DataSource = null;
                StockAdjustment_Datagrid.Rows.Clear();
                StockAdjustment_Datagrid.Columns.Clear();
                StockAdjustment_Datagrid.DataSource = dv;
            }

            else if (StockAdjustmentFilter_popup.reason != "" && StockAdjustmentFilter_popup.user != "" && StockAdjustmentFilter_popup.reason == "All")
            {

                dv.RowFilter = "[Date Searcher]  >='" + date1 + "'AND [Date Searcher] <='" + date2 + "' " + " AND [User] LIKE '%" + user + "%'";

                StockAdjustment_Datagrid.DataSource = null;
                StockAdjustment_Datagrid.Rows.Clear();
                StockAdjustment_Datagrid.Columns.Clear();
                StockAdjustment_Datagrid.DataSource = dv;
            }

            else if (StockAdjustmentFilter_popup.reason != "" && StockAdjustmentFilter_popup.user == "" && StockAdjustmentFilter_popup.reason != "All")
            {
                dv.RowFilter = "[Date Searcher]  >='" + date1 + "'AND [Date Searcher] <='" + date2 + "' " + " AND [Reason] LIKE '%" + reason + "%'";

                StockAdjustment_Datagrid.DataSource = null;
                StockAdjustment_Datagrid.Rows.Clear();
                StockAdjustment_Datagrid.Columns.Clear();
                StockAdjustment_Datagrid.DataSource = dv;
            }
            else
            {


            }



            filterlabeltxt.Text = date1 + " to " + date2;

            DataGridViewImageColumn Reason = new DataGridViewImageColumn();
            StockAdjustment_Datagrid.Columns.Add(Reason);
            Reason.HeaderText = "Reason";
            Reason.Name = "Reason";
            Reason.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Reason.Image = Properties.Resources.loading;

            DataGridViewImageColumn remarks = new DataGridViewImageColumn();
            StockAdjustment_Datagrid.Columns.Add(remarks);
            remarks.HeaderText = "";
            remarks.Name = "";
            remarks.ImageLayout = DataGridViewImageCellLayout.Zoom;
            remarks.Image = Properties.Resources.Group_203;

            getvalue();
            gettransactioncount();
            searchupdate();
        }

        private void bunifuImageButton10_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    Supplierrecord_module a = new Supplierrecord_module();
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

        private void bunifuImageButton9_Click(object sender, EventArgs e)
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

        private void bunifuImageButton15_Click(object sender, EventArgs e)
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }

            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
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

        private void bunifuImageButton13_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    Stockadjustmentrecord_module a = new Stockadjustmentrecord_module();
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }

            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
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

        public void extract()
        {
            DataView dv = new DataView(dt);
            string date1 = StockAdjustmentReportExtractionFilter_popup.startdate;
            string date2 = StockAdjustmentReportExtractionFilter_popup.enddate;
            string percentage = StockAdjustmentReportExtractionFilter_popup.percentage;
            string reason = StockAdjustmentReportExtractionFilter_popup.reason;



            if (StockAdjustmentReportExtractionFilter_popup.reason != "" && StockAdjustmentReportExtractionFilter_popup.percentage != "" && StockAdjustmentReportExtractionFilter_popup.reason != "All")
            {

                dv.RowFilter = "[Date Searcher]  >='" + date1 + "'AND [Date Searcher] <='" + date2 + "' " + " AND [Reason] LIKE '%" + reason + "%'";

                StockAdjustment_Datagrid.DataSource = null;
                StockAdjustment_Datagrid.Rows.Clear();
                StockAdjustment_Datagrid.Columns.Clear();
                StockAdjustment_Datagrid.DataSource = dv;


            }

            else
            {
                dv.RowFilter = "[Date Searcher]  >='" + date1 + "'AND [Date Searcher] <='" + date2 + "' ";

                StockAdjustment_Datagrid.DataSource = null;
                StockAdjustment_Datagrid.Rows.Clear();
                StockAdjustment_Datagrid.Columns.Clear();
                StockAdjustment_Datagrid.DataSource = dv;
            }



            filterlabeltxt.Text = date1 + " to " + date2;

            DataGridViewImageColumn Reason = new DataGridViewImageColumn();
            StockAdjustment_Datagrid.Columns.Add(Reason);
            Reason.HeaderText = "Reason";
            Reason.Name = "Reason";
            Reason.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Reason.Image = Properties.Resources.loading;

            DataGridViewImageColumn remarks = new DataGridViewImageColumn();
            StockAdjustment_Datagrid.Columns.Add(remarks);
            remarks.HeaderText = "";
            remarks.Name = "";
            remarks.ImageLayout = DataGridViewImageCellLayout.Zoom;
            remarks.Image = Properties.Resources.Group_203;

            getvalue();
            gettransactioncount();
            searchupdate();

            grosssales2 = Salestxt.Text.ToString();
            trasacntioncount2 = TransactionCounttxt.Text.ToString();
            reason2 = StockAdjustmentReportExtractionFilter_popup.reason;
            percentage2 = StockAdjustmentReportExtractionFilter_popup.percentage;
            date3 = filterlabeltxt.Text;



            StockAdjustmentReportExtraction_popup a = new StockAdjustmentReportExtraction_popup();
            a.Show();

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
            {
                StockAdjustmentReportExtractionFilter_popup a = new StockAdjustmentReportExtractionFilter_popup();
                a.Show();
                Form1.status = "false";
            }
            else
            {
                MessageBox.Show("The Module is still loading or a window is currently open.");
            }
                
        }

        private void StockAdjustment_Datagrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if(Form1.status=="true")
            {
                string columnindex = "";


                //remarks
                if (e.ColumnIndex == StockAdjustment_Datagrid.Columns[10].Index)
                {
                    columnindex = StockAdjustment_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();


                    StockAdjustment_Datagrid.Rows[e.RowIndex].Selected = true;

                    remarks = StockAdjustment_Datagrid.Rows[e.RowIndex].Cells[7].Value.ToString();



                    StockAdjustmentRemarks_popup c = new StockAdjustmentRemarks_popup();
                    c.Show();
                    Form1.status = "false";

                }
            }
            else
            {
                MessageBox.Show("The Module is still loading or a window is currently open.");
            }
           
        
        
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
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
        private void copyAlltoClipboard()
        {
            StockAdjustment_Datagrid.SelectAll();
            DataObject dataObj = StockAdjustment_Datagrid.GetClipboardContent();
            if (dataObj != null)
             Clipboard.SetDataObject(dataObj);
        }

        private void searchtxt_Enter(object sender, EventArgs e)
        {
            searchtxt.Text = "";
        }

        private void searchtxt_Leave(object sender, EventArgs e)
        {
            if (searchtxt.Text == "")
            {
                searchtxt.Text = "Type here to filter Stock Adjustment Record Content";
            }
            else
            {

            }
        }

        private void bunifuImageButton11_Click(object sender, EventArgs e)
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }
            }
        }

        private void StockAdjustment_Datagrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void pdfprint_Click(object sender, EventArgs e)
        {
            Document doc = new Document(new iTextSharp.text.Rectangle(288f, 144f), 10, 10, 10, 10);
            doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            string header = "NCR Gravel and Sand Enterprises";
            var _pdf_table = new PdfPTable(2);
            PdfPCell hc = new PdfPCell();


            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(("My print.pdf"), FileMode.Create));
            doc.Open();
            Paragraph space1 = new Paragraph("\n");

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

            iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 20, 1, BaseColor.BLACK);

            Paragraph prgheading = new Paragraph();
            prgheading.Alignment = Element.ALIGN_CENTER;
            prgheading.Add(new Chunk(header, times));
            doc.Add(space1);
            doc.Add(prgheading);
            doc.Add(space1);

            _pdf_table = new PdfPTable(printtable.Columns.Count);

            for (int j = 0; j < printtable.Columns.Count; j++)
            {
                _pdf_table.AddCell(new Phrase(printtable.Columns[j].HeaderText));
            }

            _pdf_table.HeaderRows = 1;

            for (int i = 0; i < printtable.Rows.Count; i++)
            {

                for (int k = 0; k < printtable.Columns.Count; k++)
                {

                    if (printtable[k, i].Value != null)
                    {

                        _pdf_table.AddCell(new Phrase(printtable[k, i].Value.ToString()));
                    }

                }

            }
            doc.Add(_pdf_table);

            doc.Close();
            //change to your directory
            System.Diagnostics.Process.Start(@"My print.pdf");
        }
    }
}
