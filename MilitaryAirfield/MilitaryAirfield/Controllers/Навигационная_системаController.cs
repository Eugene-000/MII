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
    public class Навигационная_системаController : Controller
    {
        private MilitaryAirfieldEntities4 db = new MilitaryAirfieldEntities4();

        // GET: Навигационная_система
        
        public async Task<ActionResult> Index()
        {
            return View(await db.Навигационная_система.ToListAsync());
        }

        // GET: Навигационная_система/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Навигационная_система навигационная_система = await db.Навигационная_система.FindAsync(id);
            if (навигационная_система == null)
            {
                return HttpNotFound();
            }
            return View(навигационная_система);
        }

        // GET: Навигационная_система/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Навигационная_система/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_навигационной_системы,Средняя_дальность_обнаружения_воздушной_цели,Количество_одновременно_сопровождаемых_целей,Количество_одновременно_атакуемых_целей")] Навигационная_система навигационная_система)
        {
            if (ModelState.IsValid)
            {
                db.Навигационная_система.Add(навигационная_система);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(навигационная_система);
        }

        // GET: Навигационная_система/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Навигационная_система навигационная_система = await db.Навигационная_система.FindAsync(id);
            if (навигационная_система == null)
            {
                return HttpNotFound();
            }
            return View(навигационная_система);
        }

        // POST: Навигационная_система/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_навигационной_системы,Средняя_дальность_обнаружения_воздушной_цели,Количество_одновременно_сопровождаемых_целей,Количество_одновременно_атакуемых_целей")] Навигационная_система навигационная_система)
        {
            if (ModelState.IsValid)
            {
                db.Entry(навигационная_система).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(навигационная_система);
        }

        // GET: Навигационная_система/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Навигационная_система навигационная_система = await db.Навигационная_система.FindAsync(id);
            if (навигационная_система == null)
            {
                return HttpNotFound();
            }
            return View(навигационная_система);
        }

        // POST: Навигационная_система/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Навигационная_система навигационная_система = await db.Навигационная_система.FindAsync(id);
            db.Навигационная_система.Remove(навигационная_система);
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
