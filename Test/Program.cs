using System.Text.Json;
using LichessNET.API;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities.Game;

var client = new LichessApiClient(File.ReadAllText("token.txt"));
var puzzle = await client.GetUserProfile("thibault");

Console.WriteLine(JsonSerializer.Serialize(puzzle, new JsonSerializerOptions { WriteIndented = true }));
;