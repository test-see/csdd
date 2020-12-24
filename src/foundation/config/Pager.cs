using System.Collections.Generic;
using System.Linq;

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
        public PagerResult(int index, int size, IQueryable<T> query)
        {
            Index = index;
            Size = size;
            Result = query.Skip(size * (index - 1)).Take(size).ToList();
            Total = query.Count();
        }
    }

    public class PagerQuery<T>
    {
        public int Index { get; set; } = 1;
        public int Size { get; set; } = 10;
        public T Query { get; set; }
    }
}
