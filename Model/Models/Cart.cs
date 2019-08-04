using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int TableID { get; set; }
        public double CartPrice { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }

    }
}
