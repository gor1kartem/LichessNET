﻿namespace LichessNET.Entities.Game;

public class TimeControl
{
    public string Type { get; set; }
    public int Limit { get; set; }
    public int Increment { get; set; }
    public string Show { get; set; }
}