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


                            StatusImgs = new Image[] {NCR_SYSTEM_1.Properties.Resources.adminlvl, NCR_SYSTEM_1.Properties.Resources.managerlvl, NCR_SYSTEM_1.Properties.Resources.cashierlvl };

                            if (row.Cells[5].Value.Equals("Admin")) //Authorize Records
                            {
                                row.Cells[7].Value = StatusImgs[0];
                            }
                            if (row.Cells[5].Value.Equals("Manager")) //Authorize Records
                            {
                                row.Cells[7].Value = StatusImgs[1];
                            }
                            if (row.Cells[5].Value.Equals("Cashier")) //Authorize Records
                            {
                                row.Cells[7].Value = StatusImgs[2];
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
            if (searchtxt.Text != "" & Form1.status == "true")
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
            else
            {

            }
            
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



        public void filter()
        {

            DataView dv = new DataView(dt);
            string date1 = ActivityLog_Filter_popup.startdate;
            string date2 = ActivityLog_Filter_popup.enddate;
            string user = ActivityLog_Filter_popup.user;
            string module = ActivityLog_Filter_popup.module;

          
            if (ActivityLog_Filter_popup.user == "" && ActivityLog_Filter_popup.module != "" && ActivityLog_Filter_popup.module == "All")
            {

                dv.RowFilter = "[Date Searcher]  >='" + date1 + "'AND [Date Searcher] <='" + date2 + "'";

                ActivityLog_Datagrid.DataSource = null;
                ActivityLog_Datagrid.Rows.Clear();
                ActivityLog_Datagrid.Columns.Clear();
                ActivityLog_Datagrid.DataSource = dv;



            }


            else if (ActivityLog_Filter_popup.user != "" && ActivityLog_Filter_popup.module != "" && ActivityLog_Filter_popup.module != "All")
            {

                dv.RowFilter = "[Date Searcher]  >='" + date1 + "'AND [Date Searcher] <='" + date2 + "' " + " AND [Area/Module] LIKE '%" + module + "%'" + " AND [User] LIKE '%" + user + "%'";


                ActivityLog_Datagrid.DataSource = null;
                ActivityLog_Datagrid.Rows.Clear();
                ActivityLog_Datagrid.Columns.Clear();
                ActivityLog_Datagrid.DataSource = dv;
            }
            else if (ActivityLog_Filter_popup.user != "" && ActivityLog_Filter_popup.module != "" && ActivityLog_Filter_popup.module == "All")
            {
                dv.RowFilter = "[Date Searcher]  >='" + date1 + "'AND [Date Searcher] <='" + date2 + "' " + " AND [User] LIKE '%" + user + "%'";

                ActivityLog_Datagrid.DataSource = null;
                ActivityLog_Datagrid.Rows.Clear();
                ActivityLog_Datagrid.Columns.Clear();
                ActivityLog_Datagrid.DataSource = dv;
            }
            else if (ActivityLog_Filter_popup.user == "" && ActivityLog_Filter_popup.module != "" && ActivityLog_Filter_popup.module != "All")
            {

                dv.RowFilter = "[Date Searcher]  >='" + date1 + "'AND [Date Searcher] <='" + date2 + "' " + " AND [Area/Module] LIKE '%" + module + "%'";

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
            if (Form1.status=="true")
            {
                ActivityLog_Filter_popup a = new ActivityLog_Filter_popup();
                a.Show();

                Form1.status = "false";
            }
            else
            {
                MessageBox.Show("The Module is still loading or a window is currently open.");
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

        private void bunifuImageButton14_Click(object sender, EventArgs e)
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

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
            {

                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    UserLoginLog_Module a = new UserLoginLog_Module();
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

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void searchtxt_Enter(object sender, EventArgs e)
        {
            searchtxt.Text = "";
        }

        private void searchtxt_Leave(object sender, EventArgs e)
        {
            if (searchtxt.Text == "")
            {
                searchtxt.Text = "Type here to filter Activity Log Content";
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

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            if (Form1.levelac.Equals("Admin") && Form1.status == "true")
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

        private void copyAlltoClipboard()
        {
            ActivityLog_Datagrid.SelectAll();
            DataObject dataObj = ActivityLog_Datagrid.GetClipboardContent();
            if (dataObj != null)
            Clipboard.SetDataObject(dataObj);
        }

        private void ActivityLog_Datagrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
