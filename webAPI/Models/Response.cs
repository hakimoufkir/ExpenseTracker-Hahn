namespace webAPI.Models
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public Response(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
