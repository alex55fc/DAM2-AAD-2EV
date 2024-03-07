namespace ParaExamen2.Models
{
	public class CursoModelo
	{
		public int  Id  { get; set; }
		public string NomCurso { get; set; }

		//esto es de alumno modelo
		public List<AlumnoModelo> ListaAlumnos { get; set; }
	}
}
