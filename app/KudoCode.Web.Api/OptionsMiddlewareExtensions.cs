using Microsoft.AspNetCore.Builder;

namespace KudoCode.Web.Api
{
    public static class OptionsMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorsOptions(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OptionsMiddleware>();
        }
    }
}