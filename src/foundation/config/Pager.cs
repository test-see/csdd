using System.Collections.Generic;

namespace foundation.config
{
    public class PagerResult<T, R> : PagerQuery<R>
    {
        public int Total { get; set; }
        public IEnumerable<T> Result { get; set; }
    }

    public class PagerQuery<T>
    {
        public int Index { get; set; } = 1;
        public int Size { get; set; } = 10;
        public T Query { get; set; }
    }
}
