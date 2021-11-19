namespace AIIVE.BookReview.Catalogo.Data
{
    public class PaginatedResult<T>
    {
        public long From { get; set; }
        public long Size { get; set; }
        public long Count { get; set; }
        public T Data { get; set; }
    }
}
