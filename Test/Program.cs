using System.Text.Json;
using LichessNET.API;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities.Game;

var client = new LichessApiClient(File.ReadAllText("token.txt"));

GameStream gameStream;

while (true)
{
    var games = await client.GetOngoingGamesAsync();
    if (games.Count > 0)
    {
        gameStream = await client.GetGameStreamAsync(games[0].GameId);
        Console.WriteLine("Game found!");
        break;
    }

    Thread.Sleep(3000);
}

gameStream.OnGameInfoFetched += (sender, game) =>
{
    Console.WriteLine(JsonSerializer.Serialize(game, new JsonSerializerOptions { WriteIndented = true }));
};

gameStream.OnMoveMade += (sender, move) =>
{
    Console.WriteLine(JsonSerializer.Serialize(move, new JsonSerializerOptions { WriteIndented = true }));
};

await Task.Delay(-1);

/*

var puzzle = await client.GetGameStreamByUserAsync("thibault");

Console.WriteLine(JsonSerializer.Serialize(puzzle, new JsonSerializerOptions { WriteIndented = true }));
*/