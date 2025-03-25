﻿using System.Text.Json.Serialization;
using LichessNET.Entities.Enumerations;

namespace LichessNET.Entities.Social;

/// <summary>
/// A small overview of a user sent by the API.
/// </summary>
public class UserOverview
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool Patron { get; set; }
    public string Flair { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Title? Title { get; set; }
}