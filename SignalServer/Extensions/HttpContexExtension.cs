using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SignalServer.Extensions
{
    public static class HttpContexExtension
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            return httpContext.User.FindFirst(x => x.Type == "id").Value;
        }

        public static string GetUsername(this HttpContext httpContext)
        {
            return httpContext.User.FindFirst(x => x.Type == "sub").Value;
        }
    }
}
