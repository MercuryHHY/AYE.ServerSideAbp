using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;
using Volo.Abp;
using AyeDemo.SqlSugarCore.Interfaces;
using AyeDemo.Domain.Helper;

namespace AyeDemo.SqlSugarCore.Repositories;

public class SqlSugarRepository<TEntity> : SimpleClient<TEntity>, ISqlSugarRepository<TEntity>, IRepository<TEntity> where TEntity : class, IEntity, new()
{
    //public ISqlSugarClient _Db => GetDbContextAsync().Result;
    public ISqlSugarClient _Db { get { return base.Context; } set { } }

    public ISugarQueryable<TEntity> _DbQueryable { get { return base.Context.Queryable<TEntity>(); } set { } }

    public IAsyncQueryableExecuter AsyncExecuter { get; }

    public bool? IsChangeTrackingEnabled => false;

    //ISugarQueryable<TEntity> ISqlSugarRepository<TEntity>._DbQueryable { get => GetDbContextAsync().Result.Queryable<TEntity>(); set=> GetDbContextAsync().Result.Queryable<TEntity>(); }

    public SqlSugarRepository(ISqlSugarClient context) : base(context)
    {
        
    }

   

    /// <summary>
    /// 获取DB
    /// </summary>
    /// <returns></returns>
    public virtual async Task<ISqlSugarClient> GetDbContextAsync()
    {
        return _Db;
    }

    /// <summary>
    /// 获取简单Db
    /// </summary>
    /// <returns></returns>
    public virtual async Task<SimpleClient<TEntity>> GetDbSimpleClientAsync()
    {
        var db = await GetDbContextAsync();
        return new SimpleClient<TEntity>(db);
    }

    #region Abp模块

    public virtual async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        return await GetFirstAsync(predicate);
    }

    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        return await GetFirstAsync(predicate);
    }

    public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        await this.DeleteAsync(predicate);
    }

    public virtual async Task DeleteDirectAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        await this.DeleteAsync(predicate);
    }

    public IQueryable<TEntity> WithDetails()
    {
        throw new NotImplementedException();
    }

    public IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<TEntity>> WithDetailsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<TEntity>> WithDetailsAsync(params Expression<Func<TEntity, object>>[] propertySelectors)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<TEntity>> GetQueryableAsync()
    {
        throw new NotImplementedException();
    }

    public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = false, CancellationToken cancellationToken = default)
    {
        return await GetListAsync(predicate);
    }

    public virtual async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        return await InsertReturnEntityAsync(entity);
    }

    public virtual async Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        await InsertRangeAsync(entities.ToList());
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        await UpdateAsync(entity);
        return entity;
    }

    public virtual async Task UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        await UpdateRangeAsync(entities.ToList());
    }

    public virtual async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        await DeleteAsync(entity);
    }

    public virtual async Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        await DeleteAsync(entities.ToList());
    }

    public virtual async Task<List<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default)
    {
        return await GetListAsync();
    }

    public virtual async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
    {
        return await this.CountAsync(_ => true);
    }

    public virtual async Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, bool includeDetails = false, CancellationToken cancellationToken = default)
    {
        return await GetPageListAsync(_ => true, skipCount, maxResultCount);
    }
    #endregion

    public virtual async Task<List<TEntity>> GetPageListAsync(Expression<Func<TEntity, bool>> whereExpression, int pageNum, int pageSize)
    {
        return await (await GetDbSimpleClientAsync()).GetPageListAsync(whereExpression, new PageModel() { PageIndex = pageNum, PageSize = pageSize });
    }

    public virtual async Task<List<TEntity>> GetPageListAsync(Expression<Func<TEntity, bool>> whereExpression, int pageNum, int pageSize, Expression<Func<TEntity, object>>? orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
    {
        return await (await GetDbSimpleClientAsync()).GetPageListAsync(whereExpression, new PageModel { PageIndex = pageNum, PageSize = pageSize }, orderByExpression, orderByType);
    }


}

public class SqlSugarRepository<TEntity, TKey> : SqlSugarRepository<TEntity>, ISqlSugarRepository<TEntity, TKey>, IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
{
    public SqlSugarRepository(ISqlSugarClient context) : base(context)
    {
    }

    public virtual async Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        await DeleteByIdAsync(id);
    }

    public virtual async Task DeleteManyAsync(IEnumerable<TKey> ids, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        await DeleteByIdsAsync(ids.Select(x => (object)x).ToArray());
    }

    public virtual async Task<TEntity?> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        return await GetByIdAsync(id);
    }

    public virtual async Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        return await GetByIdAsync(id);
    }
}