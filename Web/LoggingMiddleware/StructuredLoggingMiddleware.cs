using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Web.LoggingMiddleware
{
    public partial class StructuredLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<StructuredLoggingMiddleware> _logger;

        public StructuredLoggingMiddleware(RequestDelegate next, ILogger<StructuredLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        private static string MaskSensitiveFields(string response)
        {
            if (string.IsNullOrEmpty(response)) return response;

            // Mask fullname
            response = MyRegex().Replace(response, @"""fullname"":""********""");

            // Mask email
            response = MyRegex1().Replace(response, @"""email"":""********""");

            return response;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            var proc = Process.GetCurrentProcess();
            var startCpu = proc.TotalProcessorTime;
            var startMem = proc.WorkingSet64;

            context.Request.EnableBuffering();
            var originalResponseBody = context.Response.Body;
            await using var newResponseBody = new MemoryStream();
            context.Response.Body = newResponseBody;

            var stopwatch = Stopwatch.StartNew();

            // Read request body
            string requestBody = "";
            if (context.Request.ContentLength > 0 &&
                context.Request.Body.CanRead)
            {
                context.Request.Body.Position = 0;
                using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            await _next(context); // call next middleware

            stopwatch.Stop();

            // Read response body
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            // Copy response back to original stream
            await newResponseBody.CopyToAsync(originalResponseBody);

            string parsedRequestBody = MaskSensitiveFields(requestBody);
            string parsedResponseBody = MaskSensitiveFields(responseBody);

            var endCpu = proc.TotalProcessorTime;
            var endMem = proc.WorkingSet64;

            var log = new
            {
                timestamp = DateTime.UtcNow
                ,
                level = "Information"
                ,
                requestId = context.TraceIdentifier
                ,
                traceId = context.TraceIdentifier
                ,
                userId = context.User?.Identity?.Name ?? "anonymous"
                ,
                ip = context.Connection.RemoteIpAddress?.ToString()
                ,
                method = context.Request.Method
                ,
                path = context.Request.Path.Value
                ,
                query = context.Request.QueryString.Value
                ,
                requestHeaders = context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString())
                ,
                requestBody = parsedRequestBody
                ,
                responseStatus = context.Response.StatusCode
                ,
                responseBody = parsedResponseBody
                ,
                elapsedMs = stopwatch.ElapsedMilliseconds
                ,
                cpuUsed = (endCpu - startCpu).TotalMilliseconds
                ,
                memUsed = endMem - startMem
            };

            _logger.LogInformation("Logging Pattern {@Log}", log);
        }

        [GeneratedRegex(@"""fullname""\s*:\s*"".*?""", RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex MyRegex();
        [GeneratedRegex(@"""email""\s*:\s*"".*?""", RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex MyRegex1();
    }
}

