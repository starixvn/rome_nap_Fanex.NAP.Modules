namespace Fanex.Tarus.Security.Models
{
    using System.Net;
    using System.Web.Script.Serialization;

    internal sealed class AuthorizationResult
    {
        private AuthorizationResult(int code, string message)
        {
            ErrorCode = code;
            ErrorMessage = message;
        }

        public int ErrorCode { get; }

        public string ErrorMessage { get; }

        [ScriptIgnore]
        public bool IsOk
            => ErrorCode == 0;

        public string ToJson()
            => new JavaScriptSerializer().Serialize(this);

        public static AuthorizationResult Ok()
            => new AuthorizationResult(0, string.Empty);

        public static AuthorizationResult Forbidden()
            => Forbidden("You don't have permission to access.");

        public static AuthorizationResult Forbidden(string error)
            => new AuthorizationResult((int)HttpStatusCode.Forbidden, error);
    }
}
