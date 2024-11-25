using LichessNET.Entities.Enumerations;

namespace LichessNET.Entities
{
    public class LichessUser
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public IReadOnlyDictionary<Gamemode, GamemodeStats>? Ratings { get; set; }
        public string? Flair { get; set; }
        private ulong? createdAt { get; set; }

        public bool? Disabled { get; set; }
        public bool? TOSViolation { get; set; }
        public LichessProfile? Profile { get; set; }

        private ulong? seenAt { get; set; }

        public bool? Patron { get; set; }
        public bool? Verified { get; set; }
        public PlaytimeStats? PlayTime { get; set; }
        internal string? title { get; set; }
        public Title Title
        {
            get
            {
                switch (title)
                {
                    case "CM":
                        return Title.CM;
                    case "IM":
                        return Title.IM;
                    case "GM":
                        return Title.GM;
                    case "LM":
                        return Title.LM;
                    default:
                        return Title.None;
                }
            }
        }

        public GameCounts? Count { get; set; }
        public bool? Streaming { get; set; }
        public StreamingInfo? Streamer { get; set; }
        public bool? Followable { get; set; }
        public bool? Following { get; set; }
        public bool? Blocking { get; set; }
        public bool? FollowsYou { get; set; }

    }
}
