using System.Text.Json;
using LichessNET.API;

var client = new LichessAPIClient(File.ReadAllText("token.txt"));
var status = await client.GetRealTimeUserStatus("thibault");

Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(status, new JsonSerializerOptions()
{
    WriteIndented = true
}));