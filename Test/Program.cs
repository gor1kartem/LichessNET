using System.Text.Json;
using LichessNET.API;

var client = new LichessAPIClient(File.ReadAllText("token.txt"));

var status = await client.GetGames("rabergsel");
Console.WriteLine(JsonSerializer.Serialize(status, new JsonSerializerOptions
{
    WriteIndented = true
}));
Thread.Sleep(1000);