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
    public class Units_TableController : Controller
    {
        private ParticleModel db = new ParticleModel();

        // GET: Units_Table
        public async Task<ActionResult> Index()
        {
            return View(await db.Units_Table.ToListAsync());
        }

        // GET: Units_Table/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Units_Table units_Table = await db.Units_Table.FindAsync(id);
            if (units_Table == null)
            {
                return HttpNotFound();
            }
            return View(units_Table);
        }

        // GET: Units_Table/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Units_Table/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EnhedId,Navn")] Units_Table units_Table)
        {
            if (ModelState.IsValid)
            {
                db.Units_Table.Add(units_Table);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(units_Table);
        }

        // GET: Units_Table/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Units_Table units_Table = await db.Units_Table.FindAsync(id);
            if (units_Table == null)
            {
                return HttpNotFound();
            }
            return View(units_Table);
        }

        // POST: Units_Table/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EnhedId,Navn")] Units_Table units_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(units_Table).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(units_Table);
        }

        // GET: Units_Table/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Units_Table units_Table = await db.Units_Table.FindAsync(id);
            if (units_Table == null)
            {
                return HttpNotFound();
            }
            return View(units_Table);
        }

        // POST: Units_Table/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Units_Table units_Table = await db.Units_Table.FindAsync(id);
            db.Units_Table.Remove(units_Table);
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
