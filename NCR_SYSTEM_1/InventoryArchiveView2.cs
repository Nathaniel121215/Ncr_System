﻿using System;
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
            if (MessageBox.Show("Please confirm before proceeding" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                this.Hide();
                Form1.status = "true";
            }
            else
            {

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
    }
}
