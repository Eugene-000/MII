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
    public class Используемая_экипировкаController : Controller
    {
        private MilitaryAirfieldEntities4 db = new MilitaryAirfieldEntities4();

        // GET: Используемая_экипировка
        
        public async Task<ActionResult> Index()
        {
            var используемая_экипировка = db.Используемая_экипировка.Include(и => и.Сотрудник_аэродрома);
            return View(await используемая_экипировка.ToListAsync());
        }
        
        [HttpPost]
        public ActionResult Index(string tip, string submit)
        {
            var Используемая_экипировка = db.Используемая_экипировка.AsEnumerable();
            switch (submit)
            {
                case "Найти":
                    if (!String.IsNullOrEmpty(tip))
                    {
                        Используемая_экипировка = db.Используемая_экипировка.Where(n => n.Тип_экипировки.Contains(tip));
                    }
                    break;
                default:
                    break;
            }
            
            return View(Используемая_экипировка.ToList());
        }

        // GET: Используемая_экипировка/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Используемая_экипировка используемая_экипировка = await db.Используемая_экипировка.FindAsync(id);
            if (используемая_экипировка == null)
            {
                return HttpNotFound();
            }
            return View(используемая_экипировка);
        }

        // GET: Используемая_экипировка/Create
        public ActionResult Create()
        {
            ViewBag.ID_Сотрудника = new SelectList(db.Сотрудник_аэродрома, "ID_Сотрудника", "Фамилия");
            return View();
        }

        // POST: Используемая_экипировка/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_экипировки,ID_Сотрудника,Защитный_костюм,Наличие_авиационного_спасательного_пояса,Наплечные_знаки_различия_или_погоны,Тип_экипировки")] Используемая_экипировка используемая_экипировка)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Используемая_экипировка.Add(используемая_экипировка);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.ID_Сотрудника = new SelectList(db.Сотрудник_аэродрома, "ID_Сотрудника", "Фамилия", используемая_экипировка.ID_Сотрудника);
                return View(используемая_экипировка);
            }
            catch
            {
                ViewBag.Message = "У данного сотрудника уже имеется экипировка,для того чтобы добавить запись, добавьте сотрудника в таблице Сотрудник аэродрома!";
                return View("Error");
            }
        }

        // GET: Используемая_экипировка/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Используемая_экипировка используемая_экипировка = await db.Используемая_экипировка.FindAsync(id);
            if (используемая_экипировка == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Сотрудника = new SelectList(db.Сотрудник_аэродрома, "ID_Сотрудника", "Фамилия", используемая_экипировка.ID_Сотрудника);
            return View(используемая_экипировка);
        }

        // POST: Используемая_экипировка/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_экипировки,ID_Сотрудника,Защитный_костюм,Наличие_авиационного_спасательного_пояса,Наплечные_знаки_различия_или_погоны,Тип_экипировки")] Используемая_экипировка используемая_экипировка)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(используемая_экипировка).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.ID_Сотрудника = new SelectList(db.Сотрудник_аэродрома, "ID_Сотрудника", "Фамилия", используемая_экипировка.ID_Сотрудника);
                return View(используемая_экипировка);
            }
            catch
            {
                ViewBag.Message = "У данного сотрудника уже имеется экипировка, для успешного редактирования, не изменяйте фамилию сотрудника!";
                return View("Error");
            }
        }

        // GET: Используемая_экипировка/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Используемая_экипировка используемая_экипировка = await db.Используемая_экипировка.FindAsync(id);
            if (используемая_экипировка == null)
            {
                return HttpNotFound();
            }
            return View(используемая_экипировка);
        }

        // POST: Используемая_экипировка/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Используемая_экипировка используемая_экипировка = await db.Используемая_экипировка.FindAsync(id);
                db.Используемая_экипировка.Remove(используемая_экипировка);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Вы не можете удалить данную запись из-за того, что она связана с таблице Сотрудник аэродрома, для успешного удаления записи в таблице Используемая экипировка, удалите запись в таблице Сотрудник аэродрома!";
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
