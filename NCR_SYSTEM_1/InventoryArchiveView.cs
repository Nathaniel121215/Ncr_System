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
    public partial class InventoryArchiveView : Form
    {
        public InventoryArchiveView()
        {
            InitializeComponent();
        }

        private void InventoryArchiveView_Load(object sender, EventArgs e)
        {
            punit.Items.Add(InventoryArchive_Module.Unit);
            pcategory.Items.Add(InventoryArchive_Module.Category);

            pid.Text = InventoryArchive_Module.ID;
            pname.Text = InventoryArchive_Module.Product_Name;
            pcategory.Text = InventoryArchive_Module.Category;
            pdescription.Text = InventoryArchive_Module.Description;
            pbrand.Text = InventoryArchive_Module.Brand;
            pprice.Text = InventoryArchive_Module.Price.ToString();
            punit.Text = InventoryArchive_Module.Unit;

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            InventoryArchiveView2 c = new InventoryArchiveView2();
            c.Show();
            this.Hide();
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
