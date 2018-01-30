using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZFC.Shop.Entity;
using System.Reflection;
using Roc.Data;
using TableAttribute = ZFC.Shop.Entity.TableAttribute;
using ColumnAttribute = ZFC.Shop.Entity.ColumnAttribute;

namespace ZFC.Shop.Data
{
    public class TableConfig
    {
        public static void RegisterTables()
        {
            var assembly = typeof(AjaxResult).Assembly;
            var types = assembly.GetExportedTypes();
            
            foreach (Type item in types)
            {
                var attr = item.GetCustomAttributes(true).OfType<TableAttribute>().FirstOrDefault();
                if (attr == null) continue;

                var table = new SqlTableEntity(item);
                table.Columns = new List<SqlColumnEntity>();
                table.Type = 2;
                table.TableName = attr.Name;

                var ps = item.GetProperties();
                foreach (var p in ps)
                {
                    var column = GetColumn(p);
                    if (column != null)
                    {
                        table.AddColumn(column);
                    }
                }

                GlobalConfig.AddTable(table);
            }
        }

        private static SqlColumnEntity GetColumn(PropertyInfo p)
        {
            SqlColumnEntity column = null;

            var attrs = p.GetCustomAttributes(true);
            if (attrs == null || attrs.Length < 1) return column;

            column = new SqlColumnEntity(p.Name);

            int columnType = 1;
            var cattr = attrs.OfType<ColumnAttribute>().FirstOrDefault();
            if (cattr != null)
            {
                if (!string.IsNullOrEmpty(cattr.Name))
                    column.FieldName = cattr.Name;
                column.Key = cattr.IsKey;
                column.Increment = cattr.Increment;
                column.Ignore = cattr.Ignore;
                columnType = 2;
            }
            column.Type = columnType;
            column.ActionType = column.Ignore ? ActionType.Undo : ActionType.ReadOrWrite;
            return column;
        }
    }
}
