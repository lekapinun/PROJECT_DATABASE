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
    public class EVENTsController : Controller
    {
        private Entities db = new Entities();

        // GET: EVENTs
        public async Task<ActionResult> Index(string category,string searchString)
        {
            var eVENT = db.EVENT.Include(e => e.PROMOTE_E);
            if (!string.IsNullOrEmpty(searchString))
            {
                eVENT = eVENT.Where(a => a.EVENT_NAME.ToLower().Contains(searchString.ToLower()));
            }
            if (!string.IsNullOrEmpty(category) && !category.Equals("All"))
            {
                eVENT = eVENT.Where(a => a.CATEGORY.Equals(category));
            }
            return View(await eVENT.ToListAsync());
        }

        // GET: EVENTs/Details/5
        public async Task<ActionResult> Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENT eVENT = await db.EVENT.FindAsync(id);
            if (eVENT == null)
            {
                return HttpNotFound();
            }
            return View(eVENT);
        }

        // GET: EVENTs/Create
        public ActionResult Create()
        {
            ViewBag.PROMOTE_E_ID = new SelectList(db.PROMOTE_E, "PROMOTE_ID", "TARGET_GENDER");
            return View();
        }

        // POST: EVENTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EVENT_ID,EVENT_NAME,CATEGORY,DETAIL,PICTURE,VIDEO,TIME_START_E,TIME_END_E,CONDITION_MIN_AGE,CONDITION_MAX_AGE,CONDITION_SEX,SOLD_OUT_SEAT,MAX_SEAT,PRICE,PROMOTE_E_ID,LOCATION,S_DATE,E_DATE,S_TIME,E_TIME")] EVENT eVENT)
        {
            if (ModelState.IsValid)
            {
                eVENT.EVENT_ID = (short)(db.EVENT.Count() + 1);

                eVENT.Owner_member = Session["username"].ToString();
                var owner_mem = db.MEMBER.Where(a => a.USERNAME.Equals(eVENT.Owner_member)).FirstOrDefault();
                eVENT.MEMBER1.Add(owner_mem);

                eVENT.TIME_START_E = eVENT.S_DATE.Add(eVENT.S_TIME);
                eVENT.TIME_END_E = eVENT.S_DATE.Add(eVENT.E_TIME);

                eVENT.SOLD_OUT_SEAT = 0;

                db.EVENT.Add(eVENT);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PROMOTE_E_ID = new SelectList(db.PROMOTE_E, "PROMOTE_ID", "TARGET_GENDER", eVENT.PROMOTE_E_ID);
            return View(eVENT);
        }

        // GET: EVENTs/Edit/5
        public async Task<ActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENT eVENT = await db.EVENT.FindAsync(id);
            if (eVENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.PROMOTE_E_ID = new SelectList(db.PROMOTE_E, "PROMOTE_ID", "TARGET_GENDER", eVENT.PROMOTE_E_ID);
            return View(eVENT);
        }

        // POST: EVENTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EVENT_ID,EVENT_NAME,CATEGORY,DETAIL,PICTURE,VIDEO,TIME_START_E,TIME_END_E,CONDITION_MIN_AGE,CONDITION_MAX_AGE,CONDITION_SEX,SOLD_OUT_SEAT,MAX_SEAT,PRICE,PROMOTE_E_ID,LOCATION")] EVENT eVENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eVENT).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PROMOTE_E_ID = new SelectList(db.PROMOTE_E, "PROMOTE_ID", "TARGET_GENDER", eVENT.PROMOTE_E_ID);
            return View(eVENT);
        }

        // GET: EVENTs/Delete/5
        public async Task<ActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENT eVENT = await db.EVENT.FindAsync(id);
            if (eVENT == null)
            {
                return HttpNotFound();
            }
            return View(eVENT);
        }

        // POST: EVENTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(short id)
        {
            EVENT eVENT = await db.EVENT.FindAsync(id);
            db.EVENT.Remove(eVENT);
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
