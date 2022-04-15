using System;
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

    public VaultsController(VaultsService vs)
    {
      _vs = vs;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
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

    // [HttpGet("{id}")]
    // public ActionResult<Vault> GetById(int id)
    // {
    //   try
    //   {
    //     return Ok(_vs.GetById(id));
    //   }
    //   catch (Exception e)
    //   {

    //     return BadRequest(e.Message);

    //   }
    // }
  }
}