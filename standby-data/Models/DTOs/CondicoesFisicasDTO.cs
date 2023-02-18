using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace standby_data.Models.DTOs
{
  public class CondicoesFisicasDTO
  {
    [Key]
    public int cf_id { get; set; }

    [Required(ErrorMessage = "Precisa informar a ordem de serviço!")]
    public int cf_ordem_serv { get; set; }

    [Required(ErrorMessage = "Precisa informar a data!")]
    [Column(TypeName = "datetime")]
    public DateTime cf_data { get; set; }

    [Required(ErrorMessage = "Precisa informar o serviço!")]
    public int cf_sv_idservico { get; set; }

    [Required(ErrorMessage = "Precisa informar o tipo!")]
    [StringLength(50)]
    [Unicode(false)]
    public string cf_tipo { get; set; }


    [StringLength(50)]
    [Unicode(false)]
    public string? cf_pelicula { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? cf_tela { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? cf_tampa { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? cf_aro { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? cf_botoes { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? cf_lente_camera { get; set; }
  }
}