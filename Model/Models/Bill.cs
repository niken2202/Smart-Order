using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("Bill")]
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(256)]
        public string Voucher { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength]
        public string Content { get; set; }
        public int TableID { get; set; }
        public DateTime CreatedDate { get; set; }

        [Required]
        public decimal Total { get; set; }
        public string CreatedBy { set; get; }
        public int? Discount { set; get; }
        public bool Status { get; set; }
        public virtual IEnumerable<BillDetail> BillDetail { set; get; }

    }
}