using System.Text.Json;
using LichessNET.API;

var client = new LichessApiClient(File.ReadAllText("token.txt"));

var status = await client.GetGames("rabergsel", 2);
Console.WriteLine(JsonSerializer.Serialize(status, new JsonSerializerOptions
{
    WriteIndented = true
}));
Thread.Sleep(1000);