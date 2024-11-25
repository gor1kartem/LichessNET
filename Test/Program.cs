using LichessNET.API;

var client = new LichessAPIClient();
var users = await client.GetLeaderboard(10, LichessNET.Entities.Enumerations.Gamemode.Bullet);

int i = 1;
foreach(var u in users)
{
    Console.WriteLine($"{i}. {u.Username}, Rating {u.Ratings[LichessNET.Entities.Enumerations.Gamemode.Bullet].Rating}");
}