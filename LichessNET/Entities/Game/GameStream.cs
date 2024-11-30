using LichessNET.API;
using Newtonsoft.Json.Linq;

namespace LichessNET.Entities.Game;

public class GameStream
{
    // Define a delegate for the event
    public delegate void MoveUpdateHandler(object sender, Move move);


    /// <summary>
    /// Represents a game stream for a Lichess game, allowing real-time tracking of moves and game state.
    /// </summary>
    public GameStream(string id)
    {
        Stream = new LichessStream($"https://lichess.org/api/stream/game/{id}");
        Stream.GameUpdateReceived += ProcessData;
        Task.Run(async () => await Stream.StreamGameAsync());
    }

    public string initialFEN { get; set; } = "";
    public string player { get; set; } = "";
    public string lastMove { get; set; } = "";
    public int Turns { get; set; } = 0;

    private LichessStream Stream { get; set; }

    public List<Move> Moves { get; set; } = new List<Move>();

    // Declare the event using the delegate
    /// <summary>
    /// Occurs when a move is made in the game.
    /// </summary>
    /// <remarks>
    /// This event is triggered whenever a new move is processed in the game, providing the details of the move.
    /// The moves may be 3 to 60 seconds delayed.
    /// </remarks>
    public event MoveUpdateHandler OnMoveMade;

    internal void ProcessData(object o, JObject data)
    {
        if (data.ContainsKey("initialFEN"))
        {
            initialFEN = data["initialFEN"].ToString();
        }

        if (data.ContainsKey("player"))
        {
            player = data["player"].ToString();
        }

        if (data.ContainsKey("lastMove"))
        {
            lastMove = data["lastMove"].ToString();
        }

        if (data.ContainsKey("turns"))
        {
            Turns = data["turns"].ToObject<int>();
        }

        if (data.ContainsKey("lm"))
        {
            Moves.Add(new()
            {
                Notation = data["lm"].ToString(),
                IsWhite = data["fen"].ToString().EndsWith("w"),
                MoveNumber = (Turns + Moves.Count()) / 2
            });
            OnMoveMade?.Invoke(this, Moves[^1]);
        }
    }
}