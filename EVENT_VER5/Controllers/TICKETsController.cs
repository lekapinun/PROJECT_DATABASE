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
    public class TICKETsController : Controller
    {
        private Entities db = new Entities();

        // GET: TICKETs
        public async Task<ActionResult> Index()
        {
            var tICKET = db.TICKET.Include(t => t.EVENT).Include(t => t.MEMBER);
            return View(await tICKET.ToListAsync());
        }

        // GET: TICKETs/Details/5
        public async Task<ActionResult> Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TICKET tICKET = await db.TICKET.FindAsync(id);
            if (tICKET == null)
            {
                return HttpNotFound();
            }
            return View(tICKET);
        }

        // GET: TICKETs/Create
        public ActionResult Create()
        {
            ViewBag.EVENT_ID = new SelectList(db.EVENT, "EVENT_ID", "EVENT_NAME");
            ViewBag.MEMBER_ID = new SelectList(db.MEMBER, "MEMBER_ID", "USERNAME");
            return View();
        }

        // POST: TICKETs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_TICKET,AMOUNT,MEMBER_ID,EVENT_ID")] TICKET tICKET)
        {
            if (ModelState.IsValid)
            {
                db.TICKET.Add(tICKET);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EVENT_ID = new SelectList(db.EVENT, "EVENT_ID", "EVENT_NAME", tICKET.EVENT_ID);
            ViewBag.MEMBER_ID = new SelectList(db.MEMBER, "MEMBER_ID", "USERNAME", tICKET.MEMBER_ID);
            return View(tICKET);
        }

        // GET: TICKETs/Edit/5
        public async Task<ActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TICKET tICKET = await db.TICKET.FindAsync(id);
            if (tICKET == null)
            {
                return HttpNotFound();
            }
            ViewBag.EVENT_ID = new SelectList(db.EVENT, "EVENT_ID", "EVENT_NAME", tICKET.EVENT_ID);
            ViewBag.MEMBER_ID = new SelectList(db.MEMBER, "MEMBER_ID", "USERNAME", tICKET.MEMBER_ID);
            return View(tICKET);
        }

        // POST: TICKETs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_TICKET,AMOUNT,MEMBER_ID,EVENT_ID")] TICKET tICKET)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tICKET).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EVENT_ID = new SelectList(db.EVENT, "EVENT_ID", "EVENT_NAME", tICKET.EVENT_ID);
            ViewBag.MEMBER_ID = new SelectList(db.MEMBER, "MEMBER_ID", "USERNAME", tICKET.MEMBER_ID);
            return View(tICKET);
        }

        // GET: TICKETs/Delete/5
        public async Task<ActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TICKET tICKET = await db.TICKET.FindAsync(id);
            if (tICKET == null)
            {
                return HttpNotFound();
            }
            return View(tICKET);
        }

        // POST: TICKETs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(short id)
        {
            TICKET tICKET = await db.TICKET.FindAsync(id);
            db.TICKET.Remove(tICKET);
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
