using FireSharp.Config;
using FireSharp.Interfaces;
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
    public partial class Supplierdetails_popup : Form
    {
    
        public Supplierdetails_popup()
        {
            InitializeComponent();
        }

        private void Supplierdetails_popup_Load(object sender, EventArgs e)
        {
            refstock.Text = Supplierrecord_module.refnumber;
            datestock.Text = Supplierrecord_module.date;
            substock.Text = Supplierrecord_module.sub.ToString();
            feestock.Text = Supplierrecord_module.fee.ToString();
            totalstock.Text = Supplierrecord_module.total.ToString();
            supname.Text = Supplierrecord_module.supp;
            amountstock.Text = Supplierrecord_module.amount.ToString();
            changestock.Text = Supplierrecord_module.change.ToString();
            assistedstock.Text = Supplierrecord_module.assist;

            remarkstxt.Text = Supplierrecord_module.remarks;
          
            detailtxt.Text = Supplierrecord_module.items;

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
           
            this.Hide();
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
