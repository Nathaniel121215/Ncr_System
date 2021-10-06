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



        public AccountArchive_Module()
        {
            InitializeComponent();
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
        }

        public  async void Dataviewall()
        {
            //visual



            Account_Datagrid.Columns[5].Visible = false;
            Account_Datagrid.Columns[10].DisplayIndex = 5;

            DataGridViewColumn column10 = Account_Datagrid.Columns[10];
            column10.Width = 110;

            DataGridViewColumn column8 = Account_Datagrid.Columns[8];
            column8.Width = 90;
            DataGridViewColumn column9 = Account_Datagrid.Columns[9];
            column9.Width = 90;

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





                    dt.Rows.Add(r);
                    gettransactioncount();
                }

                catch
                {

                }

                gettransactioncount();
            }


            foreach (DataGridViewRow row in Account_Datagrid.Rows)
            {

                try
                {

                    StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.Group_179, NCR_SYSTEM_1.Properties.Resources.Group_181 };


                    if (row.Cells[5].Value.Equals("Admin")) //Authorize Records
                    {
                        row.Cells[10].Value = StatusImgs[1];
                    }
                    else
                    {
                        row.Cells[10].Value = StatusImgs[0];
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
            //restore
            string columnindex="";

            try
            {
                if (e.ColumnIndex == Account_Datagrid.Columns[9].Index)
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

                    if(data.Account_Level== "Employee")
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

                    Dataviewall();
                }

                //view

                if (e.ColumnIndex == Account_Datagrid.Columns[8].Index)
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


                }





            }
            catch
            {

            }
        }
    }
}
