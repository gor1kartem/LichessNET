using System.Text.Json;
using LichessNET.API;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities.Game;

var client = new LichessApiClient(File.ReadAllText("token.txt"));
var puzzle = await client.GetDailyPuzzle();

Console.WriteLine($"Puzzle {puzzle.id} from {puzzle.Game.Id}: {puzzle.Plays} plays");