using Microsoft.AspNetCore.Builder;
using WEB_953501_YURETSKI.Middleware;

namespace WEB_953501_YURETSKI.Extensions
{
    public static class AppExtensions
    {
        public static IApplicationBuilder UseFileLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogMiddleware>();
        }
    }
}
