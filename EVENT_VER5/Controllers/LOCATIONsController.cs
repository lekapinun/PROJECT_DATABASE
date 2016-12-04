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
    public class LOCATIONsController : Controller
    {
        private Entities db = new Entities();

        // GET: LOCATIONs
        public async Task<ActionResult> Index()
        {
            var lOCATIONs = db.LOCATION.Include(l => l.PROMOTE_L);
            return View(await lOCATIONs.ToListAsync());
        }

        // GET: LOCATIONs/Details/5
        public async Task<ActionResult> Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOCATION lOCATION = await db.LOCATION.FindAsync(id);
            if (lOCATION == null)
            {
                return HttpNotFound();
            }
            return View(lOCATION);
        }

        // GET: LOCATIONs/Create
        public ActionResult Create()
        {
            ViewBag.PROMOTE_L_ID = new SelectList(db.PROMOTE_L, "PROMOTE_ID", "PROMOTE_ID");
            return View();
        }

        // POST: LOCATIONs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_LOCATION,LOCATION_NAME,CATEGORY,DETAIL,PICTURE,S_DATE,E_DATE,ADDRESS,FACILITY,PRICE,AREA,PROMOTE_L_ID")] LOCATION lOCATION)
        {
            if (ModelState.IsValid)
            {
                lOCATION.ID_LOCATION = (short)(db.LOCATION.Count() + 1);

                lOCATION.Owner_location = Session["username"].ToString();
                var owner_lo = db.MEMBER.Where(a => a.USERNAME.Equals(lOCATION.Owner_location)).FirstOrDefault();
                lOCATION.MEMBER.Add(owner_lo);

                db.LOCATION.Add(lOCATION);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PROMOTE_L_ID = new SelectList(db.PROMOTE_L, "PROMOTE_ID", "PROMOTE_ID", lOCATION.PROMOTE_L_ID);
            return View(lOCATION);
        }

        // GET: LOCATIONs/Edit/5
        public async Task<ActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOCATION lOCATION = await db.LOCATION.FindAsync(id);
            if (lOCATION == null)
            {
                return HttpNotFound();
            }
            ViewBag.PROMOTE_L_ID = new SelectList(db.PROMOTE_L, "PROMOTE_ID", "PROMOTE_ID", lOCATION.PROMOTE_L_ID);
            return View(lOCATION);
        }

        // POST: LOCATIONs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_LOCATION,LOCATION_NAME,CATEGORY,DETAIL,PICTURE,TIME_START_L,TIME_END_L,ADDRESS,FACILITY,PRICE,AREA,PROMOTE_L_ID")] LOCATION lOCATION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOCATION).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PROMOTE_L_ID = new SelectList(db.PROMOTE_L, "PROMOTE_ID", "PROMOTE_ID", lOCATION.PROMOTE_L_ID);
            return View(lOCATION);
        }

        // GET: LOCATIONs/Delete/5
        public async Task<ActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOCATION lOCATION = await db.LOCATION.FindAsync(id);
            if (lOCATION == null)
            {
                return HttpNotFound();
            }
            return View(lOCATION);
        }

        // POST: LOCATIONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(short id)
        {
            LOCATION lOCATION = await db.LOCATION.FindAsync(id);
            db.LOCATION.Remove(lOCATION);
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
