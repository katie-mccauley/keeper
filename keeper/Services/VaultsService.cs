using System;
using System.Collections.Generic;
using keeper.Models;
using keeper.Repositories;
using Microsoft.AspNetCore.Mvc;

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

    internal Vault GetById(int id, string userId)
    {
      Vault found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("No Keep post by that id");
      }
      if (userId != found.CreatorId && found.IsPrivate)
      {
        throw new Exception("Invalid Id");
      }

      return found;
    }

    internal Vault Update(Vault updateData)
    {
      Vault original = GetById(updateData.Id, updateData.CreatorId);
      if (updateData.CreatorId != original.CreatorId)
      {
        throw new Exception("You cannot edit a vault that isnt yours");
      }
      original.Name = updateData.Name ?? original.Name;
      original.Description = updateData.Description ?? original.Description;
      original.Img = updateData.Img ?? original.Img;
      //   original.IsPrivate = updateData.IsPrivate ?? original.IsPrivate;

      return _repo.Update(original);
    }

    internal List<Vault> GetMyVaults(string userId)
    {
      List<Vault> vault = _repo.GetVaultsByUserId(userId);
      return vault;
    }

    internal List<Vault> GetVaultsByUserId(string id)
    {
      return _repo.GetVaultsByUserId(id);
    }

    internal ActionResult<string> Remove(int id, string userId)
    {
      Vault original = GetById(id, userId);
      if (userId != original.CreatorId)
      {
        throw new Exception("You cannot delete a vault that isnt yours");

      }
      return _repo.Remove(id);
    }
  }
}