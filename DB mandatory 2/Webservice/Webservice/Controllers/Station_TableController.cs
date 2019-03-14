using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webservice;
using Webservice.Models;

namespace Webservice.Controllers
{
    public class Station_TableController : Controller
    {
        private ParticleModel db = new ParticleModel();

        // GET: Station_Table
        public async Task<ActionResult> Index()
        {
            return View(await db.Station_Table.ToListAsync());
        }

        // GET: Station_Table/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station_Table station_Table = await db.Station_Table.FindAsync(id);
            if (station_Table == null)
            {
                return HttpNotFound();
            }
            return View(station_Table);
        }

        // GET: Station_Table/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Station_Table/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaaleStedId,Navn,Akronym,UTMX,UTMY,UTMZone")] Station_Table station_Table)
        {
            if (ModelState.IsValid)
            {
                db.Station_Table.Add(station_Table);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(station_Table);
        }

        // GET: Station_Table/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station_Table station_Table = await db.Station_Table.FindAsync(id);
            if (station_Table == null)
            {
                return HttpNotFound();
            }
            return View(station_Table);
        }

        // POST: Station_Table/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaaleStedId,Navn,Akronym,UTMX,UTMY,UTMZone")] Station_Table station_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(station_Table).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(station_Table);
        }

        // GET: Station_Table/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station_Table station_Table = await db.Station_Table.FindAsync(id);
            if (station_Table == null)
            {
                return HttpNotFound();
            }
            return View(station_Table);
        }

        // POST: Station_Table/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Station_Table station_Table = await db.Station_Table.FindAsync(id);
            db.Station_Table.Remove(station_Table);
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
