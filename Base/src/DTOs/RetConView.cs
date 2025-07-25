namespace CoreBackend.src.DTOs
{
    public class RetConView<T>
    {
        public int Total { get; set; }
        public IList<T> Content { get; set; }
    }
}
