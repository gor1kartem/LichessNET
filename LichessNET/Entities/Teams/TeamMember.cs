namespace LichessNET.Entities.Teams;

public class TeamMember
{
    public string ID { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Title { get; set; } = null!;
    public bool Patron { get; set; }
    public ulong joinedTeamAt { get; set; }
}