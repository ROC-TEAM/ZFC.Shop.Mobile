using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZFC.Shop.Entity;
using ZFC.Shop.Utility;
//using Roc.Data;

namespace ZFC.Shop.Data
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetList(int top, string key);

        User GetUser(string wwid);

        User GetUserByWWID(string wwid);
    }

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository()
        {
            base.ChangeDb(DbConfig.BPM);
        }

        public IEnumerable<User> GetList(int top, string key)
        {
            //sql.Top(top);
            string sqlText = string.Empty;
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (!key.IsEmpty())
            {
                sqlText = string.Format(@"
SELECT TOP {0} a.* from T_User AS a WHERE ISNULL(a.Status,'')<>'DELETED' AND (a.WWID LIKE '%'+@key+'%' OR a.CN_Name LIKE '%'+@key+'%' OR a.EN_Name LIKE '%'+@key+'%' OR a.Email LIKE '%'+@key+'%')  ORDER BY a.WWID ", top);
                parameters.Add("@key", key);
            }

            //sql.OrderBy(m => m.UserID);
            return base.GetList(sqlText, parameters);
        }

        public User GetUser(string wwid)
        {
            string sqlText = "SELECT * FROM T_User AS a WHERE ISNULL(a.Status,'')<>'DELETED' AND (a.WWID=@wwid OR a.AD=@wwid)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@wwid", wwid);
            return base.GetEntity(sqlText, parameters);
        }

        public User GetUserByWWID(string wwid)
        {
            string sqlText = "SELECT * FROM T_User AS a WHERE ISNULL(a.Status,'')<>'DELETED' AND a.WWID=@wwid";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@wwid", wwid);
            return base.GetEntity(sqlText, parameters);
        }
    }
}
