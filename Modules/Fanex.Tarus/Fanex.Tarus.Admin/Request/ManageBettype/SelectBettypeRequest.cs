namespace Fanex.Tarus.Admin.Request.ManageBettype
{
    public class SelectBettypeRequest
    {
        public int TypeId { get; set; }

        public string TypeName { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}