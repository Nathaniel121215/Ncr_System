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
    public partial class stockpurchase_Module : Form
    {
        //
        int supressor = 1;

        string id = "";
        string productname = "";
        string unit = "";

      

        string fmt = "##.00";

        public static decimal fee;
        public static decimal subtotal;
        public static decimal total;
        public static decimal payment;
        public static decimal change;


        public static List<string> productnamelist = new List<string>();
        public static List<string> quantitylist = new List<string>();
        public static List<string> Unitlist = new List<string>();
        public static List<string> Pricelist = new List<string>();
        public static List<string> Linetotallist = new List<string>();

        public static string details;
        public static string details2;

        public static int cartcount;

        //tables
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        //image
        private Image[] StatusImgs;

        //Firebase Connection

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public static stockpurchase_Module _instance;

        public stockpurchase_Module()
        {
            _instance = this;
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            //open connection
            client = new FireSharp.FirebaseClient(config);






            //Inventory datagridview add columns

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

            Inventory_Datagridview.DataSource = dt;


            DataGridViewImageColumn Add = new DataGridViewImageColumn();
            Inventory_Datagridview.Columns.Add(Add);
            Add.HeaderText = "";
            Add.Name = "";
            Add.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Add.Image = Properties.Resources.Add_Icon_2;


            DataGridViewImageColumn Indicator = new DataGridViewImageColumn();
            Inventory_Datagridview.Columns.Add(Indicator);
            Indicator.HeaderText = "Indicator";
            Indicator.Name = "Indicator";
            Indicator.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Indicator.Image = Properties.Resources.loading;





            //Cart datagridview add columns

            Cart_Datagridview.Columns.Add("id", "Product ID");
            Cart_Datagridview.Columns.Add("pn", "Product Name");
            Cart_Datagridview.Columns.Add("quant", "Quantity");
            Cart_Datagridview.Columns.Add("unit", "Unit");
            Cart_Datagridview.Columns.Add("pr", "Price");
            Cart_Datagridview.Columns.Add("lintotal", "Total");

            DataGridViewImageColumn Remove = new DataGridViewImageColumn();
            Cart_Datagridview.Columns.Add(Remove);
            Remove.HeaderText = "";
            Remove.Name = "";
            Remove.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Remove.Image = Properties.Resources.Remove_Icon;

            Cart_Datagridview.Columns["id"].ReadOnly = true;
            Cart_Datagridview.Columns["pn"].ReadOnly = true;
            Cart_Datagridview.Columns["unit"].ReadOnly = true;
            Cart_Datagridview.Columns["lintotal"].ReadOnly = true;




            DataViewAll();



            




        }

        public async void DataViewAll()
        {

            foreach (DataGridViewColumn column in Inventory_Datagridview.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //Adjusting Visual settings of Inventory Datagridview
            this.Inventory_Datagridview.AllowUserToAddRows = false;

            Inventory_Datagridview.Columns[5].Visible = false;
            Inventory_Datagridview.Columns[0].Visible = false;
            Inventory_Datagridview.Columns[7].Visible = false;
            Inventory_Datagridview.Columns[9].Visible = false;
            Inventory_Datagridview.Columns[10].Visible = false;







            Inventory_Datagridview.Columns[12].DisplayIndex = 7;

            DataGridViewColumn column1 = Inventory_Datagridview.Columns[1];
            column1.Width = 220;

            DataGridViewColumn column2 = Inventory_Datagridview.Columns[2];
            column2.Width = 80;

            DataGridViewColumn column3 = Inventory_Datagridview.Columns[3];
            column3.Width = 100;

            DataGridViewColumn column4 = Inventory_Datagridview.Columns[4];
            column4.Width = 220;

            DataGridViewColumn column6 = Inventory_Datagridview.Columns[6];
            column6.Width = 80;

            DataGridViewColumn column8 = Inventory_Datagridview.Columns[8];
            column8.Width = 60;

            DataGridViewColumn column12 = Inventory_Datagridview.Columns[12];
            column12.Width = 100;



            Inventory_Datagridview.Columns["Product Name"].HeaderCell.Style.Padding = new Padding(20, 0, 0, 0);
            Inventory_Datagridview.Columns[1].DefaultCellStyle.Padding = new Padding(20, 0, 0, 0);




            //Adjusting Visual settings of Cart Datagridview
            this.Cart_Datagridview.AllowUserToAddRows = false;

            Cart_Datagridview.Columns[0].Visible = false;

            DataGridViewColumn cartcolumn1 = Cart_Datagridview.Columns[1];
            cartcolumn1.Width = 180;

            Cart_Datagridview.Columns[1].DefaultCellStyle.Padding = new Padding(20, 0, 0, 0);
            Cart_Datagridview.Columns["pn"].HeaderCell.Style.Padding = new Padding(20, 0, 0, 0);


            //Load Inventory Datagridview content from firebase
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

                    //unselect

                    Inventory_Datagridview.Rows[0].Cells[1].Selected = false;

                }

                catch
                {

                }
             
            }



      



            //Condition for Stock indicator Images
            foreach (DataGridViewRow row in Inventory_Datagridview.Rows)
            {

                try
                {
     
                    StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.new_low_on_stock, NCR_SYSTEM_1.Properties.Resources.new_in_stock, NCR_SYSTEM_1.Properties.Resources.new_high_on_stock, NCR_SYSTEM_1.Properties.Resources.new_out_of_stock };

                    if (Convert.ToInt32(row.Cells[8].Value) <= Convert.ToInt32(row.Cells[9].Value))
                    {
                        row.Cells[12].Value = StatusImgs[0];
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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Inventory_Datagridview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // ADDING item to cart
            try
            {
                if (e.ColumnIndex == Inventory_Datagridview.Columns[11].Index)
                {
                    Inventory_Datagridview.Rows[e.RowIndex].Selected = true;


                    id = Inventory_Datagridview.Rows[e.RowIndex].Cells[0].Value.ToString();
                    productname = Inventory_Datagridview.Rows[e.RowIndex].Cells[1].Value.ToString();
                    unit = Inventory_Datagridview.Rows[e.RowIndex].Cells[2].Value.ToString();

                    string zero = "0.00";

                    Cart_Datagridview.Rows.Add(id, productname, 0, unit, zero, zero);

                    List<string> list = new List<string>();
                    for (int i = 0; i < Cart_Datagridview.Rows.Count; i++)
                    {
                        string str = Cart_Datagridview.Rows[i].Cells[0].Value.ToString();
                        if (!list.Contains(str))
                        {
                           

                            //add

                            list.Add(Cart_Datagridview.Rows[i].Cells[0].Value.ToString());

                            //unselect
                            Cart_Datagridview.Rows[0].Cells[1].Selected = false;
                        }
                        else
                        {
                            Cart_Datagridview.Rows.Remove(Cart_Datagridview.Rows[i]);
                            MessageBox.Show("You already have this item");
                        }

                    }
                }
            }
            catch
            {

            }
        }

        private void Cart_Datagridview_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // inialize variable that will be used in computation
            decimal uprice = 0;
            int qty = 0;
            decimal product = 0;

            //  calculation
            uprice = Convert.ToDecimal(Cart_Datagridview.Rows[e.RowIndex].Cells[4].Value);
            qty = Convert.ToInt32(Cart_Datagridview.Rows[e.RowIndex].Cells[2].Value);
            product = uprice * qty;

            Cart_Datagridview.Rows[e.RowIndex].Cells[4].Value = uprice.ToString(fmt);
            Cart_Datagridview.Rows[e.RowIndex].Cells[5].Value = product.ToString(fmt);

            // line total
            try
            {
                subtotal = 0;


                for (int i = 0; i < Cart_Datagridview.Rows.Count; i++)
                {
                    subtotal += Convert.ToDecimal(Cart_Datagridview.Rows[i].Cells[5].Value);
                    
                }

                Sum.Text = subtotal.ToString(fmt) + " PHP";

                if (Sum.Text == ".00 PHP")
                {
                    Sum.Text = "0.00" + " PHP";
                }
            }

            catch
            {

            }
        }

        private void Fee_OnValueChanged(object sender, EventArgs e)
        {

            compute();


        }
        // COMPUTATION
        public void compute()
        {
            try
            {
                //Calculate total
                fee = Convert.ToDecimal(Fee.Text);
                total = subtotal + fee;
                FinalTotal.Text = Convert.ToString(total) + " PHP";

                //calculate Change
                payment = Convert.ToDecimal(Payment.Text);   
                change = payment - total;
                Change.Text = Convert.ToString(change)+ " PHP";



            }
            catch
            {

            }

        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            //clear
            quantitylist.Clear();
            Unitlist.Clear();
            productnamelist.Clear();
            Pricelist.Clear();



            // details of cart
            details = "";
            details2 = "";
            for (int i = 0; i < Cart_Datagridview.RowCount; i++)
            {
                

                productnamelist.Add(Cart_Datagridview.Rows[i].Cells[1].Value.ToString());
                quantitylist.Add(Cart_Datagridview.Rows[i].Cells[2].Value.ToString());
                Unitlist.Add(Cart_Datagridview.Rows[i].Cells[3].Value.ToString());
                Pricelist.Add(Cart_Datagridview.Rows[i].Cells[4].Value.ToString());

                
                details += quantitylist[i] + "  " + "  " + Unitlist[i] + "  " + "  " + productnamelist[i] + "  " + "  " + Pricelist[i] + "\n";

                details2 += quantitylist[i] + "\r\n";
                details2 += Unitlist[i] + "\r\n";
                details2 += productnamelist[i] + "\r\n";
                details2 += Pricelist[i] + "\r\n";
            }

            cartcount = Cart_Datagridview.RowCount;


            ProcessOrder_popup a = new ProcessOrder_popup();
            a.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Payment_OnValueChanged(object sender, EventArgs e)
        {
            compute();
        }

        private void Cart_Datagridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //remove
            if (e.ColumnIndex == Cart_Datagridview.Columns[6].Index)
            {
                this.Cart_Datagridview.Rows.RemoveAt(e.RowIndex);

                try
                {
                    subtotal = 0;


                    for (int i = 0; i < Cart_Datagridview.Rows.Count; i++)
                    {
                        subtotal += Convert.ToDecimal(Cart_Datagridview.Rows[i].Cells[5].Value);

                    }

                    Sum.Text = subtotal.ToString(fmt) + " PHP";

                    if (Sum.Text == ".00 PHP")
                    {
                        Sum.Text = "0.00" + " PHP";
                    }
                }

                catch
                {

                }
            }
        }


        public void addstock()
        {
            string[] ids = new string[100];

            int z = 0;

            int[] rowindexI = new int[100];
            int[] rowindexC = new int[100];

            for (z = 0; z < Cart_Datagridview.Rows.Count; z++)
            {
                ids[z] = Cart_Datagridview.Rows[z].Cells[0].Value.ToString();

                foreach (DataGridViewRow row in Inventory_Datagridview.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(ids[z]))
                    {
                        rowindexI[z] = row.Index;

                    }
                }

                foreach (DataGridViewRow row in Cart_Datagridview.Rows)
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
                        ID = Cart_Datagridview.Rows[z].Cells[0].Value.ToString(),
                        //Product_Name = Cart_Datagridview.Rows[z].Cells[1].Value.ToString(),
                        Stock = Convert.ToInt32(Inventory_Datagridview.Rows[rowindexI[z]].Cells[8].Value) + Convert.ToInt32(Cart_Datagridview.Rows[rowindexC[z]].Cells[2].Value),
                    };

                    FirebaseResponse resps = client.Update("Inventory/" + ids[z], datas);
                    Product_class res = resps.ResultAs<Product_class>();
                }
                catch (Exception c) { MessageBox.Show(c.ToString()); }


            }

            Cart_Datagridview.Rows.Clear();
            DataViewAll();

            Confirmreceipt_Module a = new Confirmreceipt_Module();
            a.Show();

        }

        private void bunifuImageButton14_Click(object sender, EventArgs e)
        {
            Inventory_Module a = new Inventory_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
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

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            Supplierrecord_module a = new Supplierrecord_module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void searchtxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (searchtxt.Text == "" && supressor == 1)
            {
                supressor = 0;

                Inventory_Datagridview.DataSource = null;
                Inventory_Datagridview.Rows.Clear();
                Inventory_Datagridview.Columns.Clear();
                Inventory_Datagridview.DataSource = dt;


                DataGridViewImageColumn Add = new DataGridViewImageColumn();
                Inventory_Datagridview.Columns.Add(Add);
                Add.HeaderText = "";
                Add.Name = "";
                Add.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Add.Image = Properties.Resources.Add_Icon;


                DataGridViewImageColumn Indicator = new DataGridViewImageColumn();
                Inventory_Datagridview.Columns.Add(Indicator);
                Indicator.HeaderText = "Indicator";
                Indicator.Name = "Indicator";
                Indicator.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Indicator.Image = Properties.Resources.loading;


                DataViewAll();

          

            }

            if (searchtxt.Text != "")
            {
                supressor = 1;

            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = "[" + combofilter.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

            Inventory_Datagridview.DataSource = null;
            Inventory_Datagridview.Rows.Clear();
            Inventory_Datagridview.Columns.Clear();
            Inventory_Datagridview.DataSource = dv;



            DataGridViewImageColumn Add = new DataGridViewImageColumn();
            Inventory_Datagridview.Columns.Add(Add);
            Add.HeaderText = "";
            Add.Name = "";
            Add.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Add.Image = Properties.Resources.Add_Icon;


            DataGridViewImageColumn Indicator = new DataGridViewImageColumn();
            Inventory_Datagridview.Columns.Add(Indicator);
            Indicator.HeaderText = "Indicator";
            Indicator.Name = "Indicator";
            Indicator.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Indicator.Image = Properties.Resources.loading;



            searchupdate();
        }

        public void searchupdate()
        {
            foreach (DataGridViewColumn column in Inventory_Datagridview.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            //Adjusting Visual settings of Inventory Datagridview
            this.Inventory_Datagridview.AllowUserToAddRows = false;

            Inventory_Datagridview.Columns[5].Visible = false;
            Inventory_Datagridview.Columns[0].Visible = false;
            Inventory_Datagridview.Columns[7].Visible = false;
            Inventory_Datagridview.Columns[9].Visible = false;
            Inventory_Datagridview.Columns[10].Visible = false;







            Inventory_Datagridview.Columns[12].DisplayIndex = 7;

            DataGridViewColumn column1 = Inventory_Datagridview.Columns[1];
            column1.Width = 220;

            DataGridViewColumn column2 = Inventory_Datagridview.Columns[2];
            column2.Width = 80;

            DataGridViewColumn column3 = Inventory_Datagridview.Columns[3];
            column3.Width = 100;

            DataGridViewColumn column4 = Inventory_Datagridview.Columns[4];
            column4.Width = 220;

            DataGridViewColumn column6 = Inventory_Datagridview.Columns[6];
            column6.Width = 80;

            DataGridViewColumn column8 = Inventory_Datagridview.Columns[8];
            column8.Width = 60;

            DataGridViewColumn column12 = Inventory_Datagridview.Columns[12];
            column12.Width = 100;



            Inventory_Datagridview.Columns["Product Name"].HeaderCell.Style.Padding = new Padding(20, 0, 0, 0);
            Inventory_Datagridview.Columns[1].DefaultCellStyle.Padding = new Padding(20, 0, 0, 0);




            //Adjusting Visual settings of Cart Datagridview
            this.Cart_Datagridview.AllowUserToAddRows = false;

            Cart_Datagridview.Columns[0].Visible = false;

            DataGridViewColumn cartcolumn1 = Cart_Datagridview.Columns[1];
            cartcolumn1.Width = 180;

            Cart_Datagridview.Columns[1].DefaultCellStyle.Padding = new Padding(20, 0, 0, 0);
            Cart_Datagridview.Columns["pn"].HeaderCell.Style.Padding = new Padding(20, 0, 0, 0);






            //Condition for Stock indicator Images
            foreach (DataGridViewRow row in Inventory_Datagridview.Rows)
            {

                try
                {

                    StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.new_low_on_stock, NCR_SYSTEM_1.Properties.Resources.new_in_stock, NCR_SYSTEM_1.Properties.Resources.new_high_on_stock, NCR_SYSTEM_1.Properties.Resources.new_out_of_stock };

                    if (Convert.ToInt32(row.Cells[8].Value) <= Convert.ToInt32(row.Cells[9].Value) && Convert.ToInt32(row.Cells[8].Value) != 0)
                    {
                        row.Cells[12].Value = StatusImgs[0];
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

        public void clear()
        {
            //clear
            Sum.Text = "0.00 PHP";
            Fee.Text = "0.00";
            Payment.Text = "0.00";
            Change.Text = "0.00 PHP";
            FinalTotal.Text = "0.00 PHP";
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Cart_Datagridview.Rows.Clear();

            try
            {
                subtotal = 0;


                for (int i = 0; i < Cart_Datagridview.Rows.Count; i++)
                {
                    subtotal += Convert.ToDecimal(Cart_Datagridview.Rows[i].Cells[5].Value);

                }

                Sum.Text = subtotal.ToString(fmt) + " PHP";

                if (Sum.Text == ".00 PHP")
                {
                    Sum.Text = "0.00" + " PHP";
                }

            }

            catch
            {

            }
        }
    }
}


class SDU
{
    public string ID { get; set; }
    //public string Product_Name { get; set; }
    public int Stock { get; set; }
    public int Items_Sold { get; set; }
}