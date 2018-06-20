namespace Fanex.Tarus.Excels
{
    public class ExcelResult
    {
        public ExcelResult()
            : this(0, null)
        {
        }

        public ExcelResult(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; }

        public string Message { get; }

        public bool IsSuccessful
            => Code == 0;
    }
}
