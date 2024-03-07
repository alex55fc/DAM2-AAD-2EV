using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParaExamen2.Models;

namespace ParaExamen2.Controllers
{
	public class CursoController : Controller
	{
		//-----------------------------------------------------
		//esto es para que se cree la tabla en la base de datos
		public Contexto Contexto { get; }
		public CursoController(Contexto contexto)
		{
			Contexto = contexto;
		}
		//-----------------------------------------------------

		// GET: CursoController
		public ActionResult IndexCurso()
		{
			return View(Contexto.Cursos);
		}

		//-----------------------------------------------------
		// GET: CursoController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}
		//-----------------------------------------------------
		// GET: CursoController/Create
		public ActionResult CreateCurso()
		{
			return View();
		}

		// POST: CursoController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateCurso(CursoModelo cursox)
		{
			Contexto.Cursos.Add(cursox);
			Contexto.Database.EnsureCreated();
			Contexto.SaveChanges();

			try
			{
				return RedirectToAction(nameof(IndexCurso));
			}
			catch
			{
				return View();
			}
		}
		//-----------------------------------------------------
		// GET: CursoController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: CursoController/Edit/5
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

		// GET: CursoController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: CursoController/Delete/5
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
