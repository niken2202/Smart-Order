using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int CartID { get; set; }
       
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        [Required]
        [MaxLength(256)]
        public string Image { get; set; }
        public int Status { get; set; }
        public int CateID { get; set; }
        public int Type { get; set; }

        [ForeignKey("CartID")]
        public virtual Cart Cart { get; set; }

    }
}