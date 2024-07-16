using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;
using Yi.Framework.Core.Extensions;
using static System.Net.WebRequestMethods;

namespace Yi.Framework.AspNetCore.Microsoft.AspNetCore.Middlewares
{

    /// <summary>
    /// 中间件
    /// 在响应处理开始前检查特定条件，如果条件满足（状态码为200且内容类型为Excel），
    /// 则动态设置下载文件名。通过这种方式，可以方便地处理文件下载的场景
    /// </summary>
    public class ApiInfoMiddleware : IMiddleware, ITransientDependency
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.OnStarting(() =>
            {
                if (context.Response.StatusCode == StatusCodes.Status200OK
&& context.Response.Headers["Content-Type"].ToString() == "application/vnd.ms-excel")
                {
                    context.FileAttachmentHandle($"{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.xlsx");
                }
                return Task.CompletedTask;
            });

            await next(context);



        }
    }
}
