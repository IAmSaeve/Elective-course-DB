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
    public class Instrument_TableController : Controller
    {
        private ParticleModel db = new ParticleModel();

        // GET: Instrument_Table
        public async Task<ActionResult> Index()
        {
            var instrument_Table = db.Instrument_Table.Include(i => i.Equipment_Table).Include(i => i.Station_Table);
            return View(await instrument_Table.ToListAsync());
        }

        // GET: Instrument_Table/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instrument_Table instrument_Table = await db.Instrument_Table.FindAsync(id);
            if (instrument_Table == null)
            {
                return HttpNotFound();
            }
            return View(instrument_Table);
        }

        // GET: Instrument_Table/Create
        public ActionResult Create()
        {
            ViewBag.UdstyrId = new SelectList(db.Equipment_Table, "UdstyrId", "Navn");
            ViewBag.MaalestedId = new SelectList(db.Station_Table, "MaaleStedId", "Navn");
            return View();
        }

        // POST: Instrument_Table/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OpstillingId,kode,MaalestedId,UdstyrId")] Instrument_Table instrument_Table)
        {
            if (ModelState.IsValid)
            {
                db.Instrument_Table.Add(instrument_Table);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UdstyrId = new SelectList(db.Equipment_Table, "UdstyrId", "Navn", instrument_Table.UdstyrId);
            ViewBag.MaalestedId = new SelectList(db.Station_Table, "MaaleStedId", "Navn", instrument_Table.MaalestedId);
            return View(instrument_Table);
        }

        // GET: Instrument_Table/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instrument_Table instrument_Table = await db.Instrument_Table.FindAsync(id);
            if (instrument_Table == null)
            {
                return HttpNotFound();
            }
            ViewBag.UdstyrId = new SelectList(db.Equipment_Table, "UdstyrId", "Navn", instrument_Table.UdstyrId);
            ViewBag.MaalestedId = new SelectList(db.Station_Table, "MaaleStedId", "Navn", instrument_Table.MaalestedId);
            return View(instrument_Table);
        }

        // POST: Instrument_Table/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OpstillingId,kode,MaalestedId,UdstyrId")] Instrument_Table instrument_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instrument_Table).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UdstyrId = new SelectList(db.Equipment_Table, "UdstyrId", "Navn", instrument_Table.UdstyrId);
            ViewBag.MaalestedId = new SelectList(db.Station_Table, "MaaleStedId", "Navn", instrument_Table.MaalestedId);
            return View(instrument_Table);
        }

        // GET: Instrument_Table/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instrument_Table instrument_Table = await db.Instrument_Table.FindAsync(id);
            if (instrument_Table == null)
            {
                return HttpNotFound();
            }
            return View(instrument_Table);
        }

        // POST: Instrument_Table/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Instrument_Table instrument_Table = await db.Instrument_Table.FindAsync(id);
            db.Instrument_Table.Remove(instrument_Table);
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
