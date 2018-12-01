using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncMeasure
{
    public class ResultStatus
    {
        public bool Status { get; }
        public string Message { get; }

        public ResultStatus(bool status, string msg)
        {
            Status = status;
            Message = msg;
        }
    }
}
