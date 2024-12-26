using System.Text.Json;
using LichessNET.API;
using LichessNET.Database;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities.Game;

var client = new LichessApiClient(File.ReadAllText("token.txt"));
var database = new DatabaseClient();

await database.DownloadMonthlyDatabase(2015, 1, ChessVariant.Atomic, "2013-01", true);