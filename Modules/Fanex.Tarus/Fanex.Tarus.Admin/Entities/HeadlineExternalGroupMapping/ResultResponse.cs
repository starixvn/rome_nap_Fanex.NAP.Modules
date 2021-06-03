namespace Fanex.Tarus.Admin.Entities.HeadlineExternalGroupMapping
{
    public class ResultResponse : Result
    {
        public ResultResponse()
        {
        }

        public ResultResponse(int errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
        }

        public string AffectedSupers { get; set; }
    }
}
