
using AyeDemo.SqlSugarCore.Interfaces;

namespace AyeDemo.SqlSugarCore;

public class AsyncLocalDbContextAccessor
{
    public static AsyncLocalDbContextAccessor Instance { get; } = new();
    public ISqlSugarDbContext? Current
    {
        get => _currentScope.Value;
        set => _currentScope.Value = value;
    }
    public AsyncLocalDbContextAccessor()
    {
        _currentScope = new AsyncLocal<ISqlSugarDbContext?>();
    }
    private readonly AsyncLocal<ISqlSugarDbContext> _currentScope;
}
