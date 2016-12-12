using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EVENT_VER5.Models;

namespace EVENT_VER5.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(login lg)
        {
            if (ModelState.IsValid)
            {
                using (Entities ue = new Entities())
                {
                    var log = ue.MEMBER.Where(a => a.USERNAME.Equals(lg.username) && a.PASSWORD.Equals(lg.password)).FirstOrDefault();
                    if (log != null)
                    {
                        Session["username"] = log.USERNAME;
                        Session["img"] = log.URL_IMG;
                        Session["id"] = log.MEMBER_ID;
                        return RedirectToAction("Details", "MEMBERs", new { id=log.MEMBER_ID });
                    }
                    else
                    {
                        Response.Write("<script> alert('Invalid password')</script>");
                    }
                }
            }
            return View();
        }

        public async Task<ActionResult> UsersHome(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mEMBER = await db.MEMBER.FindAsync(id);
            ViewBag.mem = mEMBER;
            if (mEMBER == null)
            {
                return HttpNotFound();
            }
            return View(await db.EVENT.ToListAsync());
            //return View(mEMBER);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}