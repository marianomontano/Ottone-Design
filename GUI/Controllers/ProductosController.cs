using System;
using System.Collections;
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

        #region Get Methods
        ////GET: Productoss
        //public ActionResult Index()
        //{
        //    var productos = db.PRODUCTO.Include(p => p.CATEGORIA1).Include(p => p.SUBCATEGORIA1);

        //    return View(productos);

        //}

        //GET: Productos Filtrados
        public ActionResult Index(string _nombre, string _categoria, string _subcategoria)
        {
            var productos = db.PRODUCTO.Include(p => p.CATEGORIA1).Include(p => p.SUBCATEGORIA1);

            //cargo el selector de categorias
            var cat = from c in db.CATEGORIA
                      orderby c.NOMBRE
                      select c.NOMBRE;
            var listaCategorias = new List<string>();
            listaCategorias.AddRange(cat.Distinct());
            ViewBag._categoria = new SelectList(listaCategorias);

            //cargo el selector de subcategorias
            var subcat = from s in db.SUBCATEGORIA
                         join c in db.CATEGORIA on s.ID_CATEGORIA equals c.ID
                         where c.NOMBRE == _categoria
                         orderby s.NOMBRE
                         select s.NOMBRE;
            var listaSubcategorias = new List<string>();
            listaSubcategorias.AddRange(subcat.Distinct());
            ViewBag._subcategoria = new SelectList(listaSubcategorias);
            

            //filtro por nombre
            if(!String.IsNullOrEmpty(_nombre))
            {
                productos = productos.Where(p => p.NOMBRE.Contains(_nombre));
            }

            //filtro por categoria
            if (!String.IsNullOrEmpty(_categoria))
            {
                productos = productos.Where(p => p.CATEGORIA1 == db.CATEGORIA.FirstOrDefault(x => x.NOMBRE == _categoria));

            }

            //filtro por subcategoria
            if (!String.IsNullOrEmpty(_subcategoria))
            {
                productos = productos.Where(p => p.SUBCATEGORIA1 == db.SUBCATEGORIA.FirstOrDefault(x => x.NOMBRE == _subcategoria));
            }

            return View(productos);
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

        #endregion

        #region Post Methods

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
