using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MVC2024.Models;

namespace MVC2024.Controllers
{
	public class VehiculoController : Controller
	{
        //-------------------------------------------------------------
		//ejercicio.Crear una clase para almacenar los datos de los vehiculos de la consulta de SQL Management
		public class VehiculoTotal
		{
            public string NomMarca { get; set; }
            public string NomSerie { get; set; }
            public string  Matricula { get; set; }
            public string Color { get; set; }
        }
		public ActionResult Listado2()
		{	//esto es para llamar a una vista creada en SQL Management
			var lista = Contexto.vistaTotal.FromSql($"SELECT Marca.NomMarca, Serie.NomSerie, Vehiculo.Matricula, Vehiculo.Color FROM Marca INNER JOIN Serie ON Marca.ID = Serie.MarcaId INNER JOIN Vehiculo ON Serie.ID = Vehiculo.SerieId");
			/*hacemos una consulta a la base de datos para obtener los datos de los vehiculos de la vista creada en SQL Management
			return View(Contexto.vistaTotal.ToList());*/
			return View(lista);
		}
        //------------------------------------------------------------
        public ActionResult ListWithProcedure()
        {
			//esto es para llamar a un procedimiento almacenado
            return View(Contexto.vistaTotal.FromSql($"EXECUTE getseriesVehiculos"));
        }
        //------------------------------------------------------------
        public ActionResult ListWithProcedureAndParameter(string color = "%")
        {

			var elColor = new SqlParameter("@ColorSel", color);

            //este viewbag es para mostrar el color del vehiculo en el formulario
            /* Esta es la forma de hacerlo de Agustin
			 * ViewBag.color = new SelectList(Contexto.Vehiculo.Select(x => x.Color).Distinct(), "Color", "Color");*/
            ViewBag.color = new SelectList(Contexto.Vehiculo.Select(x => x.Color).Distinct().ToList());

            //el parametro es el color del vehiculo, el % es para que muestre todos los colores
            return View(Contexto.vistaTotal.FromSql($"EXECUTE getVehiculosPorColor {elColor}"));
        }        //-------------------------------------------------------------
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
           /* ASI lo hace agustin 
		   var vehiculos = Contexto.Vehiculo
            .Include(v => v.Serie)
            .Include(v => v.Serie.Marca)
            .Include(v => v.VehiculoExtras)
            .ThenInclude(ve => ve.Extra)
            .ToList();
            return View(vehiculos);*/
		   
            return View(Contexto.Vehiculo.Include(x => x.Serie).Include(x => x.Serie.Marca)
				.Include(x => x.VehiculoExtras).ThenInclude(xy =>xy.Extra).ToList());
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
			//hacemos un viewbag para mostrar los extras en el formulario
			ViewBag.ExtrasDeVehiculos = new MultiSelectList(Contexto.Extras, "Id", "NomExtra");
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

            foreach (var extraID in vehiculo.ExtrasSelecionados)
            {
                var obj = new VehiculoExtraModelo()
                {
                    ExtraId = extraID,
                    VehiculoId = vehiculo.Id
                };
                Contexto.VehiculoExtraModelos.Add(obj);
            }
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
			VehiculoModelo vehiculo = Contexto.Vehiculo.Find(id);
            //ejercicio de tabla M:M
			vehiculo.ExtrasSelecionados = Contexto.VehiculoExtraModelos.Where(x => x.VehiculoId == id).Select(x => x.ExtraId).ToList();
		
            ViewBag.ExtraList = new MultiSelectList(Contexto.Extras, "Id", "NomExtra", vehiculo.ExtrasSelecionados);
			return View(vehiculo);
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

			//ejercicio de tabla M:M
            var extrasActuales= Contexto.VehiculoExtraModelos.Where(x => x.VehiculoId == id);
			//Hacemos este bucle para borrar los extras actuales
			foreach (var x in extrasActuales)
			{
				Contexto.VehiculoExtraModelos.Remove(x);
			}
			
			//esto es para añadir los extras al coche, creamos un objeto de tipo VehiculoExtraModelo(tabla intermedia)
			foreach ( var extraAñadir in coche.ExtrasSelecionados)
			{
				//esto es para añadir los extras al coche
				var objVehiculoExtra = new VehiculoExtraModelo()
				{
                    ExtraId = extraAñadir,
                    VehiculoId = coche.Id
                };
				Contexto.VehiculoExtraModelos.Add(objVehiculoExtra);
			}
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
