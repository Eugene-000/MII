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
using System.Diagnostics;

namespace MilitaryAirfield.Controllers
{
    public class ПолетыController : Controller
    {
        private MilitaryAirfieldEntities4 db = new MilitaryAirfieldEntities4();

        // GET: Полеты
        public async Task<ActionResult> Index(string PlaceOfDeparture, string PlaceOfArrival)
        {
            var полеты = db.Полеты.Include(п => п.Воздушное_судно);

                if (!String.IsNullOrEmpty(PlaceOfDeparture) && !PlaceOfDeparture.Equals("null"))
                {
                    полеты = полеты.Where(p => p.Место_вылета == PlaceOfDeparture);
                }
                if (!String.IsNullOrEmpty(PlaceOfArrival) && !PlaceOfArrival.Equals("null"))
                {
                    полеты = полеты.Where(p => p.Место_прибытия == PlaceOfArrival);
                }
            
            return View(await полеты.ToListAsync());
        }
        
        [HttpPost]
        public ActionResult Index(int? kol_vo, DateTime? time_vileta, bool? boevoj_vilet, string submit, string mesto_pribitiya = "", string mesto_vileta = "")
        {
            var Полеты = db.Полеты.AsEnumerable();

            switch (submit)
            {
                case "ОК":
                    try
                    {
                        db.dobavlenie(kol_vo, mesto_pribitiya, mesto_vileta, time_vileta, boevoj_vilet);
                        db.SaveChanges();
                    }
                    catch
                    {
                        return View("Error");
                    }
                    break;
                default:
                    break;
            }
            return View(Полеты.ToList());
        }

        // GET: Полеты/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Полеты полеты = await db.Полеты.FindAsync(id);
            if (полеты == null)
            {
                return HttpNotFound();
            }
            return View(полеты);
        }

        // GET: Полеты/Create
        public ActionResult Create()
        {
            ViewBag.ID_Воздушного_судна = new SelectList(db.Воздушное_судно, "ID_Воздушного_судна", "Регистрационный_номер_судна");
            return View();
        }

        // POST: Полеты/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Номер_рейса,Место_вылета,Дата_и_время_вылета,Место_прибытия,Дата_и_время_прилета,Номер_взлетной_полосы,Расстояние_в_км_,ID_Воздушного_судна,Боевой_полет")] Полеты полеты)
        {
            if (ModelState.IsValid)
            {
                db.Полеты.Add(полеты);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Воздушного_судна = new SelectList(db.Воздушное_судно, "ID_Воздушного_судна", "Регистрационный_номер_судна", полеты.ID_Воздушного_судна);
            return View(полеты);
        }

        // GET: Полеты/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Полеты полеты = await db.Полеты.FindAsync(id);
            if (полеты == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Воздушного_судна = new SelectList(db.Воздушное_судно, "ID_Воздушного_судна", "Регистрационный_номер_судна", полеты.ID_Воздушного_судна);
            return View(полеты);
        }

        // POST: Полеты/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Номер_рейса,Место_вылета,Дата_и_время_вылета,Место_прибытия,Дата_и_время_прилета,Номер_взлетной_полосы,Расстояние_в_км_,ID_Воздушного_судна,Боевой_полет")] Полеты полеты)
        {
            if (ModelState.IsValid)
            {
                db.Entry(полеты).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Воздушного_судна = new SelectList(db.Воздушное_судно, "ID_Воздушного_судна", "Регистрационный_номер_судна", полеты.ID_Воздушного_судна);
            return View(полеты);
        }

        // GET: Полеты/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Полеты полеты = await db.Полеты.FindAsync(id);
            if (полеты == null)
            {
                return HttpNotFound();
            }
            return View(полеты);
        }

        // POST: Полеты/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Полеты полеты = await db.Полеты.FindAsync(id);
            db.Полеты.Remove(полеты);
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
