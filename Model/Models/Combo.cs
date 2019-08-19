﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("Combo")]
    public class Combo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        [MaxLength(256)]
        public string Image { get; set; }

        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual IEnumerable<DishComboMapping> DishComboMappings { set; get; }
       
    }
}