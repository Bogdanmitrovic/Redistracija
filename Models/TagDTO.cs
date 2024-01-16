using System.Text.Json.Serialization;

namespace Redistracija.Models;

public class TagDto
{
    [JsonPropertyName(name:"registracija")]
    public string? Registracija { get; set; }
    [JsonPropertyName(name:"kredit")]
    public int Kredit { get; set; }
}