using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("DishComboMapping")]
    public class DishComboMapping
    {
        [Key]
        [Column(Order = 1)]
        public int DishID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ComboID { get; set; }

        public int Amount { get; set; }

        [ForeignKey("DishID")]
        public virtual Dish Dish { set; get; }

        [ForeignKey("ComboID")]
        public virtual Combo Combo { set; get; }
    }
}