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
    public partial class Supplierrecord_Filter_popup : Form
    {
        public static string startdate;
        public static string enddate;
        public static string supplier;
        public static string assistedby;

        IFirebaseClient client;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };



        public Supplierrecord_Filter_popup()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (starttxt.Value.ToString() != "" && endtxt.Value.ToString() != "" && suppliertxt.Text != "")
            {
                startdate = starttxt.Value.ToString("MM/dd/yyyy");
                enddate = endtxt.Value.ToString("MM/dd/yyyy");
                supplier = suppliertxt.Text;
                assistedby = assistedtxt.Text;


                Supplierrecord_module._instance.filter();
                Form1.status = "true";
                this.Hide();
            }
            else
            {
                MessageBox.Show("Fill up all necessary fields.");
            }
          
        }

        private void Supplierrecord_Filter_popup_Load(object sender, EventArgs e)
        {
            endtxt.Value = DateTime.Today;
            starttxt.Value = DateTime.Today;

            client = new FireSharp.FirebaseClient(config);

            ////adding supplier combobox
            try
            {
                FirebaseResponse resp3 = client.Get("SupplierCounter/node");
                Counter_class obj = resp3.ResultAs<Counter_class>();
                int cnt = Convert.ToInt32(obj.cnt);

                List<string> supplier = new List<string>();
                for (int runs = 0; runs <= cnt; runs++)
                {
                    try
                    {
                        FirebaseResponse resp1 = client.Get("Supplier/" + runs);
                        Supplier_Class obj2 = resp1.ResultAs<Supplier_Class>();

                        supplier.Add(obj2.Supplier_Name);
                    }
                    catch
                    {

                    }

                }


                //ADD supplier to combobox
                for (int i = 0; i <= cnt; i++)
                {

                    suppliertxt.Items.Add(supplier[i].ToString());

                }
            }
            catch
            {
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                this.Hide();
                Form1.status = "true";
            }
            else
            {

            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                this.Hide();
                Form1.status = "true";
            }
            else
            {

            }
          
        }

        private void assistedtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }
    }
}
