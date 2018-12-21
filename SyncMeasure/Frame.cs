using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
