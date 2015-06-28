using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using TokenManager.Models;
using TokenManager.TokenDAL;

namespace TokenManager.Filters
{
    /// <summary>
    /// 普通Controller使用
    /// </summary>
    public class TokenValidateAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            string publicKey = string.Empty;
            try
            {
                publicKey = filterContext.RequestContext.HttpContext.Request.Headers.Get("PublicKey").Trim();
            }
            catch
            {
                var json = new System.Web.Mvc.JsonResult();
                json.ContentType = "text/HTML";
                json.Data = new TokenStatus { Code = "3000", Message = "缺失PublicKey参数" };
                json.JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet;
                filterContext.Result = json;
                return;
            }
            string privateKey = string.Empty;
            try
            {
                privateKey = filterContext.RequestContext.HttpContext.Request.Headers.Get("PrivateSecret");
            }
            catch
            {
                var json = new System.Web.Mvc.JsonResult();
                json.ContentType = "text/HTML";
                json.Data = new TokenStatus { Code = "5000", Message = "缺失PrivateKey参数" };
                json.JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet;
                filterContext.Result = json;
                return;
            }
            long timeStamp = 0;
            try
            {
                timeStamp = long.Parse(filterContext.RequestContext.HttpContext.Request.Headers.Get("TimeStamp"));
            }
            catch
            {
                var json = new System.Web.Mvc.JsonResult();
                json.ContentType = "text/HTML";
                json.Data = new TokenStatus { Code = "0000", Message = "缺少TimeStamp参数" };
                json.JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet;
                filterContext.Result = json;
                return;
            }
            var error = DateTime.Now.Ticks - timeStamp;
            if (Math.Abs(error) > 180000)
            {
                var json = new System.Web.Mvc.JsonResult();
                json.ContentType = "text/HTML";
                json.Data = new TokenStatus { Code = "1000", Message = "时间戳不在合理范围内,请检测您服务器的当前时间" };
                json.JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet;
                filterContext.Result = json;
                return;
            }
            Token token;
            using (TokenDbContext content = new TokenDbContext())
            {
                token = content.Set<Token>().SingleOrDefault(t => t.OpenToken.ToUpper() == publicKey.ToUpper());
                if (token == null)
                {
                    var json = new System.Web.Mvc.JsonResult();
                    json.ContentType = "text/HTML";
                    json.Data = new TokenStatus { Code = "2000", Message = "公钥不存在" };
                    json.JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet;
                    filterContext.Result = json;
                    return;
                }
                string resoler = token.OpenToken + token.SecretToken + timeStamp;
                string localSecret = MD5Encoding(resoler);
                if (privateKey.Trim().ToUpper() != localSecret.ToUpper())
                {
                    var json = new System.Web.Mvc.JsonResult();
                    json.ContentType = "text/HTML";
                    json.Data = new TokenStatus { Code = "4000", Message = "提供的参数有误" };
                    json.JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet;
                    filterContext.Result = json;
                    return;
                }
            }
            base.OnActionExecuting(filterContext);
        }

        private static string MD5Encoding(string raw)
        {
            MD5 md5 = MD5.Create();
            byte[] bs = Encoding.UTF8.GetBytes(raw);
            byte[] hs = md5.ComputeHash(bs);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hs)
            {
                // 以十六进制格式格式化
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// ApiController使用
    /// </summary>
    public class ApiTokenValidateAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            string publicKey = string.Empty;
            try
            {
                publicKey = actionContext.Request.Headers.GetValues("PublicKey").FirstOrDefault();
            }
            catch
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new TokenStatus { Code = "3000", Message = "缺失PublicKey参数" });
                return;
            }
            string privateKey = string.Empty;
            try
            {
                privateKey = actionContext.Request.Headers.GetValues("PrivateSecret").FirstOrDefault();
            }
            catch
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new TokenStatus { Code = "5000", Message = "缺失PrivateKey参数" });
                return;
            }
            long timeStamp = 0;
            try
            {
                timeStamp = long.Parse(actionContext.Request.Headers.GetValues("TimeStamp").FirstOrDefault());
            }
            catch
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new TokenStatus { Code = "0000", Message = "缺少TimeStamp参数" });
                return;
            }

            var error = DateTime.Now.Ticks - timeStamp;
            if (Math.Abs(error) > 180000)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new TokenStatus { Code = "1000", Message = "时间戳不在合理范围内,请检测您服务器的当前时间" });
                return;
            }
            Token token;
            using (TokenDbContext content = new TokenDbContext())
            {
                token = content.Set<Token>().SingleOrDefault(t => t.OpenToken.ToUpper() == publicKey.ToUpper());
                if (token == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new TokenStatus { Code = "2000", Message = "公钥不存在" });
                    return;
                }
                string resoler = token.OpenToken + token.SecretToken + timeStamp;
                string localSecret = MD5Encoding(resoler);
                if (privateKey.Trim().ToUpper() != localSecret.ToUpper())
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new TokenStatus { Code = "4000", Message = "提供的参数有误" });
                    return;
                }
            }
            base.OnActionExecuting(actionContext);
        }

        private static string MD5Encoding(string raw)
        {
            MD5 md5 = MD5.Create();
            byte[] bs = Encoding.UTF8.GetBytes(raw);
            byte[] hs = md5.ComputeHash(bs);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hs)
            {
                // 以十六进制格式格式化
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}