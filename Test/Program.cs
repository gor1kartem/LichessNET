using System.Text.Json;
using LichessNET.API;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities.Game;

var client = new LichessApiClient(File.ReadAllText("token.txt"));

GameStream stream;

while (true)
{
    var ongoing = await client.GetOngoingGamesAsync();
    if (ongoing.Count > 0)
    {
        stream = await client.GetGameStreamAsync(ongoing[0].GameId);
        Console.WriteLine("Game found!");
        break;
    }

    Console.WriteLine("No ongoing games");
    await Task.Delay(5000);
}

stream.OnMoveMade += (sender, move) => { Console.WriteLine($"Move {move.MoveNumber}: {move.Notation}"); };

await Task.Delay(-1);