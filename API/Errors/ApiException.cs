namespace API.Errors
{
    public class ApiException
    {
        public ApiException(int statusCode, string errors = null, string details = null)
        {
            StatusCode = statusCode;
            Errors = errors;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Errors { get; set; }
        public string Details { get; set; }
    }
}