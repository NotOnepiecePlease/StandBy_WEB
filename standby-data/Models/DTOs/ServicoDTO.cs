using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace standby_data.Models.DTOs
{
  public class ServicoDTO
  {
    [Key]
    public int sv_id { get; set; }

    [Required(ErrorMessage = "Precisa informar a ordem de serviço!")]
    public int sv_ordem_serv { get; set; }

    [Required(ErrorMessage = "Precisa informar a data!")]
    [Column(TypeName = "datetime")]
    public DateTime sv_data { get; set; }

    [Required(ErrorMessage = "Precisa informar o cliente!")]
    [Range(0, int.MaxValue, ErrorMessage = "O campo {0} deve ser um número inteiro positivo.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "O campo {0} deve ser um número inteiro positivo.")]
    public int? sv_cl_idcliente { get; set; }

    [Required(ErrorMessage = "Precisa informar o tipo de aparelho!")]
    [StringLength(50)]
    [Unicode(false)]
    public string sv_tipo_aparelho { get; set; } = "CELULAR";

    [StringLength(50)]
    [Unicode(false)]
    public string? sv_marca { get; set; }

    [Required(ErrorMessage = "Precisa informar o aparelho!")]
    [StringLength(50)]
    [Unicode(false)]
    public string sv_aparelho { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? sv_cor { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? sv_mei_serialnumber { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    public string? sv_defeito { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    public string? sv_servico { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? sv_senha { get; set; }

    [StringLength(8000)]
    [Unicode(false)]
    public string? sv_situacao { get; set; }

    [StringLength(8000)]
    [Unicode(false)]
    public string? sv_acessorios { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? sv_valorservico { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? sv_valorpeca { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? sv_lucro { get; set; }

    public int? sv_status { get; set; }

    [Required(ErrorMessage = "Precisa informar se o serviço está ativo!")]
    public int sv_ativo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? sv_data_conclusao { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? sv_previsao_entrega { get; set; }

    public int? sv_existe_um_prazo { get; set; }

    [Column(TypeName = "image")]
    public byte[]? sv_senha_pattern { get; set; }

    public int? sv_cor_tempo { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? sv_tempo_para_entregar { get; set; }

    [StringLength(8000)]
    [Unicode(false)]
    public string? sv_relato_cliente { get; set; }

    [StringLength(5000)]
    [Unicode(false)]
    public string? sv_condicoes_balcao { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? sv_avaliacao_servico { get; set; }
  }
}