using System.Text.Json;
using LichessNET.API;
using LichessNET.Converters;
using LichessNET.Database;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities.Game;
using LichessNET.Entities.Social;
using Newtonsoft.Json;

// var client = new LichessApiClient();
// await client.SetToken("lip_i3ssebB0d4eoJGW82BxG");
// var test = await client.GetUserProfile("Mitrabha");
// Console.WriteLine(test.TosViolation);

var test = JsonConvert.DeserializeObject<LichessUser>(
    "{\"id\":\"mitrabha\",\"username\":\"Mitrabha\",\"perfs\":{\"ultraBullet\":{\"games\":4616,\"rating\":2325,\"rd\":142,\"prog\":3,\"prov\":true},\"bullet\":{\"games\":20457,\"rating\":3163,\"rd\":45,\"prog\":-29},\"blitz\":{\"games\":9841,\"rating\":2620,\"rd\":45,\"prog\":-5},\"rapid\":{\"games\":89,\"rating\":2574,\"rd\":109,\"prog\":125},\"classical\":{\"games\":8,\"rating\":2190,\"rd\":260,\"prog\":0,\"prov\":true},\"correspondence\":{\"games\":0,\"rating\":1500,\"rd\":500,\"prog\":0,\"prov\":true},\"chess960\":{\"games\":355,\"rating\":2470,\"rd\":61,\"prog\":33},\"crazyhouse\":{\"games\":58,\"rating\":2117,\"rd\":178,\"prog\":105,\"prov\":true},\"puzzle\":{\"games\":780,\"rating\":2606,\"rd\":123,\"prog\":0,\"prov\":true},\"storm\":{\"runs\":368,\"score\":99},\"racer\":{\"runs\":105,\"score\":118},\"streak\":{\"runs\":21,\"score\":83}},\"title\":\"GM\",\"createdAt\":1540573022018,\"profile\":{\"flag\":\"IN\",\"bio\":\"Follow In :- Twitch- https://www.twitch.tv/gmmitrabha\\r\\nKICK- https://kick.com/gmmitrabha         \\r\\n\\r\\nJoin my team \\r\\nhttps://lichess.org/team/gm-mitrabha--friends\",\"realName\":\"Mitrabha Guha\",\"links\":\"https://www.twitch.tv/gmmitrabha\\r\\nhttps://kick.com/gmmitrabha\"},\"seenAt\":1739641995697,\"playTime\":{\"total\":4448707,\"tv\":1393779},\"url\":\"https://lichess.org/@/Mitrabha\",\"playing\":\"https://lichess.org/gvxjW0eq/white\",\"count\":{\"all\":35909,\"rated\":35469,\"ai\":3,\"draw\":2850,\"drawH\":2850,\"loss\":11977,\"lossH\":11975,\"win\":21082,\"winH\":21081,\"bookmark\":2,\"playing\":1,\"import\":26,\"me\":0},\"streamer\":{\"twitch\":{\"channel\":\"https://www.twitch.tv/GMmitrabha\"},\"youTube\":{\"channel\":\"https://www.youtube.com/channel/UChcF0lyLxcRLUA6wsfzEQCA/live\"}},\"followable\":true,\"following\":false,\"blocking\":false}");
await Task.Delay(Timeout.Infinite);