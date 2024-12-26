using System.Text.Json;
using LichessNET.API;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities.Game;

var client = new LichessApiClient(File.ReadAllText("token.txt"));

var timeline = await client.GetTimelineAsync(DateTime.Now.AddMonths(-7), 20);

foreach (var ev in timeline.Entries)
{
    Console.WriteLine($"Type: {ev.Type} @ {ev.EventTime}");
}

foreach (var u in timeline.Users)
{
    Console.WriteLine($"User: {u.Value.Name}");
}