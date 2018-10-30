namespace Fanex.Tarus.Admin.Entities
{
    public class CustomerDataRangeResult : Result
    {
        public CustomerDataRangeResult() : base()
        {
        }

        public int EffectedRow { get; set; }

        public string NoChangeUsers { get; set; }
    }
}
