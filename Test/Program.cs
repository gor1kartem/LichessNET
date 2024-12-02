using System.Text.Json;
using LichessNET.API;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities.Game;

var client = new LichessApiClient(File.ReadAllText("token.txt"));
var puzzle = await client.GetRandomPuzzle();
puzzle = await client.GetRandomPuzzle();

Console.WriteLine(JsonSerializer.Serialize(puzzle, new JsonSerializerOptions { WriteIndented = true }));