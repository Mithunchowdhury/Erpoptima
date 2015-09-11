using System;

namespace ERPOptima.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        ErpOptimaContext Get();
    }
}
