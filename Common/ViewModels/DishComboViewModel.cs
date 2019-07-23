using System;

namespace Common.ViewModels
{
    public class DishComboViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int? OrderCount { set; get; }
        public int CategoryID { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}