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
    public class UTM_TableController : Controller
    {
        private ParticleModel db = new ParticleModel();

        // GET: UTM_Table
        public async Task<ActionResult> Index()
        {
            return View(await db.UTM_Table.ToListAsync());
        }

        // GET: UTM_Table/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UTM_Table uTM_Table = await db.UTM_Table.FindAsync(id);
            if (uTM_Table == null)
            {
                return HttpNotFound();
            }
            return View(uTM_Table);
        }

        // GET: UTM_Table/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UTM_Table/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "GeometriId,UTMX,UTMY,UTMZone")] UTM_Table uTM_Table)
        {
            if (ModelState.IsValid)
            {
                db.UTM_Table.Add(uTM_Table);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(uTM_Table);
        }

        // GET: UTM_Table/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UTM_Table uTM_Table = await db.UTM_Table.FindAsync(id);
            if (uTM_Table == null)
            {
                return HttpNotFound();
            }
            return View(uTM_Table);
        }

        // POST: UTM_Table/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "GeometriId,UTMX,UTMY,UTMZone")] UTM_Table uTM_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uTM_Table).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(uTM_Table);
        }

        // GET: UTM_Table/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UTM_Table uTM_Table = await db.UTM_Table.FindAsync(id);
            if (uTM_Table == null)
            {
                return HttpNotFound();
            }
            return View(uTM_Table);
        }

        // POST: UTM_Table/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UTM_Table uTM_Table = await db.UTM_Table.FindAsync(id);
            db.UTM_Table.Remove(uTM_Table);
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
