using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SlayTheStats.Models;

public class Fight 
{
    [JsonPropertyName("damage")]
    public float DamageTaken { get; set; }

    [JsonPropertyName("enemies")]
    public string Enemy { get; set; }
}