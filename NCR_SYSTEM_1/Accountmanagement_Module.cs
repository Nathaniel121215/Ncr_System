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
    public partial class Accountmanagement_Module : Form
    {
        public static string checker = "";

        private Image[] StatusImgs;

        int supressor = 1;

        public static string User_ID = "";
        public static string Username = "";
        public static string Password = "";
        public static string Firstname = "";
        public static string Lastname = "";
        public static string Account_Level = "";
        public static string Date_Added = "";

        public static string inventory = "";
        public static string Pos = "";
        public static string Supplier = "";
        public static string Records = "";




        DataTable dt = new DataTable();


        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;
        public static Accountmanagement_Module _instance;
        public Accountmanagement_Module()
        {
            InitializeComponent();
            _instance = this;
        }

        private void Accountmanagement_Module_Load(object sender, EventArgs e)
        {
            datedisplay.Text = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            datedisplay.Select();
            this.Account_Datagrid.AllowUserToAddRows = false;


            client = new FireSharp.FirebaseClient(config);

            dt.Columns.Add("User ID");
            dt.Columns.Add("Username");
            dt.Columns.Add("Password");
            dt.Columns.Add("Firstname");
            dt.Columns.Add("Lastname");
            dt.Columns.Add("Account Level");
            dt.Columns.Add("Date Added");

            ////access////
            
            dt.Columns.Add("Inventory");
            dt.Columns.Add("Pos");
            dt.Columns.Add("Supplier");
            dt.Columns.Add("Records");



            Account_Datagrid.DataSource = dt;



            DataGridViewImageColumn update = new DataGridViewImageColumn();
            Account_Datagrid.Columns.Add(update);
            update.HeaderText = "";
            update.Name = "update";
            update.ImageLayout = DataGridViewImageCellLayout.Zoom;
            update.Image = Properties.Resources.Update_Icon;


            DataGridViewImageColumn Archive = new DataGridViewImageColumn();
            Account_Datagrid.Columns.Add(Archive);
            Archive.HeaderText = "";
            Archive.Name = "Archive";
            Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Archive.Image = Properties.Resources.Archive_Icon;

            ///////////////////////// access/////////////////////////
           
            DataGridViewImageColumn Inventory = new DataGridViewImageColumn();
            Account_Datagrid.Columns.Add(Inventory);
            Inventory.HeaderText = "Inventory Module";
            Inventory.Name = "Inventory";
            Inventory.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Inventory.Image = Properties.Resources.loading;


            DataGridViewImageColumn PoS = new DataGridViewImageColumn();
            Account_Datagrid.Columns.Add(PoS);
            PoS.HeaderText = "PoS Module";
            PoS.Name = "PoS";
            PoS.ImageLayout = DataGridViewImageCellLayout.Zoom;
            PoS.Image = Properties.Resources.loading;


            DataGridViewImageColumn Supplier = new DataGridViewImageColumn();
            Account_Datagrid.Columns.Add(Supplier);
            Supplier.HeaderText = "Supplier Module";
            Supplier.Name = "Supplier";
            Supplier.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Supplier.Image = Properties.Resources.loading;



            DataGridViewImageColumn Records = new DataGridViewImageColumn();
            Account_Datagrid.Columns.Add(Records);
            Records.HeaderText = "Record Module";
            Records.Name = "Records";
            Records.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Records.Image = Properties.Resources.loading;

            ///////////////////////// Level /////////////////////////
          

            DataGridViewImageColumn AccountLvl = new DataGridViewImageColumn();
            Account_Datagrid.Columns.Add(AccountLvl);
            AccountLvl.HeaderText = "Account Level";
            AccountLvl.Name = "Account Level";
            AccountLvl.ImageLayout = DataGridViewImageCellLayout.Zoom;
            AccountLvl.Image = Properties.Resources.loading;


            dataview();

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

        public async void dataview()
        {
            //visual

            Account_Datagrid.Columns[5].Visible = false;
            Account_Datagrid.Columns[6].Visible = false;

            Account_Datagrid.Columns[7].Visible = false;
            Account_Datagrid.Columns[8].Visible = false;
            Account_Datagrid.Columns[9].Visible = false;
            Account_Datagrid.Columns[10].Visible = false;


            Account_Datagrid.Columns[17].DisplayIndex = 5;
            Account_Datagrid.Columns[13].DisplayIndex = 7;
            Account_Datagrid.Columns[14].DisplayIndex = 8;
            Account_Datagrid.Columns[15].DisplayIndex = 9;
            Account_Datagrid.Columns[16].DisplayIndex = 10;

            DataGridViewColumn column11 = Account_Datagrid.Columns[11];
            column11.Width = 80;
            DataGridViewColumn column12 = Account_Datagrid.Columns[12];
            column12.Width = 80;

            foreach (DataGridViewColumn column in Account_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            dt.Rows.Clear();
            int i = 0;
            FirebaseResponse resp = await client.GetTaskAsync("AccountCounter/node");
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
                    FirebaseResponse resp1 = await client.GetTaskAsync("Accounts/" + i);
                    User_class user = resp1.ResultAs<User_class>();

                    DataRow r = dt.NewRow();
                    r["User ID"] = user.User_ID;
                    r["Username"] = user.Username;
                    r["Password"] = user.Password;
                    r["Firstname"] = user.Firstname;
                    r["Lastname"] = user.Lastname;
                    r["Account Level"] = user.Account_Level;
                    r["Date Added"] = user.Date_Added;

                    ////accesss////

                    r["Inventory"] = user.Inventoryaccess;
                    r["Pos"] = user.Posaccess;
                    r["Supplier"] = user.Supplieraccess;
                    r["Records"] = user.Recordaccess;


                    dt.Rows.Add(r);
                }

                catch
                {

                }


            }

            ///////////////////////////////////////////////////////

            try
            {
                

                foreach (DataGridViewRow row in Account_Datagrid.Rows)
                {

                    try
                    {


                        StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.Group_175, NCR_SYSTEM_1.Properties.Resources.Group_177, NCR_SYSTEM_1.Properties.Resources.Group_179, NCR_SYSTEM_1.Properties.Resources.Group_181 };





                        if (row.Cells[7].Value.Equals("Authorized")) //Authorize inventory
                        {
                            row.Cells[13].Value = StatusImgs[0];
                        }
                        else
                        {
                            row.Cells[13].Value = StatusImgs[1];
                        }

                        if (row.Cells[8].Value.Equals("Authorized")) //Authorize PoS
                        {
                            row.Cells[14].Value = StatusImgs[0];
                        }
                        else
                        {
                            row.Cells[14].Value = StatusImgs[1];
                        }

                        if (row.Cells[9].Value.Equals("Authorized")) //Authorize Supplier
                        {
                            row.Cells[15].Value = StatusImgs[0];
                        }
                        else
                        {
                            row.Cells[15].Value = StatusImgs[1];
                        }


                        if (row.Cells[10].Value.Equals("Authorized")) //Authorize Records
                        {
                            row.Cells[16].Value = StatusImgs[0];
                        }
                        else
                        {
                            row.Cells[16].Value = StatusImgs[1];
                        }

                        ////////////////////Level ///////////////



                        if (row.Cells[5].Value.Equals("Admin")) //Authorize Records
                        {
                            row.Cells[17].Value = StatusImgs[3];
                        }
                        else
                        {
                            row.Cells[17].Value = StatusImgs[2];
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

            checker = "allow";

        }

        private void Account_Datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(Form1.status=="true")
            {
                string columnindex = "";

                try
                {
                    //delete
                    if (e.ColumnIndex == Account_Datagrid.Columns[12].Index)
                    {
                        if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                        {

                            columnindex = Account_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();

                            //new archive code



                            var data = new AccountArchive_Class
                            {

                                User_ID = Account_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString(),
                                Username = Account_Datagrid.Rows[e.RowIndex].Cells[1].Value.ToString(),
                                Password = Account_Datagrid.Rows[e.RowIndex].Cells[2].Value.ToString(),
                                Firstname = Account_Datagrid.Rows[e.RowIndex].Cells[3].Value.ToString(),
                                Lastname = Account_Datagrid.Rows[e.RowIndex].Cells[4].Value.ToString(),
                                Account_Level = Account_Datagrid.Rows[e.RowIndex].Cells[5].Value.ToString(),
                                Date_Added = Account_Datagrid.Rows[e.RowIndex].Cells[6].Value.ToString(),
                                Inventoryaccess = Account_Datagrid.Rows[e.RowIndex].Cells[7].Value.ToString(),
                                Posaccess = Account_Datagrid.Rows[e.RowIndex].Cells[8].Value.ToString(),
                                Supplieraccess = Account_Datagrid.Rows[e.RowIndex].Cells[9].Value.ToString(),
                                Recordaccess = Account_Datagrid.Rows[e.RowIndex].Cells[10].Value.ToString(),

                                Date_Archive = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                                User = Form1.username,

                            };


                            //add to archive 
                            FirebaseResponse response3 = client.Set("AccountArchive/" + data.User_ID, data);
                            Product_class result = response3.ResultAs<Product_class>();

                            //get archive counter
                            FirebaseResponse resp = client.Get("AccountArchiveCounter/node");
                            Counter_class get = resp.ResultAs<Counter_class>();

                            //update archive counter
                            var obj = new Counter_class
                            {
                                cnt = (Convert.ToInt32(get.cnt) + 1).ToString(),
                            };

                            SetResponse response4 = client.Set("AccountArchiveCounter/node", obj);



                            //delete from current table

                            FirebaseResponse response = client.Delete("Accounts/" + columnindex);

                            if (Account_Datagrid.Rows[e.RowIndex].Cells[5].Value.ToString().Equals("Employee"))
                            {
                                //existing employee
                                FirebaseResponse resp2 = client.Get("employeeCounterExisting/node");
                                Counter_class get2 = resp2.ResultAs<Counter_class>();
                                string employee = (Convert.ToInt32(get2.cnt) - 1).ToString();
                                var obj2 = new Counter_class
                                {
                                    cnt = employee
                                };

                                SetResponse response2 = client.Set("employeeCounterExisting/node", obj2);
                            }




                            //Activity Log ARCHIVING ACCOUNT EVENT


                            FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                            Counter_class get4 = resp4.ResultAs<Counter_class>();
                            int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                            var data2 = new ActivityLog_Class
                            {
                                Event_ID = cnt4.ToString(),
                                Module = "Account Management Module",
                                Action = "Account-ID: " + data.User_ID + "   Moved to Archive Module",
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


                            

                            if (Form1.userid == data.User_ID)
                            {
                                Application.Exit();
                            }
                            else
                            {
                                dataview();
                            }

                        }
                    }


                    //update
                    if (e.ColumnIndex == Account_Datagrid.Columns[11].Index)
                    {
                        columnindex = Account_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();


                        Account_Datagrid.Rows[e.RowIndex].Selected = true;

                        User_ID = Account_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                        Username = Account_Datagrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                        Password = Account_Datagrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                        Firstname = Account_Datagrid.Rows[e.RowIndex].Cells[3].Value.ToString();
                        Lastname = Account_Datagrid.Rows[e.RowIndex].Cells[4].Value.ToString();
                        Account_Level = Account_Datagrid.Rows[e.RowIndex].Cells[5].Value.ToString();
                        Date_Added = Account_Datagrid.Rows[e.RowIndex].Cells[6].Value.ToString();

                        inventory = Account_Datagrid.Rows[e.RowIndex].Cells[7].Value.ToString();
                        Pos = Account_Datagrid.Rows[e.RowIndex].Cells[8].Value.ToString();
                        Supplier = Account_Datagrid.Rows[e.RowIndex].Cells[9].Value.ToString();
                        Records = Account_Datagrid.Rows[e.RowIndex].Cells[10].Value.ToString();




                        Updateaccount_popup update = new Updateaccount_popup();

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

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            

            if (Form1.status=="true")
            {
                Addnewaccount_popup create = new Addnewaccount_popup();
                create.Show();

                Form1.status = "false";

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

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            Inventory_Module a = new Inventory_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton12_Click(object sender, EventArgs e)
        {
            Supplierrecord_module a = new Supplierrecord_module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton15_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            Dashboard_Module a = new Dashboard_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if(searchtxt.Text != "" & Form1.status == "true")
            {

                DataView dv = new DataView(dt);
                dv.RowFilter = "[" + combofilter.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

                Account_Datagrid.DataSource = null;
                Account_Datagrid.Rows.Clear();
                Account_Datagrid.Columns.Clear();
                Account_Datagrid.DataSource = dv;

                DataGridViewImageColumn update = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(update);
                update.HeaderText = "";
                update.Name = "update";
                update.ImageLayout = DataGridViewImageCellLayout.Zoom;
                update.Image = Properties.Resources.Update_Icon;


                DataGridViewImageColumn Archive = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(Archive);
                Archive.HeaderText = "";
                Archive.Name = "Archive";
                Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Archive.Image = Properties.Resources.Archive_Icon;

                ///////////////////////// access/////////////////////////

                DataGridViewImageColumn Inventory = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(Inventory);
                Inventory.HeaderText = "Inventory Module";
                Inventory.Name = "Inventory";
                Inventory.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Inventory.Image = Properties.Resources.loading;


                DataGridViewImageColumn PoS = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(PoS);
                PoS.HeaderText = "PoS Module";
                PoS.Name = "PoS";
                PoS.ImageLayout = DataGridViewImageCellLayout.Zoom;
                PoS.Image = Properties.Resources.loading;


                DataGridViewImageColumn Supplier = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(Supplier);
                Supplier.HeaderText = "Supplier Module";
                Supplier.Name = "Supplier";
                Supplier.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Supplier.Image = Properties.Resources.loading;



                DataGridViewImageColumn Records = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(Records);
                Records.HeaderText = "Record Module";
                Records.Name = "Records";
                Records.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Records.Image = Properties.Resources.loading;

                ///////////////////////// Level /////////////////////////


                DataGridViewImageColumn AccountLvl = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(AccountLvl);
                AccountLvl.HeaderText = "Account Level";
                AccountLvl.Name = "Account Level";
                AccountLvl.ImageLayout = DataGridViewImageCellLayout.Zoom;
                AccountLvl.Image = Properties.Resources.loading;

                searchupdate();
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

                Account_Datagrid.DataSource = null;
                Account_Datagrid.Rows.Clear();
                Account_Datagrid.Columns.Clear();
                Account_Datagrid.DataSource = dt;




                DataGridViewImageColumn update = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(update);
                update.HeaderText = "";
                update.Name = "update";
                update.ImageLayout = DataGridViewImageCellLayout.Zoom;
                update.Image = Properties.Resources.Update_Icon;


                DataGridViewImageColumn Archive = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(Archive);
                Archive.HeaderText = "";
                Archive.Name = "Archive";
                Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Archive.Image = Properties.Resources.Archive_Icon;


                ///////////////////////// access/////////////////////////

                DataGridViewImageColumn Inventory = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(Inventory);
                Inventory.HeaderText = "Inventory Module";
                Inventory.Name = "Inventory";
                Inventory.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Inventory.Image = Properties.Resources.loading;


                DataGridViewImageColumn PoS = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(PoS);
                PoS.HeaderText = "PoS Module";
                PoS.Name = "PoS";
                PoS.ImageLayout = DataGridViewImageCellLayout.Zoom;
                PoS.Image = Properties.Resources.loading;


                DataGridViewImageColumn Supplier = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(Supplier);
                Supplier.HeaderText = "Supplier Module";
                Supplier.Name = "Supplier";
                Supplier.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Supplier.Image = Properties.Resources.loading;



                DataGridViewImageColumn Records = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(Records);
                Records.HeaderText = "Record Module";
                Records.Name = "Records";
                Records.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Records.Image = Properties.Resources.loading;

                ///////////////////////// Level /////////////////////////


                DataGridViewImageColumn AccountLvl = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(AccountLvl);
                AccountLvl.HeaderText = "Account Level";
                AccountLvl.Name = "Account Level";
                AccountLvl.ImageLayout = DataGridViewImageCellLayout.Zoom;
                AccountLvl.Image = Properties.Resources.loading;

                dataview();

              

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

        private void bunifuImageButton17_Click(object sender, EventArgs e)
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

        private void bunifuImageButton9_Click(object sender, EventArgs e)
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
        public void searchupdate()
        {
            //visual

            Account_Datagrid.Columns[5].Visible = false;
            Account_Datagrid.Columns[6].Visible = false;

            Account_Datagrid.Columns[7].Visible = false;
            Account_Datagrid.Columns[8].Visible = false;
            Account_Datagrid.Columns[9].Visible = false;
            Account_Datagrid.Columns[10].Visible = false;


            Account_Datagrid.Columns[17].DisplayIndex = 5;
            Account_Datagrid.Columns[13].DisplayIndex = 7;
            Account_Datagrid.Columns[14].DisplayIndex = 8;
            Account_Datagrid.Columns[15].DisplayIndex = 9;
            Account_Datagrid.Columns[16].DisplayIndex = 10;

            DataGridViewColumn column11 = Account_Datagrid.Columns[11];
            column11.Width = 80;
            DataGridViewColumn column12 = Account_Datagrid.Columns[12];
            column12.Width = 80;

            foreach (DataGridViewColumn column in Account_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            try
            {


                foreach (DataGridViewRow row in Account_Datagrid.Rows)
                {

                    try
                    {


                        StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.Group_175, NCR_SYSTEM_1.Properties.Resources.Group_177, NCR_SYSTEM_1.Properties.Resources.Group_179, NCR_SYSTEM_1.Properties.Resources.Group_181 };





                        if (row.Cells[7].Value.Equals("Authorized")) //Authorize inventory
                        {
                            row.Cells[13].Value = StatusImgs[0];
                        }
                        else
                        {
                            row.Cells[13].Value = StatusImgs[1];
                        }

                        if (row.Cells[8].Value.Equals("Authorized")) //Authorize PoS
                        {
                            row.Cells[14].Value = StatusImgs[0];
                        }
                        else
                        {
                            row.Cells[14].Value = StatusImgs[1];
                        }

                        if (row.Cells[9].Value.Equals("Authorized")) //Authorize Supplier
                        {
                            row.Cells[15].Value = StatusImgs[0];
                        }
                        else
                        {
                            row.Cells[15].Value = StatusImgs[1];
                        }


                        if (row.Cells[10].Value.Equals("Authorized")) //Authorize Records
                        {
                            row.Cells[16].Value = StatusImgs[0];
                        }
                        else
                        {
                            row.Cells[16].Value = StatusImgs[1];
                        }

                        ////////////////////Level ///////////////



                        if (row.Cells[5].Value.Equals("Admin")) //Authorize Records
                        {
                            row.Cells[17].Value = StatusImgs[3];
                        }
                        else
                        {
                            row.Cells[17].Value = StatusImgs[2];
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
        }

        private void bunifuImageButton18_Click(object sender, EventArgs e)
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
            else if (Form1.levelac.Equals("Employee") && Form1.posac.Equals("Authorized") && Form1.status == "true")
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

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
        private void copyAlltoClipboard()
        {
            Account_Datagrid.SelectAll();
            DataObject dataObj = Account_Datagrid.GetClipboardContent();
            if (dataObj != null)
            Clipboard.SetDataObject(dataObj);
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if(Form1.status=="true")
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

        private void searchtxt_Leave(object sender, EventArgs e)
        {
            if (searchtxt.Text == "")
            {
                searchtxt.Text = "Type here to filter Account management Content";
            }
            else
            {

            }
        }

        private void searchtxt_Enter(object sender, EventArgs e)
        {
            searchtxt.Text = "";
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
                    MessageBox.Show("The Module is still loading or a window is currently open.");
                }
            }
        }

        private void Account_Datagrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
