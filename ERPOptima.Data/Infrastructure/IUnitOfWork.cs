using System;

namespace ERPOptima.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
