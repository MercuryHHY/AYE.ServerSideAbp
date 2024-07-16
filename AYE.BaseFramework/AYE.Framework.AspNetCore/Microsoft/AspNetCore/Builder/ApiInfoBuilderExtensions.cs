using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using AYE.Framework.AspNetCore.Microsoft.AspNetCore.Middlewares;

namespace AYE.Framework.AspNetCore.Microsoft.AspNetCore.Builder
{

    /// <summary>
    /// 用于在ASP.NET Core应用程序中添加自定义的中间件
    /// 通过这种方式，ApiInfoMiddleware 将被添加到中间件管道中，
    /// 处理每个进入的HTTP请求。这有助于实现一些跨切面功能，如日志记录、身份验证等
    /// </summary>
    public static class ApiInfoBuilderExtensions
    {
        public static IApplicationBuilder UseYiApiHandlinge([NotNull] this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiInfoMiddleware>();
            return app;

        }
    }
}
