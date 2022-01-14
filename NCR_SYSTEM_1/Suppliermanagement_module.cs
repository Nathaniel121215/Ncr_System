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
    public partial class Suppliermanagement_module : Form
    {
        public static string checker = "";

        int supressor = 1;

        public static string Supplier_ID = "";
        public static string Supplier_Name = "";
        public static string Supplier_Address = "";
        public static string Supplier_Number = "";
        public static string Last_Transaction = "";
        public static string Date_Added = "";
  


        DataTable dt = new DataTable();

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public static Suppliermanagement_module _instance;

        public Suppliermanagement_module()
        {
            _instance = this;
            InitializeComponent();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
          

            if (Form1.status == "true")
            {
                Addsupplier_popup a = new Addsupplier_popup();
                a.Show();

                Form1.status = "false";
            }
            else
            {
                MessageBox.Show("The Module is still loading or a window is currently open.");
            }
        }

        private void Suppliermanagement_module_Load(object sender, EventArgs e)
        {
            this.Supplier_Datagrid.AllowUserToAddRows = false;
            datedisplay.Text = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            datedisplay.Select();
            client = new FireSharp.FirebaseClient(config);

            dt.Columns.Add("Supplier ID");
            dt.Columns.Add("Supplier Name");
            dt.Columns.Add("Supplier Address");
            dt.Columns.Add("Supplier Number");
            dt.Columns.Add("Last Transaction");
            dt.Columns.Add("Date Added");
 



            Supplier_Datagrid.DataSource = dt;


            DataGridViewImageColumn update = new DataGridViewImageColumn();
            Supplier_Datagrid.Columns.Add(update);
            update.HeaderText = "";
            update.Name = "update";
            update.ImageLayout = DataGridViewImageCellLayout.Zoom;
            update.Image = Properties.Resources.Update_Icon;


            DataGridViewImageColumn Archive = new DataGridViewImageColumn();
            Supplier_Datagrid.Columns.Add(Archive);
            Archive.HeaderText = "";
            Archive.Name = "Archive";
            Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Archive.Image = Properties.Resources.Archive_Icon;


            dataview();

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

        public async void dataview()
        {

            foreach (DataGridViewColumn column in Supplier_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //visual
            DataGridViewColumn column1 = Supplier_Datagrid.Columns[1];
            column1.Width = 210;

            DataGridViewColumn column2 = Supplier_Datagrid.Columns[2];
            column2.Width = 270;

            DataGridViewColumn column3 = Supplier_Datagrid.Columns[3];
            column3.Width = 220;

            DataGridViewColumn column4 = Supplier_Datagrid.Columns[4];
            column4.Width = 170;

            DataGridViewColumn column5 = Supplier_Datagrid.Columns[5];
            column5.Width = 170;


            DataGridViewColumn column6 = Supplier_Datagrid.Columns[6];
            column6.Width = 80;

            DataGridViewColumn column7 = Supplier_Datagrid.Columns[7];
            column7.Width = 80;




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
                    FirebaseResponse resp1 = await client.GetTaskAsync("Supplier/" + i);
                    Supplier_Class user = resp1.ResultAs<Supplier_Class>();

                 
                    DataRow r = dt.NewRow();

                    r["Supplier ID"] = user.Supplier_ID;
                    r["Supplier Name"] = user.Supplier_Name;
                    r["Supplier Address"] = user.Supplier_Address;
                    r["Supplier Number"] = user.Supplier_Number;
                    r["Last Transaction"] = user.Supplier_LastTransaction;

                    if(user.Supplier_LastTransaction==null)
                    {
                        r["Last Transaction"] = "None";
                    }
                    r["Date Added"] = user.Supplier_DateAdded;
               



                    dt.Rows.Add(r);
                }

                catch
                {

                }


            }

            checker = "allow";
        }

        private void Supplier_Datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(Form1.status=="true")
            {
                string columnindex = "";

                try
                {
                    //delete
                    if (e.ColumnIndex == Supplier_Datagrid.Columns[7].Index)
                    {





                        if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                        {

                            columnindex = Supplier_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();

                            //new archive code



                            var data = new SupplierArchive_Class
                            {

                                Supplier_ID = Supplier_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString(),
                                Supplier_Name = Supplier_Datagrid.Rows[e.RowIndex].Cells[1].Value.ToString(),
                                Supplier_Address = Supplier_Datagrid.Rows[e.RowIndex].Cells[2].Value.ToString(),
                                Supplier_Number = Supplier_Datagrid.Rows[e.RowIndex].Cells[3].Value.ToString(),
                                Supplier_LastTransaction = Supplier_Datagrid.Rows[e.RowIndex].Cells[4].Value.ToString(),
                                Supplier_DateAdded = Supplier_Datagrid.Rows[e.RowIndex].Cells[5].Value.ToString(),

                                Date_Archive = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                                User = Form1.username,

                            };

                            //add to archive 
                            FirebaseResponse response3 = client.Set("SupplierArchive/" + data.Supplier_ID, data);

                            //get archive counter
                            FirebaseResponse resp = client.Get("SupplierArchiveCounter/node");
                            Counter_class get = resp.ResultAs<Counter_class>();

                            //update archive counter
                            var obj = new Counter_class
                            {
                                cnt = (Convert.ToInt32(get.cnt) + 1).ToString(),
                            };

                            SetResponse response4 = client.Set("SupplierArchiveCounter/node", obj);



                            //delete from current table

                            FirebaseResponse response = client.Delete("Supplier/" + columnindex);

                            FirebaseResponse resp2 = client.Get("SupplierCounterExisting/node");
                            Counter_class get2 = resp2.ResultAs<Counter_class>();
                            string employee = (Convert.ToInt32(get2.cnt) - 1).ToString();
                            var obj2 = new Counter_class
                            {
                                cnt = employee
                            };

                            SetResponse response2 = client.Set("SupplierCounterExisting/node", obj2);


                            //Activity Log ARCHIVING ACCOUNT EVENT


                            FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                            Counter_class get4 = resp4.ResultAs<Counter_class>();
                            int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                            var data2 = new ActivityLog_Class
                            {
                                Event_ID = cnt4.ToString(),
                                Module = "Supplier Management Module",
                                Action = "Supplier-ID: " + data.Supplier_ID + "   Moved to Archive Module",
                                Date = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                                User = Form1.username,
                                Accountlvl = Form1.levelac,

                            };



                            FirebaseResponse response5 = client.Set("ActivityLog/" + data2.Event_ID, data2);



                            var obj4 = new Counter_class
                            {
                                cnt = data2.Event_ID

                            };

                            SetResponse response6 = client.Set("ActivityLogCounter/node", obj4);



                            dataview();

                        }

                        else

                        {
                        }



                    }
                    //update
                    if (e.ColumnIndex == Supplier_Datagrid.Columns[6].Index)
                    {
                        columnindex = Supplier_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();


                        Supplier_Datagrid.Rows[e.RowIndex].Selected = true;

                        Supplier_ID = Supplier_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                        Supplier_Name = Supplier_Datagrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                        Supplier_Address = Supplier_Datagrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                        Supplier_Number = Supplier_Datagrid.Rows[e.RowIndex].Cells[3].Value.ToString();
                        Last_Transaction = Supplier_Datagrid.Rows[e.RowIndex].Cells[4].Value.ToString();
                        Date_Added = Supplier_Datagrid.Rows[e.RowIndex].Cells[5].Value.ToString();



                        Updatesupplier_popup update = new Updatesupplier_popup();

                        update.Show();
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

        private void searchtxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (searchtxt.Text == "" && supressor == 1)
            {
                supressor = 0;

                Supplier_Datagrid.DataSource = null;
                Supplier_Datagrid.Rows.Clear();
                Supplier_Datagrid.Columns.Clear();
                Supplier_Datagrid.DataSource = dt;




                DataGridViewImageColumn update = new DataGridViewImageColumn();
                Supplier_Datagrid.Columns.Add(update);
                update.HeaderText = "";
                update.Name = "update";
                update.ImageLayout = DataGridViewImageCellLayout.Zoom;
                update.Image = Properties.Resources.Update_Icon;


                DataGridViewImageColumn Archive = new DataGridViewImageColumn();
                Supplier_Datagrid.Columns.Add(Archive);
                Archive.HeaderText = "";
                Archive.Name = "Archive";
                Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Archive.Image = Properties.Resources.Archive_Icon;

                dataview();

               

            }

            if (searchtxt.Text != "")
            {
                supressor = 1;

            }
        }

        private void a_Click(object sender, EventArgs e)
        {
            if (searchtxt.Text != "" & Form1.status == "true")
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "[" + combofilter.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

                Supplier_Datagrid.DataSource = null;
                Supplier_Datagrid.Rows.Clear();
                Supplier_Datagrid.Columns.Clear();
                Supplier_Datagrid.DataSource = dv;

                DataGridViewImageColumn update = new DataGridViewImageColumn();
                Supplier_Datagrid.Columns.Add(update);
                update.HeaderText = "";
                update.Name = "update";
                update.ImageLayout = DataGridViewImageCellLayout.Zoom;
                update.Image = Properties.Resources.Update_Icon;


                DataGridViewImageColumn Archive = new DataGridViewImageColumn();
                Supplier_Datagrid.Columns.Add(Archive);
                Archive.HeaderText = "";
                Archive.Name = "Archive";
                Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Archive.Image = Properties.Resources.Archive_Icon;


                foreach (DataGridViewColumn column in Supplier_Datagrid.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                //visual
                DataGridViewColumn column1 = Supplier_Datagrid.Columns[1];
                column1.Width = 210;

                DataGridViewColumn column2 = Supplier_Datagrid.Columns[2];
                column2.Width = 270;

                DataGridViewColumn column3 = Supplier_Datagrid.Columns[3];
                column3.Width = 220;

                DataGridViewColumn column4 = Supplier_Datagrid.Columns[4];
                column4.Width = 170;

                DataGridViewColumn column5 = Supplier_Datagrid.Columns[5];
                column5.Width = 170;

                DataGridViewColumn column6 = Supplier_Datagrid.Columns[6];
                column6.Width = 80;

                DataGridViewColumn column7 = Supplier_Datagrid.Columns[7];
                column7.Width = 80;
            }
            else
            {

            }



           


        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton19_Click(object sender, EventArgs e)
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

        private void bunifuImageButton17_Click(object sender, EventArgs e)
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

        private void bunifuImageButton16_Click(object sender, EventArgs e)
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

        private void bunifuImageButton9_Click_1(object sender, EventArgs e)
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

        private void bunifuImageButton18_Click(object sender, EventArgs e)
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

        private void searchtxt_Enter(object sender, EventArgs e)
        {
            searchtxt.Text = "";
        }

        private void searchtxt_Leave(object sender, EventArgs e)
        {
            if (searchtxt.Text == "")
            {
                searchtxt.Text = "Type here to filter Supplier Record Content";
            }
            else
            {

            }
        }

        private void bunifuImageButton10_Click(object sender, EventArgs e)
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
                    MessageBox.Show("The Module is still loading.");
                }
            }
        }
    }
}
