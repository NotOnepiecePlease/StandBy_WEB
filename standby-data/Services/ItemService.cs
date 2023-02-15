using standby_data.Repositories;

namespace standby_data.Services
{
  public class ItemService
  {
    public RepositoryItem repositoryItem { get; set; }

    public ItemService()
    {
      repositoryItem = new RepositoryItem();
    }
  }
}