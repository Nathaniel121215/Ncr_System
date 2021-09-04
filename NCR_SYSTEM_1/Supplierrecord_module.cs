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
    public partial class Supplierrecord_module : Form
    {

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


        public Supplierrecord_module()
        {
            InitializeComponent();
        }

        private void Supplierrecord_module_Load(object sender, EventArgs e)
        {
            datedisplay.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            client = new FireSharp.FirebaseClient(config);
            //sdasdas
            this.Supplier_Datagrid.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToAddRows = false;


            dt.Columns.Add("Restock ID");
            dt.Columns.Add("Reference Number");
            dt.Columns.Add("Supplier Name");
            dt.Columns.Add("Transaction Payment");
            dt.Columns.Add("Assisted By");
            dt.Columns.Add("Transaction_Date");


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
            View.Image = Properties.Resources.view;


            dataGridView1.Columns.Add("Q", "Quantity");
            dataGridView1.Columns.Add("U", "Unit");
            dataGridView1.Columns.Add("T", "Item");
            dataGridView1.Columns.Add("P", "Price");

            //dataGridView1.Columns.Add("Q", "Quantity");
            //dataGridView1.Columns.Add("U", "Unit");
            //dataGridView1.Columns.Add("T", "Item");
            //dataGridView1.Columns.Add("P", "Price");



            DataViewAll();

        }

        public async void DataViewAll()
        {
            foreach (DataGridViewColumn column in Supplier_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }



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
                    r["Transaction_Date"] = obj1.Date_Of_Transaction;


                    dt.Rows.Add(r);
                }

                catch
                {

                }
            }
        }

        private void Supplier_Datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnindex = "";

            try
            {
                if (e.ColumnIndex == Supplier_Datagrid.Columns[6].Index)
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

                }
            }
            catch 
            {
                
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
           



            DataView dv = new DataView(dt);
            dv.RowFilter = "[" + combofilter.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

            Supplier_Datagrid.DataSource = null;
            Supplier_Datagrid.Rows.Clear();
            Supplier_Datagrid.Columns.Clear();
            Supplier_Datagrid.DataSource = dv;


            DataGridViewButtonColumn View = new DataGridViewButtonColumn();
            Supplier_Datagrid.Columns.Add(View);
            View.HeaderText = "View Detail";
            View.Text = "View Detail";
            View.Name = "view";
            View.UseColumnTextForButtonValue = true;

            foreach (DataGridViewColumn column in Supplier_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
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


                DataGridViewButtonColumn View = new DataGridViewButtonColumn();
                Supplier_Datagrid.Columns.Add(View);
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
            Dashboard_Module a = new Dashboard_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton4_Click_1(object sender, EventArgs e)
        {
            Inventory_Module a = new Inventory_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton15_Click_1(object sender, EventArgs e)
        {
            Accountmanagement_Module a = new Accountmanagement_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            Suppliermanagement_module a = new Suppliermanagement_module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuImageButton2_Click_1(object sender, EventArgs e)
        {
         
            Salesrecord_module a = new Salesrecord_module();
            this.Hide();
            a.Show();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            //conns.Open();//OPEN YOUR CONNECTION
            //command = new SqlCommand("select * from tblSales WHERE Date BETWEEN @date1 and @date2", conns); // GET KEYWORDS FROM YOUR TEXTBOX
            //command.Parameters.AddWithValue("@date1", bunifuDatepicker1.Value);
            //command.Parameters.AddWithValue("@date2", bunifuDatepicker2.Value);
            //command.ExecuteNonQuery(); //EXECUTE COMMAND
            //dt = new DataTable();
            //SqlDataAdapter da = new SqlDataAdapter(command); // USE DATAADPATER TO UPDATE THE TABLE
            //da.Fill(dt); //UPDATE THE TABLE USING THE WORDS ON THE TEXTBOX
            //sale.DataSource = dt;
            //conns.Close();//CLOSE



            //DataView dv = new DataView(dt);
            //dv.RowFilter = "[" + bunifuDatepicker1.Value + "]" + "LIKE '%" + bunifuDatepicker2.Text + "%'";

            //Supplier_Datagrid.DataSource = null;
            //Supplier_Datagrid.Rows.Clear();
            //Supplier_Datagrid.Columns.Clear();
            //Supplier_Datagrid.DataSource = dv;

            DataView dv = new DataView(dt);
            //dv.RowFilter = "[" + test + "]" + "LIKE'%" + searchtxt.Text + "%'" + "AND'%" + searchtxt.Text + "%'";

            //dv.RowFilter = "[Transaction_Date]" + "LIKE'" + searchtxt.Text + "'";
            //dataView.RowFilter = "Name = 'StackOverflow' and Amount >= 5000 and Amount <= 5999";
            //dv.RowFilter = "Transaction_Date Like "+ searchtxt.Text;
            //TaskDataSet.Filter = "Deadline >='" + startSchedule + "' AND Deadline <= '" + endSchedule + "'";

            //dv.RowFilter = "[Transaction_Date]" + ">='" + searchtxt.Text + "%'" +"'AND <='" + searchtxt.Text + "'";


            string date1 = bunifuDatepicker1.Value.ToString("MM/dd/yyyy");
            string date2 = bunifuDatepicker2.Value.ToString("MM/dd/yyyy");

            dv.RowFilter = "[Transaction_Date]  >='" + date1 + "'AND [Transaction_Date] <='" + date2 + "'";

            /*   dv.RowFilter = "Transaction_Date >='" + searchtxt.Text + "' AND Transaction_Date <= '" + searchtxt.Text + "'*/

            Supplier_Datagrid.DataSource = null;
            Supplier_Datagrid.Rows.Clear();
            Supplier_Datagrid.Columns.Clear();
            Supplier_Datagrid.DataSource = dv;

            DataGridViewImageColumn View = new DataGridViewImageColumn();
            Supplier_Datagrid.Columns.Add(View);
            View.HeaderText = "";
            View.Name = "View";
            View.ImageLayout = DataGridViewImageCellLayout.Zoom;
            View.Image = Properties.Resources.view;
        }
    }
}
