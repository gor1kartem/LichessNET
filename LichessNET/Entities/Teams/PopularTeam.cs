using LichessNET.Entities.Social;

namespace LichessNET.Entities.Teams;

public class PopularTeam
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Open { get; set; }
    public string Flair { get; set; }
    public int NbMembers { get; set; }
    public UserOverview Leader { get; set; }
    public List<UserOverview> Leaders { get; set; }
    
}