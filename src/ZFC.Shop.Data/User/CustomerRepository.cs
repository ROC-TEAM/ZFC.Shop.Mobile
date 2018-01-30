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

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <param name="success"></param>
        /// <returns></returns>
        bool UpdateUser(Customer model, bool success);
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
            userSql.Where(m => m.Active == true && m.Deleted == false);
            userSql.And().Begin();
            userSql.Or(m => m.Username == email);
            userSql.Or(m => m.Email == email);
            userSql.End();

            return base.GetEntity(userSql.GetSql(), userSql.GetParameters());
        }

        public CustomerPassword GetUserPassword(int userId)
        {
            var passowrdSql = base.GetSqlLam<CustomerPassword>();
            passowrdSql.SelectAll().Where(m => m.CustomerId == userId);
            return base.GetEntity<CustomerPassword>(passowrdSql.GetSql(), passowrdSql.GetParameters());
        }

        public bool UpdateUser(Customer model, bool success)
        {
            bool flag = false;
            if (success)
            {
                flag = base.Update(model, m => new
                {
                    m.FailedLoginAttempts,
                    m.CannotLoginUntilDateUtc,
                    m.RequireReLogin,
                    m.LastLoginDateUtc,
                    m.LastIpAddress
                }, m => m.Id == model.Id);
            }
            else
            {
                flag = base.Update(model, m => new
                {
                    m.FailedLoginAttempts,
                    m.CannotLoginUntilDateUtc
                }, m => m.Id == model.Id);
            }
            return flag;
        }
    }
}
