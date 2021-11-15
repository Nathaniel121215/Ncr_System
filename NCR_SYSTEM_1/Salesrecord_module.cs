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
    public partial class Salesrecord_module : Form
    {
        public static string checker = "";

        private Image[] StatusImgs;

        int supressor = 1;

        public static string ID;
        public static string refnumber;
        public static string date;
        public static string items;
        public static string sub;
        public static string fee;
        public static string total;
        public static string customer;
        public static string assist;
        public static string remarks;
        public static string amount;
        public static string change;
        public static string address;

        public static int cartcount;

        public static List<string> productnamelist = new List<string>();
        public static List<string> quantitylist = new List<string>();
        public static List<string> Unitlist = new List<string>();
        public static List<string> Pricelist = new List<string>();



        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        DataTable dt = new DataTable();


        public static Salesrecord_module _instance;

        public Salesrecord_module()
        {
            InitializeComponent();
            _instance = this;
        }

        private void Salesrecord_module_Load(object sender, EventArgs e)
        {

            datedisplay.Text = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            datedisplay.Select();

            client = new FireSharp.FirebaseClient(config);

            this.Sales_Datagrid.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToAddRows = false;


            dt.Columns.Add("Purchase ID");
            dt.Columns.Add("Reference Number");
            dt.Columns.Add("Customer Name");
            dt.Columns.Add("Transaction Payment");
            dt.Columns.Add("Assisted By");
            dt.Columns.Add("Transaction Type txt");
            dt.Columns.Add("Transaction Date");

            dt.Columns.Add("Transaction Date Searcher");




            Sales_Datagrid.DataSource = dt;

            //DataGridViewButtonColumn View = new DataGridViewButtonColumn();
            //Supplier_Datagrid.Columns.Add(View);
            //View.HeaderText = "View Detail";
            //View.Text = "View Detail";
            //View.Name = "view";
            //View.UseColumnTextForButtonValue = true;

            DataGridViewImageColumn View = new DataGridViewImageColumn();
            Sales_Datagrid.Columns.Add(View);
            View.HeaderText = "";
            View.Name = "View";
            View.ImageLayout = DataGridViewImageCellLayout.Zoom;
            View.Image = Properties.Resources.view2;

            // Transaction type

            DataGridViewImageColumn TransactionType = new DataGridViewImageColumn();
            Sales_Datagrid.Columns.Add(TransactionType);
            TransactionType.HeaderText = "Transaction Type";
            TransactionType.Name = "Transaction Type";
            TransactionType.ImageLayout = DataGridViewImageCellLayout.Zoom;
            TransactionType.Image = Properties.Resources.loading;


            dataGridView1.Columns.Add("Q", "Quantity");
            dataGridView1.Columns.Add("U", "Unit");
            dataGridView1.Columns.Add("T", "Item");
            dataGridView1.Columns.Add("P", "Price");


            DataViewAll();

        }

        public async void DataViewAll()
        {
            foreach (DataGridViewColumn column in Sales_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            Sales_Datagrid.Columns[9].DisplayIndex = 5;
            Sales_Datagrid.Columns[5].Visible = false;
            Sales_Datagrid.Columns[7].Visible = false;



            dt.Rows.Clear();

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
                    r["Reference Number"] = obj1.Reference_Number;
                    r["Customer Name"] = obj1.Customer_Name;
                    r["Transaction Payment"] = obj1.Order_Total;
                    r["Assisted By"] = obj1.Assisted_By;
                    r["Transaction Date"] = obj1.Date_Of_Transaction;
                    r["Transaction Type txt"] = obj1.Transaction_Type;

                    DateTime date = Convert.ToDateTime(obj1.Date_Of_Transaction);



                    r["Transaction Date Searcher"] = date.ToString("MM/dd/yyyy");

                    dt.Rows.Add(r);
                }

                catch
                {

                }
            }

           
                foreach (DataGridViewRow row in Sales_Datagrid.Rows)
                {

                    try
                    {


                        StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.onsite_icon, NCR_SYSTEM_1.Properties.Resources.delivery_icon };



                        if (row.Cells[5].Value.Equals("On-site")) 
                        {
                            row.Cells[9].Value = StatusImgs[0];
           
                        }

                        if (row.Cells[5].Value.Equals("Delivery"))
                        {
                            row.Cells[9].Value = StatusImgs[1];
                            
                        }



                    }
                    catch
                    {

                    }

           
            }

            getsales();
            gettransactioncount();
            checker = "allow";
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Salesrecord_module a = new Salesrecord_module();
            this.Hide();
            a.Show();
        }

        private void Sales_Datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnindex = "";

            try
            {
                if (e.ColumnIndex == Sales_Datagrid.Columns[8].Index)
                {
                    columnindex = Sales_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();




                    FirebaseResponse resp1 = client.Get("CompanySales/" + columnindex);
                    Purchase_Class obj1 = resp1.ResultAs<Purchase_Class>();

                    ID = obj1.Purchase_ID;
                    refnumber = obj1.Reference_Number;
                    items = obj1.Items;
                    sub = obj1.Sub_Total;
                    fee = obj1.Fee;
                    total = obj1.Order_Total;
                    customer = obj1.Customer_Name;
                    assist = obj1.Assisted_By;
                    remarks = obj1.Remark;
                    date = obj1.Date_Of_Transaction;
                    amount = obj1.Amount_Tendered;
                    change = obj1.Change;
                    address = obj1.Customer_Address;


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

                    Customertransactionreceipt_popup c = new Customertransactionreceipt_popup();
                    c.Show();

                }
            }
            catch
            {

            }
        }

        private void a_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = "[" + combofilter.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

            Sales_Datagrid.DataSource = null;
            Sales_Datagrid.Rows.Clear();
            Sales_Datagrid.Columns.Clear();
            Sales_Datagrid.DataSource = dv;


            DataGridViewImageColumn View = new DataGridViewImageColumn();
            Sales_Datagrid.Columns.Add(View);
            View.HeaderText = "";
            View.Name = "View";
            View.ImageLayout = DataGridViewImageCellLayout.Zoom;
            View.Image = Properties.Resources.view2;

            DataGridViewImageColumn TransactionType = new DataGridViewImageColumn();
            Sales_Datagrid.Columns.Add(TransactionType);
            TransactionType.HeaderText = "Transaction Type";
            TransactionType.Name = "Transaction Type";
            TransactionType.ImageLayout = DataGridViewImageCellLayout.Zoom;
            TransactionType.Image = Properties.Resources.loading;



            getsales();
            gettransactioncount();

            filterlabeltxt.Text = "";

            searchupdate();

        }

        public void searchupdate()
        {
            foreach (DataGridViewColumn column in Sales_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            Sales_Datagrid.Columns[9].DisplayIndex = 5;
            Sales_Datagrid.Columns[5].Visible = false;
            Sales_Datagrid.Columns[7].Visible = false;


            foreach (DataGridViewRow row in Sales_Datagrid.Rows)
            {

                try
                {


                    StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.onsite_icon, NCR_SYSTEM_1.Properties.Resources.delivery_icon };



                    if (row.Cells[5].Value.Equals("On-site"))
                    {
                        row.Cells[9].Value = StatusImgs[0];

                    }

                    if (row.Cells[5].Value.Equals("Delivery"))
                    {
                        row.Cells[9].Value = StatusImgs[1];

                    }



                }
                catch
                {

                }


            }

        }

        private void searchtxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (searchtxt.Text == "" && supressor == 1)
            {
                supressor = 0;

                Sales_Datagrid.DataSource = null;
                Sales_Datagrid.Rows.Clear();
                Sales_Datagrid.Columns.Clear();
                Sales_Datagrid.DataSource = dt;


                DataGridViewImageColumn View = new DataGridViewImageColumn();
                Sales_Datagrid.Columns.Add(View);
                View.HeaderText = "";
                View.Name = "View";
                View.ImageLayout = DataGridViewImageCellLayout.Zoom;
                View.Image = Properties.Resources.view2;

                DataGridViewImageColumn TransactionType = new DataGridViewImageColumn();
                Sales_Datagrid.Columns.Add(TransactionType);
                TransactionType.HeaderText = "Transaction Type";
                TransactionType.Name = "Transaction Type";
                TransactionType.ImageLayout = DataGridViewImageCellLayout.Zoom;
                TransactionType.Image = Properties.Resources.loading;



                DataViewAll();


                filterlabeltxt.Text = "";
            }

            if (searchtxt.Text != "")
            {
                supressor = 1;

            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (checker.Equals("allow"))
            {
                Salesrecord_Filter_popup a = new Salesrecord_Filter_popup();
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
            string date1 = Salesrecord_Filter_popup.startdate;
            string date2 = Salesrecord_Filter_popup.enddate;
            string assist = Salesrecord_Filter_popup.assistedby;
            string transactiontype = Salesrecord_Filter_popup.transactiontype;

            if (Salesrecord_Filter_popup.transactiontype == "" && Salesrecord_Filter_popup.assistedby == "")
            {

                dv.RowFilter = "[Transaction Date Searcher]  >='" + date1 + "'AND [Transaction Date Searcher] <='" + date2 + "'";

                Sales_Datagrid.DataSource = null;
                Sales_Datagrid.Rows.Clear();
                Sales_Datagrid.Columns.Clear();
                Sales_Datagrid.DataSource = dv;


            }

            else if (Salesrecord_Filter_popup.transactiontype == "" && Salesrecord_Filter_popup.assistedby != "")
            {

                dv.RowFilter = "[Transaction Date Searcher]  >='" + date1 + "'AND [Transaction Date Searcher] <='" + date2 + "' " + " AND [Assisted By] LIKE '%" + assist + "%'";


                Sales_Datagrid.DataSource = null;
                Sales_Datagrid.Rows.Clear();
                Sales_Datagrid.Columns.Clear();
                Sales_Datagrid.DataSource = dv;
            }
            else if (Salesrecord_Filter_popup.transactiontype != "" && Salesrecord_Filter_popup.assistedby == "")
            {
                dv.RowFilter = "[Transaction Date Searcher]  >='" + date1 + "'AND [Transaction Date Searcher] <='" + date2 + "' " + " AND [Transaction Type txt] LIKE '%" + transactiontype + "%'";

                Sales_Datagrid.DataSource = null;
                Sales_Datagrid.Rows.Clear();
                Sales_Datagrid.Columns.Clear();
                Sales_Datagrid.DataSource = dv;
            }
            else
            {

                dv.RowFilter = "[Transaction Date Searcher]  >='" + date1 + "'AND [Transaction Date Searcher] <='" + date2 + "' " + " AND [Transaction Type txt] LIKE '%" + transactiontype + "%'" + " AND [Assisted By] LIKE '%" + assist + "%'";

                Sales_Datagrid.DataSource = null;
                Sales_Datagrid.Rows.Clear();
                Sales_Datagrid.Columns.Clear();
                Sales_Datagrid.DataSource = dv;
            }



            filterlabeltxt.Text = date1 + " to " + date2;

            DataGridViewImageColumn View = new DataGridViewImageColumn();
            Sales_Datagrid.Columns.Add(View);
            View.HeaderText = "";
            View.Name = "View";
            View.ImageLayout = DataGridViewImageCellLayout.Zoom;
            View.Image = Properties.Resources.view2;

            DataGridViewImageColumn TransactionType = new DataGridViewImageColumn();
            Sales_Datagrid.Columns.Add(TransactionType);
            TransactionType.HeaderText = "Transaction Type";
            TransactionType.Name = "Transaction Type";
            TransactionType.ImageLayout = DataGridViewImageCellLayout.Zoom;
            TransactionType.Image = Properties.Resources.loading;

            getsales();
            gettransactioncount();
            searchupdate();

        }

        public void getsales()
        {
            decimal totalsales = 0;


            for (int a = 0; a < Sales_Datagrid.Rows.Count; a++)
            {
                totalsales += Convert.ToDecimal(Sales_Datagrid.Rows[a].Cells[3].Value);
            }

            Salestxt.Text = totalsales.ToString();



        }

        public void gettransactioncount()
        {
            int transactioncount = 0;
            transactioncount = Sales_Datagrid.Rows.Count;

            TransactionCounttxt.Text = transactioncount.ToString();


        }

        private void bunifuImageButton10_Click(object sender, EventArgs e)
        {
            Supplierrecord_module a = new Supplierrecord_module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {
            Dashboard_Module a = new Dashboard_Module();
            this.Hide();
            a.Show();
        }
    }
}
