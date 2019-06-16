using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("DishBillMapping")]
    public class DishBillMapping
    {
        [Key]
        [Column(Order = 1)]
        public int DishID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int BillID { get; set; }

        [ForeignKey("DishID")]
        public virtual Dish Dish { set; get; }

        [ForeignKey("BillID")]
        public virtual Bill Bill { set; get; }

        public virtual IEnumerable<DishBillMapping> DishBillMappings { set; get; }

    }
}