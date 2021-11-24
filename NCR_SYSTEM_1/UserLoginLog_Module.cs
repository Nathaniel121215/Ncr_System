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
    public partial class UserLoginLog_Module : Form
    {
        public UserLoginLog_Module()
        {
            InitializeComponent();
            _instance = this;
        }

        IFirebaseClient client;


        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"


        };

        DataTable dt = new DataTable();

        int supressor = 1;

        private Image[] StatusImgs;

        public static UserLoginLog_Module _instance;

        public static string checker = "";


        private void UserLoginLog_Module_Load(object sender, EventArgs e)
        {
            datedisplay.Text = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            datedisplay.Select();
            this.UserLoginLog_Datagrid.AllowUserToAddRows = false;


            client = new FireSharp.FirebaseClient(config);

            dt.Columns.Add("Event ID");
            dt.Columns.Add("Date");
            dt.Columns.Add("Time in");
            dt.Columns.Add("Time out");
            dt.Columns.Add("User");
            dt.Columns.Add("Account Level");



            UserLoginLog_Datagrid.DataSource = dt;


            DataGridViewImageColumn AccountLvl = new DataGridViewImageColumn();
            UserLoginLog_Datagrid.Columns.Add(AccountLvl);
            AccountLvl.HeaderText = "Account Level";
            AccountLvl.Name = "Account Level";
            AccountLvl.ImageLayout = DataGridViewImageCellLayout.Zoom;
            AccountLvl.Image = Properties.Resources.loading;





            dataview();
        }

        public async void  dataview()
        {
            foreach (DataGridViewColumn column in UserLoginLog_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            UserLoginLog_Datagrid.Columns[5].Visible = false;

            UserLoginLog_Datagrid.Columns[6].DefaultCellStyle.Padding = new Padding(0, 0, 150, 0);

            dt.Rows.Clear();

            int i = 0;
            FirebaseResponse resp = await client.GetTaskAsync("UserLoginLogCounter/node");
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
                    FirebaseResponse resp1 = await client.GetTaskAsync("UserLoginLog/" + i);
                    LoginLog_Class act = resp1.ResultAs<LoginLog_Class>();

                    DataRow r = dt.NewRow();

                    DateTime date = Convert.ToDateTime(act.Date);




                    r["Event ID"] = act.Event_ID;
                    r["Date"] = date.ToString("MM/dd/yyyy");
                    r["Time in"] = act.Timein;
                    r["Time out"] = act.Timeout;
                    r["User"] = act.User;
                    r["Account Level"] = act.Accountlvl;


                    dt.Rows.Add(r);


                }

                catch
                {

                }

                try
                {


                    foreach (DataGridViewRow row in UserLoginLog_Datagrid.Rows)
                    {

                        try
                        {


                            StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.Group_179, NCR_SYSTEM_1.Properties.Resources.Group_181 };

                            if (row.Cells[5].Value.Equals("Admin")) //Authorize Records
                            {
                                row.Cells[6].Value = StatusImgs[1];
                            }
                            else
                            {
                                row.Cells[6].Value = StatusImgs[0];
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
            gettotalcount();
            checker = "allow";
        }

        public void gettotalcount()
        {
            int totalcount = 0;
            totalcount = UserLoginLog_Datagrid.Rows.Count;

            TransactionCounttxt.Text = totalcount.ToString();


        }

        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {
            //TIMEOUT LOG

            try
            {

                FirebaseResponse resp10 = client.Get("UserLoginLogCounter/node");
                Counter_class get10 = resp10.ResultAs<Counter_class>();
                int cnt10 = (Convert.ToInt32(get10.cnt));


                var data10 = new Timeout_Class
                {
                    Event_ID = cnt10.ToString(),
                    Timeout = DateTime.Now.ToString("hh:mm tt"),
                };

                FirebaseResponse response10 = client.Update("UserLoginLog/" + data10.Event_ID, data10);


            }

            catch (Exception b)
            {
                MessageBox.Show(b.ToString());
            }


            Application.Exit();
        }

        private void searchtxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (searchtxt.Text == "" && supressor == 1)
            {
                supressor = 0;


                UserLoginLog_Datagrid.DataSource = null;
                UserLoginLog_Datagrid.Rows.Clear();
                UserLoginLog_Datagrid.Columns.Clear();
                UserLoginLog_Datagrid.DataSource = dt;

                DataGridViewImageColumn AccountLvl = new DataGridViewImageColumn();
                UserLoginLog_Datagrid.Columns.Add(AccountLvl);
                AccountLvl.HeaderText = "Account Level";
                AccountLvl.Name = "Account Level";
                AccountLvl.ImageLayout = DataGridViewImageCellLayout.Zoom;
                AccountLvl.Image = Properties.Resources.loading;



                dataview();

                filterlabeltxt.Text = "";



            }

            if (searchtxt.Text != "")
            {
                supressor = 1;

            }
        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = "[" + combofilter.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

            UserLoginLog_Datagrid.DataSource = null;
            UserLoginLog_Datagrid.Rows.Clear();
            UserLoginLog_Datagrid.Columns.Clear();
            UserLoginLog_Datagrid.DataSource = dv;

            DataGridViewImageColumn AccountLvl = new DataGridViewImageColumn();
            UserLoginLog_Datagrid.Columns.Add(AccountLvl);
            AccountLvl.HeaderText = "Account Level";
            AccountLvl.Name = "Account Level";
            AccountLvl.ImageLayout = DataGridViewImageCellLayout.Zoom;
            AccountLvl.Image = Properties.Resources.loading;




            gettotalcount();

            filterlabeltxt.Text = "";

            searchupdate();
        }

        public void searchupdate()
        {
            foreach (DataGridViewColumn column in UserLoginLog_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            UserLoginLog_Datagrid.Columns[5].Visible = false;

            UserLoginLog_Datagrid.Columns[6].DefaultCellStyle.Padding = new Padding(0, 0, 150, 0);



            try
            {


                foreach (DataGridViewRow row in UserLoginLog_Datagrid.Rows)
                {

                    try
                    {


                        StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.Group_179, NCR_SYSTEM_1.Properties.Resources.Group_181 };

                        if (row.Cells[5].Value.Equals("Admin")) //Authorize Records
                        {
                            row.Cells[6].Value = StatusImgs[1];
                        }
                        else
                        {
                            row.Cells[6].Value = StatusImgs[0];
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

        public void filter()
        {
            DataView dv = new DataView(dt);
            string date1 = UserLoginLogFilter_popup.startdate;
            string date2 = UserLoginLogFilter_popup.enddate;
            string user = UserLoginLogFilter_popup.user;

            if (InventoryArchive_Filter_popup.user == "")
            {

                dv.RowFilter = "[Date]  >='" + date1 + "'AND [Date] <='" + date2 + "'";

                UserLoginLog_Datagrid.DataSource = null;
                UserLoginLog_Datagrid.Rows.Clear();
                UserLoginLog_Datagrid.Columns.Clear();
                UserLoginLog_Datagrid.DataSource = dv;



            }


            else
            {
                dv.RowFilter = "[Date]  >='" + date1 + "'AND [Date] <='" + date2 + "'" + " AND [User] LIKE '%" + user + "%'";

                UserLoginLog_Datagrid.DataSource = null;
                UserLoginLog_Datagrid.Rows.Clear();
                UserLoginLog_Datagrid.Columns.Clear();
                UserLoginLog_Datagrid.DataSource = dv;


            }
            

            DataGridViewImageColumn AccountLvl = new DataGridViewImageColumn();
            UserLoginLog_Datagrid.Columns.Add(AccountLvl);
            AccountLvl.HeaderText = "Account Level";
            AccountLvl.Name = "Account Level";
            AccountLvl.ImageLayout = DataGridViewImageCellLayout.Zoom;
            AccountLvl.Image = Properties.Resources.loading;


            filterlabeltxt.Text = date1 + " to " + date2;

            searchupdate();
            gettotalcount();

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (checker.Equals("allow"))
            {
                UserLoginLogFilter_popup a = new UserLoginLogFilter_popup();
                a.Show();

                checker = "dontallow";
            }
            else
            {
                MessageBox.Show("The tab is currently already open.");
            }
        }

        private void bunifuImageButton19_Click(object sender, EventArgs e)
        {

        }
    }
}
