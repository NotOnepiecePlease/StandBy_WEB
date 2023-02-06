﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace standby_data.Models
{
    public partial class tb_orcamento
    {
        [Key]
        public int orc_id { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string orc_aparelho { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string orc_modelo { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal orc_peca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal orc_valor { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal total { get; set; }
    }
}