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
    public partial class Addstock_popup : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public Addstock_popup()
        {
            InitializeComponent();
        }
        public static decimal change = 0;

        public static int referencenumber = 0;

        public static decimal fee = 0;
        public static decimal subtotal = 0;
        public static decimal total = 0;



        public static string date = "";
        public static string supplier = "";
        public static string remarks = "";
        public static string reff = "";

        public static decimal amounttendered = 0;

        public static string details = "";



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Addstock_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            Random rnd = new Random();
            referencenumber = rnd.Next(1000000, 9999999);

            String date1 = DateTime.Now.ToString("MM/dd/yyyy");
            date = date1;

            refnumber.Text = referencenumber.ToString();
            transactdate.Text = date;

            
            totaltxt.Text = Stockpurchasing_Module.total.ToString();
            subtxt.Text = Stockpurchasing_Module.total.ToString();


            items.Items.Clear();
            for (int i = 0; i < Stockpurchasing_Module.itemCount; i++)
            {
                items.Items.Add(Stockpurchasing_Module.listitems[i].ToString());

            }

            detailtxt.Text = Stockpurchasing_Module.details;




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



                for (int i = 0; i <= cnt; i++)
                {

                    supcombo.Items.Add(supplier[i].ToString());


                }
            }
            catch
            {
            }

        }

        public void compute()
        {
            try
            {

                subtotal = Convert.ToDecimal(subtxt.Text);
                fee = Convert.ToDecimal(feetxt.Text);

                total = subtotal + fee;

                totaltxt.Text = Convert.ToString(total);


                total = Convert.ToDecimal(totaltxt.Text);
                amounttendered = Convert.ToDecimal(amountentxt.Text);


                change = amounttendered - total;

                changetxt.Text = change.ToString();
            }
            catch
            {

            }

        }
       

        private void feetxt_KeyUp(object sender, KeyEventArgs e)
        {
            compute();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

            reff = refnumber.Text.ToString();
            date = transactdate.Text;
            supplier = supcombo.Text;
            fee = Convert.ToDecimal(feetxt.Text);
            total = Convert.ToDecimal(totaltxt.Text);
            remarks = remarkstxt.Text;
            subtotal = Convert.ToDecimal(subtxt.Text);
            amounttendered = Convert.ToDecimal(amountentxt.Text);
            details = detailtxt.Text;


            Finalizestock_popup fstock = new Finalizestock_popup();
            this.Hide();
            fstock.Show();
        }

        private void feetxt_OnValueChanged(object sender, EventArgs e)
        {
         
           
           

        }

        private void bunifuMetroTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void amountentxt_KeyUp(object sender, KeyEventArgs e)
        {
            compute();
            
          
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
