using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SlayTheStats.Models;
public class Run
{
    [JsonPropertyName("ascension_level")]
    public int Ascension { get; set; }

    [JsonPropertyName("relics")]
    public List<string> Relics { get; set; }

    [JsonPropertyName("victory")]
    public bool Victory { get; set; }

    [JsonPropertyName("character_chosen")]
    public string Character { get; set; }

    [JsonPropertyName("playtime")]
    public int Playtime { get; set; }

    [JsonPropertyName("score")]
    public int Score { get; set; }

    [JsonPropertyName("floor_reached")]
    public int FloorReached { get; set; }

    [JsonPropertyName("campfire_upgraded")]
    public int Smiths { get; set; }

    [JsonPropertyName("campfire_rested")]
    public int Rests { get; set; }
    [JsonPropertyName("master_deck")]
    public List<string> Deck { get; set; }
    [JsonPropertyName("neow_bonus")]
    public string NeowBonus { get; set; }
}

