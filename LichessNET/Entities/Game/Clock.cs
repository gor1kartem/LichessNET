namespace LichessNET.Entities.Game;

    public class Clock
    {
        public int Initial { get; set; }
        public int Increment { get; set; }
        public int totalTime { get; set; }
        
        public TimeSpan InitialTime
        {
            get
            {
                return TimeSpan.FromSeconds(Initial);
            }
        }
        
        public TimeSpan IncrementTime
        {
            get
            {
                return TimeSpan.FromSeconds(Increment);
            }
        }

        public TimeSpan TotalTime
        {
            get { return TimeSpan.FromSeconds(totalTime); }
        }

    }
