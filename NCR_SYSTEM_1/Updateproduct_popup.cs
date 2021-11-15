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
  
    public partial class Updateproduct_popup : Form
    {

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "flovfXDWmVoWaFqDWNv7aVwSkVY89OkcXH9Rmj2A",
            BasePath = "https://fir-test-6c417-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public static Updateproduct_popup _instance;
        public Updateproduct_popup()
        {
            InitializeComponent();
            _instance = this;
        }

        private void Updateproduct_popup_Load(object sender, EventArgs e)
        {

            client = new FireSharp.FirebaseClient(config);




            //////////ADDING UNIT COMBOBOX CONTENT
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


            //////////ADDING CATEGORY COMBOBOX CONTENT
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
















            pid.Text = Inventory_Module.id;
            pname.Text = Inventory_Module.name;
            pcategory.Text = Inventory_Module.category;
            pdescription.Text = Inventory_Module.desc;
            pbrand.Text = Inventory_Module.brand;
            pprice.Text = Inventory_Module.price.ToString();
            punit.Text = Inventory_Module.unit;
          

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Updateproductstockindicator a = new Updateproductstockindicator(); 
            a.Show();

        }

        public void update()
        {
            if (MessageBox.Show("Please confirm before proceed" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                try
                {
                    var data = new Product_class2
                    {
                        ID = pid.Text,
                        Product_Name = pname.Text,
                        Unit = punit.Text,
                        Brand = pbrand.Text,
                        Description = pdescription.Text,
                        Category = pcategory.Text,
                        Price = pprice.Text,
                        Low = Updateproductstockindicator.lowtxt,
                        High = Updateproductstockindicator.hightxt,
                    };

                    FirebaseResponse response = client.Update("Inventory/" + data.ID, data);
                    Product_class result = response.ResultAs<Product_class>();



                    this.Hide();
                    Inventory_Module._instance.DataViewAll();

                }

                catch (Exception b)
                {
                    MessageBox.Show(b.ToString());
                }



                //Activity Log UPDATING PRODUCT EVENT

                FirebaseResponse resp4 = client.Get("ActivityLogCounter/node");
                Counter_class get4 = resp4.ResultAs<Counter_class>();
                int cnt4 = (Convert.ToInt32(get4.cnt) + 1);



                var data2 = new ActivityLog_Class
                {
                    Event_ID = cnt4.ToString(),
                    Module = "Inventory Module",
                    Action = "Product-ID: " + pid.Text + "   Product Details Updated",
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



            }

            else

            {
                //do something if NO
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();

          
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
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

        private void bunifuFlatButton2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

class Product_class2
{
    public string ID { get; set; }
    public string Product_Name { get; set; }
    public string Unit { get; set; }
    public string Brand { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Price { get; set; }

    public string Low { get; set; }
    public string High { get; set; }

}