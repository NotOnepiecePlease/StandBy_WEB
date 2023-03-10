// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace standby_data.Models;

public partial class tb_compras
{
    [Key]
    public int cp_id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime cp_data { get; set; }

    public int cp_sv_id { get; set; }

    [Required]
    [StringLength(500)]
    [Unicode(false)]
    public string cp_peca_comprada { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal cp_valor_peca { get; set; }

    [Required]
    [StringLength(200)]
    [Unicode(false)]
    public string cp_fornecedor { get; set; }

    [ForeignKey("cp_sv_id")]
    [InverseProperty("tb_compras")]
    public virtual tb_servicos cp_sv { get; set; }
}