using AyeDemo.SqlSugarCore.Interfaces;
using Volo.Abp.Uow;

namespace AyeDemo.SqlSugarCore.Uow
{
    public class SqlSugarTransactionApi : ITransactionApi, ISupportsRollback
    {
        private ISqlSugarDbContext _sqlsugarDbContext;

        public SqlSugarTransactionApi(ISqlSugarDbContext sqlsugarDbContext)
        {
            _sqlsugarDbContext = sqlsugarDbContext;
        }

        public ISqlSugarDbContext GetDbContext()
        {

            return _sqlsugarDbContext;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            //  await _sqlsugarDbContext.SqlSugarClient.Ado.CommitTranAsync();
            await Task.CompletedTask;
        }

        public void Dispose()
        {
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            //   await _sqlsugarDbContext.SqlSugarClient.Ado.RollbackTranAsync();
            await Task.CompletedTask;
        }
    }
}
