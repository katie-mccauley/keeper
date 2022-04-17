using System;
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
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;
    public VaultKeepsService(VaultKeepsRepository repo, KeepsService ks, VaultsService vs)
    {
      _repo = repo;
      _ks = ks;
      _vs = vs;
    }


    internal VaultKeep GetVaultKeepById(int id)
    {
      VaultKeep found = _repo.GetVaultKeepById(id);
      if (found == null)
      {
        throw new Exception("invalid id");
      }
      return found;
    }
    internal VaultKeep Create(VaultKeep vaultKeepData, Profile userInfo)
    {
      Keep keep = _ks.GetById(vaultKeepData.KeepId);
      Vault vault = _vs.GetById(vaultKeepData.VaultId, userInfo.Id);
      if (vaultKeepData.CreatorId != userInfo?.Id)
      {
        throw new Exception("can't create a vk that isn't yours");
      }
      else if (vault.CreatorId != userInfo?.Id)
      {
        throw new Exception("can't create vk of a vault that isn't yours");
      }
      else if (keep.CreatorId != userInfo?.Id)
      {
        throw new Exception("can't create vk of a keep that isn't yours");
      }

      return _repo.Create(vaultKeepData);
    }

    internal void Delete(int id, string userId)
    {
      VaultKeep found = GetVaultKeepById(id);
      if (found.CreatorId != userId)
      {
        throw new Exception("can't deelte a vaultkeep that isnt yours");
      }
      _repo.Delete(id);
    }
  }
}