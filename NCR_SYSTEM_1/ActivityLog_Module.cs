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

        private Image[] StatusImgs;

        public ActivityLog_Module()
        {
            InitializeComponent();
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
            dt.Columns.Add("Account lvl txt");

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
            column1.Width = 210;

            ActivityLog_Datagrid.Columns[7].DefaultCellStyle.Padding = new Padding(0, 0, 190, 0);

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
                    r["Account lvl txt"] = act.Accountlvl;


                 

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
        }


        }
}
