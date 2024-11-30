using System.Text.Json;
using LichessNET.API;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities.Game;

var client = new LichessApiClient(File.ReadAllText("token.txt"));

var stream = new GameStream("e8ADz6ls");

stream.OnMoveMade += (o, m) => { Console.WriteLine($"Move {m.MoveNumber}: {m.Notation}"); };

await Task.Delay(-1);