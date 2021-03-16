namespace foundation.config
{
    public class OrderBy<T>
    {
        public bool IsAsc { get; set; }
        public T By { get; set; }
    }
}
