using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Oauth2ResourceServer.Models
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<Startup> _logger;
        private readonly IMemoryCache _cache;
        private readonly string OAUTH2_SERVER = "https://oauth2server20181116083841.azurewebsites.net";
        private readonly string OAUTH2_AUTHORIZATION_URI = "/Oauth2/CheckToken";
        private readonly string SCOPES = "http://basicscope.com,http://songresourcescope.com";

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<Startup> logger, IMemoryCache cache)
        {
            _next = next;
            _logger = logger;
            _cache = cache;
        }

        public async Task Invoke(HttpContext context)
        {
            
            _logger.LogInformation("Hello coco malo.");
            context.Request.Headers.TryGetValue("Authorization", out StringValues authorizationToken);
            var authorization = authorizationToken + "";
            authorization = authorization.Replace("Basic ", "");
            var cacheToken = _cache.Get<string>(authorization);
            if (cacheToken == authorization)
            {
                await _next(context);
                return;
            }

            var client = new HttpClient();
            var result = client.GetAsync(OAUTH2_SERVER + OAUTH2_AUTHORIZATION_URI + "?tokenKey=" + authorization + "&scopes=" + SCOPES)
                .Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                _cache.Set<string>(authorization, authorization);
                await _next(context);
                return;

            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }

    }
}
