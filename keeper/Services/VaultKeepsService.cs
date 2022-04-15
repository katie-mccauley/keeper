using keeper.Models;
using keeper.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keeper.Services
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsService : ControllerBase
  {
    private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData)
    {
      return _repo.Create(vaultKeepData);
    }

    internal void Delete(int id, string userId)
    {
      _repo.Delete(id);
    }
  }
}