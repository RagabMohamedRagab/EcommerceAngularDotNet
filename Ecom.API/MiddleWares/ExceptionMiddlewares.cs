using Ecom.API.Helpers;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace Ecom.API.MiddleWares
{
    public class ExceptionMiddlewares(RequestDelegate _next, IHostEnvironment _hostEnvironment, IMemoryCache _memoryCache)
    {
        private readonly TimeSpan _limitForIp = TimeSpan.FromSeconds(10);

        public async Task Invoke(HttpContext context)
        {
            try
            {
                ApplySecuirty(context);
                if (!IsRateLimit(context))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";
                    var rateLimitResponse = new ApiException(
                        false,
                        "Too many requests. Please try again later.",
                        (int)HttpStatusCode.TooManyRequests,
                        null
                    );
                    await context.Response.WriteAsync(JsonSerializer.Serialize(rateLimitResponse));
                    return; // ✅ FIX 1: Stop pipeline after writing response
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = _hostEnvironment.IsDevelopment()
                    ? new ApiException(
                        false,
                        "An error occurred while processing your request.",
                        (int)HttpStatusCode.InternalServerError,
                        ex.Message + " " + ex.StackTrace)
                    : new ApiException(
                        false,
                        "An error occurred while processing your request.",
                        (int)HttpStatusCode.InternalServerError,
                        ex.Message);

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse)); // ✅ FIX 2: No double serialization
            }
        }

        public bool IsRateLimit(HttpContext context)
        {
            var ip = context?.Connection?.RemoteIpAddress?.ToString();
            var cacheKey = $"ip:{ip}";
            var now = DateTime.UtcNow; // ✅ Use UTC to avoid timezone issues

            // ✅ FIX 3: Null-safe GetOrCreate with fallback
            var (timespan, count) = _memoryCache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _limitForIp;
                return (now, 0);
            }) ;

            if (now - timespan < _limitForIp)
            {
                if (count >= 5)
                {
                    return false; // Rate limit exceeded
                }

                _memoryCache.Set(cacheKey, (timespan, count + 1), _limitForIp);
            }
            else
            {
                // Window expired — reset
                _memoryCache.Set(cacheKey, (now, 1), _limitForIp);
            }

            return true;
        }

        public void ApplySecuirty(HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["X-XSS-Type-Protections"] = "1;mode=block";
            context.Response.Headers["X-Frame-Type-Options"] = "Deny";

        }
    }
}