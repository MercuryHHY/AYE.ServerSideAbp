using AyeDemo.Application;
using AyeDemo.SqlSugarCore;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace AyeDemo.Web
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAutofacModule),
        typeof(AbpSwashbuckleModule),


        typeof(AyeDemoApplicationModule),
        typeof(AyeDemoSqlSugarCoreModule)
        )]
    public class AyeDemoWebModule : AbpModule
    {
        public override Task ConfigureServicesAsync(ServiceConfigurationContext context)
        {
            //Configure<AbpAuditingOptions>(
            //    options =>
            //    {
            //        //默认关闭，开启会有大量的审计日志
            //        options.IsEnabled = false;
            //        //审计日志过滤器
            //        options.AlwaysLogSelectors.Add(x => Task.FromResult(true));
            //    });

            Configure<AbpAntiForgeryOptions>(options =>
            {
                options.AutoValidate = false;
            });


            //动态Api
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(AyeDemoApplicationModule).Assembly, options => options.RemoteServiceName = "default");
            });



            return Task.CompletedTask;  
        }



        public override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
        { 
        
            
            
            return Task.CompletedTask;
        }



    }
}
