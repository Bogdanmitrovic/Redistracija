using System.Text.Json.Serialization;

namespace Redistracija.Models;

public class Tag
{
    [JsonPropertyName("id")] public string? Id { get; set; }

    [JsonPropertyName("registracija")] public string? Registracija { get; set; }

    [JsonPropertyName("kredit")] public int Kredit { get; set; }
}