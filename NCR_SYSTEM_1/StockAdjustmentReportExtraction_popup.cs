﻿using System;
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
    public partial class StockAdjustmentReportExtraction_popup : Form
    {
        public StockAdjustmentReportExtraction_popup()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void StockAdjustmentReportExtraction_popup_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            string fmt = "##.00";


            double percent = 100 + Convert.ToDouble(Stockadjustmentrecord_module.percentage2);
            double finalpercent = .01 * percent;
            double minus = Convert.ToDouble(Stockadjustmentrecord_module.grosssales2) / finalpercent;
        

            System.Drawing.Graphics g;
            g = e.Graphics;

            Font font = new Font("Courier New", 8);

            float fontHeight = font.GetHeight();

            int upperY = 20;


            g.DrawString("Stock Adjustment Extraction Report", font, new SolidBrush(Color.Black), 580, upperY);

            g.DrawString("NCR Gravel and Sand Enteprise", new Font("Courier New", 10), new SolidBrush(Color.Black), 40, upperY);


            upperY = upperY + 15;

            g.DrawString("42 Felix Ave. Brgy San Isidro Cainta Rizal", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Cecil R. delas Armas Prop.", font, new SolidBrush(Color.Black), 40, upperY);
            upperY = upperY + 15;

            g.DrawString("Tel Nos: 647-8021/ 296-466/ 492-1773/ 341-7840", font, new SolidBrush(Color.Black), 40, upperY);
            upperY = upperY + 15;

            g.DrawString("Cell #: 0922-853-7840", font, new SolidBrush(Color.Black), 40, upperY);




            upperY = upperY + 15;

            g.DrawString("********************************************************************************************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Date: " + Stockadjustmentrecord_module.date3, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Transaction Count: " + Stockadjustmentrecord_module.trasacntioncount2, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Reason: " + Stockadjustmentrecord_module.reason2, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("********************************************************************************************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Total Value: " + Stockadjustmentrecord_module.grosssales2, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Company Average Profit Percentage: " + Stockadjustmentrecord_module.percentage2, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Total Loss: " + Stockadjustmentrecord_module.grosssales2 + " / " + finalpercent.ToString(fmt) + " = " + minus.ToString(fmt), font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("********************************************************************************************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Total Loss: " + minus.ToString(fmt), font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("********************************************************************************************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Date of Extraction: " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt"), font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Extracted By: " + Form1.username, font, new SolidBrush(Color.Black), 40, upperY);
        }

        public void CreateReceipt(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string fmt = "##.00";


            double percent = 100 + Convert.ToDouble(Stockadjustmentrecord_module.percentage2);
            double finalpercent = .01 * percent;
            double minus = Convert.ToDouble(Stockadjustmentrecord_module.grosssales2) / finalpercent;


            System.Drawing.Graphics g;
            g = e.Graphics;

            Font font = new Font("Courier New", 8);

            float fontHeight = font.GetHeight();

            int upperY = 20;


            g.DrawString("Stock Adjustment Extraction Report", font, new SolidBrush(Color.Black), 580, upperY);

            g.DrawString("NCR Gravel and Sand Enteprise", new Font("Courier New", 10), new SolidBrush(Color.Black), 40, upperY);


            upperY = upperY + 15;

            g.DrawString("42 Felix Ave. Brgy San Isidro Cainta Rizal", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Cecil R. delas Armas Prop.", font, new SolidBrush(Color.Black), 40, upperY);
            upperY = upperY + 15;

            g.DrawString("Tel Nos: 647-8021/ 296-466/ 492-1773/ 341-7840", font, new SolidBrush(Color.Black), 40, upperY);
            upperY = upperY + 15;

            g.DrawString("Cell #: 0922-853-7840", font, new SolidBrush(Color.Black), 40, upperY);




            upperY = upperY + 15;

            g.DrawString("********************************************************************************************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Date: " + Stockadjustmentrecord_module.date3, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Transaction Count: " + Stockadjustmentrecord_module.trasacntioncount2, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Reason: " + Stockadjustmentrecord_module.reason2, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("********************************************************************************************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Total Value: " + Stockadjustmentrecord_module.grosssales2, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Company Average Profit Percentage: " + Stockadjustmentrecord_module.percentage2, font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Total Loss: " + Stockadjustmentrecord_module.grosssales2 + " / " + finalpercent.ToString(fmt) + " = " + minus.ToString(fmt), font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("********************************************************************************************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Total Loss: " + minus.ToString(fmt), font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("********************************************************************************************************************", font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Date of Extraction: " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt"), font, new SolidBrush(Color.Black), 40, upperY);

            upperY = upperY + 15;

            g.DrawString("Extracted By: " + Form1.username, font, new SolidBrush(Color.Black), 40, upperY);

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
