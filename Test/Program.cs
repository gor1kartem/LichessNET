using System.Text.Json;
using LichessNET.API;

var client = new LichessAPIClient(File.ReadAllText("token.txt"));
for (int i = 0; i < 10; i++)
{
    var status = await client.GetOwnProfile();
    Console.WriteLine(JsonSerializer.Serialize(status, new JsonSerializerOptions()
    {
        WriteIndented = true
    }));
    Thread.Sleep(1000);
}