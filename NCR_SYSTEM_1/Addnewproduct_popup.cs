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
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Addproductstockindicator_popup a = new Addproductstockindicator_popup();
            a.Show();


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
                    Product_class result = response.ResultAs<Product_class>();


                    FirebaseResponse resp = client.Get("Counter2/node");
                    Counter_class get = resp.ResultAs<Counter_class>();


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
            Inventory_Module.checker = "allow";
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
            Inventory_Module.checker = "allow";
        }
    }
}
