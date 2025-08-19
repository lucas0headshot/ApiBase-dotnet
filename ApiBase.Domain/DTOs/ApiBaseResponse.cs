namespace ApiBase.Domain.DTOs
{
    public class ApiBaseResponse
    {
        public object Content { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }

    public class ApiErrorResponse : ApiBaseResponse
    {
        public string ErrorType { get; set; }

        public ApiErrorResponse()
        {
            Success = false;
        }
    }

    public class ApiSuccessResponse : ApiBaseResponse
    {
        public int? RequestCode { get; set; }

        public ApiSuccessResponse()
        {
            Success = true;
        }
    }

    public class ApiPaginatedResponse
    {
        public object Content { get; set; }
        public int Total { get; set; }
    }
}
