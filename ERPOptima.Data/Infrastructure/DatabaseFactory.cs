using ERPOptima.Model;

namespace ERPOptima.Data.Infrastructure
{
public class DatabaseFactory : Disposable, IDatabaseFactory
{
    private ErpOptimaContext dataContext;
    public ErpOptimaContext Get()
    {
        return dataContext ?? (dataContext = new ErpOptimaContext());
    }
    protected override void DisposeCore()
    {
        if (dataContext != null)
            dataContext.Dispose();
    }
}
}
