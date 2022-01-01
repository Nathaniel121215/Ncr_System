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
    public partial class Addnewproduct_popup : Form
    {
        string fmt = "##.00";

        IFirebaseClient client;


        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"


        };

        public static Addnewproduct_popup _instance;
        public Addnewproduct_popup()
        {
            InitializeComponent();
            _instance = this;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Addnewproduct_popup_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);


           

            //////ADDING UNIT COMBOBOX CONTENT
            try
            {
                FirebaseResponse resp3 = client.Get("UnitCounter/node");
                Counter_class obj = resp3.ResultAs<Counter_class>();
                int cnt = Convert.ToInt32(obj.cnt);

                List<string> unit = new List<string>();
                for (int runs = 0; runs <= cnt; runs++)
                {
                    try
                    {
                        FirebaseResponse resp1 = client.Get("Unit/" + runs);
                        Unit_Class obj2 = resp1.ResultAs<Unit_Class>();

                        unit.Add(obj2.Unit_Name);
                    }
                    catch
                    {

                    }

                }



                for (int i = 0; i <= cnt; i++)
                {

                    punit.Items.Add(unit[i].ToString());


                }
            }
            catch
            {
            }


            //////ADDING CATEGORY COMBOBOX CONTENT
            try
            {
                FirebaseResponse resp3 = client.Get("CategoryCounter/node");
                Counter_class obj = resp3.ResultAs<Counter_class>();
                int cnt = Convert.ToInt32(obj.cnt);

                List<string> category = new List<string>();
                for (int runs = 0; runs <= cnt; runs++)
                {
                    try
                    {
                        FirebaseResponse resp1 = client.Get("Category/" + runs);
                        Category_Class obj2 = resp1.ResultAs<Category_Class>();

                        category.Add(obj2.Category_Name);
                    }
                    catch
                    {

                    }

                }



                for (int i = 0; i <= cnt; i++)
                {

                    pcategory.Items.Add(category[i].ToString());


                }
            }
            catch
            {
            }



           
            //Getting the current counter

            FirebaseResponse resp = client.Get("Counter2/node");

            Counter_class get = resp.ResultAs<Counter_class>();

            var data = new Product_class
            {
                ID = (Convert.ToInt32(get.cnt) + 1).ToString(),

            };


            pid.Text = data.ID;

            namemetro(pname);
            brandmetro(pbrand);
          


        }


        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            decimal n = Convert.ToDecimal(pprice.Text);
            pprice.Text = n.ToString(fmt);

            if (pname.Text!="" && pprice.Text != "" && pbrand.Text != "" && pdescription.Text != "" && pcategory.Text != "" && punit.Text != "" && pprice.Text != "0.00")
            {
                Addproductstockindicator_popup a = new Addproductstockindicator_popup();
                a.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Fill up all necessary fields.");
            }
               
         


        


        }
        public void save()
        {
            if (MessageBox.Show("Please confirm before proceed" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                try
                {


                    var data = new Product_class
                    {
                        ID = pid.Text,
                        Product_Name = pname.Text,
                        Unit = punit.Text,
                        Brand = pbrand.Text,
                        Description = pdescription.Text,
                        Category = pcategory.Text,
                        Price = pprice.Text,
                        Items_Sold = "0",
                        Stock = "0",
                        Low = Addproductstockindicator_popup.lowtxt,
                        High = Addproductstockindicator_popup.hightxt,
                    };



                    FirebaseResponse response = client.Set("Inventory/" + data.ID, data);
      


                    var obj = new Counter_class
                    {
                        cnt = data.ID
                        
                    };

                    SetResponse response1 = client.Set("Counter2/node", obj);




                    FirebaseResponse resp3 = client.Get("inventoryCounterExisting/node");
                    Counter_class gett = resp3.ResultAs<Counter_class>();
                    int exist = (Convert.ToInt32(gett.cnt) + 1);
                    var obj2 = new Counter_class
                    {
                        cnt = exist.ToString()
                    };


                    SetResponse response2 = client.Set("inventoryCounterExisting/node", obj2);

                    //Activity Log ADDING PRODUCT EVENT

                    FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                    Counter_class get4 = resp4.ResultAs<Counter_class>();
                    int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                    var data2 = new ActivityLog_Class
                    {
                        Event_ID = cnt4.ToString(),
                        Module ="Inventory Management Module",
                        Action ="Product-ID: "+pid.Text+"   New Product Added",
                        Date = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                        User = Form1.username,
                        Accountlvl = Form1.levelac,

                    };



                    FirebaseResponse response4 = client.Set("ActivityLog/" + data2.Event_ID, data2);



                    var obj4 = new Counter_class
                    {
                        cnt = data2.Event_ID

                    };

                    SetResponse response5 = client.Set("ActivityLogCounter/node", obj4);

                    /////////////////////////////////////////


                    this.Hide();
                    Inventory_Module._instance.DataViewAll();

                }

                catch (Exception b)
                {
                    MessageBox.Show(b.ToString());
                }
            }

            else

            {
                //do something if NO
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1.status = "true";
            
        }

        public void refreshcategory()
        {
            pcategory.Items.Clear();

            try
            {
                FirebaseResponse resp3 = client.Get("CategoryCounter/node");
                Counter_class obj = resp3.ResultAs<Counter_class>();
                int cnt = Convert.ToInt32(obj.cnt);

                List<string> category = new List<string>();
                for (int runs = 0; runs <= cnt; runs++)
                {
                    try
                    {
                        FirebaseResponse resp1 = client.Get("Category/" + runs);
                        Category_Class obj2 = resp1.ResultAs<Category_Class>();

                        category.Add(obj2.Category_Name);
                    }
                    catch
                    {

                    }

                }



                for (int i = 0; i <= cnt; i++)
                {

                    pcategory.Items.Add(category[i].ToString());


                }
            }
            catch
            {
            }

        }

        public void refreshunit()
        {
            punit.Items.Clear();

            try
            {
                FirebaseResponse resp3 = client.Get("UnitCounter/node");
                Counter_class obj = resp3.ResultAs<Counter_class>();
                int cnt = Convert.ToInt32(obj.cnt);

                List<string> unit = new List<string>();
                for (int runs = 0; runs <= cnt; runs++)
                {
                    try
                    {
                        FirebaseResponse resp1 = client.Get("Unit/" + runs);
                        Unit_Class obj2 = resp1.ResultAs<Unit_Class>();

                        unit.Add(obj2.Unit_Name);
                    }
                    catch
                    {

                    }

                }



                for (int i = 0; i <= cnt; i++)
                {

                    punit.Items.Add(unit[i].ToString());


                }
            }
            catch
            {
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Addcategory_popup a = new Addcategory_popup();
            a.Show();

            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Addunit_popup a = new Addunit_popup();
            a.Show();
        }

        private void bunifuFlatButton2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1.status = "true";
        }

        private void errormessage_Click(object sender, EventArgs e)
        {

        }

        private void pprice_Enter(object sender, EventArgs e)
        {
            if(pprice.Text=="0.00")
            {
                pprice.Text = "";
            }
            else
            {
                
            }
        }

        private void pprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void pprice_Leave(object sender, EventArgs e)
        {
           
            if (pprice.Text == "")
            {
                pprice.Text = "0.00";

                decimal a = Convert.ToDecimal(pprice.Text);
                pprice.Text = a.ToString(fmt);
            }
            else
            {
                decimal a = Convert.ToDecimal(pprice.Text);
                pprice.Text = a.ToString(fmt);
                if (pprice.Text == ".00")
                {
                    pprice.Text = "0.00";
                }
            }
        }

        private void pprice_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decimal a = Convert.ToDecimal(pprice.Text);
                pprice.Text = a.ToString(fmt);
            }
        }

        private void namemetro(Bunifu.Framework.UI.BunifuMetroTextbox metroTextbox)

        {
            foreach (var ctl in metroTextbox.Controls)
            {

                if (ctl.GetType() == typeof(TextBox))

                {
                    var txt = (TextBox)ctl;
                    txt.MaxLength = 25;
               
                }

            }

        }
        private void brandmetro(Bunifu.Framework.UI.BunifuMetroTextbox metroTextbox)

        {
            foreach (var ctl in metroTextbox.Controls)
            {

                if (ctl.GetType() == typeof(TextBox))

                {
                    var txt = (TextBox)ctl;
                    txt.MaxLength = 25;

                }

            }

        }

       
    }


}
