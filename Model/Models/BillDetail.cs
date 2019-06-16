using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("BillDetails")]
    public class BillDetail
    {
        private decimal total;
        [Key]
        public int BillID { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Amount { get; set; }

        public decimal Total
        {
            get
            {
                return total;
            }
            set
            {
                total = Price * Amount;
            }
        }


        [MaxLength]
        public string Description { get; set; }

        [MaxLength(256)]
        public string Image { get; set; }

        [ForeignKey("BillID")]
        public virtual Bill Bill { set; get; }
    }
}