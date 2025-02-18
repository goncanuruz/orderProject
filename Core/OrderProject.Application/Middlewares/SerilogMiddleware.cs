using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.Middlewares
{
    public class SerilogMiddleware
    {
        readonly ILogger<SerilogMiddleware> Log;
        readonly RequestDelegate _next;
        public SerilogMiddleware(RequestDelegate next, ILogger<SerilogMiddleware> log)
        {
            if (next == null) throw new ArgumentNullException(nameof(next));
            _next = next;
            Log = log;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            var sw = Stopwatch.StartNew();
            try
            {
                string content = "";
                var request = httpContext.Request;
                try
                {
                    request.EnableBuffering();
                    request.Body.Position = 0;
                    using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
                    {
                        content = await reader.ReadToEndAsync();
                    }
                }
                finally
                {
                    request.Body.Position = 0;
                }
                var statusCode = httpContext.Response?.StatusCode;
                var level = statusCode > 499 ? LogLevel.Error : LogLevel.Information;
                sw.Stop();

                Log.Log(level, "WithParameters Method: " + httpContext.Request.Method + " Source : " + httpContext.Request.Path + " Parameters: " + content + " Time : " + sw.Elapsed.TotalMilliseconds);

                await _next(httpContext);
            }
            // catch (Exception ex) { }
            catch (Exception ex)
            { Log.Log(LogLevel.Error, "System Error: " + ex.Message); }
        }
    }
}
