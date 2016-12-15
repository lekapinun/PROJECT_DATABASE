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
using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;

namespace EVENT_VER5.Controllers
{
    public class EVENTsController : Controller
    {
        private Entities db = new Entities();

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

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
            eVENT = eVENT.OrderBy(t => t.TIME_START_E);
            return View(await eVENT.ToListAsync());
        }

        public async Task<ActionResult> Index2(short id, string category, string searchString)
        {
            var eVENT = db.EVENT.Include(e => e.PROMOTE_E);
            eVENT = eVENT.Where(a => a.MEMBER1.FirstOrDefault().MEMBER_ID.Equals(id));
            if (!string.IsNullOrEmpty(searchString))
            {
                eVENT = eVENT.Where(a => a.EVENT_NAME.ToLower().Contains(searchString.ToLower()));
            }
            if (!string.IsNullOrEmpty(category) && !category.Equals("All"))
            {
                eVENT = eVENT.Where(a => a.CATEGORY.Equals(category));
            }
            eVENT = eVENT.OrderBy(t => t.TIME_START_E);
            return View(await eVENT.ToListAsync());
        }

        // GET: EVENTs/Details/5
        public async Task<ActionResult> Details(short? id, short id_mem)
        {
            EVENTsViewModel eVENT = new EVENTsViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eVENT.event_detail = await db.EVENT.FindAsync(id);
            eVENT.mem = await db.MEMBER.FindAsync(id_mem);
            int i = 0;
            if (db.PROMOTE_E.Count() < 4)
            {
                i = db.PROMOTE_E.Count();
            }
            else
            {
                i = 4;
            }
            DateTime today = DateTime.Today;
            DateTime b_day = Convert.ToDateTime(eVENT.mem.BIRTH_DATE.ToString());
            int age = today.Year - b_day.Year;
            if (b_day > today.AddYears(-age)) age--;
            eVENT.event_for_promote = db.PROMOTE_E.Where(a => a.END_DATE > DateTime.Today && a.TARGET_MIN_AGE < age && a.TARGET_MAX_AGE > age ).OrderBy(x => Guid.NewGuid()).Take(i).ToList(); ;
            if (eVENT == null)
            {
                return HttpNotFound();
            }
            return View(eVENT);
        }


        public async Task<ActionResult> Join(short id)
        {
            EVENT eVENT = await db.EVENT.FindAsync(id);
            MEMBER mem = await db.MEMBER.FindAsync(Session["id"]);
            DateTime today = DateTime.Today;
            DateTime b_day = Convert.ToDateTime(mem.BIRTH_DATE.ToString());
            int age = today.Year - b_day.Year;
            if (b_day > today.AddYears(-age)) age--;
            if (age < eVENT.CONDITION_MIN_AGE || age > eVENT.CONDITION_MAX_AGE)
            {
                Response.Write("<script> alert('You not suitable.')</script>");
                return RedirectToAction("Details", new { id = eVENT.EVENT_ID });
            }
            if (mem.SEX != eVENT.CONDITION_SEX && eVENT.CONDITION_SEX != "None")
            {
                Response.Write("<script> alert('You not suitable.')</script>");
                return RedirectToAction("Details", new { id = eVENT.EVENT_ID });
            }
            //db.Entry(eVENT).State = EntityState.Modified;
            

            UserCredential credential;
            using (var stream = new FileStream("../../Users/LekApinun/Documents/Visual Studio 2015/Projects/EVENT_VER5/PROJECT_DATABASE/EVENT_VER5/client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(credPath, true)).Result;
                //Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            string start = eVENT.TIME_START_E.ToString();
            DateTime startT = Convert.ToDateTime(start);
            start = startT.Year.ToString() + '-' + startT.Month.ToString() + '-' + startT.Day.ToString() + 'T' + startT.TimeOfDay + "+07:00" ;

            string end = eVENT.TIME_END_E.ToString();
            DateTime endT = Convert.ToDateTime(end);
            end = endT.Year.ToString() + '-' + endT.Month.ToString() + '-' + endT.Day.ToString() + 'T' + endT.TimeOfDay + "+07:00";


            Event newEvent = new Event()
            {
                Summary = eVENT.EVENT_NAME.ToString(),
                Location = eVENT.LOCATION.ToString(),
                Description = eVENT.DETAIL.ToString(),
                Start = new EventDateTime()
                {
                    DateTime = DateTime.Parse(start),
                    TimeZone = "Asia/Bangkok",
                },
                End = new EventDateTime()
                {
                    DateTime = DateTime.Parse(end),
                    TimeZone = "Asia/Bangkok",
                },
                Attendees = new EventAttendee[] 
                {
                    new EventAttendee()
                    {
                        Email = mem.E_MAIL.ToString() ,
                        ResponseStatus = "needsAction"
                    }
                }
            };

            String calendarId = "primary";
            EventsResource.InsertRequest request = service.Events.Insert(newEvent, calendarId);
            Event createdEvent = request.Execute();
            //Console.WriteLine("Event created: {0}", createdEvent.HtmlLink);

            eVENT.MEMBER.Add(mem);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { id = eVENT.EVENT_ID });


        }

        [HttpPost, ActionName("Details")]
        public async Task<ActionResult> JoinAndPromote(short id, string user_name, EVENTsViewModel promote_event)
        {
            if (user_name != null)
            {
                EVENT eVENT = await db.EVENT.FindAsync(id);
                MEMBER mem = db.MEMBER.Where(u => u.USERNAME.Equals(user_name)).FirstOrDefault();
                eVENT.MEMBER.Add(mem);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = eVENT.EVENT_ID });
            }
            else
            {
                EVENT eVENT = await db.EVENT.FindAsync(id);
                MEMBER mEMBER = await db.MEMBER.FindAsync(Session["id"]);
                promote_event.event_promote.PROMOTE_ID = (short)(db.PROMOTE_E.Count()+1);
                promote_event.event_promote.END_DATE = DateTime.Today.AddDays(promote_event.day_of_promote);
                promote_event.event_promote.BUDGETS = promote_event.day_of_promote;
                promote_event.event_promote.MEMBER = mEMBER;
                promote_event.event_promote.EVENT.Add(eVENT);
                db.PROMOTE_E.Add(promote_event.event_promote);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = eVENT.EVENT_ID });
            }

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
                return RedirectToAction("Details", new { id = eVENT.EVENT_ID, id_mem = eVENT.MEMBER1.FirstOrDefault().MEMBER_ID});
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
        public async Task<ActionResult> Edit([Bind(Include = "EVENT_ID,EVENT_NAME,CATEGORY,DETAIL,PICTURE,VIDEO,TIME_START_E,TIME_END_E,CONDITION_MIN_AGE,CONDITION_MAX_AGE,CONDITION_SEX,SOLD_OUT_SEAT,MAX_SEAT,PRICE,LOCATION,PROMOTE_E")] EVENT eVENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eVENT).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = eVENT.EVENT_ID , id_mem = eVENT.MEMBER1.FirstOrDefault().MEMBER_ID });
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
