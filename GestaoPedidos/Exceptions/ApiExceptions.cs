namespace GestaoPedidos.Exceptions
{
    public class ApiExceptions : Exception
    {

        public int StatusCode { get; }
        public string Details { get; }

        public ApiExceptions(int statusCode, string message, string details = null) : base(message)
        {
            StatusCode = statusCode;
            Details = details;
        }
    }
}
