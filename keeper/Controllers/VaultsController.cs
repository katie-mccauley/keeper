using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keeper.Models;
using keeper.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keeper.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly KeepsService _ks;

    public VaultsController(VaultsService vs, KeepsService ks)
    {
      _vs = vs;
      _ks = ks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        vaultData.CreatorId = userInfo.Id;
        Vault vault = _vs.Create(vaultData);
        vault.Creator = userInfo;
        return Created($"api/vaults/{vault.Id}", vault);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);

      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> GetById(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();

        return Ok(_vs.GetById(id, userInfo?.Id));
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);

      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Update([FromBody] Vault updateData, int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        updateData.Id = id;
        updateData.CreatorId = userInfo.Id;
        Vault vault = _vs.Update(updateData);
        return Ok(vault);

      }
      catch (Exception e)
      {

        return BadRequest(e.Message);

      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Remove(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return _vs.Remove(id, userInfo.Id);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);

      }
    }
    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<VaultViewModel>>> GetKeepsByVaultId(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        List<VaultViewModel> keeps = _ks.GetKeepsByVaultId(id, userInfo);
        return Ok(keeps);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);

      }
    }
  }
}