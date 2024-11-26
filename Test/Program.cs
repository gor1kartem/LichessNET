using LichessNET.API;

var client = new LichessAPIClient(File.ReadAllText("token.txt"));
var game = await client.GetGame("v0ueAZUU094H", true);
Console.WriteLine(game.Id);
Console.WriteLine(game.CreatedAt);
Console.WriteLine(game.CreatedAtTimestamp);
Console.WriteLine(game.LastMoveAt);
Console.WriteLine(game.LastMoveTimestamp);
Console.WriteLine(game.Variant);
Console.WriteLine(game.White.Id);
Console.WriteLine(game.White.Rating);
Console.WriteLine(game.Black.Id);
Console.WriteLine(game.Clock.InitialTime);
Console.WriteLine(game.Opening.Eco);