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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Image = System.Drawing.Image;


namespace NCR_SYSTEM_1
{
    public partial class Accountmanagement_Module : Form
    {


        private Image[] StatusImgs;

        int supressor = 1;

        public static string User_ID = "";
        public static string Username = "";
        public static string Password = "";
        public static string Firstname = "";
        public static string Lastname = "";
        public static string Account_Level = "";
        public static string Date_Added = "";





        DataTable dt = new DataTable();
        DataTable printer = new DataTable();


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

            printer.Columns.Add("User ID");
            printer.Columns.Add("Username");
            printer.Columns.Add("Firstname");
            printer.Columns.Add("Lastname");
            printer.Columns.Add("Account Level");
            printer.Columns.Add("Date Added");

            Account_Datagrid.DataSource = dt;
            printtable.DataSource = printer;

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

         
            ///////////////////////// Level /////////////////////////


            DataGridViewImageColumn AccountLvl = new DataGridViewImageColumn();
            Account_Datagrid.Columns.Add(AccountLvl);
            AccountLvl.HeaderText = "Account Level";
            AccountLvl.Name = "Account Level";
            AccountLvl.ImageLayout = DataGridViewImageCellLayout.Zoom;
            AccountLvl.Image = Properties.Resources.loading;


            dataview();
            printdata();

            try
            {
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
            catch
            {

            }
          
        }

        public async void dataview()
        {
           
            //visual

            Account_Datagrid.Columns[5].Visible = false;
            Account_Datagrid.Columns[9].DisplayIndex = 5;

            DataGridViewColumn column7 = Account_Datagrid.Columns[7];
            column7.Width = 70;
            DataGridViewColumn column8 = Account_Datagrid.Columns[8];
            column8.Width = 70;

            Account_Datagrid.Columns[9].DefaultCellStyle.Padding = new Padding(0, 0, 80, 0);

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


                        StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.adminlvl, NCR_SYSTEM_1.Properties.Resources.managerlvl, NCR_SYSTEM_1.Properties.Resources.cashierlvl };





                        if (row.Cells[5].Value.Equals("Admin")) //Authorize inventory
                        {
                            row.Cells[9].Value = StatusImgs[0];
                        }
   

                        if (row.Cells[5].Value.Equals("Manager")) //Authorize PoS
                        {
                            row.Cells[9].Value = StatusImgs[1];
                        }
 

                        if (row.Cells[5].Value.Equals("Cashier")) //Authorize Supplier
                        {
                            row.Cells[9].Value = StatusImgs[2];
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

        public async void printdata()
        {

            printer.Rows.Clear();
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

                    DataRow r = printer.NewRow();
                    r["User ID"] = user.User_ID;
                    r["Username"] = user.Username;
                    r["Firstname"] = user.Firstname;
                    r["Lastname"] = user.Lastname;
                    r["Account Level"] = user.Account_Level;
                    r["Date Added"] = user.Date_Added;



                    printer.Rows.Add(r);
                }

                catch
                {

                }
            }
        }

        private void Account_Datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(Form1.status=="true")
            {
                string columnindex = "";

                try
                {
                    //delete
                    if (e.ColumnIndex == Account_Datagrid.Columns[8].Index)
                    {
                        if(Account_Datagrid.Rows[e.RowIndex].Cells[5].Value.ToString() != "Admin")
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

                                if (Account_Datagrid.Rows[e.RowIndex].Cells[5].Value.ToString().Equals("Cashier") || Account_Datagrid.Rows[e.RowIndex].Cells[5].Value.ToString().Equals("Manager"))
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
                        else
                        {
                            MessageBox.Show("Admin Level Account cannot be archived.");
                        }
                        
                    }


                    //update
                    if (e.ColumnIndex == Account_Datagrid.Columns[7].Index)
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
            Account_Datagrid.Columns[9].DisplayIndex = 5;

            DataGridViewColumn column7 = Account_Datagrid.Columns[7];
            column7.Width = 70;
            DataGridViewColumn column8 = Account_Datagrid.Columns[8];
            column8.Width = 70;

            Account_Datagrid.Columns[9].DefaultCellStyle.Padding = new Padding(0, 0, 80, 0);

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


                        StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.adminlvl, NCR_SYSTEM_1.Properties.Resources.managerlvl, NCR_SYSTEM_1.Properties.Resources.cashierlvl };





                        if (row.Cells[5].Value.Equals("Admin")) //Authorize inventory
                        {
                            row.Cells[9].Value = StatusImgs[0];
                        }


                        if (row.Cells[5].Value.Equals("Manager")) //Authorize PoS
                        {
                            row.Cells[9].Value = StatusImgs[1];
                        }


                        if (row.Cells[5].Value.Equals("Cashier")) //Authorize Supplier
                        {
                            row.Cells[9].Value = StatusImgs[2];
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
            else if (Form1.levelac.Equals("Cashier")  && Form1.status == "true")
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
  
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
     
            
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

        private void Account_Datagrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void Account_Datagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.Value != null)
            {
                e.Value = new String('*', e.Value.ToString().Length);
            }
        }

        private void pdfprint_Click(object sender, EventArgs e)
        {
            Document doc = new Document(new iTextSharp.text.Rectangle(288f, 144f), 10, 10, 10, 10);
            doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            string header = "NCR Gravel and Sand Enterprises";
            var _pdf_table = new PdfPTable(2);
            PdfPCell hc = new PdfPCell();


            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(("My print.pdf"), FileMode.Create));
            doc.Open();
            Paragraph space1 = new Paragraph("\n");

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

            iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 20, 1, BaseColor.BLACK);

            Paragraph prgheading = new Paragraph();
            prgheading.Alignment = Element.ALIGN_CENTER;
            prgheading.Add(new Chunk(header, times));
            doc.Add(space1);
            doc.Add(prgheading);
            doc.Add(space1);

            _pdf_table = new PdfPTable(printtable.Columns.Count);

            for (int j = 0; j < printtable.Columns.Count; j++)
            {
                _pdf_table.AddCell(new Phrase(printtable.Columns[j].HeaderText));
            }

            _pdf_table.HeaderRows = 1;

            for (int i = 0; i < printtable.Rows.Count; i++)
            {

                for (int k = 0; k < printtable.Columns.Count; k++)
                {

                    if (printtable[k, i].Value != null)
                    {

                        _pdf_table.AddCell(new Phrase(printtable[k, i].Value.ToString()));
                    }

                }

            }
            doc.Add(_pdf_table);

            doc.Close();
            //change to your directory
            System.Diagnostics.Process.Start(@"My print.pdf");
        }

        public void close()
        {
            this.Hide();
        }
    }
}
