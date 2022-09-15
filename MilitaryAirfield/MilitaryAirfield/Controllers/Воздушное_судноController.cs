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
    public class Воздушное_судноController : Controller
    {
        private MilitaryAirfieldEntities4 db = new MilitaryAirfieldEntities4();

        // GET: Воздушное_судно
        
        public async Task<ActionResult> Index(int? RangeOfFlight, string AircraftType)
        {
            var воздушное_судно = db.Воздушное_судно.Include(в => в.Вооружение).Include(в => в.Навигационная_система).Include(в => в.Топливная_система).Include(в => в.Узлы_маневрирования);
            if (RangeOfFlight != 0 && RangeOfFlight != null)
            {
                воздушное_судно = воздушное_судно.Where(p => (p.Дальность_полёта_в_км_ >= RangeOfFlight) && (p.Дальность_полёта_в_км_ < RangeOfFlight + 1000));
            }
            if (!String.IsNullOrEmpty(AircraftType) && !AircraftType.Equals("null"))
            {
                воздушное_судно = воздушное_судно.Where(p => p.Тип_воздушного_судна == AircraftType);
            }
            return View(await воздушное_судно.ToListAsync());
        }



        // GET: Воздушное_судно/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Воздушное_судно воздушное_судно = await db.Воздушное_судно.FindAsync(id);
            if (воздушное_судно == null)
            {
                return HttpNotFound();
            }
            return View(воздушное_судно);
        }

        // GET: Воздушное_судно/Create
        public ActionResult Create()
        {
            ViewBag.ID_вооружения_судна = new SelectList(db.Вооружение, "ID_вооружения_судна", "Используемые_ракеты");
            ViewBag.ID_навигационной_системы = new SelectList(db.Навигационная_система, "ID_навигационной_системы", "ID_навигационной_системы");
            ViewBag.ID_топливной_системы = new SelectList(db.Топливная_система, "ID_топливной_системы", "ID_топливной_системы");
            ViewBag.ID_узлов_маневрирования = new SelectList(db.Узлы_маневрирования, "ID_узлов_маневрирования", "ID_узлов_маневрирования");
            return View();
        }

        // POST: Воздушное_судно/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_Воздушного_судна,Регистрационный_номер_судна,Тип_воздушного_судна,Название_воздушного_судна,Год_создания,Год_начала_эксплуатации,Работоспособность,Грузоподъемность_в_кг_,Максимальная_скорость_в_км_ч_,Длина_в_м_,Мощность_или_тяга_двигателя,Дальность_полёта_в_км_,ID_навигационной_системы,ID_топливной_системы,ID_вооружения_судна,ID_узлов_маневрирования")] Воздушное_судно воздушное_судно)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Воздушное_судно.Add(воздушное_судно);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.ID_вооружения_судна = new SelectList(db.Вооружение, "ID_вооружения_судна", "Используемые_ракеты", воздушное_судно.ID_вооружения_судна);
                ViewBag.ID_навигационной_системы = new SelectList(db.Навигационная_система, "ID_навигационной_системы", "ID_навигационной_системы", воздушное_судно.ID_навигационной_системы);
                ViewBag.ID_топливной_системы = new SelectList(db.Топливная_система, "ID_топливной_системы", "ID_топливной_системы", воздушное_судно.ID_топливной_системы);
                ViewBag.ID_узлов_маневрирования = new SelectList(db.Узлы_маневрирования, "ID_узлов_маневрирования", "ID_узлов_маневрирования", воздушное_судно.ID_узлов_маневрирования);
                return View(воздушное_судно);
            }
            catch
            {
                ViewBag.Message = "Регистрационный номер воздушного судна принимает только уникальные значения!";
                return View("Error");
            }
        }

        // GET: Воздушное_судно/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Воздушное_судно воздушное_судно = await db.Воздушное_судно.FindAsync(id);
            if (воздушное_судно == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_вооружения_судна = new SelectList(db.Вооружение, "ID_вооружения_судна", "Используемые_ракеты", воздушное_судно.ID_вооружения_судна);
            ViewBag.ID_навигационной_системы = new SelectList(db.Навигационная_система, "ID_навигационной_системы", "ID_навигационной_системы", воздушное_судно.ID_навигационной_системы);
            ViewBag.ID_топливной_системы = new SelectList(db.Топливная_система, "ID_топливной_системы", "ID_топливной_системы", воздушное_судно.ID_топливной_системы);
            ViewBag.ID_узлов_маневрирования = new SelectList(db.Узлы_маневрирования, "ID_узлов_маневрирования", "ID_узлов_маневрирования", воздушное_судно.ID_узлов_маневрирования);
            return View(воздушное_судно);
        }

        // POST: Воздушное_судно/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_Воздушного_судна,Регистрационный_номер_судна,Тип_воздушного_судна,Название_воздушного_судна,Год_создания,Год_начала_эксплуатации,Работоспособность,Грузоподъемность_в_кг_,Максимальная_скорость_в_км_ч_,Длина_в_м_,Мощность_или_тяга_двигателя,Дальность_полёта_в_км_,ID_навигационной_системы,ID_топливной_системы,ID_вооружения_судна,ID_узлов_маневрирования")] Воздушное_судно воздушное_судно)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(воздушное_судно).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.ID_вооружения_судна = new SelectList(db.Вооружение, "ID_вооружения_судна", "Используемые_ракеты", воздушное_судно.ID_вооружения_судна);
                ViewBag.ID_навигационной_системы = new SelectList(db.Навигационная_система, "ID_навигационной_системы", "ID_навигационной_системы", воздушное_судно.ID_навигационной_системы);
                ViewBag.ID_топливной_системы = new SelectList(db.Топливная_система, "ID_топливной_системы", "ID_топливной_системы", воздушное_судно.ID_топливной_системы);
                ViewBag.ID_узлов_маневрирования = new SelectList(db.Узлы_маневрирования, "ID_узлов_маневрирования", "ID_узлов_маневрирования", воздушное_судно.ID_узлов_маневрирования);
                return View(воздушное_судно);
            }
            catch
            {
                ViewBag.Message = "Регистрационный номер воздушного судна принимает только уникальные значения!";
                return View("Error");
            }

        }

        // GET: Воздушное_судно/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Воздушное_судно воздушное_судно = await db.Воздушное_судно.FindAsync(id);
                if (воздушное_судно == null)
                {
                    return HttpNotFound();
                }
                return View(воздушное_судно);
        }

        // POST: Воздушное_судно/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
        try
        {
        Воздушное_судно воздушное_судно = await db.Воздушное_судно.FindAsync(id);
        db.Воздушное_судно.Remove(воздушное_судно);
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
