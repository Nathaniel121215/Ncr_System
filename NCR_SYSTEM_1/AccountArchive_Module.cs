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
        private Image[] StatusImgs;

        DataTable dt = new DataTable();

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;



        public AccountArchive_Module()
        {
            InitializeComponent();
        }

        private void AccountArchive_Module_Load(object sender, EventArgs e)
        {
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

            Dataviewall();
        }

        public  async void Dataviewall()
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
                    FirebaseResponse resp1 = await client.GetTaskAsync("AccountArchive/" + i);
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
    }
}
