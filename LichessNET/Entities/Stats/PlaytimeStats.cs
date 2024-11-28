namespace LichessNET.Entities
{
    public class PlaytimeStats
    {
        public int total;
        public int tv;

        /// <summary>
        /// The total time played by the user
        /// </summary>
        public TimeSpan TotalSpan
        {
            get { return TimeSpan.FromSeconds(total); }
        }

        /// <summary>
        /// The total time seen on TV
        /// </summary>
        public TimeSpan TVSpan
        {
            get { return TimeSpan.FromSeconds(tv); }
        }
    }
}