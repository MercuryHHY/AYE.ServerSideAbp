using Autofac.Core;
using AYE.Framework.AspNetCore;
using AyeDemo.Application;
using AyeDemo.SqlSugarCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.DependencyInjection;
using AYE.Framework.AspNetCore.Microsoft.AspNetCore.Builder;
using AYE.Framework.AspNetCore.Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Swashbuckle;
using Microsoft.OpenApi.Models;
using Volo.Abp.AspNetCore.MultiTenancy;

namespace AyeDemo.Web
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAutofacModule),
        typeof(AbpSwashbuckleModule),

        typeof(AyeFrameworkAspNetCoreModule),
        typeof(AyeDemoApplicationModule),
        typeof(AyeDemoSqlSugarCoreModule)
        )]
    public class AyeDemoWebModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override Task ConfigureServicesAsync(ServiceConfigurationContext context)
        {

            var configuration = context.Services.GetConfiguration();//配置
            var host = context.Services.GetHostingEnvironment();//运行环境
            var service = context.Services;//服务


            Configure<AbpAuditingOptions>(
                options =>
                {
                    //默认关闭，开启会有大量的审计日志
                    options.IsEnabled = false;
                    //审计日志过滤器
                    options.AlwaysLogSelectors.Add(x => Task.FromResult(true));
                });

            Configure<AbpAntiForgeryOptions>(options =>
            {
                options.AutoValidate = false;
            });


            //动态Api
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(AyeDemoApplicationModule).Assembly, options => options.RemoteServiceName = "default");
                //这里可以添加依赖模块中的 动态API
            });

           
            Configure<AbpAntiForgeryOptions>(options =>
            {
                options.AutoValidate = false;
            });



            service.AddControllers();
            service.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            service.AddAbpSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "AyeDemo", Version = "v1" });
                    options.DocInclusionPredicate((docNmae, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                }
            );



            //跨域
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]!
                                .Split(";", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            

            //授权
            context.Services.AddAuthorization();

            return Task.CompletedTask;  
        }



        public override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
        {

            var service = context.ServiceProvider;

            var env = context.GetEnvironment();
            var app = context.GetApplicationBuilder();

            app.UseRouting();

            //跨域
            app.UseCors(DefaultCorsPolicyName);

            //无感token，先刷新再鉴权
            //app.UseRefreshToken();

            //鉴权
            app.UseAuthentication();

            //多租户
            //app.UseMultiTenancy();

            //swagger
            //app.UseYiSwagger();
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            //请求处理
            app.UseYiApiHandlinge();

            //静态资源
            app.UseStaticFiles("/api/app/wwwroot");
            app.UseDefaultFiles();
            app.UseDirectoryBrowser("/api/app/wwwroot");

            //工作单元
            app.UseUnitOfWork();

            //授权
            app.UseAuthorization();

            //审计日志
            app.UseAuditing();

            //日志记录
            //app.UseAbpSerilogEnrichers();

            //终节点
            app.UseConfiguredEndpoints();

            return Task.CompletedTask;
        }



    }
}
