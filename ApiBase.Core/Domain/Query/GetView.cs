namespace ApiBase.Core.Domain.Query
{
    public class GetView
    {
        public int Total { get; set; }
        public IList<object> Content { get; set; }
    }
}
