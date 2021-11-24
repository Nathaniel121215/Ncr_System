using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_SYSTEM_1
{
    public partial class Preloader : UserControl
    {
        public Preloader()
        {
            InitializeComponent();

        }

        int dir = 1;

      
        private void Preloader_Load(object sender, EventArgs e)
        {

        }

        


        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (bunifuCircleProgressbar1.Value == 90)
            {
                dir = -1;
          
            }
            else if (bunifuCircleProgressbar1.Value == 10)
            {
                dir = +1;
           
            }

            bunifuCircleProgressbar1.Value += dir;
        }
    }
}
