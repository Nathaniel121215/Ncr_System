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
    public partial class Finalizestock_popup : Form
    {
        string fmt = "##.00";

        IFirebaseClient client;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        string[] listitems = new string[100];
        public static string plist;

        public Finalizestock_popup()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void Finalizestock_popup_Load(object sender, EventArgs e)
        {

            client = new FireSharp.FirebaseClient(config);
            items.Items.Clear();

            for (int i = 0; i < Stockpurchasing_Module.itemCount; i++)
            {
                items.Items.Add(Stockpurchasing_Module.listitems[i].ToString());

            }

            listitems = new string[items.Items.Count];

            for (int i = 0; i < Stockpurchasing_Module.itemCount; i++)
            {
                listitems[i] = items.Items[i].ToString();
            }

            plist = string.Join("\n", listitems);

            detailtxt.Text = Addstock_popup.details;

            refstock.Text = Addstock_popup.reff;
            datestock.Text = Addstock_popup.date;
            substock.Text = Addstock_popup.subtotal.ToString(fmt);
            feestock.Text = Addstock_popup.fee.ToString(fmt);
            totalstock.Text = Addstock_popup.total.ToString(fmt);
            supstock.Text = Addstock_popup.supplier;
            amountstock.Text = Addstock_popup.amounttendered.ToString(fmt);
            changestock.Text = Addstock_popup.change.ToString(fmt);
            assistedstock.Text = Form1.username;
            remarkstxt.Text = Addstock_popup.remarks.ToString();

            if(feestock.Text.Equals(".00"))
            {
                feestock.Text = "0.00";
            }
            if (changestock.Text.Equals(".00"))
            {
                changestock.Text = "0.00";
            }


        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                MessageBox.Show("Recorded successfully");

                FirebaseResponse resp = client.Get("ReStockCounter/node");
                Counter_class get = resp.ResultAs<Counter_class>();

                var data = new Stock_class
                {
                    Restock_ID = (Convert.ToInt32(get.cnt) + 1).ToString(),
                    Reference_Number = Addstock_popup.reff,
                    Date_Of_Transaction = Addstock_popup.date,
                    Order_Total = Addstock_popup.total.ToString(fmt),
                    Sub_Total = Addstock_popup.subtotal.ToString(fmt),
                    Fee = feestock.Text,
                    Items = Addstock_popup.details,
                    //Items = plist,
                    
                    Assisted_By = Form1.username,
                    Remark = Addstock_popup.remarks,
                    Supplier_Name = Addstock_popup.supplier,

                    Change = changestock.Text,
                    Amount_Tendered = Addstock_popup.amounttendered.ToString(fmt),


                };

                SetResponse response = client.Set("ReStockLog/" + data.Restock_ID, data);
                Stock_class result = response.ResultAs<Stock_class>();

                var obj = new Counter_class
                {
                    cnt = data.Restock_ID
                };

                SetResponse response1 = client.Set("ReStockCounter/node", obj);


                //update last transaction 
                int i = 0;
                FirebaseResponse resp2 = client.Get("SupplierCounter/node");
                Counter_class obj2 = resp.ResultAs<Counter_class>();
                int cnt = Convert.ToInt32(obj2.cnt);


                while (i <= cnt)
                {
           
                    i++;
                    try
                    {

                        FirebaseResponse resp1 = client.Get("Supplier/" + i);
                        Supplier_Class obj3 = resp1.ResultAs<Supplier_Class>();

                        if (Addstock_popup.supplier.Equals(obj3.Supplier_Name))
                        {
                            var data2 = new update_last
                            {

                                Supplier_ID = obj3.Supplier_ID,
                                Supplier_LastTransaction = Addstock_popup.date,

                            };
                            FirebaseResponse response2 = client.Update("Supplier/" + data2.Supplier_ID, data2);
                            

                        }
                        else
                        {

                        }
                        
                    }

                    catch
                    {

                    }
                }




                

                this.Hide();
                Stockpurchasing_Module._instance.addstock();
            }

            else

            {
                //do something if NO
            }
            

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }

   
}
