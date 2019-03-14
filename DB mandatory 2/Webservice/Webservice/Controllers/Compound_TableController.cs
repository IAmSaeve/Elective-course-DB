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
    public class Compound_TableController : Controller
    {
        private ParticleModel db = new ParticleModel();

        // GET: Compound_Table
        public async Task<ActionResult> Index()
        {
            return View(await db.Compound_Table.ToListAsync());
        }

        // GET: Compound_Table/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compound_Table compound_Table = await db.Compound_Table.FindAsync(id);
            if (compound_Table == null)
            {
                return HttpNotFound();
            }
            return View(compound_Table);
        }

        // GET: Compound_Table/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Compound_Table/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StofId,StofNavn")] Compound_Table compound_Table)
        {
            if (ModelState.IsValid)
            {
                db.Compound_Table.Add(compound_Table);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(compound_Table);
        }

        // GET: Compound_Table/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compound_Table compound_Table = await db.Compound_Table.FindAsync(id);
            if (compound_Table == null)
            {
                return HttpNotFound();
            }
            return View(compound_Table);
        }

        // POST: Compound_Table/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StofId,StofNavn")] Compound_Table compound_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compound_Table).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(compound_Table);
        }

        // GET: Compound_Table/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compound_Table compound_Table = await db.Compound_Table.FindAsync(id);
            if (compound_Table == null)
            {
                return HttpNotFound();
            }
            return View(compound_Table);
        }

        // POST: Compound_Table/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Compound_Table compound_Table = await db.Compound_Table.FindAsync(id);
            db.Compound_Table.Remove(compound_Table);
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
