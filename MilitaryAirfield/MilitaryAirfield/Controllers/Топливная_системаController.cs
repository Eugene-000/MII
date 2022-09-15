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
using System.Data.SqlClient;

namespace MilitaryAirfield.Controllers
{
    public class Топливная_системаController : Controller
    {
        private MilitaryAirfieldEntities4 db = new MilitaryAirfieldEntities4();

        // GET: Топливная_система
        
        public async Task<ActionResult> Index()
        {
            return View(await db.Топливная_система.ToListAsync());
        }

        
        [HttpPost]
        public ActionResult Index(string submit,int? id_sistemi)
        {

            var Топливная_система = db.Топливная_система.AsEnumerable();
                if(id_sistemi == null)
            {
                return View(Топливная_система.ToList());
            }
                ViewBag.Volume = id_sistemi.Value;
                string connectionString = @"Data Source=LAPTOP-KLHTJHGU\SQLEXPRESS;Initial Catalog=MilitaryAirfield;Integrated Security=True";

                SqlConnection connection = new SqlConnection(connectionString);
                try
                {
                    SqlCommand command = new SqlCommand("SELECT dbo.massa_topliva(@id_sistemi)", connection);
                    command.Parameters.AddWithValue("@id_sistemi", id_sistemi);
                    connection.Open();
                    ViewBag.Volume = "Общий объём топливных баков: " + ((int)command.ExecuteScalar()).ToString() + " литров";
                }
                catch
                {
                    return View("Error");
                }
                finally
                {
                    connection.Close();
                }
                return View(Топливная_система.ToList());
            
            
        }

        // GET: Топливная_система/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Топливная_система топливная_система = await db.Топливная_система.FindAsync(id);
            if (топливная_система == null)
            {
                return HttpNotFound();
            }
            return View(топливная_система);
        }

        
        // GET: Топливная_система/Create
        public ActionResult Create()
        {
            return View();
        }
       
        // POST: Топливная_система/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_топливной_системы,Масса_топлива_в_кг_,Вид_топлива,Способ_заправки,Количество_баков")] Топливная_система топливная_система)
        {
            if (ModelState.IsValid)
            {
                db.Топливная_система.Add(топливная_система);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(топливная_система);
        }

        // GET: Топливная_система/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Топливная_система топливная_система = await db.Топливная_система.FindAsync(id);
            if (топливная_система == null)
            {
                return HttpNotFound();
            }
            return View(топливная_система);
        }

        // POST: Топливная_система/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_топливной_системы,Масса_топлива_в_кг_,Вид_топлива,Способ_заправки,Количество_баков")] Топливная_система топливная_система)
        {
            if (ModelState.IsValid)
            {
                db.Entry(топливная_система).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(топливная_система);
        }

        // GET: Топливная_система/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Топливная_система топливная_система = await db.Топливная_система.FindAsync(id);
            if (топливная_система == null)
            {
                return HttpNotFound();
            }
            return View(топливная_система);
        }

        // POST: Топливная_система/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Топливная_система топливная_система = await db.Топливная_система.FindAsync(id);
            db.Топливная_система.Remove(топливная_система);
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
