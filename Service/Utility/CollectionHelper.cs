using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Utility
{
    public static class CollectionHelper
    {
        public static List<T> ToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();
                foreach (var row in table.AsEnumerable())
                {
                    T obj  = new T();
                    foreach (var property in obj.GetType().GetProperties())
                    {
                        try
                        {
                            var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            if (!Convert.IsDBNull(row[property.Name]))
                            {
                                var safeValue = row[property.Name] == null ? null : Convert.ChangeType(row[property.Name], propertyType);
                                property.SetValue(obj, safeValue, null);
                            }
                        }catch(Exception ex)
                        {
                            continue;
                        }
                    }
                    list.Add(obj);
                }
                return list;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
