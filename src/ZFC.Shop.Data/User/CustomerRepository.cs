using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZFC.Shop.Entity;
using ZFC.Shop.Utility;
//using Roc.Data;

namespace ZFC.Shop.Data
{
    public interface IUserRepository : IRepository<Customer>
    {
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Customer GetUser(string email);

        /// <summary>
        /// 查询用户密码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        CustomerPassword GetUserPassword(int userId);
    }

    public class UserRepository : RepositoryBase<Customer>, IUserRepository
    {
        public UserRepository()
        {

        }

        public Customer GetUser(string email)
        {
            var userSql = base.GetSqlLam<Customer>();
            userSql.SelectAll();
            userSql.Where(m => m.Active == true && m.Deleted == false && m.Username == email);

            return base.GetEntity(userSql.GetSql(), userSql.GetParameters());
        }

        public CustomerPassword GetUserPassword(int userId)
        {
            var passowrdSql = base.GetSqlLam<CustomerPassword>();
            passowrdSql.SelectAll().Where(m => m.CustomerId == userId);
            return base.GetEntity<CustomerPassword>(passowrdSql.GetSql(), passowrdSql.GetParameters());
        }
    }
}
