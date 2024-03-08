using ExamenAlexanderFuela1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamenAlexanderFuela1.Controllers
{

    public class ProductoController : Controller
    {
        //para el procedimiento 
        public class ProductoTotal
        {
            public int ProductoId { get; set; }
            public string NombreProducto { get; set; }
            public int VecesComprado { get; set; }
        }
        //-----------------------------------------------------
        //-----------------------------------------------------
        public Contexto Contexto { get; }
        public ProductoController(Contexto contexto)
        {
            Contexto = contexto;
        }

        //-----------------------------------------------------
        public ActionResult MostrarProductosConCompras2()
        {
            //esto es para llamar a un procedimiento almacenado
            return View(Contexto.vistaTotal.FromSql($"EXECUTE MostrarProductosConCompras2"));
        }
        //-----------------------------------------------------
        // GET: ProductoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductoModelo productox)
        {
            Contexto.Producto.Add(productox);
            Contexto.Database.EnsureCreated();
            Contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
