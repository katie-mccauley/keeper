using System;
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

    internal Keep GetById(int id)
    {
      Keep found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("No Keep post by that id");
      }
      return found;
    }

    internal Keep Update(Keep updateData)
    {
      Keep original = GetById(updateData.Id);
      if (updateData.CreatorId != original.CreatorId)
      {
        throw new Exception("You cannot edit a keep that isnt yours");

      }
      original.Name = updateData.Name ?? original.Name;
      original.Description = updateData.Description ?? original.Description;
      original.Img = updateData.Img ?? original.Img;
      return _repo.Update(original);
    }

    internal List<Keep> GetKeepsByUserId(string id)
    {
      return _repo.GetKeepsByUserId(id);
    }

    internal string Remove(int id, string userId)
    {
      Keep original = GetById(id);
      if (userId != original.CreatorId)
      {
        throw new Exception("You cannot delete a keep that isnt yours");

      }
      return _repo.Remove(id);
    }

    internal List<VaultViewModel> GetKeepsByVaultId(int id)
    {
      return _repo.GetKeepsByVaultId(id);
    }
  }
}