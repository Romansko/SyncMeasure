using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncMeasure
{
    public class Hand
    {
        public int Id { get; set; }
        public int FrameId { get; set; }
        public double PinchStrength { get; set; }
        public double GrabStrength { get; set; }
        public bool IsLeft { get; set; }
        public Vector Position { get; set; }
        public Vector ElbowPosition { get; set; }
        public Vector ArmPosition { get; set; }

    }
}
