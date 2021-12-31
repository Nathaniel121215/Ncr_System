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
    public partial class Processcustomerorder_popup : Form
    {
        IFirebaseClient client;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };
        string fmt = "##.00";

        public static string refferencenum;
        public static string date;
        public static string customername;
        public static string customeraddress;
        public static string trasactiontype;
        public static string remarks;

        public static int referencenumber;


        string temfee;
   



        public Processcustomerorder_popup()
        {
            InitializeComponent();
        }

        private void Processcustomerorder_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            Random rnd = new Random();
            referencenumber = rnd.Next(1000000, 9999999);

            string date1 = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");


            refnumbe.Text = referencenumber.ToString();
            transacdat.Text = date1.ToString();


        }

        private void Finalize_Button_Click(object sender, EventArgs e)
        {
            if(customernam.Text!="" && customeraddres.Text!="" && transactyp.Text !="")
            {
                temfee = POS_module.fee.ToString(fmt);


                if (temfee.Equals(".00"))
                {
                    temfee = "0.00";
                }


                refferencenum = refnumbe.Text;
                date = transacdat.Text;
                customername = customernam.Text;
                remarks = remark.Text;
                trasactiontype = transactyp.Text;
                customeraddress = customeraddres.Text;



                if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    MessageBox.Show("Recorded successfully");


                    //GET restock counter
                    FirebaseResponse resp = client.Get("SalesCount/node");
                    Counter_class get = resp.ResultAs<Counter_class>();

                    var data = new Purchase_Class
                    {
                        Purchase_ID = (Convert.ToInt32(get.cnt) + 1).ToString(),

                        Reference_Number = refferencenum,
                        Date_Of_Transaction = date,
                        Customer_Name = customername,
                        Items = POS_module.details2,
                        Order_Total = POS_module.total.ToString(),
                        Sub_Total = POS_module.subtotal.ToString(),
                        Fee = temfee,
                        Change = POS_module.change.ToString(),
                        Amount_Tendered = POS_module.payment.ToString(fmt),
                        Assisted_By = Form1.username,
                        Remark = remarks,
                        Transaction_Type = trasactiontype,
                        Customer_Address = customeraddress,


                    };

                    //ADD new restocklog
                    SetResponse response = client.Set("CompanySales/" + data.Purchase_ID, data);
                    Stock_class result = response.ResultAs<Stock_class>();

                    //update counter
                    var obj = new Counter_class
                    {
                        cnt = data.Purchase_ID
                    };

                    SetResponse response1 = client.Set("SalesCount/node", obj);

                  



                    //POS PURCHASE EVENT

                    FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                    Counter_class get4 = resp4.ResultAs<Counter_class>();
                    int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                    var data3 = new ActivityLog_Class
                    {
                        Event_ID = cnt4.ToString(),
                        Module = "Point of Sales Module",
                        Action = "Purchase-ID: " + data.Purchase_ID + "   Item Purchased",
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
                    POS_module._instance.minusstock();
               

                }

                else
                {

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
