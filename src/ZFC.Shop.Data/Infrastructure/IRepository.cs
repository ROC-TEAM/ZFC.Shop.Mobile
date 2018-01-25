using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data;
using Roc.Data;
using ZFC.Shop.Utility;
using System.Data.SqlClient;
using ZFC.Shop.Entity;

namespace ZFC.Shop.Data
{
    public interface IRepository<T> : IDependency where T : class
    {
        #region 查询
        TReturn GetEntity<TReturn>(Expression<Func<T, bool>> where = null);
        TReturn GetEntity<TReturn>(string sql, Dictionary<string, object> parameters, CommandType type = CommandType.Text);

        T GetEntity(Expression<Func<T, bool>> where = null);
        T GetEntity(string sql, Dictionary<string, object> parameters, CommandType type = CommandType.Text);

        dynamic GetDynamicEntity(Expression<Func<T, bool>> where = null);

        IEnumerable<T> GetList(Expression<Func<T, bool>> where = null);
        IEnumerable<T> GetList(string sql, Dictionary<string, object> parameters, CommandType type = CommandType.Text);

        IEnumerable<dynamic> GetDynamicList();
        IEnumerable<dynamic> GetDynamicList(string sql, Dictionary<string, object> parameters, CommandType type = CommandType.Text);

        IEnumerable<TReturn> GetList<TReturn>(Expression<Func<T, bool>> where = null);
        IEnumerable<TReturn> GetList<TReturn>(string sql, Dictionary<string, object> parameters, CommandType type = CommandType.Text);

        DataTable GetDataTable(Expression<Func<T, bool>> where = null);
        DataTable GetDataTable(string sql, Dictionary<string, object> parameters, CommandType type = CommandType.Text);

        GridReader GetReader(Expression<Func<T, bool>> where = null);
        GridReader GetReader(string sql, Dictionary<string, object> parameters, CommandType type = CommandType.Text);

        IEnumerable<dynamic> GetDynamicPageList(PaginationIn p);
        IEnumerable<dynamic> GetDynamicPageList<TResult>(PaginationIn p, SqlLam<TResult> sql);
        IEnumerable<T> GetPageList(PaginationIn p);
        IEnumerable<TReturn> GetPageList<TReturn>(PaginationIn p);

        GridReader GetReader(IEnumerable<string> sqls, List<Dictionary<string, object>> parameters);
        #endregion

        #region 执行sql语句

        bool UseTran(Action action);

        int Execute();

        int Execute<TReturn>(SqlLam<TReturn> sql);

        int Execute(string sql, Dictionary<string, object> parameters, CommandType type = CommandType.Text);

        #endregion

        #region Add

        bool Add(T model, bool ignoreKey = true);

        bool Add(T model, Expression<Func<T, object>> fields, bool ignoreKey = true);

        TKey Add<TKey>(T model) where TKey : struct;

        #endregion

        #region Update

        bool Update(T model, Expression<Func<T, bool>> where = null);

        bool Update(T model, Expression<Func<T, object>> fields, Expression<Func<T, bool>> where);

        /// <summary>
        /// 修改方法 排除某些字段 修改其他字段
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="fields">排除的字段</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        bool UpdateExclude(T model, Expression<Func<T, object>> fields, Expression<Func<T, bool>> where);
        #endregion

        #region Delete

        bool Delete();

        bool Delete(Expression<Func<T, bool>> where = null);
        #endregion

        #region Exist

        bool Exist(Expression<Func<T, bool>> where = null);

        #endregion

        #region others

        /// <summary>
        /// 切换数据库
        /// </summary>
        /// <param name="dbKey">数据库连接key</param>
        void ChangeDb(string dbKey);

        SqlLam<TReturn> GetSqlLam<TReturn>(string aliasName = "a", bool clearSql = true);

        void WriteLog(LogType type, string title, string msg);

        void WriteLog(string fileName, string title, string msg);

        void WriteErrorLog(string title, string msg);

        void WriteErrorLog(string title, Exception e);
        #endregion

        #region 导入数据

        bool ImportData(DataTable dt, SqlBulkCopyColumnMapping[] mappings = null, int size = 5000);
        bool ImportData(string tableName, DataTable dt, SqlBulkCopyColumnMapping[] mappings = null, int size = 5000);
        bool ImportData(IEnumerable<T> list, SqlBulkCopyColumnMapping[] mappings = null, int size = 5000);
        bool ImportData(string tableName, IEnumerable<T> list, SqlBulkCopyColumnMapping[] mappings = null, int size = 5000);

        #endregion
    }
}
