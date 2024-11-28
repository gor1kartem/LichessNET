using LichessNET.API;

var client = new LichessAPIClient(File.ReadAllText("token.txt"));
var games = await client.GetGame("v0ueAZUU094H");