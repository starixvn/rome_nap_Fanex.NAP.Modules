namespace Fanex.CustomerSetting.Module.SearchCopy.Entities
{
    internal class SearchResult
    {
        public SearchResult()
            : this(-1, -1, null)
        {
        }

        public SearchResult(int custId, int parentId, string breadCrumbs)
        {
            CustId = custId;
            ParentId = parentId;
            BreadCrumbs = breadCrumbs;
        }

        public int CustId { get; }

        public int ParentId { get; }

        public string BreadCrumbs { get; }
    }
}