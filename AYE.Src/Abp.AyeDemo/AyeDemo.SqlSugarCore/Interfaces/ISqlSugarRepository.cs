using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace AyeDemo.SqlSugarCore.Interfaces;

public interface ISqlSugarRepository<TEntity> : IRepository<TEntity>, ISimpleClient<TEntity> where TEntity : class, IEntity, new()
{
    //ISqlSugarClient _Db { get; }
    //ISugarQueryable<TEntity> _DbQueryable { get; set; }
    public ISugarQueryable<TEntity> _DbQueryable { get; set; }
    public ISqlSugarClient _Db { get; set; }
}


public interface ISqlSugarRepository<TEntity, TKey> : ISqlSugarRepository<TEntity>, IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
{


}