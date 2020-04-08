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
    public class ProductosController : Controller
    {
        private OTTONEEntities db = new OTTONEEntities();

        // GET: Productoss
        public ActionResult Index()
        {
            var producto = db.PRODUCTO.Include(p => p.CATEGORIA1).Include(p => p.SUBCATEGORIA1);
            return View(producto.ToList());
        }

        // GET: Productoss/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTO producto = db.PRODUCTO.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productoss/Create
        public ActionResult Create()
        {
            ViewBag.CATEGORIA = new SelectList(db.CATEGORIA, "ID", "NOMBRE");
            ViewBag.SUBCATEGORIA = new SelectList(db.SUBCATEGORIA, "ID", "NOMBRE");
            return View();
        }

        // POST: Productoss/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NOMBRE,DESCRIPCION,PRECIO,MATERIAL,CATEGORIA,SUBCATEGORIA,DIRECCION_FOTO,CODIGO_PRODUCTO")] PRODUCTO producto)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCTO.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CATEGORIA = new SelectList(db.CATEGORIA, "ID", "NOMBRE", producto.CATEGORIA);

            ViewBag.SUBCATEGORIA = new SelectList(db.SUBCATEGORIA, "ID", "NOMBRE", producto.SUBCATEGORIA);
            return View(producto);
        }

        // GET: Productoss/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTO producto = db.PRODUCTO.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CATEGORIA = new SelectList(db.CATEGORIA, "ID", "NOMBRE", producto.CATEGORIA);
            ViewBag.SUBCATEGORIA = new SelectList(db.SUBCATEGORIA, "ID", "NOMBRE", producto.SUBCATEGORIA);
            return View(producto);
        }

        // POST: Productoss/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NOMBRE,DESCRIPCION,PRECIO,MATERIAL,CATEGORIA,SUBCATEGORIA,DIRECCION_FOTO,CODIGO_PRODUCTO")] PRODUCTO producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CATEGORIA = new SelectList(db.CATEGORIA, "ID", "NOMBRE", producto.CATEGORIA);
            ViewBag.SUBCATEGORIA = new SelectList(db.SUBCATEGORIA, "ID", "NOMBRE", producto.SUBCATEGORIA);
            return View(producto);
        }

        // GET: Productoss/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTO producto = db.PRODUCTO.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productoss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCTO producto = db.PRODUCTO.Find(id);
            db.PRODUCTO.Remove(producto);
            db.SaveChanges();
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
