using System.Collections.Generic;
using keeper.Models;
using keeper.Repositories;

namespace keeper.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    internal Keep Create(Keep keepData)
    {
      return _repo.Create(keepData);
    }

    internal List<Keep> GetAll()
    {
      return _repo.GetAll();
    }
  }
}