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
    public partial class ProcessOrder_popup : Form
    {
        string fmt = "##.00";

        IFirebaseClient client;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        public static string date;
        public static string supplier;
        public static string refferencenum;
        public static string remark;


        public static int referencenumber;




        string temfee;
        


        public static ProcessOrder_popup _instance;

        public ProcessOrder_popup()
        {
            InitializeComponent();
            _instance = this;
        }

        private void ProcessOrder_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            Random rnd = new Random();
            referencenumber = rnd.Next(1000000, 9999999);

            string date = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");




            refnumber.Text = referencenumber.ToString();
            transacdate.Text = date.ToString();




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

                    suppliername.Items.Add(supplier[i].ToString());


                }
            }
            catch
            {
            }




        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if(suppliername.Text != "")
            {
                temfee = stockpurchase_Module.fee.ToString(fmt);


                if (temfee.Equals(".00"))
                {
                    temfee = "0.00";
                }
                if(remarks.Text=="")
                {
                    remarks.Text = "None";
                }



                refferencenum = refnumber.Text;
                date = transacdate.Text;
                supplier = suppliername.Text;
                remark = remarks.Text;




                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    MessageBox.Show("Recorded successfully");


                    //GET restock counter
                    FirebaseResponse resp = client.Get("ReStockCounter/node");
                    Counter_class get = resp.ResultAs<Counter_class>();

                    var data = new Stock_class
                    {
                        Restock_ID = (Convert.ToInt32(get.cnt) + 1).ToString(),

                        Reference_Number = refferencenum,
                        Date_Of_Transaction = date,
                        Supplier_Name = supplier,
                        Items = stockpurchase_Module.details2,

                        Order_Total = stockpurchase_Module.total.ToString(),
                        Sub_Total = stockpurchase_Module.subtotal.ToString(),




                        Fee = temfee,
                        Change = stockpurchase_Module.change.ToString(),
                        Amount_Tendered = stockpurchase_Module.payment.ToString(fmt),

                        Assisted_By = Form1.username,
                        Remark = remark,

                    };

                    //ADD new restocklog
                    SetResponse response = client.Set("ReStockLog/" + data.Restock_ID, data);
                    Stock_class result = response.ResultAs<Stock_class>();

                    //update counter
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

                            if (supplier.Equals(obj3.Supplier_Name))
                            {
                                var data2 = new update_last
                                {

                                    Supplier_ID = obj3.Supplier_ID,
                                    Supplier_LastTransaction = date,

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

                    //Activity Log STOCK PURCHASE EVENT

                    FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                    Counter_class get4 = resp4.ResultAs<Counter_class>();
                    int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                    var data3 = new ActivityLog_Class
                    {
                        Event_ID = cnt4.ToString(),
                        Module = "Stock Replenishment Module",
                        Action = "Restock-ID: " + data.Restock_ID + "   Item Restocked",
                        Date = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                        User = Form1.username,
                        Accountlvl = Form1.levelac,

                    };



                    FirebaseResponse response5 = client.Set("ActivityLog/" + data3.Event_ID, data3);



                    var obj4 = new Counter_class
                    {
                        cnt = data3.Event_ID

                    };

                    SetResponse response6 = client.Set("ActivityLogCounter/node", obj4);




                    this.Hide();
                    stockpurchase_Module._instance.addstock();
                }

                else

                {
                    //do something if NO
                }
            }
            else
            {
                MessageBox.Show("Fill up all necessary fields.");
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

        private void button1_Click(object sender, EventArgs e)
        {
            Addsupplier_popup a = new Addsupplier_popup();
            a.Show();
        }

        public void refreshsupplier()
        {
            suppliername.Items.Clear();

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

                    suppliername.Items.Add(supplier[i].ToString());


                }
            }
            catch
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
    }
}


internal class update_last
{
    public object Supplier_ID { get; set; }
    public object Supplier_LastTransaction { get; set; }
}