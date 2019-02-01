using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncMeasure
{
    /**
     * Class for holding data for output summary.
     */
    public struct SyncParams
    {
        public SyncConfig SyncConfig;
        public double AvgHandCvv;
        public double AvgArmCvv;
        public double AvgElbowCvv;
        public double AvgGrabSync;
        public double AvgPinchSync;
    }

    public struct SyncConfig
    {
        public Handler.ECvv CvvCalcMethod;
        public int TimeLag;
        public int NBasis;
    }

}
