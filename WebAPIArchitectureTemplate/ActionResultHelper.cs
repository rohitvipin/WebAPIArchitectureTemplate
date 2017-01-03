using System.Net;
using System.Net.Http;
using System.Web.Http.Results;

namespace WebAPIArchitectureTemplate
{
    public static class ActionResultHelper
    {
        public static ResponseMessageResult StatusCodeWithMessage(HttpStatusCode httpStatusCode, string message) => new ResponseMessageResult(new HttpResponseMessage
        {
            StatusCode = httpStatusCode,
            Content = new StringContent(message)
        });
    }
}