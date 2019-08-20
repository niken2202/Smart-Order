using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("PromotionCode")]
    public class PromotionCode
    {
        [Key]
        [MaxLength(256)]
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiredDate { get; set; }

        [Required]
        public int Discount { set; get; }
        [Required]
        public int Times { set; get; }
        public bool Status { get; set; }
    }
}