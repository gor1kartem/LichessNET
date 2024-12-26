using System.Text.Json;
using LichessNET.API;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities.Game;

var client = new LichessApiClient(File.ReadAllText("token.txt"));

var alllivestreamers = await client.GetAllLiveStreamers();
foreach (var streamer in alllivestreamers)
{
    Console.WriteLine(streamer.Name);
}