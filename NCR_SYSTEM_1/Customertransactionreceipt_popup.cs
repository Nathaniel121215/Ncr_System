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
    public partial class Customertransactionreceipt_popup : Form
    {
        public Customertransactionreceipt_popup()
        {
            InitializeComponent();
        }
        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
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

            g.DrawString("Tel Nos: 647-8021/296-466/492-1773/341-7840", font, new SolidBrush(Color.Black), 40, upperY);
            upperY = upperY + 15;

            g.DrawString("Cell #: 0922-853-7840", font, new SolidBrush(Color.Black), 130, upperY);


            upperY = upperY + 15;

            g.DrawString("********************************************", font, new SolidBrush(Color.Black), 40, upperY);


            upperY = upperY + 15;

            string info = "Reference Number:" + Salesrecord_module.refnumber.PadRight(13);
            g.DrawString(info, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Date: " + Salesrecord_module.date, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Customer Name:" + Salesrecord_module.customer, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;


            g.DrawString("Customer Address:" + Salesrecord_module.address, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Cashier:" + Salesrecord_module.assist, font, new SolidBrush(Color.Black), 40, upperY);


            upperY = upperY + 15;




            g.DrawString("********************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            string top = "x".PadRight(5) + "Item Name".PadRight(30) + "Price";
            g.DrawString(top, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("********************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            for (int i = 0; i < Salesrecord_module.cartcount; i++)
            {
                string item = Salesrecord_module.quantitylist[i].PadRight(5) + Salesrecord_module.productnamelist[i].PadRight(30) + Salesrecord_module.Pricelist[i];
                g.DrawString(item, font, new SolidBrush(Color.Black), 40, upperY);
                upperY = upperY + 20;
            }



            g.DrawString("********************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("SubTotal:".PadRight(35) + Salesrecord_module.sub, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Fee:".PadRight(35) + Salesrecord_module.fee, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Total:".PadRight(35) + Salesrecord_module.total, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("********************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Payment:".PadRight(35) + Salesrecord_module.amount, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Change:".PadRight(35) + Salesrecord_module.change, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;


            g.DrawString("********************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Thank you for purchasing", font, new SolidBrush(Color.Black), 110, upperY);

            upperY = upperY + 30;

            g.DrawString(Salesrecord_module.remarks, font, new SolidBrush(Color.Black), 40, upperY);
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

            g.DrawString("Tel Nos: 647-8021/296-466/492-1773/341-7840", font, new SolidBrush(Color.Black), 1, upperY);
            upperY = upperY + 15;

            g.DrawString("Cell #: 0922-853-7840", font, new SolidBrush(Color.Black), 43, upperY);


            upperY = upperY + 15;

            g.DrawString("********************************************", font, new SolidBrush(Color.Black), 1, upperY);


            upperY = upperY + 15;

            string info = "Reference Number:" + Salesrecord_module.refnumber.PadRight(13);
            g.DrawString(info, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("Date: " + Salesrecord_module.date, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("Customer Name:" + Salesrecord_module.customer, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;


            g.DrawString("Customer Address:" + Salesrecord_module.address, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("Cashier:" + Salesrecord_module.assist, font, new SolidBrush(Color.Black), 1, upperY);


            upperY = upperY + 15;




            g.DrawString("********************************************", font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            string top = "x".PadRight(5) + "Item Name".PadRight(30) + "Price";
            g.DrawString(top, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("********************************************", font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            for (int i = 0; i < Salesrecord_module.cartcount; i++)
            {
                string item = Salesrecord_module.quantitylist[i].PadRight(5) + Salesrecord_module.productnamelist[i].PadRight(30) + Salesrecord_module.Pricelist[i];
                g.DrawString(item, font, new SolidBrush(Color.Black), 1, upperY);
                upperY = upperY + 20;
            }



            g.DrawString("********************************************", font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("SubTotal:".PadRight(35) + Salesrecord_module.sub, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("Fee:".PadRight(35) + Salesrecord_module.fee, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("Total:".PadRight(35) + Salesrecord_module.total, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("********************************************", font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("Payment:".PadRight(35) + Salesrecord_module.amount, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("Change:".PadRight(35) + Salesrecord_module.change, font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;


            g.DrawString("********************************************", font, new SolidBrush(Color.Black), 1, upperY);

            upperY = upperY + 15;

            g.DrawString("Thank you for purchasing", font, new SolidBrush(Color.Black), 43, upperY);

            upperY = upperY + 30;

            g.DrawString(Salesrecord_module.remarks, font, new SolidBrush(Color.Black), 1, upperY);


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

        private void Customertransactionreceipt_popup_Load(object sender, EventArgs e)
        {

    

            


        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}
