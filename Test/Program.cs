using System.Text.Json;
using LichessNET.API;

var client = new LichessAPIClient(File.ReadAllText("token.txt"));
var status = await client.GetKidModeStatus();

Console.WriteLine(JsonSerializer.Serialize(status, new JsonSerializerOptions()
{
    WriteIndented = true
}));