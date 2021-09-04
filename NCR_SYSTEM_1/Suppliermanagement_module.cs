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
    public partial class Suppliermanagement_module : Form
    {
        int supressor = 1;

        public static string Supplier_ID = "";
        public static string Supplier_Name = "";
        public static string Supplier_Address = "";
        public static string Supplier_Number = "";
        public static string Last_Transaction = "";
        public static string Date_Added = "";
  


        DataTable dt = new DataTable();

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public static Suppliermanagement_module _instance;

        public Suppliermanagement_module()
        {
            _instance = this;
            InitializeComponent();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Addsupplier_popup a = new Addsupplier_popup();

            a.Show();
        }

        private void Suppliermanagement_module_Load(object sender, EventArgs e)
        {
            this.Supplier_Datagrid.AllowUserToAddRows = false;

            client = new FireSharp.FirebaseClient(config);

            dt.Columns.Add("Supplier ID");
            dt.Columns.Add("Supplier Name");
            dt.Columns.Add("Supplier Address");
            dt.Columns.Add("Supplier Number");
            dt.Columns.Add("Last Transaction");
            dt.Columns.Add("Date Added");
 



            Supplier_Datagrid.DataSource = dt;


            DataGridViewImageColumn update = new DataGridViewImageColumn();
            Supplier_Datagrid.Columns.Add(update);
            update.HeaderText = "";
            update.Name = "update";
            update.ImageLayout = DataGridViewImageCellLayout.Zoom;
            update.Image = Properties.Resources.Update_Icon;


            DataGridViewImageColumn Archive = new DataGridViewImageColumn();
            Supplier_Datagrid.Columns.Add(Archive);
            Archive.HeaderText = "";
            Archive.Name = "Archive";
            Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Archive.Image = Properties.Resources.Archive_Icon;


            dataview();
        }

        public async void dataview()
        {

            foreach (DataGridViewColumn column in Supplier_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //visual
            DataGridViewColumn column1 = Supplier_Datagrid.Columns[1];
            column1.Width = 210;

            DataGridViewColumn column2 = Supplier_Datagrid.Columns[2];
            column2.Width = 270;

            DataGridViewColumn column3 = Supplier_Datagrid.Columns[3];
            column3.Width = 220;

            DataGridViewColumn column4 = Supplier_Datagrid.Columns[4];
            column4.Width = 170;

            DataGridViewColumn column5 = Supplier_Datagrid.Columns[5];
            column5.Width = 170;




            dt.Rows.Clear();
            int i = 0;
            FirebaseResponse resp = await client.GetTaskAsync("SupplierCounter/node");
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
                    FirebaseResponse resp1 = await client.GetTaskAsync("Supplier/" + i);
                    Supplier_Class user = resp1.ResultAs<Supplier_Class>();

                 
                    DataRow r = dt.NewRow();

                    r["Supplier ID"] = user.Supplier_ID;
                    r["Supplier Name"] = user.Supplier_Name;
                    r["Supplier Address"] = user.Supplier_Address;
                    r["Supplier Number"] = user.Supplier_Number;
                    r["Last Transaction"] = user.Supplier_LastTransaction;

                    if(user.Supplier_LastTransaction==null)
                    {
                        r["Last Transaction"] = "None";
                    }
                    r["Date Added"] = user.Supplier_DateAdded;
               



                    dt.Rows.Add(r);
                }

                catch
                {

                }


            }


        }

        private void Supplier_Datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnindex = "";

            try
            {
                //delete
                if (e.ColumnIndex == Supplier_Datagrid.Columns[7].Index)
                {





                    if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                    {

                        columnindex = Supplier_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();

                        FirebaseResponse response = client.Delete("Supplier/" + columnindex);


                       
                            FirebaseResponse resp2 = client.Get("SupplierCounterExisting/node");
                            Counter_class get2 = resp2.ResultAs<Counter_class>();
                            string employee = (Convert.ToInt32(get2.cnt) - 1).ToString();
                            var obj2 = new Counter_class
                            {
                                cnt = employee
                            };

                            SetResponse response2 = client.Set("SupplierCounterExisting/node", obj2);
                        


                        dataview();

                    }

                    else

                    {
                    }



                }
                //update
                if (e.ColumnIndex == Supplier_Datagrid.Columns[6].Index)
                {
                    columnindex = Supplier_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();


                    Supplier_Datagrid.Rows[e.RowIndex].Selected = true;

                    Supplier_ID = Supplier_Datagrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                    Supplier_Name = Supplier_Datagrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                    Supplier_Address = Supplier_Datagrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                    Supplier_Number = Supplier_Datagrid.Rows[e.RowIndex].Cells[3].Value.ToString();
                    Last_Transaction = Supplier_Datagrid.Rows[e.RowIndex].Cells[4].Value.ToString();
                    Date_Added = Supplier_Datagrid.Rows[e.RowIndex].Cells[5].Value.ToString();
           


                    Updatesupplier_popup update = new Updatesupplier_popup();

                    update.Show();


                }


            }
            catch
            {

            }
        }

        private void searchtxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (searchtxt.Text == "" && supressor == 1)
            {
                supressor = 0;

                Supplier_Datagrid.DataSource = null;
                Supplier_Datagrid.Rows.Clear();
                Supplier_Datagrid.Columns.Clear();
                Supplier_Datagrid.DataSource = dt;




                DataGridViewImageColumn update = new DataGridViewImageColumn();
                Supplier_Datagrid.Columns.Add(update);
                update.HeaderText = "";
                update.Name = "update";
                update.ImageLayout = DataGridViewImageCellLayout.Zoom;
                update.Image = Properties.Resources.Update_Icon;


                DataGridViewImageColumn Archive = new DataGridViewImageColumn();
                Supplier_Datagrid.Columns.Add(Archive);
                Archive.HeaderText = "";
                Archive.Name = "Archive";
                Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Archive.Image = Properties.Resources.Archive_Icon;

                dataview();

               

            }

            if (searchtxt.Text != "")
            {
                supressor = 1;

            }
        }

        private void a_Click(object sender, EventArgs e)
        {
            



            DataView dv = new DataView(dt);
            dv.RowFilter = "[" + combofilter.selectedValue + "]" + "LIKE '%" + searchtxt.Text + "%'";

            Supplier_Datagrid.DataSource = null;
            Supplier_Datagrid.Rows.Clear();
            Supplier_Datagrid.Columns.Clear();
            Supplier_Datagrid.DataSource = dv;

            DataGridViewImageColumn update = new DataGridViewImageColumn();
            Supplier_Datagrid.Columns.Add(update);
            update.HeaderText = "";
            update.Name = "update";
            update.ImageLayout = DataGridViewImageCellLayout.Zoom;
            update.Image = Properties.Resources.Update_Icon;


            DataGridViewImageColumn Archive = new DataGridViewImageColumn();
            Supplier_Datagrid.Columns.Add(Archive);
            Archive.HeaderText = "";
            Archive.Name = "Archive";
            Archive.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Archive.Image = Properties.Resources.Archive_Icon;


            foreach (DataGridViewColumn column in Supplier_Datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //visual
            DataGridViewColumn column1 = Supplier_Datagrid.Columns[1];
            column1.Width = 210;

            DataGridViewColumn column2 = Supplier_Datagrid.Columns[2];
            column2.Width = 270;

            DataGridViewColumn column3 = Supplier_Datagrid.Columns[3];
            column3.Width = 220;

            DataGridViewColumn column4 = Supplier_Datagrid.Columns[4];
            column4.Width = 170;

            DataGridViewColumn column5 = Supplier_Datagrid.Columns[5];
            column5.Width = 170;


        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton19_Click(object sender, EventArgs e)
        {
            Dashboard_Module a = new Dashboard_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton17_Click(object sender, EventArgs e)
        {
            Inventory_Module a = new Inventory_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton16_Click(object sender, EventArgs e)
        {
            Accountmanagement_Module a = new Accountmanagement_Module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            Supplierrecord_module a = new Supplierrecord_module();
            this.Hide();
            a.Show();
        }

        private void bunifuImageButton9_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
