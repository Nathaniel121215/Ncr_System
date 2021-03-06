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

        DataTable printer = new DataTable();

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


            printer.Columns.Add("Event ID");
            printer.Columns.Add("Date");
            printer.Columns.Add("Time in");
            printer.Columns.Add("Time out");
            printer.Columns.Add("User");
            printer.Columns.Add("Account Level");


            UserLoginLog_Datagrid.DataSource = dt;
            printtable.DataSource = printer;


            DataGridViewImageColumn AccountLvl = new DataGridViewImageColumn();
            UserLoginLog_Datagrid.Columns.Add(AccountLvl);
            AccountLvl.HeaderText = "Account Level";
            AccountLvl.Name = "Account Level";
            AccountLvl.ImageLayout = DataGridViewImageCellLayout.Zoom;
            AccountLvl.Image = Properties.Resources.loading;





            dataview();
            printdata();

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

        public async void printdata()
        {
            printer.Rows.Clear();

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

                    DataRow r = printer.NewRow();

                    DateTime date = Convert.ToDateTime(act.Date);




                    r["Event ID"] = act.Event_ID;
                    r["Date"] = date.ToString("MM/dd/yyyy");
                    r["Time in"] = act.Timein;
                    r["Time out"] = act.Timeout;
                    r["User"] = act.User;
                    r["Account Level"] = act.Accountlvl;


                    printer.Rows.Add(r);


                }

                catch
                {

                }
            }
        }

        public void printfilter()
        {

            DataView dv = new DataView(printer);
            string date1 = UserLoginLogFilter_popup.startdate;
            string date2 = UserLoginLogFilter_popup.enddate;
            string user = UserLoginLogFilter_popup.user;

            if (InventoryArchive_Filter_popup.user == "")
            {

                dv.RowFilter = "[Date]  >='" + date1 + "'AND [Date] <='" + date2 + "'";

                printtable.DataSource = null;
                printtable.Rows.Clear();
                printtable.Columns.Clear();
                printtable.DataSource = dv;



            }


            else
            {
                dv.RowFilter = "[Date]  >='" + date1 + "'AND [Date] <='" + date2 + "'" + " AND [User] LIKE '%" + user + "%'";

                printtable.DataSource = null;
                printtable.Rows.Clear();
                printtable.Columns.Clear();
                printtable.DataSource = dv;


            }

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


                            StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.adminlvl, NCR_SYSTEM_1.Properties.Resources.managerlvl, NCR_SYSTEM_1.Properties.Resources.cashierlvl };

                            if (row.Cells[5].Value.Equals("Admin")) //Authorize Records
                            {
                                row.Cells[6].Value = StatusImgs[0];
                            }
                            if (row.Cells[5].Value.Equals("Manager")) //Authorize Records
                            {
                                row.Cells[6].Value = StatusImgs[1];
                            }
                            if (row.Cells[5].Value.Equals("Cashier")) //Authorize Records
                            {
                                row.Cells[6].Value = StatusImgs[2];
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
            if (searchtxt.Text != "" & Form1.status == "true")
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
            else
            {

            }
                
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


                        StatusImgs = new Image[] { NCR_SYSTEM_1.Properties.Resources.adminlvl, NCR_SYSTEM_1.Properties.Resources.managerlvl, NCR_SYSTEM_1.Properties.Resources.cashierlvl };

                        if (row.Cells[5].Value.Equals("Admin")) //Authorize Records
                        {
                            row.Cells[6].Value = StatusImgs[0];
                        }
                        if (row.Cells[5].Value.Equals("Manager")) //Authorize Records
                        {
                            row.Cells[6].Value = StatusImgs[1];
                        }
                        if (row.Cells[5].Value.Equals("Cashier")) //Authorize Records
                        {
                            row.Cells[6].Value = StatusImgs[2];
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
            if (Form1.status=="true")
            {
                UserLoginLogFilter_popup a = new UserLoginLogFilter_popup();
                a.Show();

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

        private void bunifuImageButton9_Click_1(object sender, EventArgs e)
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
            else if (Form1.levelac.Equals("Cashier") && Form1.status == "true")
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

        private void bunifuImageButton14_Click(object sender, EventArgs e)
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

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (Form1.status == "true" && (Form1.levelac.Equals("Admin") || Form1.levelac.Equals("Manager")))
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

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
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
            UserLoginLog_Datagrid.SelectAll();
            DataObject dataObj = UserLoginLog_Datagrid.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void searchtxt_Enter(object sender, EventArgs e)
        {
            searchtxt.Text = "";
        }

        private void searchtxt_Leave(object sender, EventArgs e)
        {
            if (searchtxt.Text == "")
            {
                searchtxt.Text = "Type here to filter User Login Log Content";
            }
            else
            {

            }
        }

        private void UserLoginLog_Datagrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
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
    }
}
