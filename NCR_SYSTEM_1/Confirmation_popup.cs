using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
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
    public partial class Confirmation_popup : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public Confirmation_popup()
        {
            InitializeComponent();
        }

        private void Confirmation_popup_Load(object sender, EventArgs e)
        {
            //open connection
            client = new FireSharp.FirebaseClient(config);


        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if(usertxt.Text!="" && passtxt1.Text!="")
            {

                try
                {
                    int i = 0;
                    FirebaseResponse resp = client.Get("AccountCounter/node");
                    Counter_class obj = resp.ResultAs<Counter_class>();
                    int cnt = Convert.ToInt32(obj.cnt);

                    while (i <= cnt)
                    {
                        if (i == cnt)
                        {
                            MessageBox.Show("Account does not match to any Admin/Manager Account in the database.");
                            break;
                            
                        }

                        i++;

                        try
                        {
                            FirebaseResponse resp1 = client.Get("Accounts/" + i);
                            User_class obj2 = resp1.ResultAs<User_class>();

                            if (usertxt.Text == obj2.Username)
                            {
                                if (passtxt1.Text == obj2.Password)
                                {
                                    if(obj2.Account_Level=="Admin" || obj2.Account_Level=="Manager")
                                    {
                                        this.Hide();
                                        Form1.status = "true";

                                        if(POS_module.clear2checker=="true")
                                        {
                                            POS_module._instance.clear2();
                                            POS_module.clear2checker = "false";
                                            break;
                                        }
                                        else
                                        {
                                            POS_module._instance.removewithconfirmation();
                                            break;
                                        }

                                       

                                    }
                                    else
                                    {
                                        
                                    }

                                }
                                else
                                {
                                    
                                }
                            }
                            else
                            {

                            }

                        }
                        catch
                        {

                        }
                    }


                    }
                catch
                {

                }


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

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
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

        private void passtxt1_Enter(object sender, EventArgs e)
        {
            if (passtxt1.Text == "Password")
            {
                passtxt1.Text = "";

                passtxt1.ForeColor = Color.Black;

            }
        }

        private void passtxt1_Leave(object sender, EventArgs e)
        {
            if (passtxt1.Text == "")
            {
                passtxt1.Text = "Password";
                passtxt1.ForeColor = Color.Gray;


            }
        }
    }
}
