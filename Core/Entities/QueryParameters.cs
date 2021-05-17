namespace Core.Entities
{
    public class QueryParameters
    {
        private const int MaxPageCount = 5;
        public int Page { get; set; } = 1;
        private int _pageCount = MaxPageCount;
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = (value > MaxPageCount) ? MaxPageCount : value; }
        }
        public string Query { get; set; }
        public string OrderBy { get; set; } = "MemberFName";
    }
}