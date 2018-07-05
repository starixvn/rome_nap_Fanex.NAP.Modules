namespace Fanex.Tarus.Security.HttpModules.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Web;
    using Newtonsoft.Json.Linq;
    using Sunplus.Accounting.Core.Common;

    internal sealed class UrlPermissionHttpContext
    {
        private HttpContext Context => HttpContext.Current;
        private HttpRequest Request => Context.Request;
        private HttpResponse Response => Context.Response;

        public int UserId
            => AppSession.AccId;

        public string GetLowerAbsolutePath()
        {
            var httpRequest = Request;
            var virtualDirectory = httpRequest.ApplicationPath;

            var absolutePath = virtualDirectory == "/"
                ? httpRequest.Url.AbsolutePath
                : httpRequest.Url.AbsolutePath.Replace(virtualDirectory, string.Empty);

            return absolutePath.ToLowerInvariant();
        }

        public string GetParamValueFromParamList(IEnumerable<string> paramNames)
        {
            var paramValue = GetRequestParam(paramNames);

            return paramValue == null
                ? GetJsonParam(paramNames)
                : paramValue;
        }

        public void RedirectToAccessDenyPage()
        {
            var httpResponse = Response;

            if (IsAjaxRequest())
            {
                httpResponse.Clear();
                httpResponse.StatusCode = (int)HttpStatusCode.Forbidden;
                httpResponse.ContentType = "application/json; charset=utf-8";
                httpResponse.Write("{ \"ErrorCode\": 99999, \"ErrorMessage\": \"You don't have permission to access.\" }");
                httpResponse.End();
            }
            else
            {
                httpResponse.Redirect("~/AccessDenied.aspx", true);
            }
        }

        private bool IsAjaxRequest()
        {
            var httpRequest = Request;
            var requestHeaders = httpRequest.Headers;

            return httpRequest["X-Requested-With"] == "XMLHttpRequest"
                || requestHeaders["X-Requested-With"] == "XMLHttpRequest"
                || requestHeaders["IsPostAjax"] == "true";
        }

        private string GetRequestParam(IEnumerable<string> paramNames)
        {
            var httpRequest = Request;
            string paramValue;

            foreach (var item in paramNames)
            {
                paramValue = httpRequest[item];

                if (paramValue != null)
                {
                    return paramValue;
                }
            }

            return null;
        }

        private string GetJsonParam(IEnumerable<string> paramNames)
        {
            var jsonString = ReadRequestBody();

            if (jsonString != null)
            {
                jsonString = ParseJsonStringStartsWithModel(jsonString);
                var jsonObject = JObject.Parse(jsonString.ToLowerInvariant());
                JToken jToken;

                foreach (var item in paramNames)
                {
                    jToken = jsonObject.SelectToken(item);

                    if (jToken != null)
                    {
                        return jToken.Value<string>();
                    }
                }
            }

            return null;
        }

        private string ReadRequestBody()
        {
            var inputStream = Request.InputStream;

            if (inputStream.Length != 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    var streamReader = new StreamReader(memoryStream);
                    inputStream.CopyTo(memoryStream);

                    memoryStream.Seek(0, SeekOrigin.Begin);

                    // Reset Position of the stream in order to ASP.NET can read it after that.
                    inputStream.Position = 0;

                    return streamReader.ReadToEnd();
                }
            }

            return null;
        }

        private string ParseJsonStringStartsWithModel(string jsonString)
            => jsonString.StartsWith("model=")
                ? HttpUtility.UrlDecode(jsonString).Replace("model=", string.Empty)
                : jsonString;
    }
}
