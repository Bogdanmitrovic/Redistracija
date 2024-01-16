using Microsoft.AspNetCore.Mvc;
using NRedisStack;
using Redistracija.Models;
using StackExchange.Redis;

namespace Redistracija.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class RampaController : Controller
{
    private readonly JsonCommands _jsonCommands;

    public RampaController(IConnectionMultiplexer connectionMultiplexer)
    {
        var database = connectionMultiplexer.GetDatabase();
        _jsonCommands = new JsonCommands(database);
    }
    
    [HttpPut(Name="SkiniKredit")]
    public IActionResult SkiniKredit(Tag tag)
    {
        var existingTag = _jsonCommands.Get<TagDto>($"tag:{tag.Id}");
        if (existingTag == null) return BadRequest("Tag ne postoji");
        existingTag.Kredit -= tag.Kredit;
        bool success = _jsonCommands.Set($"tag:{tag.Id}", "$.kredit", existingTag.Kredit);
        return success ? Ok(existingTag.Kredit) : BadRequest("Greska pri skidanju kredita");
    }
}