using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class CartDetailViewModel
    {
        public int ID { get; set; }
        public int CartID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }
        public int ProID { get; set; }
        public int Type { get; set; }
        public string Note { get; set; }
        public string TableName { get; set; }
        public int TableID { get; set; }

    }
}
