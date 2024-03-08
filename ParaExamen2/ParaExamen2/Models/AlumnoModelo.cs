using System.ComponentModel.DataAnnotations.Schema;

namespace ParaExamen2.Models
{
	public class AlumnoModelo
	{
        public int Id { get; set; }
		public string NomAlumno { get; set; }
		public string ApeAlumno { get; set; }

		//esto es de curso modelo
		public CursoModelo CursoAlumno { get; set; }
		public int CursoAlumnoId { get; set; }

		//esto es de alumnoasignatura modelo relacion M.M
		[NotMapped]
		public List<int> AsignaturasSeleccionadas { get; set; }
		public List<AlumnoAsignaturaModelo> ListaAlumnoAsignatura { get; set; }

    }
}
	