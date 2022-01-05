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

namespace NCR_SYSTEM_1
{
    public partial class Supplierrecord_module : Form
    {
        public static string checker = "";

        public static int cartcount;

        public static List<string> productnamelist = new List<string>();
        public static List<string> quantitylist = new List<string>();
        public static List<string> Unitlist = new List<string>();
        public static List<string> Pricelist = new List<string>();



        int supressor = 1;


        public static string ID;
        public static string refnumber;
        public static string date;
        public static string items;
        public static string sub;
        public static string fee;
        public static string total;
        public static string supp;
        public static string assist;
        public static string remarks;
        public static string amount;
        public static string change;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        DataTable dt = new DataTable();

        public static Supplierrecord_module _instance;

        //data for extract

        public static string grosssales2;
        public static string trasacntioncount2;
        public static string supplier2;
        public static string percentage2;
        public static string date3;

        public Supplierrecord_module()
        {
            InitializeComponent();
            _instance = this;
        }

        private void Supplierrecord_module_Load(object sender, EventArgs e)
        {
            datedisplay.Text = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            datedisplay.Select();
            client = new FireSharp.FirebaseClient(config);
        
            this.Supplier_Datagrid.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToAddRows = false;


            dt.Columns.Add("Restock ID");
            dt.Columns.Add("Reference Number");
            dt.Columns.Add("Supplier Name");
            dt.Columns.Add("Transaction Payment");
            dt.Columns.Add("Assisted By");
            dt.Columns.Add("Transaction Date");

            dt.Columns.Add("Transaction Date Searcher");


            Supplier_Datagrid.DataSource = dt;

            //DataGridViewButtonColumn View = new DataGridViewButtonColumn();
            //Supplier_Datagrid.Columns.Add(View);
            //View.HeaderText = "View Detail";
            //View.Text = "View Detail";
            //View.Name = "view";
            //View.UseColumnTextForButtonValue = true;

            DataGridViewImageColumn View = new DataGridViewImageColumn();
            Supplier_Datagrid.Columns.Add(View);
            View.HeaderText = "";
            View.Name = "View";
            View.ImageLayout = DataGridViewImageCellLayout.Zoom;
            View.Image = Properties.Resources.view2;

           


            dataGridView1.Columns.Add("Q", "Quantity");
            dataGridView1.Columns.Add("U", "Unit");
            dataGridView1.Columns.Add("T", "Item");
            dataGridView1.Columns.Add("P", "Price");

            //dataGridView1.Columns.Add("Q", "Quantity");
            //dataGridView1.Columns.Add("U", "Unit");
            //dataGridView1.Columns.Add("T", "Item");
            //dataGridView1.Columns.Add("P", "Price");





            DataViewAll();

            //accountlvldisplay

            if (Form1.levelac == "Admin")
            {
                accountinfolvl.Text = "Login as Administrator";
            }
            else
            {
                accountinfolvl.Text = "Login as Employee";
            }


        }

        public void getsales()
        {
            decimal totalsales = 0;


            for (int a = 0; a < Supplier_Datagrid.Rows.Count; a++)
            {
                totalsales += Convert.ToDecimal(Supplier_Datagrid.Rows[a].Cells[3].Value);
            }

            Salestxt.Text = totalsales.ToString();

        

        }

        public void gettransactioncount()
        {
            int transactioncount = 0;
            transactioncount = Supplier_Datagrid.Rows.Count;

            TransactionCounttxt.Text = transactioncount.ToString();


        }


        public async void DataViewAll()
        {
            foreach (DataGridViewColumn column in Supplier_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            Supplier_Datagrid.Columns[6].Visible = false;


            dt.Rows.Clear();

            int i = 0;
            FirebaseResponse resp = await client.GetTaskAsync("ReStockCounter/node");
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

                    FirebaseResponse resp1 = await client.GetTaskAsync("ReStockLog/" + i);
                    Stock_class obj1 = resp1.ResultAs<Stock_class>();

                    DataRow r = dt.NewRow();
                    r["Restock ID"] = obj1.Restock_ID;
                    r["Reference Number"] = obj1.Reference_Number;
                    r["Supplier Name"] = obj1.Supplier_Name;
                    r["Transaction Payment"] = obj1.Order_Total;
                    r["Assisted By"] = obj1.Assisted_By;
                    r["Transaction Date"] = obj1.Date_Of_Transaction;

                    DateTime date = Convert.ToDateTime(obj1.Date_Of_Transaction);



                    r["Transaction Date Searcher"] = date.ToString("MM/dd/yyyy");


                    dt.Rows.Add(r);
                }

                catch
                {

                }
            }
            gettransactioncount();
            getsales();
            checker = "allow";
        }

