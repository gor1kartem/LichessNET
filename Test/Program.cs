using LichessNET.API;

var client = new LichessAPIClient();
var user = await client.GetRealTimeStatus(["thibault", "rabergsel"], true);

foreach(var u in user)
{
    Console.WriteLine(u.User.Username + " has signal strength: " + u.Signal);
}