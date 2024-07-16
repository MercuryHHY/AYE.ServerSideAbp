using AyeDemo.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace AyeDemo.Application.Contracts
{
    [DependsOn(
        typeof(AbpDddApplicationContractsModule),
        typeof(AyeDemoDomainSharedModule)
        )]
    public class AyeDemoApplicationContractsModule : AbpModule
    {
    }
}
