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

            ///////////////////////////////////////////////////////////

        }

        private void Account_Datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
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






                        dataview();

                    }

                    else

                    {
                        



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


                }


            }
            catch
            {

            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Addnewaccount_popup create = new Addnewaccount_popup();

            create.Show();
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
            Inventory_Module a = new Inventory_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton19_Click(object sender, EventArgs e)
        {
            Dashboard_Module a = new Dashboard_Module();
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

        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
    }
}
