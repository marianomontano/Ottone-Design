using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GUI.Models;

namespace GUI.Controllers
{
    public class SubcategoriasController : Controller
    {
        private OTTONEEntities db = new OTTONEEntities();

        #region Get Methods

        // GET: Subcategorias
        public ActionResult Index()
        {
            var subcategoria = db.SUBCATEGORIA.Include(s => s.CATEGORIA);
            return View(subcategoria.ToList());
        }

        // GET: Subcategorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUBCATEGORIA subcategoria = db.SUBCATEGORIA.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            return View(subcategoria);
        }

        // GET: Subcategorias/Create
        public ActionResult Create()
        {
            ViewBag.ID_CATEGORIA = new SelectList(db.CATEGORIA, "ID", "NOMBRE");
            return View();
        }

        // GET: Subcategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUBCATEGORIA subcategoria = db.SUBCATEGORIA.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CATEGORIA = new SelectList(db.CATEGORIA, "ID", "NOMBRE", subcategoria.ID_CATEGORIA);
            return View(subcategoria);
        }

        // GET: Subcategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUBCATEGORIA subcategoria = db.SUBCATEGORIA.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            return View(subcategoria);
        }

        #endregion

        #region Post Methods

        // POST: Subcategorias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NOMBRE,ID_CATEGORIA")] SUBCATEGORIA subcategoria)
        {
            if (ModelState.IsValid)
            {
                db.SUBCATEGORIA.Add(subcategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CATEGORIA = new SelectList(db.CATEGORIA, "ID", "NOMBRE", subcategoria.ID_CATEGORIA);
            return View(subcategoria);
        }

        // POST: Subcategorias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NOMBRE,ID_CATEGORIA")] SUBCATEGORIA subcategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subcategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CATEGORIA = new SelectList(db.CATEGORIA, "ID", "NOMBRE", subcategoria.ID_CATEGORIA);
            return View(subcategoria);
        }

        // POST: Subcategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUBCATEGORIA subcategoria = db.SUBCATEGORIA.Find(id);
            db.SUBCATEGORIA.Remove(subcategoria);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

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
