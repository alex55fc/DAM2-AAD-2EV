using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParaExamen2.Models;

namespace ParaExamen2.Controllers
{
	public class AlumnoController : Controller
	{
		//-----------------------------------------------------
		public Contexto Contexto { get; }
		public AlumnoController(Contexto contexto)
		{
			Contexto = contexto;
		}

		//-----------------------------------------------------
		// GET: AlumnoController
		public ActionResult IndexAlumno()
		{
			return View(Contexto.Alumnos.Include(x => x.CursoAlumno).ToList());
		}

		// GET: AlumnoController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}
		//-----------------------------------------------------
		// GET: AlumnoController/Create
		public ActionResult CreateAlumno()
		{
			//esto es para tener guardado el id de todos los cursos en un viewbag
			ViewBag.CursosAlmacenados = new SelectList(Contexto.Cursos, "Id", "NomCurso");
			return View();
		}

		// POST: AlumnoController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateAlumno(AlumnoModelo alumnox)
		{
			Contexto.Alumnos.Add(alumnox);
			Contexto.Database.EnsureCreated();
			Contexto.SaveChanges();

			try
			{
				return RedirectToAction(nameof(IndexAlumno));
			}
			catch
			{
				return View();
			}
		}
		//-----------------------------------------------------
		// GET: AlumnoController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: AlumnoController/Edit/5
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

		// GET: AlumnoController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: AlumnoController/Delete/5
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
