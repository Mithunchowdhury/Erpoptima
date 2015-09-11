using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using ERPOptima.Data;
using ERPOptima.Data.Infrastructure;
using System.Data;
using System.Data.SqlClient;

namespace ERPOptima.Data.Infrastructure
{
    public abstract class BaseRepository<T> where T : class
    {
        private ErpOptimaContext dataContext;
        private readonly IDbSet<T> dbset;
        protected BaseRepository(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        public ErpOptimaContext DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }
        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }
        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }
        public virtual T GetById(long id)
        {
            return dbset.Find(id);
        }
        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }
        public virtual IEnumerable<T> GetManyWithInclude(Expression<Func<T, bool>> where, string include) {


            return dbset.Include(include).Where(where).ToList();
        
        
        }
        /// <summary>
        /// Return a paged list of entities
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="page">Which page to retrieve</param>
        /// <param name="where">Where clause to apply</param>
        /// <param name="order">Order by to apply</param>
        /// <returns></returns>
        //public virtual IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order)
        //{
        //    var results = dbset.OrderBy(order).Where(where).GetPage(page).ToList();
        //    var total = dbset.Count(where);
        //    return new StaticPagedList<T>(results, page.PageNumber, page.PageSize, total);
        //}

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }

        public int ExecuteNonQuery(string storeProcedure, SqlParameter[] parameters, bool isStoredProcedure = true)
        {

            int ret = 0;
            var conn = DataContext.Database.Connection;
            var connectionState = conn.State;

            try
            {

                if (connectionState != ConnectionState.Open)
                    conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = storeProcedure;
                    if (isStoredProcedure)
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                    }



                    if (parameters != null)
                    {
                        cmd.Parameters.Clear();
                        foreach (var item in parameters)
                        {
                            cmd.Parameters.Add(item);
                        }
                    }

                    ret = cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connectionState != ConnectionState.Open)
                    conn.Close();
            }
            return ret;

        }
        public DataTable GetFromStoredProcedure(string storeProcedure, SqlParameter[] parameters,bool isStoredProcedure=true) 
        {

            DataTable dt = new DataTable();
            var conn = DataContext.Database.Connection;
            var connectionState = conn.State;

            try
            {
               
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = storeProcedure;
                        if (isStoredProcedure)
                        {
                           
                            cmd.CommandType = CommandType.StoredProcedure;
                        }
                        
                        

                        if (parameters !=null)
                        {
                            foreach (var item in parameters)
                            {
                                cmd.Parameters.Add(item);
                            }
                        }
                       
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connectionState != ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        
        }
    }
}
