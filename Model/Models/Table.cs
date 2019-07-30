using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("Table")]
    public class Table
    {
        [Key,Column(Order =1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get;}
        [Key, Column(Order = 2)]
        public string DeviceID { get; set; }
        public int Status { get; set; }
    }
}