namespace LichessNET.Entities
{
    public class LichessProfile
    {
        private string Flag { get; set; }
        private string Location { get; set; }
        private string Bio { get; set; }
        private string RealName { get; set; }
        private ushort FideRating { get; set; }
        private ushort UsCfRating { get; set; }
        private ushort EcfRating { get; set; }
        private ushort CfcRating { get; set; }
        private ushort DsbRating { get; set; }
        private string Links { get; set; }
    }
}
