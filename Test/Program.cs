using System.Text.Json;
using LichessNET.API;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities.Game;

var client = new LichessApiClient(File.ReadAllText("token.txt"));

var ratingHistory = await client.GetRatingHistory("rabergsel");
foreach (var mode in ratingHistory)
{
    Console.WriteLine("Mode: " + mode.Key.ToString());
    foreach (var rating in mode.Value)
    {
        Console.WriteLine("Rating: " + rating.Rating);
        Console.WriteLine("Date: " + rating.Date);
    }
}