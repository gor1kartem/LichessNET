using System.Security.Principal;
using LichessNET.Entities.Enumerations;

namespace LichessNET.Entities.Game;

public class Game
{
    public Dictionary<string, string> AdditionalData = new Dictionary<string, string>();
    public string Event { get; set; }
    public string URL { get; set; }
    public GamePlayer White { get; set; }
    public GamePlayer Black { get; set; }
    public GameResult Result { get; set; }
    public string ECO { get; set; }
    public string Opening { get; set; }

    public MoveSequence Moves { get; set; }

    /// <summary>
    /// Creates a new Game instance by parsing the provided PGN (Portable Game Notation) string.
    /// </summary>
    /// <param name="pgn">The PGN string representing the chess game, containing headers and move information.</param>
    /// <returns>A Game object initialized with data extracted from the PGN string, such as players, result, and moves.</returns>
    public static Game FromPGN(string pgn)
    {
        var game = new Game();
        var lines = pgn.Split('\n');

        game.White = new GamePlayer();
        game.Black = new GamePlayer();
        game.Moves = new MoveSequence();

        foreach (var line in lines)
        {
            if (line.StartsWith('[') & line.EndsWith(']')) //Header
            {
                string key = line.Split(' ')[0].Trim('[', ']');
                string value = line.Split('\"')[1];

                switch (key.ToLower())
                {
                    case "whiteelo":
                        game.White.Rating = int.Parse(value);
                        break;
                    case "blackelo":
                        game.Black.Rating = int.Parse(value);
                        break;
                    case "event":
                        game.Event = value;
                        break;
                    case "white":
                        game.White.Name = value;
                        break;
                    case "black":
                        game.Black.Name = value;
                        break;
                    case "result":
                        if (value == "1-0")
                            game.Result = GameResult.WhiteVictory;
                        else if (value == "0-1")
                            game.Result = GameResult.BlackVictory;
                        else if (value == "1/2-1/2")
                            game.Result = GameResult.Stalemate;
                        break;
                    case "eco":
                        game.ECO = value;
                        break;
                    case "opening":
                        game.Opening = value;
                        break;
                    default:
                        game.AdditionalData.Add(key, value);
                        break;
                }
            }
            else // Moves
            {
                game.Moves.OriginalPGN = line;
                var moveTokens =
                    System.Text.RegularExpressions.Regex.Split(line, @"(\d+\.\s+|\s+1-0|\s+0-1|\s+1/2-1/2)");
                foreach (var token in moveTokens)
                {
                    if (string.IsNullOrWhiteSpace(token) || token.Contains("1-0") || token.Contains("0-1") ||
                        token.Contains("1/2-1/2"))
                        continue;

                    var moveDetails =
                        System.Text.RegularExpressions.Regex.Match(token, @"(?<move>\S+)\s*(?:{(?<info>[^}]+)})?");
                    if (moveDetails.Success)
                    {
                        var notation = moveDetails.Groups["move"].Value;
                        var info = moveDetails.Groups["info"].Value;

                        var move = new Move
                        {
                            Notation = notation,
                            MoveNumber = game.Moves.Moves.Count / 2 + 1,
                            isWhite = game.Moves.Moves.Count % 2 == 0
                        };

                        if (!string.IsNullOrEmpty(info))
                        {
                            var evalMatch =
                                System.Text.RegularExpressions.Regex.Match(info, @"\[%eval\s*(-?\d+(?:\.\d+)?)\]");
                            var clkMatch =
                                System.Text.RegularExpressions.Regex.Match(info, @"\[%clk\s*(\d+:\d{2}:\d{2})\]");

                            if (evalMatch.Success)
                            {
                                float eval;
                                if (float.TryParse(evalMatch.Groups[1].Value, out eval))
                                {
                                    move.Evaluation = eval;
                                }
                            }

                            if (clkMatch.Success)
                            {
                                TimeSpan clock;
                                if (TimeSpan.TryParse(clkMatch.Groups[1].Value, out clock))
                                {
                                    move.Clock = clock;
                                }
                            }
                        }

                        game.Moves.Moves.Add(move);
                    }
                }
            }
        }


        return game;
    }
}