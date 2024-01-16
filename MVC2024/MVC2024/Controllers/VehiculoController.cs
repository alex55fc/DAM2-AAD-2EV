using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        //-------------------------------------------------------------
        // GET: VehiculoController/Details/5
        public ActionResult Details(int id)
		{
			return View();
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
			return View();
		}

		// POST: VehiculoController/Edit/5
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

		// GET: VehiculoController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: VehiculoController/Delete/5
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
