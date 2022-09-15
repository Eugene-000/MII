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
    public class Сотрудник_Воздушное_судноController : Controller
    {
        private MilitaryAirfieldEntities4 db = new MilitaryAirfieldEntities4();

        // GET: Сотрудник_Воздушное_судно
        public async Task<ActionResult> Index()
        {
            var сотрудник_Воздушное_судно = db.Сотрудник_Воздушное_судно.Include(с => с.Воздушное_судно).Include(с => с.Сотрудник_аэродрома);
            return View(await сотрудник_Воздушное_судно.ToListAsync());
        }

        // GET: Сотрудник_Воздушное_судно/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудник_Воздушное_судно сотрудник_Воздушное_судно = await db.Сотрудник_Воздушное_судно.FindAsync(id);
            if (сотрудник_Воздушное_судно == null)
            {
                return HttpNotFound();
            }
            return View(сотрудник_Воздушное_судно);
        }

        // GET: Сотрудник_Воздушное_судно/Create
        public ActionResult Create()
        {
            ViewBag.ID_Воздушного_судна = new SelectList(db.Воздушное_судно, "ID_Воздушного_судна", "Регистрационный_номер_судна");
            ViewBag.ID_Сотрудника = new SelectList(db.Сотрудник_аэродрома, "ID_Сотрудника", "Фамилия");
            return View();
        }

        // POST: Сотрудник_Воздушное_судно/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ID_Сотрудника,ID_Воздушного_судна")] Сотрудник_Воздушное_судно сотрудник_Воздушное_судно)
        {
            //using (MilitaryAirfieldEntities2 db = new MilitaryAirfieldEntities2())
            //{

               // Сотрудник_Воздушное_судно воздушное_судно_id = db.Сотрудник_Воздушное_судно.FirstOrDefault(u => u.ID_Воздушного_судна == сотрудник_Воздушное_судно.ID_Воздушного_судна);
                //Сотрудник_Воздушное_судно сотрудник_id = db.Сотрудник_Воздушное_судно.FirstOrDefault(u => u.ID_Сотрудника == сотрудник_Воздушное_судно.ID_Сотрудника);
                //if (сотрудник_Воздушное_судно.ID_Сотрудника != воздушное_судно_id.ID_Сотрудника && сотрудник_Воздушное_судно.ID_Воздушного_судна != воздушное_судно_id.ID_Воздушного_судна)
                //{

                    if (ModelState.IsValid)
                    {
                        db.Сотрудник_Воздушное_судно.Add(сотрудник_Воздушное_судно);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }

                    ViewBag.ID_Воздушного_судна = new SelectList(db.Воздушное_судно, "ID_Воздушного_судна", "Регистрационный_номер_судна", сотрудник_Воздушное_судно.ID_Воздушного_судна);
                    ViewBag.ID_Сотрудника = new SelectList(db.Сотрудник_аэродрома, "ID_Сотрудника", "Фамилия", сотрудник_Воздушное_судно.ID_Сотрудника);
                    return View(сотрудник_Воздушное_судно);
                //}
                //else {
               //     return View("Error");
                //}
            //}
        }

        // GET: Сотрудник_Воздушное_судно/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудник_Воздушное_судно сотрудник_Воздушное_судно = await db.Сотрудник_Воздушное_судно.FindAsync(id);
            if (сотрудник_Воздушное_судно == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Воздушного_судна = new SelectList(db.Воздушное_судно, "ID_Воздушного_судна", "Регистрационный_номер_судна", сотрудник_Воздушное_судно.ID_Воздушного_судна);
            ViewBag.ID_Сотрудника = new SelectList(db.Сотрудник_аэродрома, "ID_Сотрудника", "Фамилия", сотрудник_Воздушное_судно.ID_Сотрудника);
            return View(сотрудник_Воздушное_судно);
        }

        // POST: Сотрудник_Воздушное_судно/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ID_Сотрудника,ID_Воздушного_судна")] Сотрудник_Воздушное_судно сотрудник_Воздушное_судно)
        {
            if (ModelState.IsValid)
            {
                db.Entry(сотрудник_Воздушное_судно).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Воздушного_судна = new SelectList(db.Воздушное_судно, "ID_Воздушного_судна", "Регистрационный_номер_судна", сотрудник_Воздушное_судно.ID_Воздушного_судна);
            ViewBag.ID_Сотрудника = new SelectList(db.Сотрудник_аэродрома, "ID_Сотрудника", "Фамилия", сотрудник_Воздушное_судно.ID_Сотрудника);
            return View(сотрудник_Воздушное_судно);
        }

        // GET: Сотрудник_Воздушное_судно/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудник_Воздушное_судно сотрудник_Воздушное_судно = await db.Сотрудник_Воздушное_судно.FindAsync(id);
            if (сотрудник_Воздушное_судно == null)
            {
                return HttpNotFound();
            }
            return View(сотрудник_Воздушное_судно);
        }

        // POST: Сотрудник_Воздушное_судно/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Сотрудник_Воздушное_судно сотрудник_Воздушное_судно = await db.Сотрудник_Воздушное_судно.FindAsync(id);
            db.Сотрудник_Воздушное_судно.Remove(сотрудник_Воздушное_судно);
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
