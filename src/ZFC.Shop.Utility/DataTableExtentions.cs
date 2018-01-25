using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace ZFC.Shop.Utility
{
    public static class DataTableExtentions
    {
        public static DataTable Copy(this DataTable source, int index, int size = 100)
        {
            if (source == null) return null;

            int totalCount = source.Rows.Count;
            if (totalCount < size) return source.Copy();

            int begin = index * size;
            int end = (index + 1) * size;
            end = end > totalCount ? totalCount : end;

            DataTable dest = new DataTable();

            foreach (DataColumn item in source.Columns)
            {
                dest.Columns.Add(new DataColumn(item.ColumnName, item.DataType));
            }

            for (int i = begin; i < end; i++)
            {
                DataRow dr = dest.NewRow();
                dr.ItemArray = source.Rows[i].ItemArray;
                dest.Rows.Add(dr);
            }
            return dest;
        }

        public static DataTable ToTable<T>(this IEnumerable<T> list, IEnumerable<string> columns = null, string tableName = "") where T : class
        {
            if (list == null || list.Count() < 1) return null;

            Type type = typeof(T);
            PropertyInfo[] ps = type.GetProperties();

            if (columns == null || columns.Count() < 1)
            {
                columns = ps.Select(m => m.Name);
            }
            if (columns == null || columns.Count() < 1) return null;

            DataTable dt = new DataTable();
            if (!tableName.IsEmpty()) dt.TableName = tableName;

            foreach (var item in columns)
            {
                dt.Columns.Add(item);
            }

            foreach (var item in list)
            {
                var dr = dt.NewRow();
                foreach (var c in columns)
                {
                    object value = null;
                    var p = ps.FirstOrDefault(m => string.Equals(m.Name, c, StringComparison.CurrentCultureIgnoreCase));
                    if (p != null) value = p.GetValue(item, null);
                    if (value == null) value = DBNull.Value;
                    dr[c] = value;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static string ToString(this DataRow dr, string key)
        {
            if (dr == null) return string.Empty;
            if (string.IsNullOrEmpty(key)) return string.Empty;
            object obj = dr[key];
            if (obj == null || obj == System.DBNull.Value)
                return string.Empty;
            return obj.ToString();
        }

        public static string ToString(this DataRow dr, int index)
        {
            if (dr == null) return string.Empty;
            if (index == -1) return string.Empty;
            object obj = dr[index];
            if (obj == null || obj == System.DBNull.Value)
                return string.Empty;
            return obj.ToString();
        }

        public static int ToInt(this DataRow dr, string key, int dValue = 0)
        {
            string value = dr.ToString(key);
            return value.ToInt(dValue);
        }

        public static decimal ToDecimal(this DataRow dr, int index, long dValue = 0)
        {
            string value = dr.ToString(index);
            return value.ToDecimal(dValue);
        }

        public static decimal ToDecimal(this DataRow dr, string key, long dValue = 0)
        {
            string value = dr.ToString(key);
            return value.ToDecimal(dValue);
        }

        public static long ToInt64(this DataRow dr, int index, long dValue = 0)
        {
            string value = dr.ToString(index);
            return value.ToInt64(dValue);
        }

        public static long ToInt64(this DataRow dr, string key, long dValue = 0)
        {
            string value = dr.ToString(key);
            return value.ToInt64(dValue);
        }

        public static DateTime ToIntDateTime(this DataRow dr, string key, DateTime dValue)
        {
            string value = dr.ToString(key);
            return value.ToDateTime(dValue);
        }

        public static bool ToBoolean(this DataRow dr, string key, bool dValue = false)
        {
            string value = dr.ToString(key);
            return value.ToBoolean(dValue);
        }
    }
}
