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
    public partial class Addbrand_module : Form
    {

        int supressor = 1;

        public static string Brand_ID = "";
        public static string Brand_Name = "";
        public static string Date_Added = "";

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        DataTable dt = new DataTable();

        public static Addbrand_module _instance;


        public Addbrand_module()
        {
            InitializeComponent();
            _instance = this;
        }

        private void Addbrand_module_Load(object sender, EventArgs e)
        {
            datedisplay.Text = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            datedisplay.Select();
            client = new FireSharp.FirebaseClient(config);

            this.Brand_datagrid_stocks.AllowUserToAddRows = false;

            dt.Columns.Add("Brand ID");
            dt.Columns.Add("Brand Name");
            dt.Columns.Add("Date Added");



            Brand_datagrid_stocks.DataSource = dt;

            DataGridViewImageColumn update = new DataGridViewImageColumn();
            Brand_datagrid_stocks.Columns.Add(update);
            update.HeaderText = "";
            update.Name = "update";
            update.ImageLayout = DataGridViewImageCellLayout.Zoom;
            update.Image = Properties.Resources.Update_Icon;


            DataGridViewImageColumn Archive = new DataGridViewImageColumn();
            Brand_datagrid_stocks.Columns.Add(Archive);
            Archive.HeaderText = "";
            Archive.Name = "Archive";
            Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Archive.Image = Properties.Resources.Archive_Icon;

            DataViewAll();

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

        public async void DataViewAll()
        {
            //visual
            DataGridViewColumn column2 = Brand_datagrid_stocks.Columns[2];
            column2.Width = 600;

            DataGridViewColumn column3 = Brand_datagrid_stocks.Columns[3];
            column3.Width = 80;

            DataGridViewColumn column4 = Brand_datagrid_stocks.Columns[4];
            column4.Width = 80;


            foreach (DataGridViewColumn column in Brand_datagrid_stocks.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            dt.Rows.Clear();

            int i = 0;
            FirebaseResponse resp = await client.GetTaskAsync("BrandCounter/node");
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

                    FirebaseResponse resp1 = await client.GetTaskAsync("Brand/" + i);
                    Brand_Class obj1 = resp1.ResultAs<Brand_Class>();

                    DataRow r = dt.NewRow();
                    r["Brand ID"] = obj1.Brand_ID;
                    r["Brand Name"] = obj1.Brand_Name;
                    r["Date Added"] = obj1.Date_Added;


                    dt.Rows.Add(r);
                }

                catch
                {

                }
            }
        }

        private void Brand_datagrid_stocks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Form1.status == "true")
            {
                string columnindex = "";

                try
                {


                    if (e.ColumnIndex == Brand_datagrid_stocks.Columns[3].Index)
                    {

                        columnindex = Brand_datagrid_stocks.Rows[e.RowIndex].Cells[0].Value.ToString();


                        Brand_datagrid_stocks.Rows[e.RowIndex].Selected = true;

                        Brand_ID = Brand_datagrid_stocks.Rows[e.RowIndex].Cells[0].Value.ToString();
                        Brand_Name = Brand_datagrid_stocks.Rows[e.RowIndex].Cells[1].Value.ToString();
                        Date_Added = Brand_datagrid_stocks.Rows[e.RowIndex].Cells[2].Value.ToString();



                        Updatebrand_popup a = new Updatebrand_popup();
                        a.Show();
                        Form1.status = "false";

                    }


                }
                catch
                {

                }

                //delete

                try
                {


                    if (e.ColumnIndex == Brand_datagrid_stocks.Columns[4].Index)
                    {
                        if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                        {
                            columnindex = Brand_datagrid_stocks.Rows[e.RowIndex].Cells[0].Value.ToString();

                            FirebaseResponse response = client.Delete("Brand/" + columnindex);
                            DataViewAll();
                        }
                        else
                        {

                        }

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

        private void Brand_datagrid_stocks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (searchtxt.Text != "" & Form1.status == "true")
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "[" + combofilter.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

                Brand_datagrid_stocks.DataSource = null;
                Brand_datagrid_stocks.Rows.Clear();
                Brand_datagrid_stocks.Columns.Clear();
                Brand_datagrid_stocks.DataSource = dv;


                DataGridViewImageColumn update = new DataGridViewImageColumn();
                Brand_datagrid_stocks.Columns.Add(update);
                update.HeaderText = "";
                update.Name = "update";
                update.ImageLayout = DataGridViewImageCellLayout.Zoom;
                update.Image = Properties.Resources.Update_Icon;


                DataGridViewImageColumn Archive = new DataGridViewImageColumn();
                Brand_datagrid_stocks.Columns.Add(Archive);
                Archive.HeaderText = "";
                Archive.Name = "Archive";
                Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Archive.Image = Properties.Resources.Archive_Icon;

                //visual
                DataGridViewColumn column2 = Brand_datagrid_stocks.Columns[2];
                column2.Width = 600;

                DataGridViewColumn column3 = Brand_datagrid_stocks.Columns[3];
                column3.Width = 80;

                DataGridViewColumn column4 = Brand_datagrid_stocks.Columns[4];
                column4.Width = 80;


                foreach (DataGridViewColumn column in Brand_datagrid_stocks.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            else
            {

            }
        }

        private void searchtxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (searchtxt.Text == "" && supressor == 1)
            {
                supressor = 0;

                Brand_datagrid_stocks.DataSource = null;
                Brand_datagrid_stocks.Rows.Clear();
                Brand_datagrid_stocks.Columns.Clear();
                Brand_datagrid_stocks.DataSource = dt;




                DataGridViewImageColumn update = new DataGridViewImageColumn();
                Brand_datagrid_stocks.Columns.Add(update);
                update.HeaderText = "";
                update.Name = "update";
                update.ImageLayout = DataGridViewImageCellLayout.Zoom;
                update.Image = Properties.Resources.Update_Icon;


                DataGridViewImageColumn Archive = new DataGridViewImageColumn();
                Brand_datagrid_stocks.Columns.Add(Archive);
                Archive.HeaderText = "";
                Archive.Name = "Archive";
                Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Archive.Image = Properties.Resources.Archive_Icon;

                DataViewAll();



            }

            if (searchtxt.Text != "")
            {
                supressor = 1;

            }
        }

        private void searchtxt_Enter(object sender, EventArgs e)
        {
            searchtxt.Text = "";
        }

        private void searchtxt_Leave(object sender, EventArgs e)
        {
            if (searchtxt.Text == "")
            {
                searchtxt.Text = "Type here to filter Brand table Content";
            }
            else
            {

            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true")
            {
                Addbrand_pop a = new Addbrand_pop();
                a.Show();
                Form1.status = "false";
            }
            else
            {
                MessageBox.Show("The Module is still loading or a window is currently open.");
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
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

        private void bunifuImageButton14_Click(object sender, EventArgs e)
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

        private void bunifuImageButton12_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    stockpurchase_Module a = new stockpurchase_Module();
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
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    StockAdjustment_Module a = new StockAdjustment_Module();
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
                    Addunit_module a = new Addunit_module();
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

        private void bunifuImageButton11_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    Addcategory_module a = new Addcategory_module();
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
            if (Form1.status == "true" && Form1.levelac.Equals("Admin"))
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

        private void bunifuImageButton10_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin")))
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

        private void bunifuImageButton2_Click(object sender, EventArgs e)
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

        private void bunifuImageButton17_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    Addbrand_module a = new Addbrand_module();
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
    }
}
