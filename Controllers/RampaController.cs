using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using NRedisStack.Search;
using Redistracija.Models;
using StackExchange.Redis;

namespace Redistracija.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class RampaController : Controller
{
    private readonly SearchCommands _ft;
    private readonly JsonCommands _jsonCommands;

    public RampaController(IConnectionMultiplexer connectionMultiplexer)
    {
        var database = connectionMultiplexer.GetDatabase();
        _ft = database.FT();
        _jsonCommands = new JsonCommands(database);
    }

    [HttpGet(Name = "OtvoriRampu")]
    public IActionResult OtvoriRampu(string tagId)
    {
        var tag = _jsonCommands.Get<TagDto>($"tag:{tagId}");
        if (tag == null) return BadRequest("Tag ne postoji");
        return tag.Kredit > 0 ? Ok() : BadRequest("Nema dovoljno kredita");
    }

    [HttpGet(Name = "OtvoriRampuPoRegistraciji")]
    public IActionResult OtvoriRampuPoRegistraciji(string registracija)
    {
        var result = _ft.Search("idx:tag", new Query("@reg:{" + registracija + "}")).Documents;
        if (result.Count == 0) return BadRequest("Tag ne postoji");
        var tag = JsonSerializer.Deserialize<TagDto>(result.First().GetProperties().First().Value.ToString());
        if (tag == null) return BadRequest("Tag ne postoji");
        return tag.Kredit > 0 ? Ok() : BadRequest("Nema dovoljno kredita");
    }

    [HttpGet(Name = "NadjiTagIdPoRegistraciji")]
    public IActionResult NadjiTagIdPoRegistraciji(string registracija)
    {
        var result = _ft.Search("idx:tag", new Query("@reg:{" + registracija + "}")).Documents;
        if (result.Count == 0) return BadRequest("Tag ne postoji");
        var tag = result.First().Id;
        return Ok(tag);
    }

    [HttpPut(Name = "SkiniKreditPoRegistraciji")]
    public IActionResult SkiniKreditPoRegistraciji(string registracija, int kredit)
    {
        var result = _ft.Search("idx:tag", new Query("@reg:{" + registracija + "}")).Documents;
        if (result.Count == 0) return BadRequest("Tag ne postoji");
        var tag = result.First().Id;
        var existingTag = _jsonCommands.Get<TagDto>(tag);
        if (existingTag == null) return BadRequest("Tag ne postoji");
        existingTag.Kredit -= kredit;
        var success = _jsonCommands.Set(tag, "$.kredit", existingTag.Kredit);
        return success ? Ok(existingTag.Kredit) : BadRequest("Greska pri skidanju kredita");
    }

    [HttpPut(Name = "SkiniKredit")]
    public IActionResult SkiniKredit(Tag tag)
    {
        var existingTag = _jsonCommands.Get<TagDto>($"tag:{tag.Id}");
        if (existingTag == null) return BadRequest("Tag ne postoji");
        existingTag.Kredit -= tag.Kredit;
        var success = _jsonCommands.Set($"tag:{tag.Id}", "$.kredit", existingTag.Kredit);
        return success ? Ok(existingTag.Kredit) : BadRequest("Greska pri skidanju kredita");
    }
}