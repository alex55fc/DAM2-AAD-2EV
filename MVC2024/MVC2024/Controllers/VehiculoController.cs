using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MVC2024.Models;

namespace MVC2024.Controllers
{
	public class VehiculoController : Controller
	{
		//-------------------------------------------------------------
		public Contexto Contexto { get; }

		//añadimos metodo constructor para inyectar el contexto
		public VehiculoController(Contexto contexto)
		{
			Contexto = contexto;
		}
		//-------------------------------------------------------------

		// GET: VehiculoController
		public ActionResult Index()
		{
			return View(Contexto.Vehiculo.Include(x => x.Serie).Include(x => x.Serie.Marca).ToList());
		}
		//--------------------------------------------------------------
        public ActionResult Busqueda(string busca = "")
        {
			//objeto para almacenar la busqueda en el formulario 
			ViewBag.buscar = busca;
			//
			var lista = from x in Contexto.Vehiculo.Include(x => x.Serie) where (x.Matricula.Contains(busca)) select x;
            return View(lista);
        }
        //--------------------------------------------------------------
        public ActionResult Busqueda2(string busca = "")
        {
            //objeto para almacenar la busqueda en el formulario 
            ViewBag.buscar = new SelectList(Contexto.Vehiculo, "Matricula", "Matricula", busca);
            //
            var lista = from x in Contexto.Vehiculo.Include(x => x.Serie) where (x.Matricula.Equals(busca)) select x;
            return View(lista);
        }
        //-------------------------------------------------------------
        // GET: VehiculoController/Details/5
        public ActionResult Details(int id)
		{
			return View(Contexto.Vehiculo.Include("Serie.Marca").FirstOrDefault(x => x.Id == id));
		}
		//-------------------------------------------------------------
		// GET: VehiculoController/Create
		public ActionResult Create()
		{
			ViewBag.SerieId = new SelectList(Contexto.Series, "ID", "NomSerie");
			return View();
		}
		//-------------------------------------------------------------
		// POST: VehiculoController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(VehiculoModelo vehiculo)
		{
			Contexto.Vehiculo.Add(vehiculo);
			Contexto.Database.EnsureCreated();
			Contexto.SaveChanges();

			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
		//-------------------------------------------------------------
		// GET: VehiculoController/Edit/5
		public ActionResult Edit(int id)
		{
			ViewBag.SerieId = new SelectList(Contexto.Series, "ID", "NomSerie");
			return View(Contexto.Vehiculo.Find(id));
		}
		//-------------------------------------------------------------

		// POST: VehiculoController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id,  VehiculoModelo coche)
		{
			Contexto.Vehiculo.Update(coche);
			Contexto.SaveChanges();
            /*Asi lo hizo agustin
			VehiculoModelo cocheDatosOld = Contexto.Vehiculo.Find(id)
			cocheDatosOld.Matricula = coche.Matricula
			cocheDatosOld.Color = coche.Color
			cocheDatosOld.SerieId = coche.SerieId
			Contexto.SaveChanges()
             */
            try
            {
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
		//-------------------------------------------------------------
		// GET: VehiculoController/Delete/5
		public ActionResult Delete(int id)
		{
            return View(Contexto.Vehiculo.Include("Serie.Marca").FirstOrDefault(x => x.Id == id));
        }

        // POST: VehiculoController/Delete/5
        [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			Contexto.Vehiculo.Remove(Contexto.Vehiculo.Find(id));	
			Contexto.SaveChanges();
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
		//--------------------------------------------------------------
		//ejercicio del listado de los Vehiculos
		public ActionResult Ejercicio(int marcaId = 1, int serieId= 0)
		{
            ViewBag.lasMarcas = new SelectList(Contexto.Marcas, "ID", "NomMarca", marcaId);
			ViewBag.lasSeries = new SelectList(Contexto.Series.Where(x => x.MarcaId == marcaId), "ID", "NomSerie", serieId);
            return View(Contexto.Vehiculo.ToList().Where(v => v.serieId == serieId).ToList());
        }
    }
}
