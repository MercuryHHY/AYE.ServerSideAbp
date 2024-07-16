using AyeDemo.SqlSugarCore.Abstractions;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyeDemo.SqlSugarCore.Interfaces;

public interface ISqlSugarDbContext
{
    //  IAbpLazyServiceProvider LazyServiceProvider { get; set; }
    ISqlSugarClient SqlSugarClient { get; }
    DbConnOptions Options { get; }

    /// <summary>
    /// 数据库备份
    /// </summary>
    void BackupDataBase();
    void SetSqlSugarClient(ISqlSugarClient sqlSugarClient);
}