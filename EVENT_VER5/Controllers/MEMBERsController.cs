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
using EVENT_VER5.ViewModel;

namespace EVENT_VER5.Controllers
{
    public class MEMBERsController : Controller
    {
        private Entities db = new Entities();

        // GET: MEMBERs
        public async Task<ActionResult> Index()
        {
            return View(await db.MEMBER.ToListAsync());
        }

        // GET: MEMBERs/Details/5
        public async Task<ActionResult> Details(short? id)
        {
            MEMBERsDetailsViewModel mem = new MEMBERsDetailsViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mEMBER = await db.MEMBER.FindAsync(id);
            mem.mem_profile = mEMBER;
            //ViewBag.mem = mEMBER;
            if (mEMBER == null)
            {
                return HttpNotFound();
            }
            return View(mem);
            //return View(await db.MEMBER.ToListAsync());
        }

        // GET: MEMBERs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MEMBERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MEMBER_ID,NATIONAL_ID,USERNAME,PASSWORD,FNAME,LNAME,SEX,BIRTH_DATE,ADDRESS,E_MAIL,PHONE,CREDIT_CARD,URL_IMG,RE_ENTER")] MEMBER mEMBER)
        {
            if ( db.MEMBER.Where(a => a.USERNAME.Equals(mEMBER.USERNAME)).Count() != 0)
            {
                Response.Write("<script> alert('This username was used.')</script>");
                return View(mEMBER);
            }

            if (db.MEMBER.Where(a => a.NATIONAL_ID.Equals(mEMBER.NATIONAL_ID)).Count() != 0)
            {
                Response.Write("<script> alert('This national id was used.')</script>");
                return View(mEMBER);
            }

            if (mEMBER.NATIONAL_ID.ToString().Length != 13)
            {
                Response.Write("<script> alert('National ID incorrect.')</script>");
                return View(mEMBER);
            }

            if (mEMBER.PASSWORD != mEMBER.RE_ENTER)
            {
                Response.Write("<script> alert('Password and Re-Enter not match.')</script>");
                return View(mEMBER);
            }
            if (mEMBER.BIRTH_DATE > DateTime.Today)
            {
                Response.Write("<script> alert('Birthday incorrext.')</script>");
                return View(mEMBER);
            }

            if (ModelState.IsValid)
            {
                mEMBER.MEMBER_ID = (short)(db.MEMBER.Count() + 1);

                //string[] date = mEMBER.B_DATE.Split('-');
                //mEMBER.B_DATE = date[1] + '/' + date[0] + '/' + date[2];
                //mEMBER.BIRTH_DATE = Convert.ToDateTime(mEMBER.B_DATE);

                db.MEMBER.Add(mEMBER);

                if (mEMBER.URL_IMG == null)
                {
                    mEMBER.URL_IMG = "http://downloadicons.net/sites/default/files/user-icon-2722.png";
                }

                Session["img"] = mEMBER.URL_IMG;

                db.MEMBER.Add(mEMBER);
                await db.SaveChangesAsync();
                return RedirectToAction("Login","Home");
            }

            return View(mEMBER);
        }

        public async Task<ActionResult> Follow(short id)
        {
            FOLLOWING follow_temp = new FOLLOWING();
            follow_temp.MEMBER_ID = (short)Session["id"];
            follow_temp.FOLLOWING_ID = (short)id;
            follow_temp.HISTORY = DateTime.Today;
            var follow = db.FOLLOWING.Where(m => m.MEMBER_ID.Equals(follow_temp.MEMBER_ID) && m.FOLLOWING_ID.Equals(follow_temp.FOLLOWING_ID)).FirstOrDefault();
            if (follow != null) 
            {
                return RedirectToAction("Details", new { id });
            }
            db.FOLLOWING.Add(follow_temp);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { id });
        }

        // GET: MEMBERs/Edit/5
        public async Task<ActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEMBER mEMBER = await db.MEMBER.FindAsync(id);
            if (mEMBER == null)
            {
                return HttpNotFound();
            }
            return View(mEMBER);
        }

        // POST: MEMBERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MEMBER_ID,NATIONAL_ID,USERNAME,PASSWORD,FNAME,LNAME,SEX,BIRTH_DATE,ADDRESS,E_MAIL,PHONE,CREDIT_CARD,URL_IMG,RE_ENTER")] MEMBER mEMBER)
        {
            if (ModelState.IsValid)
            {
                //string[] date = mEMBER.B_DATE.Split('-');
                //mEMBER.B_DATE = date[1] + '/' + date[0] + '/' + date[2];
                //mEMBER.BIRTH_DATE = Convert.ToDateTime(mEMBER.B_DATE);
                if (mEMBER.BIRTH_DATE > DateTime.Today)
                {
                    Response.Write("<script> alert('Birthday incorrext.')</script>");
                    return View(mEMBER);
                }
                db.Entry(mEMBER).State = EntityState.Modified;
                await db.SaveChangesAsync();
                Session["img"] = mEMBER.URL_IMG;
                return RedirectToAction("Details",new { id = mEMBER.MEMBER_ID});
            }
            return View(mEMBER);
        }

        // GET: MEMBERs/Delete/5
        public async Task<ActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEMBER mEMBER = await db.MEMBER.FindAsync(id);
            if (mEMBER == null)
            {
                return HttpNotFound();
            }
            return View(mEMBER);
        }

        // POST: MEMBERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(short id)
        {
            MEMBER mEMBER = await db.MEMBER.FindAsync(id);
            db.MEMBER.Remove(mEMBER);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
