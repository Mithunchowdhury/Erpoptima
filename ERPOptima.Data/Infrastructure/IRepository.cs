using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data;
using System.Data.SqlClient;

namespace ERPOptima.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        //void Save(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(long id);
        T GetById(string id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        

        IEnumerable<T> GetManyWithInclude(Expression<Func<T, bool>> where,string include);
        //IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order);

        DataTable GetFromStoredProcedure(string storeProcedure, SqlParameter[] parameters, bool isStoredProcedure = true);
        int ExecuteNonQuery(string storeProcedure, SqlParameter[] parameters, bool isStoredProcedure = true);
    }
}
