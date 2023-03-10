using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace standby_data.Models.DTOs
{
  public class ClienteDTO
  {
    [Key]
    public int cl_id { get; set; }
    [Required(ErrorMessage = "Nome do cliente está vazio.")]
    [StringLength(100)]
    [MinLength(4, ErrorMessage = "O Nome deve ter no mínimo 3 caracteres.")]
    [Unicode(false)]
    public string cl_nome { get; set; }
    [Required(ErrorMessage = "Telefone principal está vazio.")]
    [StringLength(15)]
    [MinLength(15, ErrorMessage = "Preencha o telefone principal corretamente : (xx) x xxxx-xxxx")]
    [Unicode(false)]
    public string cl_telefone { get; set; }
    [Required(ErrorMessage = "CPF/CNPJ está vazio.")]
    [StringLength(18)]
    [MinLength(14, ErrorMessage = "O Campo de 'CPF/CNPJ' deve ter no minimo 14 digitos.")]
    [Unicode(false)]
    public string cl_cpf { get; set; }
    [Required(ErrorMessage = "Telefone de recado está vazio.")]
    [StringLength(100)]
    [MinLength(15, ErrorMessage = "Preencha o telefone de recados corretamente : (xx) x xxxx-xxxx")]
    [Unicode(false)]
    public string cl_telefone_recado { get; set; }
    [StringLength(100)]
    [MinLength(4, ErrorMessage = "O Nome do contato deve ter no mínimo 3 caracteres.")]
    [Unicode(false)]
    public string? cl_nome_recado { get; set; }
    [StringLength(50)]
    [MinLength(3, ErrorMessage = "O Parentesco deve ter no mínimo 3 caracteres.")]
    [Unicode(false)]
    public string? cl_parentesco_recado { get; set; }
    [StringLength(1)]
    [Unicode(false)]
    [MinLength(1, ErrorMessage = "Sexo deve possuir apenas 1 digito.")]
    public string? cl_sexo { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? cl_data_nascimento { get; set; }
    [StringLength(10)]
    [MinLength(9, ErrorMessage = "O CEP precisa ter no minimo 9 digitos.")]
    [Unicode(false)]
    public string? cl_cep { get; set; }
    [StringLength(200)]
    [Unicode(false)]
    public string? cl_endereco { get; set; }
    [StringLength(100)]
    [Unicode(false)]
    public string? cl_complemento { get; set; }
    [StringLength(50)]
    [Unicode(false)]
    public string? cl_bairro { get; set; }
    [StringLength(50)]
    [Unicode(false)]
    public string? cl_cidade { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? cl_estado { get; set; } = null;
  }
}