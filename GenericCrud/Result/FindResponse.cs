namespace GenericCrud.Result
{
    public class FindResponse<T>
    {
        public IList<T> Data { get; set; }
        public long Count { get; set; }
    }
}
