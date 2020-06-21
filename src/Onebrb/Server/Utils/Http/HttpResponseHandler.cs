using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Onebrb.Server.Utils.Http
{
    /// <summary>
    /// Used as return type for HTTP responses
    /// </summary>
    public class HttpResponseHandler
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public object Object { get; set; }
    }
}
