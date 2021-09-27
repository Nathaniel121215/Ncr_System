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
    public partial class InventoryArchiveView2 : Form
    {
        public InventoryArchiveView2()
        {
            InitializeComponent();
        }

        private void InventoryArchiveView2_Load(object sender, EventArgs e)
        {
            low.Text = InventoryArchive_Module.Low;
            high.Text = InventoryArchive_Module.High;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
