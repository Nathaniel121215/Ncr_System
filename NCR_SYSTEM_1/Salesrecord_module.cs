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



        public Salesrecord_module()
        {
            InitializeComponent();
        }

        private void Salesrecord_module_Load(object sender, EventArgs e)
        {

            datedisplay.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            client = new FireSharp.FirebaseClient(config);

            this.Sales_Datagrid.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToAddRows = false;


            dt.Columns.Add("Purchase ID");
            dt.Columns.Add("Reference Number");
            dt.Columns.Add("Customer Name");
            dt.Columns.Add("Transaction Payment");
            dt.Columns.Add("Assisted By");
            dt.Columns.Add("Transaction Date");


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
            View.Image = Properties.Resources.view;


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


                    dt.Rows.Add(r);
                }

                catch
                {

                }
            }
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
                if (e.ColumnIndex == Sales_Datagrid.Columns[6].Index)
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


            DataGridViewButtonColumn View = new DataGridViewButtonColumn();
            Sales_Datagrid.Columns.Add(View);
            View.HeaderText = "View Detail";
            View.Text = "View Detail";
            View.Name = "view";
            View.UseColumnTextForButtonValue = true;

            foreach (DataGridViewColumn column in Sales_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
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


                DataGridViewButtonColumn View = new DataGridViewButtonColumn();
                Sales_Datagrid.Columns.Add(View);
                View.HeaderText = "View Detail";
                View.Text = "View Detail";
                View.Name = "view";
                View.UseColumnTextForButtonValue = true;



                DataViewAll();



            }

            if (searchtxt.Text != "")
            {
                supressor = 1;

            }
        }
    }
}
