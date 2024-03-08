namespace ParaExamen2.Models
{
    public class AlumnoAsignaturaModelo
    {
        public int Id { get; set; }

        public AlumnoModelo Alumno { get; set; }
        public int AlumnoId { get; set; }

        public AsignaturaModelo Asignatura { get; set; }
        public int AsignaturaId { get; set; }
    }
}
