using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("Dish")]
    public class Dish
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        [MaxLength]
        public string Description { get; set; }

        [MaxLength(256)]
        public string Image { get; set; }

        public int? OrderCount { set; get; }

        [Required]
        public int CategoryID { get; set; }

        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual IEnumerable<DishComboMapping> DishComboMappings { set; get; }
        public virtual IEnumerable<DishBillMapping> DishBillMappings { set; get; }
        public virtual IEnumerable<DishMaterialMapping> DishMaterialMappings { set; get; }
    }
}