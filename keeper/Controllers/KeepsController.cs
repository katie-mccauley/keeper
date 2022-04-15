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
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;

    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep keepData)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        keepData.CreatorId = userInfo.Id;
        Keep keep = _ks.Create(keepData);
        keep.Creator = userInfo;
        return Created($"api/keeps/{keep.Id}", keep);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);

      }
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetAllPosts()
    {
      try
      {
        List<Keep> keeps = _ks.GetAll();
        return Ok(keeps);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);

      }
    }
    [HttpGet("{id}")]
    public ActionResult<Keep> GetById(int id)
    {
      try
      {
        return Ok(_ks.GetById(id));
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);

      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Update([FromBody] Keep updateData, int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        updateData.Id = id;
        updateData.CreatorId = userInfo.Id;
        Keep keep = _ks.Update(updateData);
        return Ok(keep);

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
        return _ks.Remove(id, userInfo.Id);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);

      }
    }
  }
}