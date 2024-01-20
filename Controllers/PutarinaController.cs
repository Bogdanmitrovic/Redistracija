using Microsoft.AspNetCore.Mvc;
using NRedisStack;
using Redistracija.Models;
using StackExchange.Redis;

namespace Redistracija.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PutarinaController : ControllerBase
{
    private readonly IDatabase _database;
    private readonly JsonCommands _jsonCommands;

    public PutarinaController(IConnectionMultiplexer connectionMultiplexer)
    {
        _database = connectionMultiplexer.GetDatabase();
        _jsonCommands = new JsonCommands(_database);
    }

    [HttpPost(Name = "KreirajTag")]
    public IActionResult KreirajTag(Tag tag)
    {
        tag.Id ??= Guid.NewGuid().ToString();
        if (TagExists(tag.Id)) return BadRequest("Tag vec postoji");
        var tagDto = new TagDto
        {
            Registracija = tag.Registracija,
            Kredit = 0
        };
        var success = _jsonCommands.Set($"tag:{tag.Id}", "$", tagDto);
        return success ? Ok() : BadRequest("Greska pri kreiranju taga");
    }

    [HttpGet(Name = "GetKredit")]
    public IActionResult GetKredit(string tagId)
    {
        if (!TagExists(tagId)) return BadRequest("Tag ne postoji");
        var kredit = _jsonCommands.Get<int>($"tag:{tagId}", "$.kredit");
        return Ok(kredit);
    }

    [HttpGet(Name = "GetTag")]
    public IActionResult GetTag(string tagId)
    {
        var tag = _jsonCommands.Get<TagDto>($"tag:{tagId}");
        if (tag == null) return BadRequest("Tag ne postoji");
        return Ok(tag);
    }

    [HttpPut(Name = "UplatiKredit")]
    public IActionResult UplatiKredit(Tag tag)
    {
        var existingTag = _jsonCommands.Get<TagDto>($"tag:{tag.Id}");
        if (existingTag == null) return BadRequest("Tag ne postoji");
        existingTag.Kredit += tag.Kredit;
        var success = _jsonCommands.Set($"tag:{tag.Id}", "$.kredit", existingTag.Kredit);
        return success ? Ok() : BadRequest("Greska pri uplati kredita");
    }

    [HttpPut(Name = "ZameniRegistraciju")]
    public IActionResult ZameniRegistraciju(string tagId, string novaRegistracija)
    {
        var existingTag = _jsonCommands.Get<TagDto>($"tag:{tagId}");
        if (existingTag == null) return BadRequest("Tag ne postoji");
        existingTag.Registracija = novaRegistracija;
        var success = _jsonCommands.Set($"tag:{tagId}", "$", existingTag);
        return success ? Ok() : BadRequest("Greska pri zameni registracije");
    }

    [HttpDelete(Name = "ObrisiTag")]
    public IActionResult ObrisiTag(string tagId)
    {
        if (!TagExists(tagId)) return BadRequest("Tag ne postoji");
        var success = _database.KeyDelete($"tag:{tagId}");
        return success ? Ok() : BadRequest("Greska pri brisanju taga");
    }

    private bool TagExists(string id)
    {
        return _database.KeyExists($"tag:{id}");
    }
}