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
    public partial class DeliveryConfirmation_popup : Form
    {
        public DeliveryConfirmation_popup()
        {
            InitializeComponent();
        }

        private void DeliveryConfirmation_popup_Load(object sender, EventArgs e)
        {

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

            g.DrawString("Delivery Confirmation Receipt", font, new SolidBrush(Color.Black), 31, upperY);

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
            upperY = upperY + 15;


            g.DrawString("I hereby further acknowledge and confirm that", font, new SolidBrush(Color.Black), 1, upperY);
            upperY = upperY + 15;
            g.DrawString("I received the items in good order and condtion.", font, new SolidBrush(Color.Black), 1, upperY);
            upperY = upperY + 15;
            upperY = upperY + 15;
            upperY = upperY + 15;
            upperY = upperY + 15;
            upperY = upperY + 15;
            upperY = upperY + 15;
            upperY = upperY + 15;
            g.DrawString("______________________________________________", font, new SolidBrush(Color.Black), 1, upperY);
            upperY = upperY + 15;
            g.DrawString("Receiver Signature", font, new SolidBrush(Color.Black), 52, upperY);

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

            g.DrawString("Tel Nos: 647-8021/ 296-466/ 492-1773/ 341-7840", font, new SolidBrush(Color.Black), 40, upperY);
            upperY = upperY + 15;

            g.DrawString("Cell #: 0922-853-7840", font, new SolidBrush(Color.Black), 130, upperY);

            upperY = upperY + 15;

            g.DrawString("**********************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Delivery Confirmation Receipt", font, new SolidBrush(Color.Black), 110, upperY);

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
            upperY = upperY + 15;
          

            g.DrawString("I hereby further acknowledge and confirm that", font, new SolidBrush(Color.Black), 40, upperY);
            upperY = upperY + 15;
            g.DrawString("I received the items in good order and condtion.", font, new SolidBrush(Color.Black), 40, upperY);
            upperY = upperY + 15;
            upperY = upperY + 15;
            upperY = upperY + 15;
            upperY = upperY + 15;
            upperY = upperY + 15;
            upperY = upperY + 15;
            upperY = upperY + 15;
            g.DrawString("______________________________________________", font, new SolidBrush(Color.Black), 40, upperY);
            upperY = upperY + 15;
            g.DrawString("             Receiver Signature", font, new SolidBrush(Color.Black), 40, upperY);




        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {

         
                    this.Hide();
                    Form1.status = "true";

                    POS_module.payment = 0;
                    POS_module.change = 0;
                    POS_module.fee = 0;
                    POS_module.subtotal = 0;
                    POS_module.total = 0;
                

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

                POS_module.payment = 0;
                POS_module.change = 0;
                POS_module.fee = 0;
                POS_module.subtotal = 0;
                POS_module.total = 0;


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
