using standby_data.Models.DTOs;

namespace standby_data.Models.UtilModels
{
  //[Bind(include: "cl_id, cl_nome, cl_telefone, cl_telefone_recado")]
  public class AdicionarServicoModel
  {
    public ServicoDTO servico { get; set; }
    public ClienteDTO? cliente { get; set; }
    public CondicoesFisicasDTO condicaoFisica { get; set; }
    public tb_checklist checkList { get; set; }
    public ListasItems ListasItems { get; set; } = new ListasItems();
  }

  public class ListasItems
  {
    //Informacoes do aparelho
    public List<string> InMarcaItemsList { get; set; } = new List<string>();
    public List<string> InCorItemsList { get; set; } = new List<string>();

    //Condicoes Fisicas
    public List<string> CfPeliculaItemsList { get; set; } = new List<string>();
    public List<string> CfTelaItemsList { get; set; } = new List<string>();
    public List<string> CfTampaItemsList { get; set; } = new List<string>();
    public List<string> CfAroItemsList { get; set; } = new List<string>();
    public List<string> CfBotoesItemsList { get; set; } = new List<string>();
    public List<string> CfLenteCamItemsList { get; set; } = new List<string>();

    //CheckList
    public List<string> ChBiometriaFaceItemsList { get; set; } = new List<string>();
    public List<string> ChMicrofoneItemsList { get; set; } = new List<string>();
    public List<string> ChTelaItemsList { get; set; } = new List<string>();
    public List<string> ChChipItemsList { get; set; } = new List<string>();
    public List<string> ChBotoesList { get; set; } = new List<string>();
    public List<string> ChSensorList { get; set; } = new List<string>();
    public List<string> ChCamerasList { get; set; } = new List<string>();
    public List<string> ChAuricularList { get; set; } = new List<string>();
    public List<string> ChWifiList { get; set; } = new List<string>();
    public List<string> ChAltoFalanteList { get; set; } = new List<string>();
    public List<string> ChBluetoothList { get; set; } = new List<string>();
    public List<string> ChCarregamentoList { get; set; } = new List<string>();
  }
}