namespace LichessNET.Entities
{
    public class PlaytimeStats
    {
        private int total;
        private int tv;
        
        /// <summary>
        /// The total time played by the user
        /// </summary>
        public TimeSpan Total
        {
            get
            {
                return TimeSpan.FromSeconds(total);
            }
        }
        
        /// <summary>
        /// The total time seen on TV
        /// </summary>
        public TimeSpan TV
        {
            get
            {
                return TimeSpan.FromSeconds(tv);
            }
        }
    }
}
