using System.Text.Json.Serialization;

namespace Redistracija.Models;

public class TagDto
{
    [JsonPropertyName("registracija")] public string? Registracija { get; set; }

    [JsonPropertyName("kredit")] public int Kredit { get; set; }
}