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
    public partial class StockAdjustment_Module : Form
    {

        DataTable dt = new DataTable();

        int supressor = 1;

        private Image[] StatusImgs;

        public static string id = "";
        public static string name = "";
        public static string brand = "";
        public static string description = "";
        public static string category = "";
        public static decimal price = 0;
        public static int stock = 0;
        

        

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public static StockAdjustment_Module _instance;


        public StockAdjustment_Module()
        {
            InitializeComponent();
            _instance = this;
        }

        private void StockAdjustment_Module_Load_1(object sender, EventArgs e)
        {
            datedisplay.Text = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            datedisplay.Select();

            client = new FireSharp.FirebaseClient(config);
            this.StockAdjustment_Datagrid.AllowUserToAddRows = false;

            dt.Columns.Add("Product ID");
            dt.Columns.Add("Product Name"); 
            dt.Columns.Add("Brand");
            dt.Columns.Add("Product Description");
            dt.Columns.Add("Category");
            dt.Columns.Add("Price");
            dt.Columns.Add("Stock");
            

            StockAdjustment_Datagrid.DataSource = dt;


            DataGridViewImageColumn deduct = new DataGridViewImageColumn();
            StockAdjustment_Datagrid.Columns.Add(deduct);
            deduct.HeaderText = "";
            deduct.Name = "";
            deduct.ImageLayout = DataGridViewImageCellLayout.Zoom;
            deduct.Image = Properties.Resources.minus;




            DataViewAll();
        }

        public async void DataViewAll()
        {
            foreach (DataGridViewColumn column in StockAdjustment_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            DataGridViewColumn column3 = StockAdjustment_Datagrid.Columns[3];
            column3.Width = 260;


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
                    r["Brand"] = obj1.Brand;
                    r["Product Description"] = obj1.Description;
                    r["Category"] = obj1.Category;
                    r["Price"] = obj1.Price;
                    r["Stock"] = obj1.Stock;
                 




                    dt.Rows.Add(r);



                }

                catch
                {

                }
            }


        }

        private void StockAdjustment_Datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnindex = "";

            try
            {


                //update
                if (e.ColumnIndex == StockAdjustment_Datagrid.Columns[7].Index)
                {
                    columnindex = StockAdjustment_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();


                    StockAdjustment_Datagrid.Rows[e.RowIndex].Selected = true;

                    id = StockAdjustment_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                    name = StockAdjustment_Datagrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                    brand = StockAdjustment_Datagrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                    description = StockAdjustment_Datagrid.Rows[e.RowIndex].Cells[3].Value.ToString();
                    category = StockAdjustment_Datagrid.Rows[e.RowIndex].Cells[4].Value.ToString();
                    price = Convert.ToDecimal(StockAdjustment_Datagrid.Rows[e.RowIndex].Cells[5].Value);
                    stock = Convert.ToInt32(StockAdjustment_Datagrid.Rows[e.RowIndex].Cells[6].Value);

           

                    Deductstock_popup c = new Deductstock_popup();
                    c.Show();

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

                StockAdjustment_Datagrid.DataSource = null;
                StockAdjustment_Datagrid.Rows.Clear();
                StockAdjustment_Datagrid.Columns.Clear();
                StockAdjustment_Datagrid.DataSource = dt;




                DataGridViewImageColumn deduct = new DataGridViewImageColumn();
                StockAdjustment_Datagrid.Columns.Add(deduct);
                deduct.HeaderText = "";
                deduct.Name = "";
                deduct.ImageLayout = DataGridViewImageCellLayout.Zoom;
                deduct.Image = Properties.Resources.minus;

                DataViewAll();



            }

            if (searchtxt.Text != "")
            {
                supressor = 1;

            }
        }

        private void searchbutton_Click(object sender, EventArgs e)
        {

            DataView dv = new DataView(dt);
            dv.RowFilter = "[" + bunifuDropdown1.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

            StockAdjustment_Datagrid.DataSource = null;
            StockAdjustment_Datagrid.Rows.Clear();
            StockAdjustment_Datagrid.Columns.Clear();
            StockAdjustment_Datagrid.DataSource = dv;

            DataGridViewImageColumn deduct = new DataGridViewImageColumn();
            StockAdjustment_Datagrid.Columns.Add(deduct);
            deduct.HeaderText = "";
            deduct.Name = "";
            deduct.ImageLayout = DataGridViewImageCellLayout.Zoom;
            deduct.Image = Properties.Resources.minus;

            foreach (DataGridViewColumn column in StockAdjustment_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            DataGridViewColumn column3 = StockAdjustment_Datagrid.Columns[3];
            column3.Width = 260;
        }
    }
}
