using AyeDemo.SqlSugarCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Uow;

namespace AyeDemo.SqlSugarCore.Uow
{
    public class SqlSugarDatabaseApi : IDatabaseApi
    {
        public ISqlSugarDbContext DbContext { get; }

        public SqlSugarDatabaseApi(ISqlSugarDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
