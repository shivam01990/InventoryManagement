using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class ProductEntity
    {
        public int id { get; set; }
        public string product_name { get; set; }
        public string image_url { get; set; }
        public string brand { get; set; }
        public string category { get; set; }
        public string sub_category { get; set; }
        public string weight { get; set; }
        public decimal cost_price { get; set; }
        public decimal sell_price { get; set; }
        public int? Stock { get; set; }
        public bool status { get; set; }
    }
}
