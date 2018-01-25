using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Diagnostics;
using Roc.Data;
using ZFC.Shop.Utility;
using ZFC.Shop.Entity;

namespace ZFC.Shop.Data
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected DbClient client;
        protected ILog log;
        protected SqlLam<T> sql;

        private Stopwatch watch;
        public RepositoryBase()
        {
            client = new DbClient();

            sql = this.GetSqlLam<T>();

            log = LogFactory.GetLogger();
            client.AfterDb = (s, ts) => WriteLog(s, null, ts);
            client.Error = (s, e) => WriteLog(s, e, client.GetElapsed());
            watch = new Stopwatch();
        }

        #region 写日志
        private void WriteLog(SqlEntity entity, Exception e, TimeSpan ts)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SQL语句: {0}", entity.Text).AppendLine();
            sb.AppendFormat("参数: ").AppendLine();

            foreach (var item in entity.Parameters)
            {
                string name = item.Name;
                if (!name.StartsWith("@")) name = "@" + name;
                sb.AppendFormat("DECLARE {0} VARCHAR(50) SET {0}='{1}' ", name, item.Value).AppendLine();
            }
            if (e != null)
            {
                sb.AppendFormat("错误信息: {0}", e.ToString());
                log.Log("SQL_ERROR", "错误记录", sb.ToString());
            }
            else
            {
                if (ts != TimeSpan.Zero) sb.AppendFormat("耗时: [{0}]ms", ts.TotalMilliseconds);
                log.Log("SQL", "日志记录", sb.ToString());
            }
        }

        public void WriteLog(LogType type, string title, string msg)
        {
            log.Log(type, title, msg);
        }

        public void WriteLog(string fileName, string title, string msg)
        {
            log.Log(fileName, title, msg);
        }

        public void WriteErrorLog(string title, string msg)
        {
            log.Error(title, msg);
        }

        public void WriteErrorLog(string title, Exception e)
        {
            log.Error(title, e.ToString());
        }
        #endregion

        #region 查询

        public TReturn GetEntity<TReturn>(Expression<Func<T, bool>> where = null)
        {
            return this.GetList<TReturn>(where).FirstOrDefault();
        }

        public TReturn GetEntity<TReturn>(string sql, Dictionary<string, object> parameters, CommandType type = CommandType.Text)
        {
            return this.GetList<TReturn>(sql, parameters, type).FirstOrDefault();
        }

        public T GetEntity(Expression<Func<T, bool>> where = null)
        {
            return this.GetList(where).FirstOrDefault();
        }

        public T GetEntity(string sql, Dictionary<string, object> parameters, CommandType type = CommandType.Text)
        {
            return this.GetList(sql, parameters, type).FirstOrDefault();
        }

        public dynamic GetDynamicEntity(Expression<Func<T, bool>> where = null)
        {
            return client.GetList(sql.GetSql(), sql.GetParameters()).FirstOrDefault();
        }

        public IEnumerable<T> GetList(Expression<Func<T, bool>> where = null)
        {
            if (where != null) sql.Where(where);
            return client.GetList<T>(sql.GetSql(), sql.GetParameters());
        }

        public IEnumerable<T> GetList(string sql, Dictionary<string, object> parameters, CommandType type = CommandType.Text)
        {
            return client.GetList<T>(sql, parameters, type);
        }

        public IEnumerable<TReturn> GetList<TReturn>(Expression<Func<T, bool>> where = null)
        {
            if (where != null) sql.Where(where);
            return client.GetList<TReturn>(sql.GetSql(), sql.GetParameters());
        }

        public IEnumerable<TReturn> GetList<TReturn>(string sql, Dictionary<string, object> parameters, CommandType type = CommandType.Text)
        {
            return client.GetList<TReturn>(sql, parameters, type);
        }

        public IEnumerable<dynamic> GetDynamicList()
        {
            return client.GetList(sql.GetSql(), sql.GetParameters());
        }

        public IEnumerable<dynamic> GetDynamicList(string sql, Dictionary<string, object> parameters, CommandType type = CommandType.Text)
        {
            return client.GetList(sql, parameters, type);
        }

        public DataTable GetDataTable(Expression<Func<T, bool>> where = null)
        {
            if (where != null) sql.Where(where);
            return client.ExecuteDataTable(sql.GetSql(), sql.GetParameters());
        }

        public DataTable GetDataTable(string sql, Dictionary<string, object> parameters, CommandType type = CommandType.Text)
        {
            return client.ExecuteDataTable(sql, parameters, type);
        }

        public GridReader GetReader(Expression<Func<T, bool>> where = null)
        {
            if (where != null) sql.Where(where);
            return client.GetReader(sql.GetSql(), sql.GetParameters(), CommandType.Text);
        }

        public GridReader GetReader(string sql, Dictionary<string, object> parameters, CommandType type = CommandType.Text)
        {
            return client.GetReader(sql, parameters, type);
        }

        public GridReader GetReader(string sql, CommandType type, params SqlParameterEntity[] parameters)
        {
            return client.GetReader(sql, parameters, type);
        }

        public IEnumerable<T> GetPageList(PaginationIn p)
        {
            return this.GetPageList<T>(p);
        }

        public IEnumerable<TReturn> GetPageList<TReturn>(PaginationIn p)
        {
            p.WatchStart();
            int totalCount = 0;
            sql.QueryPage(p.page, p.rows);

            var reader = client.GetReader(sql.GetSql(), sql.GetParameters());
            var list = reader.Read<TReturn>();
            totalCount = reader.Read<int>().FirstOrDefault();
            p.records = totalCount;
            p.WatchEnd();
            return list;
        }

        public IEnumerable<dynamic> GetDynamicPageList(PaginationIn p)
        {
            return this.GetDynamicPageList<T>(p, sql);
        }
        public IEnumerable<dynamic> GetDynamicPageList<TResult>(PaginationIn p, SqlLam<TResult> sql)
        {
            p.WatchStart();
            int totalCount = 0;
            sql.QueryPage(p.page, p.rows);

            var reader = client.GetReader(sql.GetSql(), sql.GetParameters());
            var list = reader.Read();
            totalCount = reader.Read<int>().FirstOrDefault();
            p.records = totalCount;
            p.WatchEnd();
            return list;
        }
        public IEnumerable<dynamic> GetDynamicPageList<TResult>(PaginationIn p, string sql, List<Dictionary<string, object>> parameters)
        {
            p.WatchStart();
            int totalCount = 0;
            //sql.QueryPage(p.page, p.rows);

            int pageNumber = p.page;
            int pageSize = p.rows;
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (var item in parameters)
            {
                if (item != null && item.Count > 0)
                {
                    foreach (var q in item)
                    {
                        dic.Add(q.Key, q.Value);
                    }
                }
            }
            var reader = client.GetReader(sql, dic, CommandType.StoredProcedure);
            var list = reader.Read();
            totalCount = reader.Read<int>().FirstOrDefault();
            p.records = totalCount;
            p.WatchEnd();
            return list;
        }
        public GridReader GetReader(IEnumerable<string> sqls, List<Dictionary<string, object>> parameters)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (var item in parameters)
            {
                if (item != null && item.Count > 0)
                {
                    foreach (var p in item)
                    {
                        dic.Add(p.Key, p.Value);
                    }
                }
            }
            return client.GetReader(string.Join(";", sqls), dic);
        }

        #endregion

        #region 执行sql语句

        /// <summary>
        /// 使用事务
        /// </summary>
        /// <param name="action"></param>
        public bool UseTran(Action action)
        {
            return client.UseTran(action);
        }

        /// <summary>
        /// 执行sql语句 添加， 修改， 删除操作
        /// </summary>
        /// <returns></returns>
        public int Execute()
        {
            return client.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 执行sql语句 传入第二实体 添加， 修改， 删除操作
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Execute<TReturn>(SqlLam<TReturn> sql)
        {
            return client.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 执行sql语句 添加， 修改， 删除操作
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public int Execute(string sql, Dictionary<string, object> parameters, CommandType type = CommandType.Text)
        {
            return client.ExecuteNonQuery(sql, parameters);
        }

        #endregion

        #region Add

        public bool Add(T model, bool ignoreKey = true)
        {
            return this.Add(model, null, ignoreKey);
        }

        public bool Add(T model, Expression<Func<T, object>> fields, bool ignoreKey = true)
        {
            if (fields == null)
                sql.Insert(model, ignoreKey);
            else
                sql.Insert<object>(model, fields);
            int count = this.Execute();
            return count > 0 ? true : false;
        }

        public TKey Add<TKey>(T model) where TKey : struct
        {
            sql.Insert(model, increment: true);
            return this.GetEntity<TKey>(null);
        }
        #endregion

        #region Update

        public bool Update(T model, Expression<Func<T, bool>> where = null)
        {
            return this.Update(model, null, where);
        }

        public bool Update(T model, Expression<Func<T, object>> fields, Expression<Func<T, bool>> where)
        {
            if (fields != null) sql.Update(model, fields);
            else sql.Update(model);

            if (where != null) sql.Where(where);
            int count = this.Execute();
            return count > 0 ? true : false;
        }

        public bool UpdateExclude(T model, Expression<Func<T, object>> fields, Expression<Func<T, bool>> where)
        {
            if (fields != null) sql.Update(model, fields, true);
            else sql.Update(model);

            if (where != null) sql.Where(where);
            int count = this.Execute();
            return count > 0 ? true : false;
        }

        #endregion

        #region Delete

        public bool Delete()
        {
            return this.Delete(null);
        }

        public bool Delete(Expression<Func<T, bool>> where = null)
        {
            if (where != null) sql.Delete(where);
            else sql.Delete();

            int count = this.Execute();
            return count > 0 ? true : false;
        }

        #endregion

        #region Exist

        public bool Exist(Expression<Func<T, bool>> where = null)
        {
            if (where != null) sql.Where(where);

            sql.Select("1 as Num");

            int num = this.GetEntity<int>(null);
            return num > 0;
        }

        #endregion

        #region others

        public void ChangeDb(string dbKey)
        {
            client.ChangeDb(dbKey);
        }

        public SqlLam<TReturn> GetSqlLam<TReturn>(string aliasName = "a", bool clearSql = true)
        {
            var sql = new SqlLam<TReturn>(aliasName, ProviderType.SQLServer2005);
            sql.ClearSql = clearSql;
            return sql;
        }

        #endregion

        #region 存储过程

        public DataSet GetDataSet(string sql, CommandType type, params SqlParameterEntity[] parameters)
        {
            return client.ExecuteDataSet(sql, parameters, type);
        }

        public DataTable GetDataTable(string sql, CommandType type, params SqlParameterEntity[] parameters)
        {
            return client.ExecuteDataTable(sql, parameters, type);
        }

        public int Execute(string sql, CommandType type, params SqlParameterEntity[] parameters)
        {
            return client.ExecuteNonQuery(sql, parameters, type);
        }
        #endregion

        #region 导入数据

        public bool ImportData(DataTable dt, SqlBulkCopyColumnMapping[] mappings = null, int size = 5000)
        {
            string tableName = sql.GetTableName();
            return ImportData(tableName, dt, mappings, size);
        }

        public bool ImportData(string tableName, DataTable dt, SqlBulkCopyColumnMapping[] mappings = null, int size = 5000)
        {
            if (tableName.IsEmpty()) return false;
            try
            {
                SqlConnection sqlConn = new SqlConnection(client.ConnectionString);
                sqlConn.Open();
                SqlBulkCopy copy = new SqlBulkCopy(sqlConn);
                copy.BatchSize = size;
                copy.BulkCopyTimeout = 600;//10分钟
                copy.DestinationTableName = tableName;
                if (mappings != null && mappings.Length > 0)
                {
                    foreach (var item in mappings)
                    {
                        copy.ColumnMappings.Add(item);
                    }
                }

                copy.WriteToServer(dt);
                copy.Close();
                sqlConn.Close();
                return true;
            }
            catch (Exception e)
            {
                this.WriteLog("IMPORT_DATA_ERROR", "导入数据发生错误--ImportData", "错误信息: " + e.ToString());
                return false;
            }
        }

        public bool ImportData(string tableName, IEnumerable<T> list, SqlBulkCopyColumnMapping[] mappings = null, int size = 5000)
        {
            List<string> columns = null;
            if (mappings != null)
            {
                columns = new List<string>();
                foreach (var item in mappings)
                {
                    columns.Add(item.SourceColumn);
                }
            }
            var dt = list.ToTable(columns, tableName);
            if (dt == null || dt.Rows.Count < 1) return false;

            return ImportData(tableName, dt, mappings, size);
        }

        public bool ImportData(IEnumerable<T> list, SqlBulkCopyColumnMapping[] mappings = null, int size = 5000)
        {
            string tableName = sql.GetTableName();
            return ImportData(tableName, list, mappings, size);
        }
        #endregion
    }
}
