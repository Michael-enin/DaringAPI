namespace DaringAPI.Controllers.Errors
{
    public class ApiExceptions
    {
        public ApiExceptions(int status, string message=null, string details=null)
        {
            Status = status;
            Message = message;
            Details = details;
        }

        public int Status { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}