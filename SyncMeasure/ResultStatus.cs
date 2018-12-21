
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
