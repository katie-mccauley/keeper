using System;
using System.Collections.Generic;
using keeper.Models;
using keeper.Services;
using Microsoft.AspNetCore.Mvc;

namespace keeper.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;

    public ProfilesController(ProfilesService ps, KeepsService ks, VaultsService vs)
    {
      _ps = ps;
      _ks = ks;
      _vs = vs;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> GetById(string id)
    {
      try
      {
        return Ok(_ps.GetById(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByUserId(string id)
    {
      try
      {
        List<Keep> keep = _ks.GetKeepsByUserId(id);
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/vaults")]
    public ActionResult<List<Vault>> GetVaultsByUserId(string id)
    {
      try
      {
        // Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        List<Vault> vault = _vs.GetVaultsByUserId(id);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}