using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Webservice;
using Webservice.Models;

namespace Webservice.Controllers
{
    public class Measurement_TableController : Controller
    {
        private ParticleModel db = new ParticleModel();

        // GET: Measurement_Table
        public async Task<ActionResult> Index()
        {
            var measurement_Table = db.Measurement_Table.Include(m => m.Compound_Table).Include(m => m.Station_Table).Include(m => m.Units_Table).Include(m => m.UTM_Table);
            return View(await measurement_Table.ToListAsync());
        }

        // GET: Measurement_Table/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Measurement_Table measurement_Table = await db.Measurement_Table.FindAsync(id);
            if (measurement_Table == null)
            {
                return HttpNotFound();
            }
            return View(measurement_Table);
        }

        // GET: Measurement_Table/Create
        public ActionResult Create()
        {
            ViewBag.StofId = new SelectList(db.Compound_Table, "StofId", "StofNavn");
            ViewBag.MaaleStedId = new SelectList(db.Station_Table, "MaaleStedId", "Navn");
            ViewBag.EnhedId = new SelectList(db.Units_Table, "EnhedId", "Navn");
            ViewBag.GeometriId = new SelectList(db.UTM_Table, "GeometriId", "GeometriId");
            return View();
        }

        // POST: Measurement_Table/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,datoMaerke,MaaleStedId,GeometriId,Resultat,EnhedId,StofId")] Measurement_Table measurement_Table)
        {
            if (ModelState.IsValid)
            {
                db.Measurement_Table.Add(measurement_Table);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StofId = new SelectList(db.Compound_Table, "StofId", "StofNavn", measurement_Table.StofId);
            ViewBag.MaaleStedId = new SelectList(db.Station_Table, "MaaleStedId", "Navn", measurement_Table.MaaleStedId);
            ViewBag.EnhedId = new SelectList(db.Units_Table, "EnhedId", "Navn", measurement_Table.EnhedId);
            ViewBag.GeometriId = new SelectList(db.UTM_Table, "GeometriId", "GeometriId", measurement_Table.GeometriId);
            return View(measurement_Table);
        }

        // GET: Measurement_Table/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Measurement_Table measurement_Table = await db.Measurement_Table.FindAsync(id);
            if (measurement_Table == null)
            {
                return HttpNotFound();
            }
            ViewBag.StofId = new SelectList(db.Compound_Table, "StofId", "StofNavn", measurement_Table.StofId);
            ViewBag.MaaleStedId = new SelectList(db.Station_Table, "MaaleStedId", "Navn", measurement_Table.MaaleStedId);
            ViewBag.EnhedId = new SelectList(db.Units_Table, "EnhedId", "Navn", measurement_Table.EnhedId);
            ViewBag.GeometriId = new SelectList(db.UTM_Table, "GeometriId", "GeometriId", measurement_Table.GeometriId);
            return View(measurement_Table);
        }

        // POST: Measurement_Table/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,datoMaerke,MaaleStedId,GeometriId,Resultat,EnhedId,StofId")] Measurement_Table measurement_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(measurement_Table).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.StofId = new SelectList(db.Compound_Table, "StofId", "StofNavn", measurement_Table.StofId);
            ViewBag.MaaleStedId = new SelectList(db.Station_Table, "MaaleStedId", "Navn", measurement_Table.MaaleStedId);
            ViewBag.EnhedId = new SelectList(db.Units_Table, "EnhedId", "Navn", measurement_Table.EnhedId);
            ViewBag.GeometriId = new SelectList(db.UTM_Table, "GeometriId", "GeometriId", measurement_Table.GeometriId);
            return View(measurement_Table);
        }

        // GET: Measurement_Table/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Measurement_Table measurement_Table = await db.Measurement_Table.FindAsync(id);
            if (measurement_Table == null)
            {
                return HttpNotFound();
            }
            return View(measurement_Table);
        }

        // POST: Measurement_Table/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Measurement_Table measurement_Table = await db.Measurement_Table.FindAsync(id);
            db.Measurement_Table.Remove(measurement_Table);
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
