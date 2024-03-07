namespace ParaExamen2.Models
{
	public class AlumnoModelo
	{
        public int Id { get; set; }
		public string NomAlumno { get; set; }
		public string ApeAlumno { get; set; }

		//esto es de curso modelo
		public CursoModelo CursoAlumno { get; set; }
		public int CursoIdAlumno { get; set; }

    }
}
