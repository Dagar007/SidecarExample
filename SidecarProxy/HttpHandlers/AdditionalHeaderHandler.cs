using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace SidecarProxy.HttpHandlers
{
    public class AdditionalHeaderHandler : HttpClientHandler
    {
        public AdditionalHeaderHandler()
        {
            base.AllowAutoRedirect = true;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Header-One", "DeepakDagar");
            request.Headers.Add("Header-Two", "DeepakDagar1");
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
