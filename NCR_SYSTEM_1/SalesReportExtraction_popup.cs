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
    public partial class SalesReportExtraction_popup : Form
    {
        public SalesReportExtraction_popup()
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

            g.DrawString("==============================================", font, new SolidBrush(Color.Black), 40, upperY);
        }
    }
}
