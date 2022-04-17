using System;
using System.Collections.Generic;
using keeper.Models;
using keeper.Repositories;

namespace keeper.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    private readonly VaultsRepository _vaultsrepo;

    public KeepsService(KeepsRepository repo, VaultsRepository vaultsrepo)
    {
      _repo = repo;
      _vaultsrepo = vaultsrepo;
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

    internal List<VaultViewModel> GetKeepsByVaultId(int id, Profile userInfo)
    {
      Vault found = _vaultsrepo.GetById(id);
      if (found.IsPrivate && found.CreatorId != userInfo?.Id)
      {
        throw new Exception("Can't see private information");
      }
      return _repo.GetKeepsByVaultId(id);
    }
  }
}