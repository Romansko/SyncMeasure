using System.Collections.Generic;

namespace SyncMeasure
{
    public class Frame
    {
        public int Id { get; }
        public List<Hand> Hands { get; }
        public int Timestamp { get; }

        public Frame(int id, List<Hand> hands, int timestamp)
        {
            Id = id;
            Hands = hands ?? new List<Hand>();
            Timestamp = timestamp;
        }
    }
}
