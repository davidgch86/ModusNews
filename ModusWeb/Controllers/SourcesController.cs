using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModusWeb.DataAccess;
using System.Reflection;
using System.Xml.Linq;

namespace ModusWeb.Controllers
{
    public class SourcesController : Controller
    {
        private LocalDBEntities db = new LocalDBEntities();

        // GET: Sources
        public async Task<ActionResult> Index()
        {
            return View(await db.Sources.ToListAsync());
        }

        // GET: Sources/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = await db.Sources.FindAsync(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            return View(source);
        }

        // GET: Sources/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SourceId,Url,Description")] Source source)
        {
            if (!string.IsNullOrEmpty(source.Url))
            {
                try
                {
                    WebClient wclient = new WebClient();
                    string newsData = wclient.DownloadString(source.Url);

                    XDocument xml = XDocument.Parse(newsData);
                    if (ModelState.IsValid)
                    {
                        db.Sources.Add(source);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
                catch
                {

                }
            }


            return View(source);
        }

        // GET: Sources/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = await db.Sources.FindAsync(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            return View(source);
        }

        // POST: Sources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SourceId,Url,Description")] Source source)
        {
            if (!string.IsNullOrEmpty(source.Url))
            {
                try
                {
                    WebClient wclient = new WebClient();
                    string newsData = wclient.DownloadString(source.Url);

                    XDocument xml = XDocument.Parse(newsData);
                    if (ModelState.IsValid)
                    {
                        db.Entry(source).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
                catch
                {

                }
            }
            return View(source);
        }

        // GET: Sources/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = await db.Sources.FindAsync(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            return View(source);
        }

        // POST: Sources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Source source = await db.Sources.FindAsync(id);
            db.Sources.Remove(source);
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
