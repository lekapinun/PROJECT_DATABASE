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
    public class PROMOTE_LController : Controller
    {
        private Entities db = new Entities();

        // GET: PROMOTE_L
        public async Task<ActionResult> Index()
        {
            var pROMOTE_L = db.PROMOTE_L.Include(p => p.MEMBER);
            return View(await pROMOTE_L.ToListAsync());
        }

        // GET: PROMOTE_L/Details/5
        public async Task<ActionResult> Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROMOTE_L pROMOTE_L = await db.PROMOTE_L.FindAsync(id);
            if (pROMOTE_L == null)
            {
                return HttpNotFound();
            }
            return View(pROMOTE_L);
        }

        // GET: PROMOTE_L/Create
        public ActionResult Create()
        {
            ViewBag.MEMBER_ID = new SelectList(db.MEMBER, "MEMBER_ID", "USERNAME");
            return View();
        }

        // POST: PROMOTE_L/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PROMOTE_ID,BUDGETS,END_DATE,MEMBER_ID")] PROMOTE_L pROMOTE_L)
        {
            if (ModelState.IsValid)
            {
                db.PROMOTE_L.Add(pROMOTE_L);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MEMBER_ID = new SelectList(db.MEMBER, "MEMBER_ID", "USERNAME", pROMOTE_L.MEMBER_ID);
            return View(pROMOTE_L);
        }

        // GET: PROMOTE_L/Edit/5
        public async Task<ActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROMOTE_L pROMOTE_L = await db.PROMOTE_L.FindAsync(id);
            if (pROMOTE_L == null)
            {
                return HttpNotFound();
            }
            ViewBag.MEMBER_ID = new SelectList(db.MEMBER, "MEMBER_ID", "USERNAME", pROMOTE_L.MEMBER_ID);
            return View(pROMOTE_L);
        }

        // POST: PROMOTE_L/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PROMOTE_ID,BUDGETS,END_DATE,MEMBER_ID")] PROMOTE_L pROMOTE_L)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pROMOTE_L).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MEMBER_ID = new SelectList(db.MEMBER, "MEMBER_ID", "USERNAME", pROMOTE_L.MEMBER_ID);
            return View(pROMOTE_L);
        }

        // GET: PROMOTE_L/Delete/5
        public async Task<ActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROMOTE_L pROMOTE_L = await db.PROMOTE_L.FindAsync(id);
            if (pROMOTE_L == null)
            {
                return HttpNotFound();
            }
            return View(pROMOTE_L);
        }

        // POST: PROMOTE_L/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(short id)
        {
            PROMOTE_L pROMOTE_L = await db.PROMOTE_L.FindAsync(id);
            db.PROMOTE_L.Remove(pROMOTE_L);
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
