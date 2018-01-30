using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZFC.Shop.Entity;
using ZFC.Shop.Data;
using ZFC.Shop.Utility;

namespace ZFC.Shop.Service
{
    public interface ICustomerService : IDependency
    {
        /// <summary>
        /// 获得当前登录用户
        /// </summary>
        /// <returns></returns>
        Customer GetCurrentUser();

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        AjaxResult Login(string eid, string pwd, bool remember);

        /// <summary>
        /// 判断用户是否登录
        /// </summary>
        /// <returns></returns>
        bool IsLogin();

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        AjaxResult Logout();
    }

    public class CustomerService : ICustomerService
    {
        readonly IUserRepository userRep;
        readonly IEncryptionService encryService;
        public CustomerService(IUserRepository ur, IEncryptionService ies)
        {
            userRep = ur;
            encryService = ies;
        }

        public AjaxResult Login(string eid, string pwd, bool remember)
        {
            AjaxResult ajaxResult = new AjaxResult(false);
            var user = userRep.GetUser(eid);
            if (user == null || user.Id < 1)
            {
                ajaxResult.Msg = "没有找到此用户";
                return ajaxResult;
            }
            if (user.CannotLoginUntilDateUtc.HasValue && user.CannotLoginUntilDateUtc.Value > DateTime.UtcNow)
            {
                ajaxResult.Msg = "此用户被锁定,请联系管理员";
                return ajaxResult;
            }
            var password = userRep.GetUserPassword(user.Id);
            if (password == null || password.Id < 1)
            {
                ajaxResult.Msg = "密码错误";
                return ajaxResult;
            }
            bool validPwd = ValidPwd(password, pwd);
            if (!validPwd)
            {
                //wrong password
                user.FailedLoginAttempts++;
                int allowedAttempts = UtilityHepler.GetAppSettingInt("PasswordAllowedAttempts");
                if (allowedAttempts > 0 && user.FailedLoginAttempts >= allowedAttempts)
                {
                    int lockedMinutes = UtilityHepler.GetAppSettingInt("PasswordLockedMinutes");
                    //lock out
                    user.CannotLoginUntilDateUtc = DateTime.UtcNow.AddMinutes(lockedMinutes);
                    //reset the counter
                    user.FailedLoginAttempts = 0;
                }
                userRep.UpdateUser(user, false);
                //_customerService.UpdateCustomer(customer);

                ajaxResult.Msg = "密码错误";
                return ajaxResult;
            }

            //update login details
            user.FailedLoginAttempts = 0;
            user.CannotLoginUntilDateUtc = null;
            user.RequireReLogin = false;
            user.LastLoginDateUtc = DateTime.UtcNow;

            bool updatedUser = userRep.UpdateUser(user, true);

            if (updatedUser)
            {
                ajaxResult.Type = ResultType.Success;
                ajaxResult.Msg = "登录成功";

                if (remember)
                {
                    string loginCookieKey = WebConst.UserLoginCookieKey;
                    string value = encryService.EncryptText(user.Username);
                    CookieHelper.Add(loginCookieKey, value, DateTimeType.Minute, 30);
                }

                string sessionKey = WebConst.UserLoginSessionKey;
                SessionHelper.Add(sessionKey, user);
            }
            else
            {
                ajaxResult.Msg = "系统错误,暂时无法登录系统";
            }
            return ajaxResult;
        }

        public Customer GetCurrentUser()
        {
            string sessionKey = WebConst.UserLoginSessionKey;
            var user = SessionHelper.Get<Customer>(sessionKey);
            if (user != null) return user;

            string cookieKey = WebConst.UserLoginCookieKey;
            string username = CookieHelper.Get<string>(cookieKey);
            if (!username.IsEmpty())
            {
                username = encryService.DecryptText(username);
                user = userRep.GetUser(username);
                if (user != null)
                {
                    SessionHelper.Add(sessionKey, user);
                }
            }
            return user;
        }

        public bool IsLogin()
        {
            var user = this.GetCurrentUser();
            return user != null && user.Id > 0;
        }

        public AjaxResult Logout()
        {
            try
            {
                string sessionKey = WebConst.UserLoginSessionKey;
                SessionHelper.Remove(sessionKey);

                string cookieKey = WebConst.UserLoginCookieKey;
                CookieHelper.Remove(cookieKey);

                return new AjaxResult(true, "退出成功");
            }
            catch (Exception e)
            {
                return new AjaxResult(false, e.Message);
            }
        }

        private bool ValidPwd(CustomerPassword password, string pwd)
        {
            string savePassword = string.Empty;
            switch (password.PasswordFormatId)
            {
                case 0:
                    savePassword = pwd;
                    break;
                case 1:
                    savePassword = encryService.CreatePasswordHash(pwd, password.PasswordSalt);
                    break;
                case 2:
                    savePassword = encryService.EncryptText(pwd);
                    break;
            }
            return password.Password.Equals(savePassword);
        }
    }
}
