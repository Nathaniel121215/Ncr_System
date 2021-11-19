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
    public partial class ActivityLog_Module : Form
    {
        IFirebaseClient client;


        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"


        };

        DataTable dt = new DataTable();

        int supressor = 1;

        private Image[] StatusImgs;

        public static ActivityLog_Module _instance;

        public static string checker = "";

        public ActivityLog_Module()
        {
            InitializeComponent();
            _instance = this;
        }

        private void ActivityLog_Module_Load(object sender, EventArgs e)
        {
            datedisplay.Text = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            datedisplay.Select();
            this.ActivityLog_Datagrid.AllowUserToAddRows = false;


            client = new FireSharp.FirebaseClient(config);

            dt.Columns.Add("Event ID");
            dt.Columns.Add("Area/Module");
            dt.Columns.Add("Action/Activity");
            dt.Columns.Add("Date");
            dt.Columns.Add("User");
            dt.Columns.Add("Account Level");

            dt.Columns.Add("Date Searcher");




            ActivityLog_Datagrid.DataSource = dt;


            DataGridViewImageColumn AccountLvl = new DataGridViewImageColumn();
            ActivityLog_Datagrid.Columns.Add(AccountLvl);
            AccountLvl.HeaderText = "Account Level";
            AccountLvl.Name = "Account Level";
            AccountLvl.ImageLayout = DataGridViewImageCellLayout.Zoom;
            AccountLvl.Image = Properties.Resources.loading;





            dataview();
        }

        public async void dataview()
        {
            foreach (DataGridViewColumn column in ActivityLog_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            ActivityLog_Datagrid.Columns[6].Visible = false;
            ActivityLog_Datagrid.Columns[5].Visible = false;

            DataGridViewColumn column0 = ActivityLog_Datagrid.Columns[0];
            column0.Width = 110;

            DataGridViewColumn column1 = ActivityLog_Datagrid.Columns[1];
            column1.Width = 270;

            DataGridViewColumn column2 = ActivityLog_Datagrid.Columns[2];
            column2.Width = 340;

            ActivityLog_Datagrid.Columns[7].DefaultCellStyle.Padding = new Padding(0, 0, 150, 0);

            dt.Rows.Clear();

            int i = 0;
            FirebaseResponse resp = await client.GetTaskAsync("ActivityLogCounter/node");
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
                    FirebaseResponse resp1 = await client.GetTaskAsync("ActivityLog/" + i);
                    ActivityLog_Class act = resp1.ResultAs<ActivityLog_Class>();

                    DataRow r = dt.NewRow();

                    r["Event ID"] = act.Event_ID;
                    r["Area/Module"] = act.Module;
                    r["Action/Activity"] = act.Action;
                    r["Date"] = act.Date;
                    r["User"] = act.User;
                    r["Account Level"] = act.Accountlvl;


                 

                    DateTime date = Convert.ToDateTime(act.Date);




                    r["Date Searcher"] = date.ToString("MM/dd/yyyy");


                    dt.Rows.Add(r);

                
                }

                catch
                {

                }

                try
                {


                    foreach (DataGridViewRow row in ActivityLog_Datagrid.Rows)
                    {

                        try
                        {


                            StatusImgs = new Image[] {NCR_SYSTEM_1.Properties.Resources.Group_179, NCR_SYSTEM_1.Properties.Resources.Group_181 };

                            if (row.Cells[5].Value.Equals("Admin")) //Authorize Records
                            {
                                row.Cells[7].Value = StatusImgs[1];
                            }
                            else
                            {
                                row.Cells[7].Value = StatusImgs[0];
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
            totalcount = ActivityLog_Datagrid.Rows.Count;

            TransactionCounttxt.Text = totalcount.ToString();


        }

        private void searchtxt_KeyUp(object sender, KeyEventArgs e)
        {

            if (searchtxt.Text == "" && supressor == 1)
            {
                supressor = 0;


                ActivityLog_Datagrid.DataSource = null;
                ActivityLog_Datagrid.Rows.Clear();
                ActivityLog_Datagrid.Columns.Clear();
                ActivityLog_Datagrid.DataSource = dt;

                DataGridViewImageColumn AccountLvl = new DataGridViewImageColumn();
                ActivityLog_Datagrid.Columns.Add(AccountLvl);
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

            ActivityLog_Datagrid.DataSource = null;
            ActivityLog_Datagrid.Rows.Clear();
            ActivityLog_Datagrid.Columns.Clear();
            ActivityLog_Datagrid.DataSource = dv;

            DataGridViewImageColumn AccountLvl = new DataGridViewImageColumn();
            ActivityLog_Datagrid.Columns.Add(AccountLvl);
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
            foreach (DataGridViewColumn column in ActivityLog_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            ActivityLog_Datagrid.Columns[6].Visible = false;
            ActivityLog_Datagrid.Columns[5].Visible = false;

            DataGridViewColumn column0 = ActivityLog_Datagrid.Columns[0];
            column0.Width = 110;

            DataGridViewColumn column1 = ActivityLog_Datagrid.Columns[1];
            column1.Width = 270;

            DataGridViewColumn column2 = ActivityLog_Datagrid.Columns[2];
            column2.Width = 340;

            ActivityLog_Datagrid.Columns[7].DefaultCellStyle.Padding = new Padding(0, 0, 150, 0);

            try
            {


                foreach (DataGridViewRow row in ActivityLog_Datagrid.Rows)
                {

                    try
                    {


                        StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.Group_179, NCR_SYSTEM_1.Properties.Resources.Group_181 };

                        if (row.Cells[5].Value.Equals("Admin")) //Authorize Records
                        {
                            row.Cells[7].Value = StatusImgs[1];
                        }
                        else
                        {
                            row.Cells[7].Value = StatusImgs[0];
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

        private void bunifuImageButton19_Click(object sender, EventArgs e)
        {
            Dashboard_Module c = new Dashboard_Module();
            c.Show();
            this.Hide();
        }



        public void filter()
        {

            DataView dv = new DataView(dt);
            string date1 = ActivityLog_Filter_popup.startdate;
            string date2 = ActivityLog_Filter_popup.enddate;
            string user = ActivityLog_Filter_popup.user;


            if (InventoryArchive_Filter_popup.user == "")
            {

                dv.RowFilter = "[Date Searcher]  >='" + date1 + "'AND [Date Searcher] <='" + date2 + "'";

                ActivityLog_Datagrid.DataSource = null;
                ActivityLog_Datagrid.Rows.Clear();
                ActivityLog_Datagrid.Columns.Clear();
                ActivityLog_Datagrid.DataSource = dv;



            }


            else
            {
                dv.RowFilter = "[Date Searcher]  >='" + date1 + "'AND [Date Searcher] <='" + date2 + "'" + " AND [User] LIKE '%" + user + "%'";

                ActivityLog_Datagrid.DataSource = null;
                ActivityLog_Datagrid.Rows.Clear();
                ActivityLog_Datagrid.Columns.Clear();
                ActivityLog_Datagrid.DataSource = dv;


            }



            

            DataGridViewImageColumn AccountLvl = new DataGridViewImageColumn();
            ActivityLog_Datagrid.Columns.Add(AccountLvl);
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
                ActivityLog_Filter_popup a = new ActivityLog_Filter_popup();
                a.Show();

                checker = "dontallow";
            }
            else
            {
                MessageBox.Show("The tab is currently already open.");
            }
        }
    }
}
