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
    public partial class StockAdjustmentRemarks_popup : Form
    {
        public StockAdjustmentRemarks_popup()
        {
            InitializeComponent();
        }

        private void StockAdjustmentRemarks_popup_Load(object sender, EventArgs e)
        {
            label1.Select();
            remarktxt.Text = Stockadjustmentrecord_module.remarks.ToString();
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
    }
}
