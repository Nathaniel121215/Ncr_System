using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCR_SYSTEM_1
{
    class InventoryArchive_Class
    {
        public string ID { get; set; }
        public string Product_Name { get; set; }
        public string Unit { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
        public string Items_Sold { get; set; }
        public string Stock { get; set; }
        public string Low { get; set; }
        public string High { get; set; }


        public string Date_Archived { get; set; }
        public string User { get; set; }
    }
}
