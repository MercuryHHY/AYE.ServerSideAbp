using AyeDemo.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace AyeDemo.Domain
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AyeDemoDomainSharedModule)
        )]
    public class AyeDemoDomainModule : AbpModule
    {
    }
}
