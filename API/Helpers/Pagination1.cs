using System.Collections.Generic;

namespace API.Helpers
{
    public class Pagination1<T> where T : class
    {
        public Pagination1(int pageIndex, int pageSize, int count, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}