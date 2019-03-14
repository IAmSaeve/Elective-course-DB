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
    public class Equipment_TableController : Controller
    {
        private ParticleModel db = new ParticleModel();

        // GET: Equipment_Table
        public async Task<ActionResult> Index()
        {
            return View(await db.Equipment_Table.ToListAsync());
        }

        // GET: Equipment_Table/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment_Table equipment_Table = await db.Equipment_Table.FindAsync(id);
            if (equipment_Table == null)
            {
                return HttpNotFound();
            }
            return View(equipment_Table);
        }

        // GET: Equipment_Table/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equipment_Table/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UdstyrId,Navn")] Equipment_Table equipment_Table)
        {
            if (ModelState.IsValid)
            {
                db.Equipment_Table.Add(equipment_Table);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(equipment_Table);
        }

        // GET: Equipment_Table/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment_Table equipment_Table = await db.Equipment_Table.FindAsync(id);
            if (equipment_Table == null)
            {
                return HttpNotFound();
            }
            return View(equipment_Table);
        }

        // POST: Equipment_Table/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UdstyrId,Navn")] Equipment_Table equipment_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipment_Table).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(equipment_Table);
        }

        // GET: Equipment_Table/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment_Table equipment_Table = await db.Equipment_Table.FindAsync(id);
            if (equipment_Table == null)
            {
                return HttpNotFound();
            }
            return View(equipment_Table);
        }

        // POST: Equipment_Table/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Equipment_Table equipment_Table = await db.Equipment_Table.FindAsync(id);
            db.Equipment_Table.Remove(equipment_Table);
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
