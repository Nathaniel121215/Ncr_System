using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_SYSTEM_1
{
    public partial class Confirmcustomerreceipt_module : Form
    {
        public Confirmcustomerreceipt_module()
        {
            InitializeComponent();
        }
        string fee;
        string payment;
        string change;
        string subtotal;
        string total;
        string fmt = "##.00";

        string temfee;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            subtotal = POS_module.subtotal.ToString();
            fee = POS_module.fee.ToString(fmt);
            payment = POS_module.payment.ToString(fmt);
            change = POS_module.change.ToString();
            total = POS_module.total.ToString();

            temfee = POS_module.fee.ToString(fmt);


            if (temfee.Equals(".00"))
            {
                temfee = "0.00";
            }



            System.Drawing.Graphics g;
            g = e.Graphics;

            Font font = new Font("Courier New", 8);

            float fontHeight = font.GetHeight();

            int upperY = 20;



            g.DrawString("NCR Gravel and Sand Enteprise", new Font("Courier New", 10), new SolidBrush(Color.Black), 80, upperY);

            upperY = upperY + 15;

            g.DrawString("42 Felix Ave. Brgy San Isidro Cainta Rizal", font, new SolidBrush(Color.Black), 60, upperY);

            upperY = upperY + 15;

            g.DrawString("Cecil R. delas Armas Prop.", font, new SolidBrush(Color.Black), 110, upperY);
            upperY = upperY + 15;

            g.DrawString("Tel Nos: 647-8021/ 296-466/ 492-1773/ 341-7840", font, new SolidBrush(Color.Black), 40, upperY);
            upperY = upperY + 15;

            g.DrawString("Cell #: 0922-853-7840", font, new SolidBrush(Color.Black), 130, upperY);


            upperY = upperY + 15;

            g.DrawString("**********************************************", font, new SolidBrush(Color.Black), 40, upperY);


            upperY = upperY + 15;

            string info = "Reference Number:" + Processcustomerorder_popup.refferencenum.PadRight(13);
            g.DrawString(info, font, new SolidBrush(Color.Black), 40, upperY);
            upperY = upperY + 15;

            g.DrawString("Date:" + Processcustomerorder_popup.date, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Customer Name:" + Processcustomerorder_popup.customername, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Customer Address:" + Processcustomerorder_popup.customeraddress, font, new SolidBrush(Color.Black), 40, upperY);


            upperY = upperY + 15;

            g.DrawString("Cashier:" + Form1.username, font, new SolidBrush(Color.Black), 40, upperY);


            upperY = upperY + 15;




            g.DrawString("**********************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            string top = "x".PadRight(5) + "Item Name".PadRight(30) + "Price";
            g.DrawString(top, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("**********************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            for (int i = 0; i < POS_module.cartcount; i++)
            {
                string item = POS_module.quantitylist[i].PadRight(5) + POS_module.productnamelist[i].PadRight(30) + POS_module.Pricelist[i];
                g.DrawString(item, font, new SolidBrush(Color.Black), 40, upperY);
                upperY = upperY + 20;
            }



            g.DrawString("**********************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("SubTotal:".PadRight(35) + subtotal, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Fee:".PadRight(35) + temfee, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Total:".PadRight(35) + total, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("**********************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Payment:".PadRight(35) + payment, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Change:".PadRight(35) + change, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;


            g.DrawString("**********************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Thank you for purchasing", font, new SolidBrush(Color.Black), 120, upperY);

            upperY = upperY + 30;

            g.DrawString(Processcustomerorder_popup.remarks, font, new SolidBrush(Color.Black), 40, upperY);




            POS_module._instance.clear();


        }


        public void CreateReceipt(object sender, System.Drawing.Printing.PrintPageEventArgs e)


        {
            System.Drawing.Graphics g;
            g = e.Graphics;

            Font font = new Font("Courier New", 5);

            float fontHeight = font.GetHeight();

            int upperY = 20;



            g.DrawString("NCR Gravel and Sand Enteprise", new Font("Courier New", 6), new SolidBrush(Color.Black), 15, upperY);

            upperY = upperY + 15;

            g.DrawString("42 Felix Ave. Brgy San Isidro Cainta Rizal", font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("Cecil R. delas Armas Prop.", font, new SolidBrush(Color.Black), 31, upperY);
            upperY = upperY + 15;

            g.DrawString("Tel Nos: 647-8021/ 296-466/ 492-1773/ 341-7840", font, new SolidBrush(Color.Black), 1, upperY);
            upperY = upperY + 15;

            g.DrawString("Cell #: 0922-853-7840", font, new SolidBrush(Color.Black), 43, upperY);


            upperY = upperY + 15;

            g.DrawString("**********************************************", font, new SolidBrush(Color.Black), 1, upperY);


            upperY = upperY + 15;

            string info = "Reference Number:" + Processcustomerorder_popup.refferencenum.PadRight(13);
            g.DrawString(info, font, new SolidBrush(Color.Black), 1, upperY);
            upperY = upperY + 15;

            g.DrawString("Date:" + Processcustomerorder_popup.date, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("Customer Name:" + Processcustomerorder_popup.customername, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("Customer Address:" + Processcustomerorder_popup.customeraddress, font, new SolidBrush(Color.Black), 1, upperY);


            upperY = upperY + 15;

            g.DrawString("Cashier:" + Form1.username, font, new SolidBrush(Color.Black), 1, upperY);


            upperY = upperY + 15;




            g.DrawString("**********************************************", font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            string top = "x".PadRight(5) + "Item Name".PadRight(30) + "Price";
            g.DrawString(top, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("**********************************************", font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            for (int i = 0; i < POS_module.cartcount; i++)
            {
                string item = POS_module.quantitylist[i].PadRight(5) + POS_module.productnamelist[i].PadRight(30) + POS_module.Pricelist[i];
                g.DrawString(item, font, new SolidBrush(Color.Black), 1, upperY);
                upperY = upperY + 20;
            }



            g.DrawString("**********************************************", font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("SubTotal:".PadRight(35) + subtotal, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("Fee:".PadRight(35) + temfee, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("Total:".PadRight(35) + total, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("**********************************************", font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("Payment:".PadRight(35) + payment, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("Change:".PadRight(35) + change, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;


            g.DrawString("**********************************************", font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("Thank you for purchasing", font, new SolidBrush(Color.Black), 43, upperY);

            upperY = upperY + 30;

            g.DrawString(Processcustomerorder_popup.remarks, font, new SolidBrush(Color.Black), 1, upperY);


        }

        private void Confirmcustomerreceipt_module_Load(object sender, EventArgs e)
        {
            panel1.Height = 603;

            if (POS_module.cartcount > 8)
            {
                int sizeadjust = POS_module.cartcount - 8;
                int sizeadjus2 = sizeadjust * 25;
                panel1.Height = 603 + sizeadjus2;
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {

                if (Processcustomerorder_popup.trasactiontype == "Delivery")
                {
                    this.Hide();
                    DeliveryConfirmation_popup b = new DeliveryConfirmation_popup();
                    b.Show();
                }
                else
                {
                    this.Hide();
                    Form1.status = "true";

                    POS_module.payment = 0;
                    POS_module.change = 0;
                    POS_module.fee = 0;
                    POS_module.subtotal = 0;
                    POS_module.total = 0;
                }

            }
            else
            {

            }


        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            

            if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {

                if (Processcustomerorder_popup.trasactiontype == "Delivery")
                {
                    this.Hide();
                    DeliveryConfirmation_popup b = new DeliveryConfirmation_popup();
                    b.Show();
                }
                else
                {
                    this.Hide();
                    Form1.status = "true";

                    POS_module.payment = 0;
                    POS_module.change = 0;
                    POS_module.fee = 0;
                    POS_module.subtotal = 0;
                    POS_module.total = 0;
                }
                
            }
            else
            {

            }
        }

        private void Print_Button_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            PrintDocument printDocument = new PrintDocument();

            printDialog.Document = printDocument; //add the document to the dialog box...        

            printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(CreateReceipt); //add an event handler that will do the printing

            //on a till you will not want to ask the user where to print but this is fine for the test envoironment.

            DialogResult result = printDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                printDocument.Print();

            }
        }
    }
}
