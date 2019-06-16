using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    [Table("Material")]

    public class Material
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        [MaxLength(50)]
        public string Unit { get; set; }
        public virtual IEnumerable<DishMaterialMapping> DishMaterialMappings { set; get; }

    }
}
