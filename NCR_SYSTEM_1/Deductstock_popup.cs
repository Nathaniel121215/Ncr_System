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
    public partial class Deductstock_popup : Form
    {
        public Deductstock_popup()
        {
            InitializeComponent();
        }

        IFirebaseClient client;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        string fmt = "##.00";

        private void Deductstock_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

         


            productnametxt.Text = StockAdjustment_Module.name;
            currentstocktxt.Text = StockAdjustment_Module.stock.ToString();
            deductedstocktxt.Text = "0";
            



        }

        private void Finalize_Button_Click(object sender, EventArgs e)
        {
            if(deductedstocktxt.Text!="0" && reasontxt.Text !="" && Convert.ToInt32(deductedstocktxt.Text) > Convert.ToInt32(currentstocktxt.Text))
            {
                int minus = Convert.ToInt32(deductedstocktxt.Text);
                decimal price = Convert.ToDecimal(StockAdjustment_Module.price);
                decimal total = minus * price;


                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {


                    //DEDUCTING STOCK
                    try
                    {
                        int stock = StockAdjustment_Module.stock - minus;
                        var data = new SDU2
                        {

                            ID = StockAdjustment_Module.id,
                            Stock = stock.ToString(),

                        };

                        FirebaseResponse response = client.Update("Inventory/" + data.ID, data);



                        //ADDING RECORD TO STOCK ADJUSTMENT RECORD

                        FirebaseResponse resp2 = client.Get("StockAdjustmentCounter/node");
                        Counter_class get2 = resp2.ResultAs<Counter_class>();
                        int cnt2 = (Convert.ToInt32(get2.cnt) + 1);



                        var data2 = new StockAdjustment_Class
                        {
                            Event_ID = cnt2.ToString(),
                            Product_Name = StockAdjustment_Module.name,
                            Value = total.ToString(),
                            User = Form1.username,
                            Action = "Product-ID: " + StockAdjustment_Module.id + "   Stock Adjusted by " + minus,
                            Date = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                            Remarks = remarktxt.Text,
                            Reason = reasontxt.Text,

                        };



                        FirebaseResponse response3 = client.Set("StockAdjustment/" + data2.Event_ID, data2);



                        var obj4 = new Counter_class
                        {
                            cnt = data2.Event_ID

                        };

                        SetResponse response6 = client.Set("StockAdjustmentCounter/node", obj4);




                        //Activity Log ADDING PRODUCT EVENT

                        FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                        Counter_class get4 = resp4.ResultAs<Counter_class>();
                        int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                        var data3 = new ActivityLog_Class
                        {
                            Event_ID = cnt4.ToString(),
                            Module = "Stock Adjustment",
                            Action = "Product-ID: " + StockAdjustment_Module.id + "   Stock Adjusted by " + minus,
                            Date = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                            User = Form1.username,
                            Accountlvl = Form1.levelac,

                        };



                        FirebaseResponse response4 = client.Set("ActivityLog/" + data3.Event_ID, data3);



                        var obj5 = new Counter_class
                        {
                            cnt = data3.Event_ID

                        };

                        SetResponse response5 = client.Set("ActivityLogCounter/node", obj5);


                        this.Hide();
                        StockAdjustment_Module._instance.DataViewAll();
                        Form1.status = "true";
                        MessageBox.Show("Stock successfully adjusted.");

                    }

                    catch
                    {

                    }

                }

                else

                {
                    //do something if NO
                }
            }
            else
            {
                MessageBox.Show("Fill up all necessary Fields.");

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

        private void deductedstocktxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void deductedstocktxt_Enter(object sender, EventArgs e)
        {
            if (deductedstocktxt.Text == "" || deductedstocktxt.Text == "0.00")
            {
                deductedstocktxt.Text = "";
            }
            else
            {

            }
        }

        private void deductedstocktxt_Leave(object sender, EventArgs e)
        {
            if (deductedstocktxt.Text == "")
            {
                deductedstocktxt.Text = "0";
              
            }
            else
            {
               
            }
        }
    }
}
