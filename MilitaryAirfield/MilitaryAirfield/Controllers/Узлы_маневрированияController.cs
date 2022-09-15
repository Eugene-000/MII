using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MilitaryAirfield.Models;

namespace MilitaryAirfield.Controllers
{
    public class Узлы_маневрированияController : Controller
    {
        private MilitaryAirfieldEntities4 db = new MilitaryAirfieldEntities4();

        // GET: Узлы_маневрирования
        
        public async Task<ActionResult> Index()
        {
            return View(await db.Узлы_маневрирования.ToListAsync());
        }

        // GET: Узлы_маневрирования/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Узлы_маневрирования узлы_маневрирования = await db.Узлы_маневрирования.FindAsync(id);
            if (узлы_маневрирования == null)
            {
                return HttpNotFound();
            }
            return View(узлы_маневрирования);
        }

        // GET: Узлы_маневрирования/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Узлы_маневрирования/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_узлов_маневрирования,Размах_крыла_в_м_,Наличие_закрылок,Угол_стреловидности,Скороподъемность_в_м_с_")] Узлы_маневрирования узлы_маневрирования)
        {
            if (ModelState.IsValid)
            {
                db.Узлы_маневрирования.Add(узлы_маневрирования);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(узлы_маневрирования);
        }

        // GET: Узлы_маневрирования/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Узлы_маневрирования узлы_маневрирования = await db.Узлы_маневрирования.FindAsync(id);
            if (узлы_маневрирования == null)
            {
                return HttpNotFound();
            }
            return View(узлы_маневрирования);
        }

        // POST: Узлы_маневрирования/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_узлов_маневрирования,Размах_крыла_в_м_,Наличие_закрылок,Угол_стреловидности,Скороподъемность_в_м_с_")] Узлы_маневрирования узлы_маневрирования)
        {
            if (ModelState.IsValid)
            {
                db.Entry(узлы_маневрирования).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(узлы_маневрирования);
        }

        // GET: Узлы_маневрирования/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Узлы_маневрирования узлы_маневрирования = await db.Узлы_маневрирования.FindAsync(id);
            if (узлы_маневрирования == null)
            {
                return HttpNotFound();
            }
            return View(узлы_маневрирования);
        }

        // POST: Узлы_маневрирования/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Узлы_маневрирования узлы_маневрирования = await db.Узлы_маневрирования.FindAsync(id);
            db.Узлы_маневрирования.Remove(узлы_маневрирования);
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
