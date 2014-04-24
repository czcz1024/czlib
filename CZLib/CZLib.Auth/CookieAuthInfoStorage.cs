namespace CZLib.Auth
{
    using System;
    using System.Web;

    /// <summary>
    /// 基于Cookie的身份保存
    /// </summary>
    public class CookieAuthInfoStorage : IAuthInfoStorage
    {
        public CookieAuthInfoStorage()
        {
            this.path = "/";
            this.domain = "";
            this.expire = null;
            this.httpOnly = false;
            this.secure = false;
        }

        //public CookieAuthInfoStorage(string path, string domain, DateTime? expire, bool httpOnly, bool secure)
        //{
        //    this.path = path;
        //    this.domain = domain;
        //    this.expire = expire;
        //    this.httpOnly = httpOnly;
        //    this.secure = secure;
        //}

        protected string path;
        protected string domain;
        protected DateTime? expire;
        protected bool httpOnly;
        protected bool secure;

        public virtual void Save(string name, string value)
        {
            var cookie = new HttpCookie(name, value);
            cookie.Path = this.path;
            cookie.Domain = this.domain;
            cookie.HttpOnly = this.httpOnly;
            cookie.Secure = this.secure;
            if (this.expire.HasValue)
            {
                cookie.Expires = this.expire.Value;
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public virtual string Load(string name)
        {
            var cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value)) return string.Empty;
            return cookie.Value;
        }

        public virtual void Clear(string name)
        {
            var cookie = new HttpCookie(name, string.Empty);
            cookie.Value = string.Empty;
            cookie.Path = this.path;
            cookie.Domain = this.domain;
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);
            //HttpContext.Current.Response.Cookies.Remove(name);
        }
    }
}