using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZFC.Shop.Entity;
using ZFC.Shop.Data;
using ZFC.Shop.Utility;

namespace ZFC.Shop.Service
{
    public interface IUserService : IDependency
    {
        Suggestion GetList(int top, string key);

        User GetCurrent();

        User GetUserByWWID(string wwid);

        AjaxResult Login(string eid, string pwd);

        bool IsLogin();

        AjaxResult Logout();

        ApiResult ValidPwd(string wwid, string pwd);
    }

    public class UserService : IUserService
    {
        readonly IUserRepository userRep;
        readonly IAPIService apiService;
        public UserService(IUserRepository ur, IAPIService apis)
        {
            userRep = ur;
            apiService = apis;
        }

        public Suggestion GetList(int top, string key)
        {
            var users = userRep.GetList(top, key);
            if (users != null && users.Count() > 0)
            {
                var tempList = from item in users
                               let name = string.IsNullOrEmpty(item.CN_Name) ? item.EN_Name : item.CN_Name
                               select new
                               {
                                   Email = item.Email,
                                   Phone = item.GetPhone(),
                                   WWID = item.WWID,
                                   Name = name,
                                   DisplayName = item.GetDisplayName(),
                                   Title = item.Title,
                                   Depart = item.Department,
                                   value = string.Format("{0}({1})", name, item.WWID),
                               };

                return new Suggestion(tempList);
            }
            return new Suggestion(null);
        }

        public AjaxResult Login(string eid, string pwd)
        {
            AjaxResult ajaxResult = new AjaxResult(false);
            var result = this.ValidPwd(eid, pwd);
            if (result != null)
            {
                if (result.Flag)
                {
                    string wwid = GetEID(eid);
                    var user = userRep.GetUser(wwid);
                    if (user != null && user.UserID > 0)
                    {
                        ajaxResult.Type = ResultType.Success;
                        ajaxResult.Msg = "登录成功";

                        string loginCookieKey = WebConst.UserLoginCookieKey;
                        CookieHelper.Add(loginCookieKey, user.WWID, DateTimeType.Minute, 30);

                        string sessionKey = WebConst.UserLoginSessionKey;
                        SessionHelper.Add(sessionKey, user);
                    }
                }
            }
            if (ajaxResult.Type == ResultType.Error)
                ajaxResult.Msg = "登录失败,请检查用户名或密码";
            return ajaxResult;
        }

        public ApiResult ValidPwd(string wwid, string pwd)
        {
            return apiService.ValidPwd(wwid, pwd);
        }

        public User GetCurrent()
        {
            string sessionKey = WebConst.UserLoginSessionKey;
            var user = SessionHelper.Get<User>(sessionKey);
            if (user != null) return user;

            string cookieKey = WebConst.UserLoginCookieKey;
            string wwid = CookieHelper.Get<string>(cookieKey);
            if (!wwid.IsEmpty())
            {
                user = userRep.GetUser(wwid);
                if (user != null)
                {
                    SessionHelper.Add(sessionKey, user);
                }
            }
            return user;
        }

        public User GetUserByWWID(string wwid)
        {
            return userRep.GetUserByWWID(wwid);
        }

        public bool IsLogin()
        {
            var user = this.GetCurrent();
            return user != null && user.UserID > 0;
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

        private string GetEID(string eid)
        {
            if (eid.IndexOf(@"\") <= 0)
            {
                bool flag = System.Text.RegularExpressions.Regex.IsMatch(eid, @"^[\d]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (!flag) eid = string.Format(@"AP\{0}", eid);
            }
            return eid;
        }
    }
}
