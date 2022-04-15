using keeper.Models;
using keeper.Repositories;

namespace keeper.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }

    internal Vault Create(Vault vaultData)
    {
      return _repo.Create(vaultData);
    }

    // internal Vault GetById(int id)
    // {
    //   Vault vault = _repo.GetById(id);
    //   if(found == null){
    //     throw new Exception("No Keep post by that id");
    //   }
    // }
  }
}