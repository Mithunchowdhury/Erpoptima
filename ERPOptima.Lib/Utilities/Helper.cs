using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Lib.Utilities
{
    public enum VoucherType
    {
        CR=1,
        BR=2,
        CP=3,
        BP=4,
        JV=5,
        CT=6
    }
    public static class Helper
    {
        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();
      
                
                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            Type t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                           
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], t), null);
                            //propertyInfo.SetValue(obj, ChangeType<T>(row[prop.Name]), null);
                            
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        private static T ChangeType<T>(object value)
        {
            var t = typeof(T);

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return default(T);
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return (T)Convert.ChangeType(value, t);
        }



        public static DataTable ConvertToDatatable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                try
                {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }
                catch {
                    continue;
                }
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                try
                {
                    table.Rows.Add(values);
                }
                catch
                {
                    
                    continue;
                }
                
            }
            return table;
        }

        public static object FillTo(DataRow dr, Type tmpStorage)
        {
            object oop = Activator.CreateInstance(tmpStorage);
            PropertyInfo[] propertyInfos = oop.GetType().GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (dr.Table.Columns.Contains(propertyInfo.Name))
                {
                    if (dr.IsNull(propertyInfo.Name))
                    {
                        propertyInfo.SetValue(oop, null, null);
                    }
                    else
                    {
                        propertyInfo.SetValue(oop, dr[propertyInfo.Name], null);
                    }
                }
            }

            return oop;
        }
        //public static IEnumerable<T> GetRecursively<T,T2>(this IEnumerable<T> nestedList,Func<T,IEnumerable<T>> func) where T : class
        //{
        //    foreach (T item in nestedList)
        //    {
        //        yield return item;
        //        if (func(item) != null)
        //            foreach (T innerObject in func(item))
        //                foreach (T recursiveInner in GetRecursively(innerObject)))
        //                    yield return recursiveInner;
        //    }
        //}


    }
}
