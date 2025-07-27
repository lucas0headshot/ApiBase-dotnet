namespace ApiBase.Core.Common.DTOs
{
    public class ApiErrorResponse
    {
        public string Message { get; set; }
        public object Content { get; set; }
        public string ErrorType { get; set; }
    }
}
