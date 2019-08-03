using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("Cart")]
    public class Cart
    {
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Key, Column(Order =2)]
        public int TableID { get; set; }
        public int Type { get; set; }
        public double CartPrice { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }

    }
}
