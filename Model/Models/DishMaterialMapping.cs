using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("DishMaterialMapping")]
    public class DishMaterialMapping
    {
        [Key]
        [Column(Order = 1)]
        public int DishID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int MaterialID { get; set; }

        [ForeignKey("DishID")]
        public virtual Dish Dish { set; get; }

        [ForeignKey("MaterialID")]
        public virtual Material Material { set; get; }

    }
}