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
    public partial class AccountArchive_Module : Form
    {
        public static string checker = "";

        int supressor = 1;

        public static string User_ID = "";
        public static string Username = "";
        public static string Password = "";
        public static string Firstname = "";
        public static string Lastname = "";
        public static string Account_Level = "";
        public static string Date_Added = "";


        private Image[] StatusImgs;

        DataTable dt = new DataTable();

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;


        public static AccountArchive_Module _instance;

        public AccountArchive_Module()
        {
            InitializeComponent();
            _instance = this;
        }

        private void AccountArchive_Module_Load(object sender, EventArgs e)
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

            dt.Columns.Add("Date Archived");
            dt.Columns.Add("User");

            dt.Columns.Add("Date Archived Searcher");




            Account_Datagrid.DataSource = dt;


            DataGridViewImageColumn View = new DataGridViewImageColumn();
            Account_Datagrid.Columns.Add(View);
            View.HeaderText = "";
            View.Name = "";
            View.ImageLayout = DataGridViewImageCellLayout.Zoom;
            View.Image = Properties.Resources.View_Icon;

            DataGridViewImageColumn Restore = new DataGridViewImageColumn();
            Account_Datagrid.Columns.Add(Restore);
            Restore.HeaderText = "";
            Restore.Name = "";
            Restore.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Restore.Image = Properties.Resources.Restore_Icon;

           

            ///////////////////////// Level /////////////////////////


            DataGridViewImageColumn AccountLvl = new DataGridViewImageColumn();
            Account_Datagrid.Columns.Add(AccountLvl);
            AccountLvl.HeaderText = "Account Level";
            AccountLvl.Name = "Account Level";
            AccountLvl.ImageLayout = DataGridViewImageCellLayout.Zoom;
            AccountLvl.Image = Properties.Resources.loading;

            Dataviewall();

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

        public  async void Dataviewall()
        {
            //visual


            Account_Datagrid.Columns[8].Visible = false;
            Account_Datagrid.Columns[5].Visible = false;
            Account_Datagrid.Columns[11].DisplayIndex = 5;

            DataGridViewColumn column10 = Account_Datagrid.Columns[10];
            column10.Width = 110;

            DataGridViewColumn column8 = Account_Datagrid.Columns[8];
            column8.Width = 90;
            DataGridViewColumn column9 = Account_Datagrid.Columns[9];
            column9.Width = 90;

            DataGridViewColumn column6 = Account_Datagrid.Columns[6];
            column6.Width = 200;

            DataGridViewColumn column7 = Account_Datagrid.Columns[7];
            column7.Width = 230;

            Account_Datagrid.Columns[11].DefaultCellStyle.Padding = new Padding(0, 0, 50, 0);

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
                    FirebaseResponse resp1 = await client.GetTaskAsync("AccountArchive/" + i);
                    AccountArchive_Class user = resp1.ResultAs<AccountArchive_Class>();

                    DataRow r = dt.NewRow();
                    r["User ID"] = user.User_ID;
                    r["Username"] = user.Username;
                    r["Password"] = user.Password;
                    r["Firstname"] = user.Firstname;
                    r["Lastname"] = user.Lastname;
                    r["Account Level"] = user.Account_Level;


                    r["Date Archived"] = user.Date_Archive;
                    r["User"] = user.User;

                    DateTime date = Convert.ToDateTime(user.Date_Archive);



  
                    r["Date Archived Searcher"] = date.ToString("MM/dd/yyyy");



                    dt.Rows.Add(r);
                    gettransactioncount();
                }

                catch
                {

                }

                gettransactioncount();
                checker = "allow";
            }


            foreach (DataGridViewRow row in Account_Datagrid.Rows)
            {

                try
                {

                    StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.Group_179, NCR_SYSTEM_1.Properties.Resources.Group_181 };


                    if (row.Cells[5].Value.Equals("Admin")) //Authorize Records
                    {
                        row.Cells[11].Value = StatusImgs[1];
                    }
                    else
                    {
                        row.Cells[11].Value = StatusImgs[0];
                    }


                }
                catch
                {

                }

            }
        }

        public void gettransactioncount()
        {
            int transactioncount = 0;
            transactioncount = Account_Datagrid.Rows.Count;

            TransactionCounttxt.Text = transactioncount.ToString();


        }



        private void Account_Datagrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Form1.status=="true")
            {
                //restore
                string columnindex = "";

                try
                {
                    if (e.ColumnIndex == Account_Datagrid.Columns[10].Index)
                    {
                        if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                        {
                            columnindex = Account_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();

                            FirebaseResponse resp1 = client.Get("AccountArchive/" + columnindex);
                            User_class obj1 = resp1.ResultAs<User_class>();

                            var data = new User_class
                            {
                                User_ID = obj1.User_ID,
                                Username = obj1.Username,
                                Password = obj1.Password,
                                Firstname = obj1.Firstname,
                                Lastname = obj1.Lastname,
                                Account_Level = obj1.Account_Level,
                                Date_Added = obj1.Date_Added,
                                Inventoryaccess = obj1.Inventoryaccess,
                                Posaccess = obj1.Posaccess,
                                Recordaccess = obj1.Recordaccess,
                                Supplieraccess = obj1.Supplieraccess,


                            };

                            FirebaseResponse response = client.Set("Accounts/" + data.User_ID, data);
                            User_class result = response.ResultAs<User_class>();


                            //existing employee

                            if (data.Account_Level == "Employee")
                            {
                                FirebaseResponse resp3 = client.Get("employeeCounterExisting/node");
                                Counter_class gett = resp3.ResultAs<Counter_class>();
                                int exist = (Convert.ToInt32(gett.cnt) + 1);
                                var obj2 = new Counter_class
                                {
                                    cnt = exist.ToString()
                                };


                                SetResponse response2 = client.Set("employeeCounterExisting/node", obj2);
                            }




                            //get archive counter
                            FirebaseResponse resp = client.Get("AccountArchiveCounter/node");
                            Counter_class get = resp.ResultAs<Counter_class>();

                            //update archive counter
                            var obj = new Counter_class
                            {
                                cnt = (Convert.ToInt32(get.cnt) - 1).ToString(),
                            };

                            SetResponse response4 = client.Set("AccountArchiveCounter/node", obj);



                            //delete from current table

                            FirebaseResponse response5 = client.Delete("AccountArchive/" + columnindex);




                            //ACCOUNT ARCHIVE RESTORE EVENT

                            FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                            Counter_class get4 = resp4.ResultAs<Counter_class>();
                            int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                            var data3 = new ActivityLog_Class
                            {
                                Event_ID = cnt4.ToString(),
                                Module = "Account Archive Module",
                                Action = "Account-ID: " + data.User_ID + "   Account Restored",
                                Date = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                                User = Form1.username,
                                Accountlvl = Form1.levelac,

                            };



                            FirebaseResponse response6 = client.Set("ActivityLog/" + data3.Event_ID, data3);



                            var obj4 = new Counter_class
                            {
                                cnt = data3.Event_ID

                            };

                            SetResponse response7 = client.Set("ActivityLogCounter/node", obj4);

                            Dataviewall();
                        
                    }
                        else
                        {

                        }
                    }

                    //view

                    if (e.ColumnIndex == Account_Datagrid.Columns[9].Index)
                    {

                        columnindex = Account_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();

                        FirebaseResponse resp1 = client.Get("AccountArchive/" + columnindex);
                        User_class obj1 = resp1.ResultAs<User_class>();



                        User_ID = obj1.User_ID;
                        Username = obj1.Username;
                        Password = obj1.Password;
                        Firstname = obj1.Firstname;
                        Lastname = obj1.Lastname;
                        Account_Level = obj1.Account_Level;




                        AccountArchiveView_popup c = new AccountArchiveView_popup();
                        c.Show();
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


                Account_Datagrid.DataSource = null;
                Account_Datagrid.Rows.Clear();
                Account_Datagrid.Columns.Clear();
                Account_Datagrid.DataSource = dt;


                DataGridViewImageColumn View = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(View);
                View.HeaderText = "";
                View.Name = "";
                View.ImageLayout = DataGridViewImageCellLayout.Zoom;
                View.Image = Properties.Resources.View_Icon;

                DataGridViewImageColumn Restore = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(Restore);
                Restore.HeaderText = "";
                Restore.Name = "";
                Restore.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Restore.Image = Properties.Resources.Restore_Icon;



                ///////////////////////// Level /////////////////////////


                DataGridViewImageColumn AccountLvl = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(AccountLvl);
                AccountLvl.HeaderText = "Account Level";
                AccountLvl.Name = "Account Level";
                AccountLvl.ImageLayout = DataGridViewImageCellLayout.Zoom;
                AccountLvl.Image = Properties.Resources.loading;



                Dataviewall();

                filterlabeltxt.Text = "";



            }

            if (searchtxt.Text != "")
            {
                supressor = 1;

            }


        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
            if (searchtxt.Text != "" & Form1.status == "true")
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "[" + combofilter.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

                Account_Datagrid.DataSource = null;
                Account_Datagrid.Rows.Clear();
                Account_Datagrid.Columns.Clear();
                Account_Datagrid.DataSource = dv;



                DataGridViewImageColumn View = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(View);
                View.HeaderText = "";
                View.Name = "";
                View.ImageLayout = DataGridViewImageCellLayout.Zoom;
                View.Image = Properties.Resources.View_Icon;

                DataGridViewImageColumn Restore = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(Restore);
                Restore.HeaderText = "";
                Restore.Name = "";
                Restore.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Restore.Image = Properties.Resources.Restore_Icon;



                ///////////////////////// Level /////////////////////////


                DataGridViewImageColumn AccountLvl = new DataGridViewImageColumn();
                Account_Datagrid.Columns.Add(AccountLvl);
                AccountLvl.HeaderText = "Account Level";
                AccountLvl.Name = "Account Level";
                AccountLvl.ImageLayout = DataGridViewImageCellLayout.Zoom;
                AccountLvl.Image = Properties.Resources.loading;


                gettransactioncount();

                filterlabeltxt.Text = "";

                searchupdate();
            }
            else
            {

            }
                


        }
        public void searchupdate()
        {
            //visual



            Account_Datagrid.Columns[8].Visible = false;
            Account_Datagrid.Columns[5].Visible = false;
            Account_Datagrid.Columns[11].DisplayIndex = 5;

            DataGridViewColumn column10 = Account_Datagrid.Columns[10];
            column10.Width = 110;

            DataGridViewColumn column8 = Account_Datagrid.Columns[8];
            column8.Width = 90;
            DataGridViewColumn column9 = Account_Datagrid.Columns[9];
            column9.Width = 90;

            DataGridViewColumn column6 = Account_Datagrid.Columns[6];
            column6.Width = 200;

            DataGridViewColumn column7 = Account_Datagrid.Columns[7];
            column7.Width = 230;

            Account_Datagrid.Columns[11].DefaultCellStyle.Padding = new Padding(0, 0, 50, 0);

            foreach (DataGridViewColumn column in Account_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            foreach (DataGridViewRow row in Account_Datagrid.Rows)
            {

                try
                {

                    StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.Group_179, NCR_SYSTEM_1.Properties.Resources.Group_181 };


                    if (row.Cells[5].Value.Equals("Admin")) //Authorize Records
                    {
                        row.Cells[11].Value = StatusImgs[1];
                    }
                    else
                    {
                        row.Cells[11].Value = StatusImgs[0];
                    }


                }
                catch
                {

                }

            }


        }
        public void filter()
        {
            DataView dv = new DataView(dt);
            string date1 = AccountArchive_Filter_popup.startdate;
            string date2 = AccountArchive_Filter_popup.enddate;
            string user = AccountArchive_Filter_popup.user;

            if (InventoryArchive_Filter_popup.user == "")
            {

                dv.RowFilter = "[Date Archived Searcher]  >='" + date1 + "'AND [Date Archived Searcher] <='" + date2 + "'";

                Account_Datagrid.DataSource = null;
                Account_Datagrid.Rows.Clear();
                Account_Datagrid.Columns.Clear();
                Account_Datagrid.DataSource = dv;

                

            }


            else
            {
                dv.RowFilter = "[Date Archived Searcher]  >='" + date1 + "'AND [Date Archived Searcher] <='" + date2 + "'" + " AND [User] LIKE '%" + user + "%'";

                Account_Datagrid.DataSource = null;
                Account_Datagrid.Rows.Clear();
                Account_Datagrid.Columns.Clear();
                Account_Datagrid.DataSource = dv;


            }

            DataGridViewImageColumn View = new DataGridViewImageColumn();
            Account_Datagrid.Columns.Add(View);
            View.HeaderText = "";
            View.Name = "";
            View.ImageLayout = DataGridViewImageCellLayout.Zoom;
            View.Image = Properties.Resources.View_Icon;

            DataGridViewImageColumn Restore = new DataGridViewImageColumn();
            Account_Datagrid.Columns.Add(Restore);
            Restore.HeaderText = "";
            Restore.Name = "";
            Restore.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Restore.Image = Properties.Resources.Restore_Icon;



            ///////////////////////// Level /////////////////////////


            DataGridViewImageColumn AccountLvl = new DataGridViewImageColumn();
            Account_Datagrid.Columns.Add(AccountLvl);
            AccountLvl.HeaderText = "Account Level";
            AccountLvl.Name = "Account Level";
            AccountLvl.ImageLayout = DataGridViewImageCellLayout.Zoom;
            AccountLvl.Image = Properties.Resources.loading;

            filterlabeltxt.Text = date1 + " to " + date2;

            searchupdate();
            gettransactioncount();

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            


            if (Form1.status=="true")
            {
                AccountArchive_Filter_popup c = new AccountArchive_Filter_popup();
                c.Show();

                Form1.status = "false";
            }
            else
            {
                MessageBox.Show("The Module is still loading or a window is currently open.");
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

        private void bunifuImageButton1_Click(object sender, EventArgs e)
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
            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    AccountArchive_Module a = new AccountArchive_Module();
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

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    SupplierManagementArchive_module a = new SupplierManagementArchive_module();
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

        private void searchtxt_Enter(object sender, EventArgs e)
        {
            searchtxt.Text = "";
        }

        private void searchtxt_Leave(object sender, EventArgs e)
        {
            if (searchtxt.Text == "")
            {
                searchtxt.Text = "Type here to filter Account Archive Content";
            }
            else
            {

            }
        }

        private void bunifuImageButton11_Click(object sender, EventArgs e)
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
    }
}
