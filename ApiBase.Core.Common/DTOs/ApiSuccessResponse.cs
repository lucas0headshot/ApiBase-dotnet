namespace ApiBase.Core.Common.DTOs
{
    public class ApiSuccessResponse
    {
        public string Message { get; set; }
        public object Content { get; set; }
        public int? RequestCode { get; set; }
    }
}