        private void Supplier_Datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(Form1.status=="true")
            {
                string columnindex = "";

                try
                {
                    if (e.ColumnIndex == Supplier_Datagrid.Columns[7].Index)
                    {
                        columnindex = Supplier_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();




                        FirebaseResponse resp1 = client.Get("ReStockLog/" + columnindex);
                        Stock_class obj1 = resp1.ResultAs<Stock_class>();

                        ID = obj1.Restock_ID;
                        refnumber = obj1.Reference_Number;
                        items = obj1.Items;
                        sub = obj1.Sub_Total;
                        fee = obj1.Fee;
                        total = obj1.Order_Total;
                        supp = obj1.Supplier_Name;
                        assist = obj1.Assisted_By;
                        remarks = obj1.Remark;
                        date = obj1.Date_Of_Transaction;
                        amount = obj1.Amount_Tendered;
                        change = obj1.Change;



                        //clear
                        listBox1.Items.Clear();
                        textBox1.Text = "";
                        dataGridView1.Rows.Clear();




                        textBox1.Text = obj1.Items;
                        listBox1.Items.Clear();
                        listBox1.Items.AddRange(textBox1.Lines);



                        try
                        {


                            for (int i = 0; i <= listBox1.Items.Count; i += 4)
                            {
                                dataGridView1.Rows.Add(listBox1.Items[i], listBox1.Items[i + 1], listBox1.Items[i + 2], listBox1.Items[i + 3]);
                            }
                        }
                        catch
                        {

                        }



                        //clear
                        quantitylist.Clear();
                        Unitlist.Clear();
                        productnamelist.Clear();
                        Pricelist.Clear();




                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {


                            quantitylist.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
                            Unitlist.Add(dataGridView1.Rows[i].Cells[1].Value.ToString());
                            productnamelist.Add(dataGridView1.Rows[i].Cells[2].Value.ToString());
                            Pricelist.Add(dataGridView1.Rows[i].Cells[3].Value.ToString());



                        }


                        cartcount = dataGridView1.RowCount;

                        Suppliertransactionreceipt_popup c = new Suppliertransactionreceipt_popup();
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

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Dashboard_Module a = new Dashboard_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton15_Click(object sender, EventArgs e)
        {
            Accountmanagement_Module a = new Accountmanagement_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            Inventory_Module a = new Inventory_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (searchtxt.Text != "" & Form1.status == "true")
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "[" + combofilter.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

                Supplier_Datagrid.DataSource = null;
                Supplier_Datagrid.Rows.Clear();
                Supplier_Datagrid.Columns.Clear();
                Supplier_Datagrid.DataSource = dv;


                DataGridViewImageColumn View = new DataGridViewImageColumn();
                Supplier_Datagrid.Columns.Add(View);
                View.HeaderText = "";
                View.Name = "View";
                View.ImageLayout = DataGridViewImageCellLayout.Zoom;
                View.Image = Properties.Resources.view2;

                foreach (DataGridViewColumn column in Supplier_Datagrid.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                Supplier_Datagrid.Columns[6].Visible = false;

                getsales();
                gettransactioncount();

                filterlabeltxt.Text = "";
            }

            else
            {

            }
        }

        private void bunifuMetroTextbox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (searchtxt.Text == "" && supressor == 1)
            {
                supressor = 0;

                Supplier_Datagrid.DataSource = null;
                Supplier_Datagrid.Rows.Clear();
                Supplier_Datagrid.Columns.Clear();
                Supplier_Datagrid.DataSource = dt;


                DataGridViewImageColumn View = new DataGridViewImageColumn();
                Supplier_Datagrid.Columns.Add(View);
                View.HeaderText = "";
                View.Name = "View";
                View.ImageLayout = DataGridViewImageCellLayout.Zoom;
                View.Image = Properties.Resources.view2;



                DataViewAll();

                filterlabeltxt.Text = "";

            }

            if (searchtxt.Text != "")
            {
                supressor = 1;

            }
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

        private void bunifuImageButton4_Click_1(object sender, EventArgs e)
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

        private void bunifuImageButton15_Click_1(object sender, EventArgs e)
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

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
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

        private void bunifuImageButton2_Click_1(object sender, EventArgs e)
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

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
           

        

            if (Form1.status=="true")
            {
                Supplierrecord_Filter_popup a = new Supplierrecord_Filter_popup();
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
            string date1 = Supplierrecord_Filter_popup.startdate;
            string date2 = Supplierrecord_Filter_popup.enddate;
            string assist = Supplierrecord_Filter_popup.assistedby;
            string supplier = Supplierrecord_Filter_popup.supplier;

            if (Supplierrecord_Filter_popup.supplier == "" && Supplierrecord_Filter_popup.assistedby == "")
            {

                dv.RowFilter = "[Transaction Date Searcher]  >='" + date1 + "'AND [Transaction Date Searcher] <='" + date2 + "'";

                Supplier_Datagrid.DataSource = null;
                Supplier_Datagrid.Rows.Clear();
                Supplier_Datagrid.Columns.Clear();
                Supplier_Datagrid.DataSource = dv;

            }
            else if(Supplierrecord_Filter_popup.supplier == "" && Supplierrecord_Filter_popup.assistedby != "")
            {
          
                dv.RowFilter = "[Transaction Date Searcher]  >='" + date1 + "'AND [Transaction Date Searcher] <='" + date2 + "' " + " AND [Assisted By] LIKE '%" + assist + "%'";


                Supplier_Datagrid.DataSource = null;
                Supplier_Datagrid.Rows.Clear();
                Supplier_Datagrid.Columns.Clear();
                Supplier_Datagrid.DataSource = dv;
            }
            else if (Supplierrecord_Filter_popup.supplier != "" && Supplierrecord_Filter_popup.assistedby == "")
            {
                dv.RowFilter = "[Transaction Date Searcher]  >='" + date1 + "'AND [Transaction Date Searcher] <='" + date2 + "' " + " AND [Supplier Name] LIKE '%" + supplier + "%'";

                Supplier_Datagrid.DataSource = null;
                Supplier_Datagrid.Rows.Clear();
                Supplier_Datagrid.Columns.Clear();
                Supplier_Datagrid.DataSource = dv;
            }
            else
            {

                dv.RowFilter = "[Transaction Date Searcher]  >='" + date1 + "'AND [Transaction Date Searcher] <='" + date2 + "' " + " AND [Supplier Name] LIKE '%" + supplier + "%'" + " AND [Assisted By] LIKE '%" + assist + "%'";

                Supplier_Datagrid.DataSource = null;
                Supplier_Datagrid.Rows.Clear();
                Supplier_Datagrid.Columns.Clear();
                Supplier_Datagrid.DataSource = dv;
            }

            filterlabeltxt.Text = date1 + " to " + date2;

            DataGridViewImageColumn View = new DataGridViewImageColumn();
            Supplier_Datagrid.Columns.Add(View);
            View.HeaderText = "";
            View.Name = "View";
            View.ImageLayout = DataGridViewImageCellLayout.Zoom;
            View.Image = Properties.Resources.view2;

            Supplier_Datagrid.Columns[6].Visible = false;

            getsales();
            gettransactioncount();
        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
            {
                SupplierReportExtractionFilter_popup a = new SupplierReportExtractionFilter_popup();
                a.Show();
                Form1.status = "false";
            }
            else
            {
                MessageBox.Show("The Module is still loading or a window is currently open.");
            }
                
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
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
            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
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
            else if (Form1.levelac.Equals("Employee") && Form1.recordsac.Equals("Authorized") && Form1.status == "true")
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

        private void bunifuImageButton13_Click(object sender, EventArgs e)
        {
            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
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
            else if (Form1.levelac.Equals("Employee") && Form1.recordsac.Equals("Authorized") && Form1.status == "true")
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

        public void extract()
        {
            DataView dv = new DataView(dt);
            string date1 = SupplierReportExtractionFilter_popup.startdate;
            string date2 = SupplierReportExtractionFilter_popup.enddate;
            string supplier = SupplierReportExtractionFilter_popup.supplier;



            if (SupplierReportExtractionFilter_popup.supplier != "" && SupplierReportExtractionFilter_popup.supplier != "All")
            {

                dv.RowFilter = "[Transaction Date Searcher]  >='" + date1 + "'AND [Transaction Date Searcher] <='" + date2 + "' " + " AND [Supplier Name] LIKE '%" + supplier + "%'";

                Supplier_Datagrid.DataSource = null;
                Supplier_Datagrid.Rows.Clear();
                Supplier_Datagrid.Columns.Clear();
                Supplier_Datagrid.DataSource = dv;


            }

            else
            {
                dv.RowFilter = "[Transaction Date Searcher]  >='" + date1 + "'AND [Transaction Date Searcher] <='" + date2 + "' ";

                Supplier_Datagrid.DataSource = null;
                Supplier_Datagrid.Rows.Clear();
                Supplier_Datagrid.Columns.Clear();
                Supplier_Datagrid.DataSource = dv;
            }



            filterlabeltxt.Text = date1 + " to " + date2;

            DataGridViewImageColumn View = new DataGridViewImageColumn();
            Supplier_Datagrid.Columns.Add(View);
            View.HeaderText = "";
            View.Name = "View";
            View.ImageLayout = DataGridViewImageCellLayout.Zoom;
            View.Image = Properties.Resources.view2;

            getsales();
            gettransactioncount();

            grosssales2 = Salestxt.Text.ToString();
            trasacntioncount2 = TransactionCounttxt.Text.ToString();
            supplier2 = SupplierReportExtractionFilter_popup.supplier; 
            date3 = filterlabeltxt.Text;

            SupplierReportExtraction_popup a = new SupplierReportExtraction_popup();
            a.Show();

        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
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
            Supplier_Datagrid.SelectAll();
            DataObject dataObj = Supplier_Datagrid.GetClipboardContent();
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
                searchtxt.Text = "Type here to filter Stock Replenishment Record Content";
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

        private void Supplier_Datagrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
