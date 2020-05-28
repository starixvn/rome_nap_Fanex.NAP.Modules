namespace Fanex.Tarus.Admin.ManageUsernamePrefix.Requests
{
    public class QueryRequest
    {
        public string GroupName { get; set; }

        public string Prefix { get; set; }

        public bool? AllowNewSuper { get; set; }
    }
}
