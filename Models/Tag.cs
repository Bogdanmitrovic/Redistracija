using System.Text.Json.Serialization;

namespace Redistracija.Models;

public class Tag
{
    [JsonPropertyName(name:"id")]
    public string? Id { get; set; }
    [JsonPropertyName(name:"registracija")]
    public string? Registracija { get; set; }
    [JsonPropertyName(name:"kredit")]
    public int Kredit { get; set; }
}