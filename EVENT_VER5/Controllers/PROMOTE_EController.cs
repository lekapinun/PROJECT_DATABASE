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
    public class PROMOTE_EController : Controller
    {
        private Entities db = new Entities();

        // GET: PROMOTE_E
        public async Task<ActionResult> Index()
        {
            var pROMOTE_E = db.PROMOTE_E.Include(p => p.MEMBER);
            return View(await pROMOTE_E.ToListAsync());
        }

        // GET: PROMOTE_E/Details/5
        public async Task<ActionResult> Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROMOTE_E pROMOTE_E = await db.PROMOTE_E.FindAsync(id);
            if (pROMOTE_E == null)
            {
                return HttpNotFound();
            }
            return View(pROMOTE_E);
        }

        // GET: PROMOTE_E/Create
        public ActionResult Create()
        {
            ViewBag.MEMBER_ID = new SelectList(db.MEMBER, "MEMBER_ID", "USERNAME");
            return View();
        }

        // POST: PROMOTE_E/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PROMOTE_ID,BUDGETS,END_DATE,MEMBER_ID,TARGET_GENDER,TARGET_MIN_AGE,TARGET_MAX_AGE,TARGET_INTEREST")] PROMOTE_E pROMOTE_E)
        {
            if (ModelState.IsValid)
            {
                db.PROMOTE_E.Add(pROMOTE_E);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MEMBER_ID = new SelectList(db.MEMBER, "MEMBER_ID", "USERNAME", pROMOTE_E.MEMBER_ID);
            return View(pROMOTE_E);
        }

        // GET: PROMOTE_E/Edit/5
        public async Task<ActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROMOTE_E pROMOTE_E = await db.PROMOTE_E.FindAsync(id);
            if (pROMOTE_E == null)
            {
                return HttpNotFound();
            }
            ViewBag.MEMBER_ID = new SelectList(db.MEMBER, "MEMBER_ID", "USERNAME", pROMOTE_E.MEMBER_ID);
            return View(pROMOTE_E);
        }

        // POST: PROMOTE_E/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PROMOTE_ID,BUDGETS,END_DATE,MEMBER_ID,TARGET_GENDER,TARGET_MIN_AGE,TARGET_MAX_AGE,TARGET_INTEREST")] PROMOTE_E pROMOTE_E)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pROMOTE_E).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MEMBER_ID = new SelectList(db.MEMBER, "MEMBER_ID", "USERNAME", pROMOTE_E.MEMBER_ID);
            return View(pROMOTE_E);
        }

        // GET: PROMOTE_E/Delete/5
        public async Task<ActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROMOTE_E pROMOTE_E = await db.PROMOTE_E.FindAsync(id);
            if (pROMOTE_E == null)
            {
                return HttpNotFound();
            }
            return View(pROMOTE_E);
        }

        // POST: PROMOTE_E/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(short id)
        {
            PROMOTE_E pROMOTE_E = await db.PROMOTE_E.FindAsync(id);
            db.PROMOTE_E.Remove(pROMOTE_E);
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
