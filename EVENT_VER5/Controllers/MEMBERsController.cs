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
        public async Task<ActionResult> Create([Bind(Include = "MEMBER_ID,NATIONAL_ID,USERNAME,PASSWORD,FNAME,LNAME,SEX,BIRTH_DATE,ADDRESS,E_MAIL,PHONE,CREDIT_CARD,URL_IMG")] MEMBER mEMBER)
        {
            if (ModelState.IsValid)
            {
                db.MEMBER.Add(mEMBER);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mEMBER);
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
        public async Task<ActionResult> Edit([Bind(Include = "MEMBER_ID,NATIONAL_ID,USERNAME,PASSWORD,FNAME,LNAME,SEX,BIRTH_DATE,ADDRESS,E_MAIL,PHONE,CREDIT_CARD,URL_IMG")] MEMBER mEMBER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mEMBER).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
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
