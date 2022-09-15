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
    public class Сотрудник_аэродромаController : Controller
    {
        private MilitaryAirfieldEntities4 db = new MilitaryAirfieldEntities4();

        // GET: Сотрудник_аэродрома
        
        public async Task<ActionResult> Index()
        {
            return View(await db.Сотрудник_аэродрома.ToListAsync());
        }

        
        [HttpPost]
        public ActionResult Index(string LastName, string submit)
        {
            var Сотрудник_аэродрома = db.Сотрудник_аэродрома.AsEnumerable();

            switch (submit)
            {
                case "Найти":
                    if (!String.IsNullOrEmpty(LastName))
                    {
                        Сотрудник_аэродрома = db.Сотрудник_аэродрома.Where(n => n.Фамилия.Contains(LastName));
                    }
                    break;
                default:
                    break;
            }
            return View(Сотрудник_аэродрома.ToList());
        }

        // GET: Сотрудник_аэродрома/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудник_аэродрома сотрудник_аэродрома = await db.Сотрудник_аэродрома.FindAsync(id);
            if (сотрудник_аэродрома == null)
            {
                return HttpNotFound();
            }
            return View(сотрудник_аэродрома);
        }

        // GET: Сотрудник_аэродрома/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Сотрудник_аэродрома/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_Сотрудника,Фамилия,Имя,Отчество,Пол,Дата_рождения,Телефон,Военное_звание,Должность,Место_работы")] Сотрудник_аэродрома сотрудник_аэродрома)
        {
            if (ModelState.IsValid)
            {
                db.Сотрудник_аэродрома.Add(сотрудник_аэродрома);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(сотрудник_аэродрома);
        }

        // GET: Сотрудник_аэродрома/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудник_аэродрома сотрудник_аэродрома = await db.Сотрудник_аэродрома.FindAsync(id);
            if (сотрудник_аэродрома == null)
            {
                return HttpNotFound();
            }
            return View(сотрудник_аэродрома);
        }

        // POST: Сотрудник_аэродрома/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_Сотрудника,Фамилия,Имя,Отчество,Пол,Дата_рождения,Телефон,Военное_звание,Должность,Место_работы")] Сотрудник_аэродрома сотрудник_аэродрома)
        {
            if (ModelState.IsValid)
            {
                db.Entry(сотрудник_аэродрома).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(сотрудник_аэродрома);
        }

        // GET: Сотрудник_аэродрома/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудник_аэродрома сотрудник_аэродрома = await db.Сотрудник_аэродрома.FindAsync(id);
            if (сотрудник_аэродрома == null)
            {
                return HttpNotFound();
            }
            return View(сотрудник_аэродрома);
        }

        // POST: Сотрудник_аэродрома/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Сотрудник_аэродрома сотрудник_аэродрома = await db.Сотрудник_аэродрома.FindAsync(id);
                db.Сотрудник_аэродрома.Remove(сотрудник_аэродрома);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Для удаления сначала удалите запись из таблицы Связь сотрудника с воздушным судном!";
                return View("Error");
            }
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
