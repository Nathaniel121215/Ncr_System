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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_SYSTEM_1
{
    public partial class Stockpurchasing_Module : Form
    {
        int supressor = 1;


        string fmt = "##.00";
        DataTable dt = new DataTable();

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        private Image[] StatusImgs;
        string id = "";
        string productname = "";
        string unit = "";
        string brand = "";
        string productdescription = "";
        string category = "";
      


        public static int itemCount;
        public static string[] listitems;

        public static decimal total = 0;


        public static string details;

        public static Stockpurchasing_Module _instance;

        public Stockpurchasing_Module()
        {
            InitializeComponent();
            _instance = this;
        }

        private void Stockpurchasing_Module_Load(object sender, EventArgs e)
        {
             


            datedisplay.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");






            this.Inventory_datagrid_stocks.AllowUserToAddRows = false;
            this.carto_datagrid_stocks.AllowUserToAddRows = false;
            client = new FireSharp.FirebaseClient(config);

            dt.Columns.Add("Product ID");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Unit");
            dt.Columns.Add("Brand");
            dt.Columns.Add("Product Description");
            dt.Columns.Add("Category");
            dt.Columns.Add("Price");
            dt.Columns.Add("Items Sold");
            dt.Columns.Add("Stock");

            dt.Columns.Add("Low");
            dt.Columns.Add("High");

            Inventory_datagrid_stocks.DataSource = dt;



            

            DataGridViewButtonColumn Add = new DataGridViewButtonColumn();
            Inventory_datagrid_stocks.Columns.Add(Add);
            Add.HeaderText = "Add";
            Add.Text = "Add";
            Add.Name = "add";
            Add.UseColumnTextForButtonValue = true;


            DataGridViewImageColumn Indicator = new DataGridViewImageColumn();
            Inventory_datagrid_stocks.Columns.Add(Indicator);
            Indicator.HeaderText = "Indicator";
            Indicator.Name = "Indicator";
            Indicator.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Indicator.Image = Properties.Resources.loading;


          


            carto_datagrid_stocks.Columns.Add("id", "Product ID");
            carto_datagrid_stocks.Columns.Add("pn", "Product Name");
            carto_datagrid_stocks.Columns.Add("unit", "Unit");
            carto_datagrid_stocks.Columns.Add("brnd", "Brand");
            carto_datagrid_stocks.Columns.Add("prdctd", "Product Description");
            carto_datagrid_stocks.Columns.Add("categ", "Category");
            carto_datagrid_stocks.Columns.Add("quant", "Quantity");
            carto_datagrid_stocks.Columns.Add("pr", "Price");
            carto_datagrid_stocks.Columns.Add("lintotal", "Line Total");

            DataGridViewButtonColumn Delete = new DataGridViewButtonColumn();
            carto_datagrid_stocks.Columns.Add(Delete);
            Delete.HeaderText = "Delete";
            Delete.Text = "Delete";
            Delete.Name = "delete";
            Delete.UseColumnTextForButtonValue = true;

            carto_datagrid_stocks.Columns["id"].ReadOnly = true;
            carto_datagrid_stocks.Columns["pn"].ReadOnly = true;
            carto_datagrid_stocks.Columns["unit"].ReadOnly = true;
            carto_datagrid_stocks.Columns["brnd"].ReadOnly = true;
            carto_datagrid_stocks.Columns["prdctd"].ReadOnly = true;
            carto_datagrid_stocks.Columns["categ"].ReadOnly = true;
            carto_datagrid_stocks.Columns["lintotal"].ReadOnly = true;

            DataViewAll();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            search();
            detail.Text = "";
            List<string> productnamelist = new List<string>();
            List<string> quantitylist = new List<string>();
            List<string> Unitlist = new List<string>();
            List<string> Linetotallist = new List<string>();

            for (int i = 0; i < carto_datagrid_stocks.RowCount; i++)
            {



                productnamelist.Add(carto_datagrid_stocks.Rows[i].Cells[1].Value.ToString());
                quantitylist.Add(carto_datagrid_stocks.Rows[i].Cells[6].Value.ToString());
                Unitlist.Add(carto_datagrid_stocks.Rows[i].Cells[2].Value.ToString());
                Linetotallist.Add(carto_datagrid_stocks.Rows[i].Cells[8].Value.ToString());

                detail.Text += productnamelist[i] + "  " + quantitylist[i] + "  " + "  " + Unitlist[i] + "  " + "  " + Linetotallist[i] + "\n";
            }


            


            itemCount = productnamelist.Count;

            Item[] concat = new Item[productnamelist.Count];
            int index = 0;
            for (int i = 0; i < productnamelist.Count; i++)
            {
                concat[index++] = new Item
                {
                    Name = productnamelist[i],
                    Unit = Unitlist[i],
                    Quantity = quantitylist[i],
                    Price = Linetotallist[i]
                };
            }
       

            foreach (Item item in concat)
            {
                listBox.Items.Add(item.ToString());
               
            }

            listitems = new string[listBox.Items.Count];

            for (int i = 0; i < itemCount; i++)
            {
                listitems[i] = listBox.Items[i].ToString();
            }



            total = Convert.ToDecimal(Regex.Replace(Sum.Text, "[^0-9.]", ""));
            details = detail.Text;



            Addstock_popup f = new Addstock_popup();
            f.Show();
        }

        private void Inventory_datagrid_stocks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        public async void DataViewAll()
        {
            
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
                    r["Unit"] = obj1.Unit;
                    r["Brand"] = obj1.Brand;
                    r["Product Description"] = obj1.Description;
                    r["Category"] = obj1.Category;
                    r["Price"] = obj1.Price;
                    r["Items Sold"] = obj1.Items_Sold;
                    r["Stock"] = obj1.Stock;
                    r["Low"] = obj1.Low;
                    r["High"] = obj1.High;

                    dt.Rows.Add(r);
                }

                catch
                {

                }
            }

            Inventory_datagrid_stocks.Columns[9].Visible = false;
            Inventory_datagrid_stocks.Columns[10].Visible = false;



            for (int row = 0; row <= Inventory_datagrid_stocks.Rows.Count - 1; row++)
            {
                try
                {
                    Inventory_datagrid_stocks.Rows[row].Cells[12].Style.BackColor = Color.White;

                    //if (Convert.ToInt32(Inventory_Datagrid.Rows[row].Cells[8].Value) < Convert.ToInt32(Inventory_Datagrid.Rows[row].Cells[9].Value))
                    //{
                    //    ((DataGridViewImageCell)Inventory_Datagrid.Rows[row].Cells[13]).Value = Properties.Resources.dashboard;
                    //}
                    //else
                    //{

                    //}
                }

                catch
                {

                }

            }

            

                //Assembly myAssembly = Assembly.GetExecutingAssembly();
                //Stream myStream = myAssembly.GetManifestResourceStream("NCR_SYSTEM_1.Resources.ave.png");
                //pictureBox2.Image = new Bitmap(myStream);



                foreach (DataGridViewRow row in Inventory_datagrid_stocks.Rows)
                {

                    try
                    {
                        

                        StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.new_low_on_stock, NCR_SYSTEM_1.Properties.Resources.new_in_stock, NCR_SYSTEM_1.Properties.Resources.new_high_on_stock, NCR_SYSTEM_1.Properties.Resources.new_out_of_stock };

                        //pictureBox2.Image = Properties.Resources.dashboard;




                        //Bitmap low;
                        //low = new Bitmap(@"C:\Users\nathaniel\Desktop\Group 121.png");

                        //Bitmap high;
                        //high = new Bitmap(Properties.Resources.ave);

                        //Bitmap average;
                        //average = new Bitmap(@"C:\Users\nathaniel\Desktop\average.png");



                        if (Convert.ToInt32(row.Cells[8].Value) <= Convert.ToInt32(row.Cells[9].Value))
                        {

                            //row.Cells[13].Value = Properties.Resources.dashboard;

                            row.Cells[12].Value = StatusImgs[0];

                            //row.Cells[8].Value = string.Format("<img src='images/close_icon.png' />");
                            //row.DefaultCellStyle.BackColor = Color.FromArgb(171, 25, 28);
                        }


                        else if (Convert.ToInt32(row.Cells[8].Value) > Convert.ToInt32(row.Cells[9].Value) && Convert.ToInt32(row.Cells[8].Value) < Convert.ToInt32(row.Cells[10].Value))
                        {
                            row.Cells[12].Value = StatusImgs[1];
                        }

                        else if (Convert.ToInt32(row.Cells[8].Value) > Convert.ToInt32(row.Cells[10].Value))
                        {
                            row.Cells[12].Value = StatusImgs[2];
                        }

                        if (Convert.ToInt32(row.Cells[8].Value).Equals(0))
                        {
                            row.Cells[12].Value = StatusImgs[3];
                        }


                    }
                    catch
                    {

                    }

               

                }
            

        }

        private void Inventory_datagrid_stocks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == Inventory_datagrid_stocks.Columns[11].Index)
                {
                    Inventory_datagrid_stocks.Rows[e.RowIndex].Selected = true;


                    id = Inventory_datagrid_stocks.Rows[e.RowIndex].Cells[0].Value.ToString();
                    productname = Inventory_datagrid_stocks.Rows[e.RowIndex].Cells[1].Value.ToString();
                    unit = Inventory_datagrid_stocks.Rows[e.RowIndex].Cells[2].Value.ToString();
                    brand = Inventory_datagrid_stocks.Rows[e.RowIndex].Cells[3].Value.ToString();
                    productdescription = Inventory_datagrid_stocks.Rows[e.RowIndex].Cells[4].Value.ToString();
                    category = Inventory_datagrid_stocks.Rows[e.RowIndex].Cells[5].Value.ToString();

                    string zero = "0.00";



                    carto_datagrid_stocks.Rows.Add(id, productname, unit, brand, productdescription, category, 0, zero, zero);

                    List<string> list = new List<string>();
                    for (int i = 0; i < carto_datagrid_stocks.Rows.Count; i++)
                    {
                        string str = carto_datagrid_stocks.Rows[i].Cells[0].Value.ToString();
                        if (!list.Contains(str))
                            list.Add(carto_datagrid_stocks.Rows[i].Cells[0].Value.ToString());
                        else
                        {
                            carto_datagrid_stocks.Rows.Remove(carto_datagrid_stocks.Rows[i]);
                            MessageBox.Show("You already have this item");
                        }

                    }
                }
            }
            catch
            {

            }
        }

        private void carto_datagrid_stocks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
            if (e.ColumnIndex == carto_datagrid_stocks.Columns[9].Index)
            {
                this.carto_datagrid_stocks.Rows.RemoveAt(e.RowIndex);

                try
                {
                    decimal subtotal = 0;


                    for (int i = 0; i < carto_datagrid_stocks.Rows.Count; i++)
                    {
                        subtotal += Convert.ToDecimal(carto_datagrid_stocks.Rows[i].Cells[8].Value);
                    }

                    Sum.Text = subtotal.ToString(fmt) + " Peso";
                }

                catch
                {

                }
            }
        }

        private void carto_datagrid_stocks_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal uprice = 0;
            int qty = 0;
            decimal product = 0;


            uprice = Convert.ToDecimal(carto_datagrid_stocks.Rows[e.RowIndex].Cells[7].Value);
            qty = Convert.ToInt32(carto_datagrid_stocks.Rows[e.RowIndex].Cells[6].Value);
            product = uprice * qty;
            

            carto_datagrid_stocks.Rows[e.RowIndex].Cells[8].Value = product.ToString(fmt);

            try
            {
                decimal subtotal = 0;


                for (int i = 0; i < carto_datagrid_stocks.Rows.Count; i++)
                {
                    subtotal += Convert.ToDecimal(carto_datagrid_stocks.Rows[i].Cells[8].Value);
                }

                Sum.Text = subtotal.ToString(fmt) + " Peso";
            }

            catch
            {

            }
        }

        public void addstock()
        {
            string[] ids = new string[100];

            int z = 0;

            int[] rowindexI = new int[100];
            int[] rowindexC = new int[100];

            for (z = 0; z < carto_datagrid_stocks.Rows.Count; z++)
            {
                ids[z] = carto_datagrid_stocks.Rows[z].Cells[0].Value.ToString();

                foreach (DataGridViewRow row in Inventory_datagrid_stocks.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(ids[z]))
                    {
                        rowindexI[z] = row.Index;

                    }
                }

                foreach (DataGridViewRow row in carto_datagrid_stocks.Rows)
                {

                    if (row.Cells[0].Value.ToString().Equals(ids[z]))
                    {
                        rowindexC[z] = row.Index;
                    }

                }
                try
                {
                    var datas = new SDU
                    {
                        ID = carto_datagrid_stocks.Rows[z].Cells[0].Value.ToString(),
                        //Product_Name = carto_datagrid_stocks.Rows[z].Cells[1].Value.ToString(),
                        Stock = Convert.ToInt32(Inventory_datagrid_stocks.Rows[rowindexI[z]].Cells[8].Value) + Convert.ToInt32(carto_datagrid_stocks.Rows[rowindexC[z]].Cells[6].Value),
                    };

                    FirebaseResponse resps = client.Update("Inventory/" + ids[z], datas);
                    Product_class res = resps.ResultAs<Product_class>();
                }
                catch (Exception c) { MessageBox.Show(c.ToString()); }


            }

            carto_datagrid_stocks.Rows.Clear();
            Sum.Text = "0.00 Peso";
            DataViewAll();

        }

        private void bunifuImageButton14_Click(object sender, EventArgs e)
        {
           Inventory_Module a = new Inventory_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Dashboard_Module a = new Dashboard_Module();
            this.Hide();
            a.Show();
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

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            DataViewAll();



            DataView dv = new DataView(dt);
            dv.RowFilter = "[" + combofilter.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

            Inventory_datagrid_stocks.DataSource = null;
            Inventory_datagrid_stocks.Rows.Clear();
            Inventory_datagrid_stocks.Columns.Clear();
            Inventory_datagrid_stocks.DataSource = dv;


            DataGridViewButtonColumn Add = new DataGridViewButtonColumn();
            Inventory_datagrid_stocks.Columns.Add(Add);
            Add.HeaderText = "Add";
            Add.Text = "Add";
            Add.Name = "add";
            Add.UseColumnTextForButtonValue = true;
        }

        public void search()
        {
            searchtxt.Text = "";
            supressor = 0;

            Inventory_datagrid_stocks.DataSource = null;
            Inventory_datagrid_stocks.Rows.Clear();
            Inventory_datagrid_stocks.Columns.Clear();
            Inventory_datagrid_stocks.DataSource = dt;


            DataGridViewButtonColumn Add = new DataGridViewButtonColumn();
            Inventory_datagrid_stocks.Columns.Add(Add);
            Add.HeaderText = "Add";
            Add.Text = "Add";
            Add.Name = "add";
            Add.UseColumnTextForButtonValue = true;

            DataViewAll();
        }

        private void searchtxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (searchtxt.Text == "" && supressor == 1)
            {
                supressor = 0;

                Inventory_datagrid_stocks.DataSource = null;
                Inventory_datagrid_stocks.Rows.Clear();
                Inventory_datagrid_stocks.Columns.Clear();
                Inventory_datagrid_stocks.DataSource = dt;


                DataGridViewButtonColumn Add = new DataGridViewButtonColumn();
                Inventory_datagrid_stocks.Columns.Add(Add);
                Add.HeaderText = "Add";
                Add.Text = "Add";
                Add.Name = "add";
                Add.UseColumnTextForButtonValue = true;



                DataViewAll();

           


            }

            if (searchtxt.Text != "")
            {
                supressor = 1;

            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton13_Click(object sender, EventArgs e)
        {
            Addunit_module a = new Addunit_module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton11_Click(object sender, EventArgs e)
        {
            Addcategory_module a = new Addcategory_module();
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

        private void bunifuImageButton6_Click_1(object sender, EventArgs e)
        {
            Supplierrecord_module a = new Supplierrecord_module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton9_Click_1(object sender, EventArgs e)
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

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

class Item
{
    public string Name { get; set; }
    public string Unit { get; set; }
    public string Quantity { get; set; }
    public string Price { get; set; }

    override public string ToString()
    {
        return $"{Name + " "} {Quantity + "x "} {Unit + " "} {Price + " Peso"}";
    }
}

