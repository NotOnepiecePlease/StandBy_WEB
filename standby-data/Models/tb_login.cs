﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace standby_data.Models
{
    public partial class tb_login
    {
        [Key]
        public int lg_id { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string lg_usuario { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string lg_senha { get; set; }
        public int lg_nivel_acesso { get; set; }
    }
}