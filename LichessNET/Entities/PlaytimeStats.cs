namespace LichessNET.Entities
{
    public class PlaytimeStats
    {
        private int total;
        private int tv;

        public TimeSpan Total
        {
            get
            {
                return TimeSpan.FromSeconds(total);
            }
        }
        public TimeSpan TV
        {
            get
            {
                return TimeSpan.FromSeconds(tv);
            }
        }
    }
}
