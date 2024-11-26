using LichessNET.Entities.Enumerations;

namespace LichessNET.Entities
{
    public class UserRealTimeStatus
    {
        
        public LichessUser User { get; set; }

        public string name { get; set; }

        public string id { get; set; }

        public bool? Online { get; set; } = false;
        public bool? Playing { get; set; } = false;
        public bool? Streaming { get; set; } = false;
        public bool? Patron { get; set; } = false;
        internal int? signal { get; set; } = 5;
        public SignalConnection Signal
        {
            get
            {
                if (signal == null) return SignalConnection.Unknown;
                return (SignalConnection)signal;
            }
        }   

        public UserRealTimeStatus() { }
    }
}
