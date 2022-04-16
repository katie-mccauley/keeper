using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using keeper.Models;
using keeper.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keeper.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly VaultsService _vs;

    public AccountController(AccountService accountService, VaultsService vs)
    {
      _accountService = accountService;
      _vs = vs;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("vaults")]
    [Authorize]
    public async Task<ActionResult<List<Vault>>> GetMyVaults()
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_vs.GetMyVaults(userInfo.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }


}