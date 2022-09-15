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
using System.Data.SqlClient;

namespace MilitaryAirfield.Controllers
{
    public class ВооружениеController : Controller
    {
        private MilitaryAirfieldEntities4 db = new MilitaryAirfieldEntities4();

        // GET: Вооружение
        
        public async Task<ActionResult> Index()
        {
            return View(await db.Вооружение.ToListAsync());
        }

        
        [HttpPost]
        public ActionResult Index(string boyevay_nagruzka)
        {
            var Вооружение = db.Вооружение.AsEnumerable();
            if (boyevay_nagruzka == "Истребитель" || boyevay_nagruzka == "Вертолёт" || boyevay_nagruzka == "Бомбардировщик" || boyevay_nagruzka == "Штурмовик")
            {
                ViewBag.Nagruzka = boyevay_nagruzka;
                string connectionString = @"Data Source=LAPTOP-KLHTJHGU\SQLEXPRESS;Initial Catalog=MilitaryAirfield;Integrated Security=True";

                SqlConnection connection = new SqlConnection(connectionString);
                try
                {
                    SqlCommand command = new SqlCommand("SELECT dbo.boevaya_nagruzka(@tip)", connection);
                    command.Parameters.AddWithValue("@tip", boyevay_nagruzka);
                    connection.Open();
                        ViewBag.Nagruzka = "Общая боевая нагрузка выбранного типа воздушного судна: " + ((int)command.ExecuteScalar()).ToString() + " кг";
                }
                catch
                {
                    return View("Error");
                }
                finally
                {
                    connection.Close();
                }
                return View(Вооружение.ToList());
            }
            else {
                ViewBag.Nagruzka = "Введено неверное значение!";
                return View(Вооружение.ToList());
            }
        }
        

        // GET: Вооружение/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Вооружение вооружение = await db.Вооружение.FindAsync(id);
            if (вооружение == null)
            {
                return HttpNotFound();
            }
            return View(вооружение);
        }

        // GET: Вооружение/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Вооружение/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_вооружения_судна,Используемые_ракеты,Боевая_нагрузка_в_кг_,Количество_точек_подвески,Наличие_бомб,Стрелково_пушечное_вооружение")] Вооружение вооружение)
        {
            if (ModelState.IsValid)
            {
                db.Вооружение.Add(вооружение);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(вооружение);
        }

        // GET: Вооружение/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Вооружение вооружение = await db.Вооружение.FindAsync(id);
            if (вооружение == null)
            {
                return HttpNotFound();
            }
            return View(вооружение);
        }

        // POST: Вооружение/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_вооружения_судна,Используемые_ракеты,Боевая_нагрузка_в_кг_,Количество_точек_подвески,Наличие_бомб,Стрелково_пушечное_вооружение")] Вооружение вооружение)
        {
            if (ModelState.IsValid)
            {
                db.Entry(вооружение).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(вооружение);
        }

        // GET: Вооружение/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Вооружение вооружение = await db.Вооружение.FindAsync(id);
            if (вооружение == null)
            {
                return HttpNotFound();
            }
            return View(вооружение);
        }

        // POST: Вооружение/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Вооружение вооружение = await db.Вооружение.FindAsync(id);
            db.Вооружение.Remove(вооружение);
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
