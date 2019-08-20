using System;
using System.Collections.Generic;

namespace Common.ViewModels
{
    public class ComboViewModel
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<DishComboViewModel> dishes{get;set;}
        public bool Status { get; set; }
    }
}