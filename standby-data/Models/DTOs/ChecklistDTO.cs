using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace standby_data.Models.DTOs
{
  public class ChecklistDTO
  {
    [Key]
    public int ch_id { get; set; }

    public int ch_ordem_serv { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ch_data { get; set; }

    public int ch_sv_idservico { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ch_tipo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ch_biometria_faceid { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ch_microfone { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ch_tela { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ch_chip { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ch_botoes { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ch_sensor { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ch_cameras { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ch_auricular { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ch_wifi { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ch_altofalante { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ch_bluetooth { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ch_carregamento { get; set; }

    [StringLength(5000)]
    [Unicode(false)]
    public string ch_observacoes { get; set; }

    public bool ch_ausente { get; set; }

    [StringLength(5000)]
    [Unicode(false)]
    public string ch_motivo_ausencia { get; set; }

    [ForeignKey("ch_sv_idservico")]
    [InverseProperty("tb_checklist")]
    public virtual tb_servicos ch_sv_idservicoNavigation { get; set; }
  }
}