using LichessNET.Entities.Enumerations;

namespace LichessNET.Entities.Social;

public class UserRealTimeStatus
{
    public LichessUser User { get; set; } = new LichessUser();

    public string Name { get; set; } = "Anonymous";

    public string Id { get; set; } = "anonymous";

    public bool? Online { get; set; } = false;
    public bool? Playing { get; set; } = false;
    public bool? Streaming { get; set; } = false;
    public bool? Patron { get; set; } = false;
    internal int? Signal { get; set; } = 5;

    public SignalConnection SignalConnection
    {
        get
        {
            if (Signal == null) return SignalConnection.Unknown;
            return (SignalConnection)Signal;
        }
    }
}