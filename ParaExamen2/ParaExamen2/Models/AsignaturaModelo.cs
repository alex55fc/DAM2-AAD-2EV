namespace ParaExamen2.Models
{
    public class AsignaturaModelo
    {
        public int Id { get; set; }        
        public string NomAsignatura { get; set; }

        public List<AlumnoAsignaturaModelo> ListaAlumnoAsignatura { get; set; }

    }
}
