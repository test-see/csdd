using System.Collections.Generic;

namespace foundation.config
{
    public class PagerResult<T>
    {
        public int Index { get; set; }
        public int Size { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Result { get; set; }

        public PagerResult() { }
        public PagerResult(int index, int size)
        {
            Index = index;
            Size = size;
        }
    }

    public class PagerQuery<T>
    {
        public int Index { get; set; } = 1;
        public int Size { get; set; } = 10;
        public T Query { get; set; }
    }
}
