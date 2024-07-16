using AyeDemo.Application.Contracts;
using AyeDemo.Application.Contracts.IServices;
using AyeDemo.Application.Services;
using AyeDemo.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;

namespace AyeDemo.Application;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(AyeDemoApplicationContractsModule),
    typeof(AyeDemoDomainModule)
    )]
public class AyeDemoApplicationModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var service = context.Services;
        //service.AddTransient(typeof(IBookAppService), typeof(BookAppService));



    }


}
