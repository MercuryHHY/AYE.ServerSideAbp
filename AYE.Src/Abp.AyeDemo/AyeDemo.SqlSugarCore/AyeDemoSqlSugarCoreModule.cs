using AyeDemo.Domain;
using AyeDemo.SqlSugarCore.Interfaces;
using AyeDemo.SqlSugarCore.Repositories;
using AyeDemo.SqlSugarCore.Uow;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;

namespace AyeDemo.SqlSugarCore;

[DependsOn(
    typeof(AyeDemoDomainModule)        
    
    )]
public class AyeDemoSqlSugarCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var service = context.Services;

        ISqlSugarClient Db = new SqlSugarClient(new ConnectionConfig() 
        {
            ConnectionString = "datasource=aye-AbpWebsqllite.db",
            DbType = DbType.Sqlite,
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute,
            ConfigureExternalServices = new ConfigureExternalServices
            {
                //注意:  这儿AOP设置不能少
                EntityService = (c, p) =>
                {
                    /***高版C#写法***/
                    //支持string?和string  
                    if (p.IsPrimarykey == false && new NullabilityInfoContext()
                     .Create(c).WriteState is NullabilityState.Nullable)
                    {
                        p.IsNullable = true;
                    }
                }
            }
        });

        service.AddSingleton<ISqlSugarClient>( Db );
        //context.Services.AddTransient<>

        service.AddTransient(typeof(IRepository<>), typeof(SqlSugarRepository<>));
        service.AddTransient(typeof(IRepository<,>), typeof(SqlSugarRepository<,>));
        service.AddTransient(typeof(ISqlSugarRepository<>), typeof(SqlSugarRepository<>));
        service.AddTransient(typeof(ISqlSugarRepository<,>), typeof(SqlSugarRepository<,>));
        //service.AddTransient(typeof(ISugarDbContextProvider<>), typeof(UnitOfWorkSqlsugarDbContextProvider<>));

    }

    public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
    { 

        //利用服务代理从容器中拿到 事先注册的DB
        var db= context.ServiceProvider.GetRequiredService<ISqlSugarClient>();

        //在这里可以进行 CodeFirst 之类的数据表结构初始化操作
        Console.WriteLine("CodeFirst 正在执行");
        //建库：如果不存在创建数据库存在不会重复创建 createdb
        db.DbMaintenance.CreateDatabase();
        Type[] types = typeof(AyeDemoDomainModule).Assembly.GetTypes()
                        .Where(it => it.FullName != null && it.FullName.Contains("Domain.Entities") && it.Name.Contains("Root"))//命名空间过滤，当然也可以写其他条件过滤
                        .ToArray();
        db.CodeFirst.SetStringDefaultLength(200).InitTables(types);//根据types创建表
        Console.WriteLine("CodeFirst 执行完成！！！！！！");





    }







}
