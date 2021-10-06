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
    public partial class AccountArchiveView_popup : Form
    {
        public AccountArchiveView_popup()
        {
            InitializeComponent();
        }

        private void AccountArchiveView_popup_Load(object sender, EventArgs e)
        {
            idtxt.Text = AccountArchive_Module.User_ID;
            
            usertxt.Text = AccountArchive_Module.Username;
            passtxt.Text = AccountArchive_Module.Password;
            fnametxt.Text = AccountArchive_Module.Firstname;
            lnametxt.Text = AccountArchive_Module.Lastname;
            leveltxt.Text = AccountArchive_Module.Account_Level;

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
