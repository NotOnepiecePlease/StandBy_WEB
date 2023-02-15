using standby_data.Interfaces;
using standby_data.Models;

namespace standby_data.Repositories
{
  public class RepositoryItem : RepositoryBase<tb_comp_items>, IRepositoryItems
  {
    public RepositoryItem(bool _saveChanges = true) : base(_saveChanges)
    {
    }

    public List<string> BuscarItems(string _tela, string _grupo, string _nome)
    {
      var lista = context.tb_comp_items.Where(x =>
            x.item_tela == _tela &&
            x.item_grupo == _grupo &&
            x.item_nome == _nome).Select(x => x.item_texto).ToList();
      return lista;
    }
  }
}