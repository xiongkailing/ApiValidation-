using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TokenManager.Models;
using TokenManager.TokenDAL;

namespace TokenManager.Controllers
{
    public class TokenManageController : Controller
    {
        private TokenDbContext db;
        public TokenManageController()
        {
            db = new TokenDbContext();
        }
        public ActionResult Index()
        {
            var tokens = db.Set<Token>().ToList();
            return View(tokens);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Token entity)
        {
            if (ModelState.IsValid)
            {
                db.Set<Token>().Add(entity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Details(int id = 0)
        {
            var token = db.Set<Token>().Find(id);
            return View(token);
        }
        public ActionResult Edit(int id = 0)
        {
            var token = db.Set<Token>().Find(id);
            return View(token);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Token entity)
        {
            if (ModelState.IsValid)
            {
                db.Entry<Token>(entity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id = 0)
        {
            var token = db.Set<Token>().Find(id);
            db.Set<Token>().Remove(token);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public string CreateSecret()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
        [HttpGet]
        public TokenStatus TokenError(int type)
        {
            TokenStatus ts = new TokenStatus();
            switch (type)
            {
                case 1:
                    {
                        ts.Code = "0000";
                        ts.Message = "缺少TimeStamp参数";
                        break;
                    }
                case 2:
                    {
                        ts.Code = "1000";
                        ts.Message = "时间戳不在合理范围内,请检测您服务器的当前时间";
                        break;
                    }
                case 4:
                    {
                        ts.Code = "2000";
                        ts.Message = "公钥不存在";
                        break;
                    }
                case 8:
                    {

                        ts.Code = "3000";
                        ts.Message = "缺失PublicKey参数";
                        break;
                    }
                case 16:
                    {
                        ts.Code = "5000";
                        ts.Message = "缺失PrivateKey参数";
                        break;
                    }
                default:
                    {
                        ts.Code = "4000";
                        ts.Message = "提供的参数有错误";
                        break;
                    }
            }
            return ts;
        }
    }
}