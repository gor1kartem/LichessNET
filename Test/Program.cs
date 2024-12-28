using System.Text.Json;
using LichessNET.API;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities.Game;

var client = new LichessApiClient(File.ReadAllText("token.txt"));

var popularteams = await client.GetPopularTeamsAsync(2);
foreach (var team in popularteams)
{
    Console.WriteLine(team.Name);
}