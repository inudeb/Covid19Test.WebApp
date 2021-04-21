using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace Top10CovidCases.WebApp.Infrastructure
{
    public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IConfiguration _config;
        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _httpContextAccesor = httpContextAccessor;
            _config = config;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("x-rapidapi-key", new List<string>() { _config["rapidapiKey"]  });
            request.Headers.Add("x-rapidapi-host", new List<string>() { _config["rapidapiHost"] });
            request.Headers.Add("useQueryString", new List<string>() { true.ToString() });
            return await base.SendAsync(request, cancellationToken);
        }
       
    }
}
